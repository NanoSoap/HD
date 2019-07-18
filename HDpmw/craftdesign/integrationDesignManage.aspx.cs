using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDBusiness;
using HDPages.craftLib;
using HDPages.productLib;
using Newtonsoft.Json.Linq;
using YDCode;

namespace HDpmw.craftdesign
{
    public partial class integrationDesignManage : PageBase
    {

        private string GetDeleteScript_step()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid3.GetDeleteSelectedRowsReference(), String.Empty);
        }

        bool AppendToEnd = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                setPageInit1();
                setPageInit2();
                setPageInit3();
                setDropListInit();
                setPageContent(1);
                inittree_first();
                inittree_second();
                //Grid2
                string deleteScript_batch = GetDeleteScript_batch();
                JObject batchObj = new JObject();
                batchObj.Add("batchnumber", "");
                batchObj.Add("batchtext", "");
                batchObj.Add("workshop", "");
                batchObj.Add("batchsession", "");
                batchObj.Add("bdevice", "");
                batchObj.Add("btool", "");
                batchObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript_batch, IconHelper.GetResolvedIconUrl(Icon.Delete)));
                // 新增一条数据（新增数据后定位到[工序内容]列）
                //toolnew.OnClientClick = "setPageContent1(5);";
                toolnew.OnClientClick += Grid2.GetAddNewRecordReference(batchObj, AppendToEnd, "batchtext");
                toolreset.OnClientClick = Confirm.GetShowReference("确定要重置表格数据？", String.Empty, Grid2.GetRejectChangesReference(), String.Empty);

                //Grid3
                string deleteScript_step = GetDeleteScript_step();
                JObject stepObj = new JObject();
                stepObj.Add("batchid", "");
                stepObj.Add("snumber", "");
                stepObj.Add("stext", "");
                stepObj.Add("stool", "");
                stepObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript_step, IconHelper.GetResolvedIconUrl(Icon.Delete)));
                // 新增一条数据（新增数据后定位到[工步内容]列）
                //toolNewStep.OnClientClick = "setPageContent2(5);";

                toolNewStep.OnClientClick = Grid3.GetAddNewRecordReference(stepObj, AppendToEnd, "stext");
                toolResetStep.OnClientClick = Confirm.GetShowReference("确定要重置表格数据？", String.Empty, Grid3.GetRejectChangesReference(), String.Empty);



            }
            //隐藏按钮防误操
            toolnew.Hidden = true;
            toolreset.Hidden = true;
            toolsave.Hidden = true;
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                toolnew.Hidden = false;
                toolreset.Hidden = false;
                toolsave.Hidden = false;
            }
            toolNewStep.Hidden = true;
            toolResetStep.Hidden = true;
            toolSaveStep.Hidden = true;

            if (Grid2.SelectedRowIndexArray.Length > 0)
            {
                toolNewStep.Hidden = false;
                toolResetStep.Hidden = false;
                toolSaveStep.Hidden = false;
            }
        }

        #region 工艺卡初始化
        private void initbaseinfo_product(string strID)
        {
            if (strID.Trim() != "")
            {
                pd_product pd = new pd_product();
                DataTable dt = pd.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    kitname.Text = r["pdname"].ToString().Trim();
                    kitcode.Text = r["pdcode"].ToString().Trim();
                    mcode.Text = getCardCode();
                }
            }
        }

        private void initbaseinfo_compongall(string strID)
        {
            if (strID.Trim() != "")
            {
                pd_compongall comp = new pd_compongall();
                DataTable dt = comp.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    kitname.Text = r["comname"].ToString().Trim();
                    kitcode.Text = r["comcode"].ToString().Trim();
                    mcode.Text = getCardCode();

                }
            }
        }
        void setDropListInit()
        {
            string[] desp = new xparams().getparamData("h010").Split(',');
            string[] audp = new xparams().getparamData("h009").Split(',');
            string[] bname = new xparams().getparamData("h014").Split(',');
            string[] wkshop = new xparams().getparamData("h004").Split(',');
            designperson.DataSource = desp;
            designperson.DataBind();
            auditperson.DataSource = audp;
            auditperson.DataBind();
            batchnameEditor.DataSource = bname;
            batchnameEditor.DataBind();
            workshopEditor.DataSource = wkshop;
            workshopEditor.DataBind();
        }

        #endregion

        #region 树初始化
        private void inittree_first()
        {
            firsttree.Nodes.Clear();
            // 模拟从数据库返回数据表
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbyproduct();

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["pid"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("pid"))
                {
                    FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                    node.Text = row["nodetext"].ToString();
                    node.NodeID = row["id"].ToString().Trim();

                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());

                    node.EnableClickEvent = true;


                    firsttree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }

        private void inittree_second()
        {
            secondtree.Nodes.Clear();
            // 模拟从数据库返回数据表
            pd_compongall pm = new pd_compongall();
            DataTable table = pm.getBindTreeDataAsdt();

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["pid"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("pid"))
                {
                    FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                    node.Text = row["nodetext"].ToString();
                    node.NodeID = row["id"].ToString();

                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                    //node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    if ((row["isclick"].ToString().Trim() == "1") || (row["isclick"].ToString().Trim() == "2"))
                    {
                        node.EnableClickEvent = true;
                    }

                    secondtree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void ResolveSubTree(DataRow dataRow, FineUIPro.TreeNode treeNode)
        {
            DataRow[] rows = dataRow.GetChildRows("TreeRelation");

            if (rows.Length > 0)
            {
                // 如果是目录，则默认展开
                treeNode.Expanded = true;

                foreach (DataRow row in rows)
                {
                    FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                    node.Text = row["nodetext"].ToString();
                    node.NodeID = row["id"].ToString();
                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());

                    if ((row["isclick"].ToString().Trim() == "1") || (row["isclick"].ToString().Trim() == "2"))
                    {
                        node.EnableClickEvent = true;
                    }

                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        #endregion

        #region 树遍历
        private void getnodesum(FineUIPro.TreeNode node)
        {
            int originnumber = 0;
            int firstnumber = 0;
            int secondnumver = 0;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                FineUIPro.TreeNode first_treenode = node.Nodes[i];

                for (int j = 0; j < first_treenode.Nodes.Count; j++)
                {
                    FineUIPro.TreeNode second_treenode = first_treenode.Nodes[j];

                    if (second_treenode.Nodes.Count > 0)
                    {
                        secondnumver += second_treenode.Nodes.Count;
                        second_treenode.Text += " (" + second_treenode.Nodes.Count + ")";
                        firstnumber += secondnumver;
                        secondnumver = 0;
                    }
                }

                if (firstnumber > 0)
                {
                    first_treenode.Text += " (" + firstnumber.ToString() + ")";
                    originnumber += firstnumber;
                    firstnumber = 0;
                }
            }

            node.Text += " (" + originnumber.ToString() + ")";
        }

        private void setrepeat()
        {
            pd_machinekit_feature pmf = new pd_machinekit_feature();

            System.Data.DataTable dt = pmf.checkrepeat();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    FineUIPro.TreeNode treenode = firsttree.FindNode(r["pid"].ToString().ToUpper().Trim());

                    treenode.CssClass = "repeatnode";

                    treenode.ParentNode.CssClass = "repeatnode";
                }
            }
        }

        private void traversaltree(FineUIPro.TreeNode treenode, string strf_mtypename, string strf_mname)
        {
            foreach (FineUIPro.TreeNode tempnode in treenode.Nodes)
            {
                if (tempnode.Text == strf_mname)
                {
                    List<string> selects = new List<string>(firsttree.SelectedNodeIDArray);
                    selects.Add(tempnode.NodeID);
                    firsttree.SelectedNodeIDArray = selects.ToArray();
                    return;
                }

                traversaltree(tempnode, strf_mtypename, strf_mname);
            }
        }

        protected void firsttree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2" && treenode.Text.Trim() != "克隆零件")
            {

                initbaseinfo_product(treenode.NodeID);
                BindGridNode();
                //BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);

            }
            toolnew.Hidden = true;
            toolsave.Hidden = true;
            toolreset.Hidden = true;
            toolNewStep.Hidden = true;
            toolResetStep.Hidden = true;
            toolSaveStep.Hidden = true;
            Grid2.DataSource = null;
            Grid2.DataBind();
            Grid3.DataSource = null;
            Grid3.DataBind();
        }

        protected void secondtree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = secondtree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {

                initbaseinfo_compongall(treenode.NodeID);
                BindGridNode();
                //initphoto(treenode.NodeID);
            }

            toolnew.Hidden = true;
            toolsave.Hidden = true;
            toolreset.Hidden = true;
            toolNewStep.Hidden = true;
            toolResetStep.Hidden = true;
            toolSaveStep.Hidden = true;
            Grid2.DataSource = null;
            Grid2.DataBind();
            Grid3.DataSource = null;
            Grid3.DataBind();
        }

        #endregion

        protected void btnBatch_Click(object sender, EventArgs e)
        {

        }

        #region 工艺卡表格
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {

        }
        protected void setPageContent(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());

            if (intType == 1)
            {
                CurPage.Text = "1";
                BindGrid();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                MemoTxt.Text = "第 1 页 共 " + intTotalPage.ToString() + " 页 " + TotalPage.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage.Text = intCurPage.ToString();

                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                CurPage.Text = intTotalPage.ToString();
                MemoTxt.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage.Text.Trim() + " 条数据";
                BindGrid();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage.Text = intCurPage.ToString();

                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }
        }
        private void setPageInit1()
        {
            GridPageSize.Text = "21";
            CurPage.Text = "";
            TotalPage.Text = "";
            MemoTxt.Text = "";
        }
        private void BindGrid()
        {


            cd_integration ig = new cd_integration();
            string strcardname = Fcardname.Text.Trim();
            string strkitname = Fkitname.Text.Trim();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("iname", strcardname);
            dic.Add("kitname", strkitname);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = Grid1.SortField;
            string strSortDirection = Grid1.SortDirection;

            DataTable dt = ig.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ig.getBindDataAsdt(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();

            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        private void BindGridNode()
        {
            cd_integration ig = new cd_integration();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            Tree tree = TabStrip1.ActiveTabIndex == 0 ? firsttree : secondtree;
            dic.Add("kitID", tree.SelectedNode.NodeID);

            int intPageindex = 1;// Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = Grid1.SortField;
            string strSortDirection = Grid1.SortDirection;

            DataTable dt = ig.getBindDataAsdtNode(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ig.getBindDataAsdtNode(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();

            Grid1.DataSource = dt;
            Grid1.DataBind();
            CurPage.Text = "1";
        }
        #endregion

        #region 工序表格

        protected void setPageContent1(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize1.Text.Trim());

            if (intType == 1)
            {
                CurPage1.Text = "1";
                BindGrid1();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                MemoTxt1.Text = "第 1 页 共 " + intTotalPage.ToString() + " 页 " + TotalPage1.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage1.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage1.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                        MemoTxt1.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage1.Text.Trim() + " 条数据";
                        BindGrid1();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage1.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage1.Text = intCurPage.ToString();

                        MemoTxt1.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage1.Text.Trim() + " 条数据";
                        BindGrid1();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                CurPage1.Text = intTotalPage.ToString();
                MemoTxt1.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage1.Text.Trim() + " 条数据";
                BindGrid1();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage1.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage1.Text = intCurPage.ToString();

                        MemoTxt1.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage1.Text.Trim() + " 条数据";
                        BindGrid1();
                    }
                }
            }
        }

        private void BindGrid1()
        {
            cd_integration_batch bh = new cd_integration_batch();

            if (Grid1.SelectedRowIndexArray.Length > 0)
            {

                string strid = Grid1.DataKeys[Grid1.SelectedRowIndexArray[0]][0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("iid", strid);

                int intPageindex1 = Convert.ToInt32(CurPage1.Text.Trim());
                int intPagesize1 = Convert.ToInt32(GridPageSize1.Text.Trim());
                string strSort1 = "systemdate";//Grid2.SortField;
                string strSortDirection1 = Grid2.SortDirection;

                DataTable dt = bh.getBindDataAsdt(dic, strSort1, strSortDirection1, intPagesize1, intPageindex1);
                DataTable dt1 = bh.getBindDataAsdt(dic, strSort1, strSortDirection1);
                TotalPage1.Text = dt1.Rows.Count.ToString();

                Grid2.DataSource = dt;
                Grid2.DataBind();

            }
            toolNewStep.Hidden = true;
            toolResetStep.Hidden = true;
            toolSaveStep.Hidden = true;
            Grid3.DataSource = null;
            Grid3.DataBind();
        }

        private void setPageInit2()
        {
            GridPageSize1.Text = "21";
            CurPage1.Text = "";
            TotalPage1.Text = "";
            MemoTxt1.Text = "";
        }

        #endregion

        #region 工段表格

        private void BindGrid3()
        {
            cd_integration_batch_step step = new cd_integration_batch_step();

            if (Grid2.SelectedRowIndexArray.Length > 0)
            {

                string strbatchid = Grid2.DataKeys[Grid2.SelectedRowIndexArray[0]][0].ToString();



                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("batchid", strbatchid);

                int intPageindex2 = Convert.ToInt32(CurPage2.Text.Trim());
                int intPagesize2 = Convert.ToInt32(GridPageSize2.Text.Trim());
                //string strSort2 = "systemdate";//Grid2.SortField;
                //string strSortDirection2 = Grid2.SortDirection;

                DataTable dt = step.getBindDataAsdt(dic, Grid3.SortField, Grid3.SortDirection, intPagesize2, intPageindex2);
                DataTable dt2 = step.getBindDataAsdt(dic, Grid3.SortField, Grid3.SortDirection);
                TotalPage2.Text = dt2.Rows.Count.ToString();

                Grid3.DataSource = dt;
                Grid3.DataBind();

            }
            else
            {
                TotalPage2.Text = "1";
            }

        }
        private void setPageInit3()
        {
            GridPageSize2.Text = "21";
            CurPage2.Text = "";
            TotalPage2.Text = "";
            MemoTxt2.Text = "";
        }
        protected void setPageContent2(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize2.Text.Trim());

            if (intType == 1)
            {
                CurPage2.Text = "1";
                BindGrid3();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                MemoTxt2.Text = "第 1 页 共 " + intTotalPage.ToString() + " 页 " + TotalPage2.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage2.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage2.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                        MemoTxt2.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage2.Text.Trim() + " 条数据";
                        BindGrid3();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage2.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage2.Text = intCurPage.ToString();

                        MemoTxt2.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage2.Text.Trim() + " 条数据";
                        BindGrid3();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                CurPage2.Text = intTotalPage.ToString();
                MemoTxt2.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage2.Text.Trim() + " 条数据";
                BindGrid3();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage2.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage2.Text = intCurPage.ToString();

                        MemoTxt2.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage2.Text.Trim() + " 条数据";
                        BindGrid3();
                    }
                }
            }
        }



        #endregion

        #region 分页-1

        protected void FirstPage_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }

        protected void PrePage_Click(object sender, EventArgs e)
        {
            setPageContent(2);
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            setPageContent(3); ;
        }

        protected void LastPage_Click(object sender, EventArgs e)
        {
            setPageContent(4); ;
        }

        protected void GoPage_Click(object sender, EventArgs e)
        {
            setPageContent(5); ;
        }

        protected void SubNumber_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #region 分页-2
        protected void FirstPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(1);
        }
        protected void PrePage_Click1(object sender, EventArgs e)
        {
            setPageContent1(2);
        }
        protected void NextPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(3); ;
        }
        protected void LastPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(4); ;
        }
        protected void GoPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(5); ;
        }
        protected void SubNumber_Click1(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize1.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize1.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click1(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize1.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize1.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #region 分页-3
        protected void FirstPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(1);
        }

        protected void PrePage_Click2(object sender, EventArgs e)
        {
            setPageContent2(2);
        }
        protected void NextPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(3); ;
        }
        protected void LastPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(4); ;
        }
        protected void GoPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(5); ;
        }
        protected void SubNumber_Click2(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize2.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize2.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click2(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize2.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize2.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #region 工艺卡菜单
        private void erase()
        {
            editID.Text = "";
            mname.Text = "";
            //kitname.Text = "";
            //kitcode.Text = "";
            //mcode.Text = "";
            mtag.Text = "";
            //rawtype.Text = "";
            //rawsize.Text = "";
            //nperdesk.Text = "";
            //nperraw.Text = "";
            designdate.Text = "";
            designperson.SelectedValue = "";
            auditdate.Text = "";
            auditperson.SelectedValue = "";
            normaldate.Text = "";
            meetdate.Text = "";
            //f_mname.Text = "";

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            toolnew.Hidden = true;
            toolreset.Hidden = true;
            toolsave.Hidden = true;
            Grid2.DataSource = null;
            Grid2.DataBind();
            setPageContent(1);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Tree tree = TabStrip1.ActiveTabIndex == 1 ? secondtree : firsttree;

            if (tree.SelectedNode != null&&tree.SelectedNode.Attributes.GetValue("nodemenu").ToString()==("2"))
            {
                if (!new cd_integration().existCard(tree.SelectedNode.NodeID))
                {
                    erase();
                    //btnClone.Hidden = false;
                    //f_mname.Hidden = false;
                    btnRefresh.Enabled = true;
                    //btnClone.Enabled = true;
                    //f_mname.Enabled = true;
                    neweditWindow.Hidden = false;
                }
                else
                {
                    Alert.Show("当前产品/组件已有工艺卡请进行编辑！");
                }

            }
            else
            {
                Alert.Show("请先选择产品/组件！");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = Grid1.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择编辑记录");

                return;
            }

            initinfo(Grid1.DataKeys[intRowindexarray[0]][0].ToString().Trim());
            //btnClone.Hidden = true;
            //f_mname.Hidden = true;
            neweditWindow.Hidden = false;
        }

        private void initinfo(string strID)
        {
            cd_integration ig = new cd_integration();
            DataTable dt = ig.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            mname.Text = r["iname"].ToString().Trim();
            kitname.Text = r["kitname"].ToString();
            kitcode.Text = r["kitcode"].ToString();
            mcode.Text = r["icode"].ToString();
            mtag.Text = r["itag"].ToString();
            //rawtype.Text = r["rawtype"].ToString();
            //rawsize.Text = r["rawsize"].ToString();
            //nperraw.Text = r["nperraw"].ToString();
            //nperdesk.Text = r["nperdesk"].ToString();
            designperson.SelectedValue = r["designperson"].ToString();
            designdate.Text = r["designdate"].ToString();
            auditperson.SelectedValue = r["auditperson"].ToString();
            auditdate.Text = r["auditdate"].ToString();
            normaldate.Text = r["normaldate"].ToString();
            meetdate.Text = r["meetdate"].ToString();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = Grid1.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                cd_integration mc = new cd_integration();
                object[] keys = Grid1.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = mc.deletebycondition("cd_integration", dic);


                setPageContent(1);
                Grid2.DataSource = null;
                Grid2.DataBind();
                Grid3.DataSource = null;
                Grid3.DataBind();


                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "成功移除数据";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据移除失败";
                }

                alert.Show();
            }
            else
            {
                Alert.Show("请先选择工艺卡！");
            }
        }

        #endregion

        #region 工艺卡新增页面-按钮
        protected void btnSave_Click(object sender, EventArgs e)

        {

            Dictionary<string, string> dicIntegration = initDataDicIntegration();
            string strID = editID.Text.ToString().Trim();
            cd_integration ig = new cd_integration();
            int intresultRecord = 0;
            Tree tree = TabStrip1.ActiveTabIndex == 1 ? secondtree : firsttree;
            if (strID == "")
            {
                string mechanicalDesignID = Guid.NewGuid().ToString();
                dicIntegration.Add("ID", mechanicalDesignID);
                dicIntegration.Add("isdelid", "1");
                dicIntegration.Add("pid", tree.SelectedNode.NodeID);
                //dicMechanicalDesign.Add("mid", Grid1.DataKeys[Grid2.SelectedRowIndexArray[0]][0].ToString());
                string str_mname = dicIntegration["iname"].ToString().Trim();
                string str_mcode = dicIntegration["icode"].ToString().Trim();
                if ((ig.isExistdata("cd_integration", "iname", str_mname, "*").Trim() != "") && (ig.isExistdata("cd_integration", "icode", str_mcode, "*").Trim() != ""))
                {
                    Alert.Show(" 该名称或编号已经存在!");
                }
                else
                {
                    intresultRecord = ig.add(dicIntegration, "cd_integration");
                    setPageContent(1);
                }
            }
            else
            {
                intresultRecord = ig.update(dicIntegration, "cd_integration", "ID", strID);

            }


            if (CurPage.Text.Trim() == "")
            {
                setPageContent(1);
            }
            else
            {
                setPageContent(5);
            }

            Alert alert = new Alert();

            if (intresultRecord == 1)
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存成功";

            }
            else if (intresultRecord == 0)
            {
                alert.MessageBoxIcon = MessageBoxIcon.Error;
                alert.Message = "数据保存失败";

            }



            alert.Show();
            setPageContent(1);
        }

        private Dictionary<string, string> initDataDicIntegration()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("iname", mname.Text.ToString().Trim());
            dic.Add("kitname", kitname.Text);
            dic.Add("kitcode", kitcode.Text);
            dic.Add("icode", mcode.Text);
            dic.Add("itag", mtag.Text);
            //dic.Add("rawtype", rawtype.Text);
            //dic.Add("rawsize", rawsize.Text);
            //dic.Add("nperraw", nperraw.Text);
            //dic.Add("nperdesk", nperdesk.Text);
            dic.Add("designperson", designperson.SelectedValue == null ? "" : designperson.SelectedValue);
            dic.Add("designdate", designdate.Text);
            dic.Add("auditperson", auditperson.SelectedValue == null ? "" : auditperson.SelectedValue);
            dic.Add("auditdate", auditdate.Text);
            dic.Add("normaldate", normaldate.Text);
            dic.Add("meetdate", meetdate.Text);
            dic.Add("operater", SessionUserName);
            dic.Add("systemdate", DateTime.Now.ToString());

            return dic;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            string strID = editID.Text.ToString().Trim();

            if (strID.Trim() == "")
            {
                erase();
            }
            else
            {
                initinfo(strID);
            }
        }

        #endregion

        #region 工序表格-按钮


        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent1(1);
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            LinkButtonField deleteField = Grid2.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript_batch();
        }

        private string GetDeleteScript_batch()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
        }

        protected void toolsave_Click(object sender, EventArgs e)
        {
            cd_integration_batch bh = new cd_integration_batch();
            int update = 0;
            int delete = 0;
            int news = 0;
            if (Grid2.GetModifiedData().Count == 0)
            {
                //labResult.Text = "";
                Alert.Show("表格数据没有变化！");
                return;
            }

            // 修改的现有数据
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid2.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                string rowID = Grid2.DataKeys[rowIndex][0].ToString();
                Dictionary<string, object> dic = modifiedDict[rowIndex];

                update += bh.update(objToString(dic), "cd_integration_batch", "ID", rowID);
            }


            // 删除现有数据
            List<int> deletedRows = Grid2.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                string[] sqls = new string[2];
                string rowID = Grid2.DataKeys[rowIndex][0].ToString();
                //bh.delete("cd_integration_batch","ID",rowID);

                //bh.delete("cd_integration_batch_step", "batchid", rowID);
                sqls[0] = createSql.DeleteDatasql("cd_integration_batch", "ID", rowID);
                sqls[1] = createSql.DeleteDatasql("cd_integration_batch_step", "batchid", rowID);
                delete += DBSQL.ExecutesqlMutriTobool(sqls, DBSQL.connstr);

            }


            // 新增数据
            List<Dictionary<string, object>> newAddedList = Grid2.GetNewAddedList();
            //systemdate adder
            int i = 0;
            foreach (var item in newAddedList)
            {
                Dictionary<string, string> dic = objToString(item);
                dic.Add("ID", Guid.NewGuid().ToString());
                dic.Add("iid", Grid1.DataKeys[Grid1.SelectedRowIndexArray[0]][0].ToString());
                dic.Add("iname", Grid1.DataKeys[Grid1.SelectedRowIndexArray[0]][1].ToString());
                dic.Add("operater", SessionUserName);
                dic.Add("systemdate", DateTime.Now.AddMilliseconds(i * 1000).ToString());
                dic.Add("isdelid", "1");
                news += bh.add(dic, "cd_integration_batch");
                i++;
            }

            //if (AppendToEnd)
            //{
            //    for (int i = 0; i < newAddedList.Count; i++)
            //    {
            //        DataRow rowData = CreateNewData(table, newAddedList[i]);
            //        table.Rows.Add(rowData);
            //    }
            //}
            //else
            //{
            //    for (int i = newAddedList.Count - 1; i >= 0; i--)
            //    {
            //        DataRow rowData = CreateNewData(table, newAddedList[i]);
            //        table.Rows.InsertAt(rowData, 0);
            //    }
            //}


            //labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", Grid2.GetModifiedData().ToString(Newtonsoft.Json.Formatting.Indented));


            BindGrid1();

            Alert.Show(string.Format("更新{0}条，删除{1}条，新增{2}条！", update, delete, news));
        }
        //转换为string pair

        Dictionary<string, string> objToString(Dictionary<string, object> dic)
        {
            Dictionary<string, string> strdic = new Dictionary<string, string>();

            foreach (var item in dic)
            {
                strdic.Add(item.Key, (string)item.Value);
            }
            return strdic;
        }
        #endregion

        #region 工步表格-按钮



        protected void Grid3_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void Grid3_PreDataBound(object sender, EventArgs e)
        {
            LinkButtonField deleteField = Grid3.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript_batch_step();
        }

        private string GetDeleteScript_batch_step()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid3.GetDeleteSelectedRowsReference(), String.Empty);
        }

        protected void toolSaveStep_Click(object sender, EventArgs e)
        {
            int update = 0;
            int delete = 0;
            int news = 0;
            cd_integration_batch_step step = new cd_integration_batch_step();
            if (Grid3.GetModifiedData().Count == 0)
            {
                //labResult.Text = "";
                Alert.Show("表格数据没有变化！");
                return;
            }

            // 修改的现有数据
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid3.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                string rowID = Grid3.DataKeys[rowIndex][0].ToString();
                Dictionary<string, object> dic = modifiedDict[rowIndex];

                update += step.update(objToString(dic), "cd_integration_batch_step", "ID", rowID);
            }


            // 删除现有数据
            List<int> deletedRows = Grid3.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                string rowID = Grid3.DataKeys[rowIndex][0].ToString();
                delete += step.delete("cd_integration_batch_step", "ID", rowID);
            }


            // 新增数据
            List<Dictionary<string, object>> newAddedList = Grid3.GetNewAddedList();
            int i = 0;
            foreach (var item in newAddedList)
            {
                Dictionary<string, string> dic = objToString(item);
                dic.Add("ID", Guid.NewGuid().ToString());
                dic.Add("batchid", Grid2.DataKeys[Grid2.SelectedRowIndexArray[0]][0].ToString());
                dic.Add("operater", SessionUserName);
                dic.Add("systemdate", DateTime.Now.AddMilliseconds(i * 1000).ToString());
                dic.Add("isdelid", "1");
                news += step.add(dic, "cd_integration_batch_step");
                i++;
            }

            //if (AppendToEnd)
            //{
            //    for (int i = 0; i < newAddedList.Count; i++)
            //    {
            //        DataRow rowData = CreateNewData(table, newAddedList[i]);
            //        table.Rows.Add(rowData);
            //    }
            //}
            //else
            //{
            //    for (int i = newAddedList.Count - 1; i >= 0; i--)
            //    {
            //        DataRow rowData = CreateNewData(table, newAddedList[i]);
            //        table.Rows.InsertAt(rowData, 0);
            //    }
            //}


            //labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", Grid3.GetModifiedData().ToString(Newtonsoft.Json.Formatting.Indented));


            BindGrid3();
            Alert.Show(string.Format("更新{0}条，删除{1}条，新增{2}条！", update, delete, news));

        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {

            setPageContent2(1);
        }

        //protected void btnClone_Click(object sender, EventArgs e)
        //{
        //    string strAlert = TabStrip1.ActiveTabIndex == 1 ? "组件" : "产品";
        //    Tree tree = TabStrip1.ActiveTabIndex == 1 ? secondtree : firsttree;
        //    cd_integration mc = new cd_integration();
        //    DataTable dt = mc.getClonedata(f_mname.Text);
        //    if (dt.Rows.Count == 0)
        //    {
        //        Alert.Show(string.Format("未找到该{0}或该{0}无工艺卡！", strAlert));
        //    }
        //    else
        //    {
        //        int result = 0;
        //        List<string> sqls = new List<string>();
        //        DataRow r = dt.Rows[0];
        //        //
        //        mname.Text = r["iname"].ToString() + "（克隆）";
        //        mtag.Text = r["itag"].ToString();
        //        //rawtype.Text = r["rawtype"].ToString();
        //        //rawsize.Text = r["rawsize"].ToString();
        //        //nperraw.Text = r["nperraw"].ToString();
        //        //nperdesk.Text = r["nperdesk"].ToString();
        //        mcode.Text = getCardCode();
        //        //
        //        string ID_mechanicalCard = Guid.NewGuid().ToString();
        //        Dictionary<string, string> dic_mechanical = initDataDicIntegration();
        //        dic_mechanical.Add("ID", ID_mechanicalCard);
        //        dic_mechanical.Add("pid", tree.SelectedNode.NodeID);
        //        mc.add(dic_mechanical, "cd_integration");

        //        //工序克隆
        //        cd_integration_batch bh = new cd_integration_batch();
        //        if (bh.clone(r["ID"].ToString(), ID_mechanicalCard))
        //        {
        //            //工步克隆
        //            cd_integration_batch_step step = new cd_integration_batch_step();

        //            string[] tobatch = bh.getnewIDlist(ID_mechanicalCard);
        //            string[] frombatch = bh.getfromIDlist(r["ID"].ToString());
        //            for (int i = 0; i < frombatch.Length; i++)
        //            {
        //                sqls.Add(step.clone(frombatch[i], tobatch[i]));
        //            }
        //            result = DBSQL.ExecutesqlMutriTobool(sqls.ToArray(), DBSQL.connstr);
        //        };
        //        if (result > 1)
        //        {
        //            Alert.Show("克隆成功！");
        //            editID.Text = ID_mechanicalCard;
        //            btnRefresh.Enabled = false;
        //            //btnClone.Enabled = false;
        //            //f_mname.Enabled = false;
        //        }
        //        else
        //        {
        //            Alert.Show("克隆失败！");
        //            erase();
        //        }
        //        setPageContent(1);
        //    }
        //}


        //生成工艺卡编号函数
        private string getCardCode()
        {

            return new xparams().getparamData("H015").Split(',')[5].Split('|')[1] + Rand.Number(8);
        }

        protected void toolnew_Click(object sender, EventArgs e)
        {
            setPageContent2(5);
        }

        protected void toolNewStep_Click(object sender, EventArgs e)
        {

        }

        protected void btnmenuClone_Click(object sender, EventArgs e)
        {
            string tabName = TabStrip1.ActiveTabIndex == 0 ? "产品" : "组件";
            Tree tree = TabStrip1.ActiveTabIndex == 1 ? secondtree : firsttree;
            cd_integration ig = new cd_integration();
            strCloneID.Text = TabStrip1.ActiveTabIndex + tree.SelectedNode.NodeID;
            DataTable dt = ig.getClonedata(strCloneID.Text.Remove(0, 1));

            if (dt.Rows.Count == 0)
            {
                Alert.Show(string.Format("该{0}无工艺卡！", tabName));
                strCloneID.Text = "";
            }
            else
            {
                Alert.Show("成功！请粘贴");

            }
        }

        protected void btnmenuPaste_Click(object sender, EventArgs e)
        {
            Tree tree = TabStrip1.ActiveTabIndex == 1 ? secondtree : firsttree;
            DataTable dt0 = new cd_integration().getClonedata(tree.SelectedNode.NodeID);
            string tabName = TabStrip1.ActiveTabIndex == 0 ? "产品" : "组件";


            if (strCloneID.Text == "")
            {
                Alert.Show("请先进行克隆！");

            }
            else
            if (dt0.Rows.Count != 0)
            {
                Alert.Show(string.Format("该{0}已有工艺卡！", tabName));

            }
            else
            {
                if (TabStrip1.ActiveTabIndex != int.Parse(strCloneID.Text.Substring(0, 1)))
                {
                    Alert.Show(("不能跨类别粘贴工艺卡！"));

                }
                else
                {

                    //string strAlert = TabStrip1.ActiveTabIndex == 1 ? "组件" : "产品";
                    cd_integration ig = new cd_integration();
                    DataTable dt = ig.getClonedata(strCloneID.Text.Substring(1));
                    
                    {
                        int result = 0;
                        List<string> sqls = new List<string>();
                        DataRow r = dt.Rows[0];
                        //
                        mname.Text = r["iname"].ToString() + "（克隆）";
                        mtag.Text = r["itag"].ToString();
                        //rawtype.Text = r["rawtype"].ToString();
                        //rawsize.Text = r["rawsize"].ToString();
                        //nperraw.Text = r["nperraw"].ToString();
                        //nperdesk.Text = r["nperdesk"].ToString();
                        mcode.Text = getCardCode();
                        //
                        string ID_mechanicalCard = Guid.NewGuid().ToString();
                        Dictionary<string, string> dic_mechanical = initDataDicIntegration();
                        dic_mechanical.Add("ID", ID_mechanicalCard);
                        dic_mechanical.Add("pid", tree.SelectedNode.NodeID);
                        ig.add(dic_mechanical, "cd_integration");

                        //工序克隆
                        cd_integration_batch bh = new cd_integration_batch();
                        if (bh.clone(r["ID"].ToString(), ID_mechanicalCard))
                        {
                            //工步克隆
                            cd_integration_batch_step step = new cd_integration_batch_step();

                            string[] tobatch = bh.getnewIDlist(ID_mechanicalCard);
                            string[] frombatch = bh.getfromIDlist(r["ID"].ToString());
                            for (int i = 0; i < frombatch.Length; i++)
                            {
                                sqls.Add(step.clone(frombatch[i], tobatch[i]));
                            }
                            result = DBSQL.ExecutesqlMutriTobool(sqls.ToArray(), DBSQL.connstr);
                        };
                        if (result > 1)
                        {
                            Alert.Show("克隆成功！");
                            editID.Text = ID_mechanicalCard;
                            btnRefresh.Enabled = false;
                            //btnClone.Enabled = false;
                            //f_mname.Enabled = false;
                        }
                        else
                        {
                            Alert.Show("克隆失败！");
                            erase();
                        }
                        setPageContent(1);
                    }

                }
            }
        }
        #endregion
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HDBusiness;
using FineUIPro;

namespace HDpmw.systemInfo
{
    public partial class UserMenu : PageBase
    {
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initpage();
            }
        }

        private void initpage()
        {
            setPageInit();
            inittree();
        }

        #region 树初始化
        private void inittree()
        {
            // 模拟从数据库返回数据表
            systemMenu sm = new systemMenu();
            DataTable table = sm.getBindTreeDataAsdt();

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
                    node.EnableCheckEvent = true;
                    firsttree.Nodes.Add(node);
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
                    node.EnableCheckEvent = true;
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        #endregion

        private void BindGrid()
        {
            string strf_username = f_username.Text.Trim();
            string strf_address = f_address.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("username", strf_username);
            dic.Add("address", strf_address);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            userLogin ul = new userLogin();
            DataTable dt = ul.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ul.getBindDataAsdt(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();

            mainGrid.DataSource = dt;
            mainGrid.DataBind();
        }

        #endregion

        #region 树处理

        protected void firsttree_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                firsttree.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                firsttree.UncheckAllNodes(e.Node.Nodes);
            }
        }

        #endregion

        #region 主窗口菜单

        protected void btnFind_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择一个或多个用户");

                return;
            }

            userMenu um = new userMenu();
            List<Dictionary<string, string>> listddic = new List<Dictionary<string, string>>();

            for (int i = 0; i < intRowindexarray.Length; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("username", mainGrid.DataKeys[intRowindexarray[i]][1].ToString().Trim());
                listddic.Add(dic);
            }

            um.deleteMutri("userMenu", listddic);

            int intresult = 0;
            List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

            FineUIPro.TreeNode[] nodes = firsttree.GetCheckedNodes();
            if (nodes.Length > 0)
            {
                foreach (FineUIPro.TreeNode node in nodes)
                {
                    if (node.Leaf)
                    {
                        for (int i = 0; i < intRowindexarray.Length; i++)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("ID", Guid.NewGuid().ToString().Trim());
                            dic.Add("username", mainGrid.DataKeys[intRowindexarray[i]][1].ToString().Trim());

                            int k;
                            if (System.Int32.TryParse(node.ParentNode.NodeID, out k))
                            {
                                dic.Add("menuid", node.NodeID.ToString().Trim());
                            }
                            else
                            {
                                dic.Add("menuid", node.ParentNode.NodeID.ToString().Trim());
                                dic.Add("buttonid", node.NodeID.ToString().Trim());
                            }

                            listdic.Add(dic);
                        }
                    }
                }

                intresult = um.addMutri(listdic, "userMenu");
            }

            Alert alert = new Alert();

            if (intresult > 0)
            {
                alert.Icon = Icon.Information;
                alert.Message = "用户菜单分配成功";
            }
            else
            {
                alert.MessageBoxIcon = MessageBoxIcon.Error;
                alert.Message = "用户菜单分配失败";
            }

            alert.Show();
        }

        #endregion

        #region 表格处理
        
        protected void mainGrid_RowDoubleClick(Object sender, GridRowClickEventArgs e)
        {
            displaydetailinfo(mainGrid.DataKeys[e.RowIndex][1].ToString());
        }

        private void displaydetailinfo(string strusername)
        {
            userMenu um = new userMenu();
            System.Data.DataTable dt = um.getEditdata(strusername);

            string strhtml = "空白详细信息";
            firsttree.UncheckAllNodes(); 

            if (dt != null && dt.Rows.Count > 0)
            {
                strhtml = "<div style=\"line-height:27px;\">" +
                    "用户名：" + strusername;
                string strmenuparent = "";
                string strmenuname = "";
                foreach (System.Data.DataRow r in dt.Rows)
                {
                    if (strmenuparent != r["menuparent"].ToString().Trim())
                    {
                        //&thinsp;                      
                        strhtml += "<br/>" + r["menuparent"].ToString().Trim() + "<br/>" +
                                r["menuname"].ToString().Trim() + " || " + r["functionname"].ToString().Trim() + " ";

                        strmenuparent = r["menuparent"].ToString().Trim();
                        strmenuname = r["menuname"].ToString().Trim();
                    }
                    else
                    {
                        if (strmenuname != r["menuname"].ToString().Trim())
                        {
                            strhtml += "<br/>" + r["menuname"].ToString().Trim() + " || " + r["functionname"].ToString().Trim() + " ";

                            strmenuname = r["menuname"].ToString().Trim();
                        }
                        else
                        {
                            strhtml += r["functionname"].ToString().Trim() + " ";
                        }
                    }

                    //绑定树
                    isGo = false;
                    if (r["buttonid"].ToString().Trim() == "")
                    {
                        treeRecursive(firsttree.Nodes[0], r["menuid"].ToString().Trim());
                    }
                    else
                    {
                        treeRecursive(firsttree.Nodes[0], r["buttonid"].ToString().Trim());
                    }
                }

                strhtml += "</div>";
            }

            detailinfo.Text = strhtml;
        }

        private bool isGo=false;
        private void  treeRecursive(FineUIPro.TreeNode treenode, string strnodeid)
        {
            if(isGo)
            { return; }

            if (treenode.NodeID.Trim().ToLower() == strnodeid)
            {
                isGo = true;
                treenode.Checked = true;
                treenode.ParentNode.Checked = true;
                return;
            }
            else
            {
                if (treenode.Nodes.Count > 0)
                {
                    foreach (FineUIPro.TreeNode item in treenode.Nodes)
                    {
                        treeRecursive(item, strnodeid);
                    }
                }
            }
        }

        protected void mainGrid_Sort(object sender, GridSortEventArgs e)
        {
            setPageContent(1);
        }

        #region 分页-0

        private void setPageInit()
        {
            GridPageSize.Text = "21";
            CurPage.Text = "";
            TotalPage.Text = "";
            MemoTxt.Text = "";
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

        #endregion
    }
}
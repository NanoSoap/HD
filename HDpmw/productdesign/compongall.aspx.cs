using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDPages.productLib;
using HDBusiness;
using YDCode;
using System.Data;
using System.IO;

namespace HDpmw.productdesign
{
    public partial class compongall : PageBase
    {
        const string DEFAULT_IMAGEPATH = "~/res/images/blank.png";
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initui();
            }
        }

        private void initui()
        {
            erase();
            initdesigner();
            initexaminer();
            inittree();
        }

        private void initexaminer()
        {
            xparams x = new xparams();

            string str = x.getparamData("H009");
            string str1 = x.getparamData("H017");
            examiner.DataSource = commonLib.stringTolist(str, ',');
            examiner.DataBind();
            comtype.DataSource = commonLib.stringTolist(str1, ',');
            comtype.DataBind();
        }

        private void initdesigner()
        {
            xparams x = new xparams();

            string str = x.getparamData("H010");

            designer.DataSource = commonLib.stringTolist(str, ',');
            designer.DataBind();

            stanarder.DataSource = commonLib.stringTolist(str, ',');
            stanarder.DataBind();

            drawer.DataSource = commonLib.stringTolist(str, ',');
            drawer.DataBind();

            checker.DataSource = commonLib.stringTolist(str, ',');
            checker.DataBind();
        }

        #region 树初始化
        private void inittree()
        {
            firsttree.Nodes.Clear();
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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    node.EnableClickEvent = true;

                    firsttree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }

            //getnodesum(firsttree.Nodes[0]);

            //setrepeat();
        }

        private void ResolveSubTree(DataRow dataRow, FineUIPro.TreeNode treeNode)
        {
            pd_compongall_compose com = new pd_compongall_compose();
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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    node.EnableClickEvent = true;
                    DataTable dt = com.getEditdataByID(txtcomID.Text.Trim(), row["id"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        node.Checked = true;
                    }

                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittree1()
        {
            machinekittree.Nodes.Clear();
            pd_machinekit pm = new pd_machinekit();
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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    node.EnableClickEvent = true;

                    machinekittree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittree2()
        {
            machinekittree.Nodes.Clear();
            pd_compongall pm = new pd_compongall();
            DataTable table = pm.getBindTreeDataAsdtbystandard();
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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    node.EnableClickEvent = true;

                    machinekittree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittree3()
        {
            machinekittree.Nodes.Clear();
            pd_compongall pm = new pd_compongall();
            DataTable table = pm.getBindTreeDataAsdtbyoutbuy();
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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    node.EnableClickEvent = true;

                    machinekittree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        #endregion

        #endregion
        #region 树处理
        #endregion
        #region 主菜单操作
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_compongall pm = new pd_compongall();
                string strpid = treenode.NodeID.ToString().Trim();

                if (pm.isExistdata("pd_compongall", "ID", strpid, "ID").Trim() == "")
                {
                    Alert.Show("请先保存组件基本信息");
                    return;
                }
                try
                {
                    if (filePhoto.HasFile)
                    {
                        string fileName = filePhoto.ShortFileName;

                        if (!ValidFileType(fileName))
                        {
                            // 清空文件上传控件
                            filePhoto.Reset();

                            Alert.Show("无效的文件类型！");
                            return;
                        }

                        fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                        int lastDotIndex = fileName.LastIndexOf(".");
                        string strfiletype = fileName.Substring(lastDotIndex + 1).ToLower();
                        string strpname = fileName.Substring(0, lastDotIndex);
                        fileName = DateTime.Now.Ticks.ToString() + "." + getSuffix(fileName);

                        filePhoto.SaveAs(Server.MapPath("~/resources/photo/" + fileName));

                        imgPhoto.ImageUrl = "~/resources/photo/" + fileName;

                        // 清空文件上传组件（上传后要记着清空，否则点击提交表单时会再次上传！！）
                        filePhoto.Reset();
                        Dictionary<string, string> dic = initPhotodic(strpid, strpname, fileName, strfiletype);

                        try
                        {
                            pd_photo pp = new pd_photo();
                            string strfilename = pp.isExistdata("pd_photo", "pid", strpid, "filename");

                            if (strfilename.Trim() != "")
                            {
                                strfilename = Server.MapPath("~/resources/photo/") + strfilename;

                                if (File.Exists(strfilename))
                                {
                                    File.Delete(strfilename);
                                }

                                pp.update(dic, "pd_photo", "pid", strpid);
                            }
                            else
                            {
                                dic.Add("ID", Guid.NewGuid().ToString());
                                dic.Add("pid", strpid);
                                pp.add(dic, "pd_photo");
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {

                }
            }
        }
        private bool ValidFileType(string fileName)
        {
            xparams x = new xparams();
            string[] suffixs = x.getparamData("H008").Split(',');
            bool valid = false;


            foreach (var item in suffixs)
            {
                if (item.Equals(getSuffix(fileName)))
                {
                    valid = true;
                }
            }
            return valid;
        }
        //将文件名转换为大写并返回后缀名
        string getSuffix(string s)
        {
            string[] filenameSplit = s.ToUpper().Split('.');
            return filenameSplit[filenameSplit.Length - 1];
        }
        protected void btnFind_Click(object sender, EventArgs e)
        {
            string strf_mname = f_mname.Text.Trim();
            string strf_mtypename = "";

            traversaltree(firsttree.Nodes[0], strf_mtypename, strf_mname);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (comtype.SelectedItem == null)
            {
                Alert.Show("请选择类型！");
                return;
            }
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strID = treenode.NodeID.ToString().Trim();
                string strmtypeid = treenode.ParentNode.NodeID.Trim();

                Dictionary<string, string> dic = initDatadic();
                pd_compongall pm = new pd_compongall();
                int intresult = 0;
                if (pm.isExistdata("pd_compongall", "ID", strID, "ID").Trim() == "")
                {
                    if (pm.isExistdata("comname", dic["comname"].ToString().Trim(), strID, false).Trim() == "" &&
                        pm.isExistdata("comcode", dic["comcode"].ToString().Trim(), strID, false).Trim() == "")
                    {
                        dic.Add("ID", strID);
                        intresult = pm.add(dic, "pd_compongall");
                    }
                    else
                    {
                        Alert.Show("名称或编码已经存在");
                        return;
                    }
                }
                else
                {
                    if (pm.isExistdata("comname", dic["comname"].ToString().Trim(), strID, true).Trim() == "" &&
                        pm.isExistdata("comcode", dic["comcode"].ToString().Trim(), strID, true).Trim() == "")
                    {
                        intresult = pm.update(dic, "pd_compongall", "cast(ID as varchar(36))", strID);
                        
                    }
                    else
                    {
                        Alert.Show("名称或编码已经存在");
                        return;
                    }
                }

                inittree();

                Alert alert = new Alert();

                if (intresult == 1)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "数据保存成功";
                    imgPhoto.Reset();
                    filePhoto.Reset();
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据保存失败";
                    filePhoto.Reset();
                    imgPhoto.Reset();
                }

                alert.Show();
            }
        }
        #endregion

        #region 树目录操作-右键菜单

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            string strID = treenode.NodeID.Trim();

            //需检查零件在组件和产品表中是否存在
            if (strID != "")
            {
                pd_product_compose ppc = new pd_product_compose();
                if (ppc.isExistdata("pd_product_compose", "ID", strID, "ID").Trim() != "")
                {
                    Alert.Show("当前组件不能删除");
                    return;
                }
                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                List<string> listTablename = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("pd_compongall", "cast(ID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_compongall");

                dic = new Dictionary<string, string>();
                dic.Add("pd_compongall_compose", "cast(componID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_compongall_compose");

                dic = new Dictionary<string, string>();
                dic.Add("pd_3d", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_3d");

                dic = new Dictionary<string, string>();
                dic.Add("pd_blueprint", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_blueprint");

                dic = new Dictionary<string, string>();
                dic.Add("pd_photo", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_photo");

                dic = new Dictionary<string, string>();
                dic.Add("pd_cad", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_cad");

                int intresult = 0;

                pd_compongall pm = new pd_compongall();
                intresult = pm.deleteMutri(listdic, listTablename);

                inittree();

                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "数据删除成功";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据删除失败";
                }

                alert.Show();
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
        private void traversaltree1(FineUIPro.TreeNode treenode, string strf_mtypename, string strf_mname)
        {
            foreach (FineUIPro.TreeNode tempnode in treenode.Nodes)
            {
                if (tempnode.Text == strf_mname)
                {
                    List<string> selects = new List<string>(machinekittree.SelectedNodeIDArray);
                    selects.Add(tempnode.NodeID);
                    machinekittree.SelectedNodeIDArray = selects.ToArray();
                    return;
                }

                traversaltree1(tempnode, strf_mtypename, strf_mname);
            }
        }
        protected void firsttree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2" && treenode.Text.Trim() != "克隆组件")
            {
                initbaseinfo(treenode.NodeID);
                BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);
                //初始化图片
                initphoto(treenode.NodeID);
            }
        }

        protected void menuAdd_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                FineUIPro.TreeNode newtreenode = new FineUIPro.TreeNode();

                string newmenutxt = "新组件";
                newtreenode.Text = "<input style=\"color:darkblue;width:81px;border:none;\" value=\"" + newmenutxt + "\" " +
                        "onblur=\"ontxtBlur(this.value)\"></input>";
                comname.Text = newmenutxt;

                comcode.Text = getmcode(treenode.NodeID);

                string strGUID = Guid.NewGuid().ToString().Trim();
                newtreenode.NodeID = strGUID;
                BindGrid(treenode.NodeID, strGUID);
                newtreenode.Attributes.Add("nodemenu", "2");
                newtreenode.EnableClickEvent = true;

                treenode.Nodes.Add(newtreenode);
                treenode.Expanded = true;
            }
        }
        

        protected void menuClone_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                FineUIPro.TreeNode newtreenode = new FineUIPro.TreeNode();

                string newmenutxt = "克隆组件";
                newtreenode.Text = newmenutxt;
                string strID = treenode.NodeID;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                pd_compongall pm = new pd_compongall();
                string strGUID = Guid.NewGuid().ToString().Trim();
                txtcomID.Text = strGUID;
                int intresult = 0;
                DataTable dt = pm.getEditdata(strID);
                DataRow r = dt.Rows[0];
                dic.Add("ID", strGUID);
                dic.Add("comname", newmenutxt);
                dic.Add("comcode", getmcode(treenode.ParentNode.NodeID));
                dic.Add("remarks", r["remarks"].ToString().Trim());
                dic.Add("standards", r["standards"].ToString().Trim());
                dic.Add("designer", r["designer"].ToString().Trim());
                dic.Add("checker", r["checker"].ToString().Trim());
                dic.Add("examiner", r["examiner"].ToString().Trim());
                dic.Add("drawer", r["drawer"].ToString().Trim());
                dic.Add("stanarder", r["stanarder"].ToString().Trim());
                dic.Add("comtype", r["comtype"].ToString().Trim());
                dic.Add("specifications", r["specifications"].ToString().Trim());
                dic.Add("drawerdate", DateTime.Now.ToString());
                dic.Add("isdelid", "1");
                dic.Add("operater", SessionUserName);
                dic.Add("systemdate", DateTime.Now.ToString());
                comname.Text = r["comname"].ToString().Trim();
                if (pm.isExistdata("comname", dic["comname"].ToString().Trim(), strID, false).Trim() == "" &&
                        pm.isExistdata("comcode", dic["comcode"].ToString().Trim(), strID, false).Trim() == "")
                {
                    Dictionary<int, Dictionary<string, object>> modifiedDict = mainGrid.GetModifiedDict();

                    pd_compongall_compose pmf = new pd_compongall_compose();
                    DataTable dr = pmf.getEditdataByID(strID);
                    List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        DataRow r1 = dr.Rows[i];
                        Dictionary<string, string> dic1 = new Dictionary<string, string>();
                        dic1.Add("ID", Guid.NewGuid().ToString().Trim());
                        dic1.Add("componID", strGUID);
                        dic1.Add("composeID", r1["composeID"].ToString().Trim());
                        dic1.Add("number", r1["number"].ToString().Trim());
                        dic1.Add("remarks", r1["remarks"].ToString().Trim());
                        dic1.Add("type", r1["type"].ToString().Trim());
                        dic1.Add("operater", SessionUserName);
                        listdic.Add(dic1);
                    }
                    string[] sqltext = new string[listdic.Count + 1];

                    sqltext[0] = pmf.AddDatasql("pd_compongall_compose", dic);
                    string[] tempsql = createSql.getinsrtsqlarray("pd_compongall_compose", listdic);
                    for (int i = 0; i < tempsql.Length; i++)
                    {
                        sqltext[i + 1] = tempsql[i];
                    }

                    intresult = pmf.ExecMutri(sqltext);
                }
                else
                {
                    Alert.Show("名称或编码已经存在");
                    return;
                }
                if (intresult > 0)
                {
                    initbaseinfo(strGUID);
                    BindGrid(treenode.ParentNode.NodeID, strGUID);


                    newtreenode.NodeID = strGUID;
                    newtreenode.Attributes.Add("nodemenu", "2");
                    newtreenode.EnableClickEvent = true;

                    treenode.ParentNode.Nodes.Add(newtreenode);
                    treenode.ParentNode.Expanded = true;
                }
                else
                {
                    Alert.Show("程序错误请刷新重试！");
                    return;
                }
            }
        }

        protected void menuLookup_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                initbaseinfo(treenode.NodeID);
                BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);
                //初始化图片
                initphoto(treenode.NodeID);
            }
        }

        protected void cadLookup_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_cad pc = new pd_cad();
                string strfilename = pc.isExistdata("pd_cad", "pid", treenode.NodeID, "filename");

                //GrapkTabStrip.ActiveTabIndex = 2;

                PageContext.RegisterStartupScript("openwebcad('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + strfilename + "')");
            }
        }

        #endregion

        #region 图纸操作-保存/上传/查看

        private string getmcode(string strmtypeid)
        {
            pd_compongall pm = new pd_compongall();

            string strmcode = pm.getmaxmcode(strmtypeid);
            string strprecode = "com";

            if (strmcode.Length == 1)
            {
                strmcode = strprecode + "00" + strmcode;
            }
            else if (strmcode.Length == 2)
            {
                strmcode = strprecode + "0" + strmcode;
            }
            else
            {
                strmcode = strprecode + strmcode;
            }

            return strmcode;
        }

        private void erase()
        {
            comname.Text = "";
            comcode.Text = "";
            designer.SelectedIndex = -1;
            checker.SelectedIndex = -1;
            stanarder.SelectedIndex = -1;
            drawer.SelectedIndex = -1;
            examiner.SelectedIndex = -1;
            specifications.Text = "";
            txtnumber.Text = "0";
            drawerdate.SelectedDate = System.DateTime.Now;
            imgPhoto.ImageUrl = DEFAULT_IMAGEPATH;
            filePhoto.Reset();
        }

        private void initbaseinfo(string strID)
        {
            if (strID.Trim() != "")
            {
                pd_compongall pm = new pd_compongall();
                System.Data.DataTable dt = pm.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    comname.Text = r["comname"].ToString().Trim();
                    comcode.Text = r["comcode"].ToString().Trim();
                    drawerdate.SelectedDate = System.DateTime.Parse(r["drawerdate"].ToString().Trim());
                    designer.SelectedValue = r["designer"].ToString().Trim();
                    checker.SelectedValue = r["checker"].ToString().Trim();
                    stanarder.SelectedValue = r["stanarder"].ToString().Trim();
                    drawer.SelectedValue = r["drawer"].ToString().Trim();
                    examiner.SelectedValue = r["examiner"].ToString().Trim();
                    comtype.SelectedValue = r["comtype"].ToString().Trim();
                    specifications.Text = r["specifications"].ToString().Trim();
                }
            }
        }
        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("comname", comname.Text.Trim());
            dic.Add("comcode", comcode.Text.Trim());

            if (designer.SelectedItem != null)
            {
                dic.Add("designer", designer.SelectedValue.Trim());
            }

            if (checker.SelectedItem != null)
            {
                dic.Add("checker", checker.SelectedValue.Trim());
            }

            if (stanarder.SelectedItem != null)
            {
                dic.Add("stanarder", stanarder.SelectedValue.Trim());
            }

            if (drawer.SelectedItem != null)
            {
                dic.Add("drawer", drawer.SelectedValue.Trim());
            }

            if (examiner.SelectedItem != null)
            {
                dic.Add("examiner", examiner.SelectedValue.Trim());
            }
            if (comtype.SelectedItem != null)
            {
                dic.Add("comtype", comtype.SelectedValue.Trim());
            }
            dic.Add("drawerdate", drawerdate.SelectedDate.ToString());
            dic.Add("isdelid", "1");
            dic.Add("specifications", specifications.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", DateTime.Now.ToString());
            return dic;
        }
        private Dictionary<string, string> initDatadicPhoto()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("pname", comname.Text.Trim());
            dic.Add("pcode", comcode.Text.Trim());
            dic.Add("filename", imgPhoto.ImageUrl.Remove(0, imgPhoto.ImageUrl.Length - 22));
            dic.Add("filetype", getSuffix(imgPhoto.ImageUrl));
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName);
            dic.Add("systemdate", DateTime.Now.ToString());
            return dic;
        }
        protected void GrapkTabStrip_TabIndexChanged(object sender, EventArgs e)
        {
            if (TabStrip1.ActiveTabIndex == 0)
            {
                ;
            }
            else if (TabStrip1.ActiveTabIndex == 1)
            {
                //imgPhoto.ImageUrl= "../resources/photo/法兰.png";
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
            else if (TabStrip1.ActiveTabIndex == 2)
            {
                ;
            }
            else if (TabStrip1.ActiveTabIndex == 3)
            {
                Label3.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
        }
        protected void TabStrip2_TabIndexChanged(object sender, EventArgs e)
        {
            if (TabStrip2.ActiveTabIndex == 0)
            {
                ;
            }
            else if (TabStrip2.ActiveTabIndex == 1)
            {
                //imgPhoto.ImageUrl= "../resources/photo/法兰.png";
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
            else if (TabStrip2.ActiveTabIndex == 2)
            {
                ;
            }
            else if (TabStrip2.ActiveTabIndex == 3)
            {
                Label3.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
        }
        //CAD图纸
        protected void filemxcad_FileSelected(object sender, EventArgs e)
        {
            //FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            //if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            //{
            //    try
            //    {
            //        if (filemxcad.HasFile)
            //        {
            //            string fileName = filemxcad.ShortFileName;

            //            if (!commonLib.ValidateCADType(fileName))
            //            {
            //                Alert.Show("无效的文件类型！");
            //                return;
            //            }

            //            fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
            //            int lastDotIndex = fileName.LastIndexOf(".");
            //            string strfiletype = fileName.Substring(lastDotIndex + 1).ToLower();
            //            string strpname = fileName.Substring(0, lastDotIndex);
            //            fileName = DateTime.Now.Ticks.ToString() + fileName.Substring(lastDotIndex).Trim();

            //            filemxcad.SaveAs(Server.MapPath("~/resources/cad/" + fileName));

            //            // 清空文件上传组件
            //            filemxcad.Reset();

            //            string strpid = treenode.NodeID.ToString().Trim();
            //            Dictionary<string, string> dic = initCADdic(strpid, strpname, fileName, strfiletype);

            //            try
            //            {
            //                pd_cad pc = new pd_cad();
            //                string strfilename = pc.isExistdata("pd_cad", "pid", strpid, "filename");

            //                if (strfilename.Trim() != "")
            //                {
            //                    strfilename = Server.MapPath("~/resources/cad/") + strfilename;

            //                    if (File.Exists(strfilename))
            //                    {
            //                        File.Delete(strfilename);
            //                    }

            //                    pc.update(dic, "pd_cad", "pid", strpid);
            //                }
            //                else
            //                {
            //                    dic.Add("ID", Guid.NewGuid().ToString());
            //                    dic.Add("pid", strpid);
            //                    pc.add(dic, "pd_cad");
            //                }


            //                PageContext.RegisterStartupScript("openwebcad('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + fileName + "')");
            //            }
            //            catch
            //            {

            //            }
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        private Dictionary<string, string> initCADdic(string strpid, string strmname, string fileName, string strfiletype)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mcode", strmname);
            dic.Add("mname", strmname);
            dic.Add("filename", fileName);
            dic.Add("filetype", strfiletype);
            dic.Add("operater", SessionUserName);

            return dic;
        }

        private Dictionary<string, string> initPhotodic(string strpid, string strpname, string fileName, string strfiletype)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pcode", strpname);
            dic.Add("pname", strpname);
            dic.Add("filename", fileName);
            dic.Add("filetype", strfiletype);
            dic.Add("operater", SessionUserName);

            return dic;
        }

        private void initphoto(string strpid)
        {
            pd_photo pp = new pd_photo();
            string strfilename = pp.isExistdata("pd_photo", "pid", strpid, "filename");

            if (strfilename.Trim() != "")
            {
                imgPhoto.ImageUrl = "~/resources/photo/" + strfilename;
            }
            else
            {
                imgPhoto.ImageUrl = "~/resources/photo/法兰.png";
            }
        }

        #endregion

        #region 表格操作

        private void BindGrid(string strmtypeid, string strtID)
        {
            pd_compongall_compose pmf = new pd_compongall_compose();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("componID", strtID);
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;
            System.Data.DataTable dt = pmf.getBindDataAsdt(dic, strSort, strSortDirection, "元零件");
            DataTable dt1 = pmf.getBindDataAsdt(dic, strSort, strSortDirection, "标准件");
            DataTable dt2 = pmf.getBindDataAsdt(dic, strSort, strSortDirection, "外购件");
            DataTable dt3 = pmf.getBindDataAsdt(dic, strSort, strSortDirection, "元组件");
            mainGrid.DataSource = dt;
            mainGrid.DataBind();
            Grid1.DataSource = dt1;
            Grid1.DataBind();
            Grid2.DataSource = dt2;
            Grid2.DataBind();
        }
        protected void btnAddcom1_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strpid = treenode.NodeID;
                txtcomID.Text = strpid;
                txtnumber.Text = "1";
                machinekittreewin.Hidden = false;
                inittree1();
                //PageContext.RegisterStartupScript("searchmk(1)");
            }
        }
        protected void btnAddcom2_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strpid = treenode.NodeID;
                txtcomID.Text = strpid;
                txtnumber.Text = "2";
                machinekittreewin.Hidden = false;
                inittree2();
            }
        }
        protected void btnAddcom3_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strpid = treenode.NodeID;
                txtcomID.Text = strpid;
                txtnumber.Text = "3";
                machinekittreewin.Hidden = false;
                inittree3();
            }
        }
        protected void btnSavecom1_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<int, Dictionary<string, object>> modifiedDict = mainGrid.GetModifiedDict();

                pd_compongall_compose pmf = new pd_compongall_compose();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

                string strpid = treenode.NodeID;

                for (int i = 0; i < mainGrid.Rows.Count; i++)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    dic.Add("componID", strpid);
                    dic.Add("composeID", mainGrid.DataKeys[i][0].ToString().Trim());
                    dic.Add("number", "");
                    dic.Add("remarks", "");
                    dic.Add("type", "元零件");
                    dic.Add("isdelid", "1");
                    dic.Add("operater", SessionUserName);
                    dic.Add("systemdate", DateTime.Now.ToString());
                    foreach (int rowindex in modifiedDict.Keys)
                    {
                        if (i == rowindex)
                        {
                            Dictionary<string, object> row = modifiedDict[rowindex];

                            dic["number"] = row["number"].ToString().Trim();
                            dic["remarks"] = row["remarks"].ToString().Trim();
                            break;
                        }
                    }
                    listdic.Add(dic);
                }
                int intresult = 0;
                string[] sqltext = new string[listdic.Count + 1];

                sqltext[0] = pmf.DeleteDatasql("pd_compongall_compose", "componID", strpid, "type", "元零件");
                string[] tempsql = createSql.getinsrtsqlarray("pd_compongall_compose", listdic);
                for (int i = 0; i < tempsql.Length; i++)
                {
                    sqltext[i + 1] = tempsql[i];
                }

                intresult = pmf.ExecMutri(sqltext);

                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "数据保存成功";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据保存失败";
                }

                alert.Show();
            }
        }
        protected void btnSavecom2_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();

                pd_compongall_compose pmf = new pd_compongall_compose();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

                string strpid = treenode.NodeID;

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    dic.Add("componID", strpid);
                    dic.Add("composeID", Grid1.DataKeys[i][0].ToString().Trim());
                    dic.Add("number", "");
                    dic.Add("remarks", "");
                    dic.Add("type", "标准件");
                    dic.Add("isdelid", "1");
                    dic.Add("operater", SessionUserName);
                    dic.Add("systemdate", DateTime.Now.ToString());
                    foreach (int rowindex in modifiedDict.Keys)
                    {
                        if (i == rowindex)
                        {
                            Dictionary<string, object> row = modifiedDict[rowindex];

                            dic["number"] = row["number"].ToString().Trim();
                            dic["remarks"] = row["remarks"].ToString().Trim();
                            break;
                        }
                    }
                    listdic.Add(dic);
                }
                int intresult = 0;
                string[] sqltext = new string[listdic.Count + 1];

                sqltext[0] = pmf.DeleteDatasql("pd_compongall_compose", "componID", strpid, "type", "标准件");
                string[] tempsql = createSql.getinsrtsqlarray("pd_compongall_compose", listdic);
                for (int i = 0; i < tempsql.Length; i++)
                {
                    sqltext[i + 1] = tempsql[i];
                }

                intresult = pmf.ExecMutri(sqltext);

                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "数据保存成功";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据保存失败";
                }

                alert.Show();
            }
        }
        protected void btnSavecom3_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid2.GetModifiedDict();

                pd_compongall_compose pmf = new pd_compongall_compose();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

                string strpid = treenode.NodeID;

                for (int i = 0; i < Grid2.Rows.Count; i++)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    dic.Add("componID", strpid);
                    dic.Add("composeID", Grid2.DataKeys[i][0].ToString().Trim());
                    dic.Add("number", "");
                    dic.Add("remarks", "");
                    dic.Add("type", "外购件");
                    dic.Add("isdelid", "1");
                    dic.Add("operater", SessionUserName);
                    dic.Add("systemdate", DateTime.Now.ToString());
                    foreach (int rowindex in modifiedDict.Keys)
                    {
                        if (i == rowindex)
                        {
                            Dictionary<string, object> row = modifiedDict[rowindex];

                            dic["number"] = row["number"].ToString().Trim();
                            dic["remarks"] = row["remarks"].ToString().Trim();
                            break;
                        }
                    }
                    listdic.Add(dic);
                }
                int intresult = 0;
                string[] sqltext = new string[listdic.Count + 1];

                sqltext[0] = pmf.DeleteDatasql("pd_compongall_compose", "componID", strpid, "type", "外购件");
                string[] tempsql = createSql.getinsrtsqlarray("pd_compongall_compose", listdic);
                for (int i = 0; i < tempsql.Length; i++)
                {
                    sqltext[i + 1] = tempsql[i];
                }

                intresult = pmf.ExecMutri(sqltext);

                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "数据保存成功";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据保存失败";
                }

                alert.Show();
            }
        }
        protected void btnmachinesave_Click(object sender, EventArgs e)
        {
            pd_compongall_compose ulcom = new pd_compongall_compose();
            int intresult = 0;
            FineUIPro.TreeNode[] nodes = machinekittree.GetCheckedNodes();
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            string strID = treenode.NodeID;
            string number = txtnumber.Text.Trim();
            if (nodes.Length > 0)
            {

                foreach (FineUIPro.TreeNode node in nodes)
                {
                    DataTable dt1 = ulcom.getEditdataByID(strID, node.NodeID);
                    if (dt1.Rows.Count == 0)
                    {
                        Dictionary<string, string> diccom = new Dictionary<string, string>();
                        diccom.Add("componID", strID);
                        diccom.Add("composeID", node.NodeID);
                        diccom.Add("ID", Guid.NewGuid().ToString());
                        if (number == "1")
                        {
                            diccom.Add("type", "元零件");
                        }
                        if (number == "2")
                        {
                            diccom.Add("type", "标准件");
                        }
                        if (number == "3")
                        {
                            diccom.Add("type", "外购件");
                        }
                        if (number == "4")
                        {
                            diccom.Add("type", "元组件");
                        }
                        intresult = ulcom.add(diccom, "pd_compongall_compose");
                    }
                }

            }
            else
            {
                Alert.Show("没有列被选中!");
                return;
            }

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2" && treenode.Text.Trim() != "克隆组件")
            {
                BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);
            }
            Alert alert = new Alert();

            if (intresult == 1)
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存成功";
                alert.Show();
            }
            else
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存失败";
                alert.Show();
            }
            txtnumber.Text = "0";
        }
        protected void btnmachinefind_Click(object sender, EventArgs e)
        {
            string strf_mtypename = txtmachinename.Text.Trim();

            traversaltree1(machinekittree.Nodes[0],"", strf_mtypename);
        }
        protected void mainGrid_Click(object sender, GridRowClickEventArgs e)
        {
            windowmachInfo.Title = "元零件详细信息";
            windowmachInfo.Hidden = false;
            string pid = mainGrid.DataKeys[e.RowIndex][0].ToString();
            pd_machinekit mac = new pd_machinekit();
            DataTable dt = mac.getEditdata(pid);

            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                mname.Text = r["mname"].ToString().Trim();
                mcode.Text = r["mcode"].ToString().Trim();
                drawdate.Text = System.DateTime.Parse(r["drawdate"].ToString().Trim()).ToString();
                designer1.Text = r["designer"].ToString().Trim();
                checker1.Text = r["checker"].ToString().Trim();
                stanarder1.Text = r["stanarder"].ToString().Trim();
                drawer1.Text = r["drawer"].ToString().Trim();
                examiner1.Text = r["examiner"].ToString().Trim();
                specifications1.Text = r["specifications"].ToString().Trim();
            }
            pd_photo pp = new pd_photo();
            string strfilename = pp.isExistdata("pd_photo", "pid", pid, "filename");

            if (strfilename.Trim() != "")
            {
                Image2.ImageUrl = "~/resources/photo/" + strfilename;
            }
            else
            {
                Image2.ImageUrl = "~/resources/photo/法兰.png";
            }
        }
        protected void Grid1_Click(object sender, GridRowClickEventArgs e)
        {
            standardindow.Title = "标准件详细信息";
            standardindow.Hidden = false;
            string pid = Grid1.DataKeys[e.RowIndex][0].ToString();
            pd_standardkit st = new pd_standardkit();
            DataTable dt = st.getEditdata(pid);
            pd_photo pp = new pd_photo();
            string strfilename = pp.isExistdata("pd_photo", "pid", pid, "filename");
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                sname.Text = r["sname"].ToString().Trim();
                scode.Text = r["scode"].ToString().Trim();
                stype.Text = r["stype"].ToString().Trim();
                specification.Text = r["specification"].ToString().Trim();
                material.Text = r["material"].ToString().Trim();
            }
            if (strfilename.Trim() != "")
            {
                Image2.ImageUrl = "~/resources/photo/" + strfilename;
            }
            else
            {
                Image2.ImageUrl = "~/resources/photo/法兰.png";
            }
        }
        protected void Grid2_Click(object sender, GridRowClickEventArgs e)
        {
            outbuyWindow.Title = "外购件详细信息";
            outbuyWindow.Hidden = false;
            string pid = Grid2.DataKeys[e.RowIndex][0].ToString();
            pd_outbuykit st = new pd_outbuykit();
            DataTable dt = st.getEditdata(pid);
            pd_photo pp = new pd_photo();
            string strfilename = pp.isExistdata("pd_photo", "pid", pid, "filename");
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                oname.Text = r["oname"].ToString().Trim();
                ocode.Text = r["ocode"].ToString().Trim();
                otype.Text = r["otype"].ToString().Trim();
                ospecification.Text = r["specification"].ToString().Trim();
            }
            if (strfilename.Trim() != "")
            {
                Image3.ImageUrl = "~/resources/photo/" + strfilename;
            }
            else
            {
                Image3.ImageUrl = "~/resources/photo/法兰.png";
            }
        }
        #endregion
    }
}
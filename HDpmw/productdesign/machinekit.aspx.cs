using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDPages.productLib;
using HDBusiness;
using YDCode;
using System.IO;

namespace HDpmw.productdesign
{
    public partial class machinekit : PageBase
    {
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
            initdesigner();
            initexaminer();
            inittree();
        }

        private void initexaminer()
        {
            xparams x = new xparams();

            string str = x.getparamData("H009");

            examiner.DataSource = commonLib.stringTolist(str, ',');
            examiner.DataBind();
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

                    if (row["isclick"].ToString().Trim() == "1")
                    {
                        node.EnableClickEvent = true;
                    }

                    firsttree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }

            getnodesum(firsttree.Nodes[0]);

            setrepeat();
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

                    if (row["isclick"].ToString().Trim() == "1")
                    {
                        node.EnableClickEvent = true;
                    }

                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        #endregion

        #endregion

        #region 主菜单操作

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string strf_mname = f_mname.Text.Trim();
            string strf_mtypename = f_mtypename.Text.Trim();

            traversaltree(firsttree.Nodes[0], strf_mtypename, strf_mname);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strID = treenode.NodeID.ToString().Trim();
                string strmtypeid = treenode.ParentNode.NodeID.Trim();

                Dictionary<string, string> dic = initDatadic();
                dic.Add("mtypeid", strmtypeid);

                pd_machinekit pm = new pd_machinekit();
                int intresult = 0;

                if (pm.isExistdata("pd_machinekit", "ID", strID, "ID").Trim() == "")
                {
                    if (pm.isExistdata("mname", dic["mname"].ToString().Trim(), strID, false).Trim() == "" &&
                        pm.isExistdata("mcode", dic["mcode"].ToString().Trim(), strID, false).Trim() == "")
                    {
                        dic.Add("ID", strID);
                        intresult = pm.add(dic, "pd_machinekit");
                    }
                    else
                    {
                        Alert.Show("零件名称或零件编码已经存在");
                        return;
                    }
                }
                else
                {
                    if (pm.isExistdata("mname", dic["mname"].ToString().Trim(), strID, true).Trim() == "" &&
                        pm.isExistdata("mcode", dic["mcode"].ToString().Trim(), strID, true).Trim() == "")
                    {
                        pd_compongall_compose pcc = new pd_compongall_compose();
                        pd_product_compose ppc = new pd_product_compose();

                        if (pcc.isExistdata("pd_compongall_compose", "ID", strID, "ID").Trim()!= "" &&
                            ppc.isExistdata("pd_product_compose", "ID", strID, "ID").Trim()!= "")
                        {
                            Alert.Show("当前零件不能修改名称编码");
                            return;
                        }

                        intresult = pm.update(dic, "pd_machinekit", "cast(ID as varchar(36))", strID);
                    }
                    else
                    {
                        Alert.Show("零件名称或零件编码已经存在");
                        return;
                    }
                }

                inittree();

                Alert alert = new Alert();

                if (intresult == 1)
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

        #endregion

        #region 树目录操作-右键菜单

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

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2" && treenode.Text.Trim()!="克隆零件")
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

                string newmenutxt = "新零件";
                newtreenode.Text = "<input style=\"color:darkblue;width:81px;border:none;\" value=\"" + newmenutxt + "\" " +
                        "onblur=\"ontxtBlur(this.value)\"></input>";
                mname.Text = newmenutxt;

                mcode.Text = getmcode(treenode.NodeID);

                string strGUID = Guid.NewGuid().ToString().Trim();
                newtreenode.NodeID = strGUID;
                BindGrid(treenode.NodeID, strGUID);

                newtreenode.Attributes.Add("nodemenu", "2");
                newtreenode.EnableClickEvent = true;

                treenode.Nodes.Add(newtreenode);
                treenode.Expanded = true;
            }
        }

        protected void menuDelete_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            string strID = treenode.NodeID.Trim();

            //需检查零件在组件和产品表中是否存在
            //未删除图片图纸
            if (strID != "")
            {
                pd_compongall_compose pcc = new pd_compongall_compose();
                pd_product_compose ppc = new pd_product_compose();

                if(pcc.isExistdata("pd_compongall_compose", "composeID", strID,"ID").Trim()!="" &&
                    ppc.isExistdata("pd_product_compose", "ppid", strID, "ID").Trim()!= "")
                {
                    Alert.Show("当前零件不能删除");
                    return;
                }

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                List<string> listTablename = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("pd_machinekit", "cast(ID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_machinekit");

                dic = new Dictionary<string, string>();
                dic.Add("pd_machinekit_feature", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_machinekit_feature");

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

                pd_machinekit pm = new pd_machinekit();
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

        protected void menuClone_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                FineUIPro.TreeNode newtreenode = new FineUIPro.TreeNode();

                string newmenutxt = "克隆零件";
                newtreenode.Text = "<input style=\"color:darkblue;width:81px;border:none;\" value=\"" + newmenutxt + "\" " +
                        "onblur=\"ontxtBlur(this.value)\"></input>";

                initbaseinfo(treenode.NodeID);
                BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);

                //克隆后修改零件名称和编号
                mname.Text = newmenutxt;

                mcode.Text = getmcode(treenode.ParentNode.NodeID);

                string strGUID = Guid.NewGuid().ToString().Trim();
                newtreenode.NodeID = strGUID;
                //BindGrid(treenode.NodeID, strGUID);

                newtreenode.Attributes.Add("nodemenu", "2");
                newtreenode.EnableClickEvent = true;

                treenode.ParentNode.Nodes.Add(newtreenode);
                treenode.ParentNode.Expanded = true;

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

                if (strfilename.Trim() != "")
                {
                    PageContext.RegisterStartupScript("openwebcad('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + strfilename + "')");
                }
                else
                {
                    PageContext.RegisterStartupScript("clearcad()");
                }
            }
        }

        #endregion

        #region 图纸操作-零件保存/上传/查看

        private string getmcode(string strmtypeid)
        {
            pd_machinekit pm = new pd_machinekit();

            string strmcode = pm.getmaxmcode(strmtypeid);
            string strprecode = pm.getpremcode(strmtypeid);

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
            mname.Text = "";
            mcode.Text = "";
            designer.SelectedIndex = -1;
            checker.SelectedIndex = -1;
            stanarder.SelectedIndex = -1;
            drawer.SelectedIndex = -1;
            examiner.SelectedIndex = -1;
            drawdate.SelectedDate = null;
            specifications.Text = "";
        }

        private void initbaseinfo(string strID)
        {
            if (strID.Trim() != "")
            {
                pd_machinekit pm = new pd_machinekit();
                System.Data.DataTable dt = pm.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    mname.Text = r["mname"].ToString().Trim();
                    mcode.Text = r["mcode"].ToString().Trim();
                    drawdate.SelectedDate=System.DateTime.Parse(r["drawdate"].ToString().Trim());
                    designer.SelectedValue = r["designer"].ToString().Trim();
                    checker.SelectedValue = r["checker"].ToString().Trim();
                    stanarder.SelectedValue = r["stanarder"].ToString().Trim();
                    drawer.SelectedValue = r["drawer"].ToString().Trim();
                    examiner.SelectedValue = r["examiner"].ToString().Trim();
                    specifications.Text = r["specifications"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mname", mname.Text.Trim());
            dic.Add("mcode", mcode.Text.Trim());

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

            dic.Add("drawdate", drawdate.SelectedDate.ToString());
            dic.Add("specifications", specifications.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        protected void GrapkTabStrip_TabIndexChanged(object sender, EventArgs e)
        {
            if (GrapkTabStrip.ActiveTabIndex == 0)
            {
                ;
            }
            else if (GrapkTabStrip.ActiveTabIndex == 1)
            {
                //imgPhoto.ImageUrl= "../resources/photo/法兰.png";
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
            else if (GrapkTabStrip.ActiveTabIndex == 2)
            {
                ;
            }
            else if (GrapkTabStrip.ActiveTabIndex == 3)
            {
                Label3.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
        }

        //CAD图纸
        protected void filemxcad_FileSelected(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_machinekit pm = new pd_machinekit();
                string strpid = treenode.NodeID.ToString().Trim();

                if (pm.isExistdata("pd_machinekit", "ID", strpid, "ID").Trim()=="")
                {
                    Alert.Show("请先保存零件基本信息");
                    return;
                }

                try
                {
                    if (filemxcad.HasFile)
                    {
                        string fileName = filemxcad.ShortFileName;

                        if (!commonLib.ValidateCADType(fileName))
                        {
                            Alert.Show("无效的文件类型！");
                            return;
                        }

                        fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                        int lastDotIndex = fileName.LastIndexOf(".");
                        string strfiletype = fileName.Substring(lastDotIndex + 1).ToLower();
                        string strpname = fileName.Substring(0, lastDotIndex);
                        fileName = DateTime.Now.Ticks.ToString() + fileName.Substring(lastDotIndex).Trim();

                        filemxcad.SaveAs(Server.MapPath("~/resources/cad/" + fileName));

                        // 清空文件上传组件
                        filemxcad.Reset();
                                               
                        Dictionary<string, string> dic = initCADdic(strpid, strpname, fileName, strfiletype);

                        try
                        {
                            pd_cad pc= new pd_cad();
                            string strfilename = pc.isExistdata("pd_cad", "pid", strpid, "filename");

                            if (strfilename.Trim() != "")
                            {
                                strfilename = Server.MapPath("~/resources/cad/") + strfilename;

                                if (File.Exists(strfilename))
                                {
                                    File.Delete(strfilename);
                                }

                                pc.update(dic, "pd_cad", "pid", strpid);
                            }
                            else
                            {
                                dic.Add("ID", Guid.NewGuid().ToString());
                                dic.Add("pid", strpid);
                                pc.add(dic, "pd_cad");
                            }


                            PageContext.RegisterStartupScript("openwebcad('http://"+ Request.Url.Authority.Trim() + "/resources/cad/"+ fileName + "')");
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

        //图片
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_machinekit pm = new pd_machinekit();
                string strpid = treenode.NodeID.ToString().Trim();

                if (pm.isExistdata("pd_machinekit", "ID", strpid, "ID").Trim() == "")
                {
                    Alert.Show("请先保存零件基本信息");
                    return;
                }

                try
                {
                    if (filePhoto.HasFile)
                    {
                        string fileName = filePhoto.ShortFileName;

                        if (!commonLib.ValidateImgType(fileName))
                        {
                            Alert.Show("无效的文件类型！");
                            return;
                        }

                        fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                        int lastDotIndex = fileName.LastIndexOf(".");
                        string strfiletype = fileName.Substring(lastDotIndex + 1).ToLower();
                        string strpname = fileName.Substring(0, lastDotIndex);
                        fileName = DateTime.Now.Ticks.ToString() + fileName.Substring(lastDotIndex).Trim();

                        filePhoto.SaveAs(Server.MapPath("~/resources/photo/" + fileName));

                        imgPhoto.ImageUrl = "~/resources/photo/" + fileName;

                        // 清空文件上传组件
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

        private Dictionary<string,string> initPhotodic(string strpid,string strpname,string fileName, string strfiletype)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
           
            dic.Add("pcode", strpname);
            dic.Add("pname", strpname);
            dic.Add("filename", fileName);
            dic.Add("filetype", strfiletype);
            dic.Add("operater",SessionUserName);

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

        #region 表格操作-特征

        private void BindGrid(string strmtypeid, string strtID)
        {
            pd_machinekit_feature pmf = new pd_machinekit_feature();

            mainGrid.DataSource = pmf.getBindGridDataAsdt(strmtypeid, strtID);
            mainGrid.DataBind();
        }

        protected void btnSavefeature_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<int, Dictionary<string, object>> modifiedDict = mainGrid.GetModifiedDict();

                pd_machinekit_feature pmf = new pd_machinekit_feature();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();

                string strpid = treenode.NodeID;

                for (int i = 0; i < mainGrid.Rows.Count; i++)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    dic.Add("pid", strpid);
                    dic.Add("featureid", mainGrid.DataKeys[i][0].ToString().Trim());
                    dic.Add("featurevalue", mainGrid.DataKeys[i][2].ToString().Trim());
                    dic.Add("operater", SessionUserName);

                    foreach (int rowindex in modifiedDict.Keys)
                    {
                        if (i == rowindex)
                        {
                            Dictionary<string, object> row = modifiedDict[rowindex];

                            dic["featurevalue"] = row["featurevalue"].ToString().Trim();
                            break;
                        }
                    }

                    listdic.Add(dic);
                }

                int intresult = 0;
                string[] sqltext = new string[listdic.Count + 1];

                sqltext[0] = createSql.DeleteDatasql("pd_machinekit_feature", "pid", strpid);
                string[] tempsql = createSql.getinsrtsqlarray("pd_machinekit_feature", listdic);
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

        #endregion

    }
}
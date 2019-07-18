using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDPages.productLib;
using HDPages.craftLib;
using HDPages.baseInfoLib;
using HDBusiness;
using YDCode;
using System.IO;

namespace HDpmw.craftdesign
{
    public partial class thermalDesignManage : PageBase
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
            initdesignperson();
            initauditperson();
            inittree();

            cloneID.Text = "";

            initauditindicator();
            initheatdevice();
            editID_batch.Text = "";
            editID_indicator.Text = "";

            setbtndisplay(true);
        }

        private void initheatdevice()
        {
            bi_heatdevice bh= new bi_heatdevice();

            System.Data.DataTable dt = bh.getheatdevice();

            bdevice.DataTextField = "hdname";
            bdevice.DataValueField = "hdname";
            bdevice.DataSource = dt;
            bdevice.DataBind();
        }

        private void initauditperson()
        {
            xparams x = new xparams();

            string str = x.getparamData("H009");

            auditperson.DataSource = commonLib.stringTolist(str, ',');
            auditperson.DataBind();
        }

        private void initauditindicator()
        {
            xparams x = new xparams();

            string str = x.getparamData("H018");

            iname.DataSource = commonLib.stringTolist(str, ',');
            iname.DataBind();
        }

        private void initdesignperson()
        {
            xparams x = new xparams();

            string str = x.getparamData("H010");

            normalperson.DataSource = commonLib.stringTolist(str, ',');
            normalperson.DataBind();

            approveperson.DataSource = commonLib.stringTolist(str, ',');
            approveperson.DataBind();

            designperson.DataSource = commonLib.stringTolist(str, ',');
            designperson.DataBind();
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

                    if (row["isclick"].ToString().Trim() == "2")
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

                    if (row["isclick"].ToString().Trim() == "2")
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

                Dictionary<string, string> dic = initDatadic();

                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", strID, "ID").Trim();

                int intresult = 0;

                if (strtid == "")
                {
                    dic.Add("ID",Guid.NewGuid().ToString().Trim());
                    dic.Add("pid", strID);
                    intresult = ct.add(dic, "cd_thermal");
                }
                else
                {
                    intresult = ct.update(dic, "cd_thermal", "cast(pid as varchar(36))", strID);
                }

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
            setbtndisplay(true);

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                initbaseinfo(treenode.NodeID);

                cd_thermal ct = new cd_thermal();

                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.Trim(), "ID").Trim();

                if (strtid != "")
                {
                    setbtndisplay(false);
                    BindGrid(strtid);
                    //初始化图片
                    initphoto(strtid);
                }
                else
                {
                    tcode.Text = gettcode();
                    tname.Text = treenode.Text.Trim() + "热处理工艺卡";
                }
            }
        }

        protected void menuPaste_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strtid = cloneID.Text.Trim();
                string strpid = treenode.NodeID.Trim();

                cd_thermal ct = new cd_thermal();

                //检查空才克隆并保存
                if (strtid!="" && strpid!="")
                {
                    if(ct.isExistdata("cd_thermal", "pid",strpid,"ID").Trim()!="")
                    {
                        Alert.Show("当前零件热处理工艺不为空");
                    }
                    else
                    {
                        string strtcode= gettcode();
                        string strtname= treenode.Text.Trim() + "热处理工艺卡";

                        string[] sqltext = ct.clonethermal(Guid.NewGuid().ToString().Trim(), strtname, strtcode, strpid, SessionUserName.Trim(), strtid);

                        int intresult = ct.ExecMutri(sqltext);

                        Alert alert = new Alert();

                        if (intresult > 0)
                        {
                            alert.Icon = Icon.Information;
                            alert.Message = "数据克隆成功";
                        }
                        else
                        {
                            alert.MessageBoxIcon = MessageBoxIcon.Error;
                            alert.Message = "数据克隆失败";
                        }

                        alert.Show();
                    }
                }

                cloneID.Text = "";
                btnmenuPaste.Enabled = false;
            }
        }

        protected void menuDelete_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            string strID = treenode.NodeID.Trim();

            if (strID != "")
            {
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal","pid",strID,"ID").Trim();

                if (strtid != "")
                {
                    List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                    List<string> listTablename = new List<string>();
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    dic.Add("cd_thermal", "cast(pid as varchar(36))='" + strID + "' ");
                    listdic.Add(dic);
                    listTablename.Add("cd_thermal");

                    dic = new Dictionary<string, string>();
                    dic.Add("cd_thermal_batch", "cast(tid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("cd_thermal_batch");

                    dic = new Dictionary<string, string>();
                    dic.Add("cd_thermal_indicator", "cast(tid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("cd_thermal_indicator");

                    dic = new Dictionary<string, string>();
                    dic.Add("pd_3d", "cast(pid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("pd_3d");

                    dic = new Dictionary<string, string>();
                    dic.Add("pd_blueprint", "cast(pid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("pd_blueprint");

                    dic = new Dictionary<string, string>();
                    dic.Add("pd_photo", "cast(pid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("pd_photo");

                    dic = new Dictionary<string, string>();
                    dic.Add("pd_cad", "cast(pid as varchar(36))='" + strtid + "' ");
                    listdic.Add(dic);
                    listTablename.Add("pd_cad");

                    int intresult = 0;

                    pd_machinekit pm = new pd_machinekit();
                    intresult = pm.deleteMutri(listdic, listTablename);

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
        }

        protected void menuClone_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            cloneID.Text = "";

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {               
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.Trim(), "ID").Trim();

                if (strtid != "")
                {
                    cloneID.Text = strtid;
                    btnmenuPaste.Enabled = true;
                }
            }
        }

        protected void cadLookup_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.Trim(), "ID").Trim();

                if (strtid != "")
                {
                    pd_cad pc = new pd_cad();
                    string strfilename = pc.isExistdata("pd_cad", "pid", strtid, "filename");

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
        }

        #endregion

        #region 图纸操作-零件保存/上传/查看

        private string gettcode()
        {
            cd_thermal ct = new cd_thermal();

            string strmcode = ct.getmaxmcode();
            string strprecode = "CH";

            if (strmcode.Length == 1)
            {
                strmcode = strprecode + "000" + strmcode;
            }
            else if (strmcode.Length == 2)
            {
                strmcode = strprecode + "00" + strmcode;
            }
            else if (strmcode.Length == 3)
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
            tname.Text = "";
            tcode.Text = "";
            mtag.Text = "";
            kitweight.Text = "";
            craftmethod.Text = "";
            normalperson.SelectedIndex = -1;
            approveperson.SelectedIndex = -1;
            designperson.SelectedIndex = -1;
            auditperson.SelectedIndex = -1;
            normaldate.SelectedDate = null;
            approvedate.SelectedDate = null;
            designdate.SelectedDate = null;
            auditdate.SelectedDate = null;
        }

        private void initbaseinfo(string strID)
        {
            if (strID.Trim() != "")
            {
                cd_thermal ct = new cd_thermal();
                System.Data.DataTable dt = ct.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    tname.Text = r["tname"].ToString().Trim();
                    tcode.Text = r["tcode"].ToString().Trim();
                    mtag.Text = r["mtag"].ToString().Trim();
                    kitweight.Text = r["kitweight"].ToString().Trim();
                    craftmethod.Text = r["craftmethod"].ToString().Trim();
                    normalperson.SelectedValue = r["normalperson"].ToString().Trim();
                    approveperson.SelectedValue = r["approveperson"].ToString().Trim();
                    designperson.SelectedValue = r["designperson"].ToString().Trim();
                    auditperson.SelectedValue = r["auditperson"].ToString().Trim();

                    normaldate.SelectedDate = System.DateTime.Parse(r["normaldate"].ToString().Trim());
                    approvedate.SelectedDate = System.DateTime.Parse(r["approvedate"].ToString().Trim());
                    designdate.SelectedDate = System.DateTime.Parse(r["designdate"].ToString().Trim());
                    auditdate.SelectedDate = System.DateTime.Parse(r["auditdate"].ToString().Trim());
                }
            }
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("tname", tname.Text.Trim());
            dic.Add("tcode", tcode.Text.Trim());
            dic.Add("mtag", mtag.Text.Trim());
            dic.Add("kitweight", kitweight.Text.Trim());
            dic.Add("craftmethod", craftmethod.Text.Trim());

            if (normalperson.SelectedItem != null)
            {
                dic.Add("normalperson", normalperson.SelectedValue.Trim());
            }

            if (approveperson.SelectedItem != null)
            {
                dic.Add("approveperson", approveperson.SelectedValue.Trim());
            }

            if (designperson.SelectedItem != null)
            {
                dic.Add("designperson", designperson.SelectedValue.Trim());
            }

            if (auditperson.SelectedItem != null)
            {
                dic.Add("auditperson", auditperson.SelectedValue.Trim());
            }

            dic.Add("normaldate", normaldate.SelectedDate.ToString());
            dic.Add("approvedate", approvedate.SelectedDate.ToString());
            dic.Add("designdate", designdate.SelectedDate.ToString());
            dic.Add("auditdate", auditdate.SelectedDate.ToString());
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
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.ToString().Trim(), "ID").Trim();

                if (strtid == "")
                {
                    Alert.Show("请先保存工艺卡基本信息");
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

                        Dictionary<string, string> dic = initCADdic(strtid, strpname, fileName, strfiletype);

                        try
                        {
                            pd_cad pc = new pd_cad();
                            string strfilename = pc.isExistdata("pd_cad", "pid", strtid, "filename");

                            if (strfilename.Trim() != "")
                            {
                                strfilename = Server.MapPath("~/resources/cad/") + strfilename;

                                if (File.Exists(strfilename))
                                {
                                    File.Delete(strfilename);
                                }

                                pc.update(dic, "pd_cad", "pid", strtid);
                            }
                            else
                            {
                                dic.Add("ID", Guid.NewGuid().ToString());
                                dic.Add("pid", strtid);
                                pc.add(dic, "pd_cad");
                            }


                            PageContext.RegisterStartupScript("openwebcad('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + fileName + "')");
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
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.ToString().Trim(), "ID").Trim();

                if (strtid == "")
                {
                    Alert.Show("请先保存工艺卡基本信息");
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

                        Dictionary<string, string> dic = initPhotodic(strtid, strpname, fileName, strfiletype);

                        try
                        {
                            pd_photo pp = new pd_photo();
                            string strfilename = pp.isExistdata("pd_photo", "pid", strtid, "filename");

                            if (strfilename.Trim() != "")
                            {
                                strfilename = Server.MapPath("~/resources/photo/") + strfilename;

                                if (File.Exists(strfilename))
                                {
                                    File.Delete(strfilename);
                                }

                                pp.update(dic, "pd_photo", "pid", strtid);
                            }
                            else
                            {
                                dic.Add("ID", Guid.NewGuid().ToString());
                                dic.Add("pid", strtid);
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

        #region 表格操作-技术要求

        private void BindGrid(string strtid)
        {
            cd_thermal_batch ctb= new cd_thermal_batch();
            cd_thermal_indicator ctic = new cd_thermal_indicator();

            secondGrid.DataSource = ctic.getBindGridDataAsdt(strtid);
            secondGrid.DataBind();

            mainGrid.DataSource = ctb.getBindGridDataAsdt(strtid);
            mainGrid.DataBind();
        }

        private void setbtndisplay(bool isdisplay)
        {
            btniNew.Hidden = isdisplay;
            btniEdit.Hidden = isdisplay;
            btniDelete.Hidden = isdisplay;
            btnbNew.Hidden = isdisplay;
            btnbEdit.Hidden = isdisplay;
            btnbDelete.Hidden = isdisplay;
        }

        //工序
        protected void btniNew_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                editID_indicator.Text = "";
                erase_indicator();
                neweditWindow_indicator.Hidden = false;
            }
            else
            {
                Alert.Show("请选择新增热处理技术要求的零件!");
            }
        }

        protected void btniEdit_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                editID_indicator.Text = "";
                int index = secondGrid.SelectedRowIndex;

                if (index > -1)
                {
                    string strID = secondGrid.DataKeys[index][0].ToString().Trim();
                    editID_indicator.Text = strID;
                    initbaseinfo_indicator(strID);
                    neweditWindow_indicator.Hidden = false;
                }
                else
                {
                    Alert.Show("请选择编辑的热处理技术要求");
                }
            }
            else
            {
                Alert.Show("请选择新增热处理技术要求的零件!");
            }
        }

        protected void btniDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = secondGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                cd_thermal_indicator cti = new cd_thermal_indicator();
                object[] keys = secondGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                string strtid= keys[1].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = cti.deletebycondition("cd_thermal_indicator", dic);

                Alert alert = new Alert();
                BindGrid(strtid);

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
        }

        protected void btnSave_indicator_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<string, string> dic = initDatadic_indicator();
                string strID = editID_indicator.Text.ToString().Trim();
                cd_thermal_indicator cti = new cd_thermal_indicator();
                int intresult = 0;

                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.ToString().Trim(), "ID").Trim();

                if (strID == "")
                {
                    dic.Add("ID", Guid.NewGuid().ToString());                    
                    dic.Add("tid", strtid);

                    intresult = cti.add(dic, "cd_thermal_indicator");
                }
                else
                {
                    intresult = cti.update(dic, "cd_thermal_indicator", "ID", strID);
                }

                Alert alert = new Alert();
                BindGrid(strtid);

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

        private void erase_indicator()
        {
            iname.SelectedIndex = 0;
            ivalue.Text = "";
            icheck.Text = "";
        }
        private void initbaseinfo_indicator(string strID)
        {
            if (strID.Trim() != "")
            {
                cd_thermal_indicator cti = new cd_thermal_indicator();
                System.Data.DataTable dt = cti.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    iname.SelectedValue = r["iname"].ToString().Trim();
                    ivalue.Text = r["ivalue"].ToString().Trim();
                    icheck.Text = r["icheck"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_indicator()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("iname", iname.SelectedValue.Trim());
            dic.Add("ivalue", ivalue.Text.Trim());
            dic.Add("icheck", icheck.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #endregion

        #region 表格操作-工序

        protected void btnbNew_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                editID_batch.Text = "";
                erase_batch();
                neweditWindow_batch.Hidden = false;
            }
            else
            {
                Alert.Show("请选择新增热处理工序的零件!");
            }
        }

        protected void btnbEdit_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                editID_indicator.Text = "";
                int index = mainGrid.SelectedRowIndex;

                if (index > -1)
                {
                    string strID = mainGrid.DataKeys[index][0].ToString().Trim();
                    editID_batch.Text = strID;
                    initbaseinfo_batch(strID);
                    neweditWindow_batch.Hidden = false;
                }
                else
                {
                    Alert.Show("请选择编辑的热处理工序");
                }
            }
            else
            {
                Alert.Show("请选择编辑热处理工序的零件!");
            }
        }

        protected void btnbDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                cd_thermal_batch ctb = new cd_thermal_batch();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                string strtid = keys[1].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = ctb.deletebycondition("cd_thermal_batch", dic);

                Alert alert = new Alert();
                BindGrid(strtid);

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
        }

        protected void btnSave_batch_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Dictionary<string, string> dic = initDatadic_batch();
                string strID = editID_batch.Text.ToString().Trim();
                cd_thermal_batch ctb = new cd_thermal_batch(); ;
                int intresult = 0;

                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.ToString().Trim(), "ID").Trim();

                if (strID == "")
                {
                    dic.Add("ID", Guid.NewGuid().ToString());
                    dic.Add("tid", strtid);

                    intresult = ctb.add(dic, "cd_thermal_batch");
                }
                else
                {
                    intresult = ctb.update(dic, "cd_thermal_batch", "ID", strID);
                }

                Alert alert = new Alert();
                BindGrid(strtid);

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

        private void erase_batch()
        {
            batchnumber.Text = "";
            batchtext.Text = "";
            bdevice.SelectedIndex = 0;
            stove_code.Text = "";
            stovetemp.Text = "";
            heattemp.Text = "";
            heattime.Text = "";
            keeptime.Text = "";
            coolmedia.Text = "";
            cooltemp.Text = "";
            cooltime.Text = "";
            workhour.Text = "";
        }

        private void initbaseinfo_batch(string strID)
        {
            if (strID.Trim() != "")
            {
                cd_thermal_batch ctb = new cd_thermal_batch();
                System.Data.DataTable dt = ctb.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    batchnumber.Text = r["batchnumber"].ToString().Trim();
                    batchtext.Text = r["batchtext"].ToString().Trim();
                    bdevice.SelectedValue = r["bdevice"].ToString().Trim();
                    stove_code.Text = r["stove_code"].ToString().Trim();
                    stovetemp.Text = r["stovetemp"].ToString().Trim();
                    heattemp.Text = r["heattemp"].ToString().Trim();
                    heattime.Text = r["heattime"].ToString().Trim();
                    keeptime.Text = r["keeptime"].ToString().Trim();
                    coolmedia.Text = r["coolmedia"].ToString().Trim();
                    cooltemp.Text = r["cooltemp"].ToString().Trim();
                    cooltime.Text = r["cooltime"].ToString().Trim();
                    workhour.Text = r["workhour"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_batch()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("batchnumber", batchnumber.Text.Trim());
            dic.Add("batchtext", batchtext.Text.Trim());
            dic.Add("bdevice", bdevice.SelectedValue.Trim());
            dic.Add("stove_code", stove_code.Text.Trim());
            dic.Add("stovetemp", stovetemp.Text.Trim());
            dic.Add("heattemp", heattemp.Text.Trim());
            dic.Add("heattime", heattime.Text.Trim());
            dic.Add("keeptime", keeptime.Text.Trim());
            dic.Add("coolmedia", coolmedia.Text.Trim());
            dic.Add("cooltemp", cooltemp.Text.Trim());
            dic.Add("cooltime", cooltime.Text.Trim());
            dic.Add("workhour", workhour.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #endregion

    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDPages.planningLib;
using HDPages.productLib;
using HDBusiness;
using YDCode;
using System.IO;

namespace HDpmw.productdesign
{
    public partial class product : PageBase
    {
        #region  初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initui();
            }
        }

        private void initui()
        {
            initpdtype();
            initexaminer();
            initdesigner();
            inithidEdit();

            setPage_5Init();
            setPage_4Init();
            setPage_3Init();           
            setPage_2Init();
            setPage_1Init();
            initcomtype();
            initotype();
            initstype();
            initmainclass();
            initpdtype_1();
        }

        #region firsttree树初始化
        private void inittree(string strpdname)
        {
            firsttree.Nodes.Clear();
            // 从数据库返回数据表
            pd_product pp = new pd_product();
            DataTable table = pp.getBindTreeDataAsdt(strpdname);

            if (table != null && table.Rows.Count > 0)
            {

                DataSet ds = new DataSet();
                ds.Tables.Add(table);
                ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["ppid"], ds.Tables[0].Columns["pid"]);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.IsNull("pid"))
                    {
                        FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                        node.Text = row["pdname"].ToString();
                        string strid = row["ppid"].ToString().Trim();
                        node.NodeID = row["ppid"].ToString().Trim();

                        node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                        node.Attributes.Add("pptype", row["pptype"].ToString().Trim());

                        if (row["isclick"].ToString().Trim() == "1")
                        {
                            node.EnableClickEvent = true;
                        }

                        firsttree.Nodes.Add(node);
                        ResolveSubTree(row, node);
                    }
                }
            }
            else
            {
                Alert.Show("未找到产品："+ strpdname);
                return;
            }

            //getnodesum(firsttree.Nodes[0]);

            //setrepeat();
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
                    node.Text = row["pdname"].ToString();
                    node.NodeID = row["ppid"].ToString().Trim();
                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                    node.Attributes.Add("pptype", row["pptype"].ToString().Trim());

                    if (row["isclick"].ToString().Trim() == "1")
                    {
                        node.EnableClickEvent = true;
                    }

                    node.ToolTip= row["pdnumber"].ToString();

                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        #endregion

        private void inithidEdit()
        {
            editID_5.Text = "";
        }
        
        private void initpdtype()
        {
            xparams x = new xparams();

            string str = x.getparamData("H016");
            
            pdtype.DataSource = commonLib.stringTolist(str, ',');
            pdtype.DataBind();

            f_pdtype.DataSource = commonLib.stringTolist(str, ',');
            f_pdtype.DataBind();
        }

        private void initpdtype_1()
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getpdtype();

            f1_pdtype.DataTextField = "pdtype";
            f1_pdtype.DataValueField = "pdtype";
            f1_pdtype.DataSource = dt;
            f1_pdtype.DataBind();
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

        private void initcomtype()
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getcomtype();

            f5_comtype.DataTextField = "comtype";
            f5_comtype.DataValueField = "comtype";
            f5_comtype.DataSource =dt;
            f5_comtype.DataBind();
        }

        private void initotype()
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getotype();

            f4_otype.DataTextField = "otype";
            f4_otype.DataValueField = "otype";
            f4_otype.DataSource = dt;
            f4_otype.DataBind();

            f4_otype.SelectedValue = "零件";
        }

        private void initstype()
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getstype();

            f3_stype.DataTextField = "stype";
            f3_stype.DataValueField = "stype";
            f3_stype.DataSource = dt;
            f3_stype.DataBind();
        }

        private void initmainclass()
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getmainclass();

            f2_mainclass.DataTextField = "mainname";
            f2_mainclass.DataValueField = "ID";
            f2_mainclass.DataSource = dt;
            f2_mainclass.DataBind();

            f2_mainclass.SelectedIndex = 0;

            initsubclass(f2_mainclass.SelectedValue.Trim());
        }

        private void initsubclass(string strmainclassID)
        {
            pd_product pp = new pd_product();

            System.Data.DataTable dt = pp.getsubclass(strmainclassID);

            f2_subclass.DataTextField = "classname";
            f2_subclass.DataValueField = "ID";
            f2_subclass.DataSource = dt;
            f2_subclass.DataBind();
        }

        #endregion

        #region 主菜单操作

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string strpdname = f_pdname.Text.Trim();
            if (strpdname!="")
            {
                inittree(strpdname);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            firsttree.Nodes.Clear();
            FineUIPro.TreeNode node = new FineUIPro.TreeNode();

            string newmenutxt = "新产品";
            node.Text = "<input style=\"color:darkblue;width:81px;border:none;\" value=\"" + newmenutxt + "\" " +
                        "onblur=\"ontxtBlur(this.value)\"></input>";
            node.NodeID = Guid.NewGuid().ToString();

            pdname.Text = newmenutxt;
            pdcode.Text = getpdcode();

            node.Attributes.Add("nodemenu", "1");
            node.EnableClickEvent = true;

            firsttree.Nodes.Add(node);

            firsttree.ExpandAllNodes();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                string strID = treenode.NodeID.ToString().Trim();

                Dictionary<string, string> dic = initDatadic();

                pd_product pp = new pd_product();
                int intresult = 0;

                if (pp.isExistdata("pd_product", "ID", strID, "ID").Trim() == "")
                {
                    if (pp.isExistdata("pdname", dic["pdname"].ToString().Trim(), strID, false).Trim() == "" &&
                        pp.isExistdata("pdcode", dic["pdcode"].ToString().Trim(), strID, false).Trim() == "")
                    {
                        dic.Add("ID", strID);
                        intresult = pp.add(dic, "pd_product");
                    }
                    else
                    {
                        Alert.Show("产品名称或产品编码已经存在");
                        return;
                    }
                }
                else
                {
                    if (pp.isExistdata("pdname", dic["pdname"].ToString().Trim(), strID, true).Trim() == "" &&
                        pp.isExistdata("pdcode", dic["pdcode"].ToString().Trim(), strID, true).Trim() == "")
                    {//未检查，产品已使用不能修改名称编码
                        intresult = pp.update(dic, "pd_product", "cast(ID as varchar(36))", strID);
                    }
                    else
                    {
                        Alert.Show("产品名称或产品编码已经存在");
                        return;
                    }
                }

                inittree(dic["pdname"].ToString().Trim());

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

        #region firsttree树目录操作-右键菜单

        protected void firsttree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                initbaseinfo(treenode.NodeID.Trim());
                initphoto(treenode.NodeID);

                GrapkTabStrip.ActiveTabIndex = 0;

                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
            
        }

        //删除产品
        protected void menuMDelete_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                string strID = treenode.NodeID.Trim();
                string strpdname = treenode.Text.Trim();

                deleteproduct(strID, strpdname);
            }
        }

        private void deleteproduct(string strID,string strpdname)
        {
            if (strID != "")
            {//需检查产品是否已列入生产指令单和是否组成其他产品
                pp_producitionorder_content ppoc = new pp_producitionorder_content();
                pd_product_compose ppc = new pd_product_compose();

                if (ppoc.isExistdata("pp_producitionorder_content", "productid", strID, "ID").Trim() != "" ||
                    ppc.isExistdata("pd_product_compose","pid", strID,"ID").Trim()!="")
                {
                    Alert.Show("当前产品不能删除");
                    return;
                }

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                List<string> listTablename = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("pd_product", "cast(ID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_product");

                dic = new Dictionary<string, string>();
                dic.Add("pd_product_compose", "cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                listTablename.Add("pd_product_compose");

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

                pd_product pp = new pd_product();
                intresult = pp.deleteMutri(listdic, listTablename);

                //确认
                inittree(strpdname);

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

        protected void menuCAD_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "1")
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

        protected void GrapkTabStrip_TabIndexChanged(object sender, EventArgs e)
        {
            if (GrapkTabStrip.ActiveTabIndex == 0)
            {
                ;
            }
            else if (GrapkTabStrip.ActiveTabIndex == 1)
            {

            }
            else if (GrapkTabStrip.ActiveTabIndex == 2)
            {
                ;
            }
            else if (GrapkTabStrip.ActiveTabIndex == 3)
            {

            }
            else if (GrapkTabStrip.ActiveTabIndex == 4)
            {

            }
            else if (GrapkTabStrip.ActiveTabIndex == 5)
            {
                ;
            }
        }

        protected void menuPADD_Click(object sender, EventArgs e)
        {
            GrapkTabStrip.ActiveTabIndex=1;
            setPageContent_1(1);
        }

        protected void menuMAdd_Click(object sender, EventArgs e)
        {
            GrapkTabStrip.ActiveTabIndex = 2;
            inittree_2();
        }

        protected void menuSAdd_Click(object sender, EventArgs e)
        {
            GrapkTabStrip.ActiveTabIndex = 3;
            setPageContent_3(1);
        }

        protected void menuBAdd_Click(object sender, EventArgs e)
        {
            GrapkTabStrip.ActiveTabIndex = 4;
            setPageContent_4(1);
        }

        protected void menuCAdd_Click(object sender, EventArgs e)
        {
            GrapkTabStrip.ActiveTabIndex = 5;
            setPageContent_5(1);
        }           

        protected void menuPMSBEdit_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if(treenode.Attributes["pptype"].ToString().Trim()=="元组件")
            {
                neweditWindow_5.Title = "编辑组装元组件";
                editID_5.Text = "";
                initbaseinfo_5(treenode.ParentNode.NodeID,treenode.NodeID.Substring(0,36));
                neweditWindow_5.Hidden = false;
            }

            if (treenode.Attributes["pptype"].ToString().Trim() == "外购件")
            {
                neweditWindow_4.Title = "编辑组装外购件";
                editID_4.Text = "";
                initbaseinfo_4(treenode.ParentNode.NodeID, treenode.NodeID.Substring(0, 36));
                neweditWindow_4.Hidden = false;
            }

            if (treenode.Attributes["pptype"].ToString().Trim() == "标准件")
            {
                neweditWindow_3.Title = "编辑组装标准件";
                editID_3.Text = "";
                initbaseinfo_3(treenode.ParentNode.NodeID, treenode.NodeID.Substring(0, 36));
                neweditWindow_3.Hidden = false;
            }

            if (treenode.Attributes["pptype"].ToString().Trim() == "元零件")
            {
                neweditWindow_2.Title = "编辑组装元零件";
                editID_2.Text = "";
                initbaseinfo_2(treenode.ParentNode.NodeID, treenode.NodeID.Substring(0, 36));
                neweditWindow_2.Hidden = false;
            }            

            if (treenode.Attributes["pptype"].ToString().Trim() == "产品")
            {
                neweditWindow_1.Title = "编辑组装产品";
                editID_1.Text = "";
                initbaseinfo_1(treenode.ParentNode.NodeID, treenode.NodeID.Substring(0, 36));
                neweditWindow_1.Hidden = false;
            }
        }

        //删除产品组成件
        protected void menuPMSBDelete_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                string strID = treenode.NodeID.Trim().Substring(0, 36);
                string strparentID = treenode.ParentNode.NodeID.Trim();
                string strpdname = treenode.ParentNode.Text.Trim();

                //需检查产品是否已列入生产指令单
                if (strID != "" && strparentID!="")
                {
                    pp_producitionorder_content ppoc = new pp_producitionorder_content();

                    if (ppoc.isExistdata("pp_producitionorder_content", "productid", strparentID, "ID").Trim() != "")
                    {
                        Alert.Show("当前产品不能删除");
                        return;
                    }

                    int intresult = 0;

                    pd_product pp = new pd_product();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("pid", "cast(pid as varchar(36))='"+ strparentID + "' ");
                    dic.Add("ppid", "cast(ppid as varchar(36))='" + strID + "' ");
                    intresult = pp.deletebycondition("pd_product_compose", dic);

                    inittree(strpdname);

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

        #endregion

        #region 图纸操作-零件保存/上传/查看

        private string getpdcode()
        {
            pd_product pp= new pd_product();

            string strpdcode = pp.getmaxpdcode();
            string strprecode ="P";

            if (strpdcode.Length == 1)
            {
                strpdcode = strprecode + "000" + strpdcode;
            }
            else if (strpdcode.Length == 2)
            {
                strpdcode = strprecode + "00" + strpdcode;
            }
            else if (strpdcode.Length == 3)
            {
                strpdcode = strprecode + "0" + strpdcode;
            }
            else
            {
                strpdcode = strprecode + strpdcode;
            }

            return strpdcode;
        }

        private void erase()
        {
            pdname.Text = "";
            pdcode.Text = "";
            pdtype.SelectedIndex =0;
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
                pd_product pp = new pd_product();
                System.Data.DataTable dt = pp.getEditdata(strID);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    pdname.Text = r["pdname"].ToString().Trim();
                    pdcode.Text = r["pdcode"].ToString().Trim();
                    pdtype.SelectedValue = r["pdtype"].ToString().Trim();
                    drawdate.SelectedDate = System.DateTime.Parse(r["drawdate"].ToString().Trim());
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

            dic.Add("pdname", pdname.Text.Trim());
            dic.Add("pdcode", pdcode.Text.Trim());
            dic.Add("pdtype", pdtype.SelectedValue.Trim());

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

        protected void CADPhotoTabStrip_TabIndexChanged(object sender, EventArgs e)
        {
            if (CADPhotoTabStrip.ActiveTabIndex == 0)
            {
                ;
            }
            else if (CADPhotoTabStrip.ActiveTabIndex == 1)
            {
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
            else if (CADPhotoTabStrip.ActiveTabIndex == 2)
            {
                ;
            }
            else if (CADPhotoTabStrip.ActiveTabIndex == 3)
            {
                
            }
        }

        protected void filemxcad_FileSelected(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                pd_product pp_1 = new pd_product();
                string strpid = treenode.NodeID.ToString().Trim();

                if (pp_1.isExistdata("pd_product", "ID", strpid, "ID").Trim() == "")
                {
                    Alert.Show("请先保存产品零件基本信息");
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
                            pd_cad pc = new pd_cad();
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

        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                pd_product pp_1 = new pd_product();
                string strpid = treenode.NodeID.ToString().Trim();

                if (pp_1.isExistdata("pd_product", "ID", strpid, "ID").Trim() == "")
                {
                    Alert.Show("请先保存产品零件基本信息");
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

        #region Tab-产品库

        private void BindGrid_1()
        {
            string strf1_pdtype = f1_pdtype.SelectedValue.Trim();
            string strf1_pdname = f1_pdname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdtype", strf1_pdtype);
            dic.Add("pdname", strf1_pdname);

            int intPageindex = Convert.ToInt32(CurPage_1.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize_1.Text.Trim());
            string strSort = Grid_1.SortField;
            string strSortDirection = Grid_1.SortDirection;

            pd_product pp = new pd_product();
            DataTable dt = pp.getBindPdDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage_1.Text = pp.getPdtotalpage(dic); ;

            Grid_1.DataSource = dt;
            Grid_1.DataBind();
        }

        protected void btnFind_1_Click(object sender, EventArgs e)
        {
            setPageContent_1(1);
        }

        protected void btnLookup_1_Click(object sender, EventArgs e)
        {           
            int index = Grid_1.SelectedRowIndex;

            if (index > -1)
            {
                string strpdname = Grid_1.DataKeys[index][1].ToString().Trim();

                inittree(strpdname);
            }
        }

        protected void btnDelete_1_Click(object sender, EventArgs e)
        {
            int index = Grid_1.SelectedRowIndex;

            if (index > -1)
            {
                string strID = Grid_1.DataKeys[index][0].ToString().Trim();
                string strpdname = Grid_1.DataKeys[index][1].ToString().Trim();

                deleteproduct(strID, strpdname);
                setPageContent_1(1);
            }
        }

        protected void btnClone_1_Click(object sender, EventArgs e)
        {
            int index = Grid_1.SelectedRowIndex;

            if (index > -1)
            {
                string strID = Grid_1.DataKeys[index][0].ToString().Trim();
                string strpdname ="克隆产品";
                string strnewID = Guid.NewGuid().ToString();

                cloneproduct(strnewID, strpdname,strID);                
            }
        }

        private void cloneproduct(string strnewID,string strpdname,string strID)
        {
            pd_product pp = new pd_product();
            string[] sqltext= pp.cloneproduct(strnewID, strpdname, getpdcode(), SessionUserName.Trim(),strID);

            int intresult=pp.ExecMutri(sqltext);

            Alert alert = new Alert();

            if (intresult > 0)
            {
                setPageContent_1(1);
                inittree(strpdname);
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

        protected void Grid_1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                string strpid = treenode.NodeID.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                }

                string strppid = Grid_1.DataKeys[e.RowIndex][0].ToString().Trim();
                editID_1.Text = strppid;

                pd_product pp = new pd_product();
                //检查产品的循环嵌套
                if (strppid.ToLower().Trim()== strpid.ToLower().Trim() ||
                    pp.isExistsProduct(strpid, strppid).Trim()!="")
                {
                    Alert.Show("当前产品不能加入总成");
                    return;
                }

                pd_product_compose ppc = new pd_product_compose();

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();

                if (strID.Trim() != "")
                {
                    neweditWindow_1.Title = "编辑组装产品";
                    editID_1.Text = "";
                    initbaseinfo_1(strpid, strppid);
                }
                else
                {
                    neweditWindow_1.Title = "新增组装产品";
                    erase_1();
                }

                neweditWindow_1.Hidden = false;
            }
        }

        protected void Grid_1_RowClick(object sender, GridRowClickEventArgs e)
        {
            pd_product pp = new pd_product();
            System.Data.DataTable dt = pp.getBindPdComposeDataAsdt(Grid_1.DataKeys[e.RowIndex][0].ToString().Trim());

            Grid_11.DataSource = dt;
            Grid_11.DataBind();
        }

        protected void btnSave_1_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                Dictionary<string, string> dic = initDatadic_1();
                pd_product_compose ppc = new pd_product_compose();
                int intresult = 0;

                string strpid = treenode.NodeID.Trim();
                string strppid = editID_1.Text.Trim();
                string strpdname = treenode.Text.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                    strpdname = treenode.ParentNode.Text.Trim();
                }

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();
                if (strID.Trim() == "")
                {
                    dic.Add("pid", strpid);
                    dic.Add("ppid", strppid);
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    intresult = ppc.add(dic, "pd_product_compose");
                }
                else
                {
                    intresult = ppc.update(dic, "pd_product_compose", "ID", strID);
                }

                inittree(strpdname);

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

        private void erase_1()
        {
            pdnumber1.Text = "1";
            pptype1.Text = "产品";
            remarks1.Text = "";
        }

        private void initbaseinfo_1(string strpid, string strppid)
        {
            if (strpid.Trim() != "" && strppid.Trim() != "")
            {
                pd_product_compose ppc = new pd_product_compose();
                System.Data.DataTable dt = ppc.getEditdata(strpid, strppid);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    editID_1.Text = r["ppid"].ToString().Trim();
                    pdnumber1.Text = r["pdnumber"].ToString().Trim();
                    pptype1.Text = r["pptype"].ToString().Trim();
                    remarks1.Text = r["remarks"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_1()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdnumber", pdnumber1.Text.Trim());
            dic.Add("pptype", pptype1.Text.Trim());
            dic.Add("remarks", remarks1.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #region 分页-1

        private void setPage_1Init()
        {
            GridPageSize_1.Text = "21";
            CurPage_1.Text = "";
            TotalPage_1.Text = "";
            MemoTxt_1.Text = "";
        }

        protected void setPageContent_1(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize_1.Text.Trim());

            if (intType == 1)
            {
                CurPage_1.Text = "1";
                BindGrid_1();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_1.Text.Trim()) / intPagesize));
                MemoTxt_1.Text = "1/" + intTotalPage.ToString();
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage_1.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage_1.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_1.Text.Trim()) / intPagesize));
                        MemoTxt_1.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_1();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage_1.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_1.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage_1.Text = intCurPage.ToString();

                        MemoTxt_1.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_1();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_1.Text.Trim()) / intPagesize));
                CurPage_1.Text = intTotalPage.ToString();

                MemoTxt_1.Text = intTotalPage.ToString() + "/" + intTotalPage.ToString();
                BindGrid_1();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage_1.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_1.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage_1.Text = intCurPage.ToString();

                        MemoTxt_1.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_1();
                    }
                }
            }
        }

        protected void FirstPage1_Click(object sender, EventArgs e)
        {
            setPageContent_1(1);
        }

        protected void PrePage1_Click(object sender, EventArgs e)
        {
            setPageContent_1(2);
        }

        protected void NextPage1_Click(object sender, EventArgs e)
        {
            setPageContent_1(3); ;
        }

        protected void LastPage1_Click(object sender, EventArgs e)
        {
            setPageContent_1(4); ;
        }

        protected void GoPage1_Click(object sender, EventArgs e)
        {
            setPageContent_1(5); ;
        }

        protected void SubNumber1_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_1.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize_1.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber1_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_1.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize_1.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #endregion

        #region Tab-元零件库

        #region tree_2树初始化
        private void inittree_2()
        {
            tree_2.Nodes.Clear();
            // 模拟从数据库返回数据表
            pd_product pp = new pd_product();
            DataTable table = pp.getBindTree_2DataAsdt();

            if (table != null && table.Rows.Count > 0)
            {

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
                        node.EnableClickEvent = true;

                        tree_2.Nodes.Add(node);
                        ResolveSubTree_2(row, node);
                    }
                }
            }

        }

        private void ResolveSubTree_2(DataRow dataRow, FineUIPro.TreeNode treeNode)
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
                    node.EnableClickEvent = true;

                    treeNode.Nodes.Add(node);

                    ResolveSubTree_2(row, node);
                }
            }
        }

        #endregion

        protected void f2_mainclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            initsubclass(f2_mainclass.SelectedValue.Trim());
        }

        protected void tree_2_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = tree_2.SelectedNode;

            if (treenode.Leaf)
            {               
                strminclassID = treenode.ParentNode.NodeID.Trim();
                strsubclassID = treenode.NodeID.Trim();

                setPageContent_2(1);
            }

        }

        private string strminclassID = "";
        private string strsubclassID = "";

        private void BindGrid_2()
        {
            string strf2_mname = f2_mname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mainclassID", strminclassID);
            dic.Add("subclassID", strsubclassID);
            dic.Add("mname", strf2_mname);

            int intPageindex = Convert.ToInt32(CurPage_2.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize_2.Text.Trim());
            string strSort = Grid_2.SortField;
            string strSortDirection = Grid_2.SortDirection;

            pd_product pp = new pd_product();
            DataTable dt = pp.getBindMkitDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage_2.Text = pp.getMkittotalpage(dic); ;

            Grid_2.DataSource = dt;
            Grid_2.DataBind();
        }

        protected void btnFind_2_Click(object sender, EventArgs e)
        {
            strminclassID = f2_mainclass.SelectedValue.Trim();
            strsubclassID = f2_subclass.SelectedValue.Trim();
           
            setPageContent_2(1);
        }

        protected void Grid_2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                string strpid = treenode.NodeID.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                }

                string strppid = Grid_2.DataKeys[e.RowIndex][0].ToString().Trim();
                editID_2.Text = strppid;

                pd_product_compose ppc = new pd_product_compose();

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();

                if (strID.Trim() != "")
                {
                    neweditWindow_2.Title = "编辑组装元零件";
                    editID_2.Text = "";
                    initbaseinfo_2(strpid, strppid);
                }
                else
                {
                    neweditWindow_2.Title = "新增组装元零件";
                    erase_2();
                }

                neweditWindow_2.Hidden = false;
            }
        }

        protected void Grid_2_RowClick(object sender, GridRowClickEventArgs e)
        {
            pd_machinekit_feature pmf = new pd_machinekit_feature();

            string strID = Grid_2.DataKeys[e.RowIndex][0].ToString().Trim();
            string strmtypeid= Grid_2.DataKeys[e.RowIndex][1].ToString().Trim();
            System.Data.DataTable dt = pmf.getBindGridDataAsdt(strmtypeid, strID);

            Grid_21.DataSource = dt;
            Grid_21.DataBind();
        }

        protected void btnSave_2_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                Dictionary<string, string> dic = initDatadic_2();
                pd_product_compose ppc = new pd_product_compose();
                int intresult = 0;

                string strpid = treenode.NodeID.Trim();
                string strppid = editID_2.Text.Trim();
                string strpdname = treenode.Text.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                    strpdname = treenode.ParentNode.Text.Trim();
                }

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();
                if (strID.Trim() == "")
                {
                    dic.Add("pid", strpid);
                    dic.Add("ppid", strppid);
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    intresult = ppc.add(dic, "pd_product_compose");
                }
                else
                {
                    intresult = ppc.update(dic, "pd_product_compose", "ID", strID);
                }

                inittree(strpdname);

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

        private void erase_2()
        {
            pdnumber2.Text = "1";
            pptype2.Text = "元零件";
            remarks2.Text = "";
        }

        private void initbaseinfo_2(string strpid, string strppid)
        {
            if (strpid.Trim() != "" && strppid.Trim() != "")
            {
                pd_product_compose ppc = new pd_product_compose();
                System.Data.DataTable dt = ppc.getEditdata(strpid, strppid);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    editID_2.Text = r["ppid"].ToString().Trim();
                    pdnumber2.Text = r["pdnumber"].ToString().Trim();
                    pptype2.Text = r["pptype"].ToString().Trim();
                    remarks2.Text = r["remarks"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_2()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdnumber", pdnumber2.Text.Trim());
            dic.Add("pptype", pptype2.Text.Trim());
            dic.Add("remarks", remarks2.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #region 分页-2

        private void setPage_2Init()
        {
            GridPageSize_2.Text = "21";
            CurPage_2.Text = "";
            TotalPage_2.Text = "";
            MemoTxt_2.Text = "";
        }

        protected void setPageContent_2(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize_2.Text.Trim());

            if (intType == 1)
            {
                CurPage_2.Text = "1";
                BindGrid_2();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_2.Text.Trim()) / intPagesize));
                MemoTxt_2.Text = "1/" + intTotalPage.ToString();
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage_2.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage_2.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_2.Text.Trim()) / intPagesize));
                        MemoTxt_2.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_2();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage_2.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_2.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage_2.Text = intCurPage.ToString();

                        MemoTxt_2.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_2();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_2.Text.Trim()) / intPagesize));
                CurPage_2.Text = intTotalPage.ToString();

                MemoTxt_2.Text = intTotalPage.ToString() + "/" + intTotalPage.ToString();
                BindGrid_2();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage_2.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_2.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage_2.Text = intCurPage.ToString();

                        MemoTxt_2.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_2();
                    }
                }
            }
        }

        protected void FirstPage2_Click(object sender, EventArgs e)
        {
            setPageContent_2(1);
        }

        protected void PrePage2_Click(object sender, EventArgs e)
        {
            setPageContent_2(2);
        }

        protected void NextPage2_Click(object sender, EventArgs e)
        {
            setPageContent_2(3); ;
        }

        protected void LastPage2_Click(object sender, EventArgs e)
        {
            setPageContent_2(4); ;
        }

        protected void GoPage2_Click(object sender, EventArgs e)
        {
            setPageContent_2(5); ;
        }

        protected void SubNumber2_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_2.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize_2.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber2_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_2.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize_2.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #endregion

        #region Tab-标准件库

        private void BindGrid_3()
        {
            string strf3_stype = f3_stype.SelectedValue.Trim();
            string strf3_sname = f3_sname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("stype", strf3_stype);
            dic.Add("sname", strf3_sname);

            int intPageindex = Convert.ToInt32(CurPage_3.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize_3.Text.Trim());
            string strSort = Grid_3.SortField;
            string strSortDirection = Grid_3.SortDirection;

            pd_product pp = new pd_product();
            DataTable dt = pp.getBindStandDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage_3.Text = pp.getStandtotalpage(dic); ;

            Grid_3.DataSource = dt;
            Grid_3.DataBind();
        }

        protected void btnFind_3_Click(object sender, EventArgs e)
        {
            setPageContent_3(1);
        }

        protected void Grid_3_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                string strpid = treenode.NodeID.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                }

                string strppid = Grid_3.DataKeys[e.RowIndex][0].ToString().Trim();
                editID_3.Text = strppid;

                pd_product_compose ppc = new pd_product_compose();

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();

                if (strID.Trim() != "")
                {
                    neweditWindow_3.Title = "编辑组装标准件";
                    editID_3.Text = "";
                    initbaseinfo_3(strpid, strppid);
                }
                else
                {
                    neweditWindow_3.Title = "新增组装标准件";
                    erase_3();
                }

                neweditWindow_3.Hidden = false;
            }
        }

        protected void btnSave_3_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                Dictionary<string, string> dic = initDatadic_3();
                pd_product_compose ppc = new pd_product_compose();
                int intresult = 0;

                string strpid = treenode.NodeID.Trim();
                string strppid = editID_3.Text.Trim();
                string strpdname = treenode.Text.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                    strpdname = treenode.ParentNode.Text.Trim();
                }

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();
                if (strID.Trim() == "")
                {
                    dic.Add("pid", strpid);
                    dic.Add("ppid", strppid);
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    intresult = ppc.add(dic, "pd_product_compose");
                }
                else
                {
                    intresult = ppc.update(dic, "pd_product_compose", "ID", strID);
                }

                inittree(strpdname);

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

        private void erase_3()
        {
            pdnumber3.Text = "1";
            pptype3.Text = "标准件";
            remarks3.Text = "";
        }

        private void initbaseinfo_3(string strpid, string strppid)
        {
            if (strpid.Trim() != "" && strppid.Trim() != "")
            {
                pd_product_compose ppc = new pd_product_compose();
                System.Data.DataTable dt = ppc.getEditdata(strpid, strppid);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    editID_3.Text = r["ppid"].ToString().Trim();
                    pdnumber3.Text = r["pdnumber"].ToString().Trim();
                    pptype3.Text = r["pptype"].ToString().Trim();
                    remarks3.Text = r["remarks"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_3()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdnumber", pdnumber3.Text.Trim());
            dic.Add("pptype", pptype3.Text.Trim());
            dic.Add("remarks", remarks3.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #region 分页-3

        private void setPage_3Init()
        {
            GridPageSize_3.Text = "21";
            CurPage_3.Text = "";
            TotalPage_3.Text = "";
            MemoTxt_3.Text = "";
        }

        protected void setPageContent_3(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize_3.Text.Trim());

            if (intType == 1)
            {
                CurPage_3.Text = "1";
                BindGrid_3();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_3.Text.Trim()) / intPagesize));
                MemoTxt_3.Text = "第 1 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_3.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage_3.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage_3.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_3.Text.Trim()) / intPagesize));
                        MemoTxt_3.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_3.Text.Trim() + " 条数据";
                        BindGrid_3();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage_3.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_3.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage_3.Text = intCurPage.ToString();

                        MemoTxt_3.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_3.Text.Trim() + " 条数据";
                        BindGrid_3();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_3.Text.Trim()) / intPagesize));
                CurPage_3.Text = intTotalPage.ToString();

                MemoTxt_3.Text = "终页 共 " + intTotalPage.ToString() +
                    " 页 " + TotalPage_3.Text.Trim() + " 条数据";
                BindGrid_3();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage_3.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_3.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage_3.Text = intCurPage.ToString();

                        MemoTxt_3.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_3.Text.Trim() + " 条数据";
                        BindGrid_3();
                    }
                }
            }
        }

        protected void FirstPage3_Click(object sender, EventArgs e)
        {
            setPageContent_3(1);
        }

        protected void PrePage3_Click(object sender, EventArgs e)
        {
            setPageContent_3(2);
        }

        protected void NextPage3_Click(object sender, EventArgs e)
        {
            setPageContent_3(3); ;
        }

        protected void LastPage3_Click(object sender, EventArgs e)
        {
            setPageContent_3(4); ;
        }

        protected void GoPage3_Click(object sender, EventArgs e)
        {
            setPageContent_3(5); ;
        }

        protected void SubNumber3_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_3.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize_3.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber3_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_3.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize_3.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #endregion

        #region Tab-外购件库

        private void BindGrid_4()
        {
            string strf4_otype = f4_otype.SelectedValue.Trim();
            string strf4_oname = f4_oname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("otype", strf4_otype);
            dic.Add("oname", strf4_oname);

            int intPageindex = Convert.ToInt32(CurPage_4.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize_4.Text.Trim());
            string strSort = Grid_4.SortField;
            string strSortDirection = Grid_4.SortDirection;

            pd_product pp = new pd_product();
            DataTable dt = pp.getBindOutDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage_4.Text = pp.getOuttotalpage(dic); ;

            Grid_4.DataSource = dt;
            Grid_4.DataBind();
        }

        protected void btnFind_4_Click(object sender, EventArgs e)
        {
            setPageContent_4(1);
        }

        protected void Grid_4_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                string strpid = treenode.NodeID.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                }

                string strppid = Grid_4.DataKeys[e.RowIndex][0].ToString().Trim();
                editID_4.Text = strppid;

                pd_product_compose ppc = new pd_product_compose();

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();

                if (strID.Trim() != "")
                {
                    neweditWindow_4.Title = "编辑组装外购件";
                    editID_4.Text = "";
                    initbaseinfo_4(strpid, strppid);
                }
                else
                {
                    neweditWindow_4.Title = "新增组装外购件";
                    erase_4();
                }

                neweditWindow_4.Hidden = false;
            }
        }
         
        protected void btnSave_4_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                Dictionary<string, string> dic = initDatadic_4();
                pd_product_compose ppc = new pd_product_compose();
                int intresult = 0;

                string strpid = treenode.NodeID.Trim();
                string strppid = editID_4.Text.Trim();
                string strpdname = treenode.Text.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                    strpdname = treenode.ParentNode.Text.Trim();
                }

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();
                if (strID.Trim() == "")
                {
                    dic.Add("pid", strpid);
                    dic.Add("ppid", strppid);
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    intresult = ppc.add(dic, "pd_product_compose");
                }
                else
                {
                    intresult = ppc.update(dic, "pd_product_compose", "ID", strID);
                }

                inittree(strpdname);

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

        private void erase_4()
        {
            pdnumber4.Text = "1";
            pptype4.Text = "外购件";
            remarks4.Text = "";
        }

        private void initbaseinfo_4(string strpid, string strppid)
        {
            if (strpid.Trim() != "" && strppid.Trim() != "")
            {
                pd_product_compose ppc = new pd_product_compose();
                System.Data.DataTable dt = ppc.getEditdata(strpid, strppid);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    editID_4.Text = r["ppid"].ToString().Trim();
                    pdnumber4.Text = r["pdnumber"].ToString().Trim();
                    pptype4.Text = r["pptype"].ToString().Trim();
                    remarks4.Text = r["remarks"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_4()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdnumber", pdnumber4.Text.Trim());
            dic.Add("pptype", pptype4.Text.Trim());
            dic.Add("remarks", remarks4.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #region 分页-4

        private void setPage_4Init()
        {
            GridPageSize_4.Text = "21";
            CurPage_4.Text = "";
            TotalPage_4.Text = "";
            MemoTxt_4.Text = "";
        }

        protected void setPageContent_4(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize_4.Text.Trim());

            if (intType == 1)
            {
                CurPage_4.Text = "1";
                BindGrid_4();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_4.Text.Trim()) / intPagesize));
                MemoTxt_4.Text = "第 1 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_4.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage_4.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage_4.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_4.Text.Trim()) / intPagesize));
                        MemoTxt_4.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_4.Text.Trim() + " 条数据";
                        BindGrid_4();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage_4.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_4.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage_4.Text = intCurPage.ToString();

                        MemoTxt_4.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_4.Text.Trim() + " 条数据";
                        BindGrid_4();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_4.Text.Trim()) / intPagesize));
                CurPage_4.Text = intTotalPage.ToString();

                MemoTxt_4.Text = "终页 共 " + intTotalPage.ToString() +
                    " 页 " + TotalPage_4.Text.Trim() + " 条数据";
                BindGrid_4();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage_4.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_4.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage_4.Text = intCurPage.ToString();

                        MemoTxt_4.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage_4.Text.Trim() + " 条数据";
                        BindGrid_4();
                    }
                }
            }
        }

        protected void FirstPage4_Click(object sender, EventArgs e)
        {
            setPageContent_4(1);
        }

        protected void PrePage4_Click(object sender, EventArgs e)
        {
            setPageContent_4(2);
        }

        protected void NextPage4_Click(object sender, EventArgs e)
        {
            setPageContent_4(3); ;
        }

        protected void LastPage4_Click(object sender, EventArgs e)
        {
            setPageContent_4(4); ;
        }

        protected void GoPage4_Click(object sender, EventArgs e)
        {
            setPageContent_4(5); ;
        }

        protected void SubNumber4_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_4.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize_4.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber4_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_4.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize_4.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #endregion

        #region Tab-元组件库

        private void BindGrid_5()
        {
            string strf5_comtype = f5_comtype.SelectedValue.Trim();
            string strf5_comname = f5_comname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("comtype", strf5_comtype);
            dic.Add("comname", strf5_comname);

            int intPageindex = Convert.ToInt32(CurPage_5.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize_5.Text.Trim());
            string strSort = Grid_5.SortField;
            string strSortDirection = Grid_5.SortDirection;

            pd_product pp = new pd_product();
            DataTable dt = pp.getBindComDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage_5.Text = pp.getComtotalpage(dic); ;

            Grid_5.DataSource = dt;
            Grid_5.DataBind();
        }

        protected void btnFind_5_Click(object sender, EventArgs e)
        {
            setPageContent_5(1);
        }

        protected void Grid_5_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                string strpid = treenode.NodeID.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid = treenode.ParentNode.NodeID.Trim();
                }

                string strppid = Grid_5.DataKeys[e.RowIndex][0].ToString().Trim();
                editID_5.Text = strppid;

                pd_product_compose ppc = new pd_product_compose();

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();

                if (strID.Trim() != "")
                {
                    neweditWindow_5.Title = "编辑组装元组件";
                    editID_5.Text = "";
                    initbaseinfo_5(strpid, strppid);
                }
                else
                {
                    neweditWindow_5.Title = "新增组装元组件";
                    erase_5();
                }

                neweditWindow_5.Hidden = false;
            }
        }

        protected void Grid_5_RowClick(object sender, GridRowClickEventArgs e)
        {
            pd_product pp = new pd_product();
            System.Data.DataTable dt = pp.getBindComposeDataAsdt(Grid_5.DataKeys[e.RowIndex][0].ToString().Trim());

            Grid_51.DataSource = dt;
            Grid_51.DataBind();
        }

        protected void btnSave_5_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode != null && (treenode.Attributes["nodemenu"].ToString().Trim() == "1" ||
                treenode.Attributes["nodemenu"].ToString().Trim() == "2"))
            {
                Dictionary<string, string> dic = initDatadic_5();
                pd_product_compose ppc = new pd_product_compose();
                int intresult = 0;

                string strpid = treenode.NodeID.Trim();
                string strppid = editID_5.Text.Trim();
                string strpdname = treenode.Text.Trim();

                if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
                {
                    strpid= treenode.ParentNode.NodeID.Trim();
                    strpdname = treenode.ParentNode.Text.Trim();
                }

                Dictionary<string, string> edic = new Dictionary<string, string>();
                edic.Add("pid", " cast(pid as varchar(36))='" + strpid + "' ");
                edic.Add("ppid", " cast(ppid as varchar(36))='" + strppid + "' ");

                string strID = ppc.isExistdata("pd_product_compose", edic, "ID").Trim();
                if (strID.Trim()== "")
                {
                    dic.Add("pid", strpid);
                    dic.Add("ppid", strppid);
                    dic.Add("ID", Guid.NewGuid().ToString().Trim());
                    intresult = ppc.add(dic, "pd_product_compose");                    
                }
                else
                {
                    intresult = ppc.update(dic, "pd_product_compose", "ID", strID);
                }

                inittree(strpdname);

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

        private void erase_5()
        {
            pdnumber5.Text = "1";
            pptype5.Text = "元组件";
            remarks5.Text = "";
        }

        private void initbaseinfo_5(string strpid,string strppid)
        {
            if (strpid.Trim() != "" && strppid.Trim()!="")
            {
                pd_product_compose ppc = new pd_product_compose();
                System.Data.DataTable dt = ppc.getEditdata(strpid,strppid);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    editID_5.Text= r["ppid"].ToString().Trim();
                    pdnumber5.Text = r["pdnumber"].ToString().Trim();
                    pptype5.Text = r["pptype"].ToString().Trim();
                    remarks5.Text = r["remarks"].ToString().Trim();
                }
            }
        }

        private Dictionary<string, string> initDatadic_5()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pdnumber", pdnumber5.Text.Trim());
            dic.Add("pptype", pptype5.Text.Trim());
            dic.Add("remarks", remarks5.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        #region 分页-5

        private void setPage_5Init()
        {
            GridPageSize_5.Text = "21";
            CurPage_5.Text = "";
            TotalPage_5.Text = "";
            MemoTxt_5.Text = "";
        }

        protected void setPageContent_5(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize_5.Text.Trim());

            if (intType == 1)
            {
                CurPage_5.Text = "1";
                BindGrid_5();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_5.Text.Trim()) / intPagesize));
                MemoTxt_5.Text ="1/" + intTotalPage.ToString();
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage_5.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage_5.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_5.Text.Trim()) / intPagesize));
                        MemoTxt_5.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_5();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage_5.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_5.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage_5.Text = intCurPage.ToString();

                        MemoTxt_5.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_5();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_5.Text.Trim()) / intPagesize));
                CurPage_5.Text = intTotalPage.ToString();

                MemoTxt_5.Text = intTotalPage.ToString()+ "/" + intTotalPage.ToString();
                BindGrid_5();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage_5.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage_5.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage_5.Text = intCurPage.ToString();

                        MemoTxt_5.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid_5();
                    }
                }
            }
        }

        protected void FirstPage5_Click(object sender, EventArgs e)
        {
            setPageContent_5(1);
        }

        protected void PrePage5_Click(object sender, EventArgs e)
        {
            setPageContent_5(2);
        }

        protected void NextPage5_Click(object sender, EventArgs e)
        {
            setPageContent_5(3); ;
        }

        protected void LastPage5_Click(object sender, EventArgs e)
        {
            setPageContent_5(4); ;
        }

        protected void GoPage5_Click(object sender, EventArgs e)
        {
            setPageContent_5(5); ;
        }

        protected void SubNumber5_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_5.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize_5.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber5_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize_5.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize_5.Text = intGridPageSize.ToString();
                }
            }
        }

        #endregion

        #endregion
        
    }
}
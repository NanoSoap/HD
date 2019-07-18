using System;
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
using System.Data;
using HDPages.craftLib;

namespace HDpmw.productdesign
{
    public partial class productdetail : PageBase
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
            inittree();
            setPageInit();
            setPageInit1();
            setPageInit2();
            setPageInit3();
            setPageInit4();
            setPageInit5();
            setPageInit6();
            setPageInit7();
            setPageInit8();
            //setPageInit10();
        }
        
        #region firsttree树初始化
        private void inittree()
        {
            firsttree.Nodes.Clear();
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

            //getnodesum(firsttree.Nodes[0]);

            //setrepeat();
        }
        private void inittree(string productname,string pID)
        {
            Tree1.Nodes.Clear();
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbyproduct(productname, pID);

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


                    Tree1.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittreebymachine(string productname, string pID)
        {
            Tree2.Nodes.Clear();
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbymachine(productname, pID);

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


                    Tree2.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittreebystandard(string productname, string pID)
        {
            Tree3.Nodes.Clear();
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbystandard(productname, pID);

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


                    Tree3.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittreebyoutbuy(string productname, string pID) {
            Tree4.Nodes.Clear();
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbyoutbuy(productname, pID);

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


                    Tree4.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittreebycompongall(string productname, string pID)
        {
            Tree5.Nodes.Clear();
            // 从数据库返回数据表
            pd_compongall pp = new pd_compongall();
            DataTable table = pp.getBindTreeDataAsdtbycompongall(productname, pID);

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


                    Tree5.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
        }
        private void inittree(string strpdname)
        {
            Tree6.Nodes.Clear();
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

                        Tree6.Nodes.Add(node);
                        ResolveSubTree1(row, node);
                    }
                }
            }
            else
            {
                Alert.Show("未找到产品：" + strpdname);
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
                    node.Text = row["nodetext"].ToString();
                    node.NodeID = row["id"].ToString().Trim();
                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    
                        node.EnableClickEvent = true;
                    
                    
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }
        private void ResolveSubTree1(DataRow dataRow, FineUIPro.TreeNode treeNode)
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

                    node.ToolTip = row["pdnumber"].ToString();

                    treeNode.Nodes.Add(node);

                    ResolveSubTree1(row, node);
                }
            }
        }

        #endregion
        private void initpdtype()
        {
            xparams x = new xparams();

            string str = x.getparamData("H016");
            
            f_pdtype.DataSource = commonLib.stringTolist(str, ',');
            f_pdtype.DataBind();
        }
        
        private void initterase(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("kitID", id);
            pd_compongall com = new pd_compongall();
            DataTable dt = com.getBindDataAsdtbyproduct(dic);
            string strhtml = "空白详细信息";
            int pronumber = 0;
            int comnumber = 0;
            int stanumber = 0;
            int outnumber = 0;
            int macnumber = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                strhtml = "<div><p><b>产品</b></p>";
                strhtml += "<p>";
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    System.Data.DataRow r = dt.Rows[i];
                    if(r["ptype"].ToString().Trim()=="产品")
                    {
                        strhtml += "" + r["name"] + ":" + r["number"] + "个,";
                        pronumber += int.Parse(r["number"].ToString());
                    }
                }
                strhtml += "</p>";
                strhtml += "<p>总计:"+pronumber+"个</p><br/>";
                strhtml += "<p><b>元组件</b></p>";
                strhtml += "<p>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow r = dt.Rows[i];
                    if (r["ptype"].ToString().Trim() == "元组件")
                    {
                        strhtml += "" + r["name"] + ":" + r["number"] + "个,";
                        comnumber += int.Parse(r["number"].ToString());
                    }
                }
                strhtml += "</p>";
                strhtml += "<p>总计:" + comnumber + "个</p><br/>";
                strhtml += "<p><b>标准件</b></p>";
                strhtml += "<p>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow r = dt.Rows[i];
                    if (r["ptype"].ToString().Trim() == "标准件")
                    {
                        strhtml += "" + r["name"] + ":" + r["number"] + "个,";
                        stanumber += int.Parse(r["number"].ToString());
                    }
                }
                strhtml += "</p>";
                strhtml += "<p>总计:" + stanumber + "个</p><br/>";
                strhtml += "<p><b>外购件</b></p>";
                strhtml += "<p>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow r = dt.Rows[i];
                    if (r["ptype"].ToString().Trim() == "外购件")
                    {
                        strhtml += "" + r["name"] + ":" + r["number"] + "个,";
                        outnumber += int.Parse(r["number"].ToString());
                    }
                }
                strhtml += "</p>";
                strhtml += "<p>总计:" + outnumber + "个</p><br/>";
                strhtml += "<p><b>元零件</b></p>";
                strhtml += "<p>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow r = dt.Rows[i];
                    if (r["ptype"].ToString().Trim() == "元零件")
                    {
                        strhtml += "" + r["name"] + ":" + r["number"] + "个,";
                        macnumber += int.Parse(r["number"].ToString());
                    }
                }
                strhtml += "</p>";
                strhtml += "<p>总计:" + macnumber + "个</p><br/></div>";
            }
            detailinfo.Text = strhtml;
        }

        #endregion

        #region 主菜单操作

        protected void btnFind_Click(object sender, EventArgs e)
        {

            string strpdname = f_pdname.Text.Trim();

            traversaltree(firsttree.Nodes[0], "", strpdname);
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
        #endregion

        #region firsttree树目录操作-右键菜单

        protected void firsttree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                Panel3.Hidden = false;
                Panel1.Hidden = false;
                erase();
                initbaseinfo(treenode.NodeID.Trim());
                initphoto(treenode.NodeID);
                inittree(treenode.Text.Trim(), treenode.NodeID.Trim());
                inittreebymachine(treenode.Text.Trim(), treenode.NodeID.Trim());
                inittreebystandard(treenode.Text.Trim(), treenode.NodeID.Trim());
                inittreebyoutbuy(treenode.Text.Trim(), treenode.NodeID.Trim());
                inittreebycompongall(treenode.Text.Trim(), treenode.NodeID.Trim());
                inittree(treenode.Text.Trim());
                initterase(treenode.NodeID);
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }

        }
        protected void Tree1_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = Tree1.SelectedNode;
            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                setPageContent5(1);
            }
        }
        protected void Tree2_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = Tree2.SelectedNode;
            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                initbaseinfomach(treenode.NodeID);
                BindGrid(treenode.ParentNode.NodeID, treenode.NodeID);
                //初始化图片
                initphoto1(treenode.NodeID);
                setPageContent(1);
                initbaseinfother(treenode.NodeID);
                cd_thermal ct = new cd_thermal();
                string strtid = ct.isExistdata("cd_thermal", "pid", treenode.NodeID.Trim(), "ID").Trim();
                if (strtid != "")
                {
                    BindGrid(strtid);
                    //初始化图片
                    initphoto(strtid);
                }
            }
        }
        private void BindGrid(string strmtypeid, string strtID)
        {
            pd_machinekit_feature pmf = new pd_machinekit_feature();

            Grid4.DataSource = pmf.getBindGridDataAsdt(strmtypeid, strtID);
            Grid4.DataBind();
        }
        private void BindGrid(string strtid)
        {
            cd_thermal_batch ctb = new cd_thermal_batch();
            cd_thermal_indicator ctic = new cd_thermal_indicator();

            secondGrid.DataSource = ctic.getBindGridDataAsdt(strtid);
            secondGrid.DataBind();

            secondGrid1.DataSource = ctb.getBindGridDataAsdt(strtid);
            secondGrid1.DataBind();
        }
        protected void cadLookup_Click(object sender, EventArgs e)
        {
            Panmach.Hidden = false;
            Panmech.Hidden = true;
            Panther.Hidden = true;
            FineUIPro.TreeNode treenode = Tree2.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_cad pc = new pd_cad();
                string strfilename = pc.isExistdata("pd_cad", "pid", treenode.NodeID, "filename");

                //GrapkTabStrip.ActiveTabIndex = 2;

                PageContext.RegisterStartupScript("openwebcad1('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + strfilename + "')");
            }
        }
        protected void btncadther_Click(object sender, EventArgs e)
        {
            Panmech.Hidden = true;
            Panther.Hidden = false;
            Panmach.Hidden = true;
            FineUIPro.TreeNode treenode = Tree2.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_cad pc = new pd_cad();
                string strfilename = pc.isExistdata("pd_cad", "pid", treenode.NodeID, "filename");

                //GrapkTabStrip.ActiveTabIndex = 2;

                PageContext.RegisterStartupScript("openwebcad2('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + strfilename + "')");
            }
        }
        protected void Tree3_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = Tree3.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_standardkit st = new pd_standardkit();
                DataTable dt = st.getEditdata(treenode.NodeID.Trim());
                pd_photo pp = new pd_photo();
                string strfilename = pp.isExistdata("pd_photo", "pid", treenode.NodeID.Trim(), "filename");
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
                    Image1.ImageUrl = "~/resources/photo/" + strfilename;
                }
                else
                {
                    Image1.ImageUrl = "~/resources/photo/法兰.png";
                }
                //PageContext.RegisterStartupScript("imgPhotozoom()");
            }
        }
        protected void Tree4_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = Tree4.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                pd_outbuykit st = new pd_outbuykit();
                DataTable dt = st.getEditdata(treenode.NodeID.Trim());
                pd_photo pp = new pd_photo();
                string strfilename = pp.isExistdata("pd_photo", "pid", treenode.NodeID.Trim(), "filename");
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
                    Image2.ImageUrl = "~/resources/photo/" + strfilename;
                }
                else
                {
                    Image2.ImageUrl = "~/resources/photo/法兰.png";
                }
                PageContext.RegisterStartupScript("imgPhotozoom()");
            }
        }
        protected void Tree5_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            FineUIPro.TreeNode treenode = Tree5.SelectedNode;
            if (treenode.Attributes["nodemenu"].ToString().Trim() == "2")
            {
                setPageContent7(1);
            }
        }
        protected void Tree6_NodeCommand(object sender, TreeCommandEventArgs e)
        {

        }
        protected void menuCAD_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;

            if (treenode.Attributes["nodemenu"].ToString().Trim() == "1")
            {
                pd_cad pc = new pd_cad();
                string strfilename = pc.isExistdata("pd_cad", "pid", treenode.NodeID, "filename");

                //GrapkTabStrip.ActiveTabIndex = 2;

                PageContext.RegisterStartupScript("openwebcad('http://" + Request.Url.Authority.Trim() + "/resources/cad/" + strfilename + "')");
            }
        }
        protected void menuMech_Click(object sender, EventArgs e)
        {
            Panmech.Hidden = false;
            Panther.Hidden = true;
            Panmach.Hidden = true;
        }
        protected void menuTher_Click(object sender, EventArgs e)
        {
            Panmech.Hidden = true;
            Panther.Hidden = false;
            Panmach.Hidden = true;
        }
        protected void menuMach_Click(object sender, EventArgs e)
        {
            Panmach.Hidden = false;
            Panmech.Hidden = true;
            Panther.Hidden = true;
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
        
        #endregion

        #region 图纸操作-零件保存/上传/查看

        private string getpdcode()
        {
            pd_product pp = new pd_product();

            string strpdcode = pp.getmaxpdcode();
            string strprecode = "P";

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
            sname.Text = "";
            scode.Text = "";
            stype.Text = "";
            specification.Text = "";
            material.Text = "";
            oname.Text = "";
            ocode.Text = "";
            otype.Text = "";
            ospecification.Text = "";
            imgPhoto.ImageUrl = "~/resources/photo/blank.png";
            Image1.ImageUrl = "~/resources/photo/法兰.png";
            Image2.ImageUrl = "~/resources/photo/法兰.png";
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
                    pdtype.Text = r["pdtype"].ToString().Trim();
                    drawdate.Text = r["drawdate"].ToString().Trim();
                    designer.Text = r["designer"].ToString().Trim();
                    checker.Text = r["checker"].ToString().Trim();
                    stanarder.Text = r["stanarder"].ToString().Trim();
                    drawer.Text = r["drawer"].ToString().Trim();
                    examiner.Text = r["examiner"].ToString().Trim();
                    specifications.Text = r["specifications"].ToString().Trim();
                }
            }
        }
        private void initbaseinfomach(string strID)
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
                    drawdate1.Text = r["drawdate"].ToString().Trim();
                    designer.Text = r["designer"].ToString().Trim();
                    checker.Text = r["checker"].ToString().Trim();
                    stanarder.Text = r["stanarder"].ToString().Trim();
                    drawer.Text = r["drawer"].ToString().Trim();
                    examiner.Text = r["examiner"].ToString().Trim();
                    specifications.Text = r["specifications"].ToString().Trim();
                }
            }
        }
        private void initbaseinfother(string strID)
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
                    normalperson.Text = r["normalperson"].ToString().Trim();
                    approveperson.Text = r["approveperson"].ToString().Trim();
                    designperson.Text = r["designperson"].ToString().Trim();
                    auditperson.Text = r["auditperson"].ToString().Trim();

                    normaldate.Text = r["normaldate"].ToString().Trim();
                    approvedate.Text = r["approvedate"].ToString().Trim();
                    designdate.Text = r["designdate"].ToString().Trim();
                    auditdate.Text = r["auditdate"].ToString().Trim();
                }
            }
        }
        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


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
        private void initphoto1(string strpid)
        {
            pd_photo pp = new pd_photo();
            string strfilename = pp.isExistdata("pd_photo", "pid", strpid, "filename");

            if (strfilename.Trim() != "")
            {
                imgPhoto1.ImageUrl = "~/resources/photo/" + strfilename;
            }
            else
            {
                imgPhoto1.ImageUrl = "~/resources/photo/法兰.png";
            }
        }
        #endregion

        #region Tab-产品库
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent1(1);
        }
        protected void Grid5_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent4(1);
        }
        private void BindGridNode()
        {
            cd_integration ig = new cd_integration();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            dic.Add("kitID", Tree1.SelectedNode.NodeID);

            int intPageindex = Convert.ToInt32(CurPage4.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize4.Text.Trim());
            string strSort = Grid1.SortField;
            string strSortDirection = Grid1.SortDirection;

            DataTable dt = ig.getBindDataAsdtNode(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ig.getBindDataAsdtNode(dic, strSort, strSortDirection);
            TotalPage4.Text = dt1.Rows.Count.ToString();

            Grid1.DataSource = dt;
            Grid1.DataBind();
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
                string strSortDirection1 = Grid5.SortDirection;

                DataTable dt = bh.getBindDataAsdt(dic, strSort1, strSortDirection1, intPagesize1, intPageindex1);
                DataTable dt1 = bh.getBindDataAsdt(dic, strSort1, strSortDirection1);
                TotalPage1.Text = dt1.Rows.Count.ToString();

                Grid5.DataSource = dt;
                Grid5.DataBind();

            }
        }
        private void BindGrid4()
        {
            cd_integration_batch_step step = new cd_integration_batch_step();

            if (Grid5.SelectedRowIndexArray.Length > 0)
            {

                string strbatchid = Grid5.DataKeys[Grid5.SelectedRowIndexArray[0]][0].ToString();



                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("batchid", strbatchid);

                int intPageindex2 = Convert.ToInt32(CurPage5.Text.Trim());
                int intPagesize2 = Convert.ToInt32(GridPageSize5.Text.Trim());
                //string strSort2 = "systemdate";//Grid2.SortField;
                //string strSortDirection2 = Grid2.SortDirection;

                DataTable dt = step.getBindDataAsdt(dic, Grid6.SortField, Grid6.SortDirection, intPagesize2, intPageindex2);
                DataTable dt2 = step.getBindDataAsdt(dic, Grid6.SortField, Grid6.SortDirection);
                TotalPage5.Text = dt2.Rows.Count.ToString();

                Grid6.DataSource = dt;
                Grid6.DataBind();

            }
            else
            {
                TotalPage5.Text = "1";
            }
        }
        protected void setPageContent1(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize1.Text.Trim());

            if (intType == 1)
            {
                CurPage1.Text = "1";
                BindGrid1();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                MemoTxt1.Text = "1/" + intTotalPage.ToString() + "";
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
                        MemoTxt1.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
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

                        MemoTxt1.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid1();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage1.Text.Trim()) / intPagesize));
                CurPage1.Text = intTotalPage.ToString();
                MemoTxt1.Text = "终/" + intTotalPage.ToString();
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

                        MemoTxt1.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid1();
                    }
                }
            }
        }
        protected void setPageContent4(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize5.Text.Trim());

            if (intType == 1)
            {
                CurPage5.Text = "1";
                BindGrid4();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage5.Text.Trim()) / intPagesize));
                MemoTxt5.Text = "1/" + intTotalPage.ToString() + "";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage5.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage5.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage5.Text.Trim()) / intPagesize));
                        MemoTxt5.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid4();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage5.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage5.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage5.Text = intCurPage.ToString();

                        MemoTxt5.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid4();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage5.Text.Trim()) / intPagesize));
                CurPage5.Text = intTotalPage.ToString();
                MemoTxt5.Text = "终页/" + intTotalPage.ToString() + "页";
                BindGrid4();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage5.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage5.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage5.Text = intCurPage.ToString();

                        MemoTxt5.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid4();
                    }
                }
            }
        }
        protected void setPageContent5(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize4.Text.Trim());

            if (intType == 1)
            {
                CurPage4.Text = "1";
                BindGridNode();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage4.Text.Trim()) / intPagesize));
                MemoTxt4.Text = "1/" + intTotalPage.ToString() + "";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage4.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage5.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage4.Text.Trim()) / intPagesize));
                        MemoTxt5.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage4.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage4.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage4.Text = intCurPage.ToString();

                        MemoTxt4.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage4.Text.Trim()) / intPagesize));
                CurPage4.Text = intTotalPage.ToString();
                MemoTxt4.Text = "终页/" + intTotalPage.ToString() + "";
                BindGridNode();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage4.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage4.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage4.Text = intCurPage.ToString();

                        MemoTxt4.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode();
                    }
                }
            }
        }
        #endregion

        #region Tab-元零件库
        private void BindGrid()
        {
            FineUIPro.TreeNode treenode = Tree2.SelectedNode;
            pd_compongall mc = new pd_compongall();
            string stroname = treenode.NodeID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("pid", stroname);
            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            DataTable dt = mc.getBindDataAsdtbymechanical(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = mc.getBindDataAsdtbymechanical(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();

            mainGrid.DataSource = dt;
            mainGrid.DataBind();
        }

        
        private void BindGrid2()
        {
            cd_mechanical_batch bh = new cd_mechanical_batch();

            if (mainGrid.SelectedRowIndexArray.Length > 0)
            {

                string strid = mainGrid.DataKeys[mainGrid.SelectedRowIndexArray[0]][0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("mid", strid);

                int intPageindex1 = Convert.ToInt32(CurPage2.Text.Trim());
                int intPagesize1 = Convert.ToInt32(GridPageSize2.Text.Trim());
                string strSort1 = "systemdate";//Grid2.SortField;
                string strSortDirection1 = Grid2.SortDirection;

                DataTable dt = bh.getBindDataAsdt(dic, strSort1, strSortDirection1, intPagesize1, intPageindex1);
                DataTable dt1 = bh.getBindDataAsdt(dic, strSort1, strSortDirection1);
                TotalPage2.Text = dt1.Rows.Count.ToString();

                Grid2.DataSource = dt;
                Grid2.DataBind();

            }
        }
        private void BindGrid3()
        {
            cd_mechanical_batch_step step = new cd_mechanical_batch_step();

            if (Grid2.SelectedRowIndexArray.Length > 0)
            {

                string strbatchid = Grid2.DataKeys[Grid2.SelectedRowIndexArray[0]][0].ToString();



                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("batchid", strbatchid);

                int intPageindex2 = Convert.ToInt32(CurPage3.Text.Trim());
                int intPagesize2 = Convert.ToInt32(GridPageSize3.Text.Trim());
                //string strSort2 = "systemdate";//Grid2.SortField;
                //string strSortDirection2 = Grid2.SortDirection;

                DataTable dt = step.getBindDataAsdt(dic, Grid3.SortField, Grid3.SortDirection, intPagesize2, intPageindex2);
                DataTable dt2 = step.getBindDataAsdt(dic, Grid3.SortField, Grid3.SortDirection);
                TotalPage3.Text = dt2.Rows.Count.ToString();

                Grid3.DataSource = dt;
                Grid3.DataBind();

            }
            else
            {
                TotalPage3.Text = "1";
            }
        }
        protected void mainGrid_Sort(object sender, GridSortEventArgs e)
        {
            setPageContent(1);
        }
        protected void mainGrid_Sort1(object sender, GridSortEventArgs e)
        {
            
        }
        protected void mainGrid_RowDoubleClick(Object sender, GridRowClickEventArgs e)
        {
            //displaydetailinfo(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }
        protected void mainGrid_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent2(1);
        }
        protected void grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent3(1);
        }
        protected void mainGrid_RowDoubleClick1(Object sender, GridRowClickEventArgs e)
        {
            //displaydetailinfo(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }
        #region 分-0

        private void setPageInit()
        {
            GridPageSize.Text = "21";
            CurPage.Text = "";
            TotalPage.Text = "";
            MemoTxt.Text = "";
        }
        private void setPageInit1()
        {
            GridPageSize1.Text = "21";
            CurPage1.Text = "";
            TotalPage1.Text = "";
            MemoTxt1.Text = "";
        }
        private void setPageInit2()
        {
            GridPageSize2.Text = "21";
            CurPage2.Text = "";
            TotalPage2.Text = "";
            MemoTxt2.Text = "";
        }
        private void setPageInit3()
        {
            GridPageSize3.Text = "21";
            CurPage3.Text = "";
            TotalPage3.Text = "";
            MemoTxt3.Text = "";
        }
        private void setPageInit4()
        {
            GridPageSize4.Text = "21";
            CurPage4.Text = "";
            TotalPage4.Text = "";
            MemoTxt4.Text = "";
        }
        private void setPageInit5()
        {
            GridPageSize5.Text = "21";
            CurPage5.Text = "";
            TotalPage5.Text = "";
            MemoTxt5.Text = "";
        }
        private void setPageInit6()
        {
            GridPageSize6.Text = "21";
            CurPage6.Text = "";
            TotalPage6.Text = "";
            MemoTxt6.Text = "";
        }
        private void setPageInit7()
        {
            GridPageSize7.Text = "21";
            CurPage7.Text = "";
            TotalPage7.Text = "";
            MemoTxt7.Text = "";
        }
        private void setPageInit8()
        {
            GridPageSize8.Text = "21";
            CurPage8.Text = "";
            TotalPage8.Text = "";
            MemoTxt8.Text = "";
        }
        //private void setPageInit10()
        //{
        //    GridPageSize10.Text = "21";
        //    CurPage10.Text = "";
        //    TotalPage10.Text = "";
        //    MemoTxt10.Text = "";
        //}
        protected void setPageContent(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());

            if (intType == 1)
            {
                CurPage.Text = "1";
                BindGrid();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                MemoTxt.Text = "1/" + intTotalPage.ToString() + " ";
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
                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            " ";
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

                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            " ";
                        BindGrid();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                CurPage.Text = intTotalPage.ToString();
                MemoTxt.Text = "终";
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

                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            " ";
                        BindGrid();
                    }
                }
            }
        }
        
        protected void setPageContent2(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize2.Text.Trim());

            if (intType == 1)
            {
                CurPage2.Text = "1";
                BindGrid2();
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
                        BindGrid2();
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
                        BindGrid2();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage2.Text.Trim()) / intPagesize));
                CurPage2.Text = intTotalPage.ToString();
                MemoTxt2.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage2.Text.Trim() + " 条数据";
                BindGrid2();
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
                        BindGrid2();
                    }
                }
            }
        }
        protected void setPageContent3(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize3.Text.Trim());

            if (intType == 1)
            {
                CurPage3.Text = "1";
                BindGrid3();
                double intTotalPage3 = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage3.Text.Trim()) / intPagesize));
                MemoTxt3.Text = "1/" + intTotalPage3.ToString() + " ";
            }

            if (intType == 2)
            {
                int intCurPage3;

                if (int.TryParse(CurPage3.Text.Trim(), out intCurPage3))
                {
                    intCurPage3--;
                    if (intCurPage3 > 0)
                    {
                        CurPage3.Text = intCurPage3.ToString();
                        double intTotalPage3 = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage3.Text.Trim()) / intPagesize));
                        MemoTxt3.Text = intCurPage3.ToString() + "/" + intTotalPage3.ToString() +
                            " ";
                        BindGrid3();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage3;

                if (int.TryParse(CurPage3.Text.Trim(), out intCurPage3))
                {
                    intCurPage3++;
                    double intTotalPage3 = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage3.Text.Trim()) / intPagesize));
                    if (intCurPage3 < intTotalPage3 + 1)
                    {
                        CurPage3.Text = intCurPage3.ToString();

                        MemoTxt3.Text = intCurPage3.ToString() + "/" + intTotalPage3.ToString() +
                            " ";
                        BindGrid3();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage3 = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage3.Text.Trim()) / intPagesize));
                CurPage3.Text = intTotalPage3.ToString();
                MemoTxt3.Text = "终";
                BindGrid3();
            }

            if (intType == 5)
            {
                int intCurPage3;

                if (int.TryParse(CurPage3.Text.Trim(), out intCurPage3))
                {
                    double intTotalPage3 = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage3.Text.Trim()) / intPagesize));
                    if (intCurPage3 < intTotalPage3 + 1 && intCurPage3 > 0)
                    {
                        CurPage3.Text = intCurPage3.ToString();

                        MemoTxt3.Text = intCurPage3.ToString() + "/" + intTotalPage3.ToString() +
                            " ";
                        BindGrid3();
                    }
                }
            }
        }
        protected void FirstPage_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }
        protected void FirstPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(1);
        }
        protected void FirstPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(1);
        }
        protected void FirstPage_Click3(object sender, EventArgs e)
        {
            setPageContent3(1);
        }
        protected void FirstPage_Click4(object sender, EventArgs e)
        {
            setPageContent4(1);
        }
        protected void FirstPage_Click5(object sender, EventArgs e)
        {
            setPageContent5(1);
        }
        protected void FirstPage_Click6(object sender, EventArgs e)
        {
            setPageContent7(1);
        }
        protected void FirstPage_Click7(object sender, EventArgs e)
        {
            setPageContent8(1);
        }
        protected void FirstPage_Click8(object sender, EventArgs e)
        {
            setPageContent9(1);
        }
        //protected void FirstPage_Click10(object sender, EventArgs e)
        //{
        //    setPageContent10(1);
        //}
        protected void PrePage_Click(object sender, EventArgs e)
        {
            setPageContent(2);
        }
        protected void PrePage_Click1(object sender, EventArgs e)
        {
            setPageContent1(2);
        }
        protected void PrePage_Click2(object sender, EventArgs e)
        {
            setPageContent2(2);
        }
        protected void PrePage_Click3(object sender, EventArgs e)
        {
            setPageContent3(2);
        }
        protected void PrePage_Click4(object sender, EventArgs e)
        {
            setPageContent4(2);
        }
        protected void PrePage_Click5(object sender, EventArgs e)
        {
            setPageContent5(2);
        }
        protected void PrePage_Click6(object sender, EventArgs e)
        {
            setPageContent7(2);
        }
        protected void PrePage_Click7(object sender, EventArgs e)
        {
            setPageContent8(2);
        }
        protected void PrePage_Click8(object sender, EventArgs e)
        {
            setPageContent9(2);
        }
        //protected void PrePage_Click10(object sender, EventArgs e)
        //{
        //    setPageContent10(2);
        //}
        protected void NextPage_Click(object sender, EventArgs e)
        {
            setPageContent(3);
        }
        protected void NextPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(3);
        }
        protected void NextPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(3);
        }
        protected void NextPage_Click3(object sender, EventArgs e)
        {
            setPageContent3(3);
        }
        protected void NextPage_Click4(object sender, EventArgs e)
        {
            setPageContent4(3);
        }
        protected void NextPage_Click5(object sender, EventArgs e)
        {
            setPageContent5(3);
        }
        protected void NextPage_Click6(object sender, EventArgs e)
        {
            setPageContent7(3);
        }
        protected void NextPage_Click7(object sender, EventArgs e)
        {
            setPageContent8(3);
        }
        protected void NextPage_Click8(object sender, EventArgs e)
        {
            setPageContent9(3);
        }
        //protected void NextPage_Click10(object sender, EventArgs e)
        //{
        //    setPageContent10(3);
        //}
        protected void LastPage_Click(object sender, EventArgs e)
        {
            setPageContent(4);
        }
        protected void LastPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(4);
        }
        protected void LastPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(4);
        }
        protected void LastPage_Click3(object sender, EventArgs e)
        {
            setPageContent3(4);
        }
        protected void LastPage_Click4(object sender, EventArgs e)
        {
            setPageContent4(4);
        }
        protected void LastPage_Click5(object sender, EventArgs e)
        {
            setPageContent5(4);
        }
        protected void LastPage_Click6(object sender, EventArgs e)
        {
            setPageContent7(4);
        }
        protected void LastPage_Click7(object sender, EventArgs e)
        {
            setPageContent8(4);
        }
        protected void LastPage_Click8(object sender, EventArgs e)
        {
            setPageContent9(4);
        }
        //protected void LastPage_Click10(object sender, EventArgs e)
        //{
        //    setPageContent10(4);
        //}
        protected void GoPage_Click(object sender, EventArgs e)
        {
            setPageContent(5);
        }
        protected void GoPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(5);
        }
        protected void GoPage_Click2(object sender, EventArgs e)
        {
            setPageContent2(5);
        }
        protected void GoPage_Click3(object sender, EventArgs e)
        {
            setPageContent3(5);
        }
        protected void GoPage_Click4(object sender, EventArgs e)
        {
            setPageContent4(5);
        }
        protected void GoPage_Click5(object sender, EventArgs e)
        {
            setPageContent5(5);
        }
        protected void GoPage_Click6(object sender, EventArgs e)
        {
            setPageContent7(5);
        }
        protected void GoPage_Click7(object sender, EventArgs e)
        {
            setPageContent8(5);
        }
        protected void GoPage_Click8(object sender, EventArgs e)
        {
            setPageContent9(5);
        }
        //protected void GoPage_Click10(object sender, EventArgs e)
        //{
        //    setPageContent10(5);
        //}
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
        protected void SubNumber_Click1(object sender, EventArgs e)
        {
            //int intGridPageSize;

            //if (int.TryParse(GridPageSize1.Text.Trim(), out intGridPageSize))
            //{
            //    if (intGridPageSize > 6)
            //    {
            //        intGridPageSize--;
            //        GridPageSize1.Text = intGridPageSize.ToString();
            //    }
            //}
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
        protected void SubNumber_Click3(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize3.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize3.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void SubNumber_Click4(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize4.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize4.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void SubNumber_Click5(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize5.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize5.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void SubNumber_Click6(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize6.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize6.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void SubNumber_Click7(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize7.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize7.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void SubNumber_Click8(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize8.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize8.Text = intGridPageSize.ToString();
                }
            }
        }
        //protected void SubNumber_Click10(object sender, EventArgs e)
        //{
        //    int intGridPageSize;

        //    if (int.TryParse(GridPageSize10.Text.Trim(), out intGridPageSize))
        //    {
        //        if (intGridPageSize > 6)
        //        {
        //            intGridPageSize--;
        //            GridPageSize10.Text = intGridPageSize.ToString();
        //        }
        //    }
        //}
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
        protected void UpNumber_Click1(object sender, EventArgs e)
        {
            //int intGridPageSize;

            //if (int.TryParse(GridPageSize1.Text.Trim(), out intGridPageSize))
            //{
            //    if (intGridPageSize < 36)
            //    {
            //        intGridPageSize++;
            //        GridPageSize1.Text = intGridPageSize.ToString();
            //    }
            //}
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
        protected void UpNumber_Click3(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize3.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize3.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click4(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize4.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize4.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click5(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize5.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize5.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click6(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize6.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize6.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click7(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize7.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize7.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void UpNumber_Click8(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize8.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize8.Text = intGridPageSize.ToString();
                }
            }
        }
        //protected void UpNumber_Click10(object sender, EventArgs e)
        //{
        //    int intGridPageSize;

        //    if (int.TryParse(GridPageSize10.Text.Trim(), out intGridPageSize))
        //    {
        //        if (intGridPageSize < 36)
        //        {
        //            intGridPageSize++;
        //            GridPageSize10.Text = intGridPageSize.ToString();
        //        }
        //    }
        //}
        #endregion


        #endregion

        #region Tab-标准件库



        #endregion

        #region Tab-外购件库



        #endregion

        #region Tab-元组件库
        protected void Grid7_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent8(1);
        }
        protected void Grid8_RowClick(object sender, GridRowClickEventArgs e)
        {
            setPageContent9(1);
        }
        private void BindGridNode1()
        {
            cd_integration ig = new cd_integration();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("kitID", Tree5.SelectedNode.NodeID);

            int intPageindex = Convert.ToInt32(CurPage6.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize6.Text.Trim());
            string strSort = Grid7.SortField;
            string strSortDirection = Grid7.SortDirection;

            DataTable dt = ig.getBindDataAsdtNode(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ig.getBindDataAsdtNode(dic, strSort, strSortDirection);
            TotalPage6.Text = dt1.Rows.Count.ToString();

            Grid7.DataSource = dt;
            Grid7.DataBind();
        }
        private void BindGrid7()
        {
            cd_integration_batch bh = new cd_integration_batch();

            if (Grid7.SelectedRowIndexArray.Length > 0)
            {

                string strid = Grid7.DataKeys[Grid7.SelectedRowIndexArray[0]][0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("iid", strid);

                int intPageindex1 = Convert.ToInt32(CurPage7.Text.Trim());
                int intPagesize1 = Convert.ToInt32(GridPageSize7.Text.Trim());
                string strSort1 = "systemdate";//Grid2.SortField;
                string strSortDirection1 = Grid8.SortDirection;

                DataTable dt = bh.getBindDataAsdt(dic, strSort1, strSortDirection1, intPagesize1, intPageindex1);
                DataTable dt1 = bh.getBindDataAsdt(dic, strSort1, strSortDirection1);
                TotalPage7.Text = dt1.Rows.Count.ToString();

                Grid8.DataSource = dt;
                Grid8.DataBind();

            }
        }
        private void BindGrid8()
        {
            cd_integration_batch_step step = new cd_integration_batch_step();

            if (Grid8.SelectedRowIndexArray.Length > 0)
            {

                string strbatchid = Grid8.DataKeys[Grid8.SelectedRowIndexArray[0]][0].ToString();



                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("batchid", strbatchid);

                int intPageindex2 = Convert.ToInt32(CurPage8.Text.Trim());
                int intPagesize2 = Convert.ToInt32(GridPageSize8.Text.Trim());
                //string strSort2 = "systemdate";//Grid2.SortField;
                //string strSortDirection2 = Grid2.SortDirection;

                DataTable dt = step.getBindDataAsdt(dic, Grid9.SortField, Grid9.SortDirection, intPagesize2, intPageindex2);
                DataTable dt2 = step.getBindDataAsdt(dic, Grid9.SortField, Grid9.SortDirection);
                TotalPage8.Text = dt2.Rows.Count.ToString();

                Grid9.DataSource = dt;
                Grid9.DataBind();

            }
            else
            {
                TotalPage8.Text = "1";
            }
        }
        protected void setPageContent7(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize6.Text.Trim());

            if (intType == 1)
            {
                CurPage6.Text = "1";
                BindGridNode1();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage6.Text.Trim()) / intPagesize));
                MemoTxt6.Text = "1/" + intTotalPage.ToString() + "";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage6.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage6.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage6.Text.Trim()) / intPagesize));
                        MemoTxt6.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode1();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage6.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage6.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage6.Text = intCurPage.ToString();

                        MemoTxt6.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode1();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage6.Text.Trim()) / intPagesize));
                CurPage6.Text = intTotalPage.ToString();
                MemoTxt6.Text = "终/" + intTotalPage.ToString();
                BindGridNode1();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage6.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage6.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage6.Text = intCurPage.ToString();

                        MemoTxt6.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGridNode1();
                    }
                }
            }
        }
        protected void setPageContent8(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize7.Text.Trim());

            if (intType == 1)
            {
                CurPage7.Text = "1";
                BindGrid7();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage7.Text.Trim()) / intPagesize));
                MemoTxt7.Text = "1/" + intTotalPage.ToString() + "";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage7.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage7.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage7.Text.Trim()) / intPagesize));
                        MemoTxt7.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid7();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage7.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage7.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage7.Text = intCurPage.ToString();

                        MemoTxt7.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid7();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage7.Text.Trim()) / intPagesize));
                CurPage7.Text = intTotalPage.ToString();
                MemoTxt7.Text = "终/" + intTotalPage.ToString();
                BindGrid7();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage7.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage7.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage7.Text = intCurPage.ToString();

                        MemoTxt7.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid7();
                    }
                }
            }
        }
        protected void setPageContent9(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize8.Text.Trim());

            if (intType == 1)
            {
                CurPage8.Text = "1";
                BindGrid8();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage8.Text.Trim()) / intPagesize));
                MemoTxt8.Text = "1/" + intTotalPage.ToString() + "";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage8.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage8.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage8.Text.Trim()) / intPagesize));
                        MemoTxt8.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid8();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage8.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage8.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage8.Text = intCurPage.ToString();

                        MemoTxt8.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid8();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage8.Text.Trim()) / intPagesize));
                CurPage8.Text = intTotalPage.ToString();
                MemoTxt8.Text = "终页/" + intTotalPage.ToString() + "页";
                BindGrid8();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage8.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage8.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage8.Text = intCurPage.ToString();

                        MemoTxt8.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +
                            "";
                        BindGrid8();
                    }
                }
            }
        }
        #endregion
        #region Tab-总成统计
        //private void BindGrid10()
        //{
        //    pd_compongall com = new pd_compongall();
        //    Dictionary<string, string> dic = new Dictionary<string, string>();

        //    dic.Add("kitID", firsttree.SelectedNode.NodeID);

        //    int intPageindex = Convert.ToInt32(CurPage10.Text.Trim());
        //    int intPagesize = Convert.ToInt32(GridPageSize10.Text.Trim());
        //    string strSort = Grid10.SortField;
        //    string strSortDirection = Grid10.SortDirection;

        //    DataTable dt = com.getBindDataAsdtbyproduct(dic, strSort, strSortDirection, intPagesize, intPageindex);
        //    DataTable dt1 = com.getBindDataAsdtbyproduct(dic, strSort, strSortDirection);
        //    TotalPage10.Text = dt1.Rows.Count.ToString();

        //    Grid10.DataSource = dt;
        //    Grid10.DataBind();
        //}
        //protected void setPageContent10(int intType)
        //{
        //    int intPagesize = Convert.ToInt32(GridPageSize10.Text.Trim());

        //    if (intType == 1)
        //    {
        //        CurPage10.Text = "1";
        //        BindGrid10();
        //        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage10.Text.Trim()) / intPagesize));
        //        MemoTxt10.Text = "第 1 页 共 " + intTotalPage.ToString() + " 页 " + TotalPage10.Text.Trim() + " 条数据";
        //    }

        //    if (intType == 2)
        //    {
        //        int intCurPage;

        //        if (int.TryParse(CurPage10.Text.Trim(), out intCurPage))
        //        {
        //            intCurPage--;
        //            if (intCurPage > 0)
        //            {
        //                CurPage10.Text = intCurPage.ToString();
        //                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage10.Text.Trim()) / intPagesize));
        //                MemoTxt10.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
        //                    " 页 " + TotalPage10.Text.Trim() + " 条数据";
        //                BindGrid10();
        //            }
        //        }
        //    }

        //    if (intType == 3)
        //    {
        //        int intCurPage;

        //        if (int.TryParse(CurPage10.Text.Trim(), out intCurPage))
        //        {
        //            intCurPage++;
        //            double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage10.Text.Trim()) / intPagesize));
        //            if (intCurPage < intTotalPage + 1)
        //            {
        //                CurPage10.Text = intCurPage.ToString();

        //                MemoTxt10.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
        //                    " 页 " + TotalPage10.Text.Trim() + " 条数据";
        //                BindGrid10();
        //            }
        //        }
        //    }

        //    if (intType == 4)
        //    {
        //        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage10.Text.Trim()) / intPagesize));
        //        CurPage10.Text = intTotalPage.ToString();
        //        MemoTxt10.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage10.Text.Trim() + " 条数据";
        //        BindGrid10();
        //    }

        //    if (intType == 5)
        //    {
        //        int intCurPage;

        //        if (int.TryParse(CurPage10.Text.Trim(), out intCurPage))
        //        {
        //            double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage10.Text.Trim()) / intPagesize));
        //            if (intCurPage < intTotalPage + 1 && intCurPage > 0)
        //            {
        //                CurPage10.Text = intCurPage.ToString();

        //                MemoTxt10.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
        //                    " 页 " + TotalPage10.Text.Trim() + " 条数据";
        //                BindGrid10();
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
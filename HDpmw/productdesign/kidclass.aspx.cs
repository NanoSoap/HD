using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HDBusiness;
using FineUIPro;
using System.Data;
using YDCode;
using HDPages.productLib;

namespace HDpmw.productdesign
{
    public partial class kidclass : PageBase
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
        private void BindGrid()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mID", mainmpicweditID.Text.Trim());
            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            pd_kidclass_mainmpic ul = new pd_kidclass_mainmpic();
            DataTable dt = ul.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ul.getBindDataAsdt(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();
            mainGrid.DataSource = dt;
            mainGrid.DataBind();
        }
        private void BindGrid1()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("subID", secondrecWineditID.Text.Trim());
            int intPageindex = Convert.ToInt32(CurPage1.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize1.Text.Trim());
            string strSort = Grid1.SortField;
            string strSortDirection = Grid1.SortDirection;

            pd_kidclass_secondrec ul = new pd_kidclass_secondrec();
            DataTable dt=ul.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ul.getBindDataAsdt(dic, strSort, strSortDirection);
            TotalPage1.Text = dt1.Rows.Count.ToString();
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }
        #region 树初始化
        private void inittree()
        {
            // 模拟从数据库返回数据表
            pd_kidclass_main sm = new pd_kidclass_main();
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

                    node.Attributes.Add("nodemenu", row["nodemenu"].ToString().Trim());
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    
                        node.EnableClickEvent = true;
                    

                    firsttree.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }
            //DataTable ds = sm.getBindTreeDataAsdt();

            //foreach (DataRow row in ds.Rows)
            //{
            //    FineUIPro.TreeNode node = new FineUIPro.TreeNode();
            //    node.Text = row["mainname"].ToString();
            //    node.NodeID = row["ID"].ToString();
            //    node.EnableClickEvent = true;
            //    firsttree.Nodes.Add(node);
            //    ResolveSubTree(row, node);

            //}

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
                    node.Attributes.Add("isclick", row["isclick"].ToString().Trim());
                    
                        node.EnableClickEvent = true;
                    

                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }

        }
        
        #endregion

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
        protected void firsttree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            displaydetailinfo(e.Node.NodeID);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mainkid.Hidden = false;
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            editID.Text = treenode.NodeID;
            if(treenode.NodeID == "systemmenu")
            {
                mainmpic.Hidden = false;
                dart.Hidden = true;
            }
            else
            {
                mainmpic.Hidden = true;
                dart.Hidden = false;
                string strdartmpci = string.Empty;
                pd_kidclass_mainmpic ul = new pd_kidclass_mainmpic();
                DataTable dt = ul.getEditdata(treenode.NodeID);
                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow r in dt.Rows)
                    {
                        strdartmpci += r["mpci"] + ",";
                    }
                    strdartmpci = strdartmpci.Substring(0, strdartmpci.Length - 1);
                    dartmpci.DataSource = commonLib.stringTolist(strdartmpci, ','); ;
                    dartmpci.DataBind();
                }
                else
                {
                    dartmpci.DataSource = "" ;
                    dartmpci.DataBind();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            editwindow.Hidden = false;
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            editclassID.Text = treenode.NodeID;
            initinfo(treenode.NodeID);
        }
        protected void btnMpci_Click(object sender, EventArgs e)
        {
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            editclassID.Text = treenode.NodeID;
            pd_kidclass_main ul = new pd_kidclass_main();
            DataTable dt = ul.getEditdata(treenode.NodeID);
            if (dt != null && dt.Rows.Count > 0) {
                mainmpicWindow.Hidden = false;
                mainmpicweditID.Text = treenode.NodeID;
                setPageContent(1);
            }
            else
            {
                secondrecWin.Hidden = false;
                secondrecWineditID.Text = treenode.NodeID;
                setPageContent1(1);
            }
        }
        #endregion

        #region 主窗口菜单

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string strf_mname = f_mname.Text.Trim();
            string strf_mtypename = f_classname.Text.Trim();

            traversaltree(firsttree.Nodes[0], strf_mtypename, strf_mname);
            
        }
        private void displaydetailinfo(string NodeID)
        {
            pd_kidclass_main um = new pd_kidclass_main();
            System.Data.DataTable dt = um.getEditdata(NodeID);

            string strhtml = "空白详细信息";
            firsttree.UncheckAllNodes();

            if (dt != null && dt.Rows.Count > 0)
            {
                strhtml = "<div style=\"line-height:27px;\">";
                foreach (System.Data.DataRow r in dt.Rows)
                {
                    strhtml += "类别名：" + r["mainname"] + "<br/>序号：" + r["orderint"].ToString().Trim() + "<br/>编码："+ r["maincode"].ToString().Trim() + "<br/>创建时间：" +
                            r["adddate"].ToString().Trim() + "";
                    pd_kidclass_mainmpic mainpic = new pd_kidclass_mainmpic();
                    DataTable picdt = mainpic.getEditdata(r["ID"].ToString());
                    if (picdt != null && picdt.Rows.Count > 0)
                    {
                        strhtml += "<br/>主类别特性指标：";
                        foreach (DataRow r1 in picdt.Rows)
                        {
                            strhtml += r1["mpci"] + ",";
                        }
                    }
                }

                strhtml += "</div>";
            }
            else
            {
                pd_kidclass_secondary second = new pd_kidclass_secondary();
                System.Data.DataTable dt1 = second.getEditdata(NodeID);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    strhtml = "<div style=\"line-height:27px;\">";
                    foreach (System.Data.DataRow r in dt1.Rows)
                    {
                        strhtml += "类别名：" + r["classname"] + "<br/>序号：" + r["orderint"].ToString().Trim() + "<br/>编码：" + r["darycode"].ToString().Trim() + "<br/>创建时间：" +
                                r["adddate"].ToString().Trim() + "";
                        pd_kidclass_secondrec mainpic = new pd_kidclass_secondrec();
                        DataTable picdt = mainpic.getEditdata(r["ID"].ToString());
                        if (picdt != null && picdt.Rows.Count > 0)
                        {
                            strhtml += "<br/>特性指标：";
                            foreach (DataRow r1 in picdt.Rows)
                            {
                                strhtml += r1["mpci"] + ",";
                            }
                        }
                    }

                    strhtml += "</div>";
                }
            }
            detailinfo.Text = strhtml;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            FineUIPro.TreeNode treenode = firsttree.SelectedNode;
            editclassID.Text = treenode.NodeID;
            int intresult = 0;
            string strID = treenode.NodeID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");
            pd_kidclass_main ul = new pd_kidclass_main();
            DataTable dt = ul.getEditdata(strID);
            if (dt != null && dt.Rows.Count > 0)
            {
                intresult = ul.deletebycondition("pd_kidclass_main", dic);
                pd_kidclass_mainmpic ulpic = new pd_kidclass_mainmpic();
                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                dic1.Add("mainID", " cast(mainID as varchar(36))='" + strID + "' ");
                ulpic.deletebycondition("pd_kidclass_mainmpic", dic1);
                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "成功移除数据";
                    PageContext.RegisterStartupScript("preview();");
                }


                alert.Show();
            }
            else
            {
                if (ul.isExistdata("pd_machinekit", "mtypeid", strID, "mtypeid").Trim() != "")
                {
                    Alert.Show("该类别存在下级零件不能删除!");
                    return;
                }
                else { 
                    pd_kidclass_secondary ul1 = new pd_kidclass_secondary();
                    intresult = ul1.deletebycondition("pd_kidclass_secondary", dic);
                    pd_kidclass_secondrec ulpic = new pd_kidclass_secondrec();
                    Dictionary<string, string> dic1 = new Dictionary<string, string>();
                    dic1.Add("subID", " cast(subID as varchar(36))='" + strID + "' ");
                    ulpic.deletebycondition("pd_kidclass_secondrec", dic1);
                    if (intresult > 0)
                    {
                        alert.Icon = Icon.Information;
                        alert.Message = "成功移除数据";
                        PageContext.RegisterStartupScript("preview();");
                    }


                    alert.Show();
                }
            }
            

            if (intresult > 0)
            {
                alert.Icon = Icon.Information;
                alert.Message = "成功移除数据";
                PageContext.RegisterStartupScript("preview();");
            }
            

            alert.Show();
            
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
        #region 新增
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strID = editID.Text.ToString().Trim();

            int intresult = 0;

            if (strID == "systemmenu")
            {
                Dictionary<string, string> dic = initDatadicmain(mainname.Text.Trim(),orderint.Text.Trim(),code.Text.Trim());
                pd_kidclass_main ul = new pd_kidclass_main();
                dic.Add("ID", Guid.NewGuid().ToString());

                string strmainname = dic["mainname"].ToString().Trim();
                string strmaincode = dic["maincode"].ToString().Trim();
                if (ul.isExistdata("pd_kidclass_main", "mainname", strmainname, "mainname").Trim() != "")
                {
                    Alert.Show(strmainname + " 类别名称已经存在!");
                    return;
                }
                else if(ul.isExistdata("pd_kidclass_main", "maincode", strmaincode, "maincode").Trim() != "")
                {
                    Alert.Show(strmaincode + " 编码已经存在!");
                    return;
                }
                else
                {
                    intresult = ul.add(dic, "pd_kidclass_main");

                    string strmpic = mpci.Text.Trim();
                    if (strmpic != null && strmpic != "")
                    {
                        foreach (var str in strmpic.Split(','))
                        {
                            Dictionary<string, string> dic1 = initDatadicmainmpic(str, dic["ID"].ToString().Trim(),"");
                            dic1.Add("ID", Guid.NewGuid().ToString());
                            ul.add(dic1, "pd_kidclass_mainmpic");
                        }
                    }
                }

            }
            else
            {

                Dictionary<string, string> dic = initDatadicsecond(mainname.Text.Trim(), strID, orderint.Text.Trim(), code.Text.Trim());
                pd_kidclass_secondary ul = new pd_kidclass_secondary();
                dic.Add("ID", Guid.NewGuid().ToString());

                string strmainname = dic["classname"].ToString().Trim();
                string strdarycode = dic["darycode"].ToString().Trim();
                DataTable dt = ul.getEditdatabyID(strID, strmainname);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Alert.Show(strmainname + " 类别名称已经存在!");
                    return;
                }
                else if (ul.isExistdata("pd_kidclass_secondary", "darycode", strdarycode, "darycode").Trim() != "")
                {
                    Alert.Show(strdarycode + " 编码已经存在!");
                    return;
                }
                else
                {
                    intresult = ul.add(dic, "pd_kidclass_secondary");
                    if (dartmpci.SelectedValueArray.Length > 0)
                    {
                        string select = String.Join(", ", dartmpci.SelectedValueArray);
                        pd_kidclass_secondrec secrec = new pd_kidclass_secondrec();
                        pd_kidclass_mainmpic pic = new pd_kidclass_mainmpic();
                        foreach (var sel in select.Split(','))
                        {
                            string mpciID = pic.getCodedata(strID, sel.Trim());
                            Dictionary<string, string> dic1 = initDatadicsesecondrec(dic["ID"], mpciID);
                            dic1.Add("ID", Guid.NewGuid().ToString());
                            secrec.add(dic1, "pd_kidclass_secondrec");
                        }
                    }
                }
            }

            Alert alert = new Alert();

            if (intresult == 1)
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存成功";
                PageContext.RegisterStartupScript("preview();");
            }

            alert.Show();
            
        }
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            string strID = editclassID.Text.ToString().Trim();
            pd_kidclass_main ul = new pd_kidclass_main();
            int intresult = 0;
            if (ul.isExistdata("pd_kidclass_main", "ID", strID, "ID").Trim() != "")
            {
                DataTable dt = ul.getEditdata(strID, editmainname.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "名称已存在";
                    return;
                }
                else if (ul.isExistdata("pd_kidclass_main", "maincode", editcode.Text.Trim(), "maincode").Trim() != "")
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "编码已存在";
                    return;
                }
                else
                {
                    Dictionary<string, string> dic = initDatadicmain(editmainname.Text.Trim(), editorder.Text.Trim(), editcode.Text.Trim());
                    string strmainname = dic["mainname"].ToString().Trim();

                    intresult = ul.update(dic, "pd_kidclass_main", "ID", strID);
                }
            }
            else
            {
                pd_kidclass_secondary ul1 = new pd_kidclass_secondary();
                DataTable dt = ul1.getEditdatabyname(strID, editmainname.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "名称已存在";
                    return;
                }
                else if (ul.isExistdata("pd_kidclass_secondary", "darycode", editcode.Text.Trim(), "darycode").Trim() != "")
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "编码已存在";
                    return;
                }
                else
                {
                    Dictionary<string, string> dic = initDatadicsecondedit(editmainname.Text.Trim(), editorder.Text.Trim(), editcode.Text.Trim());
                    string strmainname = dic["classname"].ToString().Trim();

                    intresult = ul.update(dic, "pd_kidclass_secondary", "ID", strID);
                }
            }
            

            if (intresult == 1)
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存成功";
            }

            alert.Show();
        }
        protected void addmainmpic_Click(object sender, EventArgs e)
        {
            erase();
            addmainpicWin.Title = "添加主类别特性指标";
            addmainpicWin.Hidden = false;

        }
        protected void editmainmpic_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择编辑记录");

                return;
            }
            initinfo1(mainGrid.DataKeys[intRowindexarray[0]][0].ToString().Trim());
            addmainpicWin.Title = "修改主类别特性指标";
            addmainpicWin.Hidden = false;
        }
        protected void deletemainmpic_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                pd_kidclass_mainmpic ul = new pd_kidclass_mainmpic();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                if (ul.isExistdata("pd_kidclass_secondrec", "mpciID", strID, "mpciID").Trim() != "")
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "该指标给下级分类使用不能删除";
                    alert.Show();
                }
                else
                {
                    
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                    int intresult = ul.deletebycondition("pd_kidclass_mainmpic", dic);

                    setPageContent(5);



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
        }
        protected void addsecondrec_Click(object sender, EventArgs e)
        {
            
            addsecondrecWin.Title = "添加分类别特性指标";
            addsecondrecWin.Hidden = false;
            string str1 = secondrecWineditID.Text;
            pd_kidclass_secondary ul1 = new pd_kidclass_secondary();
            DataTable dt1 = ul1.getEditdata(str1);
            DataRow r1 = dt1.Rows[0];
            string mainID = r1["mainID"].ToString();
            textSavesecondrec.Text = mainID;
            DataTable dt = ul1.getEditdata(mainID, str1);
            string strdartmpci = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (System.Data.DataRow r in dt.Rows)
                {
                    strdartmpci += r["mpci"] + ",";
                }
                strdartmpci = strdartmpci.Substring(0, strdartmpci.Length - 1);
                secondrecmpci.DataSource = commonLib.stringTolist(strdartmpci, ','); ;
                secondrecmpci.DataBind();
            }
            else
            {
                secondrecmpci.DataSource = "";
                secondrecmpci.DataBind();
            }
        }
        protected void deletesecondrec_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = Grid1.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                pd_kidclass_secondrec ul = new pd_kidclass_secondrec();
                object[] keys = Grid1.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                if (ul.isExistdata("pd_machinekit_feature", "featureid", strID, "featureid").Trim() != "") {
                    Alert.Show("该特性已被零件使用不能删除！");
                    return;
                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                    int intresult = ul.deletebycondition("pd_kidclass_secondrec", dic);

                    setPageContent1(5);

                    Alert alert = new Alert();

                    if (intresult > 0)
                    {
                        alert.Icon = Icon.Information;
                        alert.Message = "成功移除数据";
                    }

                    alert.Show();
                }
            }
        }
        protected void btnSavemainpic_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = initDatadicmainmpic(mpciname.Text.Trim(), mainmpicweditID.Text.Trim(), explain.Text.Trim());
            string strID = textSavemainpic.Text.ToString().Trim();
            pd_kidclass_mainmpic ul = new pd_kidclass_mainmpic();
            int intresult = 0;
            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                string strusername = dic["mpci"].ToString().Trim();
                if (ul.getCodedata(mainmpicweditID.Text.Trim(), mpciname.Text.Trim()).Trim() != "")
                {
                    Alert.Show(strusername + " 特性指标已经存在!");
                    return;
                }
                else
                {
                    intresult = ul.add(dic, "pd_kidclass_mainmpic");
                }
            }
            else
            {
                intresult = ul.update(dic, "pd_kidclass_mainmpic", "ID", strID);
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
        protected void btnSavesecondrec_Click(object sender, EventArgs e)
        {
            string strID = textSavesecondrec.Text.Trim();
            string str1 = secondrecWineditID.Text;
            int intresult = 0;
            if (secondrecmpci.SelectedValueArray.Length > 0)
            {
                string select = String.Join(", ", secondrecmpci.SelectedValueArray);
                pd_kidclass_secondrec secrec = new pd_kidclass_secondrec();
                pd_kidclass_mainmpic pic = new pd_kidclass_mainmpic();
                foreach (var sel in select.Split(','))
                {
                    string mpciID = pic.getCodedata(strID, sel.Trim());
                    Dictionary<string, string> dic1 = initDatadicsesecondrec(str1, mpciID);
                    dic1.Add("ID", Guid.NewGuid().ToString());
                    secrec.add(dic1, "pd_kidclass_secondrec");
                }
                intresult = 1;
            }
            if (CurPage1.Text.Trim() == "")
            {
                setPageContent1(1);
            }
            else
            {
                setPageContent1(5);
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
        private Dictionary<string, string> initDatadicmainmpic(string str,string mID,string explain)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mainID", mID);
            dic.Add("mpci", str);
            dic.Add("explain", explain);
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", System.DateTime.Now.ToString());
            return dic;
        }
        private Dictionary<string, string> initDatadicmain(string name,string order,string code)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("mainname", name);
            dic.Add("orderint", order);
            dic.Add("maincode", code);
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", System.DateTime.Now.ToString());
            return dic;
        }
        private Dictionary<string, string> initDatadicsecond(string name,string id,string order,string code)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("classname", name);
            dic.Add("mainID", id);
            dic.Add("orderint", order);
            dic.Add("darycode", code);
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", System.DateTime.Now.ToString());
            return dic;
        }
        private Dictionary<string, string> initDatadicsecondedit(string name, string order, string code)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("classname", name);
            dic.Add("orderint", order);
            dic.Add("darycode", code);
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", System.DateTime.Now.ToString());
            return dic;
        }
        private Dictionary<string, string> initDatadicsesecondrec(string subID, string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("subID", subID);
            dic.Add("mpciID", id);
            dic.Add("isdelid", "1");
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", System.DateTime.Now.ToString());
            return dic;
        }
        private void erase()
        {
            textSavemainpic.Text = "";
            mpciname.Text = "";
            explain.Text = "";
        }
        private void initinfo(string strID)
        {
            pd_kidclass_main ul = new pd_kidclass_main();
            System.Data.DataTable dt = ul.getEditdata(strID);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];

                editID.Text = strID;
                editmainname.Text = r["mainname"].ToString().Trim();
                editorder.Text = r["orderint"].ToString().Trim();
                editcode.Text = r["maincode"].ToString().Trim();
            }
            else
            {
                pd_kidclass_secondary ul1 = new pd_kidclass_secondary();
                DataTable dt1 = ul1.getEditdata(strID);
                DataRow r = dt1.Rows[0];

                editID.Text = strID;
                editmainname.Text = r["classname"].ToString().Trim();
                editorder.Text = r["orderint"].ToString().Trim();
                editcode.Text = r["darycode"].ToString().Trim();
            }
        }
        private void initinfo1(string strID)
        {
            pd_kidclass_mainmpic ul = new pd_kidclass_mainmpic();
            System.Data.DataTable dt = ul.getEditdatabyID(strID);

            DataRow r = dt.Rows[0];

            textSavemainpic.Text = strID;
            mpciname.Text = r["mpci"].ToString().Trim();
            explain.Text = r["explain"].ToString().Trim();
        }
        protected void mainGrid_Sort(object sender, GridSortEventArgs e)
        {
            setPageContent(1);
        }
        protected void mainGrid_Sort1(object sender, GridSortEventArgs e)
        {
            setPageContent1(1);
        }
        #endregion
        #region 分页-0

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
        protected void setPageContent(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());

            if (intType == 1)
            {
                CurPage.Text = "1";
                BindGrid();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                //MemoTxt.Text = "1/" + intTotalPage.ToString() + "";
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
                        //MemoTxt.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +"";
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

                        //MemoTxt.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +"";
                        BindGrid();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                CurPage.Text = intTotalPage.ToString();
                //MemoTxt.Text = "" + intTotalPage.ToString() + "";
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

                        //MemoTxt.Text = "" + intCurPage.ToString() + "/" + intTotalPage.ToString() +"";
                        BindGrid();
                    }
                }
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
        protected void FirstPage_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }
        protected void FirstPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(1);
        }
        protected void PrePage_Click(object sender, EventArgs e)
        {
            setPageContent(2);
        }
        protected void PrePage_Click1(object sender, EventArgs e)
        {
            setPageContent1(2);
        }
        protected void NextPage_Click(object sender, EventArgs e)
        {
            setPageContent(3); ;
        }
        protected void NextPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(3); ;
        }
        protected void LastPage_Click(object sender, EventArgs e)
        {
            setPageContent(4); ;
        }
        protected void LastPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(4); ;
        }
        protected void GoPage_Click(object sender, EventArgs e)
        {
            setPageContent(5); ;
        }
        protected void GoPage_Click1(object sender, EventArgs e)
        {
            setPageContent1(5); ;
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
    }
}
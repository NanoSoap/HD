using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HDBusiness;
using YDCode;
using FineUIPro;

namespace HDpmw.systemInfo
{
    public partial class userRegister:PageBase
    {  
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                initpage();
            }
        }

        private void initpage()
        {
            setPageInit();
            initdropSex();
            erase();
        }

        private void initdropSex()
        {
            xparams x = new xparams();

            string str = x.getparamData("H002");
            string strDept = x.getparamData("H012");
            sex.DataSource = commonLib.stringTolist(str,',');
            sex.DataBind();
            department.DataSource = commonLib.stringTolist(strDept, ',');
            department.DataBind();
        }

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

        #region 主窗口菜单

        protected void btnFind_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            erase();
            neweditWindow.Title = "用户注册新增界面";
            neweditWindow.Hidden = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择编辑记录");

                return;
            }

            initinfo(mainGrid.DataKeys[intRowindexarray[0]][0].ToString().Trim());
            neweditWindow.Title = "用户注册编辑界面";
            neweditWindow.Hidden = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                userLogin ul = new userLogin();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = ul.deletebycondition("userLogin", dic);

                setPageContent(1);

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
        }

        #endregion

        #region 新增/neweditWindow窗口

        protected void txt_nameBlur(object sender,EventArgs e)
        {
            username.Text = ((FineUIPro.TextBox)sender).Text.ToString().Trim();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = initDatadic();
            string strID = editID.Text.ToString().Trim();
            userLogin ul = new userLogin();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                string strusername = dic["username"].ToString().Trim();
                if (ul.isExistdata("userLogin", "username", strusername, "username").Trim() != "")
                {
                    Alert.Show(strusername+" 用户账号已经存在!");
                }
                else
                {
                    intresult = ul.add(dic, "userLogin");
                }
            }
            else
            {
                intresult = ul.update(dic, "userLogin", "ID", strID);
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

        private void erase()
        {
            editID.Text = "";
            fullname.Text = "";
            username.Text = "";
            password.Text = "";
            sex.SelectedIndex = 0;
            birdate.SelectedDate =System.DateTime.Now;
            telephone.Text = "";
            address.Text = "";
            department.SelectedIndex = 0;
            staffno.Text = "";
        }

        private void initinfo(string strID)
        {
            userLogin ul= new userLogin();
            System.Data.DataTable dt = ul.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            fullname.Text = r["fullname"].ToString().Trim();
            username.Text = r["username"].ToString().Trim();
            password.Text = r["password"].ToString().Trim();
            sex.SelectedValue = r["sex"].ToString().Trim();
            department.SelectedValue = r["department"].ToString().Trim();
            staffno.Text = r["staffno"].ToString().Trim();
            DateTime seldate;
            if (System.DateTime.TryParse(r["birdate"].ToString().Trim(), out seldate))
            {
                birdate.SelectedDate = seldate;
            }

            telephone.Text = r["telephone"].ToString().Trim();
            address.Text = r["address"].ToString().Trim();
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("fullname", fullname.Text.Trim());
            dic.Add("username", username.Text.Trim());
            dic.Add("password", password.Text.Trim());
            dic.Add("sex", sex.SelectedValue.Trim());
            dic.Add("staffno", staffno.Text.Trim());
            DateTime seldate;
            if (birdate.Text.Trim() != "" && System.DateTime.TryParse(birdate.Text.Trim(), out seldate))
            {
                dic.Add("birdate", birdate.Text.Trim());
            }

            dic.Add("telephone", telephone.Text.Trim());
            dic.Add("address", address.Text.Trim());
            dic.Add("regperson",SessionUserName.Trim());
            dic.Add("regdate",System.DateTime.Now.ToString());
            dic.Add("department", department.SelectedValue);
            return dic;
        }

        #endregion

        #region 表格处理

        protected void mainGrid_RowDoubleClick(Object sender, GridRowClickEventArgs e)
        {
            displaydetailinfo(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }

        private void displaydetailinfo(string strID)
        {
            userLogin ul = new userLogin();
            System.Data.DataTable dt = ul.getEditdata(strID);

            string strhtml = "空白详细信息";

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Data.DataRow r = dt.Rows[0];
                strhtml = "<div style=\"line-height:27px;\">" +
                    "姓名：" + r["fullname"].ToString().Trim() + "<br/>" +
                    "部门：" + r["department"].ToString().Trim() + "<br/>" +
                    "工号：" + r["staffno"].ToString().Trim() + "<br/>" +
                    "账号：" + r["username"].ToString().Trim() + "<br/>" +
                    "密码：" + r["password"].ToString().Trim() + "<br/>" +
                    "性别：" + r["sex"].ToString().Trim() + "<br/>" +
                    "生日：" + r["birdate"].ToString().Trim() + "<br/>" +
                    "电话：" + r["telephone"].ToString().Trim() + "<br/>" +
                    "<div style=\"word-break:break-all;\">地址：" + r["address"].ToString().Trim() + "</div>" +
                    "注册人：" + r["regperson"].ToString().Trim() + "<br/>" +
                    "日期：" + r["regdate"].ToString().Trim() + "<br/>" +
                    "ID：" + r["ID"].ToString().Trim() + "<br/>" +
                    "</div>";
            }

            detailinfo.Text = strhtml;
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
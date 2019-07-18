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
    public partial class Xparams:PageBase
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
            erase();
        }

        private void BindGrid()
        {
            string strf_paramname = f_paramname.Text.Trim();
            string strf_paramvalue = f_paramvalue.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("paramname", strf_paramname);
            dic.Add("paramvalue", strf_paramvalue);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            xparams x = new xparams();
            DataTable dt = x.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = x.getBindDataAsdt(dic, strSort, strSortDirection);
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
            paramcode.Text=newCode().Trim();
            neweditWindow.Title = "参数配置新增界面";
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
            neweditWindow.Title = "参数配置编辑界面";
            neweditWindow.Hidden = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                xparams x = new xparams();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = x.deletebycondition("xparams", dic);

                setPageContent(5);

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = initDatadic();
            string strID = editID.Text.ToString().Trim();
            xparams x = new xparams();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = x.add(dic, "xparams");
            }
            else
            {
                intresult = x.update(dic, "xparams","ID", strID);
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
                //连续新增更新代码
                paramcode.Text = newCode().Trim();
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
                paramcode.Text = newCode().Trim();
            }
            else
            {
                initinfo(strID);
            }
        }

        private void erase()
        {
            editID.Text = "";
            paramcode.Text = "系统生成参数代码";
            paramname.Text = "";
            paramvalue.Text = "";
        }

        private void initinfo(string strID)
        {
            xparams x = new xparams();
            System.Data.DataTable dt = x.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            paramcode.Text =r["paramcode"].ToString().Trim();
            paramname.Text = r["paramname"].ToString().Trim(); ;
            paramvalue.Text = r["paramvalue"].ToString().Trim(); ;
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("paramcode", paramcode.Text.Trim());
            dic.Add("paramname", paramname.Text.Trim());
            dic.Add("paramvalue", paramvalue.Text.Trim());

            return dic;
        }

        private string newCode()
        {
            xparams x = new xparams();
            string strparamcode = x.getCodedata();

            if (strparamcode.Length == 1){
                strparamcode = "H00" + strparamcode;
            }
            else if (strparamcode.Length == 2) {            
                strparamcode =  "H0" + strparamcode;
            }
            else{            
                strparamcode = "H"+strparamcode;
            }

            return strparamcode;
        }

        #endregion

        #region 表格处理

        protected void mainGrid_RowDoubleClick(Object sender,GridRowClickEventArgs e)
        {
            displaydetailinfo(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }

        private void displaydetailinfo(string strID)
        {
            xparams x = new xparams();
            System.Data.DataTable dt = x.getEditdata(strID);

            string strhtml = "空白详细信息";

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Data.DataRow r = dt.Rows[0];
                strhtml = "<div style=\"line-height:27px;\">" +
                    "参数名：" + r["paramname"].ToString().Trim() + "<br/>" +
                     "代码：" + r["paramcode"].ToString().Trim() + "<br/>" +
                    "<div style=\"word-break:break-all;\">参数值：" + r["paramvalue"].ToString().Trim() + "</div>" +
                    "ID：" + r["ID"].ToString().Trim() + "<br/>" +
                    "</div>";
            }

            detailinfo.Text =strhtml;
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
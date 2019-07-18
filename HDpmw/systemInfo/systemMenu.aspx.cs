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
    public partial class SystemMenu : PageBase
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
            erasechild();
        }

        private void BindGrid()
        {
            string strf_menuname = f_menuname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("menuname", strf_menuname);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            systemMenu sm = new systemMenu();
            DataTable dt = sm.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = sm.getBindDataAsdt(dic, strSort, strSortDirection);
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
            neweditWindow.Title = "系统新增界面";
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
            neweditWindow.Title = "系统菜单编辑界面";
            neweditWindow.Hidden = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                systemMenu sm = new systemMenu();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<string> listTablename = new List<string>();

                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                dic = new Dictionary<string, string>();
                dic.Add("pid", " cast(pid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);

                listTablename.Add("systemMenu");
                listTablename.Add("systemMenu_button");
                int intresult = sm.deleteMutri(listdic, listTablename);

                setPageContent(5);
                childGrid.DataSource = null;
                childGrid.DataBind();
                erasechild();

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

        #region 主窗口子菜单

        protected void btnNewChild_Click(object sender, EventArgs e)
        {
            if(editID.Text.Trim()=="")
            {
                Alert.Show("请选择系统菜单");
                return;
            }

            erasechild();
            neweditchildWindow.Title = "系统新增功能按钮界面";
            neweditchildWindow.Hidden = false;
        }

        protected void btnEditChild_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = childGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择编辑记录");

                return;
            }

            initchildinfo(childGrid.DataKeys[intRowindexarray[0]][0].ToString().Trim());
            neweditchildWindow.Title = "系统菜单编辑界面";
            neweditchildWindow.Hidden = false;
        }

        protected void btnDeleteChild_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = childGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                systemMenu_button smb = new systemMenu_button();
                object[] keys = childGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                string strpid = keys[1].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = smb.deletebycondition("systemMenu_button", dic);

                BindchildGrid(strpid);
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
            systemMenu sm = new systemMenu();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = sm.add(dic, "systemMenu");
            }
            else
            {
                intresult = sm.update(dic, "systemMenu", "ID", strID);
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
            menuparent.Text = "";
            menuname.Text = "";
            nodeid.Text = "";
            sortcode.Text = "";
        }

        private void initinfo(string strID)
        {
            systemMenu sm= new systemMenu();
            System.Data.DataTable dt = sm.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            menuparent.Text = r["menuparent"].ToString().Trim();
            menuname.Text = r["menuname"].ToString().Trim();
            nodeid.Text = r["nodeid"].ToString().Trim();
            sortcode.Text = r["sortcode"].ToString().Trim();
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("menuparent", menuparent.Text.Trim());
            dic.Add("menuname", menuname.Text.Trim());
            dic.Add("nodeid", nodeid.Text.Trim());
            dic.Add("sortcode", sortcode.Text.Trim());

            return dic;
        }

        #endregion

        #region 新增/neweditchildWindow

        protected void btnSaveChild_Click(object sender, EventArgs e)
        {           
            string strID = editchildID.Text.ToString().Trim();
            string strpid = editID.Text.ToString().Trim();
            Dictionary<string, string> dic = initchildDatadic(strpid);

            systemMenu_button smb = new systemMenu_button();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = smb.add(dic, "systemMenu_button");
            }
            else
            {
                intresult = smb.update(dic, "systemMenu_button", "ID", strID);
            }

            BindchildGrid(strpid);

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

        protected void btnRefreshChild_Click(object sender, EventArgs e)
        {
            string strID = editchildID.Text.Trim();

            if (strID == "")
            {
                erasechild();
            }
            else
            {
                initchildinfo(strID);
            }
        }

        private void erasechild()
        {
            editchildID.Text = "";
            functionname.Text = "";
            buttonid.Text = "";
        }

        private void initchildinfo(string strID)
        {
            systemMenu_button smb = new systemMenu_button();
            System.Data.DataTable dt = smb.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editchildID.Text = strID;
            functionname.Text = r["functionname"].ToString().Trim();
            buttonid.Text = r["buttonid"].ToString().Trim();
        }

        private Dictionary<string, string> initchildDatadic(string strpid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pid", strpid.Trim());
            dic.Add("functionname", functionname.Text.Trim());
            dic.Add("buttonid", buttonid.Text.Trim());

            return dic;
        }

        #endregion

        #region 表格处理

        protected void mainGrid_RowClick(Object sender, GridRowClickEventArgs e)
        {
            BindchildGrid(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }

        private void BindchildGrid(string strpid)
        {
            systemMenu_button smb = new systemMenu_button();
            System.Data.DataTable dt = smb.getBindDataAsdt(strpid);

            editID.Text = strpid;
            childGrid.DataSource = dt;
            childGrid.DataBind();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HDPages.baseInfoLib;
using FineUIPro;
using HDBusiness;
using YDCode;
using System.Data;

namespace HDpmw.baseInfo
{
    public partial class heatdevice : PageBase
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
            initunit();
        }

        private void initunit()
        {
            xparams x = new xparams();

            string str = x.getparamData("H021");

            hdstatu.DataSource = commonLib.stringTolist(str, ',');
            hdstatu.DataBind();

            hdstatu.SelectedIndex = 0;
        }

        private void BindGrid()
        {
            string strf_hdname = f_hdname.Text.Trim();
            string strf_hdtype = f_hdtype.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("hdname", strf_hdname);
            dic.Add("hdtype", strf_hdtype);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            bi_heatdevice bhd = new bi_heatdevice();
            DataTable dt = bhd.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage.Text = bhd.gettotalpage(dic);

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
            neweditWindow.Title = "设备信息新增界面";
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
            neweditWindow.Title = "设备信息编辑界面";
            neweditWindow.Hidden = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                bi_heatdevice bhd = new bi_heatdevice();
                object[] keys = mainGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                List<Dictionary<string, string>> listdic = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<string> listTablename = new List<string>();

                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");
                listdic.Add(dic);
                dic = new Dictionary<string, string>();
                dic.Add("hdid", " cast(hdid as varchar(36))='" + strID + "' ");
                listdic.Add(dic);

                listTablename.Add("bi_heatdevice");
                listTablename.Add("bi_heatdevice_repair");
                int intresult = bhd.deleteMutri(listdic, listTablename); 

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
            if (editID.Text.Trim() == "")
            {
                Alert.Show("请选择设备");
                return;
            }

            erasechild();
            neweditchildWindow.Title = "设备维修信息新增界面";
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
            neweditchildWindow.Title = "设备维修信息编辑界面";
            neweditchildWindow.Hidden = false;
        }

        protected void btnDeleteChild_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = childGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                bi_heatdevice_repair bhdr = new bi_heatdevice_repair();
                object[] keys = childGrid.DataKeys[intRowindexarray[0]];

                string strID = null;
                if (keys[0] != null)
                {
                    strID = keys[0].ToString();
                }
                string strhdid = null;
                if (keys[1] != null)
                {
                    strhdid = keys[1].ToString();
                }
                //string strID = keys[0].ToString();
                //string strhdid = keys[1].ToString();出现了key[]=null的异常

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = 0;
                intresult = bhdr.deletebycondition("bi_heatdevice_repair", dic);

                strhdid = editID.Text.ToString().Trim();
                BindchildGrid(strhdid);

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
            bi_heatdevice bhd = new bi_heatdevice();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = bhd.add(dic, "bi_heatdevice");
            }
            else
            {
                intresult = bhd.update(dic, "bi_heatdevice", "ID", strID);
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
            hdname.Text = "";
            hdcode.Text = getconcode();
            hdtype.Text = "";
            hdmodel.Text = "";
            hdstatu.SelectedIndex = 0;
        }

        private void initinfo(string strID)
        {
            bi_heatdevice bhd = new bi_heatdevice();
            System.Data.DataTable dt = bhd.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            hdname.Text = r["hdname"].ToString().Trim();
            hdcode.Text = r["hdcode"].ToString().Trim();
            hdtype.Text = r["hdtype"].ToString().Trim();
            hdmodel.Text = r["hdmodel"].ToString().Trim();
            hdstatu.SelectedValue = r["hdstatu"].ToString().Trim();
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("hdname", hdname.Text.Trim());
            dic.Add("hdcode", hdcode.Text.Trim());
            dic.Add("hdtype", hdtype.Text.Trim());
            dic.Add("hdmodel", hdmodel.Text.Trim());
            dic.Add("hdstatu", hdstatu.SelectedValue.Trim());
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", DateTime.Now.ToString());
            dic.Add("isdelid", "1");

            return dic;
        }

        private string getconcode()
        {
            bi_heatdevice bhd = new bi_heatdevice();

            string strpdcode = bhd.getmaxconcode();
            string strprecode = "h";

            if (strpdcode.Length == 1)
            {
                strpdcode = strprecode + "00" + strpdcode;
            }
            else if (strpdcode.Length == 2)
            {
                strpdcode = strprecode + "0" + strpdcode;
            }       
            else
            {
                strpdcode = strprecode + strpdcode;
            }

            return strpdcode;
        }

        #endregion

        #region 新增/neweditchildWindow

        protected void btnSaveChild_Click(object sender, EventArgs e)
        {
            string strID = editchildID.Text.ToString().Trim();
            string strhdid = editID.Text.ToString().Trim();
            Dictionary<string, string> dic = initchildDatadic(strhdid);

            bi_heatdevice_repair bhdr = new bi_heatdevice_repair();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = bhdr.add(dic, "bi_heatdevice_repair");
            }
            else
            {
                intresult = bhdr.update(dic, "bi_heatdevice_repair", "ID", strID);
            }

            BindchildGrid(strhdid);

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
            hdrepairtime.SelectedDate = null;
            hdreworktime.SelectedDate = null;
            hderror.Text = "";
            hdrepaircontent.Text = "";           
        }

        private void initchildinfo(string strID)
        {
            bi_heatdevice_repair bhdr = new bi_heatdevice_repair();
            System.Data.DataTable dt = bhdr.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editchildID.Text = strID;
            hdrepairtime.SelectedDate = DateTime.Parse(r["hdrepairtime"].ToString().Trim());
            hdreworktime.SelectedDate = DateTime.Parse(r["hdreworktime"].ToString().Trim());
            hderror.Text = r["hderror"].ToString().Trim();
            hdrepaircontent.Text = r["hdrepaircontent"].ToString().Trim();
        }

        private Dictionary<string, string> initchildDatadic(string strhdid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("hdid", strhdid.Trim());
            dic.Add("hdrepairtime", hdrepairtime.SelectedDate.ToString());
            dic.Add("hdreworktime", hdreworktime.SelectedDate.ToString());
            dic.Add("hderror", hderror.Text.Trim());
            dic.Add("hdrepaircontent", hdrepaircontent.Text.Trim());
            dic.Add("operater", SessionUserName.Trim());
            dic.Add("systemdate", DateTime.Now.ToString());
            dic.Add("isdelid", "1");

            return dic;
        }

        #endregion

        #region 表格处理

        protected void mainGrid_RowClick(Object sender, GridRowClickEventArgs e)
        {
            BindchildGrid(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }

        private void BindchildGrid(string strhdid)
        {
            bi_heatdevice_repair bhdr = new bi_heatdevice_repair();
            System.Data.DataTable dt = bhdr.getBindDataAsdt(strhdid);

            editID.Text = strhdid;
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
                MemoTxt.Text = "1/" + intTotalPage.ToString();
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
                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
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

                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
                        BindGrid();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                CurPage.Text = intTotalPage.ToString();
                MemoTxt.Text = intTotalPage.ToString();
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

                        MemoTxt.Text = intCurPage.ToString() + "/" + intTotalPage.ToString();
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
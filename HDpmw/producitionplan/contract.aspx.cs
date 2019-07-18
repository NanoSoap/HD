using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HDPages.planningLib;
using FineUIPro;
using HDBusiness;
using YDCode;

namespace HDpmw.producitionplan
{
    public partial class contract :PageBase
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

            string str = x.getparamData("H003");

            conpduint.DataSource = commonLib.stringTolist(str, ',');
            conpduint.DataBind();

            conpduint.SelectedIndex = 3;
        }

        private void BindGrid()
        {
            string strf_partyA = f_partyA.Text.Trim();
            string strf_conname = f_conname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("partyA", strf_partyA);
            dic.Add("conname", strf_conname);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = mainGrid.SortField;
            string strSortDirection = mainGrid.SortDirection;

            pp_contract pc = new pp_contract();
            DataTable dt = pc.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            TotalPage.Text = pc.gettotalpage(dic);

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
            neweditWindow.Title = "技术合同新增界面";
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
            neweditWindow.Title = "技术合同编辑界面";
            neweditWindow.Hidden = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = mainGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                pp_contract pc= new pp_contract();
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

                listTablename.Add("pp_contract");
                listTablename.Add("pp_contract_content");
                int intresult = pc.deleteMutri(listdic, listTablename);

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
                Alert.Show("请选择技术合同");
                return;
            }

            erasechild();
            neweditchildWindow.Title = "技术合同清单新增界面";
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
            neweditchildWindow.Title = "技术合同清单编辑界面";
            neweditchildWindow.Hidden = false;
        }

        protected void btnDeleteChild_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = childGrid.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                pp_contract_content pcc = new pp_contract_content();
                object[] keys = childGrid.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();
                string strpid = keys[1].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ID", " cast(ID as varchar(36))='" + strID + "' ");

                int intresult = pcc.deletebycondition("pp_contract_content", dic);

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
            pp_contract pc= new pp_contract();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = pc.add(dic, "pp_contract");
            }
            else
            {
                intresult = pc.update(dic, "pp_contract", "ID", strID);
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
            conname.Text = "";
            concode.Text = getconcode();
            partyA.Text = "";
            conserial.Text = "";
            condate.SelectedDate = null;
        }

        private void initinfo(string strID)
        {
            pp_contract pc= new pp_contract();
            System.Data.DataTable dt = pc.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            conname.Text = r["conname"].ToString().Trim();
            concode.Text = r["concode"].ToString().Trim();
            partyA.Text = r["partyA"].ToString().Trim();
            conserial.Text = r["conserial"].ToString().Trim();
            condate.SelectedDate = DateTime.Parse(r["condate"].ToString().Trim());
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("conname", conname.Text.Trim());
            dic.Add("concode", concode.Text.Trim());
            dic.Add("partyA", partyA.Text.Trim());
            dic.Add("conserial", conserial.Text.Trim());
            dic.Add("condate", condate.SelectedDate.ToString());
            dic.Add("operater",SessionUserName.Trim());

            return dic;
        }

        private string getconcode()
        {
            pp_contract pc = new pp_contract();

            string strpdcode = pc.getmaxconcode();
            string strprecode = "SC";

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

        #endregion

        #region 新增/neweditchildWindow

        protected void btnSaveChild_Click(object sender, EventArgs e)
        {
            string strID = editchildID.Text.ToString().Trim();
            string strpid = editID.Text.ToString().Trim();
            Dictionary<string, string> dic = initchildDatadic(strpid);

            pp_contract_content pcc = new pp_contract_content();
            int intresult = 0;

            if (strID == "")
            {
                dic.Add("ID", Guid.NewGuid().ToString());

                intresult = pcc.add(dic, "pp_contract_content");
            }
            else
            {
                intresult = pcc.update(dic, "pp_contract_content", "ID", strID);
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
            conpdname.Text = "";
            conpdmodel.Text = "";
            conpdnumber.Text = "1";
            conpduint.SelectedIndex = 3;
            conpdprice.Text = "1.00";
            conpdmoney.Text = "";
            deliveryaddress.Text = "";
            deliverydate.SelectedDate = null;
        }

        private void initchildinfo(string strID)
        {
            pp_contract_content pcc = new pp_contract_content();
            System.Data.DataTable dt = pcc.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editchildID.Text = strID;
            conpdname.Text = r["conpdname"].ToString().Trim();
            conpdmodel.Text = r["conpdmodel"].ToString().Trim();
            conpdnumber.Text = r["conpdnumber"].ToString().Trim();
            conpduint.SelectedValue= r["conpduint"].ToString().Trim();
            conpdprice.Text = r["conpdprice"].ToString().Trim();
            deliveryaddress.Text = r["deliveryaddress"].ToString().Trim();
            deliverydate.SelectedDate =DateTime.Parse(r["deliverydate"].ToString().Trim());
        }

        private Dictionary<string, string> initchildDatadic(string strpid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("pid", strpid.Trim());
            dic.Add("conpdname", conpdname.Text.Trim());
            dic.Add("conpdmodel", conpdmodel.Text.Trim());
            dic.Add("conpdnumber", conpdnumber.Text.Trim());
            dic.Add("conpduint", conpduint.SelectedValue.Trim());
            dic.Add("conpdprice", conpdprice.Text.Trim());
            dic.Add("deliveryaddress", deliveryaddress.Text.Trim());
            dic.Add("deliverydate", deliverydate.SelectedDate.ToString());
            dic.Add("operater", SessionUserName.Trim());

            return dic;
        }

        protected void conpdnumber_Blur(object sender, EventArgs e)
        {
            setconpdmoney();
        }

        protected void conpdprice_Blur(object sender, EventArgs e)
        {
            setconpdmoney();
        }

        private void setconpdmoney()
        {
            string strconpdnumber = conpdnumber.Text.Trim();
            string strconpdprice = conpdprice.Text.Trim();

            if(strconpdnumber!="" && strconpdprice!="")
            {
                conpdmoney.Text = (double.Parse(strconpdnumber) * double.Parse(strconpdprice)).ToString("f2");
            }
        }

        #endregion

        #region 表格处理

        protected void mainGrid_RowClick(Object sender, GridRowClickEventArgs e)
        {
            BindchildGrid(mainGrid.DataKeys[e.RowIndex][0].ToString());
        }

        private void BindchildGrid(string strpid)
        {
            pp_contract_content pcc = new pp_contract_content();
            System.Data.DataTable dt = pcc.getBindDataAsdt(strpid);

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
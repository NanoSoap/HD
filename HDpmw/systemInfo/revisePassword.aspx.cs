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
    public partial class revisePassword :PageBase
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
            initdropSex();
            initinfo();
        }

        private void initdropSex()
        {
            xparams x = new xparams();

            string str = x.getparamData("H002");

            sex.DataSource = commonLib.stringTolist(str, ',');
            sex.DataBind();
        }

        private void initinfo()
        {
            userLogin ul = new userLogin();
            System.Data.DataTable dt = ul.getUserinfo(SessionUserName);

            DataRow r = dt.Rows[0];

            fullname.Text = r["fullname"].ToString().Trim();
            username.Text = r["username"].ToString().Trim();
            password.Text = r["password"].ToString().Trim();
            sex.SelectedValue = r["sex"].ToString().Trim();

            DateTime seldate;
            if (System.DateTime.TryParse(r["birdate"].ToString().Trim(), out seldate))
            {
                birdate.SelectedDate = seldate;
            }

            telephone.Text = r["telephone"].ToString().Trim();
            address.Text = r["address"].ToString().Trim();
        }

        #endregion

        #region 

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = initDatadic();
            userLogin ul = new userLogin();
            int intresult = 0;

            intresult = ul.update(dic, "userLogin", "username", username.Text.Trim());

            initinfo();

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
            initinfo();
        }

        private Dictionary<string, string> initDatadic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("fullname", fullname.Text.Trim());
            dic.Add("password", password.Text.Trim());
            dic.Add("sex", sex.SelectedValue.Trim());

            DateTime seldate;
            if (birdate.Text.Trim() != "" && System.DateTime.TryParse(birdate.Text.Trim(), out seldate))
            {
                dic.Add("birdate", birdate.Text.Trim());
            }

            dic.Add("telephone", telephone.Text.Trim());
            dic.Add("address", address.Text.Trim());
            dic.Add("regperson", SessionUserName.Trim());
            dic.Add("regdate", System.DateTime.Now.ToString());

            return dic;
        }

        #endregion
    }
}
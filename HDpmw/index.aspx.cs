using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDBusiness;

namespace HDpmw
{
    public partial class index : PageBase
    {
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Constants.IS_BASE)
                {
                    treeMenu.HideHScrollbar = false;
                    treeMenu.HideVScrollbar = false;
                    treeMenu.ExpanderToRight = false;
                    treeMenu.HeaderStyle = false;
                }

                inituser();
                inittreeMenu();
            }
        }

        private void inituser()
        {
            userLogin ul = new userLogin();
            System.Data.DataTable dt = ul.getUserinfo(SessionUserName);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                user.Text = r["fullname"].ToString();

                if(r["sex"].ToString().Trim()=="女")
                {
                    user.IconUrl = "~/res/images/woman.png";
                }
            }
        }

        //用户个人信息
        protected void UserProfile_Click(object sender,EventArgs e)
        {
            userLogin ul = new userLogin();
            System.Data.DataTable dt = ul.getUserinfo(SessionUserName);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                string strUserinfo = "<div>" +
                            "<p>" +
                            "<a href =\"http://book.douban.com/subject/25943598/\" style = \"font-size: 18px\" target =\"_blank\"><b>" + r["fullname"].ToString().Trim() + "</b></a>" +
                            "</p>" +
                            "<p>出生于" + r["birdate"].ToString() + "  手机：" + r["telephone"].ToString() + "  家庭住址：" + r["address"].ToString() +
                            "</p>" +
                            "<p>" +
                            "所属部门：" + r["department"].ToString() + " 工号：" + r["staffno"].ToString()+
                            "</p></div>";

                txtUserinfo.Text =Server.HtmlEncode(strUserinfo);
                windowUserInfo.Hidden = false;

                PageContext.RegisterStartupScript("refresh();");
            }
        }

        #region 树初始化
        private void inittreeMenu()
        {
            // 模拟从数据库返回数据表
            userMenu um= new userMenu();
            DataTable table = um.gettreeMenudata(SessionUserName);

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["pid"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("pid"))
                {               
                    FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                    node.Text = row["nodetext"].ToString();
                    node.NodeID = row["nodeid"].ToString();
                    node.EnableCheckEvent = true;
                    treeMenu.Nodes.Add(node);
                    ResolveSubTree(row, node);
                }
            }

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
                    string strnodeid = row["nodeid"].ToString();
                    node.NodeID = strnodeid;
                    node.NavigateUrl = strnodeid.Replace("_", "/") + ".aspx";
                    node.EnableCheckEvent = true;
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        #endregion

        #endregion
    }
}
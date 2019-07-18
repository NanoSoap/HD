using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using YDCode;
using System.Collections;
using System.Data;
using HDBusiness;
using FineUIPro;

namespace HDpmw.common
{
    public partial class main : PageBase
    {

        string[] s = { "<p>任意条目都可以双击在中间栏显示详细数据</p>",
            "<p>系统页面导航位于左侧<br/><br/>点击任意主目录可以合并或展开子目录</p>",
            "表格分页按钮位于底部<br/><br/>按住并拖动表格框架可以更改列宽",
            "单击表格列名称可按升序排列数据<br/><br/>再次点击为降序",
         "欲修改登记表下拉菜单信息<br/><br/>可以前往系统信息->参数配置页面中进行",
"单个浏览器窗口最多可以打开10个标签页面<br/><br/>如需关闭可以将鼠标移动到标签上右键 选择关闭全部"};

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                prevPage.Enabled = false;
            }
            else
            {

            }

        }
        protected void prevPage_Click(object sender, EventArgs e)
        {

            i.Text = (int.Parse(i.Text) - 1).ToString();

            Label1.Text = s[int.Parse(i.Text)];
            prevPage.Enabled = i.Text == "0" ? false : true;
            nextPage.Enabled = i.Text == (s.Length - 1).ToString() ? false : true;

        }

        protected void nextPage_Click(object sender, EventArgs e)
        {
            i.Text = (int.Parse(i.Text) + 1).ToString();
            Label1.Text = s[int.Parse(i.Text)];

            nextPage.Enabled = i.Text == (s.Length - 1).ToString() ? false : true;
            prevPage.Enabled = i.Text == "0" ? false : true;

        }

    }
}

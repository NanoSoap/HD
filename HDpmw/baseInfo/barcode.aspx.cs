using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;

namespace HDpmw.baseInfo
{
    public partial class barcode: PageBase
    {
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                initpage();
            }
        }

        private const string B_WIDTH = "247";
        private const string B_HEIGHT = "117";
        private void initpage()
        {
            f_width.Text = B_WIDTH;
            f_height.Text = B_HEIGHT;
            LODOPPanel.Hidden = true;
            initbarcodetype();
            inittexttype();
            initcolortype();
            initfontsizetype();
        }

        private void initbarcodetype()
        {
            List<string> barlist = new List<string>();
            
            barlist.Add("128A");
            barlist.Add("128B");
            barlist.Add("128C");
            barlist.Add("128Auto");
            barlist.Add("EAN8");
            barlist.Add("EAN13");
            barlist.Add("EAN128A");
            barlist.Add("EAN128B");
            barlist.Add("EAN128C");
            barlist.Add("Code39");
            barlist.Add("39Extended");
            barlist.Add("2_5interleaved");
            barlist.Add("2_5industrial");
            barlist.Add("2_5matrix");
            barlist.Add("UPC_A");
            barlist.Add("UPC_E0");
            barlist.Add("UPC_E1");
            barlist.Add("UPCsupp2");
            barlist.Add("UPCsupp5");
            barlist.Add("Code93");
            barlist.Add("93Extended");
            barlist.Add("MSI");
            barlist.Add("PostNet");
            barlist.Add("Codabar");
            barlist.Add("QRCode");
            barlist.Add("PDF417");

            f_bartype.DataSource = barlist;
            f_bartype.DataBind();

            f_bartype.SelectedIndex = 2;
        }

        private void inittexttype()
        {
            f_texttype.Items.Clear();

            FineUIPro.ListItem item = new FineUIPro.ListItem("不显示文本", "ShowBarText_0");
            f_texttype.Items.Add(item);
            item = new FineUIPro.ListItem("等宽文本", "ShowBarText_1");
            f_texttype.Items.Add(item);
            item = new FineUIPro.ListItem("紧缩文本", "AlignJustify_2");
            f_texttype.Items.Add(item);
        }

        private void initcolortype()
        {
            f_color.Items.Clear();

            FineUIPro.ListItem item = new FineUIPro.ListItem("红", "#FF0000");
            f_color.Items.Add(item);
            item = new FineUIPro.ListItem("黄", "#FFFF00");
            f_color.Items.Add(item);
            item = new FineUIPro.ListItem("蓝", "#436EEE");
            f_color.Items.Add(item);
            item = new FineUIPro.ListItem("黑", "#242424");
            f_color.Items.Add(item);
            item = new FineUIPro.ListItem("青", "#40E0D0");
            f_color.Items.Add(item);
        }

        private void initfontsizetype()
        {
            f_fontsize.Items.Clear();

            FineUIPro.ListItem item = new FineUIPro.ListItem("10", "10");
            f_fontsize.Items.Add(item);
            item = new FineUIPro.ListItem("12", "12");
            f_fontsize.Items.Add(item);
            item = new FineUIPro.ListItem("16", "16");
            f_fontsize.Items.Add(item);
            item = new FineUIPro.ListItem("21", "21");
            f_fontsize.Items.Add(item);
            item = new FineUIPro.ListItem("36", "36");
            f_fontsize.Items.Add(item);
        }

        #endregion

        #region 

        protected void btnPrintview_Click(object sender, EventArgs e)
        {
            if (initbardata())
            {
                PageContext.RegisterStartupScript("preview();");
            }
            else
            {
                Alert.Show("请输入条码值且选择打印条码类型!");
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (initbardata())
            {
                LODOPPanel.Hidden = false;
                PageContext.RegisterStartupScript("show();");
            }
            else
            {
                Alert.Show("请输入条码值且选择打印条码类型!");
            }
        }

        private bool initbardata()
        {
            if (f_width.Text.Trim() == "" || f_height.Text.Trim() == "")
            {
                f_width.Text = B_WIDTH;
                f_height.Text = B_HEIGHT;
            }

            if (f_barcode.Text.Trim()!="" && f_bartype.SelectedValue.Trim()!="")
            {
                return true;
            }

            return false;
        }
        
        #endregion
    }
}
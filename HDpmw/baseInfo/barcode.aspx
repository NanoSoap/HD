<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="barcode.aspx.cs" Inherits="HDpmw.baseInfo.barcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>条码打印</title>
    <script src="../res/js/LodopFuncs.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server" />
        <f:Panel ID="mainPanel" CssClass="blockpanel" Margin="3px" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px" TitleAlign="Right"
                    Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="10px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:Button ID="btnPrintview" runat="server" Text="打印预览" Icon="PrinterGo" OnClick="btnPrintview_Click" ValidateForms="neweditForm">
                                </f:Button>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:Button ID="btnShow" runat="server" Text="条码显示" Icon="PageRefresh" OnClick="btnShow_Click">
                                </f:Button>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>                               
                                <f:HyperLink ID="HyperLink1" Text="下载插件" runat="server" ToolTip="打印前请先下载安装本打印插件"
                                    NavigateUrl="http://101.132.119.216:8082/download/CLodop_Setup_for_Win32NT.zip" CssStyle="color:black;font-size:12px;padding-top:6px;padding-right:9px;" />
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="true" EnableCollapse="true" Height="50px"
                    Title="顶部面板" ShowBorder="true" ShowHeader="false" BodyPadding="3px">
                    <Items>
                        <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                            BodyPadding="5 5 0 5" CssStyle="border:none;" LabelAlign="Right" LabelWidth="72px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="f_barcode" runat="server" Label="条码值" Text=""></f:TextBox>
                                        <f:TextBox ID="f_width" runat="server" Label="宽度" Text=""></f:TextBox>
                                        <f:TextBox ID="f_height" runat="server" Label="高度" Text=""></f:TextBox>                                      
                                        <f:DropDownList ID="f_texttype" runat="server" Label="文本">
                                        </f:DropDownList>
                                        <f:DropDownList ID="f_fontsize" runat="server" Label="大小">
                                        </f:DropDownList>
                                        <f:DropDownList ID="f_color" runat="server" Label="颜色">
                                        </f:DropDownList>
                                        <f:DropDownList ID="f_bartype" runat="server" Label="类型">
                                        </f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="100%"
                    Title="条码显示区域" ShowBorder="true" ShowHeader="true" RegionPercent="100%"
                    BodyPadding="10px">
                    <Items>
                        <f:ContentPanel ID="LODOPPanel" runat="server" ShowHeader="false" Hidden="true">
                            <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="800" height="400">
                                <param name="Caption" value="条码显示区域" />
                                <param name="Border" value="0" />
                                <param name="Color" value="white" />
                                <embed id="LODOP_EM" type="application/x-print-lodop" width="800" height="400" border="0" color="white" />
                            </object>
                        </f:ContentPanel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <%--ADD_PRINT_BARCODE(Top,Left,Width,Height,BarCodeType,BarCodeValue);
        关键参数含义:
        Width 条码的总宽度，计量单位px（1px=1/96英寸）
        Height 条码的总高度（一维条码时包括文字高度）
        BarCodeType 条码的类型（规制）名称
        BarCodeValue 条码值--%>

        <script lang="javascript" type="text/javascript">

            var LODOP;

            function preview() {

                 var txtbarcode = document.getElementById("<%=f_barcode.ClientID %>");
                var txtbartype = document.getElementById("<%=f_bartype.ClientID %>");
                var txtwidth = document.getElementById("<%=f_width.ClientID %>");
                var txtheight = document.getElementById("<%=f_height.ClientID %>");

                var txttype = document.getElementById("<%=f_texttype.ClientID %>");
                var txtfontsize = document.getElementById("<%=f_fontsize.ClientID %>");

                LODOP = getLodop();
                LODOP.PRINT_INIT("条码打印");   
                LODOP.SET_PRINT_PAGESIZE(0, 720, 160,""); 
                //LODOP.SET_PRINT_MODE("POS_BASEON_PAPER",true);
                LODOP.ADD_PRINT_BARCODE(28, 34, F(txtwidth).getText(), F(txtheight).getText(), F(txtbartype).getText(), F(txtbarcode).getText());
                var txtarr = F(txttype).getValue().split('_');
                LODOP.SET_PRINT_STYLEA(0, txtarr[0], txtarr[1]);
                LODOP.SET_PRINT_STYLEA(0, "FontSize",F(txtfontsize).getValue());

                LODOP.PREVIEW();
            }

            function show() {

                var txtbarcode = document.getElementById("<%=f_barcode.ClientID %>");
                var txtbartype = document.getElementById("<%=f_bartype.ClientID %>");
                var txtwidth = document.getElementById("<%=f_width.ClientID %>");
                var txtheight = document.getElementById("<%=f_height.ClientID %>");

                var txttype = document.getElementById("<%=f_texttype.ClientID %>");
                var txtcolor = document.getElementById("<%=f_color.ClientID %>");
                var txtfontsize = document.getElementById("<%=f_fontsize.ClientID %>");

                LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
                LODOP.PRINT_INIT("条码显示");               
                LODOP.ADD_PRINT_BARCODE(28, 34, F(txtwidth).getText(), F(txtheight).getText(), F(txtbartype).getText(), F(txtbarcode).getText());
                LODOP.SET_PRINT_STYLEA(0, "color", F(txtcolor).getValue());
                LODOP.SET_PRINT_STYLEA(0, "FontSize",F(txtfontsize).getValue());
                              
                //自定义打印条码文本
                //LODOP.SET_PRINT_STYLEA(0, "ShowBarText", 0); 无效
                //LODOP.SET_PRINT_STYLEA(0, "AlignJustify", 2); 无效
                //LODOP.ADD_PRINT_TEXT("13mm", "45mm", "40mm", "3mm", "" + strCode128 + "");
                //LODOP.SET_PRINT_STYLEA(0, "Alignment", 3);
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
                //LODOP.SET_PRINT_STYLEA(0, "Alignment", 3);
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);

                LODOP.SHOW_CHART();
            };

        </script>
    </form>
</body>
</html>

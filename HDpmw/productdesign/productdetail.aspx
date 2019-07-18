<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productdetail.aspx.cs" Inherits="HDpmw.productdesign.productdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品总成库</title>
    <link href="../res/third-party/jqueryuiautocomplete/jquery-ui.css" rel="stylesheet" />
    <style>
        .text-align-center input {
            text-align: center;
        }

        .repeatnode .f-tree-cell-text {
            color: orangered;
            font-weight: bold;
        }

        .repeatnode .f-tree-folder {
            color: orangered;
        }

        .photo {
            height: 320px;
            line-height: 320px;
            overflow: hidden;
        }

            .photo img {
                height: 320px;
                vertical-align: middle;
            }

        .mytable {
            border-width: 1px;
            border-style: solid;
            border-collapse: separate;
            border-spacing: 0;
            border-bottom-width: 0;
            border-right-width: 0;
            text-align: center;
        }

            .mytable th,
            .mytable td {
                padding: 5px;
                text-align: center;
                border-bottom-width: 1px;
                border-bottom-style: solid;
                border-right-width: 1px;
                border-right-style: solid;
                width: 200px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server" />
        <f:Panel ID="mainPanel" CssClass="blockpanel" Margin="3px" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region" MaxWidth="1960px" AutoScroll="true">
            <Items>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px" TitleAlign="Right"
                    Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>

                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px"
                    Title="顶部面板" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Items>
                        <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                            BodyPadding="5 5 0 5" CssStyle="border:none;" LabelAlign="Right" LabelWidth="72px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="f_pdtype" Label="类型" runat="server" AutoSelectFirstItem="false" EmptyText="请选择产品分类"></f:DropDownList>
                                        <f:TextBox ID="f_pdname" runat="server" Label="产品" Text=""></f:TextBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="300px"
                    Title="产品库" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
                    <Items>
                        <f:Tree ID="firsttree" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                            EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                            OnNodeCommand="firsttree_NodeCommand">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" MinWidth="1000px" Layout="Region"
                    Title="产品基本信息 / 产品设计仓库" ShowBorder="true" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                    <Items>
                        <f:TabStrip ID="GrapkTabStrip" IsFluid="true" CssClass="blockpanel" RegionPosition="Center"
                            AutoPostBack="true" OnTabIndexChanged="GrapkTabStrip_TabIndexChanged"
                            ShowBorder="false" ActiveTabIndex="0" runat="server">
                            <Tabs>
                                <f:Tab Title="基本信息" BodyPadding="5px" runat="server" Icon="Images" Layout="HBox">
                                    <Items>
                                        <f:Panel ID="Panel3" Title="产品基本信息" runat="server" BoxFlex="1"
                                            BodyPadding="10px" Hidden="true" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Form BodyPadding="3px" ID="neweditForm" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                    LabelAlign="Right" runat="server" Title="表单 1">
                                                    <Rows>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="pdname" Label="名称" runat="server" Text="">
                                                                </f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="pdcode" Label="编码" runat="server" Text="">
                                                                </f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="pdtype" Label="类型" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="drawdate" Label="日期" runat="server" Text="">
                                                                </f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="designer" Label="设计" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="checker" Label="核对" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="stanarder" Label="标准" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="drawer" Label="制图" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="examiner" Label="审核" runat="server" Text=""></f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label runat="server" ID="specifications" Label="<span>装配&nbsp;&nbsp;&nbsp;&nbsp;<br/>要求</span>">
                                                                </f:Label>
                                                            </Items>
                                                        </f:FormRow>
                                                    </Rows>
                                                </f:Form>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel1" Title="产品图纸文档" BoxFlex="2" runat="server" AutoScroll="true"
                                            BodyPadding="0px" Hidden="true" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:TabStrip ID="CADPhotoTabStrip" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
                                                    AutoPostBack="true" OnTabIndexChanged="CADPhotoTabStrip_TabIndexChanged"
                                                    ShowBorder="false" ActiveTabIndex="1" runat="server">
                                                    <Tabs>
                                                        <f:Tab Title="零件文档" BodyPadding="5px" runat="server" Icon="Images">
                                                            <Items>
                                                            </Items>
                                                        </f:Tab>
                                                        <f:Tab Title="零件图片" BodyPadding="5px" runat="server" Icon="Photos">
                                                            <Items>
                                                                <f:SimpleForm RegionPosition="Center" ID="SimpleForm2" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                                                    <Items>
                                                                        <f:Image ID="imgPhoto" ImageUrl="../resources/photo/法兰.png"
                                                                            CssClass="photo" ShowEmptyLabel="true" runat="server">
                                                                        </f:Image>
                                                                    </Items>
                                                                </f:SimpleForm>
                                                            </Items>
                                                        </f:Tab>
                                                        <f:Tab Title="AUTOCAD" BodyPadding="5px" runat="server" Icon="ChartLineLink">
                                                            <Items>
                                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" BodyPadding="0px">
                                                                    <object classid="clsid:74A777F8-7A8F-4e7c-AF47-7074828086E2"
                                                                        id="MxDrawXCtrl" width="100%" height="510px">
                                                                        <param name="_Version" value="65536" />
                                                                        <param name="_ExtentX" value="24262" />
                                                                        <param name="_ExtentY" value="16219" />
                                                                        <param name="_StockProps" value="0" />
                                                                        <param name="DwgFilePath" value="" />
                                                                        <param name="IsRuningAtIE" value="1" />
                                                                        <param name="EnablePrintCmd" value="0" />
                                                                        <param name="FirstRunPan" value="0" />
                                                                        <param name="ShowCommandWindow" value="0" />
                                                                        <param name="ShowToolBars" value="0" />
                                                                        <param name="ShowStatusBar" value="0" />
                                                                        <param name="ShowModelBar" value="0" />
                                                                        <param name="Iniset" value="" />
                                                                        <param name="ToolBarFiles" value="" />
                                                                        <param name="ShowMenuBar" value="0" />
                                                                        <param name="EnableUndo" value="1" />
                                                                        <param name="ShowPropertyWindow" value="0" />
                                                                    </object>
                                                                    <f:Button ID="btnmxdtool" Text="工具栏" IconFont="Bars" runat="server" OnClientClick="mxdtool()" EnablePostBack="false"></f:Button>

                                                                </f:ContentPanel>
                                                            </Items>
                                                        </f:Tab>
                                                        <f:Tab Title="3D-Entity" BodyPadding="5px" runat="server" Icon="Shape3d">
                                                            <Items>
                                                            </Items>
                                                        </f:Tab>
                                                    </Tabs>
                                                </f:TabStrip>

                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="产品库" BodyPadding="0px" runat="server" Icon="Shape3d" Layout="Region">

                                    <Items>
                                        <f:Panel ID="Panel9" Title="产品库" runat="server" RegionPosition="Left" Width="320px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree1" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                                                    OnNodeCommand="Tree1_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu2" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                                            ShowBorder="false" ShowHeader="false" BodyPadding="0px" Layout="Region">

                                            <Items>
                                                <f:Panel runat="server" EnableCollapse="true" RegionPercent="50%" RegionPosition="Top" RegionSplit="false"
                                                    Title="装配工艺卡列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" AutoScroll="true">

                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage4" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click4">
                                                                </f:Button>
                                                                <f:Button ID="PrePage4" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click4">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage4" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage4" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click4">
                                                                </f:Button>
                                                                <f:Button ID="LastPage4" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click4">
                                                                </f:Button>
                                                                <f:Button ID="GoPage4" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click4">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber4" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click4">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize4" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber4" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click4">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage4" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill7" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt4" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Items>
                                                        <f:Grid ID="Grid1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                            EnableMultiSelect="false" SortDirection="ASC" SortField="iname"
                                                            DataKeyNames="ID,iname" BoxFlex="1" AllowSorting="true" EnableCollapse="false"
                                                            EnableHeaderMenuSort="true" ForceFit="true" CssClass="blockpanel" DataIDField="ID" EnableRowClickEvent="true"
                                                            EnableRowDoubleClickEvent="true" OnRowClick="Grid1_RowClick">


                                                            <Columns>
                                                                <f:BoundField Width="65px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                                                <f:BoundField Width="200px" DataField="iname" HeaderText="工艺卡名称" TextAlign="Center" SortField="mname" DataFormatString="{0}" />
                                                                <f:BoundField Width="200px" DataField="kitname" HeaderText="产品/组件名称" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                <f:BoundField Width="180px" DataField="kitcode" HeaderText="产品/组件型号" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                <f:BoundField Width="200px" DataField="icode" HeaderText="工艺卡编号" TextAlign="Center" SortField="mcode" DataFormatString="{0}" />

                                                            </Columns>
                                                            <Listeners>
                                                                <f:Listener Event="rowexpanderexpand" Handler="onRowExpanderExpand" />
                                                                <f:Listener Event="rowexpandercollapse" Handler="onRowExpanderCollapse" />
                                                            </Listeners>
                                                        </f:Grid>
                                                    </Items>
                                                </f:Panel>
                                                <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionSplit="true"
                                                    Title="工序列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true">
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage1" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click1">
                                                                </f:Button>
                                                                <f:Button ID="PrePage1" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click1">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage1" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage1" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click1">
                                                                </f:Button>
                                                                <f:Button ID="LastPage1" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click1">
                                                                </f:Button>
                                                                <f:Button ID="GoPage1" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click1">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber1" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click1">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize1" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber1" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click1">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage1" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill5" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt1" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Items>
                                                        <f:Grid ID="Grid5" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                            EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC" SortField="mname"
                                                            DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false" EnableRowClickEvent="true"
                                                            EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID"
                                                            OnRowClick="Grid5_RowClick">
                                                            <Columns>
                                                                <f:RowNumberField Width="55px" HeaderText="序号" TextAlign="Center">
                                                                </f:RowNumberField>
                                                                <f:RenderField Width="90px" ColumnID="batchnumber" DataField="batchnumber" HeaderText="工序号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                </f:RenderField>
                                                                <f:RenderField Width="100px" ColumnID="batchname" DataField="batchname" HeaderText="工序名称" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                </f:RenderField>
                                                                <f:RenderField Width="300px" ColumnID="batchtext" DataField="batchtext" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField Width="72px" ColumnID="workshop" DataField="workshop" HeaderText="车间" TextAlign="Center" SortField="workshop">
                                                                </f:RenderField>

                                                                <f:RenderField Width="72px" ColumnID="bdevice" DataField="bdevice" HeaderText="设备" TextAlign="Left" SortField="bdevice">
                                                                </f:RenderField>

                                                            </Columns>
                                                        </f:Grid>
                                                    </Items>
                                                </f:Panel>

                                            </Items>

                                        </f:Panel>
                                        <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionPosition="Right" RegionPercent="35%"
                                            Title="工步列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true" RegionSplit="true">
                                            <Toolbars>
                                                <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                    <Items>
                                                        <f:Button ID="FirstPage5" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click5">
                                                        </f:Button>
                                                        <f:Button ID="PrePage5" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click5">
                                                        </f:Button>
                                                        <f:TextBox ID="CurPage5" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                        </f:TextBox>
                                                        <f:Button ID="NextPage5" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click5">
                                                        </f:Button>
                                                        <f:Button ID="LastPage5" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click5">
                                                        </f:Button>
                                                        <f:Button ID="GoPage5" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click5">
                                                        </f:Button>
                                                        <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                        <f:Button ID="SubNumber5" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click5">
                                                        </f:Button>
                                                        <f:Label ID="GridPageSize5" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                        <f:Button ID="UpNumber5" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click5">
                                                        </f:Button>
                                                        <f:Button ID="TotalPage5" runat="server" Hidden="true"></f:Button>
                                                        <f:ToolbarFill ID="ToolbarFill6" runat="server"></f:ToolbarFill>
                                                        <f:Label ID="MemoTxt5" Text="页码" runat="server" Hidden="true"></f:Label>
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Items>
                                                <f:Grid ID="Grid6" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC" SortField="systemdate"
                                                    DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false"
                                                    EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID">

                                                    <Columns>
                                                        <f:RowNumberField Width="60px" HeaderText="序号" TextAlign="Center">
                                                        </f:RowNumberField>
                                                        <f:RenderField Hidden="true" Width="60px" ColumnID="snumber" DataField="snumber" HeaderText="编号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                        </f:RenderField>

                                                        <f:RenderField Width="300px" ColumnID="stext" DataField="stext" HeaderText="内容" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                                        </f:RenderField>



                                                        <f:RenderField Width="200px" ColumnID="stool" DataField="stool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                                        </f:RenderField>

                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="元零件库" BodyPadding="0px" runat="server" Icon="RubyPut" Layout="Region">

                                    <Items>

                                        <f:Panel ID="Panel7" Title="元零件库" runat="server" RegionPosition="Left" Width="230px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree2" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" Title="树控件" ShowBorder="false" runat="server"
                                                    OnNodeCommand="Tree2_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu1" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                                            ShowBorder="false" ShowHeader="false" BodyPadding="0px" Layout="Region">
                                            <Items>
                                                <f:Panel ID="Panmach" runat="server" AutoScroll="true" RegionPosition="Center"
                                                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Hidden="false">
                                                    <Items>
                                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" AutoScroll="true"
                                                            Title="中间面板" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                                                            <Items>
                                                                <f:Panel ID="Panel13" Title="零件基本信息" runat="server"
                                                                    BodyPadding="10px" ShowBorder="false" ShowHeader="true" EnableCollapse="true">
                                                                    <Items>
                                                                        <f:Form BodyPadding="3px" ID="Form4" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                                            LabelAlign="Right" runat="server" Title="表单 1">
                                                                            <Rows>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="mname" Label="名称" runat="server" Text="">
                                                                                        </f:Label>
                                                                                        <f:Label ID="mcode" Label="编码" runat="server" Text="">
                                                                                        </f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow ColumnWidths="50% 50%">
                                                                                    <Items>
                                                                                        <f:Label ID="drawdate1" Label="日期" runat="server">
                                                                                        </f:Label>
                                                                                        <f:Label ID="designer1" Label="设计" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow ColumnWidths="50% 50%">
                                                                                    <Items>
                                                                                        <f:Label ID="checker1" Label="核对" runat="server"></f:Label>
                                                                                        <f:Label ID="stanarder1" Label="标准" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow ColumnWidths="50% 50%">
                                                                                    <Items>
                                                                                        <f:Label ID="drawer1" Label="制图" runat="server"></f:Label>
                                                                                        <f:Label ID="examiner1" Label="审核" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label runat="server" ID="TextArea1" Label="<span>技术&nbsp;&nbsp;&nbsp;&nbsp;<br/>要求</span>">
                                                                                        </f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                            </Rows>
                                                                        </f:Form>
                                                                    </Items>
                                                                </f:Panel>
                                                                <f:Panel ID="Panel14" Title="零件特征信息表" BoxFlex="1" runat="server" AutoScroll="true"
                                                                    BodyPadding="0px" ShowBorder="false" ShowHeader="true" EnableCollapse="true">
                                                                    <Items>
                                                                        <f:Grid ID="Grid4" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                                            SortField="featurename" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                                            DataKeyNames="featureid,pid,featurevalue" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                                                            <Columns>
                                                                                <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                                                <f:BoundField Width="120px" DataField="featurename" HeaderText="特征名" TextAlign="Left" SortField="featurename" DataFormatString="{0}" />
                                                                                <f:RenderField Width="100px" ColumnID="featurevalue" DataField="featurevalue"
                                                                                    ExpandUnusedSpace="true" HeaderText="特征值">
                                                                                </f:RenderField>
                                                                            </Columns>
                                                                        </f:Grid>
                                                                    </Items>
                                                                </f:Panel>
                                                            </Items>
                                                        </f:Panel>
                                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" MinWidth="530px"
                                                            Title="零件图纸/图片/3D" ShowBorder="false" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                                                            <Items>
                                                                <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
                                                                    AutoPostBack="true" OnTabIndexChanged="GrapkTabStrip_TabIndexChanged"
                                                                    ShowBorder="false" ActiveTabIndex="1" runat="server">
                                                                    <Tabs>
                                                                        <f:Tab Title="零件文档" BodyPadding="5px" runat="server" Icon="Images">
                                                                            <Items>
                                                                            </Items>
                                                                        </f:Tab>
                                                                        <f:Tab Title="零件图片" BodyPadding="5px" runat="server" Icon="Photos">
                                                                            <Items>
                                                                                <f:SimpleForm RegionPosition="Center" ID="SimpleForm4" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                                                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                                                                    <Items>
                                                                                        <f:Image ID="imgPhoto1" ImageUrl="../resources/photo/法兰.png"
                                                                                            CssClass="photo" ShowEmptyLabel="true" runat="server">
                                                                                        </f:Image>
                                                                                    </Items>
                                                                                </f:SimpleForm>
                                                                            </Items>
                                                                        </f:Tab>
                                                                        <f:Tab Title="AUTOCAD" BodyPadding="5px" runat="server" Icon="ChartLineLink">
                                                                            <Items>
                                                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" BodyPadding="0px">
                                                                                    <object classid="clsid:74A777F8-7A8F-4e7c-AF47-7074828086E2"
                                                                                        id="MxDrawXCtrl1" width="100%" height="510px">
                                                                                        <param name="_Version" value="65536" />
                                                                                        <param name="_ExtentX" value="24262" />
                                                                                        <param name="_ExtentY" value="16219" />
                                                                                        <param name="_StockProps" value="0" />
                                                                                        <param name="DwgFilePath" value="" />
                                                                                        <param name="IsRuningAtIE" value="1" />
                                                                                        <param name="EnablePrintCmd" value="0" />
                                                                                        <param name="FirstRunPan" value="0" />
                                                                                        <param name="ShowCommandWindow" value="0" />
                                                                                        <param name="ShowToolBars" value="0" />
                                                                                        <param name="ShowStatusBar" value="0" />
                                                                                        <param name="ShowModelBar" value="0" />
                                                                                        <param name="Iniset" value="" />
                                                                                        <param name="ToolBarFiles" value="" />
                                                                                        <param name="ShowMenuBar" value="0" />
                                                                                        <param name="EnableUndo" value="1" />
                                                                                        <param name="ShowPropertyWindow" value="0" />
                                                                                    </object>
                                                                                    <f:Button ID="Button1" Text="工具栏" IconFont="Bars" runat="server" OnClientClick="mxdtool()" EnablePostBack="false"></f:Button>

                                                                                </f:ContentPanel>
                                                                            </Items>
                                                                        </f:Tab>
                                                                        <f:Tab Title="3D-Entity" BodyPadding="5px" runat="server" Icon="Shape3d">
                                                                            <Items>
                                                                                <f:Label ID="Label3" CssClass="highlight" Text="初始文本" runat="server" />
                                                                            </Items>
                                                                        </f:Tab>
                                                                    </Tabs>
                                                                </f:TabStrip>
                                                            </Items>
                                                        </f:Panel>
                                                    </Items>
                                                </f:Panel>
                                                <f:Panel ID="Panmech" runat="server" EnableCollapse="true" RegionPosition="Center" RegionSplit="true"
                                                    ShowBorder="false" ShowHeader="true" BodyPadding="1px" AutoScroll="true" Hidden="true" Layout="Region">
                                                    <Items>
                                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                                                            ShowBorder="false" ShowHeader="false" BodyPadding="0px" Layout="Region">
                                                            <Items>
                                                                <f:Panel runat="server" EnableCollapse="true" RegionPercent="50%" RegionPosition="Top" RegionSplit="false"
                                                                    Title="机加工工艺卡列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
                                                                    <Toolbars>
                                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                                            <Items>
                                                                                <f:Button ID="FirstPage" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click">
                                                                                </f:Button>
                                                                                <f:Button ID="PrePage" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click">
                                                                                </f:Button>
                                                                                <f:TextBox ID="CurPage" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                                </f:TextBox>
                                                                                <f:Button ID="NextPage" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click">
                                                                                </f:Button>
                                                                                <f:Button ID="LastPage" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click">
                                                                                </f:Button>
                                                                                <f:Button ID="GoPage" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click">
                                                                                </f:Button>
                                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                                <f:Button Hidden="true" ID="SubNumber" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click">
                                                                                </f:Button>
                                                                                <f:Label Hidden="true" ID="GridPageSize" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                                <f:Button Hidden="true" ID="UpNumber" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click">
                                                                                </f:Button>
                                                                                <f:Button ID="TotalPage" runat="server" Hidden="true"></f:Button>
                                                                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                                                <f:Label ID="MemoTxt" Text="页码" runat="server"></f:Label>
                                                                            </Items>
                                                                        </f:Toolbar>
                                                                    </Toolbars>
                                                                    <Items>

                                                                        <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                                            EnableMultiSelect="false" SortDirection="ASC" SortField="mname"
                                                                            DataKeyNames="ID,mname" BoxFlex="1" AllowSorting="true" EnableCollapse="false"
                                                                            EnableHeaderMenuSort="true" ForceFit="true" CssClass="blockpanel" DataIDField="ID" EnableRowClickEvent="true"
                                                                            EnableRowDoubleClickEvent="true" OnSort="mainGrid_Sort" OnRowClick="mainGrid_RowClick">
                                                                            <Columns>
                                                                                <f:BoundField Width="60px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                                                                <f:BoundField Width="200px" DataField="mname" HeaderText="名称" TextAlign="Center" SortField="mname" DataFormatString="{0}" />
                                                                                <f:BoundField Width="200px" DataField="kitname" HeaderText="零（部）件名称" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="kitcode" HeaderText="零（部）件图号" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                                <f:BoundField Width="200px" DataField="mcode" HeaderText="工艺卡编号" TextAlign="Center" SortField="mcode" DataFormatString="{0}" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="mtag" HeaderText="材料牌号" TextAlign="Center" SortField="mtag" DataFormatString="{0}" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="rawtype" HeaderText="毛坯种类" TextAlign="Center" SortField="rawtype" DataFormatString="{0}" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="rawsize" HeaderText="毛坯外形尺寸" TextAlign="Center" SortField="rawsize" DataFormatString="{0}" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="nperraw" HeaderText="每毛坯可制件数" TextAlign="Center" SortField="nperraw" DataFormatString="{0}" />
                                                                                <f:BoundField Hidden="true" Width="200px" DataField="nperdesk" HeaderText="每台件数" TextAlign="Center" SortField="nperdesk" DataFormatString="{0}" />
                                                                                <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                                                                                    <ItemTemplate>
                                                                                    </ItemTemplate>
                                                                                </f:TemplateField>
                                                                            </Columns>
                                                                            <Listeners>
                                                                                <f:Listener Event="rowexpanderexpand" Handler="onRowExpanderExpand" />
                                                                                <f:Listener Event="rowexpandercollapse" Handler="onRowExpanderCollapse" />
                                                                            </Listeners>
                                                                        </f:Grid>
                                                                    </Items>
                                                                </f:Panel>
                                                                <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionSplit="true"
                                                                    Title="工序列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true">
                                                                    <Items>
                                                                        <f:Grid ID="Grid2" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                                            EnableMultiSelect="false" SortField="mname" SortDirection="ASC"
                                                                            DataKeyNames="ID" BoxFlex="1" AllowSorting="true"
                                                                            EnableHeaderMenuSort="true" OnSort="mainGrid_Sort1"
                                                                            EnableRowClickEvent="true" OnRowClick="grid2_RowClick">
                                                                            <Columns>
                                                                                <f:RowNumberField Width="55px" HeaderText="序号" TextAlign="Center">
                                                                                </f:RowNumberField>
                                                                                <f:RenderField Width="90px" ColumnID="batchnumber" DataField="batchnumber" HeaderText="工序号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                                    <Editor>
                                                                                        <f:TextBox runat="server"></f:TextBox>
                                                                                    </Editor>
                                                                                </f:RenderField>
                                                                                <f:RenderField Width="100px" ColumnID="batchname" DataField="batchname" HeaderText="工序名称" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                                    <Editor>
                                                                                        <f:DropDownList runat="server" ID="DropDownList1"></f:DropDownList>
                                                                                    </Editor>
                                                                                </f:RenderField>
                                                                                <f:RenderField Width="300px" ColumnID="batchtext" DataField="batchtext" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center">
                                                                                    <Editor>
                                                                                        <f:TextArea runat="server"></f:TextArea>
                                                                                    </Editor>
                                                                                </f:RenderField>
                                                                                <f:RenderField Width="72px" ColumnID="workshop" DataField="workshop" HeaderText="车间" TextAlign="Center" SortField="workshop">
                                                                                    <Editor>
                                                                                        <f:DropDownList runat="server" ID="DropDownList2"></f:DropDownList>
                                                                                    </Editor>
                                                                                </f:RenderField>
                                                                                <f:RenderField Width="72px" ColumnID="bdevice" DataField="bdevice" HeaderText="设备" TextAlign="Left" SortField="bdevice">
                                                                                    <Editor>
                                                                                        <f:TextBox runat="server">
                                                                                        </f:TextBox>
                                                                                    </Editor>
                                                                                </f:RenderField>
                                                                            </Columns>
                                                                        </f:Grid>
                                                                    </Items>
                                                                    <Toolbars>
                                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                                            <Items>
                                                                                <f:Button ID="FirstPage2" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click2">
                                                                                </f:Button>
                                                                                <f:Button ID="PrePage2" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click2">
                                                                                </f:Button>
                                                                                <f:TextBox ID="CurPage2" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                                </f:TextBox>
                                                                                <f:Button ID="NextPage2" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click2">
                                                                                </f:Button>
                                                                                <f:Button ID="LastPage2" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click2">
                                                                                </f:Button>
                                                                                <f:Button ID="GoPage2" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click2">
                                                                                </f:Button>
                                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                                <f:Button ID="SubNumber2" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click2" Hidden="true">
                                                                                </f:Button>
                                                                                <f:Label ID="GridPageSize2" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;" Hidden="true"></f:Label>
                                                                                <f:Button ID="UpNumber2" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click2" Hidden="true">
                                                                                </f:Button>
                                                                                <f:Button ID="TotalPage2" runat="server" Hidden="true"></f:Button>
                                                                                <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                                                                <f:Label ID="MemoTxt2" Text="页码" runat="server"></f:Label>
                                                                            </Items>
                                                                        </f:Toolbar>
                                                                    </Toolbars>
                                                                </f:Panel>
                                                            </Items>
                                                        </f:Panel>
                                                        <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionPosition="Right" RegionPercent="38%"
                                                            Title="工步列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true" RegionSplit="true">
                                                            <Items>
                                                                <f:Grid ID="Grid3" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                                    EnableMultiSelect="false" SortField="systemdate" SortDirection="ASC"
                                                                    DataKeyNames="ID" BoxFlex="1" AllowSorting="true"
                                                                    EnableHeaderMenuSort="true" OnSort="mainGrid_Sort1"
                                                                    EnableRowDoubleClickEvent="true">
                                                                    <Columns>
                                                                        <f:RowNumberField Width="60px" HeaderText="序号" TextAlign="Center">
                                                                        </f:RowNumberField>
                                                                        <f:RenderField Hidden="true" Width="60px" ColumnID="snumber" DataField="snumber" HeaderText="编号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                            <Editor>
                                                                                <f:TextBox runat="server"></f:TextBox>
                                                                            </Editor>
                                                                        </f:RenderField>
                                                                        <f:RenderField Width="300px" ColumnID="stext" DataField="stext" HeaderText="内容" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                                                            <Editor>
                                                                                <f:TextArea runat="server"></f:TextArea>
                                                                            </Editor>
                                                                        </f:RenderField>
                                                                        <f:RenderField Width="200px" ColumnID="stool" DataField="stool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                                                            <Editor>
                                                                                <f:TextBox runat="server">
                                                                                </f:TextBox>
                                                                            </Editor>
                                                                        </f:RenderField>
                                                                    </Columns>
                                                                </f:Grid>
                                                            </Items>
                                                            <Toolbars>
                                                                <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                                    <Items>
                                                                        <f:Button ID="FirstPage3" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click3">
                                                                        </f:Button>
                                                                        <f:Button ID="PrePage3" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click3">
                                                                        </f:Button>
                                                                        <f:TextBox ID="CurPage3" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                        </f:TextBox>
                                                                        <f:Button ID="NextPage3" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click3">
                                                                        </f:Button>
                                                                        <f:Button ID="LastPage3" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click3">
                                                                        </f:Button>
                                                                        <f:Button ID="GoPage3" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click3">
                                                                        </f:Button>
                                                                        <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                        <f:Button ID="SubNumber3" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click3" Hidden="true">
                                                                        </f:Button>
                                                                        <f:Label ID="GridPageSize3" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;" Hidden="true"></f:Label>
                                                                        <f:Button ID="UpNumber3" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click3" Hidden="true">
                                                                        </f:Button>
                                                                        <f:Button ID="TotalPage3" runat="server" Hidden="true"></f:Button>
                                                                        <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                                                        <f:Label ID="MemoTxt3" Text="页码" runat="server"></f:Label>
                                                                    </Items>
                                                                </f:Toolbar>
                                                            </Toolbars>
                                                        </f:Panel>
                                                    </Items>
                                                </f:Panel>
                                                <f:Panel ID="Panther" runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" AutoScroll="true" Layout="Region"
                                                    Title="中间面板" ShowBorder="false" ShowHeader="false" BodyPadding="0px" Hidden="true">
                                                    <Items>
                                                        <f:Panel runat="server" RegionPosition="Center" Layout="Region" AutoScroll="true"
                                                            Title="右上面板" ShowBorder="false" ShowHeader="false" EnableCollapse="true" BodyPadding="0px">
                                                            <Items>
                                                                <f:Panel ID="Panel12" Title="工艺卡基本信息" runat="server" RegionPosition="Left" Width="360px"
                                                                    BodyPadding="5px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" CssStyle="border-right:1px solid lightgray">
                                                                    <Items>
                                                                        <f:Form BodyPadding="3px" ID="Form6" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                                            LabelAlign="Right" MaxWidth="360px" runat="server" Title="表单 1">
                                                                            <Rows>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="tname" Label="名称" runat="server" Text="">
                                                                                        </f:Label>
                                                                                        <f:Label ID="tcode" Label="编号" runat="server" Text="">
                                                                                        </f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="mtag" Label="材料" runat="server" Text="">
                                                                                        </f:Label>
                                                                                        <f:Label ID="kitweight" Label="重量" runat="server">
                                                                                        </f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="craftmethod" Label="路线" runat="server" Text="">
                                                                                        </f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="designperson" Label="设计" runat="server"></f:Label>
                                                                                        <f:Label ID="designdate" Label="日期" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="auditperson" Label="审核" runat="server"></f:Label>
                                                                                        <f:Label ID="auditdate" Label="日期" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="normalperson" Label="标准化" runat="server"></f:Label>
                                                                                        <f:Label ID="normaldate" Label="日期" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                                <f:FormRow>
                                                                                    <Items>
                                                                                        <f:Label ID="approveperson" Label="批准" runat="server"></f:Label>
                                                                                        <f:Label ID="approvedate" Label="日期" runat="server"></f:Label>
                                                                                    </Items>
                                                                                </f:FormRow>
                                                                            </Rows>
                                                                        </f:Form>
                                                                    </Items>
                                                                </f:Panel>
                                                                <f:Panel Title="热处理工艺曲线图" runat="server" AutoScroll="true" Layout="Fit" RegionPosition="Center"
                                                                    ShowBorder="false" ShowHeader="true">
                                                                    <Items>
                                                                        <f:TabStrip ID="TabStrip2" IsFluid="true" CssClass="blockpanel"
                                                                            AutoPostBack="true" OnTabIndexChanged="GrapkTabStrip_TabIndexChanged"
                                                                            ShowBorder="false" ActiveTabIndex="1" runat="server">
                                                                            <Tabs>
                                                                                <f:Tab Title="零件文档" BodyPadding="5px" runat="server" Icon="Images">
                                                                                    <Items>
                                                                                    </Items>
                                                                                </f:Tab>
                                                                                <f:Tab Title="零件图片" BodyPadding="5px" runat="server" Icon="Photos">
                                                                                    <Items>
                                                                                        <f:SimpleForm RegionPosition="Center" ID="SimpleForm5" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                                                                            ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                                                                            <Items>
                                                                                                <f:Image ID="Image3" ImageUrl="../resources/photo/法兰.png"
                                                                                                    CssClass="photo" ShowEmptyLabel="true" runat="server">
                                                                                                </f:Image>

                                                                                            </Items>
                                                                                        </f:SimpleForm>
                                                                                    </Items>
                                                                                </f:Tab>
                                                                                <f:Tab Title="AUTOCAD" BodyPadding="5px" runat="server" Icon="ChartLineLink">
                                                                                    <Items>
                                                                                        <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" BodyPadding="0px">
                                                                                            <object classid="clsid:74A777F8-7A8F-4e7c-AF47-7074828086E2"
                                                                                                id="MxDrawXCtrl2" width="100%" height="339px">
                                                                                                <param name="_Version" value="65536" />
                                                                                                <param name="_ExtentX" value="24262" />
                                                                                                <param name="_ExtentY" value="16219" />
                                                                                                <param name="_StockProps" value="0" />
                                                                                                <param name="DwgFilePath" value="" />
                                                                                                <param name="IsRuningAtIE" value="1" />
                                                                                                <param name="EnablePrintCmd" value="0" />
                                                                                                <param name="FirstRunPan" value="0" />
                                                                                                <param name="ShowCommandWindow" value="0" />
                                                                                                <param name="ShowToolBars" value="0" />
                                                                                                <param name="ShowStatusBar" value="0" />
                                                                                                <param name="ShowModelBar" value="0" />
                                                                                                <param name="Iniset" value="" />
                                                                                                <param name="ToolBarFiles" value="" />
                                                                                                <param name="ShowMenuBar" value="0" />
                                                                                                <param name="EnableUndo" value="1" />
                                                                                                <param name="ShowPropertyWindow" value="0" />
                                                                                            </object>
                                                                                            <f:Button ID="Button2" Text="工具栏" IconFont="Bars" runat="server" OnClientClick="mxdtool()" EnablePostBack="false"></f:Button>

                                                                                        </f:ContentPanel>
                                                                                    </Items>
                                                                                </f:Tab>
                                                                                <f:Tab Title="3D-Entity" BodyPadding="5px" runat="server" Icon="Shape3d">
                                                                                    <Items>
                                                                                        <f:Label ID="Label1" CssClass="highlight" Text="初始文本" runat="server" />
                                                                                    </Items>
                                                                                </f:Tab>
                                                                            </Tabs>
                                                                        </f:TabStrip>
                                                                    </Items>
                                                                </f:Panel>
                                                                <f:Panel Title="工艺卡技术要求" runat="server" AutoScroll="true" Layout="Fit" RegionPosition="Right" MinWidth="360px"
                                                                    ShowBorder="false" ShowHeader="true" CssStyle="border-left:1px solid lightgray">
                                                                    <Items>
                                                                        <f:Grid ID="secondGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                                            SortField="iname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                                            DataKeyNames="ID,tid" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">

                                                                            <Columns>
                                                                                <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                                                <f:BoundField Width="100px" DataField="iname" SortField="iname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                                                <f:BoundField Width="100px" DataField="ivalue" HeaderText="值" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                                                <f:BoundField Width="100px" DataField="icheck" ExpandUnusedSpace="true" HeaderText="检验方法" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                                            </Columns>
                                                                        </f:Grid>
                                                                    </Items>
                                                                </f:Panel>
                                                            </Items>
                                                        </f:Panel>
                                                        <f:Panel ID="Panel15" Title="工艺卡工序内容" BoxFlex="1" runat="server" AutoScroll="true" RegionPosition="Bottom" MinHeight="270px"
                                                            BodyPadding="0px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" CssStyle="border-top:1px solid lightgray">
                                                            <Items>
                                                                <f:Grid ID="secondGrid1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" AllowPaging="false"
                                                                    DataKeyNames="ID,tid" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">

                                                                    <Columns>
                                                                        <f:BoundField Width="72px" DataField="batchnumber" HeaderText="工序号" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="batchtext" ExpandUnusedSpace="true" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="bdevice" ExpandUnusedSpace="true" HeaderText="设备" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="stove_code" ExpandUnusedSpace="true" HeaderText="装炉方式" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="stovetemp" ExpandUnusedSpace="true" HeaderText="装炉温度" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="heattemp" ExpandUnusedSpace="true" HeaderText="加热温度" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="heattime" ExpandUnusedSpace="true" HeaderText="升温时间(min)" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="keeptime" ExpandUnusedSpace="true" HeaderText="保温时间(min)" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="coolmedia" ExpandUnusedSpace="true" HeaderText="冷却介质" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="cooltemp" ExpandUnusedSpace="true" HeaderText="冷却温度" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="cooltime" ExpandUnusedSpace="true" HeaderText="冷却时间(S)" TextAlign="Center"></f:BoundField>
                                                                        <f:BoundField DataField="workhour" ExpandUnusedSpace="true" HeaderText="工时(min)" TextAlign="Center"></f:BoundField>
                                                                    </Columns>
                                                                </f:Grid>
                                                            </Items>
                                                        </f:Panel>
                                                    </Items>
                                                </f:Panel>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="标准件库" BodyPadding="0px" runat="server" Icon="ChartLineLink" Layout="Region">

                                    <Items>
                                        <f:Panel ID="Panel10" Title="标准件库" runat="server" RegionPosition="Left" Width="320px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree3" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                                                    OnNodeCommand="Tree3_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel6" Title="标准件库" runat="server" RegionPosition="center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Form BodyPadding="3px" ID="Form3" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                    LabelAlign="Right" MaxWidth="720px" runat="server" Title="表单 1">
                                                    <Rows>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="sname" Label="名称" runat="server" Text=""></f:Label>
                                                                <f:Label ID="scode" Label="编码" runat="server" Text=""></f:Label>

                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow ColumnWidths="50% 50%">
                                                            <Items>
                                                                <f:Label ID="stype" Label="类型" runat="server" Text=""></f:Label>
                                                                <f:Label ID="specification" Label="规格" runat="server" Text=""></f:Label>

                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow ColumnWidths="50% 50%">
                                                            <Items>
                                                                <f:Label ID="material" Label="材料" runat="server" Text=""></f:Label>

                                                            </Items>
                                                        </f:FormRow>
                                                    </Rows>
                                                </f:Form>
                                                <f:SimpleForm RegionPosition="Center" ID="SimpleForm3" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                                    <Items>
                                                        <f:Image ID="Image1" ImageUrl="../resources/photo/法兰.png"
                                                            CssClass="photo" ShowEmptyLabel="true" runat="server">
                                                        </f:Image>
                                                    </Items>
                                                </f:SimpleForm>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="外购件库" BodyPadding="0px" runat="server" Icon="BugMagnify" Layout="Region">

                                    <Items>
                                        <f:Panel ID="Panel11" Title="外购件库" runat="server" RegionPosition="Left" Width="320px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree4" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                                                    OnNodeCommand="Tree4_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" Title="外购件库" runat="server" RegionPosition="center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Form BodyPadding="3px" ID="Form2" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                    LabelAlign="Right" MaxWidth="720px" runat="server" Title="表单 1">
                                                    <Rows>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:Label ID="oname" Label="名称" runat="server" Text=""></f:Label>
                                                                <f:Label ID="ocode" Label="编码" runat="server" Text=""></f:Label>

                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow ColumnWidths="50% 50%">
                                                            <Items>
                                                                <f:Label ID="otype" Label="类型" runat="server" Text=""></f:Label>
                                                                <f:Label ID="ospecification" Label="规格" runat="server" Text=""></f:Label>

                                                            </Items>
                                                        </f:FormRow>
                                                    </Rows>
                                                </f:Form>
                                                <f:SimpleForm RegionPosition="Center" ID="SimpleForm1" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                                    <Items>
                                                        <f:Image ID="Image2" ImageUrl="../resources/photo/法兰.png"
                                                            CssClass="photo" ShowEmptyLabel="true" runat="server">
                                                        </f:Image>
                                                    </Items>
                                                </f:SimpleForm>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="元组件库" BodyPadding="0px" runat="server" Icon="Coins" Layout="Region">

                                    <Items>
                                        <f:Panel ID="Panel2" Title="元组件库" runat="server" RegionPosition="Left" Width="320px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree5" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                                                    OnNodeCommand="Tree5_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                                            ShowBorder="false" ShowHeader="false" BodyPadding="0px" Layout="Region">

                                            <Items>
                                                <f:Panel runat="server" EnableCollapse="true" RegionPercent="50%" RegionPosition="Top" RegionSplit="false"
                                                    Title="装配工艺卡列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" AutoScroll="true">

                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage6" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click6">
                                                                </f:Button>
                                                                <f:Button ID="PrePage6" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click6">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage6" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage6" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click6">
                                                                </f:Button>
                                                                <f:Button ID="LastPage6" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click6">
                                                                </f:Button>
                                                                <f:Button ID="GoPage6" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click6">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber6" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click6">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize6" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber6" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click6">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage6" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt6" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Items>
                                                        <f:Grid ID="Grid7" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                            EnableMultiSelect="false" SortDirection="ASC" SortField="iname"
                                                            DataKeyNames="ID,iname" BoxFlex="1" AllowSorting="true" EnableCollapse="false"
                                                            EnableHeaderMenuSort="true" ForceFit="true" CssClass="blockpanel" DataIDField="ID" EnableRowClickEvent="true"
                                                            EnableRowDoubleClickEvent="true" OnRowClick="Grid7_RowClick">


                                                            <Columns>
                                                                <f:BoundField Width="65px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                                                <f:BoundField Width="200px" DataField="iname" HeaderText="工艺卡名称" TextAlign="Center" SortField="mname" DataFormatString="{0}" />
                                                                <f:BoundField Width="200px" DataField="kitname" HeaderText="产品/组件名称" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                <f:BoundField Width="180px" DataField="kitcode" HeaderText="产品/组件型号" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                                                <f:BoundField Width="200px" DataField="icode" HeaderText="工艺卡编号" TextAlign="Center" SortField="mcode" DataFormatString="{0}" />

                                                            </Columns>
                                                            <Listeners>
                                                                <f:Listener Event="rowexpanderexpand" Handler="onRowExpanderExpand" />
                                                                <f:Listener Event="rowexpandercollapse" Handler="onRowExpanderCollapse" />
                                                            </Listeners>
                                                        </f:Grid>
                                                    </Items>
                                                </f:Panel>
                                                <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionSplit="true"
                                                    Title="工序列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true">
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage7" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click7">
                                                                </f:Button>
                                                                <f:Button ID="PrePage7" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click7">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage7" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage7" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click7">
                                                                </f:Button>
                                                                <f:Button ID="LastPage7" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click7">
                                                                </f:Button>
                                                                <f:Button ID="GoPage7" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click7">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber7" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click7">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize7" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber7" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click7">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage7" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill8" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt7" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Items>
                                                        <f:Grid ID="Grid8" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                            EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC" SortField="mname"
                                                            DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false" EnableRowClickEvent="true"
                                                            EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID"
                                                            OnRowClick="Grid8_RowClick">
                                                            <Columns>
                                                                <f:RowNumberField Width="55px" HeaderText="序号" TextAlign="Center">
                                                                </f:RowNumberField>
                                                                <f:RenderField Width="90px" ColumnID="batchnumber" DataField="batchnumber" HeaderText="工序号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                    <Editor>
                                                                        <f:TextBox runat="server"></f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="100px" ColumnID="batchname" DataField="batchname" HeaderText="工序名称" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                                    <Editor>
                                                                        <f:DropDownList runat="server" ID="DropDownList3"></f:DropDownList>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="300px" ColumnID="batchtext" DataField="batchtext" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center">
                                                                    <Editor>
                                                                        <f:TextArea runat="server"></f:TextArea>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="72px" ColumnID="workshop" DataField="workshop" HeaderText="车间" TextAlign="Center" SortField="workshop">
                                                                    <Editor>
                                                                        <f:DropDownList runat="server" ID="DropDownList4"></f:DropDownList>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <%--<f:RenderField Width="72px" ColumnID="batchsession" DataField="batchsession" HeaderText="工段" TextAlign="Center" SortField="batchsession">
                                                <Editor>
                                                    <f:TextBox runat="server"></f:TextBox>
                                                </Editor>
                                            </f:RenderField>--%>
                                                                <f:RenderField Width="72px" ColumnID="bdevice" DataField="bdevice" HeaderText="设备" TextAlign="Left" SortField="bdevice">
                                                                    <Editor>
                                                                        <f:TextBox runat="server">
                                                                        </f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <%--<f:RenderField Width="200px" ColumnID="btool" DataField="btool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                                <Editor>
                                                    <f:TextBox runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>--%>
                                                            </Columns>
                                                        </f:Grid>
                                                    </Items>
                                                </f:Panel>

                                            </Items>

                                        </f:Panel>
                                        <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionPosition="Right" RegionPercent="35%"
                                            Title="工步列表" ShowBorder="false" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true" RegionSplit="true">
                                            <Toolbars>
                                                <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                    <Items>
                                                        <f:Button ID="FirstPage8" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click8">
                                                        </f:Button>
                                                        <f:Button ID="PrePage8" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click8">
                                                        </f:Button>
                                                        <f:TextBox ID="CurPage8" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                        </f:TextBox>
                                                        <f:Button ID="NextPage8" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click8">
                                                        </f:Button>
                                                        <f:Button ID="LastPage8" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click8">
                                                        </f:Button>
                                                        <f:Button ID="GoPage8" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click8">
                                                        </f:Button>
                                                        <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                        <f:Button ID="SubNumber8" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click8">
                                                        </f:Button>
                                                        <f:Label ID="GridPageSize8" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                        <f:Button ID="UpNumber8" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click8">
                                                        </f:Button>
                                                        <f:Button ID="TotalPage8" runat="server" Hidden="true"></f:Button>
                                                        <f:ToolbarFill ID="ToolbarFill9" runat="server"></f:ToolbarFill>
                                                        <f:Label ID="MemoTxt8" Text="页码" runat="server" Hidden="true"></f:Label>
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Items>
                                                <f:Grid ID="Grid9" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC" SortField="systemdate"
                                                    DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false"
                                                    EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID">

                                                    <Columns>
                                                        <f:RowNumberField Width="60px" HeaderText="序号" TextAlign="Center">
                                                        </f:RowNumberField>
                                                        <f:RenderField Hidden="true" Width="60px" ColumnID="snumber" DataField="snumber" HeaderText="编号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                        </f:RenderField>

                                                        <f:RenderField Width="300px" ColumnID="stext" DataField="stext" HeaderText="内容" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                                        </f:RenderField>



                                                        <f:RenderField Width="200px" ColumnID="stool" DataField="stool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                                        </f:RenderField>

                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="总成统计" BodyPadding="0px" runat="server" Icon="Coins" Layout="Region">
                                    <Items>
                                        <f:Panel ID="Panel4" Title="总成统计" runat="server" RegionPosition="Left" Width="320px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" AutoScroll="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="Tree6" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                    EnableCollapse="true" ShowBorder="false" Title="树控件" runat="server"
                                                    OnNodeCommand="Tree6_NodeCommand">
                                                    <Listeners>
                                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                                    </Listeners>
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel8" Title="总成统计" runat="server" RegionPosition="center"
                                            BodyPadding="10px" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableCollapse="true">
                                            <Items>
                                                <f:Label ID="detailinfo" runat="server" Text="" EncodeText="false"></f:Label>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>

                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Panel>
        </Items>
        </f:Panel>

        <f:Menu ID="firstmenu" runat="server">
            <Items>

                <f:MenuButton ID="btnmenuCAD" runat="server" Text="CAD" OnClick="menuCAD_Click" OnClientClick="settabindex()" Icon="ChartLineLink">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow" />
            </Listeners>
        </f:Menu>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnmenuMach" runat="server" Text="基本信息" OnClick="menuMach_Click" Icon="ApplicationViewDetail">
                </f:MenuButton>
                <f:MenuButton ID="btncadLookup" runat="server" Text="基本信息CAD" OnClick="cadLookup_Click" Icon="ApplicationEdit" OnClientClick="settabindex1()">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuMech" runat="server" Text="机加工工艺" OnClick="menuMech_Click" Icon="BasketEdit">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuTher" runat="server" Text="热处理工艺" OnClick="menuTher_Click" Icon="BookEdit">
                </f:MenuButton>
                <f:MenuButton ID="btncadther" runat="server" Text="热处理CAD" OnClick="btncadther_Click" Icon="ChartBarError" OnClientClick="settabindex2()">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow1" />
            </Listeners>
        </f:Menu>
    </form>
    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>

    <script>

        var CADPhotoTabStripID = '<%= CADPhotoTabStrip.ClientID %>';
        var GrapkTabStripID = '<%= GrapkTabStrip.ClientID %>';
        var TabStrip1ID = '<%= TabStrip1.ClientID %>';
        var TabStrip2ID = '<%= TabStrip2.ClientID %>';
        var isShow = true;

        function mxdtool() {

            var MxDrawX = document.getElementById("MxDrawXCtrl");
            MxDrawX.ShowToolBar("常用工具", isShow);

            MxDrawX.HideToolBarControl("常用工具", "新建,打开mxg文件,保存,保存为mxg文件,打印图形", true, true);

            isShow = !isShow;
        }
        function mxdtool1() {

            var MxDrawX = document.getElementById("MxDrawXCtrl1");
            MxDrawX.ShowToolBar("常用工具", isShow);

            MxDrawX.HideToolBarControl("常用工具", "新建,打开mxg文件,保存,保存为mxg文件,打印图形", true, true);

            isShow = !isShow;
        }
        function mxdtool2() {

            var MxDrawX = document.getElementById("MxDrawXCtrl2");
            MxDrawX.ShowToolBar("常用工具", isShow);

            MxDrawX.HideToolBarControl("常用工具", "新建,打开mxg文件,保存,保存为mxg文件,打印图形", true, true);

            isShow = !isShow;
        }
        function openwebcad(strwebfilepath) {

            var MxDrawX = document.getElementById("MxDrawXCtrl");

            MxDrawX.OpenWebDwgFile(strwebfilepath);
        }
        function openwebcad1(strwebfilepath) {

            var MxDrawX = document.getElementById("MxDrawXCtrl1");

            MxDrawX.OpenWebDwgFile(strwebfilepath);
        }
        function openwebcad2(strwebfilepath) {

            var MxDrawX = document.getElementById("MxDrawXCtrl2");

            MxDrawX.OpenWebDwgFile(strwebfilepath);
        }
        //在加载文件前激活控件选项卡
        function settabindex() {
            F(GrapkTabStripID).setActiveTab(0, false);
            F(CADPhotoTabStripID).setActiveTab(2, false);
        }
        function settabindex1() {
            F(TabStrip1ID).setActiveTab(2, false);
        }
        function settabindex2() {
            F(TabStrip2ID).setActiveTab(2, false);
        }
    </script>
    <script>

        var grid1 = '<%= mainGrid.ClientID %>';

        function onRowExpanderExpand(event, rowId) {
            var grid = this, rowEl = grid.getRowEl(rowId), rowData = grid.getRowData(rowId);

            var tplEl = rowEl.find('.f-grid-rowexpander-details .f-grid-tpl');
            if (!tplEl.text()) {

                var dataUrl = '../craftdesign/mechanicalDesignManageGrid.ashx?id=' + rowId + '&name=' + encodeURIComponent(rowData.values['Name']); // 这里可传递行中任意数据（rowData）

                // 直接调用 jQuery 的 ajax 函数
                $.ajax({
                    dataType: 'json',
                    cache: false,
                    url: dataUrl,
                    method: 'GET',
                    success: function (data) {
                        // data: [["入学",62,72,47,55,55],["期中",69,71,70,45,53],["期末",40,55,64,64,79]]
                        var html = [];
                        html.push('<table class="mytable f-widget-header"><tr><th class="f-widget-header"></th><th class="f-widget-header">设计</th><th class="f-widget-header">审核</th><th class="f-widget-header">标准化</th><th class="f-widget-header">会签</th>');

                        for (var i = 0, count = data.length; i < count; i++) {
                            var item = data[i];

                            html.push('<tr>');


                            html.push('<td class="f-widget-content">' + item[0] + '</td>');

                            html.push('<td class="f-widget-content">' + item[1] + '</td>');

                            html.push('<td class="f-widget-content">' + item[2] + '</td>');

                            html.push('<td class="f-widget-content">' + item[3] + '</td>');

                            html.push('<td class="f-widget-content">' + item[4] + '</td>');



                            html.push('</tr>');
                        }

                        html.push('</table>');

                        tplEl.html(html.join(''));

                        rowExpandersDoLayout();
                    }
                });

            }
        }

        function onRowExpanderCollapse(event, rowId) {
            rowExpandersDoLayout();
        }

        // 重新布局表格（解决出现纵向滚动条时的布局问题）
        function rowExpandersDoLayout() {
            var grid1Cmp = F(grid1);
            grid1Cmp.doLayout();
        }

    </script>
    <script>

        var treeID = '<%= firsttree.ClientID %>';
        var treeID1 = '<%=Tree2.ClientID%>';
        var menuID = '<%= firstmenu.ClientID %>';
        var menuID1 = '<%= Menu1.ClientID %>';
        var pdnameID = '<%= pdname.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;
        var currentNodeId1;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;

            var tree = F(treeID);
            var nodeData = tree.getNodeData(currentNodeId);

            if (nodeData.attrs["nodemenu"] != 0) {
                F(menuID).show();
            }

            return false;
        }
        function onTreeNodeContextMenu2(event, nodeId) { }
        function onTreeNodeContextMenu1(event, nodeId) {
            currentNodeId1 = nodeId;

            var tree = F(treeID1);
            var nodeData = tree.getNodeData(currentNodeId1);

            if (nodeData.attrs["nodemenu"] != 0) {
                F(menuID1).show();
            }

            return false;
        }
        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(nodemenu) {

            var menu = F(menuID);
            $.each(menu.items, function (index, item) {

                if (nodemenu == 2) {
                    item.setDisabled(false);
                }
                else {
                    item.setDisabled(true);
                }
            });
        }
        function setMenuItemsDisabled1(nodemenu) {

            var menu = F(menuID1);
            $.each(menu.items, function (index, item) {

                if (nodemenu == 2) {
                    item.setDisabled(false);
                }
                else {
                    item.setDisabled(true);
                }
            });
        }
        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId);

                //alert((JSON.stringify(nodeData)));

                setMenuItemsDisabled(nodeData.attrs["nodemenu"]);
            }
        }
        function onMenuShow1() {
            if (currentNodeId1) {
                var tree = F(treeID1);
                var nodeData = tree.getNodeData(currentNodeId1);

                //alert((JSON.stringify(nodeData)));

                setMenuItemsDisabled1(nodeData.attrs["nodemenu"]);
            }
        }
        function ontxtBlur(newmenutxt) {
            F(pdnameID).setText(newmenutxt);
        }

    </script>


    <script src="../Scripts/e-smart-zoom-jquery.min.js"></script>

    <script type="text/javascript">

        var f_pdnameID = '<%= f_pdname.ClientID %>';
        var f_pdtypeID = '<%= f_pdtype.ClientID %>';
        var imgPhotoID = '<%= imgPhoto.ClientID %>';
        var imgPhotoID1 = '<%= Image1.ClientID %>';
        var imgPhotoID2 = '<%= Image2.ClientID %>';
        var zoomflag = 0;

        function imgPhotozoom() {

            if (zoomflag == 0) {
                $('#' + imgPhotoID).smartZoom();
                zoomflag = 1;
            }
        }

        F.ready(function () {

            //$('#' + imgPhotoID).smartZoom();
            //$('#' + imgPhotoID1).smartZoom();
            //$('#' + imgPhotoID2).smartZoom();
            $('#' + f_pdnameID + ' input').on('click', searchpd);

            function searchpd() {

                var strpdtypename = $('#' + f_pdtypeID + ' input').val();

                if (strpdtypename.trim().length == 0) { strpdtypename = "All"; }

                $.ajax({
                    type: "post",
                    url: "pdlist.ashx",
                    data: "productSearch=" + strpdtypename,
                    datatype: "json",
                    async: false,
                    success: function (msg, textstatus, xmlhttprequest) {
                        if (msg != null) {
                            var production = new Array();
                            production = msg.split(',');
                            $('#' + f_pdnameID + ' input').autocomplete(
                                {
                                    source: production,
                                    minChars: 1, //自动完成激活之前填入的最小字符
                                    width: 260,//提示的宽度，溢出隐藏
                                    scrollHeight: 300,   //提示的高度，溢出显示滚动条
                                    max: 50,//列表里的条目数
                                    matchContains: true, //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                                    autoFill: false,//自动填充
                                    // mustMatch: true, //只会允许匹配的结果出现在输入框
                                    //extraParams: {name: function () { return $('#txtSearchSupplierName').val(); } },
                                    //formatItem: function (data, i, total) {
                                    //    return data[0];
                                    //},
                                    //formatMatch: function (data, i, total) {
                                    //    return data[0];
                                    //},
                                    //formatResult: function (data) {
                                    //    return data[0];
                                    //}
                                });
                        }
                    },
                    error: function (errorinfo) {
                        alert("获取产品信息错误。");
                    }
                });
            }

        });

    </script>

</body>
</html>

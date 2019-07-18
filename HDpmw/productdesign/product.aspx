<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="HDpmw.productdesign.product" %>

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
            height: 390px;
            line-height: 390px;
            overflow: hidden;
        }

            .photo img {
                height: 390px;
                vertical-align: middle;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server" />
        <f:Panel ID="mainPanel" CssClass="blockpanel" Margin="3px" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region" MaxWidth="1960px" AutoScroll="true">
            <Items>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px" TitleAlign="Right"
                    Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="10px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>
                                <f:Button ID="btnAdd" Text="新增产品" Icon="Add" runat="server" OnClick="btnAdd_Click"></f:Button>
                                <f:Button ID="btnSave" Text="保存信息" Icon="SystemSaveNew" runat="server" OnClick="btnSave_Click" ValidateForms="neweditForm"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true"
                    Title="顶部面板" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Items>
                        <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                            BodyPadding="0" LabelAlign="Right" LabelWidth="72px">
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
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="360px"
                    Title="产品管理器" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
                    <Items>
                        <f:Tree ID="firsttree" IsFluid="true" CssClass="blockpanel" ShowHeader="false" ShowBorder="false"
                            EnableCollapse="true" Title="树控件" runat="server"
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
                                            BodyPadding="10px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Form BodyPadding="3px" ID="neweditForm" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                                    LabelAlign="Right" runat="server" Title="表单 1">
                                                    <Rows>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:TextBox ID="pdname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                                                </f:TextBox>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:TextBox ID="pdcode" Label="编码" Required="true" ShowRedStar="true" runat="server" Text="">
                                                                </f:TextBox>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="pdtype" Label="类型" Required="true" ShowRedStar="true" runat="server" EmptyText="请选择产品类型"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DatePicker ID="drawdate" Label="日期" Required="true" ShowRedStar="True" runat="server" EmptyText="请选择产品批准日期">
                                                                </f:DatePicker>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="designer" Label="设计" runat="server" AutoSelectFirstItem="false" EmptyText="请选择设计人员"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="checker" Label="核对" runat="server" AutoSelectFirstItem="false" EmptyText="请选择核对人员"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="stanarder" Label="标准" runat="server" AutoSelectFirstItem="false" EmptyText="请选择标准人员"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="drawer" Label="制图" runat="server" AutoSelectFirstItem="false" EmptyText="请选择制图人员"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:DropDownList ID="examiner" Label="审核" runat="server" AutoSelectFirstItem="false" EmptyText="请选择审核人员"></f:DropDownList>
                                                            </Items>
                                                        </f:FormRow>
                                                        <f:FormRow>
                                                            <Items>
                                                                <f:TextArea runat="server" ID="specifications" Label="<span>装配&nbsp;&nbsp;&nbsp;&nbsp;<br/>要求</span>" MinHeight="160px">
                                                                </f:TextArea>
                                                            </Items>
                                                        </f:FormRow>
                                                    </Rows>
                                                </f:Form>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel1" Title="产品图纸文档" BoxFlex="2" runat="server" AutoScroll="true"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
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
                                                                        <f:FileUpload runat="server" ID="filePhoto" ShowRedStar="false" ShowEmptyLabel="true"
                                                                            ButtonText="上传零件图片" ButtonOnly="true" Required="false" ButtonIconFont="_Upload"
                                                                            AutoPostBack="true" OnFileSelected="filePhoto_FileSelected">
                                                                        </f:FileUpload>
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
                                                                    <f:Button ID="btnupload" Text="上传零件CAD图纸" IconFont="_Upload" runat="server" OnClientClick="upload_click()" EnablePostBack="false"></f:Button>
                                                                    <f:FileUpload runat="server" ID="filemxcad" ShowRedStar="false" ShowEmptyLabel="true" LabelWidth="0px"
                                                                        ButtonText="" ButtonOnly="true" Required="false" Hidden="true"
                                                                        AutoPostBack="true" OnFileSelected="filemxcad_FileSelected">
                                                                    </f:FileUpload>
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
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar8" runat="server">
                                            <Items>
                                                <f:DropDownList ID="f1_pdtype" Label="类型" runat="server" AutoSelectFirstItem="true" LabelWidth="63px"></f:DropDownList>
                                                <f:TextBox ID="f1_pdname" runat="server" Label="名称" Text="" LabelWidth="63px"></f:TextBox>
                                                <f:Button ID="btnFind_1" Text="查询" Icon="SystemSearch" OnClick="btnFind_1_Click" runat="server"></f:Button>                                               
                                                <f:Button ID="btnLookup_1" Text="查看" Icon="ApplicationSideTree" OnClick="btnLookup_1_Click" runat="server"></f:Button>
                                                <f:Button ID="btnClone_1" Text="克隆" Icon="DatabaseCopy" OnClick="btnClone_1_Click" runat="server"></f:Button>
                                                <f:Button ID="btnDelete_1" Text="删除" Icon="PackageDelete" OnClick="btnDelete_1_Click" runat="server"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:Panel ID="Panel10" Title="产品库" runat="server" RegionPosition="Left" Width="600px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Grid ID="Grid_1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="pdname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID,pdname" BoxFlex="1"
                                                    EnableRowClickEvent="true" OnRowClick="Grid_1_RowClick"
                                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid_1_RowDoubleClick">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="pdname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="pdname" />
                                                        <f:BoundField Width="120px" DataField="pdcode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="63px" DataField="pdtype" HeaderText="类型" TextAlign="Center" SortField="pdtype" />
                                                        <f:BoundField DataField="drawdate" HeaderText="日期" TextAlign="Left" ExpandUnusedSpace="true" DataFormatString="{0:yyyy-MM-dd}"/>
                                                    </Columns>
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage_1" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="PrePage_1" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage5_Click">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage_1" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage_1" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="LastPage_1" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="GoPage_1" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage5_Click">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber_1" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber5_Click">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize_1" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber_1" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber5_Click">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage_1" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill5" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt_1" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel11" Title="产品库总成明细" runat="server" AutoScroll="true" RegionPosition="Center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Grid ID="Grid_11" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" AllowPaging="false"
                                                    SortField="composename" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="composename" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="composename" />
                                                        <f:BoundField Width="120px" DataField="composecode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="63px" DataField="pdnumber" HeaderText="数量" TextAlign="Center" />
                                                        <f:BoundField Width="81px" DataField="pptype" HeaderText="类型库" TextAlign="Center" SortField="pptype" />
                                                        <f:BoundField DataField="remarks" HeaderText="备注" TextAlign="Left" ExpandUnusedSpace="true" />
                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="元零件库" BodyPadding="0px" runat="server" Icon="RubyPut" Layout="Region">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar5" runat="server">
                                            <Items>
                                                <f:DropDownList ID="f2_mainclass" Label="主类" runat="server" AutoSelectFirstItem="true" LabelWidth="63px" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="f2_mainclass_SelectedIndexChanged"></f:DropDownList>
                                                <f:DropDownList ID="f2_subclass" Label="子类" runat="server" AutoSelectFirstItem="true" LabelWidth="63px"></f:DropDownList>
                                                <f:TextBox ID="f2_mname" runat="server" Label="名称" Text="" LabelWidth="63px"></f:TextBox>
                                                <f:Button ID="btnFind_2" Text="查询" Icon="SystemSearch" OnClick="btnFind_2_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:Panel ID="Panel9" Title="元零件特征库目录" runat="server" RegionPosition="Left" Width="270px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Tree ID="tree_2" IsFluid="true" CssClass="blockpanel" ShowHeader="false" ShowBorder="false"
                                                    EnableCollapse="true" Title="树控件" runat="server" BodyPadding="0px"
                                                    OnNodeCommand="tree_2_NodeCommand">
                                                </f:Tree>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel7" Title="元零件库" runat="server" RegionPosition="Left" Width="600px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Grid ID="Grid_2" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="mname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID,mtypeid" BoxFlex="1"
                                                    EnableRowClickEvent="true" OnRowClick="Grid_2_RowClick"
                                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid_2_RowDoubleClick">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="mname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="mname" />
                                                        <f:BoundField Width="120px" DataField="mcode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField DataField="drawdate" HeaderText="日期" TextAlign="Left" ExpandUnusedSpace="true" DataFormatString="{0:yyyy-MM-dd}" />
                                                    </Columns>
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage_2" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage2_Click">
                                                                </f:Button>
                                                                <f:Button ID="PrePage_2" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage2_Click">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage_2" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage_2" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage2_Click">
                                                                </f:Button>
                                                                <f:Button ID="LastPage_2" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage2_Click">
                                                                </f:Button>
                                                                <f:Button ID="GoPage_2" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage2_Click">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber_2" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber2_Click">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize_2" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber_2" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber2_Click">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage_2" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt_2" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel8" Title="元零件库零件特征信息" runat="server" AutoScroll="true" RegionPosition="Center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Grid ID="Grid_21" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" AllowPaging="false"
                                                    SortField="featurename" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="featurename" HeaderText="特征名" TextAlign="Left" HeaderTextAlign="Center" SortField="featurename" />
                                                        <f:BoundField DataField="featurevalue" HeaderText="特征值" TextAlign="Left" ExpandUnusedSpace="true" />
                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="标准件库" BodyPadding="0px" runat="server" Icon="ChartLineLink" Layout="Region">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar4" runat="server">
                                            <Items>
                                                <f:DropDownList ID="f3_stype" Label="类型" runat="server" AutoSelectFirstItem="true" LabelWidth="63px"></f:DropDownList>
                                                <f:TextBox ID="f3_sname" runat="server" Label="名称" Text="" LabelWidth="63px"></f:TextBox>
                                                <f:Button ID="btnFind_3" Text="查询" Icon="SystemSearch" OnClick="btnFind_3_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:Panel ID="Panel6" Title="标准件库" runat="server" RegionPosition="center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Grid ID="Grid_3" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="sname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1"
                                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid_3_RowDoubleClick">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="160px" DataField="sname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="sname" />
                                                        <f:BoundField Width="160px" DataField="scode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="160px" DataField="material" HeaderText="材料" TextAlign="Left" HeaderTextAlign="Center" SortField="material" />
                                                        <f:BoundField Width="160px" DataField="specification" HeaderText="型号规格" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField DataField="stype" HeaderText="类型" TextAlign="Left" ExpandUnusedSpace="true" SortField="stype" />
                                                    </Columns>
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage_3" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage3_Click">
                                                                </f:Button>
                                                                <f:Button ID="PrePage_3" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage3_Click">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage_3" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage_3" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage3_Click">
                                                                </f:Button>
                                                                <f:Button ID="LastPage_3" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage3_Click">
                                                                </f:Button>
                                                                <f:Button ID="GoPage_3" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage3_Click">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber_3" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber3_Click">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize_3" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber_3" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber3_Click">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage_3" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt_3" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="外购件库" BodyPadding="0px" runat="server" Icon="BugMagnify" Layout="Region">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <f:DropDownList ID="f4_otype" Label="类型" runat="server" AutoSelectFirstItem="true" LabelWidth="63px"></f:DropDownList>
                                                <f:TextBox ID="f4_oname" runat="server" Label="名称" Text="" LabelWidth="63px"></f:TextBox>
                                                <f:Button ID="btnFind_4" Text="查询" Icon="SystemSearch" OnClick="btnFind_4_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:Panel ID="Panel5" Title="外购件库" runat="server" RegionPosition="center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Grid ID="Grid_4" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="oname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1"
                                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid_4_RowDoubleClick">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="160px" DataField="oname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="oname" />
                                                        <f:BoundField Width="160px" DataField="ocode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="160px" DataField="specification" HeaderText="型号规格" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField DataField="otype" HeaderText="类型" TextAlign="Left" ExpandUnusedSpace="true" SortField="otype" />
                                                    </Columns>
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage_4" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage4_Click">
                                                                </f:Button>
                                                                <f:Button ID="PrePage_4" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage4_Click">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage_4" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage_4" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage4_Click">
                                                                </f:Button>
                                                                <f:Button ID="LastPage_4" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage4_Click">
                                                                </f:Button>
                                                                <f:Button ID="GoPage_4" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage4_Click">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber_4" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber4_Click">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize_4" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber_4" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber4_Click">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage_4" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt_4" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="元组件库" BodyPadding="0px" runat="server" Icon="Coins" Layout="Region">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <f:DropDownList ID="f5_comtype" Label="类型" runat="server" AutoSelectFirstItem="true" LabelWidth="63px"></f:DropDownList>
                                                <f:TextBox ID="f5_comname" runat="server" Label="名称" Text="" LabelWidth="63px"></f:TextBox>
                                                <f:Button ID="btnFind_5" Text="查询" Icon="SystemSearch" OnClick="btnFind_5_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:Panel ID="Panel2" Title="元组件库" runat="server" RegionPosition="Left" Width="600px" CssStyle="border-right:1px solid lightgray"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Layout="Fit">
                                            <Items>
                                                <f:Grid ID="Grid_5" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="comname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1"
                                                    EnableRowClickEvent="true" OnRowClick="Grid_5_RowClick"
                                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid_5_RowDoubleClick">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="comname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="comname" />
                                                        <f:BoundField Width="120px" DataField="comcode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="standards" HeaderText="型号规格" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="63px" DataField="comtype" HeaderText="类型" TextAlign="Center" SortField="comtype" />
                                                        <f:BoundField DataField="remarks" HeaderText="备注" TextAlign="Left" ExpandUnusedSpace="true" />
                                                    </Columns>
                                                    <Toolbars>
                                                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                                            <Items>
                                                                <f:Button ID="FirstPage_5" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="PrePage_5" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage5_Click">
                                                                </f:Button>
                                                                <f:TextBox ID="CurPage_5" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                                                </f:TextBox>
                                                                <f:Button ID="NextPage_5" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="LastPage_5" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage5_Click">
                                                                </f:Button>
                                                                <f:Button ID="GoPage_5" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage5_Click">
                                                                </f:Button>
                                                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                                                <f:Button ID="SubNumber_5" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber5_Click">
                                                                </f:Button>
                                                                <f:Label ID="GridPageSize_5" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                                                <f:Button ID="UpNumber_5" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber5_Click">
                                                                </f:Button>
                                                                <f:Button ID="TotalPage_5" runat="server" Hidden="true"></f:Button>
                                                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                                <f:Label ID="MemoTxt_5" Text="页码" runat="server"></f:Label>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                </f:Grid>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" Title="元组件库组件明细" runat="server" AutoScroll="true" RegionPosition="Center"
                                            BodyPadding="0px" ShowBorder="false" ShowHeader="false" EnableCollapse="true">
                                            <Items>
                                                <f:Grid ID="Grid_51" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" AllowPaging="false"
                                                    SortField="composename" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                                    DataKeyNames="ID" BoxFlex="1">
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="composename" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center" SortField="composename" />
                                                        <f:BoundField Width="120px" DataField="composecode" HeaderText="编码" TextAlign="Left" HeaderTextAlign="Center" />
                                                        <f:BoundField Width="63px" DataField="number" HeaderText="数量" TextAlign="Center" />
                                                        <f:BoundField Width="81px" DataField="type" HeaderText="类型库" TextAlign="Center" SortField="type" />
                                                        <f:BoundField DataField="specification" HeaderText="型号规格" TextAlign="Left" ExpandUnusedSpace="true" />
                                                    </Columns>
                                                </f:Grid>
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
                <f:MenuButton ID="btnmenuPADD" runat="server" Text="新增组成产品" OnClick="menuPADD_Click" Icon="VectorAdd">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuMAdd" runat="server" Text="新增组成元零件" OnClick="menuMAdd_Click" Icon="DriveAdd">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuSAdd" runat="server" Text="新增组成标准件" OnClick="menuSAdd_Click" Icon="CoinsAdd">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuBAdd" runat="server" Text="新增组成外购件" OnClick="menuBAdd_Click" Icon="LorryAdd">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuCAdd" runat="server" Text="新增组成元组件" OnClick="menuCAdd_Click" Icon="RubyAdd">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuMDelete" runat="server" Text="删除产品" OnClick="menuMDelete_Click" Icon="Delete" ConfirmText="确定删除当前产品？">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuCAD" runat="server" Text="CAD" OnClick="menuCAD_Click" OnClientClick="settabindex()" Icon="ChartLineLink">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuPMSBEdit" runat="server" Text="编辑组成件" OnClick="menuPMSBEdit_Click" Icon="ShapeSquareEdit">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuPMSBDelete" runat="server" Text="删除组成件" OnClick="menuPMSBDelete_Click" Icon="PackageDelete" ConfirmText="确定删除当前组成件？">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow" />
            </Listeners>
        </f:Menu>

        <f:Window ID="neweditWindow_5" Width="650px" Height="203px" Icon="TagBlue" Title="新增/编辑组装元组件" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm_5" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="pdnumber5" Label="数量" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="pptype5" Label="类型库" Text="元组件" Required="true" ShowRedStar="true" runat="server" Enabled="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="remarks5" Label="备注" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbar5" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_5" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_5_Click" ValidateForms="neweditForm_5">
                        </f:Button>
                        <f:TextBox ID="editID_5" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditWindow_4" Width="650px" Height="203px" Icon="TagBlue" Title="新增/编辑组装外购件" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm_4" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="pdnumber4" Label="数量" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="pptype4" Label="类型库" Text="外购件" Required="true" ShowRedStar="true" runat="server" Enabled="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="remarks4" Label="备注" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbar4" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_4" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_4_Click" ValidateForms="neweditForm_4">
                        </f:Button>
                        <f:TextBox ID="editID_4" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditWindow_3" Width="650px" Height="203px" Icon="TagBlue" Title="新增/编辑组装标准件" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm_3" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="pdnumber3" Label="数量" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="pptype3" Label="类型库" Text="标准件" Required="true" ShowRedStar="true" runat="server" Enabled="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="remarks3" Label="备注" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbar3" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_3" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_3_Click" ValidateForms="neweditForm_3">
                        </f:Button>
                        <f:TextBox ID="editID_3" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditWindow_2" Width="650px" Height="203px" Icon="TagBlue" Title="新增/编辑组装元零件" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm_2" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="pdnumber2" Label="数量" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="pptype2" Label="类型库" Text="元零件" Required="true" ShowRedStar="true" runat="server" Enabled="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="remarks2" Label="备注" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar6" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_2" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_2_Click" ValidateForms="neweditForm_2">
                        </f:Button>
                        <f:TextBox ID="editID_2" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditWindow_1" Width="650px" Height="203px" Icon="TagBlue" Title="新增/编辑组装产品" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm_1" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="pdnumber1" Label="数量" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="pptype1" Label="类型库" Text="元零件" Required="true" ShowRedStar="true" runat="server" Enabled="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="remarks1" Label="备注" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar7" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_1" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_1_Click" ValidateForms="neweditForm_1">
                        </f:Button>
                        <f:TextBox ID="editID_1" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

    </form>

    <script>

        var filemxcadID = '<%= filemxcad.ClientID %>';
        var CADPhotoTabStripID = '<%= CADPhotoTabStrip.ClientID %>';
        var GrapkTabStripID = '<%= GrapkTabStrip.ClientID %>';

        var isShow = true;

        function mxdtool() {

            var MxDrawX = document.getElementById("MxDrawXCtrl");
            MxDrawX.ShowToolBar("常用工具", isShow);

            MxDrawX.HideToolBarControl("常用工具", "新建,打开mxg文件,保存,保存为mxg文件,打印图形", true, true);

            isShow = !isShow;
        }

        function upload_click() {
            F(filemxcadID).el.find('.f-field-fileupload-fileinput').click();
        }

        function openwebcad(strwebfilepath) {

            var MxDrawX = document.getElementById("MxDrawXCtrl");
            
            MxDrawX.OpenWebDwgFile(strwebfilepath);
        }

        function clearcad() {

            var MxDrawX = document.getElementById("MxDrawXCtrl");

            MxDrawX.NewFile();
        }

        //在加载文件前激活控件选项卡
        function settabindex() {
            F(GrapkTabStripID).setActiveTab(0, false);
            F(CADPhotoTabStripID).setActiveTab(2, false);
        }

    </script>

    <script>

        var treeID = '<%= firsttree.ClientID %>';
        var menuID = '<%= firstmenu.ClientID %>';
        var pdnameID = '<%= pdname.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

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

        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(nodemenu) {

            var menu = F(menuID);
            $.each(menu.items, function (index, item) {

                if (nodemenu == 2 && (index == 0 || index == 1 || index == 2 || index == 3 || index == 4 || index == 6 || index == 7)) {
                    item.setDisabled(true);
                }
                else if (nodemenu == 1 && (index == 9 || index == 10)) {
                    item.setDisabled(true);
                }
                else {
                    item.setDisabled(false);
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

        function ontxtBlur(newmenutxt) {
            F(pdnameID).setText(newmenutxt);
        }

    </script>

    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/e-smart-zoom-jquery.min.js"></script>

    <script type="text/javascript">

        var f_pdnameID = '<%= f_pdname.ClientID %>';
        var f_pdtypeID = '<%= f_pdtype.ClientID %>';
        var imgPhotoID = '<%= imgPhoto.ClientID %>';
        var zoomflag = 0;

        function imgPhotozoom() {

            if (zoomflag == 0) {
                $('#' + imgPhotoID).smartZoom();
                zoomflag = 1;
            }
        }

        F.ready(function () {

            //$('#' + imgPhotoID).smartZoom();

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

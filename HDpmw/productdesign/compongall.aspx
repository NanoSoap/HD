<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compongall.aspx.cs" Inherits="HDpmw.productdesign.compongall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../res/third-party/jqueryuiautocomplete/jquery-ui.css" rel="stylesheet" />
    <title>权限分配</title>
    <style>

        .repeatnode .f-tree-cell-text {
            color: orangered;
            font-weight: bold;
        }

        .repeatnode .f-tree-folder {
            color: orangered;
        }

        .photo {
            height: 360px;
            line-height: 360px;
            overflow: hidden;
        }

            .photo img {
                height:360px;
                vertical-align: middle;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server" />
        <f:Panel ID="mainPanel" CssClass="blockpanel" Margin="3px" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px" TitleAlign="Right"
                    Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>
                                <f:Button ID="btnSave" Text="保存信息" Icon="SystemSaveNew" runat="server" OnClick="btnSave_Click" ValidateForms="neweditForm"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true" Height="50px"
                    Title="顶部面板" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Items>
                        <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                            BodyPadding="5 5 0 5" CssStyle="border:none;" LabelAlign="Right" LabelWidth="90px">
                            <Rows>
                                <f:FormRow>
                                    <Items>

                                        <f:TextBox ID="f_mname" runat="server" Label="组件名称" Text=""></f:TextBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>

                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Width="300px" Title="元组件库" ShowBorder="true" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                    <Items>
                        <f:Tree ID="firsttree" IsFluid="true" CssClass="blockpanel" ShowHeader="false" OnNodeCommand="firsttree_NodeCommand"
                            EnableCollapse="false" EnableCheckBox="false" ShowBorder="false" Title="树控件" runat="server">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>

                    </Items>

                </f:Panel>
                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Title="中间面板" Width="700px" ShowBorder="true" ShowHeader="false"
                    BodyPadding="0px">
                    <Items>
                        <f:Panel ID="Panel3" Title="组件基本信息" runat="server"
                            BodyPadding="10px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" Height="270px">
                            <Items>
                                <f:Form RegionPosition="Top" BodyPadding="0px" ID="neweditForm" LabelWidth="70px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                                    runat="server" Title="表单 1">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="comcode" Label="编码" Required="true" ShowRedStar="true" runat="server" Text="">
                                                </f:TextBox>
                                                <f:TextBox ID="comname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                                </f:TextBox>
                                                <f:DropDownList ID="designer" Label="设计" runat="server" AutoSelectFirstItem="false" EmptyText="请选择设计人员"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="checker" Label="核对" runat="server" AutoSelectFirstItem="false" EmptyText="请选择核对人员"></f:DropDownList>
                                                <f:DropDownList ID="stanarder" Label="标准" runat="server" AutoSelectFirstItem="false" EmptyText="请选择标准人员"></f:DropDownList>
                                                <f:DropDownList ID="drawer" Label="制图" runat="server" AutoSelectFirstItem="false" EmptyText="请选择制图人员"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="examiner" Label="审核" runat="server" AutoSelectFirstItem="false" EmptyText="请选择审核人员"></f:DropDownList>
                                                <f:DropDownList ID="comtype" Label="类型" runat="server" AutoSelectFirstItem="true"></f:DropDownList>
                                                <f:DatePicker ID="drawerdate" Label="日期" Required="false" ShowRedStar="true" runat="server" Text="">
                                                </f:DatePicker>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextArea runat="server" ID="specifications" Label="<span>技术&nbsp;&nbsp;&nbsp;&nbsp;<br/>要求</span>" MinHeight="100px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>

                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel1" Title="零件库" BoxFlex="1" runat="server"
                            BodyPadding="0px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" AutoScroll="true">
                            <Items>
                                <f:TabStrip ID="GrapkTabStrip" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
                                    AutoPostBack="true"
                                    ShowBorder="false" ActiveTabIndex="0" runat="server">
                                    <Tabs>
                                        <f:Tab Title="元零件" BodyPadding="2px" runat="server" Icon="Application">
                                            <Items>
                                                <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="adddate" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="mainGrid_Click"
                                                    DataKeyNames="pID,number,remarks" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar1" runat="server">
                                                            <Items>
                                                                <f:Button ID="btnAddcom1" Text="添加元零件" Icon="TabAdd" OnClick="btnAddcom1_Click" runat="server">
                                                                </f:Button>
                                                                <f:Button ID="btnSavecom1" Text="保存元零件" Icon="TableSave" OnClick="btnSavecom1_Click" runat="server">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="name" HeaderText="名称" TextAlign="Left" SortField="name" DataFormatString="{0}" />
                                                        <f:RenderField Width="100px" ColumnID="number" DataField="number"
                                                            ExpandUnusedSpace="true" HeaderText="数量">
                                                            <Editor>
                                                                <f:TextBox ID="txtcomnumber1" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:RenderField Width="100px" ColumnID="remarks" DataField="remarks"
                                                            ExpandUnusedSpace="true" HeaderText="备注">
                                                            <Editor>
                                                                <f:TextBox ID="txtremarks1" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Tab>
                                        <f:Tab Title="标准件" BodyPadding="2px" runat="server" Icon="ApplicationForm">
                                            <Items>
                                                <f:Grid ID="Grid1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="adddate" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_Click"
                                                    DataKeyNames="pID,number,remarks" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar3" runat="server">
                                                            <Items>
                                                                <f:Button ID="btnAddcom2" Text="添加标准件" Icon="TabAdd" OnClick="btnAddcom2_Click" runat="server">
                                                                </f:Button>
                                                                <f:Button ID="btnSavecom2" Text="保存标准件" Icon="TableSave" OnClick="btnSavecom2_Click" runat="server">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="name" HeaderText="名称" TextAlign="Left" SortField="name" DataFormatString="{0}" />
                                                        <f:RenderField Width="100px" ColumnID="number" DataField="number"
                                                            ExpandUnusedSpace="true" HeaderText="数量">
                                                            <Editor>
                                                                <f:TextBox ID="txtcomnumber2" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:RenderField Width="100px" ColumnID="remarks" DataField="remarks"
                                                            ExpandUnusedSpace="true" HeaderText="备注">
                                                            <Editor>
                                                                <f:TextBox ID="txtremarks2" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Tab>
                                        <f:Tab Title="外购件" BodyPadding="2px" runat="server" Icon="Outline">
                                            <Items>
                                                <f:Grid ID="Grid2" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                                    SortField="adddate" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_Click"
                                                    DataKeyNames="pID,number,remarks" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar4" runat="server">
                                                            <Items>
                                                                <f:Button ID="btnAddcom3" Text="添加外购件" Icon="TabAdd" OnClick="btnAddcom3_Click" runat="server">
                                                                </f:Button>
                                                                <f:Button ID="btnSavecom3" Text="保存外购件" Icon="TableSave" OnClick="btnSavecom3_Click" runat="server">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                                        <f:BoundField Width="120px" DataField="name" HeaderText="名称" TextAlign="Left" SortField="name" DataFormatString="{0}" />
                                                        <f:RenderField Width="100px" ColumnID="number" DataField="number"
                                                            ExpandUnusedSpace="true" HeaderText="数量">
                                                            <Editor>
                                                                <f:TextBox ID="txtcomnumber3" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:RenderField Width="100px" ColumnID="remarks" DataField="remarks"
                                                            ExpandUnusedSpace="true" HeaderText="备注">
                                                            <Editor>
                                                                <f:TextBox ID="txtremarks3" Required="true" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                    </Columns>
                                                </f:Grid>
                                            </Items>
                                        </f:Tab>
                                    </Tabs>
                                </f:TabStrip>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                    Title="组件图纸/图片/3D" ShowBorder="true" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                    <Items>
                        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
                            AutoPostBack="true" OnTabIndexChanged="GrapkTabStrip_TabIndexChanged"
                            ShowBorder="false" ActiveTabIndex="0" runat="server">
                            <Tabs>
                                <f:Tab Title="零件图纸" BodyPadding="5px" runat="server" Icon="Images">
                                    <Items>
                                        <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" BodyPadding="0px">
                                            <object classid="clsid:4D318A33-5A8E-11D0-873F-0000C06B6F77"
                                                id="DWGThumbnail" width="100%" height="510px">
                                            </object>
                                            <f:Button ID="Button1" Text="工具栏" IconFont="Bars" runat="server" OnClientClick="showall()" EnablePostBack="false"></f:Button>
                                        </f:ContentPanel>
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
                                        <f:Label ID="Label3" CssClass="highlight" Text="初始文本" runat="server" />
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Menu ID="Menu1" runat="server">
            <Items>

                <f:MenuButton ID="menuAdd" runat="server" Text="新增组件" OnClick="menuAdd_Click" Icon="Add">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuClone" runat="server" Text="克隆组件" OnClick="menuClone_Click" Icon="DatabaseCopy">
                </f:MenuButton>
                <f:MenuButton ID="btncadLookup" runat="server" Text="CAD" OnClick="cadLookup_Click" Icon="ChartLineLink" OnClientClick="settabindex()">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnDelete" runat="server" Text="删除" ConfirmText="确定删除当前类别？" OnClick="btnDelete_Click" Icon="Delete">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow" />
            </Listeners>
        </f:Menu>

        <f:Window ID="machinekittreewin" Width="300px" Height="600px" Icon="TagBlue" Title="类别新增界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true" AutoScroll="true">
            <Items>
                <f:Tree ID="machinekittree" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                    EnableCollapse="false" Title="树控件" ShowBorder="false" runat="server" EnableCheckBox="true" OnlyLeafCheck="true">
                    <Listeners>
                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                    </Listeners>
                </f:Tree>
            </Items>

            <Toolbars>
                <f:Toolbar ID="Toolbar6" Position="Top" runat="server">
                    <Items>

                        <f:Button ID="btnmachinesave" runat="server" Text="保存" Icon="SystemSave" OnClick="btnmachinesave_Click">
                        </f:Button>
                        <f:Button ID="btnmachinefind" runat="server" Text="查询" Icon="SystemSave" OnClick="btnmachinefind_Click">
                        </f:Button>
                        <f:TextBox ID="txtcomID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                        <f:TextBox ID="txtnumber" Label="编号" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
                <f:Toolbar Position="Top" runat="server">
                    <Items>
                        <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                            CssStyle="border:none;" LabelAlign="Right" LabelWidth="90px" Width="200px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                        <f:TextBox ID="txtmachinename" Label="名称" runat="server" Text="">
                        </f:TextBox>
                                        </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="windowmachInfo" Width="650px" Height="700px" Icon="TagBlue" Title="元零件信息"
            EnableMaximize="false" EnableCollapse="false" runat="server" EnableResize="false" Hidden="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
            <Items>
                <f:Form BodyPadding="3px" ID="Form2" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                    LabelAlign="Right" MaxWidth="720px" runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="mname" Label="名称" runat="server" Text=""></f:Label>
                                <f:Label ID="mcode" Label="编码" runat="server" Text=""></f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="drawdate" Label="日期" runat="server" Text=""></f:Label>
                                <f:Label ID="designer1" Label="设计" runat="server" Text=""></f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="checker1" Label="核对" runat="server" Text=""></f:Label>
                                <f:Label ID="stanarder1" Label="标准" runat="server" Text=""></f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="drawer1" Label="制图" runat="server" Text=""></f:Label>
                                <f:Label ID="examiner1" Label="审核" runat="server" Text=""></f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="specifications1" Label="<span>技术&nbsp;&nbsp;&nbsp;&nbsp;<br/>要求</span>" runat="server" Text=""></f:Label>

                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Items>
                <f:TabStrip ID="TabStrip2" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
                    AutoPostBack="true" OnTabIndexChanged="TabStrip2_TabIndexChanged"
                    ShowBorder="false" ActiveTabIndex="0" runat="server">
                    <Tabs>
                        <f:Tab Title="零件图纸" BodyPadding="5px" runat="server" Icon="Images">
                            <Items>
                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" BodyPadding="0px">
                                    <object classid="clsid:4D318A33-5A8E-11D0-873F-0000C06B6F77"
                                        id="DWGThumbnail1" width="100%" height="510px">
                                    </object>
                                </f:ContentPanel>
                            </Items>
                        </f:Tab>
                        <f:Tab Title="零件图片" BodyPadding="5px" runat="server" Icon="Photos">
                            <Items>
                                <f:SimpleForm RegionPosition="Center" ID="SimpleForm1" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                    <Items>
                                        <f:Image ID="Image2" ImageUrl="../resources/photo/法兰.png"
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
        </f:Window>
        <f:Window ID="standardindow" Width="650px" Height="700px" Icon="TagBlue" Title="元零件信息"
            EnableMaximize="false" EnableCollapse="false" runat="server" EnableResize="false" Hidden="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
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
        </f:Window>
        <f:Window ID="outbuyWindow" Width="650px" Height="700px" Icon="TagBlue" Title="元零件信息"
            EnableMaximize="false" EnableCollapse="false" runat="server" EnableResize="false" Hidden="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
            <Items>
                <f:Form BodyPadding="3px" ID="Form4" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
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
                <f:SimpleForm RegionPosition="Center" ID="SimpleForm4" IsFluid="true" CssClass="blockpanel" BodyPadding="6px" EnableCollapse="false"
                                    ShowBorder="false" Title="图片" ShowHeader="false" LabelWidth="0px" runat="server">
                                    <Items>
                                        <f:Image ID="Image3" ImageUrl="../resources/photo/法兰.png"
                                            CssClass="photo" ShowEmptyLabel="true" runat="server">
                                        </f:Image>
                                    </Items>
                                </f:SimpleForm>
            </Items>
        </f:Window>
    </form>
    <script>

        var filemxcadID = '<%= filemxcad.ClientID %>';
        var GrapkTabStripID = '<%= TabStrip1.ClientID %>';
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
        //在加载文件前激活控件选项卡
        function settabindex() {
            F(GrapkTabStripID).setActiveTab(2, false);
        }
    </script>
    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/e-smart-zoom-jquery.min.js"></script>
    <script type="text/javascript">
        var f_mnameID = '<%= f_mname.ClientID %>';
        var f_mtypenameID = '';
        var imgPhotoID = '<%= imgPhoto.ClientID %>';
        var btnAddcom1 = '<%= btnAddcom1.ClientID %>'; 
        var txtnumber = '<%= txtnumber.ClientID %>'; 
        var txtmachinename = '<%= txtmachinename.ClientID %>';
        var zoomflag = 0;
        function imgPhotozoom() {

            if (zoomflag == 0) {
                $('#' + imgPhotoID).smartZoom();
                zoomflag = 1;
            }
        }
        
        F.ready(function () {
            //$('#' + imgPhotoID).smartZoom();
            
                $.ajax({
                    type: "post",
                    url: "txtlist.ashx",
                    data: "machinekitSearch=" + 1,
                    datatype: "json",
                    async: false,
                    success: function (msg, textstatus, xmlhttprequest) {
                        if (msg != null) {
                            var machinekitor = new Array();
                            machinekitor = msg.split(',');
                            $('#' + txtmachinename + ' input').autocomplete(
                                {
                                    source: machinekitor,
                                    minChars: 1, //自动完成激活之前填入的最小字符
                                    width: 110,//提示的宽度，溢出隐藏
                                    scrollHeight: 200,   //提示的高度，溢出显示滚动条
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
                        alert("获取零件名称信息错误。");
                    }
                });
            
            $.ajax({
                type: "post",
                url: "comclist.ashx",
                data: "kidclassSearch=All",
                datatype: "json",
                async: false,
                success: function (msg, textstatus, xmlhttprequest) {
                    if (msg != null) {
                        var kidclass = new Array();
                        kidclass = msg.split(',');
                        $('#' + f_mnameID + ' input').autocomplete(
                            {
                                source: kidclass,
                                minChars: 1, //自动完成激活之前填入的最小字符
                                width: 260,//提示的宽度，溢出隐藏  
                                scrollHeight: 300,   //提示的高度，溢出显示滚动条   
                                max: 50,//列表里的条目数    
                                matchContains: true, //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示    
                                autoFill: false,//自动填充   
                                // mustMatch: true, //只会允许匹配的结果出现在输入框
                                //extraParams: { name: function () { return $('#txtSearchSupplierName').val(); } },
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
                    alert("获取组件信息错误。");
                }
            });

        });
        
    </script>
    <script>
        var treeID = '<%= firsttree.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var addid1 = '<%=menuAdd.ClientID%>';
        var delid = '<%=btnDelete.ClientID%>';
        var mnameID = '<%= comname.ClientID %>';
        var mainkid = '<%= btnmenuClone.ClientID %>';
        var btncadID = '<%= btncadLookup.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }
        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(isclick) {

            if (isclick == 0) {
                F(addid1).setDisabled(true);
                F(mainkid).setDisabled(true);
                F(btncadID).setDisabled(true);
            }
            if (isclick == 1) {
                F(addid1).setDisabled(false);
                F(mainkid).setDisabled(true);
                F(btncadID).setDisabled(true);
            }
            if (isclick == 2) {
                F(addid1).setDisabled(true);
                F(mainkid).setDisabled(false);
                F(btncadID).setDisabled(false);
            }
        }

        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId);
                if (nodeData.leaf) {
                    F(delid).setDisabled(false);
                } else {
                    F(delid).setDisabled(true);
                }
                setMenuItemsDisabled(nodeData.attrs["isclick"]);
            }
        }

        function onExpandNode() {
            if (currentNodeId) {
                alert(mainkid);
                F(mainkid).disabled;
            }
        }

        function onCollapseNode() {
            if (currentNodeId) {
                F(treeID).collapseNode(currentNodeId, true);
            }
        }
        function ontxtBlur(newmenutxt) {
            F(mnameID).setText(newmenutxt);
        }
    </script>
    <script lang="javascript" type="text/javascript">

        function preview() {
            history.go(0);
        }

    </script>
</body>
</html>

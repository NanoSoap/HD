<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thermalDesignManage.aspx.cs" Inherits="HDpmw.craftdesign.thermalDesignManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>热处理工艺管理</title>
    <link href="../res/third-party/jqueryuiautocomplete/jquery-ui.css" rel="stylesheet" />
    <style>
        .repeatnode .f-tree-cell-text {
            color: orangered;
            font-weight: bold;
        }

        .repeatnode .f-tree-folder {
            color: orangered;
        }

        .photo {
            height: 281px;
            line-height: 281px;
            overflow: hidden;
        }

            .photo img {
                height: 281px;
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
                    Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="6px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>
                                <f:Button ID="btnSave" Text="保存工艺信息" Icon="SystemSaveNew" runat="server" OnClick="btnSave_Click" ValidateForms="neweditForm"></f:Button>
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
                                        <f:TextBox ID="f_mtypename" runat="server" Label="分类" Text=""></f:TextBox>
                                        <f:TextBox ID="f_mname" runat="server" Label="零件" Text=""></f:TextBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:TextBox ID="cloneID" runat="server" Label="克隆ID" Text="" Hidden="true"></f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="260px"
                    Title="元零件库目录" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
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
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" AutoScroll="true" Layout="Region"
                    Title="中间面板" ShowBorder="true" ShowHeader="false" BodyPadding="0px">
                    <Items>
                        <f:Panel runat="server" RegionPosition="Center" Layout="Region" AutoScroll="true"
                            Title="右上面板" ShowBorder="false" ShowHeader="false" EnableCollapse="true" BodyPadding="0px">
                            <Items>
                                <f:Panel ID="Panel3" Title="工艺卡基本信息" runat="server" RegionPosition="Left" Width="530px"
                                    BodyPadding="5px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" CssStyle="border-right:1px solid lightgray">
                                    <Items>
                                        <f:Form BodyPadding="3px" ID="neweditForm" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                            LabelAlign="Right" MaxWidth="720px" runat="server" Title="表单 1">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextBox ID="tname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                                        </f:TextBox>
                                                        <f:TextBox ID="tcode" Label="编号" Required="true" ShowRedStar="true" runat="server" Text="">
                                                        </f:TextBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextBox ID="mtag" Label="材料" Required="true" ShowRedStar="true" runat="server" Text="">
                                                        </f:TextBox>
                                                        <f:NumberBox ID="kitweight" Label="重量" runat="server" Text="" DecimalPrecision="2" NoDecimal="false" NoNegative="true">
                                                        </f:NumberBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextBox ID="craftmethod" Label="路线" Required="true" ShowRedStar="true" runat="server" Text="">
                                                        </f:TextBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="designperson" Label="设计" runat="server" AutoSelectFirstItem="false" EmptyText="请选择设计人员"></f:DropDownList>
                                                        <f:DatePicker ID="designdate" Label="日期" runat="server" EmptyText="请选择工艺设计日期"></f:DatePicker>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="auditperson" Label="审核" runat="server" AutoSelectFirstItem="false" EmptyText="请选择审核人员"></f:DropDownList>
                                                        <f:DatePicker ID="auditdate" Label="日期" runat="server" EmptyText="请选择工艺审核日期"></f:DatePicker>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="normalperson" Label="标准化" runat="server" AutoSelectFirstItem="false" EmptyText="请选择标准化人员"></f:DropDownList>
                                                        <f:DatePicker ID="normaldate" Label="日期" runat="server" EmptyText="请选择工艺标准化日期"></f:DatePicker>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="approveperson" Label="批准" runat="server" AutoSelectFirstItem="false" EmptyText="请选择批准人员"></f:DropDownList>
                                                        <f:DatePicker ID="approvedate" Label="日期" runat="server" EmptyText="请选择工艺批准日期"></f:DatePicker>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                    </Items>
                                </f:Panel>
                                <f:Panel Title="热处理工艺曲线图" runat="server" AutoScroll="true" Layout="Fit" RegionPosition="Center"
                                    ShowBorder="false" ShowHeader="true">
                                    <Items>
                                        <f:TabStrip ID="GrapkTabStrip" IsFluid="true" CssClass="blockpanel"
                                            AutoPostBack="true" OnTabIndexChanged="GrapkTabStrip_TabIndexChanged"
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
                                                                id="MxDrawXCtrl" width="100%" height="339px">
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
                                <f:Panel Title="工艺卡技术要求" runat="server" AutoScroll="true" Layout="Fit" RegionPosition="Right" MinWidth="360px"
                                    ShowBorder="false" ShowHeader="true" CssStyle="border-left:1px solid lightgray">
                                    <Items>
                                        <f:Grid ID="secondGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                            SortField="iname" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                            DataKeyNames="ID,tid" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                            <Toolbars>
                                                <f:Toolbar ID="Toolbar3" runat="server">
                                                    <Items>
                                                        <f:Button ID="btniNew" Text="新增" Icon="SystemSave" OnClick="btniNew_Click" runat="server" Hidden="true"></f:Button>
                                                        <f:Button ID="btniEdit" Text="编辑" Icon="ApplicationEdit" OnClick="btniEdit_Click" runat="server" Hidden="true"></f:Button>
                                                        <f:Button ID="btniDelete" Text="删除" Icon="Delete" OnClick="btniDelete_Click" runat="server" ConfirmText="确定要删除当前热处理的技术要求吗？" Hidden="true"></f:Button>
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center"/>
                                                <f:BoundField Width="100px" DataField="iname" SortField="iname" HeaderText="名称" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                <f:BoundField Width="100px" DataField="ivalue" HeaderText="值" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                                <f:BoundField Width="100px" DataField="icheck" ExpandUnusedSpace="true" HeaderText="检验方法" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                            </Columns>
                                        </f:Grid>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel1" Title="工艺卡工序内容" BoxFlex="1" runat="server" AutoScroll="true" RegionPosition="Bottom" MinHeight="270px"
                            BodyPadding="0px" ShowBorder="false" ShowHeader="true" EnableCollapse="true" CssStyle="border-top:1px solid lightgray">
                            <Items>
                                <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" AllowPaging="false"
                                    DataKeyNames="ID,tid" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <f:Button ID="btnbNew" Text="新增" Icon="SystemSave" OnClick="btnbNew_Click" runat="server" Hidden="true"></f:Button>
                                                <f:Button ID="btnbEdit" Text="编辑" Icon="ApplicationEdit" OnClick="btnbEdit_Click" runat="server" Hidden="true"></f:Button>
                                                <f:Button ID="btnbDelete" Text="删除" Icon="Delete" OnClick="btnbDelete_Click" runat="server" ConfirmText="确定要删除当前热处理工序吗？" Hidden="true"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:BoundField Width="72px" DataField="batchnumber" HeaderText="工序号" TextAlign="Center"></f:BoundField>
                                        <f:BoundField DataField="batchtext" ExpandUnusedSpace="true" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
                                        <f:BoundField Width="160px" DataField="bdevice" HeaderText="设备" TextAlign="Left" HeaderTextAlign="Center"></f:BoundField>
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

        <f:Menu ID="firstmenu" runat="server">
            <Items>
                <f:MenuButton ID="btnmenuDelete" runat="server" Text="删除工艺" OnClick="menuDelete_Click" Icon="Delete">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuClone" runat="server" Text="克隆工艺" OnClick="menuClone_Click" Icon="DatabaseCopy">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuPaste" runat="server" Text="粘贴工艺" OnClick="menuPaste_Click" Icon="PastePlain" Enabled="false">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btncadLookup" runat="server" Text="CAD" OnClick="cadLookup_Click" Icon="ChartLineLink" OnClientClick="settabindex()">
                </f:MenuButton>
            </Items>
        </f:Menu>

        <f:Window ID="neweditWindow_batch" Width="720px" Height="369px" Icon="TagBlue" Title="新增/编辑热处理工序" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm__batch" LabelWidth="103px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:NumberBox ID="batchnumber" Label="工序号" Text="1" runat="server" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true"></f:NumberBox>
                                <f:TextBox ID="batchtext" Label="工序内容" Text="" Required="true" ShowRedStar="true" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:DropDownList ID="bdevice" Label="设备" runat="server" AutoSelectFirstItem="true"></f:DropDownList>
                                <f:TextBox ID="stove_code" Label="装炉方式" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="stovetemp" Label="装炉温度" Text="" runat="server"></f:TextBox>
                                <f:TextBox ID="heattemp" Label="加热温度" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="heattime" Label="升温时间" Text="" Required="true" ShowRedStar="true" runat="server"></f:TextBox>
                                <f:TextBox ID="keeptime" Label="保温时间" Text="" Required="true" ShowRedStar="true" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="coolmedia" Label="冷却介质" Text="" Required="true" ShowRedStar="true" runat="server"></f:TextBox>
                                <f:TextBox ID="cooltemp" Label="冷却温度" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="cooltime" Label="冷却时间(S)" Text="" Required="true" ShowRedStar="true" runat="server"></f:TextBox>
                                <f:TextBox ID="workhour" Label="工时(min)" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbarbatch" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_batch" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_batch_Click" ValidateForms="neweditForm__batch">
                        </f:Button>
                        <f:TextBox ID="editID_batch" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditWindow_indicator" Width="496px" Height="242px" Icon="TagBlue" Title="新增/编辑技术要求" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm__indicator" LabelWidth="87px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:DropDownList ID="iname" Label="技术指标" runat="server" AutoSelectFirstItem="true"></f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="ivalue" Label="指标值" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="icheck" Label="检验方法" Text="" runat="server"></f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar4" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave_indicator" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_indicator_Click" ValidateForms="neweditForm__indicator">
                        </f:Button>
                        <f:TextBox ID="editID_indicator" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

    </form>

    <%--<script src="../res/js/mxdrawcad.js"></script>--%>

    <script>

        var filemxcadID = '<%= filemxcad.ClientID %>';
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
            F(GrapkTabStripID).setActiveTab(2, false);
        }

    </script>

    <script>

        var treeID = '<%= firsttree.ClientID %>';
        var menuID = '<%= firstmenu.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;

            var tree = F(treeID);
            var nodeData = tree.getNodeData(currentNodeId);

            if (nodeData.attrs["nodemenu"] == 2) {
                F(menuID).show();
            }

            return false;
        }

    </script>

    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/e-smart-zoom-jquery.min.js"></script>

    <script type="text/javascript">

        var f_mnameID = '<%= f_mname.ClientID %>';
        var f_mtypenameID = '<%= f_mtypename.ClientID %>';
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

            $('#' + f_mnameID + ' input').on('click', searchmk);

            function searchmk() {

                var strmtypename = $('#' + f_mtypenameID + ' input').val();

                if (strmtypename.trim().length == 0) { strmtypename = "All"; }

                $.ajax({
                    type: "post",
                    url: "../productdesign/mklist.ashx",
                    data: "machinekitSearch=" + strmtypename,
                    datatype: "json",
                    async: false,
                    success: function (msg, textstatus, xmlhttprequest) {
                        if (msg != null) {
                            var machinekitor = new Array();
                            machinekitor = msg.split(',');
                            $('#' + f_mnameID + ' input').autocomplete(
                                {
                                    source: machinekitor,
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
                        alert("获取零件名称信息错误。");
                    }
                });

            }

            $.ajax({
                type: "post",
                url: "../productdesign/kclist.ashx",
                data: "kidclassSearch=All",
                datatype: "json",
                async: false,
                success: function (msg, textstatus, xmlhttprequest) {
                    if (msg != null) {
                        var kidclass = new Array();
                        kidclass = msg.split(',');
                        $('#' + f_mtypenameID + ' input').autocomplete(
                            {
                                source: kidclass,
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
                    alert("获取零件分类信息错误。");
                }
            });

        });

    </script>
</html>

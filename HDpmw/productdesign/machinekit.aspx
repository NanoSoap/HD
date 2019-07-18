<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machinekit.aspx.cs" Inherits="HDpmw.productdesign.machinekit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>元零件库</title>
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
                                <f:Button ID="btnSave" Text="保存信息" Icon="SystemSaveNew" runat="server" OnClick="btnSave_Click" ValidateForms="neweditForm"></f:Button>
                            </Items>

                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="false" EnableCollapse="true"
                    Title="顶部面板" ShowBorder="false" ShowHeader="false" BodyPadding="3px">
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
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="270px"
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
                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" AutoScroll="true"
                    MinWidth="720px" Title="中间面板" ShowBorder="true" ShowHeader="false" BodyPadding="0px">
                    <Items>
                        <f:Panel ID="Panel3" Title="零件基本信息" runat="server"
                            BodyPadding="10px" ShowBorder="false" ShowHeader="true" EnableCollapse="true">
                            <Items>
                                <f:Form BodyPadding="3px" ID="neweditForm" LabelWidth="63px" ShowBorder="false" ShowHeader="false"
                                    LabelAlign="Right" MaxWidth="720px" runat="server" Title="表单 1">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="mname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                                </f:TextBox>
                                                <f:TextBox ID="mcode" Label="编码" Required="true" ShowRedStar="true" runat="server" Text="">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="50% 50%">
                                            <Items>
                                                <f:DatePicker ID="drawdate" Label="日期" Required="true" ShowRedStar="True" runat="server" EmptyText="请选择零件批准日期">
                                                </f:DatePicker>
                                                <f:DropDownList ID="designer" Label="设计" runat="server" AutoSelectFirstItem="false" EmptyText="请选择设计人员"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="50% 50%">
                                            <Items>
                                                <f:DropDownList ID="checker" Label="核对" runat="server" AutoSelectFirstItem="false" EmptyText="请选择核对人员"></f:DropDownList>
                                                <f:DropDownList ID="stanarder" Label="标准" runat="server" AutoSelectFirstItem="false" EmptyText="请选择标准人员"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="50% 50%">
                                            <Items>
                                                <f:DropDownList ID="drawer" Label="制图" runat="server" AutoSelectFirstItem="false" EmptyText="请选择制图人员"></f:DropDownList>
                                                <f:DropDownList ID="examiner" Label="审核" runat="server" AutoSelectFirstItem="false" EmptyText="请选择审核人员"></f:DropDownList>
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
                        <f:Panel ID="Panel1" Title="零件特征信息表" BoxFlex="1" runat="server" AutoScroll="true"
                            BodyPadding="0px" ShowBorder="false" ShowHeader="true" EnableCollapse="true">
                            <Items>
                                <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                    SortField="featurename" SortDirection="ASC" AllowSorting="true" EnableHeaderMenuSort="true"
                                    DataKeyNames="featureid,pid,featurevalue" BoxFlex="1" AllowCellEditing="true" ClicksToEdit="1">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <f:Button ID="btnSavefeature" Text="保存特征" Icon="TableSave" OnClick="btnSavefeature_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField Width="51px" HeaderText="序号" TextAlign="Center" />
                                        <f:BoundField Width="120px" DataField="featurename" HeaderText="特征名" TextAlign="Left" SortField="featurename" DataFormatString="{0}" />
                                        <f:RenderField Width="100px" ColumnID="featurevalue" DataField="featurevalue"
                                            ExpandUnusedSpace="true" HeaderText="特征值">
                                            <Editor>
                                                <f:TextBox ID="txtfeaturevalue" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true" MinWidth="530px"
                    Title="零件图纸/图片/3D" ShowBorder="true" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                    <Items>
                        <f:TabStrip ID="GrapkTabStrip" IsFluid="true" CssClass="blockpanel" MinHeight="560px"
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

        <f:Menu ID="firstmenu" runat="server">
            <Items>
                <f:MenuButton ID="btnmenuAdd" runat="server" Text="新增零件" OnClick="menuAdd_Click" Icon="Add">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnmenuDelete" runat="server" Text="删除零件" OnClick="menuDelete_Click" Icon="Delete" ConfirmText="确定要删除当前零件？">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuClone" runat="server" Text="克隆零件" OnClick="menuClone_Click" Icon="DatabaseCopy">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuLookup" runat="server" Text="基本信息" OnClick="menuLookup_Click" Icon="ApplicationViewDetail">
                </f:MenuButton>
                <f:MenuButton ID="btncadLookup" runat="server" Text="CAD" OnClick="cadLookup_Click" Icon="ChartLineLink" OnClientClick="settabindex()">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow" />
            </Listeners>
        </f:Menu>

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
        var mnameID = '<%= mname.ClientID %>';
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

                if (nodemenu == 1 && (index == 2 || index == 3 || index == 4 || index == 5)) {
                    item.setDisabled(true);
                }
                else if (nodemenu == 2 && (index == 0)) {
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
            F(mnameID).setText(newmenutxt);
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
                    url: "mklist.ashx",
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
                url: "kclist.ashx",
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

</body>
</html>

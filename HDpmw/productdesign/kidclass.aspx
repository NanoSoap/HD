<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kidclass.aspx.cs" Inherits="HDpmw.productdesign.kidclass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../res/third-party/jqueryuiautocomplete/jquery-ui.css" rel="stylesheet" />
    <title>权限分配</title>
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

                                        <f:TextBox ID="f_classname" runat="server" Label="大类名称" Text=""></f:TextBox>
                                        <f:TextBox ID="f_mname" runat="server" Label="小类名称" Text=""></f:TextBox>
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
                    Width="500px" Title="元零件特征库" ShowBorder="true" ShowHeader="true" BodyPadding="10px" AutoScroll="true">
                    <Items>
                        <f:Tree ID="firsttree" IsFluid="true" CssClass="blockpanel" ShowHeader="false" OnNodeCommand="firsttree_NodeCommand"
                            EnableCollapse="false" EnableCheckBox="false" ShowBorder="false" Title="树控件" runat="server">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>

                    </Items>

                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                    Title="类别详细信息" ShowBorder="true" ShowHeader="true"
                    BodyPadding="10px">
                    <Items>
                        <f:Label ID="detailinfo" runat="server" Text="空白详细信息" EncodeText="false"></f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnAdd" runat="server" Text="新增大类" OnClick="btnAdd_Click" Icon="Add">
                    
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnAdd1" runat="server" Text="新增小类" OnClick="btnAdd_Click" Icon="Add">
                    
                </f:MenuButton>
                <f:MenuButton ID="btnEdit" runat="server" Text="编辑大类" OnClick="btnEdit_Click" Icon="ApplicationViewDetail">
                </f:MenuButton>
                <f:MenuButton ID="btnMpci" runat="server" Text="大类指标" OnClick="btnMpci_Click" Icon="BasketEdit">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnEdit1" runat="server" Text="编辑小类" OnClick="btnEdit_Click" Icon="ApplicationEdit">
                </f:MenuButton>
                <f:MenuButton ID="btnMpci1" runat="server" Text="小类指标" OnClick="btnMpci_Click" Icon="BookEdit">
                </f:MenuButton>
                <f:MenuSeparator runat="server" />
                <f:MenuButton ID="btnDelete" runat="server" Text="删除" ConfirmText="确定删除当前类别？" OnClick="btnDelete_Click" Icon="Delete">
                </f:MenuButton>
            </Items>
            <Listeners>
                <f:Listener Event="show" Handler="onMenuShow" />
            </Listeners>
        </f:Menu>
        <f:Window ID="mainkid" Width="650px" Height="320px" Icon="TagBlue" Title="类别新增界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true" AutoScroll="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm" LabelWidth="90px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="mainname" Label="类别名称" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true">
                                </f:TextBox>
                                <f:NumberBox ID="orderint" Label="序号" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="code" Label="编码" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true">
                                </f:TextBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="mainmpic">
                            <Items>
                                <f:TextBox ID="mpci" Label="特性指标" Required="false" ShowRedStar="false" runat="server" Text="">
                                </f:TextBox>
                                <f:Label runat="server" Text="以','分隔，例：材料,颜色,长度"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="dart">
                            <Items>
                            <f:CheckBoxList ID="dartmpci" ColumnNumber="6" Label="特性指标" runat="server">
                                </f:CheckBoxList>
                                </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbar" Position="Top" runat="server">
                    <Items>
                        <f:Button Hidden="true" runat="server" ID="testaa"></f:Button>
                        <f:Button ID="btnSave" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_Click" ValidateForms="neweditForm">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>

                        <f:TextBox ID="editID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="editwindow" Width="650px" Height="220px" Icon="TagBlue" Title="类别编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="Form2" LabelWidth="90px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="editmainname" Label="类别名称" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true">
                                </f:TextBox>
                                <f:NumberBox ID="editorder" Label="序号" NoDecimal="true" NoNegative="true"  Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="editcode" Label="编码" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true">
                                </f:TextBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave1" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave1_Click" ValidateForms="Form2">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>

                        <f:TextBox ID="editclassID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="mainmpicWindow" Width="650px" Height="500px" Icon="TagBlue" Title="类别编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" SortField="adddate" SortDirection="ASC"
                            DataKeyNames="ID,orderint" BoxFlex="1" AllowSorting="true" OnSort="mainGrid_Sort"
                            EnableHeaderMenuSort="true"
                            EnableRowDoubleClickEvent="true" >
                            <Columns>
                                <f:BoundField Width="72px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="100px" DataField="mpci" HeaderText="特性指标" TextAlign="Center" SortField="mpci" DataFormatString="{0}" />
                                <f:BoundField Width="120px" DataField="explain" HeaderText="说明" TextAlign="Center" />
                                <f:BoundField DataField="adddate" HeaderText="添加时间" TextAlign="Left" ExpandUnusedSpace="true" />
                            </Columns>
                        </f:Grid>
            </Items>
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
                                <f:Button ID="SubNumber" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click">
                                </f:Button>
                                <f:Label ID="GridPageSize" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                <f:Button ID="UpNumber" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click">
                                </f:Button>
                                <f:Button ID="TotalPage" runat="server" Hidden="true"></f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Label ID="MemoTxt" Text="页码" runat="server"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
            <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="addmainmpic" runat="server" Text="新增" Icon="SystemSave" OnClick="addmainmpic_Click">
                        </f:Button>
                        <f:Button ID="editmainmpic" runat="server" Text="修改" Icon="SystemSave" OnClick="editmainmpic_Click">
                        </f:Button>
                        <f:Button ID="deletemainmpic" runat="server" Text="删除" Icon="SystemSave" OnClick="deletemainmpic_Click">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                        </f:ToolbarSeparator>

                        <f:TextBox ID="mainmpicweditID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="addmainpicWin" Width="650px" Height="190px" Icon="TagBlue" Title="主类别特性指标列表" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="Form3" LabelWidth="90px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="mpciname" Label="指标名称" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true">
                                </f:TextBox>
                                <f:TextBox ID="explain" Label="说明" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar4" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSavemainpic" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSavemainpic_Click" ValidateForms="addmainpicWin">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server">
                        </f:ToolbarSeparator>
                        <f:TextBox ID="textSavemainpic" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="secondrecWin" Width="650px" Height="500px" Icon="TagBlue" Title="分类别特性指标列表" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Grid ID="Grid1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" SortField="adddate" SortDirection="ASC"
                            DataKeyNames="ID" BoxFlex="1" AllowSorting="true" OnSort="mainGrid_Sort1"
                            EnableHeaderMenuSort="true"
                            EnableRowDoubleClickEvent="true" >
                            <Columns>
                                <f:BoundField Width="72px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="100px" DataField="mpci" HeaderText="特性指标" TextAlign="Center" SortField="mpci" DataFormatString="{0}" />
                                
                                <f:BoundField DataField="adddate" HeaderText="添加时间" TextAlign="Left" ExpandUnusedSpace="true" />
                            </Columns>
                        </f:Grid>
            </Items>
            <Toolbars>

                        <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                            <Items>                               
                                <f:Button ID="FirstPage1" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click">
                                </f:Button>
                                <f:Button ID="PrePage1" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click">
                                </f:Button>
                                <f:TextBox ID="CurPage1" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                </f:TextBox>
                                <f:Button ID="NextPage1" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click">
                                </f:Button>
                                <f:Button ID="LastPage1" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click">
                                </f:Button>
                                <f:Button ID="GoPage1" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click">
                                </f:Button>
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Button ID="SubNumber1" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click">
                                </f:Button>
                                <f:Label ID="GridPageSize1" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                <f:Button ID="UpNumber1" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click">
                                </f:Button>
                                <f:Button ID="TotalPage1" runat="server" Hidden="true"></f:Button>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                <f:Label ID="MemoTxt1" Text="页码" runat="server"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
            <Toolbars>
                <f:Toolbar ID="Toolbar5" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="addsecondrec" runat="server" Text="新增" Icon="SystemSave" OnClick="addsecondrec_Click">
                        </f:Button>
                        <f:Button ID="deletesecondrec" runat="server" Text="删除" Icon="SystemSave" OnClick="deletesecondrec_Click">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator5" runat="server">
                        </f:ToolbarSeparator>

                        <f:TextBox ID="secondrecWineditID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="addsecondrecWin" Width="650px" Height="200px" Icon="TagBlue" Title="主类别特性指标列表" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true" AutoScroll="true">
            <Items>
                <f:Form BodyPadding="10px" ID="Form4" LabelWidth="90px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:CheckBoxList ID="secondrecmpci" ColumnNumber="6" Label="特性指标" runat="server">
                                </f:CheckBoxList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar6" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSavesecondrec" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSavesecondrec_Click">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator6" runat="server">
                        </f:ToolbarSeparator>
                        <f:TextBox ID="textSavesecondrec" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>
    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        var f_mnameID = '<%= f_mname.ClientID %>';
        var f_mtypenameID = '<%= f_classname.ClientID %>';

        F.ready(function () {
            $('#' + f_mnameID + ' input').on('click', searchmk);

            function searchmk() {

                var strmtypename = $('#' + f_mtypenameID + ' input').val();

                if (strmtypename.trim().length == 0) { strmtypename = "All"; }

                $.ajax({
                    type: "post",
                    url: "sklist.ashx",
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
                        alert("获取小分类信息错误。");
                    }
                });

            }
            $.ajax({
                type: "post",
                url: "mclist.ashx",
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
                    alert("获取大分类信息错误。");
                }
            });

        });

    </script>
    <script>
        var treeID = '<%= firsttree.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var addid = '<%=btnAdd.ClientID%>';
        var addid1 = '<%=btnAdd1.ClientID%>';
        var editid = '<%=btnEdit.ClientID%>';
        var mpciid = '<%=btnMpci.ClientID%>';
        var editid1 = '<%=btnEdit1.ClientID%>';
        var mpciid1 = '<%=btnMpci1.ClientID%>';
        var delid = '<%=btnDelete.ClientID%>';
        var mainkid = '<%=mainkid.ClientID%>';
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
                F(addid).setDisabled(false);
                F(addid1).setDisabled(true);
                F(editid).setDisabled(true);
                F(mpciid).setDisabled(true);
                F(editid1).setDisabled(true);
                F(mpciid1).setDisabled(true);
            }
            if (isclick == 1) {
                F(addid).setDisabled(true);
                F(addid1).setDisabled(false);
                F(editid).setDisabled(false);
                F(mpciid).setDisabled(false);
                F(editid1).setDisabled(true);
                F(mpciid1).setDisabled(true);
            }
            if (isclick == 2) {
                F(addid).setDisabled(true);
                F(addid1).setDisabled(true);
                F(editid).setDisabled(true);
                F(mpciid).setDisabled(true);
                F(editid1).setDisabled(false);
                F(mpciid1).setDisabled(false);
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
    </script>
    <script lang="javascript" type="text/javascript">
        
            function preview() {
                history.go(0);
            }
            
        </script>
</body>
</html>

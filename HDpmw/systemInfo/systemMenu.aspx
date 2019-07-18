<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="systemMenu.aspx.cs" Inherits="HDpmw.systemInfo.SystemMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统菜单</title>
    <style>
        .text-align-center input {
            text-align: center;
        }
    </style>
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
                                <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>
                                <f:Button ID="btnNew" Text="新增" Icon="Add" runat="server" OnClick="btnNew_Click"></f:Button>
                                <f:Button ID="btnEdit" Text="编辑" Icon="Pencil" runat="server" OnClick="btnEdit_Click"></f:Button>
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前菜单？" OnClick="btnDelete_Click"></f:Button>
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
                                        <f:TextBox ID="f_menuname" runat="server" Label="菜单名" Text=""></f:TextBox>
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
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" Width="736px"
                    Title="系统菜单列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px"  AutoScroll="true">
                    <Items>
                        <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" SortField="menuname" SortDirection="ASC"
                            DataKeyNames="ID,menuname" BoxFlex="1" AllowSorting="true"
                            EnableHeaderMenuSort="true" OnSort="mainGrid_Sort"
                            EnableRowClickEvent="true" OnRowClick="mainGrid_RowClick">
                            <Columns>
                                <f:BoundField Width="63px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="100px" DataField="menuparent" HeaderText="父菜单名" TextAlign="Center" SortField="menuparent" DataFormatString="{0}" />
                                <f:BoundField Width="120px" DataField="menuname" HeaderText="菜单名" TextAlign="Left" HeaderTextAlign ="Center" />
                                <f:BoundField Width="303px" DataField="nodeid" HeaderText="菜单ID" TextAlign="Left" DataFormatString="{0}" HeaderTextAlign ="Center"/>
                                <f:BoundField DataField="sortcode" HeaderText="排序号" TextAlign="Left" SortField="sortcode" ExpandUnusedSpace="true" />
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
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                    Title="系统菜单功能按钮列表" ShowBorder="true" ShowHeader="true" Layout="VBox"
                    BodyPadding="0px">
                    <Items>
                        <f:Grid ID="childGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" BoxFlex="1" AllowSorting="true" SortDirection="ASC" SortField="functionname"
                            DataKeyNames="ID,pid">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                                    <Items>
                                        <f:Button ID="btnNewChild" Text="新增" Icon="Add" runat="server" OnClick="btnNewChild_Click"></f:Button>
                                        <f:Button ID="btnEditChild" Text="编辑" Icon="Pencil" runat="server" OnClick="btnEditChild_Click"></f:Button>
                                        <f:Button ID="btnDeleteChild" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前菜单？" OnClick="btnDeleteChild_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>                               
                                <f:RowNumberField Width="72px" TextAlign="Center" HeaderText="序号"/>
                                <f:BoundField Width="100px" DataField="functionname" HeaderText="功能名" TextAlign="Center" DataFormatString="{0}" />
                                <f:BoundField DataField="buttonid" HeaderText="按钮ID" TextAlign="Left" ExpandUnusedSpace="true" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <f:Window ID="neweditWindow" Width="650px" Height="203px" Icon="TagBlue" Title="系统菜单新增编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm" LabelWidth="96px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="menuparent" Label="父菜单名" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                                <f:TextBox ID="menuname" Label="菜单名" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="nodeid" Label="菜单ID" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                                <f:TextBox ID="sortcode" Label="排序号" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="neweditToolbar" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSave" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_Click" ValidateForms="neweditForm">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnRefresh" runat="server" Text="刷新" Icon="PageRefresh" OnClick="btnRefresh_Click">
                        </f:Button>
                        <f:TextBox ID="editID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window ID="neweditchildWindow" Width="650px" Height="163px" Icon="TagBlue" Title="系统菜单功能按钮新增编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditchildForm" LabelWidth="72px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="functionname" Label="功能名" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                                <f:TextBox ID="buttonid" Label="按钮ID" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btnSaveChild" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSaveChild_Click" ValidateForms="neweditchildForm">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnRefreshChild" runat="server" Text="刷新" Icon="PageRefresh" OnClick="btnRefreshChild_Click">
                        </f:Button>
                        <f:TextBox ID="editchildID" Label="可编辑childID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

    </form>
</body>
</html>

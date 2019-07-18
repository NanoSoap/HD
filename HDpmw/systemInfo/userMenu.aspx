<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userMenu.aspx.cs" Inherits="HDpmw.systemInfo.UserMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限分配</title>
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
                                <f:Button ID="btnSave" Text="保存" Icon="SystemSaveNew" runat="server" OnClick="btnSave_Click"></f:Button>
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
                                       
                                        <f:TextBox ID="f_username" runat="server" Label="账号" Text=""></f:TextBox>
                                        <f:TextBox ID="f_address" runat="server" Label="地址" Text=""></f:TextBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="510px"
                    Title="用户信息列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
                    <Items>
                        <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="true" SortField="username" SortDirection="ASC"
                            DataKeyNames="ID,username" BoxFlex="1" AllowSorting="true"
                            EnableHeaderMenuSort="true" OnSort="mainGrid_Sort"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="mainGrid_RowDoubleClick">
                            <Columns>
                                <f:BoundField Width="72px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="100px" DataField="fullname" HeaderText="姓名" TextAlign="Center" SortField="fullname" DataFormatString="{0}" />
                                <f:BoundField Width="100px" DataField="username" HeaderText="账号" TextAlign="Center" SortField="username" DataFormatString="{0}" />
                                <f:BoundField DataField="telephone" HeaderText="电话" TextAlign="Left" ExpandUnusedSpace="true" />
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
                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Width="270px" Title="用户可用菜单信息" ShowBorder="true" ShowHeader="true"
                    BodyPadding="10px">
                    <Items>
                        <f:Label ID="detailinfo" runat="server" Text="空白详细信息" EncodeText="false"></f:Label>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                    Title="系统菜单树列表" ShowBorder="true" ShowHeader="true" BodyPadding="10px" AutoScroll="true">
                    <Items>
                        <f:Tree ID="firsttree" IsFluid="true" CssClass="blockpanel" ShowHeader="false" 
                            EnableCollapse="false" EnableCheckBox="true" Title="树控件" runat="server" CascadeCheck="true"
                            OnNodeCheck="firsttree_NodeCheck">
                        </f:Tree>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

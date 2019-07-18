<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userRegister.aspx.cs" Inherits="HDpmw.systemInfo.userRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户注册</title>
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
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前用户？" OnClick="btnDelete_Click"></f:Button>
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
                <f:Panel runat="server" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Width="200px" Title="用户详细信息" ShowBorder="true" ShowHeader="true"
                    BodyPadding="10px">
                    <Items>
                        <f:Label ID="detailinfo" runat="server" Text="空白详细信息" EncodeText="false"></f:Label>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Center"
                    Title="注册用户列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
                    <Items>
                        <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" SortField="username" SortDirection="ASC"
                            DataKeyNames="ID,username" BoxFlex="1" AllowSorting="true"
                            EnableHeaderMenuSort="true" OnSort="mainGrid_Sort"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="mainGrid_RowDoubleClick">
                            <Columns>
                                <f:BoundField Width="72px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="100px" DataField="fullname" HeaderText="姓名" TextAlign="Center" SortField="fullname" DataFormatString="{0}" />
                                <f:BoundField Width="100px" DataField="department" HeaderText="部门" TextAlign="Center" SortField="fullname" DataFormatString="{0}" />
                                <f:BoundField Width="100px" DataField="staffno" HeaderText="工号" TextAlign="Center" SortField="fullname" DataFormatString="{0}" />
                                <f:BoundField Width="100px" DataField="username" HeaderText="账号" TextAlign="Center" SortField="username" DataFormatString="{0}" />
                                <f:BoundField Width="100px" DataField="password" HeaderText="密码" TextAlign="Center" />
                                <f:BoundField Width="120px" DataField="telephone" HeaderText="电话" TextAlign="Left" HeaderTextAlign="Center" />
                                <f:BoundField DataField="address" HeaderText="地址" TextAlign="Left" SortField="address" ExpandUnusedSpace="true" />
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
            </Items>
        </f:Panel>

        <f:Window ID="neweditWindow" Width="650px" Height="350px" Icon="TagBlue" Title="用户注册新增编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm" LabelWidth="72px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="fullname" Label="姓名" Required="true" ShowRedStar="true" runat="server" Text="" EnableBlurEvent="true" OnBlur="txt_nameBlur">
                                </f:TextBox>
                                 <f:DatePicker ID="birdate" Label="生日" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="username" Label="账号" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                                <f:TextBox ID="password" Label="密码" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:DropDownList ID="sex" runat="server" Label="性别" AutoSelectFirstItem="true">
                                </f:DropDownList>
                               <f:TextBox ID="telephone" Label="电话" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">

                            <Items>
                                <f:DropDownList ID="department" runat="server" Label="部门">
                                </f:DropDownList>
                                <f:TextBox ID="staffno" runat="server" Label="工号" Required="True" ShowRedStar="True">
                                </f:TextBox>
                            </Items>

                        </f:FormRow><f:FormRow >
                            <Items>                               
                                <f:TextBox ID="address" Label="地址" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox> 
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
                        <f:Button ID="btnRefresh" runat="server" Text="刷新" Icon="PageRefresh" OnClick="btnRefresh_Click">
                        </f:Button>
                        <f:TextBox ID="editID" Label="可编辑ID" runat="server" Text="" Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

    </form>
</body>
</html>

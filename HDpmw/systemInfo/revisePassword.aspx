<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="revisePassword.aspx.cs" Inherits="HDpmw.systemInfo.revisePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改密码</title>
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
                                <f:Button ID="btnSave" runat="server" Text="保存" Icon="SystemSave" OnClick="btnSave_Click" ValidateForms="neweditForm">
                                </f:Button>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:Button ID="btnRefresh" runat="server" Text="刷新" Icon="PageRefresh" OnClick="btnRefresh_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" RegionPosition="Top" RegionSplit="true" EnableCollapse="true"
                    Title="顶部面板" ShowBorder="true" ShowHeader="false" BodyPadding="3px">
                    <Items>
                        <f:Form BodyPadding="10px" ID="neweditForm" LabelWidth="72px" ShowBorder="false" ShowHeader="false" LabelAlign="Right" Width="669px"
                            runat="server" Title="表单 1">
                            <Rows>
                                <f:FormRow ColumnWidths="50% 50%">
                                    <Items>
                                        <f:TextBox ID="fullname" Label="姓名" Required="true" ShowRedStar="true" runat="server" Text="">
                                        </f:TextBox>
                                        <f:DatePicker ID="birdate" Label="生日" Required="true" ShowRedStar="true" runat="server" Text="">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="50% 50%">
                                    <Items>
                                        <f:TextBox ID="username" Label="账号" Required="true" ShowRedStar="true" runat="server" Text="" Enabled="false">
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
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="address" Label="地址" Required="true" ShowRedStar="true" runat="server" Text="">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xparams.aspx.cs" Inherits="HDpmw.systemInfo.Xparams" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>参数配置</title>
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
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前参数？" OnClick="btnDelete_Click"></f:Button>
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
                                        <f:TextBox ID="f_paramname" runat="server" Label="参数名" Text=""></f:TextBox>
                                        <f:TextBox ID="f_paramvalue" runat="server" Label="参数值" Text=""></f:TextBox>
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
                    Width="200px" Title="参数详细信息" ShowBorder="true" ShowHeader="true"
                    BodyPadding="10px">
                    <Items>
                        <f:Label ID="detailinfo" runat="server" Text="空白详细信息" EncodeText="false"></f:Label>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="gridPanel" RegionPosition="Center" AutoScroll="true"
                    Title="参数配置列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px">
                    <Items>
                        <f:Grid ID="mainGrid" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false" SortField="paramcode" SortDirection="ASC"
                            DataKeyNames="ID,paramname" BoxFlex="1" AllowSorting="true"
                            EnableHeaderMenuSort="true" OnSort="mainGrid_Sort"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="mainGrid_RowDoubleClick">
                            <Columns>
                                <f:BoundField Width="63px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                <f:BoundField Width="96px" DataField="paramcode" HeaderText="代码" TextAlign="Center" SortField="paramcode" DataFormatString="{0}" />
                                <f:BoundField Width="127px" DataField="paramname" HeaderText="名称" TextAlign="Left" SortField="paramname" HeaderTextAlign="Center" />
                                <f:BoundField DataField="paramvalue" HeaderText="参数值" TextAlign="Left" ExpandUnusedSpace="true" />
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

        <f:Window ID="neweditWindow" Width="650px" Height="203px" Icon="TagBlue" Title="参数配置新增编辑界面" Hidden="true"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="true">
            <Items>
                <f:Form BodyPadding="10px" ID="neweditForm" LabelWidth="72px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                    runat="server" Title="表单 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:TextBox ID="paramcode" Label="代码" Required="true" ShowRedStar="true" runat="server" Text="系统生成参数代码" Enabled="false">
                                </f:TextBox>
                                <f:TextBox ID="paramname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="paramvalue" Label="参数值" Required="true" ShowRedStar="true" runat="server" Text="">
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
    </form>
</body>
</html>

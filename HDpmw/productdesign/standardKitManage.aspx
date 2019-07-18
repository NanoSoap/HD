<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="standardKitManage.aspx.cs" Inherits="HDpmw.productdesign.standardKitManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标准件库</title>
    <style>
        .text-align-center input {
            text-align: center;
        }

        .photo {
            height: 300px;
            line-height: 300px;
            overflow: hidden;
        }

            .photo img {
               
                height: 300px;
                vertical-align: middle;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server"></f:PageManager>
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
                                    <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前标准件？" OnClick="btnDelete_Click"></f:Button>
                                    <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                    <f:Button ID="btnPhoto" Text="查看图片" runat="server" OnClick="btnPhoto_Click"></f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Panel>
                    <f:Panel runat="server" RegionPosition="top" RegionSplit="true" EnableCollapse="true" Height="50px"
                        Title="顶部面板" ShowBorder="true" ShowHeader="false" BodyPadding="3px">
                        <Items>
                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                                BodyPadding="5 5 0 5" CssStyle="border:none;" LabelAlign="Right" LabelWidth="100px">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="Fsname" runat="server" Label="标准件名" Text=""></f:TextBox>
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
                    <f:Panel runat="server" RegionPosition="Center"
                        Title="标准件列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">
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
                        <Items>
                            <f:Grid ID="Grid1" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                EnableCheckBoxSelect="true" EnableMultiSelect="false" SortDirection="ASC" SortField="sname"
                                DataKeyNames="ID" BoxFlex="1" AllowSorting="true"
                                EnableHeaderMenuSort="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                EnableRowDoubleClickEvent="true" OnSort="Grid1_Sort">
                                <Columns>
                                    <f:BoundField Width="72px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                    <f:BoundField Width="200px" DataField="sname" HeaderText="名称" TextAlign="Center" SortField="sname" DataFormatString="{0}" />
                                    <f:BoundField Width="200px" DataField="scode" HeaderText="编码" TextAlign="Center" SortField="scode" DataFormatString="{0}" />
                                    <f:BoundField Width="200px" DataField="specification" HeaderText="规格" TextAlign="Center" SortField="specification" DataFormatString="{0}" />
                                    <f:BoundField Width="200px" DataField="material" HeaderText="材料" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                    <f:BoundField Width="200px" DataField="stype" HeaderText="类型" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                </Columns>
                            </f:Grid>

                        </Items>
                    </f:Panel>
                </Items>
            </f:Panel>

            <f:Window ID="neweditWindow" Width="750px" Height="650px" Icon="TagBlue" Title="标准件编辑" Hidden="true"
                EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
                IsModal="true">
                <Items>
                    <f:Panel runat="server" Layout="Region" ID="panel1" ShowHeader="false" ShowBorder="false">
                        <Items>
                            <f:Form RegionPosition="Top" BodyPadding="10px" ID="neweditForm" LabelWidth="80px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                                runat="server" Title="表单 1">
                                <Rows>
                                    <f:FormRow >
                                        <Items>
                                            <f:TextBox ID="sname" Label="名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="50% 50%">
                                        <Items>
                                            <f:TextBox ID="scode" runat="server" Label="编码" Required="True" ShowRedStar="True">
                                            </f:TextBox>
                                            <f:DropDownList ID="stype" Label="类型" Required="true" ShowRedStar="true" runat="server" AutoSelectFirstItem="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="50% 50%">
                                        <Items>
                                            <f:TextBox ID="specification" Label="规格" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>
                                            <f:TextBox ID="material" runat="server" Label="材料" Required="True" ShowRedStar="True">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>

                                        <Items>
                                        </Items>

                                    </f:FormRow>


                                </Rows>

                            </f:Form>
                            <f:SimpleForm RegionPosition="Center" ID="SimpleForm1" IsFluid="true" CssClass="blockpanel" BodyPadding="10px" EnableCollapse="false"
                                ShowBorder="true" Title="标准件图片" ShowHeader="true" runat="server">
                                <Items>
                                    <f:Image ID="imgPhoto" CssClass="photo" ImageUrl="~/res/images/blank.png" ShowEmptyLabel="true" runat="server">
                                    </f:Image>
                                    <f:FileUpload runat="server" ID="filePhoto" ShowRedStar="false" ShowEmptyLabel="true"
                                        ButtonText="上传图片" ButtonOnly="true" Required="false" ButtonIcon="ImageAdd"
                                        AutoPostBack="true" OnFileSelected="filePhoto_FileSelected">
                                    </f:FileUpload>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:Panel>
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
                            </f:TextBox> <f:TextBox ID="str_filename" Label="文件名" runat="server" Text="" Hidden="true">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Window>
            <f:Window ID="photoWindow" Width="800px" Height="800px" Icon="TagBlue" Title="标准件图片" Hidden="true"
                EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
                IsModal="true">
                <Items>


                    <f:Label runat="server" ID="imageText" EncodeText="false"></f:Label>
                </Items>


            </f:Window>

        </div>
    </form>
</body>
</html>

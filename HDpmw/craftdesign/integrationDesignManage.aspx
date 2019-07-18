<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="integrationDesignManage.aspx.cs" Inherits="HDpmw.craftdesign.integrationDesignManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta name="sourcefiles" content="~/craftdesign/mechanicalDesignManageGrid.ashx" />--%>
    <link href="../res/third-party/jqueryuiautocomplete/jquery-ui.css" rel="stylesheet" />
    <title>机加工工艺管理</title>

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

        .mytable {
            border-width: 1px;
            border-style: solid;
            border-collapse: separate;
            border-spacing: 0;
            border-bottom-width: 0;
            border-right-width: 0;
            text-align: center;
        }

            .mytable th,
            .mytable td {
                padding: 5px;
                text-align: center;
                border-bottom-width: 1px;
                border-bottom-style: solid;
                border-right-width: 1px;
                border-right-style: solid;
                width: 200px;
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
                        Title="顶部工具栏" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                <Items>
                                    <f:Button ID="btnFind" Text="查询" Icon="SystemSearch" runat="server" OnClick="btnFind_Click"></f:Button>
                                    <f:Button ID="btnNew" Text="新增" Icon="Add" runat="server" OnClick="btnNew_Click"></f:Button>
                                    <f:Button ID="btnEdit" Text="编辑" Icon="Pencil" runat="server" OnClick="btnEdit_Click"></f:Button>
                                    <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" ConfirmText="确定删除当前项目？" OnClick="btnDelete_Click"></f:Button>
                                    <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                    <f:Button  Hidden="true" ID="btnBatch" Text="工艺详情" runat="server" OnClick="btnBatch_Click"></f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Panel>
                    <f:Panel runat="server" ID="gridPanel" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Width="270px"
                        Title="目录" ShowBorder="true" ShowHeader="true" BodyPadding="0px" AutoScroll="true">
                        <Items>
                            <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="false" TabPosition="Top"
                                EnableTabCloseMenu="false" ActiveTabIndex="0" runat="server">
                                <Tabs>
                                    <f:Tab Title="产品" Layout="Fit" runat="server">
                                        <Items>
                                            <f:Tree ID="firsttree" runat="server" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                EnableCollapse="true" Title="树控件" ShowBorder="false"
                                                OnNodeCommand="firsttree_NodeCommand">
                                                <Listeners>
                                                    <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                                </Listeners>

                                            </f:Tree>
                                        </Items>



                                    </f:Tab>
                                    <f:Tab Title="组件" Layout="Fit" runat="server">
                                        <Items>
                                            <f:Tree ID="secondtree" runat="server" IsFluid="true" CssClass="blockpanel" ShowHeader="false"
                                                EnableCollapse="true" Title="树控件" ShowBorder="false"
                                                OnNodeCommand="secondtree_NodeCommand">
                                                <Listeners>
                                                    <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu_2" />
                                                </Listeners>

                                            </f:Tree>
                                        </Items>
                                    </f:Tab>
                                </Tabs>
                            </f:TabStrip>
                        </Items>
                    </f:Panel>
                    <f:Panel runat="server" RegionPosition="top" RegionSplit="true" EnableCollapse="true" Height="50px"
                        Title="顶部面板" ShowBorder="true" ShowHeader="false" BodyPadding="3px">
                        <Items>
                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" Title="表单" runat="server"
                                BodyPadding="5 5 0 5" CssStyle="border:none;" LabelAlign="Right" LabelWidth="130px">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="Fkitname" runat="server" Label="产品/组件名称" Text=""></f:TextBox>
                                            <f:TextBox ID="Fcardname" runat="server" Label="工艺卡名称" Text=""></f:TextBox>
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
                    <f:Panel runat="server" RegionPosition="Center" RegionSplit="true" EnableCollapse="true"
                        ShowBorder="false" ShowHeader="false" BodyPadding="0px" Layout="Region">

                        <Items>
                            <f:Panel runat="server" EnableCollapse="true" RegionPercent="50%" RegionPosition="Top" RegionSplit="true"
                                Title="工艺卡列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" AutoScroll="true">

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
                                        EnableMultiSelect="false" SortDirection="ASC" SortField="iname"
                                        DataKeyNames="ID,iname" BoxFlex="1" AllowSorting="true" EnableCollapse="false"
                                        EnableHeaderMenuSort="true" ForceFit="true" CssClass="blockpanel" DataIDField="ID" EnableRowClickEvent="true"
                                        EnableRowDoubleClickEvent="true" OnSort="Grid1_Sort" OnRowClick="Grid1_RowClick">


                                        <Columns>
                                            <f:BoundField Width="65px" DataField="rowno" HeaderText="序号" TextAlign="Center" />
                                            <f:BoundField Width="200px" DataField="iname" HeaderText="工艺卡名称" TextAlign="Center" SortField="iname" DataFormatString="{0}" />
                                            <f:BoundField Width="200px" DataField="kitname" HeaderText="产品/组件名称" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                            <f:BoundField Width="180px" DataField="kitcode" HeaderText="产品/组件型号" TextAlign="Center" DataFormatString="{0}" HeaderTextAlign="Center" />
                                            <f:BoundField Width="200px" DataField="icode" HeaderText="工艺卡编号" TextAlign="Center" SortField="mcode" DataFormatString="{0}" />
                                            <%-- <f:BoundField Hidden="true" Width="200px" DataField="mtag" HeaderText="材料牌号" TextAlign="Center" SortField="mtag" DataFormatString="{0}" />
                                            <f:BoundField Hidden="true" Width="200px" DataField="rawtype" HeaderText="毛坯种类" TextAlign="Center" SortField="rawtype" DataFormatString="{0}" />
                                            <f:BoundField Hidden="true" Width="200px" DataField="rawsize" HeaderText="毛坯外形尺寸" TextAlign="Center" SortField="rawsize" DataFormatString="{0}" />
                                            <f:BoundField Hidden="true" Width="200px" DataField="nperraw" HeaderText="每毛坯可制件数" TextAlign="Center" SortField="nperraw" DataFormatString="{0}" />
                                            <f:BoundField Hidden="true" Width="200px" DataField="nperdesk" HeaderText="每台件数" TextAlign="Center" SortField="nperdesk" DataFormatString="{0}" />
                                            <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </f:TemplateField>--%>
                                        </Columns>
                                        <Listeners>
                                            <f:Listener Event="rowexpanderexpand" Handler="onRowExpanderExpand" />
                                            <f:Listener Event="rowexpandercollapse" Handler="onRowExpanderCollapse" />
                                        </Listeners>
                                    </f:Grid>
                                </Items>
                            </f:Panel>
                            <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionSplit="true"
                                Title="工序列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true">
                                <Toolbars>
                                    <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                        <Items>
                                            <f:Button ID="FirstPage1" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click1">
                                            </f:Button>
                                            <f:Button ID="PrePage1" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click1">
                                            </f:Button>
                                            <f:TextBox ID="CurPage1" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                            </f:TextBox>
                                            <f:Button ID="NextPage1" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click1">
                                            </f:Button>
                                            <f:Button ID="LastPage1" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click1">
                                            </f:Button>
                                            <f:Button ID="GoPage1" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click1">
                                            </f:Button>
                                            <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                            <f:Button ID="SubNumber1" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click1">
                                            </f:Button>
                                            <f:Label ID="GridPageSize1" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                            <f:Button ID="UpNumber1" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click1">
                                            </f:Button>
                                            <f:Button ID="TotalPage1" runat="server" Hidden="true"></f:Button>
                                            <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                            <f:Label ID="MemoTxt1" Text="页码" runat="server"></f:Label>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Tools>

                                    <f:Tool ID="toolnew" runat="server" IconFont="_Plus" ToolTip="新增" Text="新增" EnablePostBack="false" OnClick="toolnew_Click"></f:Tool>
                                    <f:Tool ID="toolreset" runat="server" IconFont="_Refresh" Text="重置" EnablePostBack="false"></f:Tool>
                                    <f:Tool ID="toolsave" runat="server" IconFont="_Save" ToolTip="保存" Text="保存" OnClick="toolsave_Click"></f:Tool>
                                </Tools>
                                <Items>
                                    <f:Grid ID="Grid2" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                        EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC"
                                        DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false" EnableRowClickEvent="true"
                                        EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID" OnPreDataBound="Grid2_PreDataBound"
                                        EnableRowDoubleClickEvent="true" OnSort="Grid2_Sort" AllowCellEditing="true" ClicksToEdit="1" OnRowClick="Grid2_RowClick">


                                        <Columns>
                                            <f:LinkButtonField ColumnID="Delete" Width="60px" EnablePostBack="false" HeaderText="删除" HeaderTextAlign="Center"
                                                Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField Width="55px" HeaderText="序号" TextAlign="Center">
                                            </f:RowNumberField>
                                            <f:RenderField Width="90px" ColumnID="batchnumber" DataField="batchnumber" HeaderText="工序号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                <Editor>
                                                    <f:TextBox runat="server"></f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="batchname" DataField="batchname" HeaderText="工序名称" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                                <Editor>
                                                    <f:DropDownList runat="server" ID="batchnameEditor"></f:DropDownList>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="batchtext" DataField="batchtext" HeaderText="工序内容" TextAlign="Left" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:TextArea runat="server"></f:TextArea>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="72px" ColumnID="workshop" DataField="workshop" HeaderText="车间" TextAlign="Center" SortField="workshop">
                                                <Editor>
                                                    <f:DropDownList runat="server" ID="workshopEditor"></f:DropDownList>
                                                </Editor>
                                            </f:RenderField>
                                            <%--<f:RenderField Width="72px" ColumnID="batchsession" DataField="batchsession" HeaderText="工段" TextAlign="Center" SortField="batchsession">
                                                <Editor>
                                                    <f:TextBox runat="server"></f:TextBox>
                                                </Editor>
                                            </f:RenderField>--%>
                                            <f:RenderField Width="72px" ColumnID="bdevice" DataField="bdevice" HeaderText="设备" TextAlign="Left" SortField="bdevice">
                                                <Editor>
                                                    <f:TextBox runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <%--<f:RenderField Width="200px" ColumnID="btool" DataField="btool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                                <Editor>
                                                    <f:TextBox runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>--%>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Panel>

                        </Items>

                    </f:Panel>
                    <f:Panel runat="server" EnableCollapse="true" AutoScroll="true" RegionPosition="Right" RegionPercent="45%"
                        Title="工步列表" ShowBorder="true" ShowHeader="true" BodyPadding="1px" CustomToolAhead="true" RegionSplit="true">
                        <Toolbars>
                            <f:Toolbar runat="server" Position="Bottom" CssStyle="padding-left:12px;">
                                <Items>
                                    <f:Button ID="FirstPage2" runat="server" Text="" Icon="RewindBlue" ToolTip="第一页" OnClick="FirstPage_Click2">
                                    </f:Button>
                                    <f:Button ID="PrePage2" runat="server" Text="" Icon="ReverseBlue" ToolTip="向前翻一页" OnClick="PrePage_Click2">
                                    </f:Button>
                                    <f:TextBox ID="CurPage2" runat="server" Text="" Width="87px" CssClass="text-align-center">
                                    </f:TextBox>
                                    <f:Button ID="NextPage2" runat="server" Text="" Icon="PlayBlue" ToolTip="向后翻一页" OnClick="NextPage_Click2">
                                    </f:Button>
                                    <f:Button ID="LastPage2" runat="server" Text="" Icon="ForwardBlue" ToolTip="最后一页" OnClick="LastPage_Click2">
                                    </f:Button>
                                    <f:Button ID="GoPage2" runat="server" Text="" Icon="EjectBlue" ToolTip="跳转到页" OnClick="GoPage_Click2">
                                    </f:Button>
                                    <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                    <f:Button ID="SubNumber2" runat="server" Text="" Icon="ControlStartBlue" ToolTip="减少表格每页显示数据条数" OnClick="SubNumber_Click2">
                                    </f:Button>
                                    <f:Label ID="GridPageSize2" Text="21" runat="server" Width="36px" CssStyle="padding-left:9px;"></f:Label>
                                    <f:Button ID="UpNumber2" runat="server" Text="" Icon="ControlEndBlue" ToolTip="增加表格每页显示数据条数" OnClick="UpNumber_Click2">
                                    </f:Button>
                                    <f:Button ID="TotalPage2" runat="server" Hidden="true"></f:Button>
                                    <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                    <f:Label ID="MemoTxt2" Text="页码" runat="server"></f:Label>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Tools>

                            <f:Tool ID="toolNewStep" runat="server" IconFont="_Plus" ToolTip="新增" Text="新增" EnablePostBack="false"></f:Tool>
                            <f:Tool ID="toolResetStep" runat="server" IconFont="_Refresh" Text="重置" EnablePostBack="false"></f:Tool>
                            <f:Tool ID="toolSaveStep" runat="server" IconFont="_Save" ToolTip="保存" Text="保存" OnClick="toolSaveStep_Click"></f:Tool>
                        </Tools>
                        <Items>
                            <f:Grid ID="Grid3" Title="表格" ShowBorder="false" ShowHeader="false" runat="server" PageSize="21" AllowPaging="false"
                                EnableCheckBoxSelect="false" EnableMultiSelect="false" SortDirection="ASC" SortField="systemdate"
                                DataKeyNames="ID" BoxFlex="1" AllowSorting="false" EnableCollapse="false"
                                EnableHeaderMenuSort="false" ForceFit="true" CssClass="blockpanel" DataIDField="ID" OnPreDataBound="Grid3_PreDataBound"
                                EnableRowDoubleClickEvent="true" AllowCellEditing="true" ClicksToEdit="1">


                                <Columns>
                                    <f:LinkButtonField ColumnID="Delete" Width="55px" EnablePostBack="false" HeaderText="删除" HeaderTextAlign="Center"
                                        Icon="Delete" TextAlign="Center" />
                                    <f:RowNumberField Width="60px" HeaderText="序号" TextAlign="Center">
                                    </f:RowNumberField>
                                    <f:RenderField Hidden="true" Width="60px" ColumnID="snumber" DataField="snumber" HeaderText="编号" TextAlign="Center" HeaderTextAlign="Center" SortField="batchnumber">
                                        <Editor>
                                            <f:TextBox runat="server"></f:TextBox>
                                        </Editor>
                                    </f:RenderField>

                                    <f:RenderField Width="300px" ColumnID="stext" DataField="stext" HeaderText="内容" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                        <Editor>
                                            <f:TextArea runat="server"></f:TextArea>
                                        </Editor>
                                    </f:RenderField>



                                    <f:RenderField Width="200px" ColumnID="stool" DataField="stool" HeaderText="工艺装备" TextAlign="Center" SortField="btool">
                                        <Editor>
                                            <f:TextBox runat="server">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>

                                </Columns>
                            </f:Grid>
                        </Items>
                    </f:Panel>
                </Items>
            </f:Panel>
            <f:Window ID="neweditWindow" Width="750px" Height="500px" Icon="TagBlue" Title="工艺卡编辑" Hidden="true"
                EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
                IsModal="true">
                <Items>
                    <f:Panel runat="server" Layout="Region" ID="panel1" ShowHeader="false" ShowBorder="false">
                        <Items>
                            <f:Form RegionPosition="Top" BodyPadding="10px" ID="neweditForm" LabelWidth="130px" ShowBorder="false" ShowHeader="false" LabelAlign="Right"
                                runat="server" Title="表单 1">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="mname" Label="工艺卡名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox Enabled="false" ID="kitname" Label="产品/组件名称" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>
                                            <f:TextBox Enabled="false" ID="kitcode" Label="产品/组件型号" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="50% 50%">
                                        <Items>
                                            <f:TextBox Enabled="false" ID="mcode" Label="工艺卡编号" Required="true" ShowRedStar="true" runat="server" Text="">
                                            </f:TextBox>
                                            <f:TextBox ID="mtag" Label="材料牌号" Required="true" ShowRedStar="true" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <%--<f:FormRow>

                                        <Items>
                                            <f:TextBox ID="rawtype" runat="server" Label="毛坯种类" Required="True" ShowRedStar="True">
                                            </f:TextBox>
                                            <f:TextBox ID="rawsize" runat="server" Label="毛坯外形尺寸" Required="True" ShowRedStar="True">
                                            </f:TextBox>
                                        </Items>

                                    </f:FormRow>--%>
                                    <%--<f:FormRow>

                                        <Items>
                                            <f:NumberBox NoDecimal="true" NoNegative="true" ID="nperraw" runat="server" Label="每毛坯可制件数" Required="True" ShowRedStar="True">
                                            </f:NumberBox>
                                            <f:NumberBox NoDecimal="true" NoNegative="true" ID="nperdesk" runat="server" Label="每台件数" Required="True" ShowRedStar="True">
                                            </f:NumberBox>
                                        </Items>

                                    </f:FormRow>--%>
                                    <f:FormRow>

                                        <Items>
                                            <f:DropDownList ID="designperson" runat="server" Label="设计人员">
                                            </f:DropDownList>
                                            <f:DatePicker ID="designdate" runat="server" Label="设计日期">
                                            </f:DatePicker>
                                        </Items>

                                    </f:FormRow>
                                    <f:FormRow>

                                        <Items>
                                            <f:DropDownList ID="auditperson" runat="server" Label="审核人员">
                                            </f:DropDownList>
                                            <f:DatePicker ID="auditdate" runat="server" Label="审核日期">
                                            </f:DatePicker>
                                        </Items>

                                    </f:FormRow>
                                    <f:FormRow>

                                        <Items>
                                            <f:DatePicker ID="normaldate" runat="server" Label="标准化日期">
                                            </f:DatePicker>
                                            <f:DatePicker ID="meetdate" runat="server" Label="会签日期">
                                            </f:DatePicker>
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
                    </f:Panel>



                </Items>



            </f:Window>
            <%--<f:Window ID="batchwindow" Width="750px" Height="650px" Icon="TagBlue" Title="工序编辑" Hidden="true"
                EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
                IsModal="true">
                <Items>
                    <f:Grid runat="server">
                    </f:Grid>
                </Items>
            </f:Window>--%>
        </div>
        <f:Menu ID="firstmenu" runat="server">
            <Items>

                <f:MenuButton ID="btnmenuClone" runat="server" Text="克隆工艺" OnClick="btnmenuClone_Click" Icon="DatabaseCopy">
                </f:MenuButton>
                <f:MenuButton ID="btnmenuPaste" runat="server" Text="粘贴工艺" OnClick="btnmenuPaste_Click" Icon="PastePlain" >
                </f:MenuButton>

            </Items>
        </f:Menu>
        <f:TextBox ID="strCloneID" runat="server" Hidden="true"></f:TextBox>
    </form>

    <script src="../res/third-party/jqueryuiautocomplete/jquery-ui.js" type="text/javascript"></script>
    <%-- 自动完成-零件名 --%>
    <%--<script type="text/javascript">

        var f_mnameID = '<%= f_mname.ClientID %>';

        F.ready(function () {



            $.ajax({
                type: "post",
                url: "/productdesign/pdlist.ashx",
                data: "productSearch=Clone",
                datatype: "json",
                async: false,
                success: function (msg, textstatus, xmlhttprequest) {
                    if (msg != null) {
                        var machinekitor = new Array();
                        machinekitor = msg.split(',');

                        $('#' + f_mnameID + ' input').autocomplete({
                            source: machinekitor,
                            open: function (event, ui) {
                                var inputEl = $(this);
                                // 当前输入框所在窗体的z-index
                                var wndZIndex = parseInt(inputEl.parents('.f-window').css('z-index'), 10);

                                // 设置autocomplete弹出层的z-index
                                inputEl.autocomplete('widget').css('z-index', wndZIndex + 1);
                            }
                        });
                    }
                },
                error: function (errorinfo) {
                    alert("获取产品/组件名称信息错误。");
                }
            });





        });

    </script>--%>
    <script>

        var grid1 = '<%= Grid1.ClientID %>';

        function onRowExpanderExpand(event, rowId) {
            var grid = this, rowEl = grid.getRowEl(rowId), rowData = grid.getRowData(rowId);

            var tplEl = rowEl.find('.f-grid-rowexpander-details .f-grid-tpl');
            if (!tplEl.text()) {

                var dataUrl = './mechanicalDesignManageGrid.ashx?id=' + rowId + '&name=' + encodeURIComponent(rowData.values['Name']); // 这里可传递行中任意数据（rowData）

                // 直接调用 jQuery 的 ajax 函数
                $.ajax({
                    dataType: 'json',
                    cache: false,
                    url: dataUrl,
                    method: 'GET',
                    success: function (data) {
                        // data: [["入学",62,72,47,55,55],["期中",69,71,70,45,53],["期末",40,55,64,64,79]]
                        var html = [];
                        html.push('<table class="mytable f-widget-header"><tr><th class="f-widget-header"></th><th class="f-widget-header">设计</th><th class="f-widget-header">审核</th><th class="f-widget-header">标准化</th><th class="f-widget-header">会签</th>');

                        for (var i = 0, count = data.length; i < count; i++) {
                            var item = data[i];

                            html.push('<tr>');


                            html.push('<td class="f-widget-content">' + item[0] + '</td>');

                            html.push('<td class="f-widget-content">' + item[1] + '</td>');

                            html.push('<td class="f-widget-content">' + item[2] + '</td>');

                            html.push('<td class="f-widget-content">' + item[3] + '</td>');

                            html.push('<td class="f-widget-content">' + item[4] + '</td>');



                            html.push('</tr>');
                        }

                        html.push('</table>');

                        tplEl.html(html.join(''));

                        rowExpandersDoLayout();
                    }
                });

            }
        }

        function onRowExpanderCollapse(event, rowId) {
            rowExpandersDoLayout();
        }

        // 重新布局表格（解决出现纵向滚动条时的布局问题）
        function rowExpandersDoLayout() {
            var grid1Cmp = F(grid1);
            grid1Cmp.doLayout();
        }

    </script>

    <script>
        var treeID1 = '<%= secondtree.ClientID %>';
        var menuID = '<%= firstmenu.ClientID %>';

        // 保存当前菜单对应的树节点ID
        var currentNodeId2;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu_2(event, nodeId) {
            currentNodeId2 = nodeId;

            var tree = F(treeID1);
            var nodeData = tree.getNodeData(currentNodeId2);

            if (nodeData.attrs["nodemenu"] == 2) {
                F(menuID).show();
            }

            return false;
        }
         设置所有菜单项的禁用状态
        //function setMenuItemsDisabled(isclick) {

        //    //if (isclick == 0) {
        //    //    F(addid1).setDisabled(true);
        //    //    F(mainkid).setDisabled(true);
        //    //    F(btncadID).setDisabled(true);
        //    //}
        //    //if (isclick == 1) {
        //    //    F(addid1).setDisabled(false);
        //    //    F(mainkid).setDisabled(true);
        //    //    F(btncadID).setDisabled(true);
        //    //}
        //    //if (isclick == 2) {
        //    //    F(addid1).setDisabled(true);
        //    //    F(mainkid).setDisabled(false);
        //    //    F(btncadID).setDisabled(false);
        //    //}
        //}

        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId2) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId2);

                //alert((JSON.stringify(nodeData)));

                //setMenuItemsDisabled(nodeData.attrs["nodemenu"]);
            }
        }

       
        //function ontxtBlur(newmenutxt) {
        //    F(mnameID).setText(newmenutxt);
        //}
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

        // 设置所有菜单项的禁用状态
        //function setMenuItemsDisabled(nodemenu) {

        //    var menu = F(menuID);
        //    //$.each(menu.items, function (index, item) {

        //    //    if (nodemenu == 1 && (index == 2 || index == 3 || index == 4 || index == 5)) {
        //    //        item.setDisabled(true);
        //    //    }
        //    //    else if (nodemenu == 2 && (index == 0)) {
        //    //        item.setDisabled(true);
        //    //    }
        //    //    else {
        //    //        item.setDisabled(false);
        //    //    }
        //    //});

        //}

        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId);

                //alert((JSON.stringify(nodeData)));

                //setMenuItemsDisabled(nodeData.attrs["nodemenu"]);
            }
        }

        //function ontxtBlur(newmenutxt) {
        //    F(mnameID).setText(newmenutxt);
        //}

    </script>
</body>

</html>

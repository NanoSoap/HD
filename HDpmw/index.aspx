<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HDpmw.index" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>江苏恒达机械制造信息一体化集成系统</title>
    <link href="~/res/css/index.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" Layout="Region" CssClass="mainpanel" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:ContentPanel ID="topPanel" CssClass="topregion bgpanel" RegionPosition="Top" ShowBorder="false" ShowHeader="false" EnableCollapse="true" runat="server">
                    <div id="header" class="f-widget-header f-mainheader">
                        <table>
                            <tr>
                                <td>
                                    <f:Button runat="server" CssClass="icononlyaction" ID="btnHomePage" ToolTip="官网首页" IconAlign="Top" IconUrl="res/images/logo.png"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false"
                                        OnClientClick="window.open('./UserLogin.aspx','_blank');">
                                        
                                    </f:Button>
                                    <a class="logo" href="./index.aspx" title="江苏恒达机械制造有限公司">江苏恒达机械制造信息一体化集成系统
                                    </a>
                                </td>
                                <td style="text-align: right;">
                                    <f:Button runat="server" CssClass="icontopaction themes" ID="btnThemeSelect" Text="界面风格" IconAlign="Top" IconFont="_Skin"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onThemeSelectClick" />
                                        </Listeners>
                                    </f:Button>
                                    <f:Button ID="user" runat="server" CssClass="userpicaction" Text="登陆人员" IconUrl="~/res/images/man.png" IconAlign="Left"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Menu runat="server">
                                            <f:MenuButton Text="用户信息" IconFont="_User" runat="server" OnClick="UserProfile_Click">
                                            </f:MenuButton>
                                            <f:MenuButton Text="切换账号" IconFont="_SignIn" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onSignInClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator runat="server"></f:MenuSeparator>
                                            <f:MenuButton Text="关闭系统" IconFont="_SignOut" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onSignOutClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                        </Menu>
                                    </f:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </f:ContentPanel>
                <f:Panel ID="leftPanel" CssClass="leftregion bgpanel" Width="220px" ShowHeader="false" Title="菜单"
                    EnableCollapse="true" Layout="Fit" RegionPosition="Left"
                    RegionSplit="true" RegionSplitWidth="3" RegionSplitIcon="false" runat="server">
                    <Items>
                        <f:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="treeMenu" EnableSingleClickExpand="true"
                            HeaderStyle="true" HideHScrollbar="true" HideVScrollbar="true" ExpanderToRight="true">
                            <%--<Nodes>
                                <f:TreeNode NodeID="baseInfo" Text="基础信息" Expanded="true" IconFont="InfoCircle">
                                    <f:TreeNode NodeID="baseInfo_entpriseInfo" Text="企业信息" NavigateUrl="baseInfo/entpriseInfo.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="baseInfo_barcode" Text="条码打印" NavigateUrl="baseInfo/barcode.aspx"></f:TreeNode>
                                </f:TreeNode>
                                <f:TreeNode NodeID="systemInfo" Text="系统信息" Expanded="true" IconFont="_Android">
                                    <f:TreeNode NodeID="systemInfo_userRegister" Text="用户注册" NavigateUrl="systemInfo/userRegister.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="systemInfo_revisePassword" Text="修改密码" NavigateUrl="systemInfo/revisePassword.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="systemInfo_userMenu" Text="权限分配" NavigateUrl="systemInfo/userMenu.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="systemInfo_systemMenu" Text="系统菜单" NavigateUrl="systemInfo/systemMenu.aspx"></f:TreeNode>
                                    <f:TreeNode NodeID="systemInfo_xparams" Text="参数配置" NavigateUrl="systemInfo/xparams.aspx"></f:TreeNode>
                                    <f:TreeNode Text="备份数据" NavigateUrl="#"></f:TreeNode>
                                </f:TreeNode>
                            </Nodes>--%>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel ID="mainPanel" CssClass="centerregion" ShowHeader="false" Layout="Fit" RegionPosition="Center" runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server" ShowInkBar="true">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="首页" IconFont="_Home" EnableIFrame="true" IFrameUrl="~/common/main.aspx" 
                                    BodyPadding="10px" AutoScroll="true"  runat="server">                                  
                                </f:Tab>
                            </Tabs>
                            <Tools>
                                <f:Tool runat="server" EnablePostBack="false" IconFont="_Fold" CssClass="icononlyaction" ToolTip="折叠/展开侧边栏" ID="toolCollapseSidebar">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onFoldClick" />
                                    </Listeners>
                                </f:Tool>
                                <f:Tool runat="server" EnablePostBack="false" IconFont="_Refresh" CssClass="tabtool" ToolTip="刷新本页" ID="toolRefresh">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onToolRefreshClick" />
                                    </Listeners>
                                </f:Tool>
                                <f:Tool runat="server"></f:Tool>
                            </Tools>
                        </f:TabStrip>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <f:Window ID="windowThemeRoller" Title="界面风格" Hidden="true" EnableIFrame="true" IFrameUrl="./common/themes.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1020px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>

        <f:Window ID="windowUserInfo" Width="650px" Height="350px" Icon="TagBlue" Title="用户信息"
            EnableMaximize="false" EnableCollapse="false" runat="server" EnableResize="false" Hidden="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
            <Toolbars>
                <f:Toolbar Position="Bottom" ToolbarAlign="Center" runat="server">
                    <Items>
                        <f:Button ID="btnRefresh" Text="刷新" IconFont="_Refresh" runat="server" EnablePostBack="false" OnClientClick="refresh();">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

        <f:Window runat="server" Hidden="true">
            <Items>
                <f:TextArea runat="server" ID="txtUserinfo"></f:TextArea>
            </Items>
        </f:Window>

    </form>
    <script>
        var treeMenuClientID = '<%= treeMenu.ClientID %>';
        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
        var windowThemeRollerClientID = '<%= windowThemeRoller.ClientID %>';
        var leftPanelClientID = '<%= leftPanel.ClientID %>';
        var toolCollapseSidebarClientID = '<%=toolCollapseSidebar.ClientID%>';

        var windowUserInfoClientID = '<%= windowUserInfo.ClientID %>';
        var txtUserinfoClientID = '<%= txtUserinfo.ClientID %>';

        function htmldecode(s){
                var div = document.createElement('div');
                div.innerHTML = s;
                return div.innerText || div.textContent;
            }
        //用户个人信息加载
        function refresh() {
            // 第一个参数: 遮罩层的透明度
            F(windowUserInfoClientID).showLoading(0.8);

            // 加个延迟，方便观察加载动画
            window.setTimeout(function () {
                F(windowUserInfoClientID).bodyEl.html(htmldecode(F(txtUserinfoClientID).getText()));
                F(windowUserInfoClientID).hideLoading();
            }, 1000);
        }

        // 点击主题仓库
        function onThemeSelectClick(event) {
            F(windowThemeRollerClientID).show();
        }

        function onSignOutClick(event) {
            window.opener = null; 
　　　　　  window.open(' ', '_self', ' '); 
　　　　　  window.close(); 
        }

        function onSignInClick(event) {
            window.location = "UserLogin.aspx";
        }

        // 折叠/展开侧边栏
        function onFoldClick(event) {

            var leftPanel = F(leftPanelClientID);
            var toolCollapseSidebar = F(toolCollapseSidebarClientID);
            var collapsed;

            var currentCollapsed = toolCollapseSidebar.iconFont === 'f-iconfont-unfold';
            if (F.isUND(collapsed)) {
                collapsed = !currentCollapsed;
            } else {
                if (currentCollapsed === collapsed) {
                    return;
                }
            }

            if (collapsed) {
                toolCollapseSidebar.setIconFont('f-iconfont-unfold');

                leftPanel.collapse();
            }
            else {
                toolCollapseSidebar.setIconFont('f-iconfont-fold');

                leftPanel.expand();
            }
        }

        // 添加示例标签页（通过href在树中查找）
        // href: 选项卡对应的网址
        // actived: 是否激活选项卡（默认为true）
        //快速导航
        function addExampleTabByHref(href,actived) {
           
            var leftPanel = F(leftPanelClientID);
            var treeMenu = leftPanel.getItem(0);

            alert(treeMenu);
                
            F.addMainTabByHref(F(mainTabStripClientID),treeMenu,href,actived);
        }

        function addExampleTab(tabOptions,actived) {         

            if (typeof (tabOptions) === 'string') {
                tabOptions = {
                    id: arguments[0],
                    iframeUrl: arguments[1],
                    title: arguments[2],
                    icon: arguments[3],
                    createToolbar: arguments[4],
                    refreshWhenExist: arguments[5],
                    iconFont: arguments[6]
                };
            }
            
			F.addMainTab(F(mainTabStripClientID), tabOptions, actived);
        }

        // 点击标题栏工具图标 - 刷新
        function onToolRefreshClick(event) {
            var mainTabStrip = F(mainTabStripClientID);

            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeWnd = activeTab.getIFrameWindow();
                iframeWnd.location.reload();
            }
        }

        // 页面控件初始化完毕后执行
        F.ready(function () {
            var treeMenu = F(treeMenuClientID);
            var mainTabStrip = F(mainTabStripClientID);
            if (!treeMenu) return;

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateHash: 切换Tab时，是否更新地址栏Hash值（默认值：true）
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame（默认值：false）
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame（默认值：false）
            // maxTabCount: 最大允许打开的选项卡数量
            // maxTabMessage: 超过最大允许打开选项卡数量时的提示信息
            F.initTreeTabStrip(treeMenu, mainTabStrip, {
                maxTabCount: 10,
                maxTabMessage: '请先关闭一些选项卡（最多允许打开 10 个）！'
            });            
        });

        //*****以下为侧边栏处理*****//     

        //*****以上为侧边栏处理*****//

    </script>
</body>
</html>

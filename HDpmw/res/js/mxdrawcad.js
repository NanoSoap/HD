
var isShow = false;
//var MxDrawX = document.all.item("MxDrawXCtrl");

//var MxDrawX = document.getElementById("MxDrawXCtrl");

//axMxDrawX1.EnableToolBarButton("打开dwg文件", false);

//MxDrawX.HideToolBarControl("常用工具", "保存,打开dwg文件", true, true);

//隐藏/显示工具栏
function HideToolbar() {
    //MxDrawX.ShowToolBar("常用工具", isShow);
    MxDrawX.ShowToolBar("绘图工具", isShow);
    MxDrawX.ShowToolBar("编辑工具", isShow);
    MxDrawX.ShowToolBar("特性", isShow);
    MxDrawX.ShowToolBar("ET工具", isShow);
    isShow = !isShow;
}

//隐藏/显示菜单栏
function HideMenuBar() {
    MxDrawX.ShowMenuBar(isShow);
    isShow = !isShow;
}

//隐藏/显示标尺栏
function HideRulerWindow() {
    MxDrawX.ShowRulerWindow(isShow);
    isShow = !isShow;
}

//隐藏/显示属性栏
function HidePropertyWindow() {
    MxDrawX.ShowPropertyWindow(isShow);
    isShow = !isShow;
}

//隐藏/显示命令栏
function HideCommandWindow() {
    MxDrawX.ShowCommandWindow(isShow);
    isShow = !isShow;
}

//隐藏/显示模型栏
function HideModelBar() {
    MxDrawX.ShowModelBar(isShow);
    isShow = !isShow;
}

//隐藏/显示状态栏
function HideStatusBar() {
    MxDrawX.ShowStatusBar(isShow);
    isShow = !isShow;
}

//模式切换
var isBrowner = false;

function BrownerMode() {
    isBrowner = !isBrowner;
    MxDrawX.BrowseMode = isBrowner;
    MxDrawX.ShowMenuBar = !isBrowner;
    MxDrawX.ShowPropertyWindow = !isBrowner;
}
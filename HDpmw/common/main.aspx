<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="HDpmw.common.main" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/index.aspx;~/res/css/index.css;~/code/PageBase.cs" />
    <link href="../res/css/iconfont.css" rel="stylesheet" />
    <style>
        body {
            padding: 0 10px;
        }

        .weixin {
            position: fixed;
            bottom: 10px;
            right: 10px;
            text-align: center;
            border: solid 1px #ddd;
            padding: 10px;
            background-color: #efefef;
        }

        .important {
            border-style: solid;
            border-width: 3px;
            display: inline-block;
            padding: 20px;
            position: absolute;
            top: 10px;
            right: 10px;
        }

        .i_box {
            width: 100%;
            margin: 0 0 0 0;
            position: relative;
        }

        .fy {
            position: absolute;
            right: 0;
            top: 30px;
        }

            .fy button {
                background: none;
                margin: 0 0 0 10px;
                cursor: pointer;
            }

        .i_box h2 {
            font-size: 22px;
            height: 60px;
            line-height: 60px;
        }

        .h_box {
            border: 1px solid #dddddd;
            padding: 20px;
        }

            .h_box label {
                line-height: 25px;
                font-size: 20px;
                font-weight: bold;
            }

        .link {
            height: 25px;
            line-height: 25px;
        }

        .brow {
            padding: 30px 0 0 0;
        }

            .brow h3 {
                font-size: 14px;
                font-weight: normal;
            }

            .brow p {
                line-height: 21px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
        <div class="i_box">
            <h2>使用指南</h2>
    <f:TextBox ID="i" runat ="server" Hidden="true" Text="-1"></f:TextBox>
            <div class="h_box">
                <f:Label    ID="Label1" runat="server" Text="<div>产品介绍、使用指南和常见问题里的内容可以基本解答用户使用软件时的疑问。<br/><br/>若仍有其他不能解决的问题请联系客服处理。<br/><br/>通过意见反馈通道，您可以提交反馈意见，帮助我们更好地完善系统的用户体验。</div>"  EncodeText="false">
                </f:Label>
                <div class="fy">
                    <f:Button ID="prevPage" runat="server" OnClick="prevPage_Click" Text="上一条"></f:Button>
                    <f:Button ID="nextPage" runat="server" OnClick="nextPage_Click" Text="下一条"></f:Button>
                </div>
            </div>
            <h2>快速访问</h2>
            <div class="v_box">
                <f:Button ID="btnentpriseInfo" Size="Large" Text="企业信息" IconAlign="Top" IconFont="Building" runat="server"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({
                id: 'baseInfo_entpriseInfo',
                    
                iframeUrl: 'baseInfo/entpriseInfo.aspx',
                title: '企业信息',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnbarcode" Size="Large" Text="条码打印" IconAlign="Top" IconFont="_Print" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'baseInfo_barcode',
                iframeUrl: 'baseInfo/barcode.aspx',
                title: '条码打印',
                icon:'',
                refreshWhenExist: true
                });"  />
                <f:Button ID="btnrawstockInfo" Size="Large" Text="原材料信息" IconAlign="Top" IconFont="FileTextO" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({
                id: 'procontrol_rawstockInfo',
                iframeUrl: 'procontrol/rawstockInfo.aspx',
                title: '原材料信息',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnlamination" Size="Large" Text="预涂膜车间" IconAlign="Top" IconFont="Inbox" runat="server"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'procontrol_lamination',
                iframeUrl: 'procontrol/lamination.aspx',
                title: '预涂膜车间',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btncutting" Size="Large" Text="分切车间" IconAlign="Top" IconFont="Dropbox" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'procontrol_cutting',
                iframeUrl: 'procontrol/cutting.aspx',
                title: '分切车间',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnproutstat" Size="Large" Text="产品出库" IconAlign="Top" IconFont="_Upload" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({
                id: 'procontrol_proutstat',
                iframeUrl: 'procontrol/proutstat.aspx',
                title: '产品出库',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnrawstocklist" Size="Large" Text="原材料统计" IconAlign="Top" IconFont="PieChart" runat="server"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'statistics_rawstocklist',
                iframeUrl: 'baseInfo/entpriseInfo.aspx',
                title: '原材料统计',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btncuttinglist" Size="Large" Text="分切统计" IconAlign="Top" IconFont="LineChart" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'statistics_cuttinglist',
                iframeUrl: 'statistics/cuttinglist.aspx',
                title: '分切统计',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnstatoutpro" Size="Large" Text="出库统计" IconAlign="Top" IconFont="FileTextO" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'statistics_statoutpro',
                iframeUrl: 'statistics/statoutpro.aspx',
                title: '出库统计',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnqualtrat" Size="Large" Text="质量跟踪" IconAlign="Top" IconFont="Compass" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'statistics_qualtrat',
                iframeUrl: 'statistics/qualtrat.aspx',
                title: '质量跟踪',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnrevisePassword" Size="Large" Text="修改密码" IconAlign="Top" IconFont="UnlockAlt" runat="server" Hidden="false"
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'systemInfo_revisePassword',
                iframeUrl: 'systemInfo/revisePassword.aspx',
                title: '修改密码',
                icon:'',
                refreshWhenExist: true
                });" />
                <f:Button ID="btnExcel" Size="Large" Text="班次统计" IconAlign="Top" IconFont="_File" runat="server" 
                    CssClass="marginr" EnablePostBack="false" OnClientClick="top.addExampleTab({ 
                id: 'statistics_classlist',
                iframeUrl: 'statistics/classlist.aspx',
                title: '班次统计',
                icon:'',
                refreshWhenExist: true
                });"></f:Button>
            </div>
            <h2>相关链接</h2>
            <div class="link">
                <p>江苏恒达机械制造信息一体化集成系统：http://101.132.119.216:8086</p>
            </div>
            <div class="brow">
                <h3>推荐使用</h3>
                <p>Chrome、Firefox、Safari、Edge、IE11浏览器，以获取最好的性能。</p>
                <span>IE8.0 360 浏览器有限支持 </span>
            </div>
            <div class="brow">
                <p>Copyright ©2019 江苏恒达机械制造有限公司</p>
                <p>江苏恒阜远科技有限公司 </p>
                <p>中国联合网络通信有限公司盐城市分公司 技术支持</p>
            </div>
        </div>
    </form>
    <script>
</script>
</body>
</html>

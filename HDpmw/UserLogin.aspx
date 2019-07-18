<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="UserLogin.aspx.cs" Inherits="HDpmw.UserLogin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>江苏恒达机械制造有限公司</title>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <link href="res/css/login.css" rel="stylesheet" />
    <%--<script type="text/javascript">        
        //本页面一定是顶层窗口，不会嵌在IFrame中        
    if (top.window != window) {                
        top.window.location.href = "./UserLogin.aspx";        
    }
    </script>--%>

    <script>
        $(document).ready(function () {
            $("#txtUserName").focus();
        });
    </script>

</head>
<body>
<div class="xk_bg">
	<div class="w1080">
    	<div class="xk_logo"><img src="images/xk_logo.png"></div>
        <div class="xk_log">
        	<div class="xk_log_l fn_left">
            	<img src="images/xk_lc.png">
            </div>
            <div class="xk_log_r fn_right">
            	<form id="form1" runat="server">
                	<h1>用户登录</h1>
                    <div><input type="text" placeholder="用户名" name="txtUserName" id="txtUserName" runat="server"></div>
                    <div><input type="password" placeholder="密码" name="txtPassword" id="txtPassword" runat="server"></div>
                    <div>
                        <input type="text" placeholder="验证码" class="yzm" name="txtverifycode" id="txtverifycode" runat="server">
                    	<img src="common/verificationCode.aspx" alt="验证码" id="imgCode">
                    </div>
                    <div><asp:Button CssClass="loginbtn" ID="btnLogin" runat="server" OnClick="btnLogin_Click"/></div>
                    <div><asp:Label runat="server" Text="" ID="lblMessage" Width="300px "></asp:Label></div>
                </form>
            </div> 
        </div>
    </div>
</div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="lifesense.Web.Admin.LeftMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>左边菜单导航</title>
    <style type="text/css">
    Images
    {
     margin-right:3px;
     vertical-align:middle;
    }
    body
    {
    margin:3px;
	background-color:#687FD9;
	font-size:12px;
	
	background-repeat: repeat-x;
	background-position: left top;
	font-family:Tahoma,宋体;
    }
	a:link,a:visited{
	text-decoration: none;
	color: #0077FF;
}
a:hover{
	color: #03C;
	text-decoration: underline;
}
    .menuMain
    {
     background-color:White;
    }
    .menuMain b
    {
     cursor:pointer;
    }
    .menuMain .menuMain
    {
     padding:2px;
    }
    .menuMain .menuItems
    {
     margin-left:10px;margin-top:5px;
    }
    .menuMain .menuItems .menuItem
    {
     padding:2px;
    }
#Menu {
	margin-top: 6px;
	border-top: solid 1px #5B99D;
	border-right: solid 1px #5B99D;
	border-left: solid 1px #5B99D;
}
.myinfo {
	background-image: url(Images/top/top/menubg.gif);
	background-position: left top;
	background-repeat: repeat-x;
	padding-left: 10px;
	line-height: 26px;
    border-bottom:solid 1px #AACAEA;
    border-top:solid 1px #AACAEA;
    margin-top:-1px;
	cursor: pointer;
}
.myinfo a,.myinfo a:visited,.myinfo a:hover {
	line-height: 26px;
	font-weight: bold;
	color: #333;
	height: 26px;
	display: block;
	padding-left: 16px;
	text-decoration: none;
	font-size: 13px;
}
.add {
	background-image: url(Images/top/addfor.gif);
	background-repeat: no-repeat;
	background-position: left top;
}
.sub {
	background-image: url(Images/top/addfor.gif);
	background-position: left -30px;
	background-repeat: no-repeat;
}



a.menuItem2 {
	display: block;
	width:124px;
	line-height: 22px;
	height: 22px;
	padding:0px 0px 0px 12px;
	background-image: url(Images/top/addfor_hov.gif);
	background-repeat: no-repeat;
	background-position: left top;
	color: #333333;
}
a.menuItem2:hover {
	background-image: url(Images/top/addfor_hov.gif);
	text-decoration: none;
	background-repeat: no-repeat;
	background-position: left -22px;
	color: #39F;
}
a.menuItem3 {
	display: block;
	width:124px;
	line-height: 22px;
	height: 22px;
	padding:0px 0px 0px 12px;
	background-image: url(Images/top/addfor_hov.gif);
	background-repeat: no-repeat;
	background-position: left top;
	font-weight:bold;
	color: #000;
}
a.menuItem3:hover {
	background-image: url(Images/top/addfor_hov.gif);
	text-decoration: none;
	background-repeat: no-repeat;
	background-position: left -22px;
	color: #39F;
}
    </style>

    <base target="main" />
    <script type ="text/javascript" >
        function checkEnable(id) {
            try {
                var name = document.getElementsByTagName("div");
                for (var i = 0; i < name.length; i++) {
                    if (name[i].getAttribute("name") != "" && name[i].getAttribute("name") == "divmenu") {
                        if (name[i].getAttribute("id") == id && name[i].style.display == "none") {
                            name[i].style.display = "block";
                            name[i].parentNode.innerHTML = name[i].parentNode.innerHTML.replace("add", "sub");
                        } else {
                            name[i].style.display = "none";
                            name[i].parentNode.innerHTML = name[i].parentNode.innerHTML.replace("sub", "add");
                        }
                    }
                }
            } catch (e) { }
        }
        function checkEnable2(id) {
            try {
                var name = document.getElementById(id);
                if (document.getElementById(id).style.display == "none") {
                    document.getElementById(id).style.display = "block";
                } else {
                    document.getElementById(id).style.display = "none";
                }
            } catch (e) { }
        }
        //菜单缩略
        function ToExpand() {
            try {
                var name = document.getElementsByTagName("div");
                var lbname = document.getElementById("MenuFunction");
                var b = false;
                if (lbname.innerHTML != "展开") {
                    b = true;
                    lbname.innerHTML = "展开";
                } else { lbname.innerHTML = "收缩"; }
                for (var i = 0; i < name.length; i++) {
                    if (name[i].getAttribute("name") != "" && (name[i].getAttribute("name") == "divmenu" || name[i].getAttribute("name") == "divmenuIt")) {
                        if (b == true) {
                            name[i].style.display = "none";
                            name[i].parentNode.innerHTML = name[i].parentNode.innerHTML.replace("sub", "add");
                        } else {
                            name[i].style.display = "block";
                            name[i].parentNode.innerHTML = name[i].parentNode.innerHTML.replace("add", "sub");
                        }
                    }
                }
            } catch (e) { }
        } 
    </script>
</head>
<body >
<div>
    <div style="background-image:url('Images/top/e2.gif');border:1px solid #5B99D7; font-weight:bold;padding:5px 8px;"><img src="Images/top/bul_title.gif" width="16" height="16" align="absmiddle" /> 后台功能菜单</div>
    <div style="border-left:1px solid #5B99D7;border-right:1px solid #5B99D7;border-bottom:1px solid #617387;border-bottom-width:2px;padding:5px; background-color:#FDFEE9;">
       <%-- <a id="MenuFunction" onclick="ToExpand()" href="javascript:">展开</a> | --%>
        <a  href="EditPwd.aspx" target="main">修改密码</a> |
        <a  href="Logout.aspx?action=logout" target="_top">退出</a>
    </div>
   <%-- <asp:Literal ID="litMenu" runat="server" ></asp:Literal>--%>
    <div  class='menuMain' >
        <div class="menuItem" ><a class="menuItem2" href="../User/UserList.aspx" target="main">查看用户</a></div>
        <div class="menuItem" ><a class="menuItem2" href="#" target="main">用户统计</a></div>
    </div>
</div>
</body>
</html>

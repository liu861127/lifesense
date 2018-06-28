<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Site_Head.aspx.cs" Inherits="lifesense.Web.Admin.Site_Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>乐心数据分析后台</title> 
 <style type="text/css">  
    body {	font-size: 12px;margin:0px;}
    a:link { color:#000000;text-decoration:none}
	a:hover {color:#666666;}
	a:visited {color:#000000;text-decoration:none}
	td {font-size: 9pt;  COLOR: #000000; font-family: "宋体"}
	img {filter:Alpha(opacity:100); chroma(color=#FFFFFF)}
	#Top
	{
	  margin: auto;
	  height:28px;
	  background-image:url("images/admin/admin_top_bg.gif");
		} 
	</style>
		<base target="main">
	<script type ="text/javascript" >
            function preloadImg(src)
            {
	            var img=new Image();
	            img.src=src
            }
            preloadImg("Images/top/admin_top_open.gif");

            var displayBar=true;
            function switchBar(obj)
            {
	            if (displayBar)
	            {
		            parent.frame.cols="0,*";
		            displayBar=false;
		            obj.src = "Images/top/admin_top_open.gif";
		            obj.title="打开左边管理菜单";
	            }
	            else{
		            parent.frame.cols="180,*";
		            displayBar=true;
		            obj.src = "Images/top/admin_top_close.gif";
		            obj.title="关闭左边管理菜单";
	            }
	        }
	       
		</script>
	</head>
	<body>
	    <form id="form1" runat="server"> 
	    	<asp:ScriptManager ID="ScriptManager" runat="server">
	</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel" runat="server">
	<ContentTemplate>
	<div id="Top">
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr valign="middle">
				<td width="50">
					<img onclick="switchBar(this)" src="Images/top/admin_top_close.gif" title="关闭左边管理菜单" style="CURSOR:hand">
				</td>
				<td>
					<table border ="0" width ="100%" >
					    <tr>
					        <td width="60"> <font color="#333333">时间：</font>
					        </td>
					        <td>
					          <font color="#CC0000">
                                  <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></font>
                                </td>
					        <td style="display:none;">
					          <font color="#CC0000">
					          &nbsp;</font></marquee></td>
					    </tr>
					</table>
				</td>
				<td width="34" align="center">&nbsp;</td>
			</tr>
		</table>
	</div> 
		<asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="30000"></asp:Timer>
	    		</ContentTemplate>
	</asp:UpdatePanel>
        </form>
	</body>

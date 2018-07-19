<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPwd.aspx.cs" Inherits="lifesense.Web.Admin.EditPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
     #Edit
     {
     	background-color:#EBF7FC;
     	}
    .Edit
    {
    	background-color:#EBF7FC;
    	width:100%;
    	}
    .right
    {
    	text-align:right;
        width:7%;
    	}
        .style1
        {
            width: 132px;
        }
        .auto-style1 {
            text-align: right;
            width: 14%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="SectionHeader"><b>后台管理员密码修改</b></div>
    <div id="Edit">
          <table class="Edit">
            <tr>
                <td class="auto-style1">
                    原来密码：</td>
                <td class="style1">
                    <asp:TextBox ID="oldPwd" runat="server" TextMode="Password" style="margin-left: 0px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="原来密码不能为空！" ControlToValidate="oldPwd"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    新密码：</td>
                <td class="style1">
                    <asp:TextBox ID="newPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    确认新密码：</td>
                <td class="style1">
                    <asp:TextBox ID="surePwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="newPwd" ControlToValidate="surePwd" ErrorMessage="两次输入密码不一致！"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="Button1" runat="server" Text="确定" CssClass="Button" 
                        onclick="Button1_Click" />
                </td>
                <td class="style1">
                    <asp:Button ID="Button2" runat="server" Text="取消" CssClass="Button" 
                        onclick="Button2_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

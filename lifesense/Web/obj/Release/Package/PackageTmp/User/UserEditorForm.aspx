<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEditorForm.aspx.cs" Inherits="lifesense.Web.User.UserEditorForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 29%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td height="25" width="30%" align="right">ID：</td>
                                <td height="25" width="*" align="left">
                                    <asp:Label ID="lblID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="25" width="30%" align="right">账号：</td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtFUserID" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td height="25" width="30%" align="right">用户名：</td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtFUserName" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td height="25" width="30%" align="right">密码：</td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtUserPwd" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdbg" align="center" valign="bottom">
                        <asp:Button ID="btnSave" runat="server" Text="保存"
                            OnClick="btnSave_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                            onmouseout="this.className='inputbutton'"></asp:Button>
                        <asp:Button ID="btnCancle" runat="server" Text="取消"
                            OnClick="btnCancle_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                            onmouseout="this.className='inputbutton'"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

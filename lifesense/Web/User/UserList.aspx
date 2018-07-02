<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="lifesense.Web.User.UserList" EnableEventValidation = "false"  %>

<!DOCTYPE html>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户列表</title>
    <link href="../../css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../css/cssdata.css" rel="stylesheet" type="text/css" />
    <script src="../js/CheckBox.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="handdatetime" runat="server" />
    <table cellspacing="0" cellpadding="2" border="1" style="width: 100%; border-collapse: collapse;">
        <tr align="left" class="tdbg-dark2">
            <td>
            <asp:Button ID="BtnShow" runat="server" Text="导出Excel" OnClick="BtnShow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font style="color: Red;">
                <label runat="server" id="lblmsg">
                </label>
            </font>
            </td>
        </tr>
        <tr align="left" class="tdbg-dark2">
            <td>&nbsp;&nbsp;&nbsp;<hr />&nbsp;&nbsp;用户名：<asp:TextBox ID="txtUserName" runat="server" Width="257px"></asp:TextBox>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
&nbsp;<asp:Button ID="btnsubmit" runat="server" Text="查询" OnClick="btnsubmit_Click" />
            &nbsp;
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" />
                &nbsp; <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="新增" />
            </td>
        </tr>
        <tr align="left" class="tdbg-dark2">
        <td>
             <label runat="server" id="lblCheck" visible="false"></label>
        </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="Gdv_data" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  ShowFooter="true"
            CellPadding="4" Width="100%">
            <HeaderStyle CssClass="tdbg-tree" Height="22px" HorizontalAlign="Center" />
            <RowStyle CssClass="tdbg-dark" Height="22px" HorizontalAlign="Center" />
            <Columns>
                 <asp:TemplateField ControlStyle-Width="30" HeaderText="选择"    >
                                <ItemTemplate>
                                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
            <asp:TemplateField HeaderText="账号">
                    <ItemTemplate>
                     <a href='UserEditorForm.aspx?ID=<%#Eval("ID")%>' target="_blank"><%#Eval("UserID")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="用户名称" DataField="UserName" ItemStyle-CssClass="dmessage" />
           <%--     <asp:BoundField HeaderText="密码" DataField="UserPwd" />--%>
                            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="UserEditorForm.aspx?id={0}"
                                Text="编辑"  />
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除"   Visible="false"  >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
            </Columns>
             <FooterStyle CssClass="tdbg-tree" Height="20px" HorizontalAlign="Center" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging"
            AlwaysShow="true" NextPageText='下一页' PrevPageText='上一页' CurrentPageButtonClass="current"
            PageSize="10" CssClass="AspNetPager" ShowDisabledButtons="true" HorizontalAlign="center"
            FirstPageText="第一页" LastPageText="最后一页" InputBoxClass="textbox" ShowBoxThreshold="10"
            SubmitButtonStyle="margin-left:5px;" ShowCustomInfoSection="Left" CustomInfoSectionWidth="15%"
            CustomInfoTextAlign="right" TextAfterInputBox=" 页" TextBeforeInputBox="转到 ">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>

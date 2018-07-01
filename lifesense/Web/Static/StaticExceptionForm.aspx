<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticExceptionForm.aspx.cs" Inherits="lifesense.Web.Static.StaticExceptionForm" EnableEventValidation = "false"  %>

<!DOCTYPE html>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户列表</title>
    <link href="../../css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../css/cssdata.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../js/CheckBox.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 72px;
        }
    </style>
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
            <td class="auto-style1">&nbsp;&nbsp;&nbsp;<hr />&nbsp;&nbsp;用户名：<asp:TextBox ID="txtUserName" runat="server" Width="257px"></asp:TextBox>&nbsp;起始日期：<asp:TextBox ID="txtstartime" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="89px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;结束日期：<asp:TextBox ID="txtenddate" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="108px"></asp:TextBox>&nbsp;&nbsp; &nbsp;
                <asp:CheckBox ID="chkIsAll" runat="server" OnCheckedChanged="chkIsAll_CheckedChanged" Text="是否全选" autopostback="true"/>
                &nbsp; &nbsp; &nbsp;
&nbsp;<asp:Button ID="btnsubmit" runat="server" Text="查询" OnClick="btnsubmit_Click" />
            &nbsp;
                <asp:Button ID="btnSysData" runat="server" Text="同步" OnClick="btnSysData_Click" />
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
                <asp:BoundField HeaderText="账号" DataField="UserID" ItemStyle-CssClass="dmessage" />
                <asp:BoundField HeaderText="日期" DataField="WriteTime" />
                <asp:BoundField HeaderText="请求地址" DataField="Url" />
                <asp:BoundField HeaderText="失败原因" DataField="Reason" />   
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


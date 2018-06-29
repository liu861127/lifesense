<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticForm.aspx.cs" Inherits="lifesense.Web.Static.StaticForm" EnableEventValidation = "false" %>

<!DOCTYPE html>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>乐心数据分析列表</title>
    <link href="../../css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../css/cssdata.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            <td>时间类型：
            <asp:DropDownList ID="ddltimType" runat="server">
            <asp:ListItem Text="测量时间" Value="MeasureTime"></asp:ListItem>
            <asp:ListItem Text="入睡时间" Value="SleepingTime"></asp:ListItem>
            <asp:ListItem Text="醒来时间" Value="WakingTime"></asp:ListItem>
            <asp:ListItem Value="StartTime">心率测量开始时间</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
            起始日期：<asp:TextBox ID="txtstartime" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="89px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;结束日期：<asp:TextBox ID="txtenddate" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="108px"></asp:TextBox>&nbsp;&nbsp; 用户ID:&nbsp;&nbsp; <asp:TextBox ID="txtUserID" runat="server" Width="162px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnsubmit" runat="server" Text="查询" OnClick="btnsubmit_Click" />
            &nbsp;&nbsp;<asp:Button ID="btnSynchronization" runat="server" Text="同步" OnClick="btnSynchronization_Click" />
                &nbsp;&nbsp;&nbsp;<hr /><br />
            </td>
        </tr>
        <tr align="left" class="tdbg-dark2">
        <td>
            <label style="color:Red; font-size:18px;" runat="server" id="lblmsge"></label>
        </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="gvStatic" runat="server" AutoGenerateColumns="False"  ShowFooter="true"
            CellPadding="4" Width="100%" OnRowDataBound="gvStatic_RowDataBound">
            <HeaderStyle CssClass="tdbg-tree" Height="22px" HorizontalAlign="Center" />
            <RowStyle CssClass="tdbg-dark" Height="22px" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField HeaderText="用户ID" DataField="用户ID" ItemStyle-CssClass="dmessage" ItemStyle-Width="50" />
                <asp:BoundField HeaderText="测量时间" DataField="测量时间" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField HeaderText="步数" DataField="步数" />
                <asp:BoundField HeaderText="卡里路" DataField="卡里路" ItemStyle-CssClass="dpassmode" />
                <asp:BoundField HeaderText="里程" DataField="里程" />
                <asp:BoundField HeaderText="入睡时间" DataField="入睡时间" />
                <asp:BoundField HeaderText="醒来时间" DataField="醒来时间" />
                <asp:BoundField HeaderText="深睡时长(分钟)" DataField="深睡时长(分钟)" />
                <asp:BoundField HeaderText="浅睡时长(分钟)" DataField="浅睡时长(分钟)" />
                <asp:BoundField HeaderText="醒来时长（分钟)" DataField="醒来时长（分钟)" />
                <asp:BoundField HeaderText="醒来次数" DataField="醒来次数" />
                <asp:BoundField HeaderText="心率测量开始时间" DataField="心率测量开始时间" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
             <FooterStyle CssClass="tdbg-tree" Height="20px" HorizontalAlign="Center" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging"
            AlwaysShow="true" NextPageText='下一页' PrevPageText='上一页' CurrentPageButtonClass="current"
            PageSize="50" CssClass="AspNetPager" ShowDisabledButtons="true" HorizontalAlign="center"
            FirstPageText="第一页" LastPageText="最后一页" InputBoxClass="textbox" ShowBoxThreshold="10"
            SubmitButtonStyle="margin-left:5px;" ShowCustomInfoSection="Left" CustomInfoSectionWidth="15%"
            CustomInfoTextAlign="right" TextAfterInputBox=" 页" TextBeforeInputBox="转到 ">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>

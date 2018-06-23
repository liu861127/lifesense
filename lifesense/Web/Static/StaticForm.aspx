<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticForm.aspx.cs" Inherits="lifesense.Web.Static.StaticForm" %>

<!DOCTYPE html>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>彩票出票查询系统---方案列表</title>
    <link href="../../css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../css/cssdata.css" rel="stylesheet" type="text/css" />
<script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            <asp:ListItem Text="投注时间" Value="ReceiveTime"></asp:ListItem>
            <asp:ListItem Text="结算时间" Value="BonusTime"></asp:ListItem>
            <asp:ListItem Text="出票时间" Value="PrintOutTime"></asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
            起始日期：<asp:TextBox ID="txtstartime" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="70px"></asp:TextBox>&nbsp;<asp:DropDownList ID="dlsHours" runat="server">
                </asp:DropDownList>
                时 &nbsp;<asp:DropDownList ID="dlsMinute" runat="server">
                </asp:DropDownList>
                分 &nbsp;<asp:DropDownList ID="dlsSencod" runat="server">
                </asp:DropDownList>
                秒 &nbsp;&nbsp;&nbsp;结束日期：<asp:TextBox ID="txtenddate" runat="server"  Columns="10"  onfocus="WdatePicker()" Width="70px"></asp:TextBox>&nbsp;<asp:DropDownList ID="dleHours" runat="server">
                </asp:DropDownList>
                时 &nbsp;<asp:DropDownList ID="dleMinute" runat="server">
                </asp:DropDownList>
                分 &nbsp;<asp:DropDownList ID="dleSecond" runat="server">
                </asp:DropDownList>
                秒 &nbsp;&nbsp;&nbsp;<hr />彩种：<asp:DropDownList ID="ddlTypeGroup" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlTypeGroup_SelectedIndexChanged"/>
                &nbsp;&nbsp;&nbsp;玩法：
        <asp:DropDownList ID="ddlType" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlstuats" runat="server">
            <asp:ListItem Text="请选择订单状态" Value="w"></asp:ListItem>
        <asp:ListItem Text="全部" Value="w"></asp:ListItem>
                    <asp:ListItem Text="已出票" Value="0"></asp:ListItem>
                    <asp:ListItem Text="待出票" Value="1"></asp:ListItem>
                    <asp:ListItem Text="撤单" Value="2"></asp:ListItem>
                    <asp:ListItem Text="中奖" Value="3"></asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="NoType" runat="server">
                <asp:ListItem Value="1" Text="我方流水号"></asp:ListItem>
                <asp:ListItem Value="2" Text="订单号" Selected="True"></asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="txtmsgID" runat="server" Width="86"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddllistpalitype" runat="server">
            <asp:ListItem Text="请选择排序方式" Value="-1"></asp:ListItem>
            <asp:ListItem Text="订单号" Value="MessageId"></asp:ListItem>
            <asp:ListItem Text="出票时间" Value="PrintOutTime"></asp:ListItem>
            <asp:ListItem Text="投注时间" Value="ReceiveTime"></asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="chenkbos" runat="server" Text="降序" />
        &nbsp;&nbsp;&nbsp;期号：<asp:TextBox ID="txtstatrIssue" runat="server" Width="86"></asp:TextBox>-<asp:TextBox
            ID="txtEndIssue" runat="server" Width="86"></asp:TextBox><br />
               奖金：<asp:DropDownList ID="ddpPrize" runat="server">
            <asp:ListItem Value="0">全部</asp:ListItem>
            <asp:ListItem Value="1">0元以上</asp:ListItem>
            <asp:ListItem Value="50">50元以上</asp:ListItem>
            <asp:ListItem Value="100">100元以上</asp:ListItem>
            <asp:ListItem Value="200">200元以上</asp:ListItem>
            <asp:ListItem Value="300">300元以上</asp:ListItem>
            <asp:ListItem Value="400">400元以上</asp:ListItem>
            <asp:ListItem Value="500">500元以上</asp:ListItem>
            <asp:ListItem Value="600">600元以上</asp:ListItem>
            <asp:ListItem Value="1000">1000元以上</asp:ListItem>
            <asp:ListItem Value="1200">1200元以上</asp:ListItem>
            <asp:ListItem Value="1500">1500元以上</asp:ListItem>
            <asp:ListItem Value="3000">3000元以上</asp:ListItem>
            <asp:ListItem Value="4000">4000元以上</asp:ListItem>
            <asp:ListItem Value="5000">5000元以上</asp:ListItem>
            <asp:ListItem Value="10000">10000元以上</asp:ListItem>
            </asp:DropDownList>
                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label ID="lblProvinceId" runat="server" Text="出票省份：" ></asp:Label>
                <asp:DropDownList ID="ddlProvinceId" runat="server">
                </asp:DropDownList>
&nbsp;<asp:Button ID="btnsubmit" runat="server" Text="查询" OnClick="btnsubmit_Click" />
            </td>
        </tr>
        <tr align="left" class="tdbg-dark2">
        <td>
            <label style="color:Red; font-size:18px;" runat="server" id="lblmsge"></label>
        </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="Gdv_data" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  ShowFooter="true"
            CellPadding="4" Width="100%">
            <HeaderStyle CssClass="tdbg-tree" Height="22px" HorizontalAlign="Center" />
            <RowStyle CssClass="tdbg-dark" Height="22px" HorizontalAlign="Center" />
            <Columns>
            <asp:TemplateField HeaderText="流水号">
                    <ItemTemplate>
                     <a href='UserEditorForm.aspx?ID=<%#Eval("ID")%>' target="_blank"><%#Eval("ID")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="订单号" DataField="MessageId" ItemStyle-CssClass="dmessage" />
                <asp:BoundField HeaderText="商户名称" DataField="UserName" />
                <asp:BoundField HeaderText="投注金额" DataField="Amount" />
                <asp:TemplateField HeaderText="订单状态">
                    <ItemTemplate>
                       <%-- <%#TicketOut.Business.Lottery.GetLotteryType(Eval("Status").ToString())%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="玩法类型" DataField="PassModeCHN" ItemStyle-CssClass="dpassmode" />
                <asp:BoundField HeaderText="期号" DataField="IssueNum" />
                <asp:TemplateField HeaderText="是否中奖">
                    <ItemTemplate>
                        <%--<%#Convert.ToInt32(Eval("IsWin")) == 0 ? "<span style='color:blue'>否</span>" : "<span style='color:red'>是</span>"%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="接收时间" DataField="ReceiveTime" />
                <asp:BoundField HeaderText="出票时间" DataField="PrintOutTime" />
                                <asp:TemplateField HeaderText="出票省份">
                    <ItemTemplate>
                       <%--<%# TicketOut.Business.Comm.Common.GetProvinceNameByProvinceId(Eval("ProvinceId")==null?"":Eval("ProvinceId").ToString())%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="结算时间" DataField="BonusTime" />
                <asp:BoundField HeaderText="奖金" DataField="Bonus" />
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

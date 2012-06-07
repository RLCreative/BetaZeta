<%@ Page Title="Track an order" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="sales_trackorders" CodeBehind="trackorders.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="salerepview">
<h2><strong>Track Orders:</strong></h2>
<img id="Img1" src="~/images/right.gif" runat="server" alt="" />You can track your order by entering any of the information below.<br /><br />
<asp:Panel id="pnlTrackInfo" runat="server" Visible="false">
<table align="center">
    <tr>
        <td width="170"</td><td></td>
    </tr>
    <tr>
        <td width="170">PDF Reference Number:</td><td><asp:TextBox ID="txtPDFRefNum" runat="server" Width="200"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="170">Regal Lager Order Number:</td><td><asp:TextBox ID="txtGPOrderNum" runat="server" Width="200"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="170">Tracking Number:</td><td><asp:TextBox ID="txtTrackingNum" runat="server" Width="200"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="170"></td><td align="center"><asp:Button ID="btnSubmit" 
            runat="server" Text="Track" Width="125" onclick="btnSubmit_Click"></asp:Button></td>
    </tr>
    <tr>
        <td width="170"</td><td></td>
    </tr>
</table>
<br /><br />
</asp:Panel>
<asp:Panel id="pnlOther" runat="server" Visible="false" Font-Italic="True">
    Tracking info: The only tracking information we have available is <asp:Label ID="lblTrackingInfo" runat="server"></asp:Label>   
</asp:Panel>
<asp:Panel id="pnlError" runat="server" Visible="false" Font-Italic="True">
    <asp:Label id="lblError" runat="server"></asp:Label><br /><br />
</asp:Panel>
</div>
</asp:Content>


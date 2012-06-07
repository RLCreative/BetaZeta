<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="admin_orderdetails" CodeBehind="orderdetails.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="retailersview">
<h2><strong>Order Details:</strong></h2>
<h4><strong><asp:Label ID="lblTitle" runat="server"></asp:Label></strong></h4>

<telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" Pagesize="20"
    DataSourceID="DropShipOrderDetail" GridLines="None" Skin="Telerik">
<HeaderContextMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</HeaderContextMenu>

<MasterTableView AutoGenerateColumns="False" DataSourceID="DropShipOrderDetail">
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridBoundColumn DataField="ItemNum" HeaderText="ItemNum" 
            SortExpression="ItemNum" UniqueName="ItemNum">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Name" 
            HeaderText="Name" SortExpression="Name" UniqueName="Name">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Quantity" DataType="System.Decimal" 
            HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" 
            UniqueName="Quantity">
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>

<FilterMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</FilterMenu>
    </telerik:RadGrid>
    <asp:SqlDataSource ID="DropShipOrderDetail" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RLConnectString %>" 
        SelectCommand="SELECT     SOPPDFDetail.ItemNum, wsPF_ProductExtraInfo.Name, CAST(SOPPDFDetail.Quantity as Decimal(10,0)) as Quantity
                        FROM         SOPPDFHeader INNER JOIN SOPPDFDetail ON SOPPDFHeader.PDFRefNum = SOPPDFDetail.PDFRefNum INNER JOIN
                                     wsPF_ProductExtraInfo ON SOPPDFDetail.ItemNum = wsPF_ProductExtraInfo.ProductID
                        WHERE (SOPPDFHeader.PDFRefNum = @PDFRefNum)"> 
        <SelectParameters>
            <asp:QueryStringParameter Name="PDFRefNum" QueryStringField="PDFRefNum" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <table width="500" align="center">
        <tr>
            <td align="center"><asp:HyperLink ID="hypBack" runat="server" ImageUrl="~/images/left.png"></asp:HyperLink>
            <asp:HyperLink ID="hypBackText" runat="server" Text="Back to Orders"></asp:HyperLink></td>
            <td align="center">|</td>  
            <td align="center"><asp:HyperLink ID="hypPrint" runat="server" ImageUrl="~/images/icon-print.gif"></asp:HyperLink>
            <asp:HyperLink ID="hypPrintText" runat="server" Text="Print Order"></asp:HyperLink></td>
            <td align="center">|</td>
            <td align="center"><asp:HyperLink ID="hypTrack" runat="server" ImageUrl="~/images/41.png"></asp:HyperLink>
            <asp:HyperLink ID="hypTrackText" runat="server" Text="Track Order"></asp:HyperLink></td>
        </tr>
    </table>  
    
</div>
</asp:Content>


<%@ Page Title="Confirm your order" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="sales_confirmorder" CodeBehind="confirmorder.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="salerepview">
<h2><strong>Order Confirmation:</strong></h2>
Your shopping cart has been saved with the PO Number: <asp:Label ID="lblConfirmPO" Font-Bold="true" Font-Italic="true" runat="server" />. If the details are correct please submit the order below. 
You can add more items to the cart by clicking on edit order,  below. If you are ready to submit the order select the method of shipping and click submit order.
<br /><br />
<!-- SHOW ORDER DETAILS -->
Customer: <asp:label Font-Bold="true" ID="lblCustomer" runat="server" Visible="true" /><br />
PO Number: <asp:label Font-Bold="true" ID="lblPONum" runat="server" Visible="true" />
<asp:GridView runat="server" ID="gvOrderConfirm" AutoGenerateColumns="false" 
    EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" 
    CellPadding="5" ShowFooter="true" DataKeyNames="ItemNum" 
    OnRowDataBound="gvOrderConfirm_RowDataBound" OnRowCommand="gvOrderConfirm_RowCommand" >
    <HeaderStyle HorizontalAlign="Left" BackColor="#7E7E7E" ForeColor="#FFFFFF" />
    <FooterStyle HorizontalAlign="Right" BackColor="#7E7E7E" ForeColor="#FFFFFF" />
    <AlternatingRowStyle BackColor="#F8F8F8" />
    <Columns>
        <asp:BoundField DataField="ItemNum" HeaderText="ItemNum" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Price" HeaderText="Price" />                    
        <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                <asp:TextBox runat="server" ID="txtQuantity" Columns="5" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnUpdateCart" Visible="false" Enabled="false" Text="Update" CommandName="Fix" 
                    CommandArgument='<%# Eval("ItemNum") %>' 
                    style="font-size:12px;"></asp:LinkButton>                            
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="TotalPrice" HeaderText="Total" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
        <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="btnRemove" Visible="false" Enabled="false" Text="Remove" CommandName="Remove" 
                    CommandArgument='<%# Eval("ItemNum") %>' style="font-size:12px;"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<table width="100%">
    <tr>
        <td align="left"><asp:Label ID="lblMessage" runat="server" Font-Italic="True"></asp:Label></td>
        <td align="right"></td>
    </tr>
</table>
<table width="100%">
                <tr>
                    <td align="left" width="165">New Order<asp:ImageButton ID="btnNewOrder" runat="server" 
                        ImageUrl="~/images/new_order.png" AlternateText="Create New Order!" onclick="btnNewOrder_Click" /></td>
                    <td align="left" width="165">View Orders<asp:ImageButton ID="btnViewOrders" runat="server" 
                        ImageUrl="~/images/view_orders.png" AlternateText="Find Existing Order!" onclick="btnViewOrder_Click" /></td>        
                    <td align="left" width="165">Edit order<asp:ImageButton ID="btnEditOrder" runat="server" 
                        ImageUrl="~/images/edit_order.png" AlternateText="Edit This Order!" onclick="btnEditOrder_Click"/></td>
                    <td align="left" width="165">Delete order<asp:ImageButton ID="btnDeleteOrder" runat="server" 
                        ImageUrl="~/images/delete_order.png" AlternateText="Delete This Order!" onclick="btnDeleteOrder_Click" 
                        OnClientClick="return confirm('Are you sure want to delete this order?')" /></td>                    
                    <td align="right">Submit order<asp:ImageButton ID="btnSubmitOrder" runat="server" 
                        ImageUrl="~/images/submit_order.png" onclick="btnSubmitOrder_Click"  AlternateText="Submit Order for Processing!"
                        OnClientClick="return confirm('Do you want to submit this order?  This order will be processed and shipped.')" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>        
    
<br /><br />
<!-- END ORDER DETAILS -->
<!-- HIDDEN FIELDS -->
<asp:Panel ID="pnlHidden" runat="server" Visible="false">
        <table width="50%" align="left">
           <tr>
              <td align="right" width="100">GUID:</td>
              <td align="left">  <asp:label ID="lblGuid" runat="server" Visible="true" /></td>
           </tr>
           <tr>
              <td align="right" width="100">CustomerID:</td>
              <td align="left">  <asp:label ID="lblCustomerID" runat="server" Visible="true" /></td>
           </tr>
           <tr>
              <td align="right" width="100">HeaderOrderID:</td>
              <td align="left">  <asp:label ID="lblHeaderOrderID" runat="server" Visible="true" /></td>
           </tr>
           <tr>
              <td align="right" width="100">PO Number:</td>
              <td align="left">  <asp:label ID="lblPONumber" runat="server" Visible="true" /></td>
           </tr>
           <tr>
              <td align="right" width="100">Error Message:</td>
              <td align="left">  <asp:label ID="lblErrorMessage" runat="server" Visible="true" /></td>
           </tr>
        </table>
</asp:Panel>
<!-- END HIDDEN FIELDS -->
</div>
</asp:Content>


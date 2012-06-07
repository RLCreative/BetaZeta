<%@ Page Title="Confirm your order" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_confirmorder" CodeBehind="confirmorder.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/loggedinstyles.css" />
    <div id="retailersview">
        <h2><strong>Order Confirmation:</strong></h2>
        Your shopping cart has been saved with the PO Number:
        <asp:Label ID="lblConfirmPO" Font-Bold="true" Font-Italic="true" runat="server" />.
        If the details are correct please submit the order below. You can add more items
        to the cart by clicking on edit order, below.
        <br />
        <br />
        <!-- SHOW ORDER DETAILS -->
        <strong>PO Number:</strong>&nbsp;&nbsp;<asp:Label ID="lblPONum" runat="server" Visible="true"
            Font-Bold="true" /><br />
        <br />
        <asp:GridView runat="server" ID="gvOrderConfirm" AutoGenerateColumns="false" EmptyDataText="There is nothing in your shopping cart."
            GridLines="None" Width="100%" CellPadding="5" ShowFooter="true" DataKeyNames="ItemNum"
            OnRowDataBound="gvOrderConfirm_RowDataBound" OnRowCommand="gvOrderConfirm_RowCommand">
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
                        <asp:LinkButton runat="server" ID="btnUpdateCart" Visible="false" Enabled="false"
                            Text="Update" CommandName="Fix" CommandArgument='<%# Eval("ItemNum") %>' Style="font-size: 12px;"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalPrice" HeaderText="Total" ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnRemove" Visible="false" Enabled="false" Text="Remove"
                            CommandName="Remove" CommandArgument='<%# Eval("ItemNum") %>' Style="font-size: 12px;"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Font-Italic="True"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlShipping" runat="server" Visible="false">
                        <!-- SHIPPING SECTION -->
                        <table align="center" width="650">
                            <tr>
                                <td valign="top" align="center">
                                    <img src="../images/truck.gif" alt="UPS Truck" /><strong>UPS Shipping Options:</strong>
                                    <span class="textsmall">(Required for Dropship orders only)</span>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" style="border-style: solid; border-width: 1px; border-color: #7E7E7E;">
                                        <tr class="upsHeader">
                                            <td>&nbsp;</td>
                                            <td><strong>Level of Service</strong></td>
                                            <td><strong>Estimated Cost</strong></td>
                                            <td><strong>Estimated Time of Delivery</strong></td>
                                        </tr>
                                        <tr class="upsRowOdd">
                                            <td align="right">
                                                <asp:CheckBox ID="chkb1Day" runat="server" TextAlign="Right" /></td>
                                            <td>UPS Next Day Air</td>
                                            <td>
                                                <asp:Label ID="lbl1DayCost" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl1DayTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="upsRowEven">
                                            <td align="right">
                                                <asp:CheckBox ID="chkb2Day" runat="server" TextAlign="Right" /></td>
                                            <td>UPS Second Day Air</td>
                                            <td>
                                                <asp:Label ID="lbl2DayCost" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl2DayTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="upsRowOdd">
                                            <td align="right">
                                                <asp:CheckBox ID="chkb3Day" runat="server" TextAlign="Right" /></td>
                                            <td>UPS 3rd Day Select</td>
                                            <td>
                                                <asp:Label ID="lbl3DayCost" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl3DayTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="upsRowEven">
                                            <td align="right">
                                                <asp:CheckBox ID="chkbGrnd" runat="server" TextAlign="Right" Checked-="true" />
                                            </td>
                                            <td>UPS Ground</td>
                                            <td>
                                                <asp:Label ID="lblGrndCost" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblGrndTime" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblReturnString" runat="server" Visible="false"></asp:Label><asp:Label
                                        ID="lblFreightCollectAccount" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <!-- END SHIPPING SECTION -->
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="buttons" Visible="true" runat="server">
                        <table align="left">
                            <tr>
                                <td align="left" width="165">New Order<asp:ImageButton ID="btnNewOrder" runat="server"
                                    ImageUrl="~/images/new_order.png" AlternateTex="Create New Order!" OnClick="btnNewOrder_Click" />
                                </td>
                                <td align="left" width="165">View Order<asp:ImageButton ID="btnViewOrders" runat="server"
                                    ImageUrl="~/images/view_orders.png" AlternateTex="Find Existing Order!" /></td>
                                <td align="left" width="165">Edit order<asp:ImageButton ID="btnEditOrder" runat="server"
                                    ImageUrl="~/images/edit_order.png" AlternateTex="Edit This Order!" OnClick="btnEditOrder_Click" />
                                </td>
                                <td align="left" width="165">Delete order<asp:ImageButton ID="btnDeleteOrder" runat="server"
                                    ImageUrl="~/images/delete_order.png" AlternateTex="Delete This Order!" OnClick="btnDeleteOrder_Click"
                                    OnClientClick="return confirm('Are you sure want to delete this order?')" />
                                </td>
                                <td align="right">Submit order<asp:ImageButton ID="btnSubmitOrder" runat="server"
                                    ImageUrl="~/images/submit_order.png" OnClick="btnSubmitOrder_Click" AlternateTex="Submit Order for Processing!"
                                    OnClientClick="return confirm('Do you want to submit this order?  This order will be processed and shipped.')" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlHidden" runat="server" Visible="false">
                        <table width="50%" align="left">
                            <tr>
                                <td align="right" width="100">GUID:</td>
                                <td align="left">
                                    <asp:Label ID="lblGuid" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">CustomerID:</td>
                                <td align="left">
                                    <asp:Label ID="lblCustomerID" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">HeaderOrderID:</td>
                                <td align="left">
                                    <asp:Label ID="lblHeaderOrderID" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">PO Number:</td>
                                <td align="left">
                                    <asp:Label ID="lblPONumber" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">Dropship:</td>
                                <td align="left">
                                    <asp:Label ID="lblDropship" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">Freight Collect:</td>
                                <td align="left">
                                    <asp:Label ID="lblFreightCollect" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">Carrier:</td>
                                <td align="left">
                                    <asp:Label ID="lblCarrier" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">Account#:</td>
                                <td align="left">
                                    <asp:Label ID="lblAccountNum" runat="server" Visible="true" /></td>
                            </tr>
                            <tr>
                                <td align="right" width="100">Error Message:</td>
                                <td align="left">
                                    <asp:Label ID="lblErrorMessage" runat="server" Visible="true" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
        <!-- END ORDER DETAILS -->
        <!-- HIDDEN FIELDS -->
        <!-- END HIDDEN FIELDS -->
    </div>
</asp:Content>


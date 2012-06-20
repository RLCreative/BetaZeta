<%@ Page Title="Create an order" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_createorder" CodeBehind="createorder.aspx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<link rel="stylesheet" type="text/css" href="css/loggedinstyles.css" />--%>
    <div id="retailersview">
        <h1>Add Items to Your Order:</h1>
        <table width="900">
            <tr>
                <td width="125"><strong>Item#</strong></td>
                <td width="300"><strong>Description</strong></td>
                <td width="75"><strong>Price</strong></td>
                <td width="100"><strong>Quantity</strong></td>
                <td width="80" align="center"><strong>Add to Cart</strong></td>
                <td width="100"><strong></strong></td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:TextBox ID="txtItemNum" runat="server" AutoPostBack="True" OnTextChanged="txtItemNum_TextChanged"></asp:TextBox>
                </td>
                <cc1:AutoCompleteExtender ID="aceItemNumber" runat="server" TargetControlID="txtItemNum"
                    ServicePath="../Autocomplete.asmx" ServiceMethod="AutoCompleteItemNum" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>
                <td style="vertical-align: top;">
                    <asp:TextBox ID="txtDescription" Width="300" runat="server" AutoPostBack="True" OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
                </td>
                <cc1:AutoCompleteExtender ID="aceDescription" runat="server" TargetControlID="txtDescription"
                    ServicePath="../Autocomplete.asmx" ServiceMethod="AutoCompleteDescription" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>
                <td style="vertical-align: top;">
                    <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                <td style="vertical-align: top;">
                    <asp:TextBox ID="txtQty" Width="35" runat="server"></asp:TextBox>
                </td>
                <td style="vertical-align: top;">
                    <asp:ImageButton ID="btnAddToCart" runat="server" ImageUrl="~/images/add_to_cart.png"
                        OnClick="btnAddToCart_Click" /></td>
                <td style="vertical-align: top;">
                    <asp:Button Width="85" ID="btnClear" runat="server" Text="Clear Values" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="The quantity value can not be zero!"
            ControlToValidate="txtQty" MinimumValue="1" MaximumValue="9999"></asp:RangeValidator>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="popupControl" Visible="false">
            <table>
                <tr>
                    <td align="left"></td>
                    <td align="right">
                        <asp:Label ID="lblDupes" ForeColor="Firebrick" runat="server"></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:GridView runat="server" ID="gvShoppingCart" AutoGenerateColumns="false" EmptyDataText="There is nothing in your shopping cart."
            GridLines="None" Width="100%" CellPadding="5" ShowFooter="true" DataKeyNames="ItemNum"
            OnRowDataBound="gvShoppingCart_RowDataBound" OnRowCommand="gvShoppingCart_RowCommand">
            <HeaderStyle HorizontalAlign="Left" BackColor="#7E7E7E" ForeColor="#FFFFFF" />
            <FooterStyle HorizontalAlign="Right" BackColor="#7E7E7E" ForeColor="#FFFFFF" />
            
            <Columns>
                <asp:BoundField DataField="ItemNum" HeaderText="ItemNum" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtQuantity" Columns="5" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                        <asp:LinkButton runat="server" ID="btnUpdateCart" Text="Update" CommandName="Fix"
                            CommandArgument='<%# Eval("ItemNum") %>' Style="font-size: 12px;"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalPrice" HeaderText="Total" ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnRemove" Text="Remove" CommandName="Remove"
                            CommandArgument='<%# Eval("ItemNum") %>' Style="font-size: 12px;"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <hr />
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMessage" runat="server" Font-Italic="True"></asp:Label></td>
                <td align="right"></td>
            </tr>
        </table>
        <hr />
        <table width="100%">
            <tr>
                <td align="left" width="150">New Cart<asp:ImageButton ID="btnNewCart" runat="server"
                    ImageUrl="~/images/new_cart.png" AlternateText="Create New Cart" OnClick="btnNewCart_Click" />
                </td>
                <td align="left" width="150">Delete Cart<asp:ImageButton ID="btnDeleteCart" runat="server"
                    OnClientClick="return confirm('Are you sure you want to delete this cart?')"
                    ImageUrl="~/images/delete_cart.png" AlternateText="Delete Existing Cart" OnClick="btnDeleteCart_Click" />
                </td>
                <td align="left"></td>
                <td align="right">Save Order<asp:ImageButton ID="btnSaveOrder" runat="server" OnClientClick="return confirm('Do you want to save this order?  You will be sent to a confimation page.')"
                    ImageUrl="~/images/save.png" OnClick="btnSaveOrder_Click" AlternateText="Save Cart" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Panel ID="pnlHidden" runat="server" Visible="false">
            ---- DEVELOPER STUFF ----<br />
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
                    <td align="right" width="100">PO Number:</td>
                    <td align="left">
                        <asp:Label ID="lblPONumber" runat="server" Visible="true" /></td>
                </tr>
                <tr>
                    <td align="right" width="100">HeaderOrderID:</td>
                    <td align="left">
                        <asp:Label ID="lblHeaderOrderID" runat="server" Visible="true" /></td>
                </tr>
                <tr>
                    <td align="right" width="100">DropShip:</td>
                    <td align="left">
                        <asp:Label ID="lblDropShip" runat="server" Visible="true" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

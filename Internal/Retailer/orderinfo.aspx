<%@ Page Title="Review Company/Shipping Information" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_orderinfo" CodeBehind="orderinfo.aspx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../../Content/960_12_col.css" />
    
    <div class="container_12">
    <div id="grid_12">
        <h1>Create New Order</h1>
            To create a new order, Request a Ship Date, and if desired, a date by which to cancel
            the order if it has not shipped.<br />
            &nbsp;<strong>*</strong>Required<br /><br />

    </div>
    <%--<h2>
                Order Details</h2>--%>
    <%--<div class="grid_4">
            <p class="form">
                1. Order Date:*</p>
                <div  style="text-align: center"><asp:Calendar ID="calOrderDate" runat="server" BackColor="White" 
                        CellPadding="5" DayNameFormat="FirstLetter"
                Font-Names="Verdana" Font-Size="8pt" ForeColor="#FFFFFF" Height="180px" Width="250px" FirstDayOfWeek="Sunday"
                SelectMonthText="" BorderColor="#999999" BorderStyle="Solid" DayHeaderStyle-HorizontalAlign="Center">
                <SelectedDayStyle BackColor="Green" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <SelectorStyle BackColor="Green" HorizontalAlign="Center" VerticalAlign="Middle" />
                <WeekendDayStyle BackColor="#FFFFCC" ForeColor="#999999" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <OtherMonthDayStyle ForeColor="Gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                <DayStyle ForeColor="#666666" HorizontalAlign="Center" VerticalAlign="Middle" />
                <NextPrevStyle VerticalAlign="Bottom" HorizontalAlign="Center" />
                <DayHeaderStyle BackColor="Gray" Font-Bold="True" Font-Size="7pt" ForeColor="White" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <TitleStyle BackColor="#257E5C" BorderColor="Black" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:Calendar>
                </div>
        </div>--%>
    
    <div class="grid_4">        
        <p class="form">
            1. Requested Ship Date:<strong>*</strong>
        </p>
        <asp:Calendar ID="calShipDate" runat="server" BackColor="White" CellPadding="5" DayNameFormat="FirstLetter"
            Font-Names="Verdana" Font-Size="8pt" ForeColor="#FFFFFF" Height="180px" Width="250px"
            FirstDayOfWeek="Sunday" SelectMonthText="" BorderColor="#999999">
            <SelectedDayStyle BackColor="#2A2B2C" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#2A2B2C" />
            <WeekendDayStyle BackColor="#FFFFCC" ForeColor="#999999" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <OtherMonthDayStyle ForeColor="Gray" />
            <DayStyle ForeColor="#666666" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <DayHeaderStyle BackColor="Gray" Font-Bold="True" Font-Size="7pt" ForeColor="White" />
            <TitleStyle BackColor="#2A2B2C" BorderColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:Calendar>
    </div>
    <div class="grid_4">
        <p class="form">
            2. Cancel by Date:
        </p>
        <asp:Calendar ID="calCancelDate" runat="server" BackColor="White" CellPadding="5"
            DayNameFormat="FirstLetter" Font-Names="Verdana" Font-Size="8pt" ForeColor="#FFFFFF"
            Height="180px" Width="250px" FirstDayOfWeek="Sunday" SelectMonthText="" BorderColor="#999999">
            <SelectedDayStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="" />
            <WeekendDayStyle BackColor="#FFFFCC" ForeColor="#999999" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <OtherMonthDayStyle ForeColor="Gray" />
            <DayStyle ForeColor="#666666" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <DayHeaderStyle BackColor="Gray" Font-Bold="True" Font-Size="7pt" ForeColor="White" />
            <TitleStyle BackColor="#2A2B2C" BorderColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:Calendar>
    </div>
    <hr class="clear" />
    <div class="grid_12">
        <p class="form">
            PO Number*
            <asp:TextBox ID="txtPONum" runat="server" Width="196px" valign="top" class="textbox" />
            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvPONum" ControlToValidate="txtPONum"
                runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </p>
        <p class="form">
            Ordered By
            <asp:TextBox ID="txtOrderedBy" runat="server" Width="200" valign="top" class="textbox" />
        </p>
        <p class="form">
            Special Instructions<br />
            <asp:TextBox ID="txtComments" runat="server" Width="282px" Wrap="true" MaxLength="250"
                TextMode="MultiLine" Rows="3" valign="top" class="textbox" />
        </p>
    </div>
    <hr />
    <div class="grid_12">
        <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Panel ID="pnlRetailer" runat="server">
            <h2>Shipping Methods</h2>
            Select a ship to address below for your order.
            
            <asp:GridView ID="gvShippingAddress" runat="server" AutoGenerateColumns="False" Width="100%"
                CellPadding="10" DataKeyNames="CUSTNMBR,ADRSCODE" DataSourceID="ShippingAddresses"
                ForeColor="White" GridLines="None" OnRowCommand="gvShippingAddress_RowCommand">
                <RowStyle BackColor="transparent" ForeColor="White" />
                <Columns>
                    <asp:TemplateField SortExpression="AddressTotal" HeaderText="Address:" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblGVAddress1" runat="server" Text='<%# Bind("Address1") %>' />
                            <asp:Label ID="lblGVAddressLine2" runat="server" Text='<%# Bind("Address2") %>' /><br />
                            <asp:Label ID="lblGVCity" runat="server" Text='<%# Bind("City") %>' />,
                            <asp:Label ID="lblGVState" runat="server" Text='<%# Bind("State") %>' />
                            <asp:Label ID="lblGVZip" runat="server" Text='<%# Bind("Zip") %>' />
                            <asp:Label ID="lblGVCountry" runat="server" Text='<%# Bind("Country") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SHIPMTHD" HeaderStyle-Font-Underline="true" HeaderText="Default Shipping Method:"
                        HeaderStyle-HorizontalAlign="Left" SortExpression="SHIPMTHD" />
                    <asp:ButtonField ButtonType="Button" ImageUrl="~/images/go.png" HeaderText="" Text="Continue..."
                        CommandName="CreateOrder" ControlStyle-CssClass="btnsubmitchart" />
                </Columns>
                <FooterStyle BackColor="transparent" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="transparent" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="transparent" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="transparent" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="transparent" />
                <AlternatingRowStyle BackColor="transparent" ForeColor="#284775" />
            </asp:GridView>
            <asp:SqlDataSource ID="ShippingAddresses" runat="server" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                SelectCommand="SOP_GetRetailerShippingAddresses" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="CustomerID" SessionField="CustomerID" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <hr />
            
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDropShipHeader" Visible="true">
            <h3>Drop Shipping</h3>
            <asp:CheckBox ID="ckbDropShip" runat="server" Text="  Choose to Drop Ship This Order"
                AutoPostBack="true" EnableViewState="true" Font-Bold="false" OnCheckedChanged="ckbDropShip_CheckedChanged"
                Visible="true" CssClass="form" ForeColor="White" /><br /><br />
        </asp:Panel>
        <asp:Panel ID="pnlDropShip" runat="server" Visible="false">
            <asp:Label ID="lblSendError" runat="server" CssClass="input" Font-Bold="True" ForeColor="Red"></asp:Label>
            <asp:Panel ID="pnlForm" runat="server" EnableViewState="False">
                <asp:ValidationSummary EnableClientScript="false" ID="ValidationSummary1" runat="server"
                    Font-Bold="True" CssClass="textmain" HeaderText="Please correct errors and try again!"
                    Height="25px"></asp:ValidationSummary>
                <h2>Drop Ship Address</h2>
                <p>
                    Enter the address you would like your order drop shipped to. <strong>*</strong>Required
                </p>
                <table id="Table2" cellspacing="0" cellpadding="5" width="100%" border="0">
                    <tr>
                        <td class="style1">First&nbsp;Name* </td>
                        <td width="80%">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Columns="30" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvFirstName" runat="server"
                                CssClass="textsmall" ControlToValidate="txtFirstName">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Last&nbsp;Name* </td>
                        <td width="80%">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Columns="30" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvLastName" runat="server"
                                CssClass="textsmall" ControlToValidate="txtLastName">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Address* </td>
                        <td width="80%">
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox" Columns="30" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvAddress1" runat="server"
                                CssClass="textsmall" ControlToValidate="txtAddress1">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Apt./Suite* </td>
                        <td width="80%">
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox" Columns="30" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">City* </td>
                        <td width="80%">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" Columns="20" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvCity" runat="server"
                                CssClass="textsmall" ControlToValidate="txtCity">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">State/Province* </td>
                        <td width="80%">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="textbox">
                                <asp:ListItem Value="0" Selected="True">-- Select --</asp:ListItem>
                                <asp:ListItem Value="AL">AL</asp:ListItem>
                                <asp:ListItem Value="AK">AK</asp:ListItem>
                                <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                <asp:ListItem Value="AR">AR</asp:ListItem>
                                <asp:ListItem Value="CA">CA</asp:ListItem>
                                <asp:ListItem Value="CO">CO</asp:ListItem>
                                <asp:ListItem Value="CT">CT</asp:ListItem>
                                <asp:ListItem Value="DC">DC</asp:ListItem>
                                <asp:ListItem Value="DE">DE</asp:ListItem>
                                <asp:ListItem Value="FL">FL</asp:ListItem>
                                <asp:ListItem Value="GA">GA</asp:ListItem>
                                <asp:ListItem Value="HI">HI</asp:ListItem>
                                <asp:ListItem Value="ID">ID</asp:ListItem>
                                <asp:ListItem Value="IL">IL</asp:ListItem>
                                <asp:ListItem Value="IN">IN</asp:ListItem>
                                <asp:ListItem Value="IA">IA</asp:ListItem>
                                <asp:ListItem Value="KS">KS</asp:ListItem>
                                <asp:ListItem Value="KY">KY</asp:ListItem>
                                <asp:ListItem Value="LA">LA</asp:ListItem>
                                <asp:ListItem Value="MA">MA</asp:ListItem>
                                <asp:ListItem Value="MD">MD</asp:ListItem>
                                <asp:ListItem Value="ME">ME</asp:ListItem>
                                <asp:ListItem Value="MI">MI</asp:ListItem>
                                <asp:ListItem Value="MN">MN</asp:ListItem>
                                <asp:ListItem Value="MO">MO</asp:ListItem>
                                <asp:ListItem Value="MS">MS</asp:ListItem>
                                <asp:ListItem Value="MT">MT</asp:ListItem>
                                <asp:ListItem Value="NE">NE</asp:ListItem>
                                <asp:ListItem Value="NV">NV</asp:ListItem>
                                <asp:ListItem Value="NH">NH</asp:ListItem>
                                <asp:ListItem Value="NJ">NJ</asp:ListItem>
                                <asp:ListItem Value="NM">NM</asp:ListItem>
                                <asp:ListItem Value="NY">NY</asp:ListItem>
                                <asp:ListItem Value="NC">NC</asp:ListItem>
                                <asp:ListItem Value="ND">ND</asp:ListItem>
                                <asp:ListItem Value="OH">OH</asp:ListItem>
                                <asp:ListItem Value="OK">OK</asp:ListItem>
                                <asp:ListItem Value="OR">OR</asp:ListItem>
                                <asp:ListItem Value="PA">PA</asp:ListItem>
                                <asp:ListItem Value="RI">RI</asp:ListItem>
                                <asp:ListItem Value="SC">SC</asp:ListItem>
                                <asp:ListItem Value="SD">SD</asp:ListItem>
                                <asp:ListItem Value="TN">TN</asp:ListItem>
                                <asp:ListItem Value="TX">TX</asp:ListItem>
                                <asp:ListItem Value="UT">UT</asp:ListItem>
                                <asp:ListItem Value="VT">VT</asp:ListItem>
                                <asp:ListItem Value="VA">VA</asp:ListItem>
                                <asp:ListItem Value="WA">WA</asp:ListItem>
                                <asp:ListItem Value="WV">WV</asp:ListItem>
                                <asp:ListItem Value="WI">WI</asp:ListItem>
                                <asp:ListItem Value="WY">WY</asp:ListItem>
                                <asp:ListItem Value="AB">Canada-AB</asp:ListItem>
                                <asp:ListItem Value="BC">Canada-BC</asp:ListItem>
                                <asp:ListItem Value="MB">Canada-MB</asp:ListItem>
                                <asp:ListItem Value="NB">Canada-NB</asp:ListItem>
                                <asp:ListItem Value="NF">Canada-NF</asp:ListItem>
                                <asp:ListItem Value="NS">Canada-NS</asp:ListItem>
                                <asp:ListItem Value="NT">Canada-NT</asp:ListItem>
                                <asp:ListItem Value="ON">Canada-ON</asp:ListItem>
                                <asp:ListItem Value="QC">Canada-QC</asp:ListItem>
                                <asp:ListItem Value="SK">Canada-SK</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvState" runat="server"
                                CssClass="textsmall" ControlToValidate="ddlState" InitialValue="0">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">ZIP/Postal Code </td>
                        <td width="80%">
                            <asp:TextBox ID="txtZip" runat="server" CssClass="textbox" Columns="9" MaxLength="7"></asp:TextBox>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvZip" runat="server"
                                CssClass="textsmall" ControlToValidate="txtZip" Display="Dynamic">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator EnableClientScript="false" ID="revZip" runat="server"
                                CssClass="textsmall" ControlToValidate="txtZip" Display="Dynamic" ValidationExpression="^(\d{5}|[a-zA-Z][0-9][a-zA-Z]( ){0,1}[0-9][a-zA-Z][0-9])$">Invalid</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Country </td>
                        <td width="80%">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textbox">
                                <asp:ListItem Value="0" Selected="True">-- Select --</asp:ListItem>
                                <asp:ListItem Value="CA">Canada</asp:ListItem>
                                <asp:ListItem Value="US">United States</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvCountry" runat="server"
                                CssClass="textsmall" ControlToValidate="ddlCountry" InitialValue="0">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Phone </td>
                        <td width="80%">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" Columns="20" MaxLength="10"></asp:TextBox><span
                                class="textsmallnocolor">&nbsp;Numbers Only
                                <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvPhone" runat="server"
                                    CssClass="textsmall" ControlToValidate="txtPhone" Display="Dynamic">Required</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator EnableClientScript="false" ID="revPhone" runat="server"
                                    CssClass="textsmall" ControlToValidate="txtPhone" Display="Dynamic" ValidationExpression="^(\d{10})$">Invalid</asp:RegularExpressionValidator>
                            </span></td>
                    </tr>
                    <tr>
                        <td class="style1"></td>
                        <td align="left" colspan="2" width="80%">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btnsubmitchart" Text="Continue..."
                                OnClick="btnSubmit_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblFormSpecialType" runat="server" Text="N/A" Visible="false"></asp:Label>
        </asp:Panel>

    </div>
        </div>
</asp:Content>
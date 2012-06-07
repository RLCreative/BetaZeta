<%@ Page Title="View Pending/Processed orders" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_vieworders" CodeBehind="vieworders.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="grid_12">
        <h1>Retailer Dashboard</h1>
        <h2>Your Information:</h2>
        <p>
            Please review your company information below. <strong>If you are on hold you may submit
                an order but it will not be processed.</strong>If you have questions, please
            call Customer Care at (800) 593-5522.
        </p>
        <asp:SqlDataSource ID="GetRetailerInfo" runat="server" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
            SelectCommand="SOP_GetRetailerInfo" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="CustomerID" SessionField="CustomerID"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:FormView ID="fvRetailer" runat="server" CellPadding="0" Width="100%" DataKeyNames="CUSTNMBR"
            DataSourceID="GetRetailerInfo">
            <HeaderStyle BackColor="transparent" Font-Bold="false" />
            <FooterStyle BackColor="transparent" ForeColor="" />
            <RowStyle BackColor="transparent" ForeColor="" />
            <ItemTemplate>
                <div class="grid_3">
                    <table align="left">
                        <tr>
                            <td width="120" align="right"><b>Company:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("CUSTNAME") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td width="120" align="right"><b>Contact:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblContact" runat="server" Text='<%# Bind("CNTCPRSN") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td width="100" align="right"><b>Phone:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Fax:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Shipping Method:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblShipMethod" runat="server" Text='<%# Bind("SHIPMTHD") %>' />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="grid_3">
                    <table align="left">
                        <tr>
                            <td align="right"><b>Address:</b></td>
                            <td align="right"></td>
                            <td>
                                <asp:Label ID="lblFVAddress1" runat="server" Text='<%# Bind("ADDRESS1") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>City:</b></td>
                            <td align="right"></td>
                            <td>
                                <asp:Label ID="lblFVAddress2" runat="server" Text='<%# Bind("CITY") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>State:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblFVState" runat="server" Text='<%# Bind("STATE") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Zip:</b></td>
                            <td align="right"></td>
                            <td>
                                <asp:Label ID="lblFVZip" runat="server" Text='<%# Bind("ZIP") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Country:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblFVCountry" runat="server" Text='<%# Bind("Country") %>' />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="grid_2">
                    <table align="left">
                        <tr>
                            <td align="right"><b>Price Level:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:Label ID="lblPriceLevel" runat="server" Text='<%# Bind("PRCLEVEL") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>InActive:</b></td>
                            <td align="right"></td>
                            <td>
                                <asp:CheckBox Enabled="false" ID="ckbActive" runat="server" Checked='<%# Bind("Inactive") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>On Hold:</b></td>
                            <td width="5" align="right"></td>
                            <td>
                                <asp:CheckBox Enabled="false" ID="ckbOnHol" runat="server" Checked='<%# Bind("OnHold") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>&nbsp;</b></td>
                            <td align="right"></td>
                            <td>&nbsp; </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
            <PagerStyle BackColor="transparent" ForeColor="" />
            <HeaderStyle BackColor="transparent" Font-Bold="True" ForeColor="" />
            <EditRowStyle BackColor="" />
        </asp:FormView>
    </div>
    <div class="grid_12">
        <hr />
        <h2>Current Orders:</h2>
        <p>
            More detail about your confirmed orders is available by simply clicking on the (<strong>>
            </strong>) on the left hand side of the row.
        </p>
        <br />
        <h4>Pending Orders:</h4>
        
            
                <!-- PENDING ORDERS -->
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgPending">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgPending" LoadingPanelID="RadAjaxLoadingPanel1" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="rgConfirmed">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgConfirmed" LoadingPanelID="RadAjaxLoadingPanel2" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px"
                    Width="75px" Transparency="3" MinDisplayTime="0">
                    <img style="margin-top: 25px;" alt="Loading..." src="../images/loading.gif" />
                </telerik:RadAjaxLoadingPanel>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Height="75px"
                    Width="75px" Transparency="3" MinDisplayTime="0">
                    <img style="margin-top: 25px;" alt="Loading..." src="../images/loading.gif" />
                </telerik:RadAjaxLoadingPanel>
                <telerik:RadGrid runat="server" AllowPaging="True" AllowSorting="True" ID="rgPending"
                    DataSourceID="PendingOrders" GridLines="None" Skin="Telerik" AllowFilteringByColumn="True"
                    OnItemCommand="rgPending_ItemCommand">
                    <HeaderContextMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </HeaderContextMenu>
                    <MasterTableView DataSourceID="PendingOrders" AutoGenerateColumns="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridButtonColumn HeaderText="View Order" Text="Click to View" CommandName="Select">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="CartName" HeaderText="PO Number" SortExpression="CartName"
                                UniqueName="CartName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Updated" DataType="System.DateTime" HeaderText="Last Updated"
                                SortExpression="Updated" UniqueName="Updated">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GUID" DataType="System.Guid" HeaderText="Shopping Cart ID"
                                SortExpression="GUID" UniqueName="GUID">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn Text="Delete" HeaderText="Delete Order" CommandName="Delete">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" HeaderText="OrderID"
                                SortExpression="OrderID" UniqueName="OrderID" Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
                <asp:SqlDataSource ID="PendingOrders" runat="server" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                    SelectCommand="SELECT  SOPShoppingCartInfo.CustomerID, SOPShoppingCartInfo.GUID, SOPShoppingCartInfo.CartName, SOPShoppingCartInfo.Active, SOPShoppingCartInfo.Updated, SOPShoppingCartHeader.OrderID
                       FROM SOPShoppingCartInfo INNER JOIN
                            SOPShoppingCartHeader ON SOPShoppingCartInfo.CartName = SOPShoppingCartHeader.PONum
                       WHERE (([CustomerID] = @CustomerID) AND Active = '1') 
                       Order by [Updated]">
                    <SelectParameters>
                        <asp:SessionParameter Name="CustomerID" SessionField="CustomerID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <!-- CONFIRMED ORDERS -->
                <br />
                <h4>Confirmed Orders:</h4>
                <telerik:RadGrid runat="server" ID="rgConfirmed" AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" ShowStatusBar="True" DataSourceID="DropShipOrders" GridLines="None"
                    PageSize="12" Skin="Telerik" AutoGenerateColumns="False">
                    <HeaderContextMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </HeaderContextMenu>
                    <MasterTableView DataKeyNames="PDFRefNum" DataSourceID="DropShipOrders">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <NestedViewTemplate>
                            <fieldset style="padding: 5px;">
                                <legend style="padding: 5px;"><b>Order Details: PDF Order </b>
                                    <asp:Label ID="lblPDFRefNum" Font-Bold="true" Text='<%# Eval("PDFRefNum") %>' Visible="True"
                                        runat="server" />
                                </legend>
                                <asp:SqlDataSource ID="DropShipOrdersDetail" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT     SOPPDFHeader.PDFRefNum, SOPPDFHeader.ReceivedDateTime, SOPPDFHeader.FormSpecialType, SOPPDFHeader.CustomerNum, SOPPDFHeader.PONum, 
                                          SOPPDFHeader.OrderDate, SOPPDFHeader.OrderedBy, SOPPDFHeader.Company, SOPPDFHeader.Address, SOPPDFHeader.City, SOPPDFHeader.Country, 
                                          SOPPDFHeader.StateProv, SOPPDFHeader.ZipCode, SOPPDFHeader.Phone, SOPPDFHeader.Comments, CAST(SOPPDFHeader.OrderTotal AS Decimal(10, 2)) 
                                          AS OrderTotal, SOPPDFHeader.Fax, 
                                          CASE SOPPDFHeader.Status WHEN 'P' THEN 'Processed' WHEN 'E' THEN 'A Error Occurred' WHEN 'N' THEN 'Waiting to be Processed' WHEN 'X' THEN 'Testing Order' END AS Status, 
                                          SOPPDFResults.GPOrderID, SOPPDFResults.IntegrationDate, SOPPDFResults.EmailSent, 
                                          CASE SOPPDFHeader.Dropship WHEN 'True' THEN 'Yes' WHEN 'False' THEN 'No' END as Dropship, SOPPDFHeader.ShippingMethod
                                          FROM         SOPPDFHeader LEFT OUTER JOIN
                                          province ON SOPPDFHeader.StateProv = province.name FULL OUTER JOIN
                                          SOPPDFResults ON SOPPDFHeader.SOPPDFResultsID = SOPPDFResults.SOPPDFResultsID
                                          WHERE     (SOPPDFHeader.PDFRefNum = @PDFRefNum)" runat="server">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblPDFRefNum" PropertyName="Text" Type="String"
                                            Name="PDFRefNum" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:FormView ID="FormView1" AllowPaging="true" GridLines="None" Width="100%" DataSourceID="DropShipOrdersDetail"
                                    runat="server">
                                    <ItemTemplate>
                                        <table class="outerTable" style="font-size: 1.2em;">
                                            <tr>
                                                <td>
                                                    <table class="innerTable">
                                                        <tr>
                                                            <td class="innerTableLeftTD">CustomerNum: </td>
                                                            <td>
                                                                <asp:Label ID="lblCustomerNum" runat="server" Text='<%# Eval("CustomerNum") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">FormSpecialType: </td>
                                                            <td>
                                                                <asp:Label ID="lblFormSpecialType" runat="server" Text='<%# Eval("FormSpecialType") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">DateReceived: </td>
                                                            <td>
                                                                <asp:Label ID="lblReceivedDateTime" runat="server" Text='<%# Eval("ReceivedDateTime") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">PONum: </td>
                                                            <td>
                                                                <asp:Label ID="lblPONum" runat="server" Text='<%# Eval("PONum") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">OrderTotal: </td>
                                                            <td>$
                                                                <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderTotal") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">OrderedBy: </td>
                                                            <td>
                                                                <asp:Label ID="lblOrderedBy" runat="server" Text='<%# Eval("OrderedBy") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table class="innerTable">
                                                        <tr>
                                                            <td class="innerTableLeftTD">Company: </td>
                                                            <td>
                                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Address: </td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">City/St/Zip: </td>
                                                            <td>
                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' />,
                                                                <asp:Label ID="lblStateProv" runat="server" Text='<%# Eval("StateProv") %>' />
                                                                <asp:Label ID="lblZipCode" runat="server" Text='<%# Eval("ZipCode") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Country: </td>
                                                            <td>
                                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Phone: </td>
                                                            <td>
                                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Fax: </td>
                                                            <td>
                                                                <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table class="innerTable">
                                                        <tr>
                                                            <td class="innerTableLeftTD">Status: </td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">GPOrderID: </td>
                                                            <td>
                                                                <asp:Label ID="lblGPOrderID" runat="server" Text='<%# Eval("GPOrderID") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">IntegrationDate: </td>
                                                            <td>
                                                                <asp:Label ID="lblIntegrationDate" runat="server" Text='<%# Eval("IntegrationDate") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">EmailSent: </td>
                                                            <td>
                                                                <asp:Label ID="lblEmailSent" runat="server" Text='<%# Eval("EmailSent") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Comments: </td>
                                                            <td>
                                                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>' /><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Dropship Order: </td>
                                                            <td>
                                                                <asp:Label ID="lblDropShip" runat="server" Text='<%# Eval("Dropship") %>' /><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="innerTableLeftTD">Dropship Method: </td>
                                                            <td>
                                                                <asp:Label ID="lblShippingMethod" runat="server" Text='<%# Eval("ShippingMethod") %>' /><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 90px;">
                                                    <asp:HyperLink ID="hypBack" runat="server" ImageUrl="~/images/enlarge.gif" NavigateUrl='<%# Eval("PDFRefNum","orderdetails.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                    <asp:HyperLink ID="hypBackText" runat="server" Text="View Details" NavigateUrl='<%# Eval("PDFRefNum","orderdetails.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                </td>
                                                <td style="padding-left: 90px;">
                                                    <asp:HyperLink ID="hypPrint" runat="server" ImageUrl="~/images/icon-print.gif" NavigateUrl='<%# Eval("PDFRefNum","printorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                    <asp:HyperLink ID="hypPrintText" runat="server" Text="Print Order" NavigateUrl='<%# Eval("PDFRefNum","printorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                </td>
                                                <td style="padding-left: 90px;">
                                                    <asp:HyperLink ID="hypTrack" runat="server" ImageUrl="~/images/41.png" NavigateUrl='<%# Eval("PDFRefNum","trackorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                    <asp:HyperLink ID="hypTrackText" runat="server" Text="Track Order" NavigateUrl='<%# Eval("PDFRefNum","trackorders.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </fieldset>
                        </NestedViewTemplate>
                        <Columns>
                            <telerik:GridBoundColumn DataField="PDFRefNum" HeaderText="Reference#" ReadOnly="True"
                                SortExpression="PDFRefNum" UniqueName="PDFRefNum" DataType="System.Int32">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerNum" HeaderText="Customer#" SortExpression="CustomerNum"
                                UniqueName="CustomerNum">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PONum" HeaderText="PO Number" SortExpression="PONum"
                                UniqueName="PONum">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DateReceived" DataType="System.DateTime" HeaderText="Date Received"
                                SortExpression="DateReceived" UniqueName="DateReceived">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Company" HeaderText="Company" SortExpression="Company"
                                UniqueName="Company">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Total" DataType="System.Decimal" HeaderText="Total"
                                ReadOnly="True" SortExpression="Total" UniqueName="Total">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                UniqueName="Status">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
                <asp:SqlDataSource ID="DropShipOrders" runat="server" ConnectionString="<%$ ConnectionStrings:RLConnectString %>"
                    SelectCommand="SELECT PDFRefNum, CustomerNum, PONum, ReceivedDateTime AS DateReceived, Company, CAST(OrderTotal AS Decimal(10 , 2)) AS Total, 
                    CASE Status WHEN 'P' THEN 'Processed'
                                WHEN 'E' THEN 'Error'
                                WHEN 'N' THEN 'New' END as Status 
                    FROM SOPPDFHeader 
                    WHERE ([CustomerNum] = @CustomerID)
                    ORDER BY DateReceived DESC">
                    <SelectParameters>
                        <asp:SessionParameter Name="CustomerID" SessionField="CustomerID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="DropShipData" runat="server" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                    SelectCommand="SELECT dbo.wsPF_ProductExtraInfo.ProductID, dbo.wsPF_ProductExtraInfo.Name, 
                    dbo.wsPF_Brands.Name AS Brand,  dbo.wsPF_ProductExtraInfo.webpage as Webpage
                    FROM dbo.wsPF_Brands INNER JOIN dbo.wsPF_ProductExtraInfo ON dbo.wsPF_Brands.BrandID = dbo.wsPF_ProductExtraInfo.Brand 
                    WHERE (dbo.wsPF_ProductExtraInfo.DisplayOnOrderForm = 1) 
                    ORDER BY dbo.wsPF_ProductExtraInfo.ProductID"></asp:SqlDataSource>
                </div>                 
                <br />
                <br />
</asp:Content> 
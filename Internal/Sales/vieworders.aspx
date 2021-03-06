<%@ Page Title="Pending::Confirmed orders" Language="C#" MasterPageFile="~/internal/internal.master" AutoEventWireup="true" Inherits="sales_vieworders" CodeBehind="vieworders.aspx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="salerepview">
<h1>Rep Dashboard</h1>
<h2><strong>View Orders:</strong></h2>
<img id="Img1" src="~/images/right.gif" alt="" runat="server" />Use this page to view both pending/confirmed orders.<br />
<img id="Img2" src="~/images/right.gif" alt="" runat="server" />You can see more details about your confirmed orders by simply clicking 
on the (<strong>></strong>) on the left hand side of the row.
<h4><strong>Pending Orders:</strong></h4>
<!-- PENDING ORDERS -->
<telerik:RadAjaxManager runat="server">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="rgPending">
            <updatedcontrols>                
                <telerik:AjaxUpdatedControl ControlID="rgPending" LoadingPanelID="RadAjaxLoadingPanel1" />                
            </updatedcontrols>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgConfirmed">
            <updatedcontrols>                
                <telerik:AjaxUpdatedControl ControlID="rgConfirmed" LoadingPanelID="RadAjaxLoadingPanel2" />
            </updatedcontrols>
        </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px"
        Width="75px" Transparency="3" MinDisplayTime="0">
        <img style="margin-top: 25px;" alt="Loading..." src="../images/loading.gif" />
</telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Height="75px"
        Width="75px" Transparency="3" MinDisplayTime="0">
        <img style="margin-top: 25px;" alt="Loading..." src="../images/loading.gif" />
</telerik:RadAjaxLoadingPanel>
<telerik:radgrid id="rgPending" runat="server" AllowPaging="True" AllowSorting="True" 
        DataSourceID="PendingOrders" GridLines="None" 
        Skin="Telerik" 
        onitemcommand="rgPending_ItemCommand" >
    <headercontextmenu enabletheming="True">
        <collapseanimation duration="200" type="OutQuint" />
    </headercontextmenu>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
        <ClientEvents OnRowSelected="RowSelected" />
    </ClientSettings>
    <mastertableview datasourceid="PendingOrders" autogeneratecolumns="False">
        <rowindicatorcolumn>
            <HeaderStyle Width="20px" />
        </rowindicatorcolumn>
        <expandcollapsecolumn>
            <HeaderStyle Width="20px" />
        </expandcollapsecolumn>
        <Columns>
            <telerik:GridButtonColumn HeaderText="View Order" Text="Click to View" CommandName="Select">
            </telerik:GridButtonColumn>
            <telerik:GridBoundColumn DataField="CustomerNum" HeaderText="Customer#" 
                SortExpression="CustomerNum" UniqueName="CustomerNum">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Company" HeaderText="Company" 
                SortExpression="Company" UniqueName="Company">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CartName" HeaderText="PO Number" 
                SortExpression="CartName" UniqueName="CartName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Updated" DataType="System.DateTime" 
                HeaderText="Last Updated" SortExpression="Updated" UniqueName="Updated">
            </telerik:GridBoundColumn>                        
            <telerik:GridBoundColumn DataField="GUID" DataType="System.Guid" 
                HeaderText="Shopping Cart ID" SortExpression="GUID" UniqueName="GUID" Visible="false">
            </telerik:GridBoundColumn>            
            <telerik:GridButtonColumn Text="Delete" HeaderText="Delete Order" CommandName="Delete">
            </telerik:GridButtonColumn>
            <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" 
                HeaderText="OrderID" SortExpression="OrderID" UniqueName="OrderID" visible="false">
            </telerik:GridBoundColumn>
        </Columns>
    </mastertableview>
    <filtermenu enabletheming="True">
        <collapseanimation duration="200" type="OutQuint" />
    </filtermenu>
    </telerik:radgrid>
    <asp:SqlDataSource ID="PendingOrders" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RLConnectionString %>" 
        SelectCommand="SELECT  SOPShoppingCartInfo.CustomerID, SOPShoppingCartHeader.CustomerNum, SOPShoppingCartHeader.Company, SOPShoppingCartInfo.GUID, 
                       SOPShoppingCartInfo.CartName, SOPShoppingCartInfo.Active, SOPShoppingCartInfo.Updated, SOPShoppingCartHeader.OrderID
                       FROM SOPShoppingCartInfo INNER JOIN
                       SOPShoppingCartHeader ON SOPShoppingCartInfo.CartName = SOPShoppingCartHeader.PONum
                       WHERE ([CustomerNum] IN (SELECT CUSTNMBR FROM   RM00101 WHERE SLPRSNID = @SalesRep AND INACTIVE = '0'))
                       Order by [Updated]">
        <SelectParameters>
            <asp:SessionParameter Name="SalesRep" SessionField="SalesRep" Type="String" />           
        </SelectParameters>
    </asp:SqlDataSource>
<!-- CONFIRMED ORDERS -->
<h4><strong>Confirmed Orders:</strong></h4> 
<telerik:radgrid runat="server" ID="rgConfirmed" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" ShowStatusBar="True"
        DataSourceID="DropShipOrders" GridLines="None"  PageSize="12" Skin="Telerik" AutoGenerateColumns="False">
<HeaderContextMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</HeaderContextMenu>
<MasterTableView datakeynames="PDFRefNum" datasourceid="DropShipOrders">
<RowIndicatorColumn>
    <HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>
<ExpandCollapseColumn>
    <HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <NestedViewTemplate>
        <fieldset style="padding:5px;">
            <legend style="padding:5px;"><b>Order Details:  PDF Order </b><asp:Label ID="lblPDFRefNum" Font-Bold="true" Text='<%# Eval("PDFRefNum") %>' Visible="True" runat="server" />
                       </legend>
                       <asp:SqlDataSource ID="DropShipOrdersDetail" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                           ProviderName="System.Data.SqlClient" 
                           SelectCommand="SELECT     SOPPDFHeader.PDFRefNum, SOPPDFHeader.ReceivedDateTime, SOPPDFHeader.FormSpecialType, SOPPDFHeader.CustomerNum, SOPPDFHeader.PONum, 
                                          SOPPDFHeader.OrderDate, SOPPDFHeader.OrderedBy, SOPPDFHeader.Company, SOPPDFHeader.Address, SOPPDFHeader.City, SOPPDFHeader.Country, 
                                          SOPPDFHeader.StateProv, SOPPDFHeader.ZipCode, SOPPDFHeader.Phone, SOPPDFHeader.Comments, CAST(SOPPDFHeader.OrderTotal AS Decimal(10, 2)) 
                                          AS OrderTotal, SOPPDFHeader.Fax, 
                                          CASE SOPPDFHeader.Status WHEN 'P' THEN 'Processed' WHEN 'E' THEN 'An Error Occurred' WHEN 'N' THEN 'Waiting to be Processed' WHEN 'X' THEN 'Test Order' END AS Status, 
                                          SOPPDFResults.GPOrderID, SOPPDFResults.IntegrationDate, SOPPDFResults.EmailSent, 
                                          CASE SOPPDFHeader.Dropship WHEN 'True' THEN 'Yes' WHEN 'False' THEN 'No' END as Dropship, SOPPDFHeader.ShippingMethod
                                          FROM         SOPPDFHeader LEFT OUTER JOIN
                                          province ON SOPPDFHeader.StateProv = province.name FULL OUTER JOIN
                                          SOPPDFResults ON SOPPDFHeader.SOPPDFResultsID = SOPPDFResults.SOPPDFResultsID
                                          WHERE     (SOPPDFHeader.PDFRefNum = @PDFRefNum)" runat="server">
                           <SelectParameters>
                               <asp:ControlParameter ControlID="lblPDFRefNum" PropertyName="Text" Type="String" Name="PDFRefNum" />
                           </SelectParameters>
                       </asp:SqlDataSource>
                       <asp:FormView ID="FormView1" AllowPaging="true" GridLines="None" Width="100%" DataSourceID="DropShipOrdersDetail" runat="server">
                            <ItemTemplate>
                                <table class="outerTable">
                                    <tr>
                                        <td>
                                            <table class="innerTable">
                                                <tr><td class="innerTableLeftTD">CustomerNum:</td><td><asp:Label ID="lblCustomerNum" runat="server" Text='<%# Eval("CustomerNum") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">FormSpecialType:</td><td><asp:Label ID="lblFormSpecialType" runat="server" Text='<%# Eval("FormSpecialType") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">DateReceived:</td><td><asp:Label ID="lblReceivedDateTime" runat="server" Text='<%# Eval("ReceivedDateTime") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">PONum:</td><td><asp:Label ID="lblPONum" runat="server" Text='<%# Eval("PONum") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">OrderTotal:</td><td>$ <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderTotal") %>' /></td></tr>       
                                                <tr><td class="innerTableLeftTD">OrderedBy:</td><td><asp:Label ID="lblOrderedBy" runat="server" Text='<%# Eval("OrderedBy") %>' /></td></tr>                                                                            
                                            </table>
                                        </td>
                                        <td>
                                            <table class="innerTable">
                                                
                                                <tr><td class="innerTableLeftTD">Company:</td><td><asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">Address:</td><td><asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">City/St/Zip:</td><td><asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' />, <asp:Label ID="lblStateProv" runat="server" Text='<%# Eval("StateProv") %>' /> <asp:Label ID="lblZipCode" runat="server" Text='<%# Eval("ZipCode") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">Country:</td><td><asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">Phone:</td><td><asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">Fax:</td><td><asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>' /></td></tr>
                                            </table>    
                                       </td>
                                       <td>
                                            <table class="innerTable">
                                                <tr><td class="innerTableLeftTD">Status:</td><td><asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">GPOrderID:</td><td><asp:Label ID="lblGPOrderID" runat="server" Text='<%# Eval("GPOrderID") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">IntegrationDate:</td><td><asp:Label ID="lblIntegrationDate" runat="server" Text='<%# Eval("IntegrationDate") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">EmailSent:</td><td><asp:Label ID="lblEmailSent" runat="server" Text='<%# Eval("EmailSent") %>' /></td></tr>
                                                <tr><td class="innerTableLeftTD">Comments:</td><td><asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>' /><br /></td></tr>
                                                <tr><td class="innerTableLeftTD">Dropship Order:</td><td><asp:Label ID="lblDropShip" runat="server" Text='<%# Eval("Dropship") %>' /><br /></td></tr>
                                                <tr><td class="innerTableLeftTD">Dropship Method:</td><td><asp:Label ID="lblShippingMethod" runat="server" Text='<%# Eval("ShippingMethod") %>' /><br /></td></tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center"><asp:HyperLink ID="hypBack" runat="server" ImageUrl="~/images/enlarge.gif" NavigateUrl='<%# Eval("PDFRefNum","orderdetails.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                            <asp:HyperLink ID="hypBackText" runat="server" Text="View Details" NavigateUrl='<%# Eval("PDFRefNum","orderdetails.aspx?PDFRefNum={0}") %>'></asp:HyperLink></td>
                                        <td align="center"><asp:HyperLink ID="hypPrint" runat="server" ImageUrl="~/images/icon-print.gif" NavigateUrl='<%# Eval("PDFRefNum","printorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                            <asp:HyperLink ID="hypPrintText" runat="server" Text="Print Order" NavigateUrl='<%# Eval("PDFRefNum","printorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink></td>
                                        <td align="center"><asp:HyperLink ID="hypTrack" runat="server" ImageUrl="~/images/41.png" NavigateUrl='<%# Eval("PDFRefNum","trackorder.aspx?PDFRefNum={0}") %>'></asp:HyperLink>
                                            <asp:HyperLink ID="hypTrackText" runat="server" Text="Track Order" NavigateUrl='<%# Eval("PDFRefNum","trackorders.aspx?PDFRefNum={0}") %>'></asp:HyperLink></td>                                         
                                    </tr>
                                </table>
                            </ItemTemplate>
                       </asp:FormView>                     
                   </fieldset>
    </NestedViewTemplate>
    <Columns>
        <telerik:GridBoundColumn DataField="PDFRefNum" HeaderText="Reference#" 
            ReadOnly="True" SortExpression="PDFRefNum" UniqueName="PDFRefNum" 
            DataType="System.Int32">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CustomerNum" HeaderText="Customer#" 
            SortExpression="CustomerNum" UniqueName="CustomerNum">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PONum" HeaderText="PO Number" 
            SortExpression="PONum" UniqueName="PONum">
        </telerik:GridBoundColumn>     
        <telerik:GridBoundColumn DataField="DateReceived" DataType="System.DateTime" 
            HeaderText="Date Received" SortExpression="DateReceived" 
            UniqueName="DateReceived">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Company" HeaderText="Company Name" 
            SortExpression="Company" UniqueName="Company">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Total" DataType="System.Decimal" 
            HeaderText="Total" ReadOnly="True" SortExpression="Total" UniqueName="Total">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" 
            SortExpression="Status" UniqueName="Status">
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>
    <clientsettings>
        <selecting allowrowselect="True" />
    </clientsettings>
<FilterMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</FilterMenu>
</telerik:radgrid>
    <asp:SqlDataSource ID="DropShipOrders" runat="server" 
    ConnectionString="<%$ ConnectionStrings:RLConnectString %>" 
    SelectCommand="SELECT PDFRefNum, CustomerNum, PONum, ReceivedDateTime AS DateReceived, Company, CAST(OrderTotal AS Decimal(10 , 2)) AS Total, 
                    CASE Status WHEN 'P' THEN 'Processed'
                                WHEN 'E' THEN 'Error'
                                WHEN 'N' THEN 'New' END as Status 
                    FROM SOPPDFHeader 
                    WHERE ([CustomerNum] IN (SELECT CUSTNMBR FROM   RM00101 WHERE SLPRSNID = @SalesRep AND INACTIVE = '0')) 
                    ORDER BY DateReceived DESC" >
        <SelectParameters>
            <asp:SessionParameter Name="SalesRep" SessionField="SalesRep" Type="String" />           
        </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="DropShipData" runat="server" 
    ConnectionString="<%$ ConnectionStrings:RLConnectionString %>" SelectCommand="SELECT dbo.wsPF_ProductExtraInfo.ProductID, dbo.wsPF_ProductExtraInfo.Name, 
        dbo.wsPF_Brands.Name AS Brand,  dbo.wsPF_ProductExtraInfo.webpage as Webpage
        FROM dbo.wsPF_Brands INNER JOIN dbo.wsPF_ProductExtraInfo ON dbo.wsPF_Brands.BrandID = dbo.wsPF_ProductExtraInfo.Brand 
        WHERE (dbo.wsPF_ProductExtraInfo.DisplayOnOrderForm = 1) 
        ORDER BY dbo.wsPF_ProductExtraInfo.ProductID">

</asp:SqlDataSource>

</div>
    <h2>Downloads:</h2>
    <h4>Print/Fax</h4>
    <a href="../Downloads/rl order forms.pdf" target="_blank">Complete Order Form</a><br />
    <a href="../Downloads/rl price list.pdf" target="_blank">Complete Price List</a><br />
    <a href="../Downloads/rl canadian order forms.pdf" target="_blank">Canadian Order Form</a><br />
    <a href="../Downloads/rl canadian price lists.pdf" target="_blank">Canadian Price List</a><br />
    <br />
    <a href="../Downloads/bm order form.pdf" target="_blank">Bambino Mio Order Form</a><br />
    <a href="../Downloads/bm price list.pdf" target="_blank">Bambino Mio Price List</a><br />
    <a href="../Downloads/cy order form.pdf" target="_blank">Cybex Order Form</a><br />
    <a href="../Downloads/cy price list.pdf" target="_blank">Cybex Price List</a><br />
    <a href="../Downloads/de order form.pdf" target="_blank">Dekor Order Form</a><br />
    <a href="../Downloads/de price list.pdf" target="_blank">Dekor Price List</a><br />
    <a href="../Downloads/la order form.pdf" target="_blank">Lascal Order Form</a><br />
    <a href="../Downloads/la price list.pdf" target="_blank">Lascal Price List</a><br />
    <a href="../Downloads/mp order form.pdf" target="_blank">My Carry Potty Order Form</a><br />
    <a href="../Downloads/mp price list.pdf" target="_blank">My Carry Potty Price List</a><br />
    <br />
    <a href="../Downloads/item size and case pack information.pdf" target="_blank">Product
        Case Pack Information</a><br />
    <h4>Electronic</h4>
    <a href="../Downloads/RL Order Form - US.pdf" target="_blank">Complete Order Form</a><br />
    <a href="../Downloads/RL Order Form - Canada.pdf" target="_blank">Canadian Order Form</a><br />
    <br />
</asp:Content>


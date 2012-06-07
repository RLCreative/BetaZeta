<%@ Page Title="View Stock Status" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="sales_viewstock" CodeBehind="viewstock.aspx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaData" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="retailersview">
<h2><strong>View Stock Status:</strong></h2>
<img id="Img1" src="~/images/right.gif" runat="server" />Use this page to view the 
    stock status/availability of products.

<telerik:RadAjaxManager runat="server">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="RadGridStock">
            <updatedcontrols>                
                <telerik:AjaxUpdatedControl ControlID="RadGridStock" LoadingPanelID="RadAjaxLoadingPanel1" />
            </updatedcontrols>
        </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px"
        Width="75px" Transparency="3" MinDisplayTime="0">
        <img style="margin-top: 25px;" alt="Loading..." src="../images/loading.gif" />
</telerik:RadAjaxLoadingPanel>
<!-- AVAILABILITY -->

<telerik:RadGrid ID="RadGridStock" runat="server" AllowPaging="True" GridLines="None"
    DataSourceID="GetProductAvailability" Skin="Telerik" PageSize="20" 
        AllowFilteringByColumn="True" AllowSorting="True">
    <HeaderContextMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </HeaderContextMenu>
    <MasterTableView AutoGenerateColumns="False" 
        DataSourceID="GetProductAvailability">
        <RowIndicatorColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>
        <ExpandCollapseColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
        <NestedViewTemplate>
            <fieldset style="padding: 10px;">
                <legend style="padding: 5px;"><b>Product Details:</b>
                    <asp:Label ID="Label1" Font-Bold="true" Font-Italic="true" Text='<%# Eval("ProductID") %>'
                        Visible="false" runat="server" />
                    
                </legend>
                <asp:SqlDataSource ID="ProductDataSource" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="SELECT   RTRIM(wsPF_ProductExtraInfo.ProductID) as ProductID, wsPF_Brands.Name AS Brand, 
                                wsPF_ProductExtraInfo.Name AS [Product Description], wsPF_ProductExtraInfo.Register, wsPF_ProductExtraInfo.Style, 
                                wsPF_ProductExtraInfo.Color, wsPF_ProductExtraInfo.ImageName, wsPF_ProductExtraInfo.Size, wsPF_ProductExtraInfo.WebPage
                                FROM wsPF_Brands INNER JOIN
                                wsPF_ProductExtraInfo ON wsPF_Brands.BrandID = wsPF_ProductExtraInfo.Brand
                                WHERE (wsPF_ProductExtraInfo.ProductID = @ProductID)" runat="server">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label1" PropertyName="Text" Type="String" Name="ProductID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="StockDataSource" ConnectionString="<%$ ConnectionStrings:RLConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    SelectCommand="SELECT RTRIM(wsPF_ProductExtraInfo.ProductID) as ProductID, 
	                    RTRIM(wsPF_ProductExtraInfo.Webpage) as Webpage, 
	                    RTRIM(wsPF_ProductExtraInfo.Name) as Names, 
	                    RTRIM(wsPF_ProductExtraInfo.UPC) as UPC, 
	                    IVCBR002.UOFM, 
	                    CAST(IVCBR002.QTY_in_Master_Pack as Decimal(10,2)) as QTY_in_Master_Pack, 
                        CAST(IVCBR002.QTY_in_Sub_Pack as Decimal(10,2)) as QTY_in_Sub_Pack, 
                        CAST(IVCBR002.Item_Length as Decimal(10,2)) as Item_Length, 
                        CAST(IVCBR002.Item_Width as Decimal(10,2)) as Item_Width, 
                        CAST(IVCBR002.Item_Height as Decimal(10,2)) as Item_Height, 
                        IVCBR002.Item_Cube, 
                        CAST(IVCBR002.Item_Weight as Decimal(10,2)) as Item_Weight, 
                        CAST(IVCBR002.Item_Master_Length as Decimal(10,2)) as Item_Master_Length, 
                        CAST(IVCBR002.Item_Master_Width as Decimal(10,2)) as Item_Master_Width, 
                        CAST(IVCBR002.Item_Master_Height as Decimal(10,2)) as Item_Master_Height, 
                        IVCBR002.U_Of_M_Cube, 
                        CAST(IVCBR002.U_Of_M_Weight as Decimal(10,2)) as U_Of_M_Weight, 
                        CAST(IVCBR002.Freight_Class as Decimal(10,2)) as Freight_Class, 
                        IVCBR002.NMFC_Code, 
                        IVCBR002.Country_of_Origin
                    FROM         wsPF_ProductExtraInfo INNER JOIN
                                          IVCBR002 ON wsPF_ProductExtraInfo.ProductID = IVCBR002.ITEMNMBR
                    WHERE (wsPF_ProductExtraInfo.ProductID = @ProductID)" runat="server">                    
                <SelectParameters>
                        <asp:ControlParameter ControlID="Label1" PropertyName="Text" Type="String" Name="ProductID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:FormView ID="FormView1" AllowPaging="true" GridLines="None" Width="90%" DataSourceID="StockDataSource"
                    runat="server">
                    <ItemTemplate>
                        <table width="100%" class="outerTable" align="center">
                            <tr>
                                <td>
                                    <table class="innerTableStockPic">
                                        <tr>
                                            <td class="innerTableLeftTD">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Webpage") %>'
                                                    ImageUrl='<%# Eval("ProductID", "~/images/thumbs/{0}.jpg") %>' />
                                            </td>
                                        </tr>
                                    </table>        
                                </td>                                
                               <td>
                                    <table class="innerTableStock">                                               
                                        <tr><td class="innerTableLeftTD">Name:</td><td><asp:Label ID="lblNames" runat="server" Text='<%# Eval("Names") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">UPC:</td><td><asp:Label ID="lblUPC" runat="server" Text='<%# Eval("UPC") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">UOM:</td><td><asp:Label ID="lblUOFM" runat="server" Text='<%# Eval("UOFM") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">QTY Master Pack:</td><td><asp:Label ID="lblQTYMaster" runat="server" Text='<%# Eval("QTY_in_Master_Pack") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">QTY Sub Pack:</td><td><asp:Label ID="lblQtySub" runat="server" Text='<%# Eval("QTY_in_Sub_Pack") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Length:</td><td><asp:Label ID="lblFax" runat="server" Text='<%# Eval("Item_Length") %>' /></td></tr>                                        
                                    </table>    
                               </td>
                                <td>
                                    <table class="innerTableStock">
                                        <tr><td class="innerTableLeftTD">Width:</td><td><asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Item_Width") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Height:</td><td><asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Item_Height") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Item Cube:</td><td><asp:Label ID="lblCube" runat="server" Text='<%# Eval("Item_Cube") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Item Weight:</td><td><asp:Label ID="lblItemWeight" runat="server" Text='<%# Eval("Item_Weight") %>' /></td></tr>                                               
                                        <tr><td class="innerTableLeftTD">Item Master Length:</td><td><asp:Label ID="lblMasterLength" runat="server" Text='<%# Eval("Item_Master_Length") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Item Master Width:</td><td><asp:Label ID="lblMasterWidth" runat="server" Text='<%# Eval("Item_Master_Width") %>' /></td></tr>
                                       
                                    </table>    
                               </td>
                               <td>
                                    <table class="innerTableStock">
                                        <tr><td class="innerTableLeftTD">Item Master Height:</td><td><asp:Label ID="lblMasterHeight" runat="server" Text='<%# Eval("Item_Master_Height") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">UOM Cube:</td><td><asp:Label ID="lblUOMCube" runat="server" Text='<%# Eval("U_Of_M_Cube") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">UOM Weight:</td><td><asp:Label ID="lblUOMWeight" runat="server" Text='<%# Eval("U_Of_M_Weight") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Freight Class:</td><td><asp:Label ID="lblFreightClass" runat="server" Text='<%# Eval("Freight_Class") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">NMFC Code:</td><td><asp:Label ID="lblNMFC" runat="server" Text='<%# Eval("NMFC_Code") %>' /></td></tr>
                                        <tr><td class="innerTableLeftTD">Country of Origin:</td><td><asp:Label ID="lblCountryOfOrigin" runat="server" Text='<%# Eval("Country_of_Origin") %>' /></td></tr>
                                    </table>    
                               </td>
                            </tr>
                        </table>                        
                    </ItemTemplate>
                </asp:FormView>
            </fieldset>
        </NestedViewTemplate>
        <Columns>
            <telerik:GridBoundColumn DataField="ProductID" HeaderText="ProductID"
                SortExpression="ProductID" UniqueName="ProductID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                UniqueName="Name">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Brand" HeaderText="Brand"
                SortExpression="Brand" UniqueName="Brand">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="QuanityAvailable" DataType="System.Decimal" 
                HeaderText="QuanityAvailable" ReadOnly="True" SortExpression="QuanityAvailable" 
                UniqueName="QuanityAvailable">
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </FilterMenu>
</telerik:RadGrid>
<asp:SqlDataSource ID="GetProductAvailability" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RLConnectionString %>" SelectCommand="SELECT     
	wsPF_ProductExtraInfo.ProductID, wsPF_ProductExtraInfo.Name, wsPF_Brands.Name AS Brand, 
	CASE WHEN IV00102.QTYONHND BETWEEN 25 AND 10000000 THEN 'Available' 
	WHEN IV00102.QTYONHND < 1 THEN 'Out of Stock' 
	WHEN IV00102.QTYONHND BETWEEN 1 AND 25 THEN 'Low stock' END as QuanityAvailable
    FROM         wsPF_ProductExtraInfo INNER JOIN
                      IV00102 ON wsPF_ProductExtraInfo.ProductID = IV00102.ITEMNMBR INNER JOIN
                      IV00101 ON IV00102.ITEMNMBR = IV00101.ITEMNMBR INNER JOIN
                      wsPF_Brands ON wsPF_ProductExtraInfo.Brand = wsPF_Brands.BrandID
WHERE     (IV00101.INACTIVE <> '1') AND (wsPF_ProductExtraInfo.DisplayOnOrderForm = '1') AND (IV00102.LOCNCODE = 'MAIN')
ORDER BY wsPF_ProductExtraInfo.ProductID">
</asp:SqlDataSource>

  
</div> 
</asp:Content>


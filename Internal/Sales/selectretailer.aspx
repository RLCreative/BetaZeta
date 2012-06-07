<%@ Page Title="Select a retailer..." Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="sales_selectretailer" CodeBehind="selectretailer.aspx.cs" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="salerepview">

<h2><strong>Select Retailer:</strong></h2>
<img src="~/images/right.gif" runat="server" />Select the retailer you would like to place an order for.
<telerik:RadGrid ID="rgRetailers" runat="server" AllowPaging="True" PageSize="15" 
        AllowSorting="True" DataSourceID="SqlDataSource1" GridLines="None" OnItemDataBound="rgRetailers_ItemDataBound" 
        Skin="Telerik" style="margin-right: 0px" 
        onitemcommand="rgRetailers_ItemCommand" AutoGenerateColumns="False">
<HeaderContextMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</HeaderContextMenu>

<MasterTableView datasourceid="SqlDataSource1">
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/images/go.png" 
            HeaderText="Create Order" Text="Click to Create New Order" UniqueName="column"  CommandName="Select">
        </telerik:GridButtonColumn>
        <telerik:GridBoundColumn DataField="CustomerNum" HeaderText="Customer#" 
            ReadOnly="True" SortExpression="CustomerNum" UniqueName="CustomerNum">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" 
            ReadOnly="True" SortExpression="CustomerName" UniqueName="CustomerName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="City" HeaderText="City" ReadOnly="True" 
            SortExpression="City" UniqueName="City">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="State" HeaderText="State" ReadOnly="True" 
            SortExpression="State" UniqueName="State">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Zip" HeaderText="Zip" ReadOnly="True" 
            SortExpression="Zip" UniqueName="Zip">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" ReadOnly="True" 
            SortExpression="Phone" UniqueName="Phone">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="HOLD" DataType="System.Byte" 
            HeaderText="Status" SortExpression="HOLD" UniqueName="HOLD" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/images/notonhold.png" 
            HeaderText="Status" Text="Retailers Status" UniqueName="Status" CommandName="Select">
        </telerik:GridButtonColumn>        
    </Columns>
</MasterTableView>

<FilterMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</FilterMenu>
    </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RLConnectionString %>" 
            SelectCommand="SELECT RTRIM(CUSTNMBR) AS CustomerNum, RTRIM(CUSTNAME) AS CustomerName, RTRIM(CITY) AS City, RTRIM(STATE) AS State, SUBSTRING(ZIP, 1, 5) AS Zip, SUBSTRING(PHONE1, 1, 10) AS Phone, HOLD FROM RM00101 WHERE (SLPRSNID = @SalesRep) AND (INACTIVE = '0') ORDER BY CUSTNAME">
            <SelectParameters>
                <asp:SessionParameter Name="SalesRep" SessionField="SalesRep" />
            </SelectParameters>
        </asp:SqlDataSource>
    <br />

    
    <asp:SqlDataSource ID="DataRetailers" runat="server"  
        ConnectionString="<%$ ConnectionStrings:RLConnectionString %>" 
        
            SelectCommand="SELECT RTRIM(CUSTNMBR) AS CustomerNum, RTRIM(CUSTNAME) AS CustomerName, RTRIM(CITY) AS City, RTRIM(STATE) AS State, SUBSTRING(ZIP, 1, 5) AS Zip, SUBSTRING(PHONE1, 1, 10) AS Phone, HOLD FROM RM00101 WHERE (SLPRSNID = '@SalesRep') AND (INACTIVE = '0') ORDER BY CUSTNAME">
    </asp:SqlDataSource>
    <br />
    
</div>
</asp:Content>


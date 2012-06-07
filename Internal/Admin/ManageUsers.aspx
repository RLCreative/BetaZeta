<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="admin_ManageUsers" CodeBehind="ManageUsers.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="adminview">
<h2><strong>Manage Users:</strong></h2>
    <telerik:RadAjaxManager runat="server">
        <ajaxsettings>
            <telerik:AjaxSetting AjaxControlID="rgUsers">
                <updatedcontrols>                    
                    <telerik:AjaxUpdatedControl ControlID="rgUsers" LoadingPanelID="RadAjaxLoadingPanel1" />
                </updatedcontrols>
            </telerik:AjaxSetting>
        </ajaxsettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px"
            Width="75px" Transparency="3" MinDisplayTime="0">
            <img style="margin-top: 10px;" alt="Loading..." src="../images/loading.gif" />
    </telerik:RadAjaxLoadingPanel>
<telerik:radgrid runat="server" DataSourceID="GetUserInfo" GridLines="None" PageSize="15"
        Skin="Telerik" ID="rgUsers" AllowPaging="True" AllowSorting="True" 
        onupdatecommand="rgUsers_UpdateCommand" onitemcommand="rgUsers_ItemCommand" OnDeleteCommand="rgUsers_ItemCommand">
<HeaderContextMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</HeaderContextMenu>

<MasterTableView datasourceid="GetUserInfo" autogeneratecolumns="False"  EditMode="PopUp" CommandItemDisplay="None" DataKeyNames="UserID">
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridEditCommandColumn>
        </telerik:GridEditCommandColumn>        
        <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" UniqueName="UserName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" 
            SortExpression="Email" UniqueName="Email">
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="CustomerID" HeaderText="CustomerID" 
            SortExpression="CustomerID" UniqueName="CustomerID">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="RoleName" HeaderText="RoleName" 
            SortExpression="RoleName" UniqueName="RoleName">
        </telerik:GridBoundColumn>
        <telerik:GridCheckBoxColumn UniqueName="IsLockedOut" HeaderText="LockedOut" DataField="IsLockedOut" AllowSorting="true">
        </telerik:GridCheckBoxColumn>
        <telerik:GridCheckBoxColumn UniqueName="Dropshipper" HeaderText="Dropshipper" DataField="Dropshipper" AllowSorting="true">
        </telerik:GridCheckBoxColumn>
        <telerik:GridCheckBoxColumn UniqueName="FreightCollect" HeaderText="FreightCollect" DataField="FreightCollect" AllowSorting="true">
        </telerik:GridCheckBoxColumn>
        <telerik:GridButtonColumn Text="Delete" HeaderText="Delete User" ConfirmDialogType="Classic" ConfirmText="Are you sure you want to delete this user?" 
            ConfirmTitle="Delete User?" CommandName="Delete" CommandArgument="Delete" >
        </telerik:GridButtonColumn>
    </Columns>

<EditFormSettings EditFormType="Template">
<EditColumn UniqueName="EditCommandColumn1"></EditColumn>
        <FormStyle BackColor="#ECFED8" />
        <FormTemplate>
        <table  id="Table1" cellspacing="1" cellpadding="1" width="610" border="0">
            <tr>
                <td>
                    <table id="Table3" cellspacing="1" cellpadding="1" width="250" border="0">                        
                        <tr>
                            <td>
                                Username:</td>
                            <td>
                                <asp:TextBox ID="txtUserName" Text='<%# Bind( "Username") %>' ReadOnly="true" runat="server">
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Email:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" Text='<%# Bind( "Email") %>' runat="server">
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                CustomerID:</td>
                            <td>
                                <asp:TextBox ID="txtCustomerID" Text='<%# Bind( "CustomerID") %>' runat="server">
                                </asp:TextBox></td>
                        </tr>                
                        <tr>
                            <td>
                                Current Role:</td>
                            <td>
                                <asp:label ID="lblRoleName" Text='<%# Bind( "Rolename") %>' ReadOnly="true" runat="server">
                                </asp:label></td>
                        </tr>
                        <tr>
                            <td>
                                IsLockedOut:</td>
                            <td>
                                <asp:CheckBox ID="ckbLockedOut" runat="server" Checked='<%# Bind( "IsLockedOut") %>'>
                                </asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td>
                                Dropshipper:</td>
                            <td>
                                <asp:CheckBox ID="ckbDropshipper" runat="server" Checked='<%# Bind( "Dropshipper") %>'>
                                </asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td>
                                Freight Collect:</td>
                            <td>
                                <asp:CheckBox ID="ckbFreightCollect" runat="server" Checked='<%# Bind( "FreightCollect") %>'>
                                </asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td>
                                Carrier:</td>
                            <td>
                                <asp:DropDownList ID="ddlCarrier" runat="server" DataValueField="Carrier" SelectedValue='<%# Bind( "Carrier") %>' AppendDataBoundItems="true">
                                    <asp:ListItem Text="-----" Value="" ></asp:ListItem>
                                    <asp:ListItem Text="Fedex" Value="Fedex"></asp:ListItem>
                                    <asp:ListItem Text="UPS" Value="UPS"></asp:ListItem>
                                </asp:DropDownList>                                
                        </tr> 
                        <tr>
                            <td>
                                Account#:</td>
                            <td>
                                <asp:TextBox ID="txtAccountNum" Text='<%# Bind( "[Account#]") %>' runat="server">
                                </asp:TextBox></td>
                        </tr>                                                 
                    </table>
                </td>
                <td>
                    <table id="Table4" cellspacing="1" cellpadding="1" width="350" border="0">
                        <tr>
                            <td>
                                <strong>Update Role?</strong><br />Select the role for this user.<br />Only one role per user.
                            </td>
                       </tr>
                        <tr>                            
                            <td>
                                <asp:RadioButtonList ID="rblRoles" runat="server" Enabled="true">
                                    <asp:ListItem Text="Admin" Value="admin" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Customer Care" Value="customercare" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Retailer" Value="retailer" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Sales" Value="sales" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Salesrep" Value="salesrep" Selected="False"></asp:ListItem>
                                </asp:RadioButtonList>
                           </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update">
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>    
        </FormTemplate>
    </EditFormSettings>
</MasterTableView>
<FilterMenu EnableTheming="True">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
</FilterMenu>
    </telerik:radgrid>

<asp:SqlDataSource ID="GetUserInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ASPNETDB %>" 
       SelectCommand="SELECT     vw_aspnet_MembershipUsers.UserId, vw_aspnet_MembershipUsers.UserName, vw_aspnet_MembershipUsers.Email, ISNULL(aspnet_Users_CustomerID.CustomerID,'') as CustomerID, 
                      vw_aspnet_Roles.RoleName, vw_aspnet_MembershipUsers.IsLockedOut, ISNULL(aspnet_Users_CustomerID.Dropshipper,'False') as Dropshipper,
                      ISNULL(aspnet_Users_CustomerID.FreightCollect,'False') as FreightCollect, aspnet_Users_CustomerID.Carrier as Carrier, 
                      aspnet_Users_CustomerID.Account# as Account#
                      FROM         aspnet_Users_CustomerID RIGHT OUTER JOIN
                      vw_aspnet_MembershipUsers ON aspnet_Users_CustomerID.UserName = vw_aspnet_MembershipUsers.UserName LEFT OUTER JOIN
                      vw_aspnet_Roles INNER JOIN
                      vw_aspnet_UsersInRoles ON vw_aspnet_Roles.RoleId = vw_aspnet_UsersInRoles.RoleId ON 
                      vw_aspnet_MembershipUsers.UserId = vw_aspnet_UsersInRoles.UserId">
</asp:SqlDataSource>
<asp:SqlDataSource ID="GetRoles" runat="server" ConnectionString="<%$ ConnectionStrings:ASPNETDB %>" 
       SelectCommand="SELECT RoleName from vw_aspnet_Roles">
</asp:SqlDataSource>
<br /><br />
<asp:Label ID="lblError" runat="server"></asp:Label>
<br /><br />
<a href="../default.aspx">Home</a> |
<a href="../security/Login.aspx">Login</a> |
<a href="../security/ChangePassword.aspx">Change Password</a><br />
</div>
</asp:Content>


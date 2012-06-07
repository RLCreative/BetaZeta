<%@ Page Title="Create User" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="admin_createuser" CodeBehind="CreateUser.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderId="MainContent" runat="server">
<div id="adminview">
<strong><font size="3">Create a user:</font></strong><br />
  <table align="center" width="500">
    <tr>
          <td align="center"><asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
                  continuedestinationpageurl="../default.aspx" BackColor="#303233" 
                  BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                  Font-Names="Verdana" Font-Size="10pt" ForeColor="White">
              <SideBarStyle BackColor="#7C6F57" BorderWidth="0px" Font-Size="0.9em" 
                  VerticalAlign="Top" />
              <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" 
                  ForeColor="#FFFFFF" />
              <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                  BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                  ForeColor="#284775" />
              <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                  BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                  ForeColor="#284775" />
              <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" 
                  HorizontalAlign="Center" />
              <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                  BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                  ForeColor="#284775" />
              <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
              <StepStyle BorderWidth="0px" />
              <WizardSteps>
            <asp:CreateUserWizardStep runat="server"></asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>
            </WizardSteps>
            </asp:CreateUserWizard>
            <br />
            <a href="../default.aspx">Home</a> |
            <a href="../security/Login.aspx">Login</a> |
            <a href="../security/ChangePassword.aspx">Change Password</a><br />  
            </td>
        </tr>
    </table>
</div>  
</asp:Content>



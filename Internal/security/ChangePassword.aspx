<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="True" Inherits="security_changePassword" CodeBehind="ChangePassword.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
<br />
<table align="center" width="500">
    <tr>
        <td align="center">
            <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="#303233" 
                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="10pt" ForeColor="White">
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            </asp:ChangePassword>                  
        </td>
    </tr>
    <tr>
        <td align="center">
            <strong>Your password must be a minimum of 7 characters long and contain at 
            least one non-alphnumeric (@,#,$ for example).</strong></td>
    </tr>
</table>
<br />
<br />
</asp:Content>


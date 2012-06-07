<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="True" Inherits="security_forgotPassword" CodeBehind="ForgotPassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
<br />
<table align="center" width="500">
    <tr>
        <td align="center">
            <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" OnSendingMail="PasswordRecovery1_SendingMail"
                BackColor="#303233" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="10pt" ForeColor="White">                
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            </asp:PasswordRecovery>      
        </td>
    </tr>
</table>
<br />
<br />
</asp:Content>


<%@ Page Title="Login" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="True" Inherits="security_login" CodeBehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="grid_6">
        <h1>Retailer Sign In</h1>
        <hr />
        <asp:Login ID="Login1" runat="server" FailureText="The username and password you provided did not match our records, please try again!"
            FailureTextStyle-ForeColor="Red" DisplayRememberMe="False" InstructionText="Enter your username and password to login!"
            UserNameRequiredErrorMessage="You must enter a username!" DestinationPageUrl="~/internal/retailers/vieworders.aspx"
            PasswordRecoveryText="Forgot your password?" PasswordRecoveryUrl="~/internal/security/ForgotPassword.aspx"
            EnableTheming="False" Font-Bold="False" OnLoggedIn="Login1_LoggedIn">
            <LayoutTemplate>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name</asp:Label>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ErrorMessage="You must enter a username!" ToolTip="You must enter a username!"
                    ValidationGroup="Login1">*</asp:RequiredFieldValidator><br />
                <asp:TextBox ID="UserName" runat="server" Width="200" CssClass="textbox"></asp:TextBox><br />
                <br />
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="Password" runat="server" Width="200" TextMode="Password" CssClass="textbox"></asp:TextBox><br />
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><br />
                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" ValidationGroup="Login1"
                    OnClick="LoginButton_Click" CssClass="btnblue" Font-Size="1em" />&nbsp; <a href="/internal/security/ForgotPassword.aspx"
                        class="txtsmall">Forgot your password?</a>
            </LayoutTemplate>
        </asp:Login>
        <hr />
        <h2>Not Registered?</h2>
        <p>
            If you are a retailer interested in being able to manage your orders online, or
            if you would like to take advantage of our drop ship program please register with
            us.
        </p>
        <a href="/internal/retailerinquiry.aspx" class="btnblue">Register</a>
        <asp:Label Visible="false" ID="lblResult" runat="server" />
    </div>
    <div class="prefix_2 grid_3">
        <img src="../../images/gentleman.png" alt="Akord Login Page" />
    </div>
</asp:Content>

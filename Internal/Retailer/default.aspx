<%@ Page Title="Traditional Retailers Login" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_default" CodeBehind="default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaData" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<h4><strong>Retailer Login</strong></h4>
<table align="center" width="400">
     <tr>
        <td align="right" width="200"></td>
        <td></td>
    </tr>
    <tr>
        <td align="right" width="200">Username:</td>
        <td><asp:TextBox ID="txtUserName" runat="server" Width="200"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" width="200">Password:</td>
        <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200"></asp:TextBox></td>
    </tr> 
    <tr>
        <td align="right" width="200"></td>
        <td align="left"><asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="120" 
                onclick="btnSubmit_Click" /></td>
    </tr> 
     <tr>
        <td align="right" width="200"></td>
        <td></td>
    </tr>  
</table>
<table align="center" width="400">
    <tr>
        <td align="center"><asp:Label ID="lblErrorMessage" runat="server"></asp:Label></td>
    </tr>    
</table>
</asp:Content>


﻿<%@ Page Title="Print Order Details" Language="C#" MasterPageFile="~/internal/internal.master"
    AutoEventWireup="true" Inherits="retailers_printorder" CodeBehind="printorder.aspx.cs" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.0.12.215, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaData" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="retailersview">
    <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="800" Width="100%" 
        Report="RegalLagerReports.OrderDetails, RegalLagerReports, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
    </telerik:ReportViewer>
<br /><br />
</div>
</asp:Content>


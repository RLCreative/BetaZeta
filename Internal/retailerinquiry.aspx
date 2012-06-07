<%@ Page Title="Retailer Inquiry" Language="C#" MasterPageFile="~/internal/internal.master" AutoEventWireup="true" Inherits="retailerinquiry" Codebehind="retailerinquiry.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MetaData" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="grid_7 marginbottom">
        <h1>
            Retailer Inquiry</h1>
        <asp:Label ID="lblSendError" runat="server" Style="font-size: 0.875em; font-weight: normal;"></asp:Label>
        <asp:Panel ID="pnlForm" runat="server" EnableViewState="False">
            <p>
                If you are interested in becoming a retailer of Akord Products, please submit the information below 
                and our Sales Team will be in touch with you, or just give us a call.</p>
                <hr />
            <div class="form">
                <asp:ValidationSummary ID="ValidationSummary1" Font-Bold="True" runat="server" HeaderText="Please correct errors and try again!">
                </asp:ValidationSummary>
                First&nbsp;Name*<br />
                <asp:TextBox ID="txtFirstName" CssClass="textbox" runat="server" MaxLength="30" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" CssClass="txtsmall" runat="server" ControlToValidate="txtFirstName">Required</asp:RequiredFieldValidator><br />
                Last&nbsp;Name*<br />
                <asp:TextBox ID="txtLastName" CssClass="textbox" runat="server" MaxLength="30" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" CssClass="txtsmall" runat="server" ControlToValidate="txtLastName">Required</asp:RequiredFieldValidator><br />
                Business&nbsp;Name*<br />
                <asp:TextBox ID="txtStoreName" runat="server" CssClass="textbox" MaxLength="30" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStoreName" CssClass="txtsmall" runat="server" ControlToValidate="txtStoreName">Required</asp:RequiredFieldValidator><br />
                Years in Business*<br />
                <asp:TextBox ID="txtyearsbusiness" runat="server" CssClass="textbox" MaxLength="30" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvyearsbusiness" CssClass="txtsmall" runat="server" ControlToValidate="txtyearsbusiness">Required</asp:RequiredFieldValidator><br />
                Address<br />
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox" MaxLength="30" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" CssClass="textsmall" ControlToValidate="txtAddress1">Required</asp:RequiredFieldValidator><br />
                Apt./Suite<br />
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox" MaxLength="30" Columns="30"></asp:TextBox><span
                    class="textsmall">&nbsp;</span><br />
                City*<br />
                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="30" Columns="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" CssClass="textsmall" ControlToValidate="txtCity">Required</asp:RequiredFieldValidator><br />
                State/Province*<br />
                <asp:DropDownList ID="ddlState" runat="server" CssClass="textbox">
                    <asp:ListItem Value="0" Selected="True">-- Select --</asp:ListItem>
                    <asp:ListItem Value="AL">AL</asp:ListItem>
                    <asp:ListItem Value="AK">AK</asp:ListItem>
                    <asp:ListItem Value="AZ">AZ</asp:ListItem>
                    <asp:ListItem Value="AR">AR</asp:ListItem>
                    <asp:ListItem Value="CA">CA</asp:ListItem>
                    <asp:ListItem Value="CO">CO</asp:ListItem>
                    <asp:ListItem Value="CT">CT</asp:ListItem>
                    <asp:ListItem Value="DC">DC</asp:ListItem>
                    <asp:ListItem Value="DE">DE</asp:ListItem>
                    <asp:ListItem Value="FL">FL</asp:ListItem>
                    <asp:ListItem Value="GA">GA</asp:ListItem>
                    <asp:ListItem Value="HI">HI</asp:ListItem>
                    <asp:ListItem Value="ID">ID</asp:ListItem>
                    <asp:ListItem Value="IL">IL</asp:ListItem>
                    <asp:ListItem Value="IN">IN</asp:ListItem>
                    <asp:ListItem Value="IA">IA</asp:ListItem>
                    <asp:ListItem Value="KS">KS</asp:ListItem>
                    <asp:ListItem Value="KY">KY</asp:ListItem>
                    <asp:ListItem Value="LA">LA</asp:ListItem>
                    <asp:ListItem Value="ME">ME</asp:ListItem>
                    <asp:ListItem Value="MD">MD</asp:ListItem>
                    <asp:ListItem Value="MA">MA</asp:ListItem>
                    <asp:ListItem Value="MI">MI</asp:ListItem>
                    <asp:ListItem Value="MN">MN</asp:ListItem>
                    <asp:ListItem Value="MS">MS</asp:ListItem>
                    <asp:ListItem Value="MO">MO</asp:ListItem>
                    <asp:ListItem Value="MT">MT</asp:ListItem>
                    <asp:ListItem Value="NE">NE</asp:ListItem>
                    <asp:ListItem Value="NV">NV</asp:ListItem>
                    <asp:ListItem Value="NH">NH</asp:ListItem>
                    <asp:ListItem Value="NJ">NJ</asp:ListItem>
                    <asp:ListItem Value="NM">NM</asp:ListItem>
                    <asp:ListItem Value="NY">NY</asp:ListItem>
                    <asp:ListItem Value="NC">NC</asp:ListItem>
                    <asp:ListItem Value="ND">ND</asp:ListItem>
                    <asp:ListItem Value="OH">OH</asp:ListItem>
                    <asp:ListItem Value="OK">OK</asp:ListItem>
                    <asp:ListItem Value="OR">OR</asp:ListItem>
                    <asp:ListItem Value="PA">PA</asp:ListItem>
                    <asp:ListItem Value="RI">RI</asp:ListItem>
                    <asp:ListItem Value="SC">SC</asp:ListItem>
                    <asp:ListItem Value="SD">SD</asp:ListItem>
                    <asp:ListItem Value="TN">TN</asp:ListItem>
                    <asp:ListItem Value="TX">TX</asp:ListItem>
                    <asp:ListItem Value="UT">UT</asp:ListItem>
                    <asp:ListItem Value="VT">VT</asp:ListItem>
                    <asp:ListItem Value="VA">VA</asp:ListItem>
                    <asp:ListItem Value="WA">WA</asp:ListItem>
                    <asp:ListItem Value="WV">WV</asp:ListItem>
                    <asp:ListItem Value="WI">WI</asp:ListItem>
                    <asp:ListItem Value="WY">WY</asp:ListItem>
                    <asp:ListItem Value="AB">Canada-AB</asp:ListItem>
                    <asp:ListItem Value="BC">Canada-BC</asp:ListItem>
                    <asp:ListItem Value="MB">Canada-MB</asp:ListItem>
                    <asp:ListItem Value="NB">Canada-NB</asp:ListItem>
                    <asp:ListItem Value="NF">Canada-NF</asp:ListItem>
                    <asp:ListItem Value="NS">Canada-NS</asp:ListItem>
                    <asp:ListItem Value="NT">Canada-NT</asp:ListItem>
                    <asp:ListItem Value="ON">Canada-ON</asp:ListItem>
                    <asp:ListItem Value="QC">Canada-QC</asp:ListItem>
                    <asp:ListItem Value="SK">Canada-SK</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvState" runat="server" CssClass="textsmall" ControlToValidate="ddlState"
                    InitialValue="0">Required</asp:RequiredFieldValidator>
                <br />
              ZIP/Postal&nbsp;Code*<br />
                <asp:TextBox ID="txtZip" runat="server" CssClass="textbox" MaxLength="7" Columns="9" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvZip" runat="server" CssClass="textsmall" Display="Dynamic" ControlToValidate="txtZip">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revZip" runat="server" CssClass="textsmall" Display="Dynamic" ControlToValidate="txtZip"
                    ValidationExpression="^(\d{5}|[a-zA-Z][0-9][a-zA-Z]( ){0,1}[0-9][a-zA-Z][0-9])$">Invalid</asp:RegularExpressionValidator>
                <br />
                Country*<br />
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textbox">
                    <asp:ListItem Value="0" Selected="True">-- Select --</asp:ListItem>
                    <asp:ListItem Value="CA">Canada</asp:ListItem>
                    <asp:ListItem Value="US">United States</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" CssClass="textsmall" ControlToValidate="ddlCountry"
                    InitialValue="0">Required</asp:RequiredFieldValidator>
                <br />
                 Phone*<br />
                <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" MaxLength="10" Columns="20"></asp:TextBox><span
                    class="textsmall">&nbsp;Numbers Only</span>
                 <asp:RequiredFieldValidator ID="rfvtxtphone" runat="server" CssClass="textsmall" ControlToValidate="txtPhone"
                    InitialValue="0">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPhone" runat="server" CssClass="textsmallerror" Display="Dynamic"
                    ControlToValidate="txtPhone" ValidationExpression="^(\d{10})$">Invalid</asp:RegularExpressionValidator><br />
              E-mail*<br />
                <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" MaxLength="200" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" CssClass="txtsmall" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" CssClass="txtsmall" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic" ValidationExpression="[A-Za-z0-9._%-]+@[A-Za-z0-9._%-]+\.[A-Za-z]{2,4}">Invalid</asp:RegularExpressionValidator>
                <br />
                Website*<br />
                <asp:TextBox ID="txtwebsite" CssClass="textbox" runat="server" MaxLength="200" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtweb" CssClass="txtsmall" runat="server" ControlToValidate="txtwebsite"
                    Display="Dynamic">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="txtsmall" runat="server"
                    ControlToValidate="txtwebsite" Display="Dynamic" ValidationExpression="[A-Za-z0-9._%-]+.[A-Za-z0-9._%-]+\.[A-Za-z]{2,4}">Invalid</asp:RegularExpressionValidator>
                <br />
                Are you interested in dropshipping?<br />
                <asp:CheckBoxList ID="chkbDropship" runat="server" CellSpacing="1" CellPadding="1" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                    <asp:ListItem Value="Maybe">Maybe</asp:ListItem>
                </asp:CheckBoxList>
                <br />
                Comments:<br />
                <asp:TextBox ID="txtComments" runat="server" CssClass="textbox" Rows="4" TextMode="MultiLine" MaxLength="500"
                    Columns="35"></asp:TextBox><span class="textsmall">&nbsp;(Optional)</span><br />
                <br />
                <asp:Button ID="btnSubmit" CssClass="btnblue" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    BackColor="#269168" ForeColor="White" BorderStyle="None" BorderWidth="0px" Font-Size="Medium"></asp:Button></div>
        </asp:Panel>
    </div>
    <div class="prefix_1 grid_4" style="margin-top: 30px;">
        <h2>
            Give us a Call</h2>
        <hr />
        <p>
            Call us toll free at <strong>800-593-5522</strong>, Monday through Friday, 8:30 AM&ndash;5:30 PM EST</p>
        <h2>
            Our Address</h2>
        <hr />
        <p>
            Regal Lager, Inc.<br />
            1100 Cobb Place Blvd.<br />
            Kennesaw, GA 30144
        </p>
    </div>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Join.ascx.cs" EnableViewState="false" Inherits="LinkMe.Web.Landing.Controls.Join" %>
<%@ Register TagPrefix="uc" TagName="PasswordHashSaver" Src="~/ui/controls/common/PasswordHashSaver.ascx" %>
<%@ Register TagPrefix="wc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<%-- It is important that all validators have Display="Dynamic" so that stray &nbsp;'s aren't
     left everywhere, ruining margins and paddings more often than not. (MF 2008-09-30)
--%>

<div class="errors">
    <asp:RequiredFieldValidator ID="reqEmail" EnableClientScript="false" ControlToValidate="txtEmail" runat="server" Display="Dynamic" />
    <wc:EmailAddressValidator ID="valEmail" EnableClientScript="false" ControlToValidate="txtEmail" ShowErrorTextInline="true" CheckDns="true" runat="server" Display="Dynamic" />
    <asp:RequiredFieldValidator ID="reqFirst" EnableClientScript="false" ControlToValidate="txtFirstName" runat="server" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="valFirst" EnableClientScript="false" ControlToValidate="txtFirstName" runat="server" Display="Dynamic" />
    <asp:RequiredFieldValidator ID="reqLast" EnableClientScript="false" ControlToValidate="txtLastName" runat="server" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="valLast" EnableClientScript="false" ControlToValidate="txtLastName" runat="server" Display="Dynamic" />
    <asp:RequiredFieldValidator ID="reqPass" EnableClientScript="false" ControlToValidate="txtPassword" runat="server" Display="Dynamic" />
    <wc:RequiredCheckBoxValidator ID="reqTerms" EnableClientScript="false" Enabled="true" Visible="true" ControlToValidate="chkAcceptTermsAndConditions" runat="server" Display="Dynamic" />
</div>

<span class="forms_v2">
    <div class="firstname_field textbox_field field">
        <asp:Label AssociatedControlID="txtFirstName" runat="server">First name</asp:Label>
        <div class="textbox_control control">
            <asp:TextBox id="txtFirstName" CssClass="textbox" runat="server" />
        </div>
    </div>

    <div class="lastname_field textbox_field field">
        <asp:Label AssociatedControlID="txtLastName" runat="server">Last name</asp:Label>
        <div class="textbox_control control">
            <asp:TextBox id="txtLastName" CssClass="textbox" runat="server" />
        </div>
    </div>

    <div class="email_field textbox_field field">
        <asp:Label AssociatedControlID="txtEmail" runat="server">Email <span class="helptext">Your username</span></asp:Label>
        <div class="email_textbox_control textbox_control control">
            <asp:TextBox id="txtEmail" CssClass="textbox" runat="server" />
        </div>
    </div>

    <div class="password_field password_textbox_field textbox_field field">
        <asp:Label AssociatedControlID="txtPassword" runat="server">Password</asp:Label>
        <div class="password_textbox_control textbox_control control">
            <asp:TextBox TextMode="Password" id="txtPassword" CssClass="textbox" runat="server" />
        </div>
        <uc:PasswordHashSaver ID="ucPasswordHashSaver" runat="server" />
    </div>

    <div class="accepttc_field checkbox_field field">
        <div class="checkbox_control control">
            <input type="checkbox" class="checkbox" id="chkAcceptTermsAndConditions" name="chkAcceptTermsAndConditions" value="true" runat="server" />
            <label for="<% =chkAcceptTermsAndConditions.ClientID %>">I accept the <%=TermsAndConditionsPopup %></label>
        </div>
    </div>
</span>
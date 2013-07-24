<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LocationField.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.LocationField" %>
<%@ Register TagPrefix="uc" TagName="Location" Src="~/ui/controls/common/Location.ascx" %>
<%@ Register TagPrefix="uc" TagName="LocationConfirmation" src="~/ui/controls/common/LocationConfirmation.ascx" %>

<div id="divField" class="field textbox_field autocomplete_textbox_field location_autocomplete_textbox_field">
    <label for="<%= ucLocation.TextBoxClientID %>"><asp:Literal ID="litLabel" runat="server" /></label>
    <div class="control textbox_control autocomplete_textbox_control">
        <uc:Location id="ucLocation" runat="server" />
        <uc:LocationConfirmation ID="ucLocationConfirmation" runat="server" Display="Dynamic" />
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CountryField.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Fields.CountryField" %>
<%@ Register TagPrefix="uc" TagName="LocationCountry" Src="~/ui/controls/common/LocationCountry.ascx" %>

<div id="divField" class="country_dropdown_field dropdown_field field" runat="server">
    <asp:Label ID="lblField" AssociatedControlID="ucLocationCountry" runat="server" />
    <div class="country_dropdown_control dropdown_control control">
        <uc:LocationCountry ID="ucLocationCountry" runat="server" />
    </div>
</div>


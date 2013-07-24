<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="LocationCountry.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.LocationCountry" %>
<%@ Register TagPrefix="wc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>

<script src="<%=ApplicationPath %>/js/controls/Location.js" type="text/javascript"></script>

<span class="forms_v2">
    <asp:dropdownlist id="ddlCountry" CssClass="country_dropdown dropdown" runat="server" />
    <wc:LinkMeRequiredFieldValidator id="reqCountry" runat="server" controltovalidate="ddlCountry" />
</span>

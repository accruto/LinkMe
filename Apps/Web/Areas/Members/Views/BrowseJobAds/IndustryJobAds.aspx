<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<LocationsJobAdsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = "Find jobs in " + Model.Industry.Name; %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="location_section section">
        <div class="section-title">
            <h1>Find <%= Model.Industry.Name %> jobs by location</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("JobAdsByLocation", Model); %>
        </div>
    </div>

</asp:Content>


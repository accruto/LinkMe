<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<IndustriesJobAdsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = "Find jobs in " + Model.Location.Name; %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="industry_section section">
        <div class="section-title">
            <h1>Find <%= Model.Location.Name %> jobs by industry</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("JobAdsByIndustry", Model); %>
        </div>
    </div>

</asp:Content>


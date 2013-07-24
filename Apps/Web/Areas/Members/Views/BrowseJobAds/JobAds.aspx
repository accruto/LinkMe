<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<JobAdsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="industry_section section">
        <div class="section-title">
            <h1>Find jobs in <%= Model.Country.Name %> by industry</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("JobAdsByIndustry", new IndustriesJobAdsModel { Industries = Model.Industries }); %>
        </div>
    </div>

<%  if (Model.CountrySubdivisions.Count > 0 || Model.Regions.Count > 0)
    { %>
    <div class="location_section section">
        <div class="section-title">
            <h1>Find jobs in <%=Model.Country.Name%> by location</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("JobAdsByLocation", new LocationsJobAdsModel { CountrySubdivisions = Model.CountrySubdivisions, Regions = Model.Regions }); %>
        </div>
    </div>
<%  } %>

</asp:Content>


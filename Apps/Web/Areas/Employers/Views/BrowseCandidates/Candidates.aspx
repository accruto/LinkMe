<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<CandidatesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="industry_section section">
        <div class="section-title">
            <h1>Find candidates in <%= Model.Country.Name %> by salary</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("CandidatesBySalaryBand", new SalaryBandsCandidatesModel { SalaryBands = Model.SalaryBands }); %>
        </div>
    </div>

<%  if (Model.CountrySubdivisions.Count > 0 || Model.Regions.Count > 0)
    { %>
    <div class="location_section section">
        <div class="section-title">
            <h1>Find candidates in <%=Model.Country.Name%> by location</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("CandidatesByLocation", new LocationsCandidatesModel { CountrySubdivisions = Model.CountrySubdivisions, Regions = Model.Regions }); %>
        </div>
    </div>
<%  } %>

</asp:Content>


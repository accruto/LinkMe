<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<LocationsCandidatesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = "Find candidates with desired salaries between " + Model.SalaryBand.LowerBound ?? 0 + " and " + Model.SalaryBand.UpperBound; %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="location_section section">
        <div class="section-title">
            <h1>Find candidates by location</h1>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("CandidatesByLocation", Model); %>
        </div>
    </div>

</asp:Content>


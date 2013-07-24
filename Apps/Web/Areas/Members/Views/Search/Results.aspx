<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Query.Search.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
<%
    if (Model.ListType == JobAdListType.BrowseResult)
        Page.Title = "Find " + ((BrowseListModel)Model).Industry.Name + " jobs in " + ((BrowseListModel)Model).Location.Name;
    else
        Page.Title = "Jobs - Online Job Search for Jobs, Employment &amp; Careers in Australia";
%>    
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.JobSearchResults)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.JobSearchResults)%>
    </mvc:RegisterJavaScripts>
    
    <% Html.RenderPartial("JSVariables", Model); %>

    <script language="javascript" type="text/javascript">
        <% if (!String.IsNullOrEmpty(Model.Criteria.GetKeywords())) { %>
        criteria["<%= JobAdSearchCriteriaKeys.Keywords %>"] = "<%= Model.Criteria.GetKeywords().Replace("\"", "\\\"") %>";
        <% } %>
        <% if (!String.IsNullOrEmpty(Model.Criteria.AdTitle)) { %>
        criteria["<%= JobAdSearchCriteriaKeys.AdTitle %>"] = "<%= Model.Criteria.AdTitle %>";
        <% } %>
        <% if (!String.IsNullOrEmpty(Model.Criteria.AdvertiserName)) { %>
        criteria["<%= JobAdSearchCriteriaKeys.AdvertiserName %>"] = "<%= Model.Criteria.AdvertiserName %>";
        <% } %>
        <% if (Model.Criteria.Location != null) { %>
        criteria["<%= JobAdSearchCriteriaKeys.Location %>"] = "<%= Model.Criteria.Location %>";
        criteria["<%= JobAdSearchCriteriaKeys.Distance %>"] = "<%= Model.Criteria.Distance == 0 ? Model.AncillaryData.DefaultDistance : Model.Criteria.Distance %>";
        criteria["<%= JobAdSearchCriteriaKeys.CountryId %>"] = "<%= Model.Criteria.Location == null ? Model.AncillaryData.DefaultCountry.Id : Model.Criteria.Location.Country.Id %>";
        <% } %>
        <% if (Model.Criteria.JobTypes != JobTypes.All) { %>
        criteria["<%= JobAdSearchCriteriaKeys.JobTypes %>"] = "<%= Model.Criteria.JobTypes %>";
        <% } %>
        <% if (Model.Criteria.Salary != null && Model.Criteria.Salary.LowerBound != null) { %>
        criteria["<%= JobAdSearchCriteriaKeys.SalaryLowerBound %>"] = "<%= ((int)Model.Criteria.Salary.LowerBound.Value).ToString() %>";
        <% } %>
        <% if (Model.Criteria.Salary != null && Model.Criteria.Salary.UpperBound != null) { %>
        criteria["<%= JobAdSearchCriteriaKeys.SalaryUpperBound %>"] = "<%= ((int)Model.Criteria.Salary.UpperBound.Value).ToString() %>";
        <% } %>
        <% if (Model.Criteria.ExcludeNoSalary) { %>
        criteria["<%= JobAdSearchCriteriaKeys.IncludeNoSalary %>"] = "<%= !Model.Criteria.ExcludeNoSalary %>";
        <% } %>
        <% if (Model.Criteria.Recency != null) { %>
        criteria["<%= JobAdSearchCriteriaKeys.Recency %>"] = "<%= Model.Criteria.Recency == null ? Model.AncillaryData.DefaultRecency : Model.Criteria.Recency.Value.Days %>";
        <% } %>
        <% if (Model.Criteria.IndustryIds != null) { %>
        criteria["<%= JobAdSearchCriteriaKeys.IndustryIds %>"] = <%= "[" + string.Join(",", (from id in Model.Criteria.IndustryIds select "\"" + id + "\"").ToArray()) + "]" %>;
        <% } %>
        <% if (Model.Criteria.HasNotes.HasValue) { %>
        criteria["<%= JobAdSearchCriteriaKeys.HasNotes %>"] = "<%= Model.Criteria.HasNotes %>";
        <% } %>
        <% if (Model.Criteria.HasViewed.HasValue) { %>
        criteria["<%= JobAdSearchCriteriaKeys.HasViewed %>"] = "<%= Model.Criteria.HasViewed %>";
        <% } %>
        <% if (Model.Criteria.IsFlagged.HasValue) { %>
        criteria["<%= JobAdSearchCriteriaKeys.IsFlagged %>"] = "<%= Model.Criteria.IsFlagged %>";
        <% } %>
        <% if (Model.Criteria.HasApplied.HasValue) { %>
        criteria["<%= JobAdSearchCriteriaKeys.HasApplied %>"] = "<%= Model.Criteria.HasApplied %>";
        <% } %>
        <% if (Model.Criteria.SortCriteria != null && (Model.Criteria.SortCriteria.SortOrder != JobAdSearchCriteria.DefaultSortOrder || Model.Criteria.SortCriteria.ReverseSortOrder)) { %>
        criteria["<%= JobAdSearchCriteriaKeys.SortOrder %>"] = "<%= Model.Criteria.SortCriteria.SortOrder %>";
        criteria["<%= JobAdSearchCriteriaKeys.SortOrderDirection %>"] = "<%= Model.Criteria.SortCriteria.ReverseSortOrder ? JobAdSearchCriteriaKeys.SortOrderIsAscending : JobAdSearchCriteriaKeys.SortOrderIsDescending %>";
        <% } %>
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageSubHeader" runat="server">
    <ul class="breadcrumbs">
        <li>Candidate site</li>
        <li><a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">New job search</a></li>
        <li>Search results</li>
    </ul>    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="leftside <%= CurrentMember == null ? "notloggedin" : "" %>" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.LeftSide)) %>"></div>
    
    <div class="rightside <%= CurrentMember == null ? "notloggedin" : "" %>  <%= Model.ListType %>" apiurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiSearch)) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.PartialSearch)) %>">

        <div class="resultheader">
            <div class="icon"></div>
            <span id="results-header-text"></span>
        </div>

        <% Html.RenderPartial("ResultsCount", Model); %>

        <% Html.RenderPartial("Sort", Model); %>

        <% Html.RenderPartial("TitleBar", Model); %>

        <div class="results">
            <% Html.RenderPartial("JobAdList", Model); %>
            <% Html.RenderPartial("EmptyList", Model); %>
        </div>
        
        <% Html.RenderPartial("Pagination", Model); %>

        <% Html.RenderPartial("GoogleJobsAdSense", Model.Criteria); %>
    
    </div>
    
    <% Html.RenderPartial("Overlays", Model); %>
</asp:Content>
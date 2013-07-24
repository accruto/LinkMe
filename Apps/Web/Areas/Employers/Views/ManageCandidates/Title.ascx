<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<span id="results-header-text" style="display:none;">
    <%
        var totalCandidates = Model.JobAd.ApplicantCounts.New + Model.JobAd.ApplicantCounts.Rejected + Model.JobAd.ApplicantCounts.ShortListed;%>
    <span class="manage-candidate_search-criterion search-criterion">
        <span class="search-criterion-data"><%=totalCandidates%> Candidate<%if (totalCandidates > 1) {%>s<%}%></span>
        <span class="search-criterion-name"> for </span>
        <span class="search-criterion-data"><%= Model.JobAd.Title%></span>
    </span>
</span>



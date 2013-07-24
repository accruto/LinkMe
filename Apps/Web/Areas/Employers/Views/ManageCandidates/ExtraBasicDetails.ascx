<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<%  var applications = Model.JobAd.Applications;
    var candidateId = (Guid)ViewData["CandidateId"]; %>
<div class="date-applied js_ellipsis">Added: <%= Html.Encode(applications.ContainsKey(candidateId) ? applications[candidateId].CreatedTime.ToString("dd MMM yyyy") : "N/A")%></div>

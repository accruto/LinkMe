<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<SuggestedCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Views"%>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<%  var candidateId = (Guid)ViewData["CandidateId"]; %>
<div class="shortlist-link_holder" onclick="onClickShortlistCandidate('<%= Model.JobAd.Id %>', '<%= candidateId %>');"></div>

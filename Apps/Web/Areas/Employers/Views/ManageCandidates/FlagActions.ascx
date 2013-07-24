<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<%  var candidateId = (Guid)ViewData["CandidateId"];
    var jobAdId = Model.JobAd.Id;
    var status = Model.ApplicantStatus;
    var showShortlistIcon = false;
    var showRejectIcon = false;
    var showRemoveIcon = false;

    if (status == ApplicantStatus.New)
    {
        showShortlistIcon = true;
        showRejectIcon = true;
    }
    if (status == ApplicantStatus.Shortlisted)
    {
        showRejectIcon = true;
    }
    if (status == ApplicantStatus.Rejected)
    {
        showShortlistIcon = true;
        showRemoveIcon = true;
    }

    if (showShortlistIcon)
    { %>
<div class="shortlist-link_holder" onclick="onClickShortlistCandidate('<%= jobAdId %>', '<%= candidateId %>');"></div>
<%  }
    if (showRejectIcon)
    { %>
<div class="reject-link_holder" onclick="onClickRejectCandidate('<%= jobAdId %>', '<%= candidateId %>');"></div>
<%  }
    if (showRemoveIcon)
    { %>
<div class="remove-link_holder" onclick="onClickRemoveCandidate('<%= jobAdId %>', '<%= candidateId %>');"></div>
<%  } %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<%  if (Model.ApplicantStatus == ApplicantStatus.New)
    {
        if (Model.JobAd.Applications.Count == 0)
        { %>
            No candidates have applied for this role yet.
<%      }
        else
        {%>
            All candidates who had applied for this role have been moved or deleted.
<%      }
    }
    else if (Model.ApplicantStatus == ApplicantStatus.Shortlisted)
    { %>
            No candidates are currently shortlisted for the role.
<%  }
    else if (Model.ApplicantStatus == ApplicantStatus.Rejected)
    { %>
            No candidates are currently rejected for the role.
<%  } %>
<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<MemberJobAdView>" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Users.Members.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>

<%  var jobTypes = Model.Description.JobTypes.GetOrderedJobTypes(); %>

<div class="jobad-list-view row<%= CurrentMember == null ? " anonymous" : "" %><%= Model.Features.IsFlagSet(JobAdFeatures.Highlight) ? " featured" : " " %>" id="<%= Model.Id %>">
    <div class="topbar"></div>
    <div class="bg">
        <div class="column tick">
            <div class="icon new <%= Model.IsNew() ? "active" : "" %>"></div>
            <div class="checkbox"></div>
        </div>
        <div class="column jobtype">
            <div class="icon types" jobtypes="<%= Model.Description.JobTypes %>"></div>
            <div class="subtypes">
<%  foreach (var jt in new[] { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare })
    { %>
                <div class="subtype <%= jt %> <%= jobTypes.Count > 1 && jobTypes.Skip(1).Contains(jt) ? "active" : "" %>"></div>
<%  } %>
            </div>
        </div>
        <div class="column title">
            <a href="<%= Model.GenerateJobAdUrl() %>" class="title" title="<%= Model.Title %>"><%= Model.Title%></a>
            <div class="company" title="<%= Model.ContactDetails.GetContactDetailsDisplayText() %>"><%= Model.ContactDetails.GetContactDetailsDisplayText() %></div>
            <div class="location" title="<%= Model.Description.Location %>"><%= Model.Description.Location %></div>
            <div class="action">
                <div class="icon folder" title="Add this job to a folder"></div>
                <div class="icon note" title="Add or edit notes for this job"><span class="count">(0)</span></div>
                <div class="icon email" title="Email this job to friends"></div>
                <div class="icon download" title="Download this job as a DOC"></div>
                <div class="icon hide" title="Hide this job from further searches"></div>
                <div class="icon restore" title="Restore this job to search result"></div>
            </div>
        </div>
        <div class="column info">
            <div class="icon viewed <%= Model.Applicant.HasViewed ? "active" : "" %>"></div>
            <div class="icon applied <%= Model.Applicant.HasApplied ? "active" : "" %>"></div>
        </div>
        <div class="divider"></div>
        <div class="salary"><%= Model.Description.Salary.GetJobAdDisplayText() %></div>
        <div class="divider"></div>
        <div class="date"><%= Model.CreatedTime.GetDateAgoText() %></div>
        <div class="column flag">
            <div class="flag <%= Model.Applicant.IsFlagged ? "flagged" : "" %>"></div>
            <div class="button expand"></div>
        </div>
        <div class="featured">FEATURED</div>
    </div>
    <div class="details collapsed">
        <div class="divider"></div>
        <div class="bg">
            <div class="summary">
                <ul class="bulletpoints">
<%  if (!Model.Description.BulletPoints.IsNullOrEmpty())
    {
        foreach (var bp in Model.Description.BulletPoints)
        { %>
                    <li><%= bp %></li>
<%      }
    } %>
                </ul>
            </div>
            <div class="industry">
                <div class="title">Industry:</div>
                <div class="desc"><%= Model.Description.Industries == null ? "" : string.Join(", ", (from ind in Model.Description.Industries select ind.Name).ToArray()) %></div>
            </div>
            <div class="description"><%= Model.Description.Content.GetContentDisplayText() %></div>
        </div>
    </div>
    <div class="bottombar"></div>
</div>
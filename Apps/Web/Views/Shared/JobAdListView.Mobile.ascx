<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<MemberJobAdView>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Users.Members.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>

<div class="jobad-list-view row jobad <%= Model.Features.IsFlagSet(JobAdFeatures.Highlight) ? "featured" : "" %>" id="<%= Model.Id %>" data-url="<%= Model.GenerateJobAdUrl() %>">
    <div class="titleline">
        <div class="icon new <%= Model.IsNew() ? "active" : "" %>"></div>
        <div class="icon jobtype" jobtypes="<%= Model.Description.JobTypes %>"></div>
        <div class="title"><%= Model.Title %></div>
    </div>
    <div class="icon saved <%= CurrentMember == null ? "notloggedin" : "" %> <%= Model.Applicant.IsInMobileFolder ? "active" : "" %>" data-url="<%= CurrentMember == null ? Html.RouteRefUrl(JobAdsRoutes.AddJobAdToMobileFolder, new { backTo = "SearchResult", jobAdId = Model.Id, returnUrl = ClientUrl }) : Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToMobileFolder) %>"></div>
    <div class="company"><%= Model.ContactDetails.GetContactDetailsDisplayText() %></div>
    <div class="location"><%= Model.Description.Location %></div>
    <div class="salary"><%= Model.Description.Salary.GetJobAdDisplayText() %></div>
    <div class="date">Posted <%= Model.CreatedTime.GetDateAgoText() %></div>
    <div class="featured"></div>
</div>
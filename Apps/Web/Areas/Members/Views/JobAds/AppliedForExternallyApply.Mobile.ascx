<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

<div class="appliedforexternally">
    <div class="contact">The application for this job redirects you to the <%= Model.IntegratorName %> website.</div>
    <div class="button save <%= Model.JobAd.Applicant.IsInMobileFolder ? "saved" : "" %>" data-url="<%= Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToMobileFolder) %>" id="<%= Model.JobAd.Id %>"><%= Model.JobAd.Applicant.IsInMobileFolder ? "JOB SAVED" : "SAVE THIS JOB FOR LATER"%></div>
    <div class="divider"></div>
    <a class="button apply" target="_blank" href="<%= Model.JobAd.Integration.ExternalApplyUrl %>" data-externallyappliedurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiExternallyApplied, new {jobAdId = Model.JobAd.Id})) %>" data-appliedpageurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>">APPLY</a>
</div>
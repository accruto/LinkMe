<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain.Contacts" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

<%
    var hasResume = false;
    var defaultResumeSource = ResumeSource.Uploaded;
    if (Model.Applicant.HasResume)
    {
        hasResume = true;
        defaultResumeSource = ResumeSource.Profile;
    } else if (Model.Applicant.LastUsedResumeFile != null)
    {
        hasResume = true;
        defaultResumeSource = ResumeSource.LastUsed;
    }
%>
<% if (hasResume) { %>
    <div class="attachresume">
	    <%= Html.RadioButtonsField(new { rs = defaultResumeSource }, "Resume", c => c.rs)
            .Without(ResumeSource.Uploaded)
	        .WithLabel(ResumeSource.Profile, "Apply using my Linkme resume")
	        .WithLabel(ResumeSource.LastUsed, "Attach my most recent resume document ")
	        .WithDisabled(ResumeSource.Profile, !Model.Applicant.HasResume)
	        .WithDisabled(ResumeSource.LastUsed, Model.Applicant.LastUsedResumeFile == null)
	        .WithOrder(ResumeSource.Profile, 0)
	        .WithOrder(ResumeSource.LastUsed, 1) %>
        <div class="mostrecentresume"><%= Model.Applicant.LastUsedResumeFile != null ? Model.Applicant.LastUsedResumeFile.FileName : "" %></div>
        <%= Html.CheckBoxField("SendMeConfirmation", true).WithLabelOnRight("").WithAttribute("data-email", CurrentMember.GetBestEmailAddress().Address) %>
        <div class="errorinfo">Apply failed. Please try again</div>
        <% if (Model.JobAd.Processing == JobAdProcessing.ManagedInternally) { %>
            <div class="button apply" data-appliedurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>" data-applywithlastusedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new { jobAdId = Model.JobAd.Id }) %>" data-applywithprofileurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new { jobAdId = Model.JobAd.Id }) %>">APPLY</div>
        <% } else { %>
            <% if (Model.JobAd.Integration.ApplicationRequirements == null) { %>
                <a class="button apply" target="_blank" href="<%=Html.RouteRefUrl(JobAdsRoutes.RedirectToExternal, new {jobAdId = Model.JobAd.Id})%>" data-appliedurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>" data-applywithlastusedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new { jobAdId = Model.JobAd.Id }) %>" data-applywithprofileurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new { jobAdId = Model.JobAd.Id }) %>">APPLY</a>
            <% } else { %>
                <div class="button apply" data-appliedurl="<%=Html.RouteRefUrl(JobAdsRoutes.JobAdQuestions, new {jobAdId = Model.JobAd.Id})%>" data-applywithlastusedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new { jobAdId = Model.JobAd.Id }) %>" data-applywithprofileurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new { jobAdId = Model.JobAd.Id }) %>">APPLY</div>
            <% } %>
        <% } %>
        <div class="divider"></div>
        <div class="button save <%= Model.JobAd.Applicant.IsInMobileFolder ? "saved" : "" %>" data-url="<%= Html.RouteRefUrl(JobAdsRoutes.ApiAddJobAdsToMobileFolder) %>" id="<%= Model.JobAd.Id %>"><%= Model.JobAd.Applicant.IsInMobileFolder ? "JOB SAVED" : "SAVE THIS JOB FOR LATER"%></div>
    </div>
<% } else { %>
    <div class="noresume">
        <p>You currently do not have any resume available on your LinkMe account.</p>
        <p>Please login on a computer and upload your resume to apply for this job.</p>
        <ul>
            <% if (Model.CurrentSearch != null) { %>
                <li><%= Html.RouteRefLink("Back", SearchRoutes.Results) %><span>&nbsp;to search results</span></li>
            <% } %>
            <li><a href="<%= Model.JobAd.GenerateJobAdUrl() %>">Return</a><span>&nbsp;to job ad</span></li>
        </ul>
    </div>
<% } %>
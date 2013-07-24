<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<div class="managedinternally">
	<div class="contact"><%= Model.JobAd.ContactDetails.GetContactDetailsDisplayText() %></div>
    <% Html.RenderPartial("AttachResume", Model); %>
</div>
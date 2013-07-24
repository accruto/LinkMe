<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds" %>

<div class="managedexternally">
	<div class="contact">The application for this job redirects you to a new website.</div>
    <% Html.RenderPartial("AttachResume", Model); %>
</div>
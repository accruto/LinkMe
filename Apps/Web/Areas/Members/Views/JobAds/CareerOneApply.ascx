<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>

<div class="careeroneapply">
	<a class="applybutton" target="_blank" href="<%= Model.JobAd.Integration.ExternalApplyUrl %>">Apply for this job on the <%= Model.IntegratorName %> website</a>
	<div class="text">The application form redirects you to the <%= Model.IntegratorName %> website*</div>
</div>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        appliedPageUrl = "<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>";
        externallyAppliedUrl = "<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.ApiExternallyApplied, new {jobAdId = Model.JobAd.Id})) %>";
    });
</script>
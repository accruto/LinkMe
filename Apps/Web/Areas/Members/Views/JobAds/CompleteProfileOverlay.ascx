<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

<div class="CompleteProfile CallToActionOverlay">
	<div class="top-bar"></div>
	<div class="left-edge">
		<div class="vline vline-1" colour_start="A8B4BE" colour_end="DDDDDD"></div>
		<div class="vline vline-2" colour_start="8DA4B5" colour_end="B8C6D0"></div>
	</div>
	<div class="bg">
		<div class="dialog-title">
			<div class="close-icon"></div>
		</div>
		<div class="top-bar"></div>
		<div class="left-side">
			<div class="progressbar">
				<div class="bg"></div>
				<div class="fg"></div>
				<div class="text"><%= Model.Applicant.ProfileCompletePercent %></div>
				<div class="alerticon"></div>
			</div>
			<div class="recommand">
				<p>We've already extracted information from your resume.<br/>We <span class="red">highly recommend</span> that you review your profile to ensure this has been successful.</p>
				<p>Candidates who complete their profile are <span class="red">70%</span> more likely to be contacted by employers.</p>
			</div>
			<div class="contacted"><span class="red"><%= Model.ContactedLastWeek.ToString("N0") %></span> candidates were contacted in the past week - don't miss out.</div>
			<a class="completemyprofilenow" href="<%= CurrentMember == null ? Html.RouteRefUrl(LinkMe.Web.Areas.Public.Routes.HomeRoutes.GuestsProfile) : Html.RouteRefUrl(ProfilesRoutes.Profile) %>">Complete my pforile now</a>
		</div>
		<div class="divider"></div>
		<div class="suggestedjobs">
			<% Html.RenderPartial("SuggestedJobs", Model.SuggestedJobs); %>
			<a class="alljobsbutton" href="<%= CurrentMember == null ? JobAdsRoutes.Similar.GenerateUrl(new {jobAdId = Model.JobAd.Id.ToString()}) : JobAdsRoutes.Suggested.GenerateUrl() %>">See more jobs like this</a>
		</div>
	</div>
	<div class="right-edge">
		<div class="vline vline-1" colour_start="97ACBD" colour_end="BCC9D4"></div>
		<div class="vline vline-2" colour_start="B3BDC5" colour_end="DDDEE0"></div>
		<div class="vline vline-3" colour_start="B5BEC6" colour_end="DFE0E1"></div>
		<div class="vline vline-4" colour_start="B6BFC7" colour_end="E1E1E1"></div>
		<div class="vline vline-5" colour_start="D8DADB" colour_end="D4D6D7"></div>
	</div>
	<div class="bottom-bar"></div>
</div>
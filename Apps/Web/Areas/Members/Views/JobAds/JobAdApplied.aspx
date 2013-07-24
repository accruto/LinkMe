<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.JobAds)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.JavaScript(JavaScripts.JobAd) %>
        <%= Html.JavaScript(JavaScripts.JQueryFileUpload) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<%  var contact = Model.JobAd.ContactDetails.GetContactDetailsDisplayText(); %>

    <div class="appliedpage">
        <% if (Model.JobAd.Processing != JobAdProcessing.ManagedInternally) { %>
            <div class="popupwarning">
                <div class="icon warning"></div>
                <span class="application">This advertiser's application form should have opened in a new window.</span>
                <span class="popup">Experiencing problems? Please check your browser's pop-up blocker settings and <%= Html.RouteRefLink("try to open the window again", JobAdsRoutes.RedirectToExternal, new { jobAdId = Model.JobAd.Id, applicationId = Request["applicationId"] }, new { target = "_blank" })%>.</span>
            </div>
        <% } %>
	    <div class="applied">
		    <div class="top-bar">We hope your application is successful</div>
		    <div class="contact" title="Please contact <%= string.IsNullOrEmpty(contact) ? "the advertiser" : contact %> directly if you have any questions about the progress of your application.">Please contact <b><%= string.IsNullOrEmpty(contact) ? "the advertiser" : contact %></b> directly if you have any questions about the progress of your application.</div>
		    <div class="bg">
			    <div class="links">
				    <div class="linkholder jobad" url="<%= Model.JobAd.GenerateJobAdUrl() %>">
				        <div class="icon"></div>
				        <a class="link" href="<%= Model.JobAd.GenerateJobAdUrl() %>">Go back to the job ad</a>
				    </div>
				    <% if (Model.CurrentSearch != null) { %>
				        <div class="linkholder searchresult" url="<%= Html.RouteRefUrl(SearchRoutes.Results) %>">
				            <div class="icon"></div>
				            <a class="link" href="<%= Html.RouteRefUrl(SearchRoutes.Results) %>">Return to your search results</a>
				        </div>
                    <% } %>
				    <div class="linkholder newjobsearch" url="<%= new ReadOnlyApplicationUrl(false, "~/search/jobs") %>">
				        <div class="icon"></div>
				        <a class="link" href="<%= new ReadOnlyApplicationUrl(false, "~/search/jobs") %>">Start a new job search</a>
				    </div>
				    <div class="linkholder profile" url="<%= CurrentMember == null ? LinkMe.Web.Areas.Public.Routes.HomeRoutes.GuestsProfile.GenerateUrl() : ProfilesRoutes.Profile.GenerateUrl() %>">
				        <div class="icon "></div>
				        <a class="link" href="<%= CurrentMember == null ? LinkMe.Web.Areas.Public.Routes.HomeRoutes.GuestsProfile.GenerateUrl() : ProfilesRoutes.Profile.GenerateUrl() %>">Review your LinkMe profile</a>
				    </div>
			    </div>
			    <div class="suggestedjobs">
			        <% Html.RenderPartial("SuggestedJobs", Model.SuggestedJobs); %>
			        <a class="alljobsbutton" href="<%= CurrentMember == null ? JobAdsRoutes.Similar.GenerateUrl(new {jobAdId = Model.JobAd.Id.ToString()}) : JobAdsRoutes.Suggested.GenerateUrl() %>">See more jobs like this</a>
			    </div>
		    </div>
		    <div class="bottom-bar"></div>
	    </div>
	    <% Html.RenderPartial("GoogleVerticalAds"); %>
    </div>
</asp:Content>
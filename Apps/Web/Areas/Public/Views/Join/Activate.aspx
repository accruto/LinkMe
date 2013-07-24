<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ActivateModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Join"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Join)%>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JoinFlow) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JQueryValidation) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="joinflow-header">Get Started: Create your profile</div>
	<div class="joinflow-steps"><% Html.RenderPartial("Steps", Model); %></div>

	<div id="joinflow-content">
        <div class="step-content">
            <div class="divider-2-1"></div>
            <div class="thanks-for-signup">
                <div class="top-bar"></div>
                <div class="bg">
                    <div class="fg">
				        <div class="check-icon"></div>
				        <span class="title">Your profile has been created.</span>
				        <div class="mail-icon"></div>
				        <span class="email-line">We've sent an email containing an activation link to:</span>
				        <span class="email-address"><b><%= Model.EmailAddress %></b></span>
				        <span class="check">Please check that email account and click on the link in the email to activate your account.</span>
				        <span class="change">Is that email address incorrect? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, null, new { id = "ChangeEmailAddress" }) %></span>
				        <span class="contact">If you haven't received your activation email within 2 hours, please check your Spam folder first. If you still can't find the email, try logging in using your email address and password, and you'll be able to re-send the activation email. Alternatively, you can contact our Member Support Hotline on <%= Constants.PhoneNumbers.FreecallHtml %> or <%= Constants.PhoneNumbers.InternationalHtml %>.</span>
                    </div>
                </div>
                <div class="bottom-bar"></div>
			</div>
        </div>
    </div>
    
    <script language="javascript" type="text/javascript">

        var urls = {
            Join: '<%= Html.RouteRefUrl(JoinRoutes.Join) %>',
            PersonalDetails: '<%= Html.RouteRefUrl(JoinRoutes.PersonalDetails) %>',
            JobDetails: '<%= Html.RouteRefUrl(JoinRoutes.JobDetails) %>',
            Activate: '<%= Html.RouteRefUrl(JoinRoutes.Activate) %>'
        };
	
	    $(document).ready(function() {
		    initScriptForStep("Activate", urls);
	    });
    </script>
    
</asp:Content>
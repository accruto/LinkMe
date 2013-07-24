<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<div class="managedexternallyapply">
	<div class="contact"><%= Model.JobAd.ContactDetails.GetContactDetailsDisplayText() %></div>
	<div class="loginarea">
		<div class="loginbutton"></div>
		<div class="text">Already have a LinkMe profile? Login to apply for this job now</div>
		<div class="loginfields">
	        <div class="divider"></div>
	        <div class="validationerror">
	            <div>
		            <div class="alerticon"></div>
		            <div class="prompt">There are some errors, please correct them below.</div>
	            </div>
	            <ul>
	            </ul>
		    </div>
		    <%= Html.TextBoxField("LoginId", "").WithLabel("Your email").WithIsRequired().WithAttribute("data-watermark", "") %>
		    <%= Html.PasswordField("Password", "").WithLabel("Password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
			<div class="joinnow"><span>Don't have an account? </span><div class="link">Join now</div></div>
			<div class="loginbutton" applyareaurl="<%= Html.RouteRefUrl(JobAdsRoutes.LoggedInUserApplyArea, new { jobAdId = Model.JobAd.Id }) %>" loginurl="<%= Html.RouteRefUrl(AccountsRoutes.ApiLogIn) %>"></div>
		</div>
	</div>
	<div class="joinarea">
		<div class="divider"></div>
	    <div class="validationerror">
	        <div>
		        <div class="alerticon"></div>
		        <div class="prompt">There are some errors, please correct them below.</div>
	        </div>
	        <ul>
	        </ul>
		</div>
	    <%= Html.TextBoxField("FirstName", "").WithLabel("First name").WithIsRequired().WithAttribute("data-watermark", "") %>
	    <%= Html.TextBoxField("LastName", "").WithLabel("Last name").WithIsRequired().WithAttribute("data-watermark", "")%>
	    <%= Html.TextBoxField("EmailAddress", "").WithLabel("Email address").WithIsRequired().WithAttribute("data-watermark", "")%>
		<div class="field resume_field read-only_field">
		    <label for="Resume">Upload your resume</label>
			<div class="textbox_control resume_control control">
				<input class="textbox" value="" id="Resume" name="Resume" type="text" readonly="readonly">
			</div>
			<div class="browse-holder">
				<form id="resumeupload" method="post" enctype="multipart/form-data" url="<%= ResumesRoutes.Upload.GenerateUrl() %>">
					<label class="fileinput-button browse-button">
						<span></span>
						<input type="file" name="file" multiple />
					</label>
				</form>
			</div>
			<div class="help-icon" helpfor="Resume"></div>
			<div class="help-area" helpfor="Resume">
				<div class="triangle"></div>
				<div class="help-text"><span>MS Word, RTF, DOC, DOCX, Text or HTML. 2MB max size</span></div>
			</div>
			<input type="hidden" id="FileReferenceId" name="FileReferenceId" value="" />
		</div>
	    <%= Html.CheckBoxField("CreateProfile", true).WithLabelOnRight("Create a LinkMe profile using this information ").WithCssPrefix("CreateProfile") %>
	    <div class="passwordsection">
			<div class="divider"></div>
		    <%= Html.PasswordField("JoinPassword", "").WithLabel("Password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
			<%= Html.PasswordField("JoinConfirmPassword", "").WithLabel("Confirm password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
		    <%= Html.CheckBoxField("TAndC", true).WithLabel("abc").WithLabelOnRight("I accept the LinkMe ").WithCssPrefix("TAndC").WithIsRequired().WithAttribute("url", SupportRoutes.Terms.GenerateUrl().AbsolutePath)%>
	    </div>
<%  if (Model.JobAd.Integration.ApplicationRequirements == null)
    { %>
        <a class="apply-button externalexpressapply" target="_blank" href="<%=Html.RouteRefUrl(JobAdsRoutes.RedirectToExternal, new {jobAdId = Model.JobAd.Id})%>" appliedurl="<%=Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new {jobAdId = Model.JobAd.Id})%>" joinurl="<%=Html.RouteRefUrl(JoinRoutes.ApiJoin)%>" applywithlastusedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new {jobAdId = Model.JobAd.Id})%>" applywithprofileurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new {jobAdId = Model.JobAd.Id})%>" applywithuploadedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new {jobAdId = Model.JobAd.Id})%>" parseurl="<%=Html.RouteRefUrl(ProfilesRoutes.ApiParseResume)%>" apiapplyurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApply, new {jobAdId = Model.JobAd.Id})%>">Apply this job on external Site</a>
<%  }
    else
    { %>        
        <div class="apply-button externalexpressapply" appliedurl="<%=Html.RouteRefUrl(JobAdsRoutes.JobAdQuestions, new {jobAdId = Model.JobAd.Id})%>" joinurl="<%=Html.RouteRefUrl(JoinRoutes.ApiJoin)%>" applywithlastusedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new {jobAdId = Model.JobAd.Id})%>" applywithprofileurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new {jobAdId = Model.JobAd.Id})%>" applywithuploadedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new {jobAdId = Model.JobAd.Id})%>" parseurl="<%=Html.RouteRefUrl(ProfilesRoutes.ApiParseResume)%>" apiapplyurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApply, new {jobAdId = Model.JobAd.Id})%>">Apply this job on external Site</div>
<%  } %>
	</div>
</div>
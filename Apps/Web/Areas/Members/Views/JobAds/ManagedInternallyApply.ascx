<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
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

<div class="managedinternallyapply">
	<div class="contact"><%= Model.JobAd.ContactDetails.GetContactDetailsDisplayText() %></div>
	<div class="promptarea">
		<div class="join">
			<div class="text blue">Don't Seek. Be Sought.</div>
			<div class="text">Join LinkMe and let employers and recruiters search for you</div>
		</div>
		<div class="login">
			<div class="text">Login and use your LinkMe profile to pre-fill your application</div>
		</div>
		<div class="divider"></div>
	</div>
	<div class="joinandloginsection">
	    <div class="validationerror">
	        <div>
		        <div class="alerticon"></div>
		        <div class="prompt">There are some errors, please correct them below.</div>
	        </div>
	        <ul>
	        </ul>
		</div>
		<%= Html.TextBoxField("LoginId", "").WithLabel("What is your email?").WithIsRequired().WithAttribute("data-watermark", "")%>
		<%= Html.RadioButtonsField("Resume", JoinOrLoginToApply.SignMeUp).WithLabel("Do you have a LinkMe password?").WithLabel(JoinOrLoginToApply.SignMeUp, "No, sign me up").WithLabel(JoinOrLoginToApply.HaveLinkMePassword, "Yes, I have a LinkMe password") %>
	    <%= Html.TextBoxField("FirstName", "").WithLabel("First name").WithIsRequired().WithAttribute("data-watermark", "") %>
	    <%= Html.TextBoxField("LastName", "").WithLabel("Last name").WithIsRequired().WithAttribute("data-watermark", "")%>
	    <%= Html.PasswordField("JoinPassword", "").WithLabel("Password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
	    <div class="forgotpassword">
	        <div class="icon"></div>
	        <%= Html.RouteRefLink("Forgot your password?", AccountsRoutes.NewPassword, null, new { @id = "ForgotPassword" }) %>
	    </div>
		<%= Html.PasswordField("JoinConfirmPassword", "").WithLabel("Confirm password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
		<div class="field resume_field read-only_field">
		    <label for="Resume">Upload your resume</label>
			<div class="textbox_control resume_control control">
				<input class="textbox" value="" id="Resume" name="Resume" type="text" readonly="readonly">
			</div>
			<div class="browse-holder">
				<form id="resumeupload" method="post" enctype="multipart/form-data" url="<%= Html.RouteRefUrl(ResumesRoutes.Upload) %>">
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
	    <%= Html.MultilineTextBoxField("CoverLetter", "").WithLabel("Cover letter").WithAttribute("maxlength", "1000") %>
	    <%= Html.CheckBoxField("TAndC", true).WithLabel("abc").WithLabelOnRight("I accept the LinkMe ").WithCssPrefix("TAndC").WithIsRequired().WithAttribute("url", SupportRoutes.Terms.GenerateUrl().AbsolutePath)%>
        <a class="apply-button expressapply" target="_blank" href="javascript:void(0);" applyareaurl="<%= Html.RouteRefUrl(JobAdsRoutes.LoggedInUserApplyArea, new { jobAdId = Model.JobAd.Id }) %>" loginurl="<%= Html.RouteRefUrl(AccountsRoutes.ApiLogIn) %>" appliedurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>" joinurl="<%= Html.RouteRefUrl(JoinRoutes.ApiJoin) %>" applywithlastusedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new { jobAdId = Model.JobAd.Id }) %>" applywithprofileurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new { jobAdId = Model.JobAd.Id }) %>" applywithuploadedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new { jobAdId = Model.JobAd.Id }) %>">Apply this job</a>
	</div>
</div>
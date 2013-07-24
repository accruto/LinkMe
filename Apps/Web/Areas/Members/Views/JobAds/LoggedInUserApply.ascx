<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<div class="loggedinuserapply">
	<div class="contact"><%= Model.JobAd.ContactDetails.GetContactDetailsDisplayText() %></div>
    <div class="validationerror">
        <div>
	        <div class="alerticon"></div>
	        <div class="prompt">There are some errors, please correct them below.</div>
        </div>
        <ul>
        </ul>
	</div>
	<div class="resumesection">

<%      var defaultResumeSource = ResumeSource.Uploaded;
        if (Model.Applicant.HasResume)
            defaultResumeSource = ResumeSource.Profile;
        else if (Model.Applicant.LastUsedResumeFile != null)
            defaultResumeSource = ResumeSource.LastUsed; %>

	    <%= Html.RadioButtonsField("Resume", defaultResumeSource)
	        .WithLabel(ResumeSource.Profile, "Apply using my Linkme resume - ")
	        .WithLabel(ResumeSource.Uploaded, "Attach a resume (from my computer)")
	        .WithLabel(ResumeSource.LastUsed, "Attach my most recent resume document ")
	        .WithDisabled(ResumeSource.Profile, !Model.Applicant.HasResume)
	        .WithDisabled(ResumeSource.LastUsed, Model.Applicant.LastUsedResumeFile == null)
	        .WithOrder(ResumeSource.Profile, 0)
	        .WithOrder(ResumeSource.Uploaded, 1)
	        .WithOrder(ResumeSource.LastUsed, 2) %>
	    <div class="uploadsection">
			<div class="field resume_field read-only_field">
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
			<%= Html.CheckBoxField("Overwrite", false).WithLabelOnRight("Overwrite my LinkMe resume with this resume") %>
			<div class="help-icon" helpfor="Overwrite"></div>
			<div class="help-area" helpfor="Overwrite">
				<div class="triangle"></div>
				<div class="help-text"><span>The resume you chose above will overwrite your existing LinkMe resume, any changes you made to your LinkMe resume will be lost. And you cannot undo this action.</span></div>
			</div>
	    </div>
	    
<%      if (Model.Applicant.HasResume)
        { %>
	    <a class="reviewprofile" href="<%= Html.RouteRefUrl(ProfilesRoutes.Download) %>">Review</a>
<%      }
        else
        { %>
        <div class="reviewprofile">Currently unavailable</div>
<%      }
        if (Model.Applicant.LastUsedResumeFile != null)
        { %>
        <a class="lastusedresumefile" href="<%= Html.RouteRefUrl(ProfilesRoutes.DownloadResume, new { fileReferenceId = Model.Applicant.LastUsedResumeFile.FileReferenceId }) %>">(<%= Model.Applicant.LastUsedResumeFile.FileName%>)</a>
<%      }
        else
        { %>
        <div class="lastusedresumefile">Currently unavailable</div>
<%      } %>
	</div>
	
<%      if (Model.JobAd.Application.IncludeCoverLetter)
        { %>
	<div class="coverlettersection">
	    <%= Html.MultilineTextBoxField("CoverLetter", Model.JobAd.GetDefaultCoverLetter(CurrentMember)).WithLabel("Cover letter").WithAttribute("maxlength", "1000") %>
	</div>
<%      } %>
	
<%      if (Model.JobAd.Processing == JobAdProcessing.ManagedInternally)
        { %>
    <div class="expressapply" appliedurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new { jobAdId = Model.JobAd.Id }) %>" applywithlastusedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new { jobAdId = Model.JobAd.Id }) %>" applywithprofileurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new { jobAdId = Model.JobAd.Id }) %>" applywithuploadedresumeurl="<%= Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new { jobAdId = Model.JobAd.Id }) %>"></div>
<%      }
        else if (Model.JobAd.Processing == JobAdProcessing.ManagedExternally)
        {
            if (Model.JobAd.Integration.ApplicationRequirements != null)
            { %>
    <div class="expressapply" appliedurl="<%= Html.RouteRefUrl(JobAdsRoutes.JobAdQuestions, new { jobAdId = Model.JobAd.Id }) %>" applywithlastusedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new {jobAdId = Model.JobAd.Id})%>" applywithprofileurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new {jobAdId = Model.JobAd.Id})%>" applywithuploadedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new {jobAdId = Model.JobAd.Id})%>"></div>
<%          }
            else
            { %>
    <a class="expressapply" target="_blank" href="<%=Html.RouteRefUrl(JobAdsRoutes.RedirectToExternal, new {jobAdId = Model.JobAd.Id})%>" appliedurl="<%=Html.RouteRefUrl(JobAdsRoutes.JobAdApplied, new {jobAdId = Model.JobAd.Id})%>" applywithlastusedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithLastUsedResume, new {jobAdId = Model.JobAd.Id})%>" applywithprofileurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithProfile, new {jobAdId = Model.JobAd.Id})%>" applywithuploadedresumeurl="<%=Html.RouteRefUrl(JobAdsRoutes.ApiApplyWithUploadedResume, new {jobAdId = Model.JobAd.Id})%>"></a>
<%          }
        }%>
	
	<div class="overwrite-overlay">
		<div>
			<span class="prompt-text">Are you sure you want to overwrite your existing LinkMe resume with this resume? Any changes you made to your LinkMe resume will be lost.</span>
			<span class="prompt-text red bold">You cannot undo this action!</span>
			<div class="yes"></div>
			<div class="cancel"></div>
		</div>
	</div>
</div>
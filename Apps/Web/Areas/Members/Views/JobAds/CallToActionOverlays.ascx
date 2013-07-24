<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdModel>" %>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Users"%>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Import Namespace="LinkMe.Web.UI.Unregistered"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search"%>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<%  if (Model.VisitorStatus.Frequency == VisitorFrequency.Occasional && Model.VisitorStatus.ShouldPrompt)
    { %>
<div class="Occasional CallToActionOverlay">
    <div class="top-bar"></div>
	<div class="left-edge">
		<div class="vline vline-1" colour_start="A8B4BE" colour_end="DDDDDD"></div>
		<div class="vline vline-2" colour_start="8DA4B5" colour_end="B8C6D0"></div>
	</div>
    <div class="bg">
	    <div class="dialog-title">
		    <div class="close-icon"></div>
	    </div>
		<div class="alertcreated"><span class="desc">Your email alert was successfully created. We'll send job alerts to </span><span class="alertaddress"></span></div>
		<div class="validationerror">
			<div class="top-bar">
				<div class="leftcorner"></div>
				<div class="center"></div>
				<div class="rightcorner"></div>
			</div>
			<div class="bg">
				<div>
					<div class="alerticon"></div>
					<div class="prompt">There are some errors, please correct them below.</div>
				</div>
				<ul>
				</ul>
			</div>
			<div class="bottom-bar">
				<div class="leftcorner"></div>
				<div class="center"></div>
				<div class="rightcorner"></div>
			</div>
		</div>
	    <div class="left-side">
		    <%= Html.CheckBoxField("AlreadyHaveProfile", false).WithLabelOnRight("I already have a LinkMe profile").WithCssPrefix("AlreadyHaveProfile")%>
		    <div class="withoutprofile">
			    <%= Html.TextBoxField("FirstName", "").WithLabel("First name").WithIsRequired().WithAttribute("data-watermark", "")%>
			    <%= Html.TextBoxField("LastName", "").WithLabel("Last name").WithIsRequired().WithAttribute("data-watermark", "")%>
			    <%= Html.TextBoxField("EmailAddress", "").WithLabel("Your email address").WithIsRequired().WithAttribute("data-watermark", "")%>
			    <%= Html.CheckBoxField("CreateProfile", true).WithLabelOnRight("Create a LinkMe profile using this information ").WithCssPrefix("CreateProfile")%>
			    <div class="sendmejobsbyemail" saveurl="<%= Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.SearchRoutes.ApiSaveSearch) %>"></div>
				<a class="seemorejobslikethis" href="<%= CurrentMember == null ? JobAdsRoutes.Similar.GenerateUrl(new {jobAdId = Model.JobAd.Id.ToString()}) : JobAdsRoutes.Suggested.GenerateUrl() %>">See more jobs like this</a>
		    </div>
		    <div class="withprofile">
			    <%= Html.TextBoxField("Username", "").WithLabel("Your email").WithIsRequired().WithAttribute("data-watermark", "")%>
			    <%= Html.PasswordField("Password", "").WithLabel("Password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "")%>
			    <div class="loginandsendmejobsbyemail" loginurl="<%= Html.RouteRefUrl(AccountsRoutes.ApiLogIn) %>" saveurl="<%= Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.SearchRoutes.ApiSaveSearch) %>"></div>
				<a class="seemorejobslikethis" href="<%= CurrentMember == null ? JobAdsRoutes.Similar.GenerateUrl(new {jobAdId = Model.JobAd.Id.ToString()}) : JobAdsRoutes.Suggested.GenerateUrl() %>">See more jobs like this</a>
		    </div>
	    </div>
	    <div class="right-side">
		    <div class="bg">
			    <div class="jobsbyemail <% if (Model.CurrentSearch == null) { %>viewjobonly<% } %>">Jobs by email</div>
			    <div class="searchtitle">We'll alert you<%= Model.CurrentSearch == null ? " to similar jobs" : " when new jobs match your search"%></div>
			    <% if (Model.CurrentSearch != null && Model.CurrentSearch.Criteria != null)
          { %>
				    <div class="search-criteria"><%= Model.CurrentSearch.Criteria.GetDisplayHtml()%></div>
			    <% } %>
		    </div>
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
<%  } %>

<% if ((Model.VisitorStatus.Frequency == VisitorFrequency.Casual || Model.VisitorStatus.Frequency == VisitorFrequency.Occasional) && Model.VisitorStatus.ShouldPrompt)
   { %>
<div class="Casual CallToActionOverlay">
    <div class="top-bar"></div>
	<div class="left-edge">
		<div class="vline vline-1" colour_start="A8B4BE" colour_end="DDDDDD"></div>
		<div class="vline vline-2" colour_start="8DA4B5" colour_end="B8C6D0"></div>
	</div>
    <div class="bg">
	    <div class="dialog-title">
		    <div class="close-icon"></div>
	    </div>
		<div class="alertcreated"><span class="desc">Your email alert was successfully created. We'll send job alerts to </span><span class="alertaddress"></span></div>
		<div class="validationerror">
			<div class="top-bar">
				<div class="leftcorner"></div>
				<div class="center"></div>
				<div class="rightcorner"></div>
			</div>
			<div class="bg">
				<div>
					<div class="alerticon"></div>
					<div class="prompt">There are some errors, please correct them below.</div>
				</div>
				<ul>
				</ul>
			</div>
			<div class="bottom-bar">
				<div class="leftcorner"></div>
				<div class="center"></div>
				<div class="rightcorner"></div>
			</div>
		</div>
		<div class="topfields">
			<%= Html.TextBoxField("JoinFirstName", "").WithLabel("First name").WithIsRequired().WithAttribute("data-watermark", "") %>
			<%= Html.TextBoxField("JoinLastName", "").WithLabel("Last name").WithIsRequired().WithAttribute("data-watermark", "")%>
			<%= Html.TextBoxField("JoinEmailAddress", "").WithLabel("Your email").WithIsRequired().WithAttribute("data-watermark", "")%>
		    <div class="usernamearrow"></div>
		</div>
		<div class="divider"></div>
		<div class="addpassword">Now just add a password and your resume and let jobs find you</div>
	    <div class="left-side">
			<%= Html.PasswordField("JoinPassword", "").WithLabel("Password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
			<%= Html.PasswordField("JoinConfirmPassword", "").WithLabel("Confirm password").WithIsRequired().WithCssPrefix("password").WithAttribute("data-watermark", "") %>
			<div class="field resume_field read-only_field compulsory_field">
				<label for="Resume">Your resume</label>
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
		    <%= Html.CheckBoxField("TAndC", true).WithLabel("").WithLabelOnRight("I accept the LinkMe ").WithCssPrefix("TAndC").WithIsRequired().WithAttribute("url", SupportRoutes.Terms.GenerateUrl().AbsolutePath)%>
			<div class="createmylinkmeprofile" parseurl="<%= ProfilesRoutes.ApiParseResume.GenerateUrl() %>" joinurl="<%= JoinRoutes.ApiJoin.GenerateUrl() %>" ></div>
	    </div>
	    <div class="right-side">
		    <div class="bg">
		    </div>
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
<% } %>

<% if (Model.VisitorStatus.ShouldPrompt || (Model.JobAd.Processing == JobAdProcessing.ManagedExternally && CurrentMember == null))
       Html.RenderPartial("CompleteProfileOverlay", Model); %>
<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CareerObjectivesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var careerObjectivesMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
</script>

<div class="content careerobjectives">
	<div class="edit"></div>
	<div class="cancel"></div>
	<div class="view-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<div class="incomplete-alert objective-incomplete">
						<span class="title">You have not entered your Career objectives</span>
						<span class="desc">Employers and recruiters regularly use this information to assess suitable candidates. We suggest you add these details to increase your chances of being contacted.</span>
						<span class="clickon">Click on</span>
						<div class="edit-button"></div>
						<span class="afterbutton">to edit</span>
					</div>
					<div class="objective">
						<span class="title">Career Objectives</span>
						<span class="desc"></span>
					</div>
					<div class="divider gray"></div>
					<div class="divider light"></div>
					<div class="summary">
						<span class="title">Summary</span>
						<span class="desc"></span>
					</div>
					<div class="divider gray"></div>
					<div class="divider light"></div>
					<div class="skills">
						<span class="title">Skills</span>
						<span class="desc"></span>
					</div>
				</div>
			</div>
		</div>
		<div class="footer-bar"></div>
	</div>
	<div class="edit-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
			    <%= Html.MultilineTextBoxField(Model.Member, m => m.Objective).WithLabel("Career Objectives") %>
				<%= Html.MultilineTextBoxField(Model.Member, m => m.Summary).WithLabel("Summary") %>
				<%= Html.MultilineTextBoxField(Model.Member, m => m.Skills).WithLabel("Skills") %>
				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiCareerObjectives) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="footer-bar"></div>
	</div>
</div>
<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<OtherModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var otherMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
</script>

<div class="content other">
	<div class="edit"></div>
	<div class="cancel"></div>
	<div class="view-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<div class="incomplete-alert other-incomplete">
						<span class="title">You have not entered any additional information.</span>
						<span class="desc">Employers and recruiters are always looking for interesting candidates.</span>
						<span class="desc">Additional information helps you stand out from the crowd and increases your chances of being contacted.</span>
						<span class="clickon">Click on</span>
						<div class="edit-button"></div>
						<span class="afterbutton">to edit</span>
					</div>
					<div class="other-info">
						<div class="courses">
							<span class="title">Courses</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="awards">
							<span class="title">Awards</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="certifications">
							<span class="title">Professional certifications</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="interests">
							<span class="title">Interests</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="affiliation">
							<span class="title">Affiliation</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="others">
							<span class="title">Other</span>
							<span class="desc"></span>
						</div>
						<div class="divider gray"></div>
						<div class="divider light"></div>
						<div class="references">
							<span class="title">References</span>
							<span class="desc"></span>
						</div>
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
			    <%= Html.MultilineTextBoxField(Model.Member, m => m.Courses).WithLabel("Courses").WithHelpArea("Courses", "List relevant courses you have completed")%>
			    <%= Html.TextBoxField(Model.Member, m => m.Awards).WithLabel("Awards").WithHelpArea("Awards", "List professional awards")%>
				<%= Html.TextBoxField(Model.Member, m => m.Professional).WithLabel("Professional certifications")%>
                <%= Html.MultilineTextBoxField(Model.Member, m => m.Interests).WithLabel("Interests").WithHelpArea("Interests", "Companies may use this to determine if you fit their company culture")%>
                <%= Html.TextBoxField(Model.Member, m => m.Affiliations).WithLabel("Affiliations")%>
                <%= Html.MultilineTextBoxField(Model.Member, m => m.Other).WithLabel("Other").WithHelpArea("Other", "Add any extra information you consider relevant")%>
                <%= Html.MultilineTextBoxField(Model.Member, m => m.Referees).WithLabel("References").WithHelpArea("Referees", "<span>This should ideally include your current and most recent manager.</span><span>Remember to get their permission and prepare them for the call.</span>")%>
				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiOther) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="footer-bar"></div>
	</div>
</div>
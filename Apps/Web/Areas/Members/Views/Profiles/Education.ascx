<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<EducationModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var educationMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
</script>

<div class="content education">
	<div class="view-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<div class="item" item_id="">
						<div class="item-button">
							<div class="new"></div>
							<div class="whitespace"></div>
							<div class="edit"></div>
							<div class="cancel"></div>
							<div class="delete"></div>
							<div class="bottom-edge"></div>
						</div>
						<div class="header-bar"></div>
						<div class="bg">
							<div class="fg">
								<div class="incomplete-alert school-incomplete">
									<span class="title">You have not entered any education / qualifications.</span>
									<span class="desc">Employers and recruiters regularly use this information to assess suitable candidates. We suggest you add these details to increase your chances of being contacted.</span>
									<span class="clickon">Click on</span>
									<div class="new-button"></div>
									<span class="afterbutton">to add education / qualifications.</span>
								</div>
								<div class="incomplete-alert highest-level-incomplete">
									<span class="title">You have not entered your highest education level</span>
									<span class="desc">Employers and recruiters regularly use this information to assess suitable candidates. We suggest you add these details to increase your chances of being contacted.</span>
									<span class="clickon">Click on</span>
									<div class="edit-button"></div>
									<span class="afterbutton">to edit.</span>
								</div>
								<div class="item-header">
									<div class="highest-level">
										<span class="desc"></span>
									</div>
								</div>
								<div class="item-details">
									<div class="degree"></div>
									<div class="major title"></div>
									<div class="school title"></div>
									<div class="divider gray"></div>
									<div class="divider light"></div>
									<div class="completion">
										<span class="title">Completion</span>
										<div>
											<span class="desc"></span>
											<span class="desc gray"></span>
										</div>
									</div>
									<div class="description">
										<span class="desc"></span>
									</div>
								</div>
							</div>
						</div>
						<div class="footer-bar"></div>
					</div>
					<div class="item-divider">
						<div class="left-edge"></div>
						<div class="bg"></div>
						<div class="right-edge"></div>
					</div>
				</div>
			</div>
		</div>
		<div class="footer-bar"></div>
		<div class="bottom-new-icon"></div>
	</div>
	<div class="edit-mode hover">
		<div class="item-button">
			<div class="new"></div>
			<div class="whitespace"></div>
			<div class="edit"></div>
			<div class="cancel"></div>
			<div class="delete"></div>
			<div class="bottom-edge"></div>
		</div>
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="newtitle">Add Education/Qualification:</div>
				<div class="generalfields">
                    <%= Html.EducationLevelField(Model.Member, m => m.HighestEducationLevel)
                        .WithLabel("Highest education level")
                        .WithAttribute("size", "6")
                        .WithHelpArea("If your qualification isn't listed, pick the nearest equivalent. If you want to include multiple or unique qualifications, you can do so in your full profile later.") %>
					<div class="divider"></div>
					<div class="divider light"></div>
				</div>
				<% var school = Model.Member.Schools[0]; %>
				<%= Html.ControlsField().WithLabel("Completion")
					.Add(Html.DropDownListField(school, "EndDateMonth", s => s.EndDate == null ? (int?)null : s.EndDate.Value.Month, Model.Reference.Months)
                        .WithText(s => s == null ? "Current" : s.Value.GetMonthFullDisplayText()).WithCssPrefix("month").WithAttribute("size", "13"))
					.Add(Html.DropDownListField(school, "EndDateYear", s => s.EndDate == null ? (int?)null : s.EndDate.Value.Year, Model.Reference.Years)
                    .WithText(s => s == null ? "Year" : s.Value.ToString())            
						.WithCssPrefix("year").WithLabel("").WithAttribute("size", "10"))%>
					
				<%= Html.TextBoxField(school, s => s.Degree).WithLabel("Qualification").WithHelpArea("Avoid abbreviations")%>
				<%= Html.TextBoxField(school, s => s.Major).WithLabel("Major")%>
				<%= Html.TextBoxField(school, s => s.Institution).WithLabel("Institution")%>
				<%= Html.TextBoxField(school, s => s.City).WithLabel("City")%>
				<%= Html.MultilineTextBoxField(school, s => s.Description).WithLabel("Description")%>

				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiEducation) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="footer-bar"></div>
		<div class="bottom-new-icon"></div>
	</div>
</div>
<div class="prompt-layer delete">
	<div>
		<span class="prompt-text">Are you sure you want to delete this school?</span>
		<span class="prompt-text red bold">This action is not undoable!</span>
		<div class="delete" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiRemoveSchool) %>"></div>
		<div class="cancel"></div>
	</div>
</div>
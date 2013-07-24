<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<EmploymentHistoryModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var employmentMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
</script>

<div class="content employmenthistory">
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
								<div class="incomplete-alert job-incomplete">
									<span class="title">You have not entered any employment details</span>
									<span class="desc">Employers and recruiters regularly use this information to assess suitable candidates. We suggest you add these details to increase your chances of being contacted.</span>
									<span class="clickon">Click on</span>
									<div class="new-button"></div>
									<span class="afterbutton">to add a job.</span>
								</div>
								<div class="incomplete-alert recent-incomplete">
									<span class="title">You have not entered your most recent profession, most recent role seniority and industry experience</span>
									<span class="desc">Employers and recruiters regularly use this information to assess suitable candidates. We suggest you add these details to increase your chances of being contacted.</span>
									<span class="clickon">Click on</span>
									<div class="edit-button"></div>
									<span class="afterbutton">to edit.</span>
								</div>
								<div class="item-header">
									<div class="profession">
										<span class="desc"></span>
									</div>
									<div class="industry">
										<span class="desc"></span>
									</div>
								</div>
								<div class="item-details">
									<div class="job-title"></div>
									<div class="company title"></div>
									<div class="divider gray"></div>
									<div class="divider light"></div>
									<div class="tenure">
										<span class="title">Tenure</span>
										<div>
											<span class="desc"></span>
											<span class="desc gray"></span>
										</div>
									</div>
									<div class="duties">
										<span class="title">Responsibilities and duties</span>
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
				<div class="newtitle">Add Employment history:</div>
				<div class="generalfields">
					<%= Html.ProfessionField(Model.Member, m => m.RecentProfession)
						.WithLabel("Most recent profession").WithAttribute("size", "9")
						.WithHelpArea("Select the best description for your current or most recent role")%>
					<%= Html.SeniorityField(Model.Member, m => m.RecentSeniority)
						.WithLabel("Most recent role seniority").WithAttribute("size", "9")
						.WithHelpArea("Select the best description for your current or most recent role. Employers can search for candidates with suitable experience")%>
					<%= Html.IndustriesField(Model.Member, m => m.IndustryIds, Model.Reference.Industries)
						.WithLabel("Industry").WithAttribute("size", "6").WithAttribute("maxcount", "5")
						.WithHelpArea("Which industries have you worked in recently, or have the most experience in? Employers can search for candidates with experience in particular industries")%>
					<div class="divider"></div>
					<div class="divider light"></div>
				</div>
				<%var job = Model.Member.Jobs[0];%>
                <%= Html.ControlsField().WithLabel("Start Date")
	                .Add(Html.DropDownListField(job, "StartDateMonth", h => h.StartDate == null ? (int?)null : h.StartDate.Value.Month, Model.Reference.Months)
		                .WithText(h => h == null ? "Month" : h.Value.GetMonthFullDisplayText()).WithCssPrefix("month").WithAttribute("size", "12"))
	                .Add(Html.DropDownListField(job, "StartDateYear", h => h.StartDate == null ? (int?)null : h.StartDate.Value.Year, Model.Reference.Years)
                        .WithText(h => h == null ? "Year" : h.Value.ToString())
		                .WithCssPrefix("year").WithLabel("").WithAttribute("size", "10"))
					.WithHelpArea("StartDateYear", "We suggest that you list all the roles you've had, and provide a detailed description for those in the past 5-10 years")%>

                <%= Html.ControlsField().WithLabel("End Date")
	                .Add(Html.DropDownListField(job, "EndDateMonth", h => h.EndDate == null ? (int?)null : h.EndDate.Value.Month, Model.Reference.Months)
		                .WithText(h => h == null ? "Now" : h.Value.GetMonthFullDisplayText()).WithCssPrefix("month").WithAttribute("size", "12"))
	                .Add(Html.DropDownListField(job, "EndDateYear", h => h.EndDate == null ? (int?)null : h.EndDate.Value.Year, Model.Reference.Years)
                        .WithText(h => h == null ? "Year" : h.Value.ToString())
		                .WithCssPrefix("year").WithLabel("").WithAttribute("size", "10"))
					.WithHelpArea("EndDateYear", "\"Now\" means the job is current")%>
                		
                <%= Html.TextBoxField(job, h => h.Title).WithLabel("Job title").WithHelpArea("If your title is unusual, you could consider adapting it to ensure that employers can find you easily") %>
                <%= Html.TextBoxField(job, h => h.Company).WithLabel("Company name") %>
                <%= Html.MultilineTextBoxField(job, h => h.Description).WithLabel("Responsibilities and duties").WithHelpArea("<span>Be concise. Focus on your achievements and how your company benefited from your efforts.</span><span>Avoid jargon and abbreviations.</span>") %>

				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiEmploymentHistory) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="footer-bar"></div>
		<div class="bottom-new-icon"></div>
	</div>
</div>
<div class="prompt-layer delete">
	<div>
		<span class="prompt-text">Are you sure you want to delete this job?</span>
		<span class="prompt-text red bold">This action is not undoable!</span>
		<div class="delete" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiRemoveJob) %>"></div>
		<div class="cancel"></div>
	</div>
</div>
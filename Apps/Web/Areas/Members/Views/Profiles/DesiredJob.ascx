<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<DesiredJobModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Profiles"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<script type="text/javascript">
    var desiredJobMemberModel = <%= new ProfileJavaScriptSerializer().Serialize(Model.Member) %>;
</script>

<div class="content desiredjob">
	<div class="edit"></div>
	<div class="cancel"></div>
	<div class="view-mode">
		<div class="header-bar"></div>
		<div class="bg">
			<div class="fg">
				<div class="field">
					<div class="availability-icon"></div>
					<div class="title status"></div>
					<div class="job-title"></div>
					<div class="divider gray long"></div>
					<div class="job-type">
						<span class="title">I prefer </span>
						<span class="desc"></span>
					</div>
					<div class="salary">
						<span class="title red" style="display:none;">The information below is not shown to employers</span>
						<span class="title">My expected minimum salary is:</span>
						<span class="desc"></span>
					</div>
					<div class="relocation">
						<span class="title">I would consider relocation to:</span>
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
                <%= Html.TextBoxField(Model.Member, m => m.DesiredJobTitle)
                    .WithLabel("Desired job title")
                    .WithHelpArea("<span>The job title(s) that best describe the role(s) you're looking for.</span><span>Be realistic! Employers prefer candidates who understand their own capabilities.</span>") %>
                <%= Html.CheckBoxesField(Model.Member, m => m.DesiredJobTypes)
                    .Without(JobTypes.All)
                    .Without(JobTypes.None)
                    .WithCssPrefix("desiredjobtype")
                    .WithLabel("Desired job type")
                    .WithHelpArea("FullTime", "Full-time and Part-time reflect how many hours you can expect to work per week. Contract and Temp reflect the duration of the job contract Jobshare is a part-time role, shared with another employee.") %>
				<div class="jobtype-icon full-time" value="FullTime"></div>
				<div class="jobtype-icon part-time" value="PartTime"></div>
				<div class="jobtype-icon contract" value="Contract"></div>
				<div class="jobtype-icon temp" value="Temp"></div>
				<div class="jobtype-icon jobshare" value="JobShare"></div>
				<div class="divider"></div>
				<div class="divider light"></div>
                <%= Html.RadioButtonsField(Model.Member, m => m.Status)
                    .WithLabel("Your availability")
                    .WithIsRequired()
                    .WithCssPrefix("availability")
                    .WithHelpArea("AvailableNow", "<span>Keep your work status up to date so employers know if you're looking for work or not.</span><span>We'll contact you occasionally to confirm your availability.</span>") %>
                    
	            <div class="availability-icon immediately-available" value="AvailableNow"></div>
	            <div class="availability-icon actively-looking" value="ActivelyLooking"></div>
	            <div class="availability-icon not-looking-but-happy-to-talk" value="OpenToOffers"></div>
	            <div class="availability-icon not-looking" value="NotLooking"></div>
				<div class="salary-section">
					<div class="field">
						<label for="DesiredSalary">Expected minimum salary</label>
						<div class="mandatory"></div>
						<div class="total-remuneration">
							<%= Html.RadioButtonsField(Model.Member, m => m.DesiredSalaryRate)
								.With(Model.Reference.SalaryRates)
								.WithId(SalaryRate.Year, "SalaryRateYear")
								.WithId(SalaryRate.Hour, "SalaryRateHour")
								.WithLabel("Total remuneration:")
								.WithLabel(SalaryRate.Year, "per annum")
								.WithLabel(SalaryRate.Hour, "per hour")
                                .WithHelpArea("DesiredSalary", "<span>Include Super and the value of all bonuses and benefits.</span><span>Candidates who specify their salary expectations are more likely to be contacted by employers. There will always be an opportunity to negotiate this later.</span>")%>
							<div class="help-icon" helpfor="DesiredSalary"></div>
						</div>
						<div class="salary-range">
							<div class="left-range">$0</div>
							<div class="right-range">$250,000+</div>
						</div>
						<div class="salary-slider-bg"></div>
						<div class="salary-slider"></div>
						<div class="salary-slider-ruler"></div>
						<div class="salary-desc"><center>Select a minimum salary</center></div>
						<%= Html.TextBoxField(Model.Member, "SalaryLowerBound", m => m.DesiredSalaryLowerBound == null ? null : ((int) m.DesiredSalaryLowerBound.Value).ToString()) %>
						<%= Html.CheckBoxField(Model.Member, "IsSalaryNotVisible", m => m.IsSalaryNotVisible).WithLabelOnRight("Don't show to employers").WithCssPrefix("not-show-salary").WithHelpArea("not-show-salary", "<span>You can choose not to disclose your salary expectations to employers; however, we recommend you keep this visible as employers often exclude candidates who don't indicate their salary expectations</span>")%>
						<div class="help-icon" helpfor="not-show-salary"></div>
					</div>
				</div>
				<div class="divider"></div>
				<div class="divider light"></div>
                <%= Html.CheckBoxField(Model.Member, m => m.SendSuggestedJobs)
                    .WithLabel("Suggested jobs")
                    .WithHelpText("Email me suggested jobs")%>
                <div class="help-area" helpfor="SendSuggestedJobs">
                    <div class="triangle"></div>
                    <div class="help-text">
                        <span>This sets up an updated email alert for advertised jobs that match your profile. You can unsubscribe at any time.</span>
                    </div>
                </div>
				<div class="divider"></div>
				<div class="divider light"></div>
                <%= Html.RadioButtonsField(Model.Member, m => m.RelocationPreference)
                    .WithLabel("Would you move for a job?")
                    .WithId(RelocationPreference.Yes, "RelocationPreferenceYes")
                    .WithId(RelocationPreference.No, "RelocationPreferenceNo")
                    .WithId(RelocationPreference.WouldConsider, "RelocationPreferenceWouldConsider")
                    .WithLabel(RelocationPreference.WouldConsider, "Would consider")
                    .WithCssPrefix("RelocationPreference")
                    .WithHelpArea("RelocationPreferenceWouldConsider", "Employers can search for candidates within a particular location and may consider candidates who can relocate to that area.") %>
                    
                <div id="WhereToRelocate">
                    <%= Html.CountryField(Model, "CountryId", m => m.Reference.CurrentCountry.Id, Model.Reference.Countries)
                        .WithLabel("Where can you relocate?")
                        .WithAttribute("size", "6")
                        .WithHelpArea("Select which countries, states and regions you can locate to.") %>

                    <div class="field">
                        <label></label>
	                    <div class="aus-map">
<%      foreach (var subdivision in Model.Reference.CountrySubdivisions)
        {
            if (subdivision.Name == "Australian Capital Territory") continue;
            %>	                    
		                    <div class="state" state="<%= subdivision.Name %>"></div>
<%      } %>
                            <div class="region" region="Australian Capital Territory"></div>
<%      foreach (var region in Model.Reference.Regions)
        { %>	                    
		                    <div class="region" region="<%= region.Name %>"></div>
<%      } %>
<%      foreach (var region in Model.Reference.Regions)
        { %>	                    
		                    <div class="hint" hintfor="<%= region.Name %>"></div>
<%      } %>
                            <div class="hint" hintfor="Australian Capital Territory"></div>                        
		                    <img usemap="#mapAus" src="data:image/gif;base64,R0lGODlhAQABAJH/AP///wAAAMDAwAAAACH5BAEAAAIALAAAAAABAAEAAAICVAEAOw==" />
		                    <map name="mapAus" >
			                    <area shape="poly" href="javascript:void(0);" coords="120,43,105,33,75,56,69,62,70,71,64,79,48,85,30,89,10,100,2,133,21,179,29,179,30,183,30,192,22,195,18,197,17,203,29,210,38,211,52,201,75,199,120,181" areafor="Western Australia" />
			                    <area shape="poly" href="javascript:void(0);" coords="123,44,130,39,137,36,151,35,151,29,161,28,160,18,170,22,179,17,183,23,183,38,172,44,191,57,191,136,123,136" areafor="Northern Territory" />
			                    <area shape="poly" href="javascript:void(0);" coords="124,140,213,140,213,239,208,239,204,234,203,226,197,220,188,224,182,224,180,221,185,219,188,219,191,220,195,218,199,219,200,215,204,210,201,207,196,210,193,211,191,209,189,217,183,218,184,214,189,199,175,215,171,211,162,200,156,192,142,187,124,188" areafor="South Australia" />
			                    <area shape="poly" href="javascript:void(0);" coords="194,57,206,56,207,64,215,64,218,57,220,47,221,23,228,9,240,38,249,43,255,64,257,78,275,87,288,104,303,129,308,125,308,130,305,133,303,129,299,139,302,143,307,143,309,146,303,146,297,147,297,155,299,158,293,161,280,157,276,160,216,160,216,136,194,136" areafor="Queensland" />
			                    <area shape="poly" href="javascript:void(0);" coords="217,164,277,164,279,161,289,161,295,164,298,164,298,160,300,159,309,157,311,165,302,194,297,196,283,196,283,202,253,202,253,212,276,212,276,202,283,202,283,210,291,210,286,221,284,234,280,234,270,229,267,223,245,223,225,208,217,207" areafor="New South Wales" />
			                    <area shape="poly" href="javascript:void(0);" coords="216,211,225,212,245,228,251,225,268,227,271,234,283,239,281,242,268,245,257,253,252,252,245,248,246,243,252,242,256,240,247,233,242,241,246,243,235,251,221,248,216,244" areafor="Victoria" />
			                    <area shape="poly" href="javascript:void(0);" coords="237,251,242,261,251,265,254,276,255,285,261,279,262,264,265,262,266,253,270,259,270,274,265,284,259,285,252,285,247,279,223,278,223,270,243,269,241,261,235,256" areafor="Tasmania" />
			                    <area shape="poly" href="javascript:void(0);" coords="134,13,132,19,138,20,145,16,150,17,149,22,138,21,130,39,137,36,151,35,151,29,161,28,160,18,154,17,150,12,137,15" areafor="Darwin" />
			                    <area shape="poly" href="javascript:void(0);" coords="21,179,29,179,30,183,30,192,22,193,23,184" areafor="Perth" />
			                    <area shape="poly" href="javascript:void(0);" coords="251,265,254,276,255,285,261,279,262,264,257,265" areafor="Hobart" />
			                    <area shape="poly" href="javascript:void(0);" coords="247,233,256,240,252,242,242,241" areafor="Melbourne" />
			                    <area shape="poly" href="javascript:void(0);" coords="297,147,297,155,308,152,308,146,303,146" areafor="Gold Coast" />
			                    <area shape="poly" href="javascript:void(0);" coords="303,129,299,139,302,143,307,143,305,133" areafor="Brisbane" />
			                    <area shape="poly" href="javascript:void(0);" coords="283,196,283,210,290,210,296,197" areafor="Sydney" />
			                    <area shape="poly" href="javascript:void(0);" coords="254,202,276,202,276,212,254,212" areafor="Australian Capital Territory" />
			                    <area shape="poly" href="javascript:void(0);" coords="196,210,201,207,204,210,200,215,199,219,194,219,196,216" areafor="Adelaide" />
		                    </map>
	                    </div>
	                    <div id="AnyWhereInAus">
							<div class="checkbox"></div>
							<span>Anywhere in <%= Model.Reference.CurrentCountry.Name %></span>
						</div>
                    </div>
                    <div class="field">
                        <label>Locations you've selected</label>
                        <div>
                            <div id="RelocationLocation">
                                <%= Html.CountriesField(Model.Member, m => m.RelocationCountryIds, Model.Reference.Countries).WithLabel(" ") %>
                                <%= Html.LocationsField(Model.Member, m => m.RelocationCountryLocationIds, Model.Reference.CountrySubdivisions.Cast<NamedLocation>().Concat(Model.Reference.Regions.Cast<NamedLocation>())).WithLabel(" ") %>
                            </div>
                        </div>
                    </div>
                </div>
				<div class="save" url="<%= Html.RouteRefUrl(ProfilesRoutes.ApiDesiredJob) %>"></div>
				<div class="cancel"></div>
			</div>
		</div>
		<div class="footer-bar"></div>
	</div>
</div>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<JobDetailsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Candidates"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Join"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Join)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.JoinFlow) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="joinflow-header">Get Started: Create your profile</div>
	<div class="joinflow-steps"><% Html.RenderPartial("Steps", Model); %></div>
	
    <%= Html.ErrorSummary("There are some errors, please correct them below.")%>

<%  using (Html.RenderForm(Model.GetUrl(Context.GetClientUrl(true)), "JobDetailsForm"))
    { %>
	<div id="joinflow-content">
        <div class="fieldsets">

<%      using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (Model.IsUploadingResume)
            { %>
                <div class="alert">
                    <span class="alert-text">Candidates who show complete this section are <span class="red">63%</span> more likely to be contacted by potential employers.</span>
                    <span class="alert-text">Please review the information below and complete any remaining blank fields.</span>
                </div>
<%          }

            if (!Model.IsUploadingResume)
            { %>
				<div class="divider-2-1"></div>
				<div>
					<%= Html.TextBoxField(Model.Member, m => m.JobTitle)
					    .WithLabel("Most recent job title")
                        .WithHelpArea("Your current role, or if you're currently unemployed, the title of your most recent job.") %>
                        
					<%= Html.TextBoxField(Model.Member, m => m.JobCompany)
					    .WithLabel("Most recent employer")
					    .WithHelpArea("Your current employer, or if you're currently unemployed, the name of your most recent employer.") %>
				</div>
<%          } %>

            <div class="divider-2-1"></div>
            <div class="experience">
                <%= Html.SeniorityField(Model.Member, m => m.RecentSeniority)
                    .WithLabel("Most recent role seniority")
                    .WithAttribute("size", "9")
                    .WithHelpArea("Select the best description for your current or most recent role. Employers can search for candidates with suitable experience.") %>

                <%= Html.ProfessionField(Model.Member, m => m.RecentProfession)
                    .WithLabel("Most recent profession")
                    .WithAttribute("size", "9")
                    .WithHelpArea("Select the best description for your current or most recent role.") %>
                    
                <%= Html.IndustriesField(Model.Member, m => m.IndustryIds, Model.Reference.Industries)
                    .WithLabel("Industry")
                    .WithHelpArea("Which industries have you worked in recently, or have the most experience in? Employers can search for candidates with experience in particular industries.") %>
                    
                <%= Html.EducationLevelField(Model.Member, m => m.HighestEducationLevel)
                    .WithLabel("Highest education level")
                    .WithAttribute("size", "6")
                    .WithHelpArea("If your qualification isn't listed, pick the nearest equivalent. If you want to include multiple or unique qualifications, you can do so in your full profile later.") %>
            </div>
            <div class="divider-2-1"></div>
            <div class="desired-job">
                <%= Html.TextBoxField(Model.Member, m => m.DesiredJobTitle)
                    .WithLabel("Desired job title(s)")
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
            </div>
            
            <div class="divider-2-1"></div>
            <div class="ethnic-status">
                <%= Html.CheckBoxesField(Model.Member, m => m.EthnicStatus)
                    .Without(EthnicStatus.None)
                    .WithLabel(EthnicStatus.Aboriginal, EthnicStatus.Aboriginal.GetDisplayText())
                    .WithLabel(EthnicStatus.TorresIslander, EthnicStatus.TorresIslander.GetDisplayText())
                    .WithLabel("Show employers that you are...")
                    .WithHelpArea("TorresIslander", "If these apply to you, we recommend you select the appropriate options as employers can specifically search for Indigenous candidates.") %>
            </div>

            <div class="divider-2-1"></div>
            <div class="relocate-section">
                <%= Html.RadioButtonsField(Model.Member, m => m.RelocationPreference)
                    .WithLabel("Would you move for a job?")
                    .WithId(RelocationPreference.Yes, "RelocationPreferenceYes")
                    .WithId(RelocationPreference.No, "RelocationPreferenceNo")
                    .WithId(RelocationPreference.WouldConsider, "RelocationPreferenceWouldConsider")
                    .WithLabel(RelocationPreference.WouldConsider, "Would consider")
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
            </div>
            
            <div class="divider-2-1"></div>
            <div class="secondary-info">
                <%= Html.TextBoxField(Model.Member, m => m.SecondaryEmailAddress)
                    .WithLabel("Secondary e-mail")
                    .WithHelpArea("Used in case your primary email address isn't working.") %>
                    
                <%= Html.TextBoxField(Model.Member, m => m.SecondaryPhoneNumber)
                    .WithLabel("Secondary phone")
                    .WithHelpArea("Shown to employers on your Profile. We recommend you have two contact phone numbers to ensure employers can get in touch easily.") %>
                    
                <%= Html.RadioButtonsField(Model.Member, m => m.SecondaryPhoneNumberType)
                    .WithLabel("") %>

            </div>
            
            <div class="divider-2-1"></div>
			<div class="visa-info">
			    <%= Html.TextBoxField(Model.Member, m => m.Citizenship)
			        .WithLabel("Citizenship")
			        .WithHelpArea("List your citizenship details") %>
			        
			    <%= Html.RadioButtonsField(Model.Member, m => m.VisaStatus)
			        .WithLabel(VisaStatus.Citizen, VisaStatus.Citizen.GetDisplayText())
			        .WithLabel(VisaStatus.UnrestrictedWorkVisa, VisaStatus.UnrestrictedWorkVisa.GetDisplayText())
			        .WithLabel(VisaStatus.RestrictedWorkVisa, VisaStatus.RestrictedWorkVisa.GetDisplayText())
			        .WithLabel(VisaStatus.NoWorkVisa, VisaStatus.NoWorkVisa.GetDisplayText())
			        .WithLabel(VisaStatus.NotApplicable, VisaStatus.NotApplicable.GetDisplayText())
			        .WithLabel("Visa")
			        .WithCssPrefix("Visa")
			        .WithHelpArea("NotApplicable", "Visa restrictions include being sponsored by a particular employer or being limited to a certain number of hours per week.") %>

			</div>
			
            <div class="divider-2-1"></div>
            <div class="private-info">
                <%= Html.RadioButtonsField(Model.Member, m => m.Gender)
                    .WithLabel("Gender")
                    .WithIsRequired()
                    .WithHelpArea("Female", "For our records only - never shown to employers.") %>
                    
                <%= Html.ControlsField()
                    .WithLabel("Date of birth")
                    .Add(Html.DropDownListField(Model.Member, "DateOfBirthMonth", m => m.DateOfBirth == null ? (int?)null : m.DateOfBirth.Value.Month, Model.Reference.Months)
                        .WithText(m => m == null ? "" : m.Value.GetMonthDisplayText())
                        .WithCssPrefix("month").WithAttribute("size", "12"))
                    .Add(Html.DropDownListField(Model.Member, "DateOfBirthYear", m => m.DateOfBirth == null ? (int?)null : m.DateOfBirth.Value.Year, Model.Reference.Years)
                        .WithCssPrefix("year")
                        .WithLabel("")
                        .WithAttribute("size", "10"))
                    .WithIsRequired()
                    .WithHelpArea("DateOfBirthYear", "For our records only - never shown to employers.") %>
                    
                <%= Html.Hidden("DateOfBirth") %>

            </div>
            <div class="divider-2-1"></div>
            <div class="other-info">
                <%= Html.CheckBoxField(Model, m => m.SendSuggestedJobs)
                    .WithLabel("Suggested jobs")
                    .WithHelpText("Email me suggested jobs") %>
                    
                <%= Html.ExternalReferralField(Model, m => m.ExternalReferralSourceId, Model.Reference.ExternalReferralSources)
                    .WithLabel("How did you find LinkMe?")
                    .WithAttribute("size", "10")
					.WithHelpArea("For our own records") %>
                <div class="help-area" helpfor="SendSuggestedJobs">
                    <div class="triangle"></div>
                    <div class="help-text">
                        <span>This sets up an updated email alert for advertised jobs that match your profile. You can unsubscribe at any time.</span>
                    </div>
                </div>
            </div>
            <div class="finish-button"></div>
			<div class="button-spliter"></div>
            <a class="back-link" href="<%= Html.RouteRefUrl(JoinRoutes.PersonalDetails) %>">Back to confirm your details</a>
            <%= Html.ButtonsField().Add(new PreviousButton()).Add(new NextButton())%>
        <% } %>
        </div>
    </div>
<%  } %>                        

	<script language="javascript" type="text/javascript">

	    var urls = {
	        Join: '<%= Html.RouteRefUrl(JoinRoutes.Join) %>',
	        PersonalDetails: '<%= Html.RouteRefUrl(JoinRoutes.PersonalDetails) %>',
	        JobDetails: '<%= Html.RouteRefUrl(JoinRoutes.JobDetails) %>',
	        Activate: '<%= Html.RouteRefUrl(JoinRoutes.Activate) %>'
	    };
	
	    $(document).ready(function() {
	        initScriptForStep("JobDetails", urls);
	    });
    </script>
</asp:Content>


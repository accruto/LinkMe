<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Query.Members"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Widgets"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>

<!-- Filters -->
<% using (Html.RenderForm("FiltersForm"))
   { %>
<div class="filters_ascx refine_section section js_collapsible">
    <div class="overlay-container forms_v2">
	    <div class="custom-overlay shadow" style="display:none;">
		    <div class="overlay">
			    <div class="overlay-title"><span class="overlay-title-text"> Title </span><span class="close-icon"></span></div>
			    <div class="overlay-content">
			         <div class="overlay-text">Text goes here</div>
			         <div class="buttons-holder">
	                 </div>
			    </div>
		    </div>
	    </div>
    </div>
	<div class="section-collapsible-title section-collapsible-title-active main-title">
	    <div class="main-title-nav-icon filter-section-icon">Refine and filter results</div>
	</div>			
	<div class="section-content section-collapsible-content">				
	<% if (!(Model is SuggestedCandidatesListModel))
    { %>
		<div class="section">
			<!-- Non collapsible - links section -->
			<div class="section-content">
                <ul class="plain_action-list action-list">
                    <li><a class="save-search-action js_save-search" href="javascript:void(0)">Save search</a></li>
                    <li><a class="new-candidate-alert-action js_email-updates" href="javascript:void(0)">Email me updates to results</a></li>
                </ul>
            </div>
        </div>
	<%} %>
		
		<!-- Job Hunter Status filter section -->
		
		<div class="candidate-status_section section js_collapsible">
		    <input id="chkJobHunterStatus" type="checkbox" class="section-icon-clear-chk"/><label for="chkJobHunterStatus"></label>&nbsp;
			<div class="section-collapsible-title">
			    Job Hunter status
			</div>
			<div class="section-content">
                <div id="candidateStatusCheckboxes" class="pushchecks forms_v2">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.CheckBoxesField(Model.Criteria, c => c.CandidateStatusFlags)
                                .Without(CandidateStatusFlags.NotLooking)
                                .Without(CandidateStatusFlags.All) %>
                     <% } %>
                </div>
                <div class="count_holder">
                    <div class="job-hunter-status-count_holder">(<span class="candidate-status-count <%= CandidateStatusFlags.AvailableNow %>">0</span>)</div>
                    <div class="job-hunter-status-count_holder">(<span class="candidate-status-count <%= CandidateStatusFlags.ActivelyLooking %>">0</span>)</div>
                    <div class="job-hunter-status-count_holder">(<span class="candidate-status-count <%= CandidateStatusFlags.OpenToOffers %>">0</span>)</div>
                    <div class="job-hunter-status-count_holder">(<span class="candidate-status-count <%= CandidateStatusFlags.Unspecified %>">0</span>)</div>
                </div>
            </div>
        </div>

        <!-- Jop Type filter section -->
        
        <div class="job-type_section section js_collapsible js_open-if-active">
            <input id="chkJobType" type="checkbox" class="section-icon-clear-chk"/><label for="chkJobType"></label>&nbsp;
			<div class="section-collapsible-title">
			    Full-time, part time, etc.
			</div>
            <div class="section-content">
                <div id="jobTypeCheckboxes" class="pushchecks forms_v2">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.CheckBoxesField(Model.Criteria, c => c.JobTypes)
                                .Without(JobTypes.None)
                                .Without(JobTypes.All) %>
                     <% } %>
                </div>
                <div class="count_holder">
                    <div class="job-type-count_holder">(<span class="job-type-count <%= JobTypes.FullTime %>">0</span>)</div>
                    <div class="job-type-count_holder">(<span class="job-type-count <%= JobTypes.PartTime %>">0</span>)</div>
                    <div class="job-type-count_holder">(<span class="job-type-count <%= JobTypes.Contract %>">0</span>)</div>
                    <div class="job-type-count_holder">(<span class="job-type-count <%= JobTypes.Temp %>">0</span>)</div>
                    <div class="job-type-count_holder">(<span class="job-type-count <%= JobTypes.JobShare %>">0</span>)</div>
                </div>
            </div>
        </div>
        
        <!-- Salary filter section -->
        
        <div class="desired-salary_section section js_collapsible js_open-if-active">
            <input id="chkSalary" type="checkbox" class="section-icon-clear-chk"/><label for="chkSalary"></label>&nbsp;
			<div class="section-collapsible-title">
			    Desired salary <small>(per annum)</small>
			</div>
            <div class="section-content forms_v2">
                <div class="inputs_for_sliders">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.TextBoxField(Model, "SalaryLowerBound", m => m.Criteria.Salary != null && m.Criteria.Salary.LowerBound != null ? (int)m.Criteria.Salary.LowerBound.Value : m.MinSalary)
                                    .WithLabel("From") %>
                            <%= Html.TextBoxField(Model, "SalaryUpperBound", m => m.Criteria.Salary != null && m.Criteria.Salary.UpperBound != null ? (int)m.Criteria.Salary.UpperBound.Value : m.MaxSalary)
                                    .WithLabel("To") %>
                     <% } %>
                </div>
			    <div class="slider-limits">
				    <div class="minrange"></div>
				    <div class="maxrange"></div>
			    </div>
			    <div class="slider-holder">
				    <div id="salary-range"></div>
				    <div class="ruler50"> &nbsp; </div>
			    </div>
			    <div>
				    <center><div class="range"></div></center>
			    </div>
			    
                 <% using (Html.BeginFieldSet())
                    { %>
                        <div class="checkbox_field field">
                            <div class="checkbox_control control">
                                <input type="checkbox" id="ExcludeNoSalary" class="checkbox js_exclude-no-salary" <% if (Model.Criteria.ExcludeNoSalary) { %>checked<% } %> /><label for="excludeNoMinSal">Exclude results without a salary</label>
                            </div>
                        </div>
                 <% } %>
			</div>
		</div>

		<!-- Resume recency filter section -->
		
		<div class="resume-recency_section section js_collapsible js_open-if-active">
		    <input id="chkResumeRecency" type="checkbox" class="section-icon-clear-chk"/><label for="chkResumeRecency"></label>&nbsp;
			<div class="section-collapsible-title">
				Resume recency
			</div>
			<div class="section-content">
			    <div class="forms_v2 inputs_for_sliders">
                     <input type="text" name="Recency" id="Recency" value="<%= Model.Criteria.Recency != null ? Model.Criteria.Recency.Value.Days.ToString() : "" %>" />
                </div>
				<div class="slider-limits">
					<div class="minrange"></div>
					<div class="maxrange"></div>
				</div>
				<div class="slider-holder">
					<div id="time-range"></div>
					<div class="ruler10"> &nbsp; </div>
				</div>
				<div>
					<center><div class="range"></div></center>
				</div>
			</div>
		</div>
		
		<!-- Distance filter section -->

		<div class="distance_section section js_collapsible js_open-if-active">
		    <input id="chkDistance" type="checkbox" class="section-icon-clear-chk"/><label for="chkDistance"></label>&nbsp;
			<div class="section-collapsible-title">
				Distance
			</div>
			<div class="section-content">
				<div class="no_loc">
					<div class="small">No location specified. <a href="#" class="js_dist-key-loc<%if (Model is SuggestedCandidatesListModel) { %> loc-suggest-candidate<% } %>">Enter a location</a> to filter candidates by distance.</div>
				</div>
				<div class="spec_loc">
					<div class="small">From <span class="loc_label"><%= Model.Criteria.Location%></span> <a href="#" class="js_dist-key-loc<%if (Model is SuggestedCandidatesListModel) { %> loc-suggest-candidate<% } %>">Change</a></div>
					<div class="slider-limits">
						<div class="minrange"></div>
						<div class="maxrange"></div>
					</div>
					<div class="slider-holder">
						<div id="dist-range"></div>
						<div class="ruler8"> &nbsp; </div>
					</div>
					<div>
						<center><div class="range"></div></center>
					</div>
					<div class="small">
					    <input id="IncludeRelocatingFilter" name="IncludeRelocating" type="checkbox" <% if (Model.Criteria.IncludeRelocating){ %> checked="checked" <% } %> value="true" tabindex="17"/>
                        <input name="IncludeRelocating" type="hidden" value="false" />
                        Include those willing to relocate here
                    </div>
                    <div class="small">
					    <input id="IncludeInternationalFilter" name="IncludeInternational" type="checkbox" <% if (Model.Criteria.IncludeInternational){ %> checked="checked" <% } %> value="true" <% if (!Model.Criteria.IncludeRelocating){ %> disabled="disabled" <% } %> tabindex="18"/>
                         <input name="IncludeInternational" type="hidden" value="false" />
                         Include international candidates
                     </div>
				</div>
			</div>
		</div>

		<!-- Employer filter section -->
		
		<div class="employer_section section js_collapsible js_open-if-active">
		    <input id="chkEmployer" type="checkbox" class="section-icon-clear-chk"/><label for="chkEmployer"></label>&nbsp;
			<div class="section-collapsible-title">
			    Employer
			</div>
            <div class="section-content">
                <div class="forms_v2 small js_contains_watermarked">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.TextBoxField(Model.Criteria, "CompanyKeywords", c => c.CompanyKeywords)
                                .WithLabel("")
                                .WithAttribute("data-watermark", "e.g. David Jones")
                                .WithAttribute("autocomplete", "off") %>
                            <%= Html.ButtonsField().WithCssPrefix("go_button").Add(new GoButton("GoEmployer"))%>
                            <%= Html.RadioButtonsField(Model.Criteria, c => c.CompaniesToSearch)
                                .WithLabel("")
                                .WithLabel(JobsToSearch.LastJob, "Current job").WithId(JobsToSearch.LastJob, "LastCompany")
                                .WithLabel(JobsToSearch.RecentJobs, "Most recent 3").WithId(JobsToSearch.RecentJobs, "RecentCompanies")
                                .WithLabel(JobsToSearch.AllJobs, "Any").WithId(JobsToSearch.AllJobs, "AllCompanies")
                            %>
                     <% } %>
                </div>
            </div>
        </div>
        
		<!-- Education filter section -->

		<div class="education_section section js_collapsible js_open-if-active">
		    <input id="chkEducation" type="checkbox" class="section-icon-clear-chk"/><label for="chkEducation"></label>&nbsp;
			<div class="section-collapsible-title">
				Education
			</div>
			<div class="section-content">
                <div class="forms_v2 js_contains_watermarked">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.TextBoxField(Model.Criteria, "EducationKeywords", c => c.EducationKeywords)
                                .WithLabel("")
                                .WithAttribute("data-watermark", "e.g. Bachelor of Arts or MBA")
                                .WithAttribute("autocomplete", "off") %>
                            <%= Html.ButtonsField().WithCssPrefix("go_button").Add(new GoButton("GoEducation")) %>
                     <% } %>
                </div>
			</div>
		</div>

		<!-- Industry filter section -->
		
		<div class="industry_section section js_collapsible js_open-if-active">
		    <input id="chkIndustry" type="checkbox" class="section-icon-clear-chk"/><label for="chkIndustry"></label>&nbsp;
			<div class="section-collapsible-title">
				Industry
			</div>
			<div class="section-content">
			    <div class="industries_section toggle-checkboxes js_toggle_checkboxes js_collapsible_toggle_checkboxes">
				    <div class="parent-toggle-checkbox-holder">
				        <div class="parent-holder">
				            <input id="all-industries" type="checkbox" <%= Model.Criteria.IndustryIds != null && Model.Criteria.IndustryIds.Count > 0 ? "" : "checked=\"checked\"" %> />
				            <label class="js_ellipsis" for="all-industries">All</label>
				        </div>
				    </div>
				    <div class="children-toggle-checkbox-holder list-right">
				    <% for (var index = 0; index < Model.Industries.Count; ++index)
                       {
                           var industry = Model.Industries[index]; %>
    			            <div class="child-holder">
    			                <input type="checkbox" id="industry<%=index %>" name="IndustryIds" value="<%= industry.Id %>" <%= Model.Criteria.IndustryIds != null && Model.Criteria.IndustryIds.Contains(industry.Id) ? "checked=\"checked\"" : "" %> />
    			                <label for="industry<%= index %>" ><label class="js_ellipsis"><%= industry.Name %></label>&nbsp;<span class="industries-count_holder">(<span class="industries-count <%= industry.Id %>">0</span>)</span></label>                           
    			            </div>
			        <% } %>
			        </div>
				</div>
			</div>
		</div>

		<!-- Non-profile information filter section -->
		
		<div class="filter-by-activity_section section js_collapsible js_open-if-active">
		    <input id="chkFilterByActivity" type="checkbox" class="section-icon-clear-chk"/><label for="chkFilterByActivity"></label>&nbsp;
			<div class="section-collapsible-title">
				Non-profile information
			</div>
			<div class="section-content">
				<table class="filter-by-activity_table">
				    <tr>
				        <td>&nbsp;</td>
				        <td>Either</td>
				        <td>Yes</td>
				        <td>No</td>
				    </tr>
				    <tr>
				        <td><span class="in-folders-icon">In a folder</span></td>
				        <td><input id="InFolderEither" class="filter-activity-either" type="radio" name="InFolder" value="" <%= Model.Criteria.InFolder == null ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="InFolderYes" class="filter-activity-yes" type="radio" name="InFolder" value="true" <%= Model.Criteria.InFolder == true ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="InFolderNo" class="filter-activity-no" type="radio" name="InFolder" value="false" <%= Model.Criteria.InFolder == false ? "checked=\"checked\"" : "" %> /></td>
				    </tr>
				    <tr>
				        <td><span class="has-notes-icon">Has notes</span></td>
				        <td><input id="HasNotesEither" class="filter-activity-either" type="radio" name="HasNotes" value="" <%= Model.Criteria.HasNotes == null ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="HasNotesYes" class="filter-activity-yes" type="radio" name="HasNotes" value="true" <%= Model.Criteria.HasNotes == true ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="HasNotesNo" class="filter-activity-no" type="radio" name="HasNotes" value="false" <%= Model.Criteria.HasNotes == false ? "checked=\"checked\"" : "" %> /></td>
				    </tr>
				    <tr>
				        <td><span class="viewed-icon">Was viewed</span></td>
				        <td><input id="HasViewedEither" class="filter-activity-either" type="radio" name="HasViewed" value="" <%= Model.Criteria.HasViewed == null ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="HasViewedYes" class="filter-activity-yes" type="radio" name="HasViewed" value="true" <%= Model.Criteria.HasViewed == true ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="HasViewedNo" class="filter-activity-no" type="radio" name="HasViewed" value="false" <%= Model.Criteria.HasViewed == false ? "checked=\"checked\"" : "" %> /></td>
				    </tr>
				    <tr>
				        <td><span class="is-flagged-icon">Is flagged</span></td>
				        <td><input id="IsFlaggedEither" class="filter-activity-either" type="radio" name="IsFlagged" value="" <%= Model.Criteria.IsFlagged == null ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="IsFlaggedYes" class="filter-activity-yes" type="radio" name="IsFlagged" value="true" <%= Model.Criteria.IsFlagged == true ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="IsFlaggedNo" class="filter-activity-no" type="radio" name="IsFlagged" value="false" <%= Model.Criteria.IsFlagged == false ? "checked=\"checked\"" : "" %> /></td>
				    </tr>
				    <tr>
				        <td><span class="contact-unlocked-icon">Contact is unlocked</span></td>
				        <td><input id="IsUnlockedEither" class="filter-activity-either" type="radio" name="IsUnlocked" value="" <%= Model.Criteria.IsUnlocked == null ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="IsUnlockedYes" class="filter-activity-yes" type="radio" name="IsUnlocked" value="true" <%= Model.Criteria.IsUnlocked == true ? "checked=\"checked\"" : "" %> /></td>
				        <td><input id="IsUnlockedNo" class="filter-activity-no" type="radio" name="IsUnlocked" value="false" <%= Model.Criteria.IsUnlocked == false ? "checked=\"checked\"" : "" %> /></td>
				    </tr>
				</table>
			</div>
		</div>
		
		<!-- Visa status filter section -->

		<div class="visa-status_section section js_collapsible js_open-if-active">
		    <input id="chkVisaStatus" type="checkbox" class="section-icon-clear-chk"/><label for="chkVisaStatus"></label>&nbsp;
			<div class="section-collapsible-title">
				Visa status
			</div>
			<div class="section-content">
			    <div class="visa-status_text">Show only:</div>
				<div id="VisaStatusCheckBoxes" class="visaStatusChecks forms_v2">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.CheckBoxesField(Model.Criteria, c => c.VisaStatusFlags == null ? VisaStatusFlags.All : c.VisaStatusFlags.Value)
						        .Without(VisaStatusFlags.All)
                                .WithLabel(VisaStatusFlags.Citizen, VisaStatusFlags.Citizen.GetDisplayText())
                                .WithLabel(VisaStatusFlags.UnrestrictedWorkVisa, VisaStatusFlags.UnrestrictedWorkVisa.GetDisplayText())
                                .WithLabel(VisaStatusFlags.RestrictedWorkVisa, VisaStatusFlags.RestrictedWorkVisa.GetDisplayText())
                                .WithLabel(VisaStatusFlags.NoWorkVisa, VisaStatusFlags.NoWorkVisa.GetDisplayText())
                                .WithVertical()%>
                            <%= Html.CheckBoxField(Model.Criteria, "VisaStatus", c => c.VisaStatusFlags != null)
                                .WithIsHidden() %>
                     <% } %>
                </div>
			</div>
		</div>

		<!-- Indigenous status filter section -->

		<div class="indigenous-status_section section js_collapsible js_open-if-active">
		    <input id="chkIndigenousStatus" type="checkbox" class="section-icon-clear-chk"/><label for="chkIndigenousStatus"></label>&nbsp;
			<div class="section-collapsible-title">
				Indigenous status
			</div>
			<div class="section-content">
			    <div class="indigenous-status_text">Show only:</div>
				<div id="IndegenousStatusCheckboxes" class="indigenousStatusChecks forms_v2">
                     <% using (Html.BeginFieldSet())
                        { %>
                            <%= Html.CheckBoxesField(Model.Criteria, c => c.EthnicStatus == null ? EthnicStatus.Aboriginal | EthnicStatus.TorresIslander : c.EthnicStatus.Value)
						        .Without(EthnicStatus.None)
                                .WithLabel(EthnicStatus.Aboriginal, "Australian Aboriginal")
                                .WithLabel(EthnicStatus.TorresIslander, "Torres Strait Islander")
                                .WithVertical() %>
                            <%= Html.CheckBoxField(Model.Criteria, "EthnicStatus", c => c.EthnicStatus != null)
                                .WithIsHidden() %>
                     <% } %>
                </div>
			</div>
		</div>

		<!-- Communities filter section -->
		
<%  if (Model.CanSelectCommunity)
    { %>		
		<div class="communities_section section js_collapsible js_open-if-active">
		    <input id="chkCommunities" type="checkbox" class="section-icon-clear-chk"/><label for="chkCommunities"></label>&nbsp;
			<div class="section-collapsible-title">
				Communities
			</div>
			<div class="section-content">
				<div id="CommunitiesDropdown" class="forms_v2">
<%      using (Html.BeginFieldSet())
        { %>
                    <%= Html.CommunityField(Model, m => m.Criteria.CommunityId, Model.Communities.Values.OrderBy(c => c.Name))
                        .WithText(c => c == null ? "Select a community" : c.Name)
                        .WithLabel("").WithCssPrefix("select") %>
<%      } %>
                </div>
			</div>
		</div>
<%  } %>		
		<div class="section reset-all-filters">
		    <a href="javascript:void(0);" class="clear-all js_reset-all-filters">Reset all filters</a>
		</div>
    </div>
</div>
<% } %>

<%-- 'Update' is invoked by changing any criteria --%>
<script type="text/javascript">
    
    $(".industry_section .js_ellipsis").industriesEllipsis();
    function updateIndustries() {
        (function($) {
            $(".industry_section .js_ellipsis").industriesEllipsis();
        })(jQuery);
    }
    
    function allChecked(container) {
        return (function($) {
            return $("input:checked", container).length == $("input[type='checkbox']", container).length;
        })(jQuery);
    };

    function noneChecked(container) {
        return (function($) {
            return $("input:checked", container).length == 0;
        })(jQuery);
    }

    function forceNoneToAllChecked(container) {
        return (function($) {
            if (noneChecked(container)) {
                $("input[type='checkbox']", container).each(function() {
                    $(this).attr("checked", "checked");
                    this.refresh();
                });
            }
        })(jQuery);
    }
    
    (function($) {

        // document onready
        //

        $(".pushchecks input").click(function() {
            if($("." + $(this).attr("id") + "_pushcheck").hasClass("disabled_pushcheck")){
                return false;
            }
            
            if (noneChecked($("#candidateStatusCheckboxes")) || noneChecked($("#jobTypeCheckboxes"))) {
                return false;
            }

            var sectionClassName = (($(this).closest(".section").attr("class")).split(' '))[0];
            activateSection(sectionClassName);
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        $("#left-sidebar .go_button").click(function() {
            $(this).closest("fieldset").find("input:text").each(function() {
                if (($(this).hasClass("text-label")) || ($(this).val() == "")) {
                    return false;
                }
                else {
                    var sectionClassName = (($(this).closest(".section").attr("class")).split(' '))[0];
                    activateSection(sectionClassName);
                    $('html, body').animate({ scrollTop: 0 }, 'slow');
                    updateResults(false);
                    return false;
                }
            });
        });

        function generatePushCheckParameters(cssClassPrefix, elementId) {
            return {
                cssClass: cssClassPrefix + "_pushcheck pushcheck " + elementId + "_pushcheck",
                hoverClass: cssClassPrefix + "_pushcheck-hover pushcheck-hover",
                downClass: cssClassPrefix + "_pushcheck-down pushcheck-down",
                hoverDownClass: cssClassPrefix + "_pushcheck-down pushcheck-down",
                checkedClass: cssClassPrefix + "_pushcheck-checked pushcheck-checked",
                checkedHoverClass: cssClassPrefix + "_pushcheck-checked-hover pushcheck-checked-hover",
                checkedDownClass: cssClassPrefix + "_pushcheck-checked-down pushcheck-checked-down",
                checkedHoverDownClass: cssClassPrefix + "_pushcheck-checked-down pushcheck-checked-down",
                mutuallyExclusiveStateClasses: false,
                labelTextInside: false
            };
        }

        $("#AvailableNow").customCheckbox(generatePushCheckParameters("immediately-available","AvailableNow"));
        $("#ActivelyLooking").customCheckbox(generatePushCheckParameters("actively-looking","ActivelyLooking"));
        $("#OpenToOffers").customCheckbox(generatePushCheckParameters("happy-to-talk","OpenToOffers"));
        $("#Unspecified").customCheckbox(generatePushCheckParameters("not-specified","Unspecified"));
        $("#FullTime").customCheckbox(generatePushCheckParameters("full-time","FullTime"));
        $("#PartTime").customCheckbox(generatePushCheckParameters("part-time","PartTime"));
        $("#Contract").customCheckbox(generatePushCheckParameters("contract","Contract"));
        $("#Temp").customCheckbox(generatePushCheckParameters("temp","Temp"));
        $("#JobShare").customCheckbox(generatePushCheckParameters("jobshare","JobShare"));

        forceNoneToAllChecked($("#candidateStatusCheckboxes"));

        /* Filter activation checkboxes */
        $(".section-icon-clear-chk").customCheckbox(generatePushCheckParameters("filter"));
        $(".section-icon-clear-chk").toggleActivationStatus();

        /* Initialization for overlays */
        $(".js_dist-key-loc").makeSearchOverlay();
        $(".js_key-loc").makeSearchOverlay();

        /* Initialization for collapsible sections */
        $(".js_collapsible").makeSectionCollapsible(true);

        /* Initialization for clearing all filters */
        $(".js_reset-all-filters").click(function() {
            resetAllFilterValues();
        });
        
        /* Desired salary section */
        
        initializeSalary(
            <%= Model.Criteria.Salary == null || Model.Criteria.Salary.LowerBound == null ? "null" : Model.Criteria.Salary.LowerBound.Value.ToString() %>,
            <%= Model.Criteria.Salary == null || Model.Criteria.Salary.UpperBound == null ? "null" : Model.Criteria.Salary.UpperBound.Value.ToString() %>,
            <%= Model.MinSalary.ToString() %>,
            <%= Model.MaxSalary.ToString() %>,
            <%= Model.StepSalary.ToString() %>);
                   
        /* Updating results on change of Exclude results without a salary checkbox */
        $(".js_exclude-no-salary").change(function(){
            activateSection("desired-salary_section");
            updateResults(false);
        });
        
        /* Initialization for Distance Slider view */
        
        initializeDistance(
            <%= Model.Criteria.Distance == null ? "null" : Model.Criteria.Distance.Value.ToString() %>,
            [<%= string.Join(",", (from d in Model.Distances select d.ToString()).ToArray()) %>],
            <%= Model.DefaultDistance %>);
			
		<% if (Model.Criteria.Location != null && !Model.Criteria.Location.IsCountry) { %>
			activateSection("distance_section");
		<% } %>
        
        if (($("#Location").hasClass("text-label")) || ($("#Location").val() == '')) {
            $(".loc_label").text('');
            $(".spec_loc").hide();
            $(".no_loc").show();
        } else {
            $(".no_loc").hide();
            $(".spec_loc").show();
            $(".loc_label").text($("#Location").val());
        }
        
        initializeRecency(
            <%= Model.Criteria.Recency != null ? Model.Criteria.Recency.Value.Days : Model.DefaultRecency %>,
            [<%= string.Join(",", (from r in Model.Recencies select "{days: " + r.Days + ", label: '" + r.Description + "'}").ToArray()) %>],
            <%= Model.DefaultRecency %>);

        /* Updating results on Indigenous Status filter */
        $(".indigenousStatusChecks input").click(function() {
            activateSection("indigenous-status_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        $(".visaStatusChecks input").click(function() {
            activateSection("visa-status_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
        
        /* Updating results on Communities filter */
        $("#CommunityId").change(function() {
            if($(this).val() == ""){
                $(this).addClass("select_dropdown");
            } else {
                $(this).removeClass("select_dropdown");
            }
            activateSection("communities_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        /* Initialization for toggle checkboxes*/
        /*$(".js_toggle_checkboxes").toggleCheckboxes(false);
        $(".js_collapsible_toggle_checkboxes").makeCollapsibleDisplay(5, 10);*/

        /* Quick hack for ensuring industries are kept up-to-date */
        updateIndustries();
        $("#left-sidebar .industry_section .section-collapsible-title").click(function() {
            updateIndustries();
        });

        /* Updating results on Industries filter */
        $(".industry_section").find(".parent-toggle-checkbox-holder input[type='checkbox'], .children-toggle-checkbox-holder input[type='checkbox']").click(function() {
            activateSection("industry_section");
            //updateResults(false); //call to updateResults is made inside linkme.toggle-checkbox.js to avoid time mismatch of checking/unchecking and AJAX calls. It needs to be sequential.
        });

        /* Reflecting changes on change of Distance filter checkboxes */
        $("#IncludeRelocatingFilter").click(function() {
            $(this).is(":checked") ? $("#IncludeRelocating").attr("checked", "checked") : $("#IncludeRelocating").removeAttr("checked");
            activateSection("distance_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });
        $("#IncludeInternationalFilter").click(function() {
            $(this).is(":checked") ? $("#IncludeInternational").attr("checked", "checked") : $("#IncludeInternational").removeAttr("checked");
            activateSection("distance_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        /* Updating results on Filet by activity filter */
        $(".filter-by-activity_table input").change(function() {
            activateSection("filter-by-activity_section");
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            updateResults(false);
        });

        /* Watermark effect on filter textboxes */
        $(".js_contains_watermarked").find("input[type=text]").addClass("js_watermarked");

        /* Opening up active filters on load (in case of saved searches / bookmarked urls) */
        $(".js_open-if-active").openSectionOnLoad(<%= Model.MinSalary.ToString() %>, <%= Model.MaxSalary.ToString() %>);

        /* Initializing Save Search Overlays */
        
<%  if (CurrentEmployer != null)
    { %>
        $(".js_save-search").click(function() {
            showSaveSearchOverlay("<%= CurrentEmployer.EmailAddress.Address %>", <%= string.IsNullOrEmpty(Model.SavedSearch) ? "null" : "\"" + Model.SavedSearch.Replace("\"", "\\\"") + "\"" %>, "<%= !string.IsNullOrEmpty(Model.SavedSearch) ? Model.SavedSearch.Replace("\"", "\\\"") : Model.Criteria.GetSavedSearchDisplayText() %>");
        });
        $(".js_email-updates").click(function() {
            showSaveAlertOverlay("<%= CurrentEmployer.EmailAddress.Address %>", <%= string.IsNullOrEmpty(Model.SavedSearch) ? "null" : "\"" + Model.SavedSearch.Replace("\"", "\\\"") + "\"" %>, "<%= !string.IsNullOrEmpty(Model.SavedSearch) ? Model.SavedSearch.Replace("\"", "\\\"") : Model.Criteria.GetSavedSearchDisplayText() %>");
        });
<%  }
    else
    { %>
        $(".js_save-search").click(function() {
            showLoginOverlay("savesearch");
        });
        $(".js_email-updates").click(function() {
            showLoginOverlay("savealert");
        });
<%  } %>
    })(jQuery);
</script>

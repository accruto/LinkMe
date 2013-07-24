<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SuggestedCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Context" %>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Employers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Query.Members"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.JobAds) %>
        <%= Html.StyleSheet(StyleSheets.FlagLists) %>
        <%= Html.StyleSheet(StyleSheets.BlockLists) %>
        <%= Html.StyleSheet(StyleSheets.Error) %>
        <%= Html.StyleSheet(StyleSheets.Overlay) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
        <%= Html.StyleSheet(StyleSheets.JQuerySlider) %>
        <%= Html.StyleSheet(StyleSheets.JQueryTabs) %>
        <%= Html.StyleSheet(StyleSheets.JQueryDragDrop) %>
        <%= Html.StyleSheet(StyleSheets.Pagination) %>
        <%= Html.StyleSheet(StyleSheets.Notes) %>
        <%= Html.StyleSheet(StyleSheets.JQueryFileUploadUi) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">

    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftAjax) %>
        <%= Html.JavaScript(JavaScripts.MicrosoftMvcAjax) %>
        <%= Html.JavaScript(JavaScripts.CustomCheckbox) %>
        <%= Html.JavaScript(JavaScripts.AlignWith) %>
        <%= Html.JavaScript(JavaScripts.DesktopMenu) %>
        <%= Html.JavaScript(JavaScripts.TextOverflow) %>
        <%= Html.JavaScript(JavaScripts.Download) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.JavaScript(JavaScripts.SectionCollapsible) %>
        <%= Html.JavaScript(JavaScripts.Slider) %>
        <%= Html.JavaScript(JavaScripts.EApi) %>
        <%= Html.JavaScript(JavaScripts.EmployersApi)%>
        <%= Html.JavaScript(JavaScripts.Credits) %>
        <%= Html.JavaScript(JavaScripts.Overlay) %>
        <%= Html.JavaScript(JavaScripts.Actions) %>
        <%= Html.JavaScript(JavaScripts.Folders) %>
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
        <%= Html.JavaScript(JavaScripts.BlockLists) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.JavaScript(JavaScripts.JsonManipulations) %>
        <%= Html.JavaScript(JavaScripts.Tabs) %>
        <%= Html.JavaScript(JavaScripts.ToggleCheckbox) %>
        <%= Html.JavaScript(JavaScripts.Tooltips) %>
        <%= Html.JavaScript(JavaScripts.CenterAlign) %>
        <%= Html.JavaScript(JavaScripts.HoverIntent) %>
        <%= Html.JavaScript(JavaScripts.Notes) %>
        <%= Html.JavaScript(JavaScripts.FileUpload) %>
        <%= Html.JavaScript(JavaScripts.FileUploadUi) %>
        <%= Html.JavaScript(JavaScripts.TinyMceSrc) %>
        <%= Html.JavaScript(JavaScripts.JqueryTinyMce) %>
        <%= Html.JavaScript(JavaScripts.JqueryTinyMceInit) %>
        <% if (CurrentEmployer != null) { %>
            <%= Html.JavaScript(JavaScripts.CandidateResult) %>
        <% } else { %>
            <%= Html.JavaScript(JavaScripts.CandidateResultLogin) %>
        <% }%>
    </mvc:RegisterJavaScripts>
    
    <script language="javascript" type="text/javascript">

        <% var employerContext = ViewContext.HttpContext.GetEmployerContext(); %>

        var candidateContext = {
            isAnonymous: <%= CurrentEmployer == null ? "true" : "false" %>,
            isSearch: true,
            selectedCandidateIds: [],
            folderId: null,
            flagListId: null,
            blockListId: null,
            hideCreditReminder: <%= employerContext.IsCreditReminderHidden ? "true" : "false" %>,
            hideBulkCreditReminder: <%= employerContext.IsBulkCreditReminderHidden ? "true" : "false" %>
        };
        
        currentRequest = null;
        
        function updateResults(isCreditAction) {
            (function($) {
                // the following has not been tested yet
                if (currentRequest){
                    currentRequest.abort();
                }

                showResultsOverlay(function () { updateResults(false); });

                var anyKeywordsArray = new Array();
                if ($("#AnyKeywords1").val() != "") {
                    anyKeywordsArray.push($("#AnyKeywords1").val());
                }
                if ($("#AnyKeywords2").val() != "") {
                    anyKeywordsArray.push($("#AnyKeywords2").val());
                }
                if ($("#AnyKeywords3").val() != "") {
                    anyKeywordsArray.push($("#AnyKeywords3").val());
                }

                var sortOrderDirectionVar = $(".ascending").hasClass("selected") ? "SortOrderIsAscending" : "SortOrderIsDescending";

                var jhsCheckboxContainer = $("#candidateStatusCheckboxes");

                var requestData = {
                    <%= PresentationKeys.DetailLevel %>: $("input[name='DetailLevel']").val(),

                    <%= MemberSearchCriteriaKeys.JobAdId %>: "<%= Model.JobAd.Id %>",
                    <%= JobAdSearchType.SuggestedCandidates %>: true,
                    
                    <%= MemberSearchCriteriaKeys.Keywords %>: ($("#Keywords").val() == $("#Keywords").attr('data-watermark')) ? "" : $("#Keywords").val(),
                    <%= MemberSearchCriteriaKeys.AllKeywords %>: $('#AllKeywords').val(),
                    <%= MemberSearchCriteriaKeys.ExactPhrase %>: $('#ExactPhrase').val(),
                    <%= MemberSearchCriteriaKeys.AnyKeywords %>: anyKeywordsArray,
                    <%= MemberSearchCriteriaKeys.JobTitle %>: $('#JobTitle').val(),
                    <%= MemberSearchCriteriaKeys.JobTitlesToSearch %>: $("input:radio[name='JobTitlesToSearch']:checked").val(),
                    <%= MemberSearchCriteriaKeys.DesiredJobTitle %>: $('#DesiredJobTitle').val(),
                    <%= MemberSearchCriteriaKeys.WithoutKeywords %>: $('#WithoutKeywords').val(),

                    <%= MemberSearchCriteriaKeys.Location %>: ($("#Location").val() == $("#Location").attr('data-watermark')) ? "" : $("#Location").val(),
                    <%= MemberSearchCriteriaKeys.CountryId %>: $("#CountryId").val(),
                    
                    <%= MemberSearchCriteriaKeys.SortOrder %>: $("#SortOrder").val(),
                    <%= MemberSearchCriteriaKeys.SortOrderDirection %>: sortOrderDirectionVar,
                    <%= MemberSearchCriteriaKeys.IncludeSynonyms %>: $("#IncludeSynonyms").val(),
                    
                    <%= PaginationKeys.Items %>: ($("#Items").length > 0) ? $("#Items").val() : 25,
                    <%= PaginationKeys.Page %>: $("#Page").val()
                };
                
                if (isSectionActivated("candidate-status_section")) {
                    requestData.<%= CandidateStatus.AvailableNow %> = allChecked(jhsCheckboxContainer) ? false : ($("#AvailableNow").is(":checked") ? [true, false] : false);
                    requestData.<%= CandidateStatus.ActivelyLooking %> = allChecked(jhsCheckboxContainer) ? false : ($("#ActivelyLooking").is(":checked") ? [true, false] : false);
                    requestData.<%= CandidateStatus.OpenToOffers %> = allChecked(jhsCheckboxContainer) ? false : ($("#OpenToOffers").is(":checked") ? [true, false] : false);
                    requestData.<%= CandidateStatus.Unspecified %> = allChecked(jhsCheckboxContainer) ? false : ($("#Unspecified").is(":checked") ? [true, false] : false);
                }

                if (isSectionActivated("job-type_section")) {
                    requestData.<%= JobTypes.FullTime %> = $("#FullTime").is(":checked") ? [true, false] : false;
                    requestData.<%= JobTypes.PartTime %> = $("#PartTime").is(":checked") ? [true, false] : false;
                    requestData.<%= JobTypes.Contract %> = $("#Contract").is(":checked") ? [true, false] : false;
                    requestData.<%= JobTypes.Temp %> = $("#Temp").is(":checked") ? [true, false] : false;
                    requestData.<%= JobTypes.JobShare %> = $("#JobShare").is(":checked") ? [true, false] : false;
                }

                if (isSectionActivated("desired-salary_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.SalaryLowerBound %> = $("#SalaryLowerBound").val() == <%= Model.MinSalary %> ? "" : $("#SalaryLowerBound").val();
                    requestData.<%= MemberSearchCriteriaKeys.SalaryUpperBound %> = $("#SalaryUpperBound").val() == <%= Model.MaxSalary %> ? "" : $("#SalaryUpperBound").val();
                    requestData.<%= MemberSearchCriteriaKeys.ExcludeNoSalary %> = $("#ExcludeNoSalary").is(":checked") ? true : false;
                }

                if (isSectionActivated("resume-recency_section")) {
                    var recency = $("#Recency").val();
                    if (recency == "")
                        recency = <%= Model.DefaultRecency %>;
                    requestData.<%= MemberSearchCriteriaKeys.Recency %> = recency;
                }
                
                if (isSectionActivated("distance_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.Distance %> = $("#Distance").val();
                    requestData.<%= MemberSearchCriteriaKeys.IncludeRelocating %> = $("#IncludeRelocating").is(":checked") ? [true, false] : false;
                    requestData.<%= MemberSearchCriteriaKeys.IncludeInternational %> = $("#IncludeInternational").is(":checked") ? [true, false] : false;
                } else {
					requestData.<%= MemberSearchCriteriaKeys.Location %> = "";
                }

                if (isSectionActivated("employer_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.CompanyKeywords %> = $("#CompanyKeywords").val() == $("#CompanyKeywords").attr('data-watermark') ? "" : $("#CompanyKeywords").val();
                    requestData.<%= MemberSearchCriteriaKeys.CompaniesToSearch %> = $("input:radio[name='CompaniesToSearch']:checked").val();
                }

                if (isSectionActivated("education_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.EducationKeywords %> = $("#EducationKeywords").val() == $("#EducationKeywords").attr('data-watermark') ? "" : $("#EducationKeywords").val();
                }
                
                if (isSectionActivated("industry_section")) {
                    var industryIdsArray = new Array();
                    $("input[name=IndustryIds]").each(function() {
                        if ($(this).is(":checked")) {
                            industryIdsArray.push($(this).val());
                        }
                    });

                    requestData.<%= MemberSearchCriteriaKeys.IndustryIds %> = industryIdsArray;
                }
                
                if (isSectionActivated("filter-by-activity_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.InFolder %> = $("input:radio[name='InFolder']:checked").val();
                    requestData.<%= MemberSearchCriteriaKeys.IsFlagged %> = $("input:radio[name='IsFlagged']:checked").val();
                    requestData.<%= MemberSearchCriteriaKeys.HasNotes %> = $("input:radio[name='HasNotes']:checked").val();
                    requestData.<%= MemberSearchCriteriaKeys.HasViewed %> = $("input:radio[name='HasViewed']:checked").val();
                    requestData.<%= MemberSearchCriteriaKeys.IsUnlocked %> = $("input:radio[name='IsUnlocked']:checked").val();
                }
                
                if (isSectionActivated("indigenous-status_section")) {
                    requestData.<%= EthnicStatus.Aboriginal %> = $("#Aboriginal").is(":checked") ? [true, false] : false;
                    requestData.<%= EthnicStatus.TorresIslander %> = $("#TorresIslander").is(":checked") ? [true, false] : false;
                }
                
                if (isSectionActivated("visa-status_section")) {
                    requestData.<%= VisaStatusFlags.Citizen %> = $("#Citizen").is(":checked") ? [true, false] : false;
                    requestData.<%= VisaStatusFlags.UnrestrictedWorkVisa %> = $("#UnrestrictedWorkVisa").is(":checked") ? [true, false] : false;
                    requestData.<%= VisaStatusFlags.RestrictedWorkVisa %> = $("#RestrictedWorkVisa").is(":checked") ? [true, false] : false;
                    requestData.<%= VisaStatusFlags.NoWorkVisa %> = $("#NoWorkVisa").is(":checked") ? [true, false] : false;
                }
                
                if (isSectionActivated("communities_section")) {
                    requestData.<%= MemberSearchCriteriaKeys.CommunityId %> = $("#CommunityId").val();
                }

                currentRequest = $.get("<%= Html.RouteRefUrl(JobAdsRoutes.PartialSuggestedCandidates) %>",
                    requestData,
                    function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showResultsFailedOverlay(function() { updateResults(false); });
                            return;
                        }
                        hideResultsOverlay();
                        $("#search-results-container").html(data);
                        $("#search-header-text").html($("#results-header-text").html());
                        $(".spelling-container").hide();
                        
                        initializeActions(candidateContext);
                        currentRequest = null;

                        updateIndustries(); // From Filters.ascx
                        $(".js_synonyms-filter").synonymsFilter();
                        
                        /* Displaying / Updating candidate counts in filters */
                        if($("#isNewSearch").val() == "true"){
                            initializeFilterCounts();
                            $("#isNewSearch").val("false");
                        }
                        updateFilterCounts();
                    });
                    
                if (isCreditAction) {
                    $.get("<%= Html.RouteRefUrl(CandidatesRoutes.Credits) %>",
                        null,
                        function(data, textStatus, xmlHttpRequest) {
                            $("#credit-summary-container").html(data);
                        });
                }
                
            })(jQuery);
        }
    </script>
    <% if (Model.IsFirstSearch){ %>
        <script language="javascript" type="text/javascript">
            (function($) {
                $(document).ready(function() {
                    $(".js_help-text").show();
                    $(".js_help-text").find(".close-icon").click(function() {
                        $(".js_help-text").hide();
                    });
                    $(".js_help-text").delay("7000").fadeOut("3000");
                });
            })(jQuery);
        </script>
    <% } %>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li><a href="<%= NavigationManager.GetUrlForPage<EmployerJobAds>("mode", Model.JobAd.Status.ToString()) %>"><%= Model.JobAd.Status.ToString()%> job ads</a></li>
        <li class="current-breadcrumb">Suggested candidates</li>
    </ul>
	<script language="javascript" type="text/javascript">
		temporaryBlockListName = "Suggested candidates";
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">

    <div>
	    <div class="overlay-container forms_v2">
			<div class="key_loc-overlay shadow" style="display:none;">
				<span class="overlay-arrow"> &nbsp; </span>
				<div class="overlay">
					<div class="overlay-title"> Change keywords or location <span class="close-icon"></span></div>
					<div class="overlay-content">
						 <% Html.RenderPartial("KeywordLocationSearch", new KeywordLocationSearchModel { Criteria = Model.Criteria, CanSearchByName = Model.CanSearchByName, Distances = Model.Distances, DefaultDistance = Model.DefaultDistance, Countries = Model.Countries, DefaultCountry = Model.DefaultCountry }); %> 
						 <div class="retain-filters_section">
                            <div class="retain-filters_holder">
                                <small><input type="checkbox" name="retain-filters" id="retain-filters" checked="checked" /> Retain current filter settings</small>
                            </div>
                        </div>
					</div>
				</div>
			</div>
		</div>
		<% Html.RenderPartial("SearchTipsOverlay", null); %>

        <div>
	        <div class="section">
		        <div class="section-content">
			        <ul class="plain_action-list action-list">
		                <li><a class = "back-action" href="<%= NavigationManager.GetUrlForPage<EmployerJobAds>("mode", Model.JobAd.Status.ToString()) %>">Back to job overview</a></li>
			        </ul>
		        </div>
	        </div>
        </div>


        <% Html.RenderPartial("FoldersSection"); %>
        <div class="help-text_anchor">
            <div class="help-text_container js_help-text">
                <span class="arrow"> &nbsp; </span>
		        <div class="holder">
			        <div class="content">
				         <span class="text">Refine your results here</span>
				         <span class="close-icon"></span>
			        </div>
		        </div>            
            </div>
        </div>
		<% Html.RenderPartial("Filters", Model); %>
		<% Html.RenderPartial("BlockListsSection"); %>
	</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="results_aspx">
        <% Html.RenderPartial("CandidateResults", Model); %>
    </div>

    <script type="text/javascript">
        (function($) {
            $(document).ready(function() {
                $(".js_folders_collapsible").makeFoldersSectionCollapsible(!candidateContext.isAnonymous);
                $(".js_jobads_collapsible").makeJobAdsSectionCollapsible(!candidateContext.isAnonymous);
                $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(true);
                
                initializeFolders(candidateContext);
                initializeJobAds(candidateContext);
                initializeBlockLists();
                initializeActions(candidateContext);
                
                $("#search-header-text").html($("#results-header-text").html());
            
                $("#GoInternal, #GoHidden").click(function() { updateResults(false); return false; });
                
                $("#hlExpandedView").click( function() {
                    $("#DetailLevel").val("<% =Enum.Format(typeof(DetailLevel), DetailLevel.Expanded, "G") %>");
                    updateResults(false);
                } );
                $("#hlCompactView" ).click( function() {
                    $("#DetailLevel").val("<% =Enum.Format(typeof(DetailLevel), DetailLevel.Compact, "G") %>");
                    updateResults(false);
                } );
                
	            /* Initialization for tabs */
	            $(".js_tabs").makeTabs( {
	                selectedFlagClass:                  "js_selected-tab",
	                hyperlinkReplacementTag:            "span",
	                hyperlinkOrReplacementClassesToAdd: "tab-content-outer",
	                tabContentSelector:                 "span",
	                tabContentClassesToAdd:             "tab-content-inner"
	            } );
    	        
		        // Synonyms filter
                $(".js_synonyms-filter").synonymsFilter();    

    <%  if (CurrentEmployer != null)
        { %>            
                $(".js_results-count").find(".save-search").find("a").click(function() {
                    showSaveSearchResultsOverlay(candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds : candidateContext.selectedCandidateIds, candidateContext);
                });
    <%  }
        else
        { %>
                $(".js_results-count").find(".save-search").find("a").click(function() {
                    showLoginOverlay("addresults");
                });
    <%  } %>    
            
                /* Displaying / Updating candidate counts & status in filters */
                initializeFilterCounts();
                updateFilterCounts();
            });
        })(jQuery);
    </script>
</asp:Content>


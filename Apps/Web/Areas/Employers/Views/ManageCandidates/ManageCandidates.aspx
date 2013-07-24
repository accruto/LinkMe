<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<ManageCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Context" %>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Employers"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Contenders"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarOnLeft) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
        <%= Html.StyleSheet(StyleSheets.JobAds) %>
        <%= Html.StyleSheet(StyleSheets.Folders) %>
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
        <%= Html.JavaScript(JavaScripts.EmployersApi) %>
        <%= Html.JavaScript(JavaScripts.Credits) %>
        <%= Html.JavaScript(JavaScripts.Overlay) %>
        <%= Html.JavaScript(JavaScripts.Actions) %>
        <%= Html.JavaScript(JavaScripts.Folders) %>
        <%= Html.JavaScript(JavaScripts.FlagLists) %>
        <%= Html.JavaScript(JavaScripts.BlockLists) %>
        <%= Html.JavaScript(JavaScripts.EmployersJobAds) %>
        <%= Html.JavaScript(JavaScripts.Search) %>
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
            isAnonymous: false,
            isSearch: false,
            jobAdId: '<%= Model.JobAd.Id %>',
            flagListId: null,
            blockListId: null,
            selectedCandidateIds: new Array(),
            hideCreditReminder: <%= employerContext.IsCreditReminderHidden ? "true" : "false" %>,
            hideBulkCreditReminder: <%= employerContext.IsBulkCreditReminderHidden ? "true" : "false" %>,
            applicantStatus: '<%= Model.ApplicantStatus %>',
        };
        
        currentRequest = null;

        function updateResults(isCreditAction) {
            (function($) {
                if (currentRequest)
                    currentRequest.abort();

                showResultsOverlay(function () { updateResults(false); });

                var requestData = {
                    <%= PresentationKeys.DetailLevel %>: $("input[name='DetailLevel']").val(),
                    <%= PresentationKeys.Status %>: $("input[name='JobAdApplicantStatus']").val(),

                    <%= MemberSearchCriteriaKeys.SortOrder %>: $("#SortOrder").val(),
                    <%= MemberSearchCriteriaKeys.SortOrderDirection %>: $("input:radio[name=SortOrderDirection]:checked").val(),

                    <%= PaginationKeys.Items %>: $("#CategoryTabPagger").attr($(".category-indicator").attr("currentCategory") + "-Items"),
                    <%= PaginationKeys.Page %>: $("#CategoryTabPagger").attr($(".category-indicator").attr("currentCategory") + "-Page")
                };

                currentRequest = $.get("<%= Html.RouteRefUrl(JobAdsRoutes.PartialManageJobAdCandidates) %>",
                    requestData,
                    function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showResultsFailedOverlay(function() { updateResults(false); });
                            return;
                        }
                        $("#search-results-container").html(data);
                        hideResultsOverlay();
                        $("#search-header-text").html($("#results-header-text").html());
                        
                        initializeActions(candidateContext);
                        currentRequest = null;
						if ($(".results-count_save-search_holder:hidden").length > 0) $("#results-header").addClass("no-result-in-current-category");
						else $("#results-header").removeClass("no-result-in-current-category");
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

</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li><a href="<%= NavigationManager.GetUrlForPage<EmployerJobAds>("mode", Model.JobAd.Status.ToString()) %>"><%= Model.JobAd.Status.ToString()%> job ads</a></li>
        <li class="current-breadcrumb">Manage candidates</li>
    </ul>
	<script language="javascript" type="text/javascript">
		bHideTemporaryBlockList = true;
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div>
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
        <% Html.RenderPartial("BlockListsSection"); %>

	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div>
        <input type="hidden" id="CategoryTabPagger" />
		<% Html.RenderPartial("CandidateResults", Model); %>
    </div>

    <input type="hidden" id="JobAdApplicantStatus" name="JobAdApplicantStatus" value="<% = Model.ApplicantStatus.ToString("G") %>" /> 

    <script type="text/javascript">
        (function($) {
        
            $(".js_folders_collapsible").makeFoldersSectionCollapsible(true);
            $(".js_jobads_collapsible").makeJobAdsSectionCollapsible(!candidateContext.isAnonymous);
            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(true);

            initializeFolders(candidateContext);
            initializeJobAds(candidateContext);
            initializeBlockLists();
            initializeActions(candidateContext);
            
            $(".js_results-count").find(".save-search").find("a").click(function() {
                showSaveSearchResultsOverlay(candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds : candidateContext.selectedCandidateIds, candidateContext);
            });

            $("#search-header-text").html($("#results-header-text").html());
            
            $("#hlExpandedView").click( function() {
                $("#DetailLevel").val("<%= DetailLevel.Expanded.ToString("G") %>");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            });
            $("#hlCompactView" ).click( function() {
                $("#DetailLevel").val("<%= DetailLevel.Compact.ToString("G") %>");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            });

	        // Tabs
	        
	        $(".js_tabs").makeTabs({
	            selectedFlagClass:                  "js_selected-tab",
	            hyperlinkReplacementTag:            "span",
	            hyperlinkOrReplacementClassesToAdd: "tab-content-outer",
	            tabContentSelector:                 "span",
	            tabContentClassesToAdd:             "tab-content-inner"
	        });
	        
        })(jQuery);
    </script>
    
    <script type="text/javascript">
		$(document).ready(function() {
		    $(".categories-tab").click(function () {
		        $("#JobAdApplicantStatus").val($(this).attr("categoryName"));
                $('html, body').animate({ scrollTop: 0 }, 'slow');
				$(".categories-tab").removeClass("active");
				$(this).addClass("active");
				$(".category-indicator").attr("currentCategory", $(this).attr("categoryName"));
				$(".categories-description").find("span").removeClass("active");
				$(".categories-description").find("span[categoryName='" + $(this).attr("categoryName") + "']").addClass("active");
                updateResults(false);
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					switch($(".category-indicator").attr("currentCategory")) {
						case "New":
							$(".category-indicator").css("margin-left", "67px");
							break;
						case "Shortlisted":
							$(".category-indicator").css("margin-left", "227px");
							break;
						case "Rejected":
							$(".category-indicator").css("margin-left", "379px");
							break;
					}
				}
			});
			<% if (Model.ApplicantStatus == ApplicantStatus.NotSubmitted || Model.ApplicantStatus == ApplicantStatus.Removed) {%>
			    $(".new-category-tab").trigger("click");
			<%}%>
		});
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FlagListListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Profiles"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Context" %>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Query.Members"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarOnLeft) %>
        <%= Html.StyleSheet(StyleSheets.SearchResults) %>
        <%= Html.StyleSheet(StyleSheets.JQueryWidgets) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.EmployerLoggedInFrontPage) %>
        <%= Html.StyleSheet(StyleSheets.Search) %>
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
            folderId: null,
            flagListId: '<%= Model.FlagList.Id %>',
            blockListId: null,
            selectedCandidateIds: new Array(),
            hideCreditReminder: <%= employerContext.IsCreditReminderHidden ? "true" : "false" %>,
            hideBulkCreditReminder: <%= employerContext.IsBulkCreditReminderHidden ? "true" : "false" %>
        };
        
        currentRequest = null;

        function updateResults(isCreditAction) {
            (function($) {
                if (currentRequest)
                    currentRequest.abort();

                showResultsOverlay(function () { updateResults(false); });

                var requestData = {
                    <%= PresentationKeys.DetailLevel %>: $("input[name='DetailLevel']").val(),

                    <%= MemberSearchCriteriaKeys.SortOrder %>: $("#SortOrder").val(),
                    <%= MemberSearchCriteriaKeys.SortOrderDirection %>: $("input:radio[name=SortOrderDirection]:checked").val(),

                    <%= PaginationKeys.Items %>: ($("#Items").length > 0) ? $("#Items").val() : 25,
                    <%= PaginationKeys.Page %>: $("#Page").val()
                };

                currentRequest = $.get("<%= Html.RouteRefUrl(CandidatesRoutes.PartialFlagList) %>",
                    requestData,
                    function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showResultsFailedOverlay(function() { updateResults(false); });
                            return;
                        }
                        $("#search-results-container").html(data);
                        hideResultsOverlay();
                        $("#flaglist-header-text").html($("#results-header-text").html());
                        
                        initializeActions(candidateContext);
                        currentRequest = null;
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
        <li><%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders) %></li>
        <li class="current-breadcrumb"><%= Model.FlagList.GetNameDisplayText() %></li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div>
	    <div class="section">
		    <div class="section-content">
			    <ul class="plain_action-list action-list">
				    <li><%= Html.RouteRefLink("New search", SearchRoutes.Search, null, new {@class = "new-search-action"}) %></li>
<% if (Model.CurrentSearch != null)
   { %>
                    <li><%= Html.RouteRefLink("Back to search results", SearchRoutes.Results, null, new { @class = "current-search-action back-action" })%></li>
<% } %>				    
			    </ul>
		    </div>
	    </div>

        <% Html.RenderPartial("FoldersSection"); %>
        <% Html.RenderPartial("BlockListsSection"); %>

	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <% Html.RenderPartial("CandidateResults", Model); %>

    <script type="text/javascript">
        (function($) {
        
            $(".js_folders_collapsible").makeFoldersSectionCollapsible(true);
            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(true);

            initializeFolders(candidateContext);
            initializeBlockLists();
            initializeActions(candidateContext);
            
            $(".js_results-count").find(".save-search").find("a").click(function() {
                showSaveSearchResultsOverlay(candidateContext.selectedCandidateIds.length == 0 ? candidateContext.candidateIds : candidateContext.selectedCandidateIds, candidateContext);
            });

            $("#flaglist-header-text").html($("#results-header-text").html());
            
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
</asp:Content>


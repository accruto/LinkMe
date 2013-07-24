<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<BlockListListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Converters"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Context" %>

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
            flagListId: null,
            blockListId: '<%= Model.BlockList.Id %>',
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

                currentRequest = $.get("<%= Html.RouteRefUrl(Model.BlockList.BlockListType == BlockListType.Temporary ? CandidatesRoutes.TemporaryPartialBlockList : CandidatesRoutes.PermanentPartialBlockList) %>",
                    requestData,
                    function(data, textStatus, xmlHttpRequest) {
                        if (data == "") {
                            showResultsFailedOverlay(function() { updateResults(false); });
                            return;
                        }
                        $("#search-results-container").html(data);
                        hideResultsOverlay();
                        $("#blocklist-header-text").html($("#results-header-text").html());
                        
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
        <% if (Model.CurrentSearch is SuggestedCandidatesNavigation)
 {%>
        <li>Suggested candidates</li>
        <li><%= Html.NavigationLink("Suggested candidates results", Model.CurrentSearch, null) %></li>
        <li class="current-breadcrumb"><%= Model.BlockList.GetNameDisplayText() %> block list</li>
<%
 }
           else if (Model.CurrentSearch == null)
   { %>        
        <li><%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders) %></li>
        <li class="current-breadcrumb"><%= Model.BlockList.GetNameDisplayText() %> block list</li>
<% }
   else
   { %>
        <li><%= Html.RouteRefLink("New candidate search", SearchRoutes.Search) %></li>
        <li><%= Html.RouteRefLink("Search results", SearchRoutes.Results) %></li>
        <li class="current-breadcrumb"><%= Model.BlockList.GetNameDisplayText() %> block list</li>
<% } %>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div>
	    <div class="section">
		    <div class="section-content">
			    <ul class="plain_action-list action-list">
<% if (Model.CurrentSearch is SuggestedCandidatesNavigation)
   {%>
                    <li><%= Html.BackToLink(Model.CurrentSearch, new { @class = "current-search-action back-action" }) %></li>
<%
    }
   else
   { %>
				    <li><%= Html.RouteRefLink("New search", SearchRoutes.Search, null, new { @class = "new-search-action" })%></li>
<% if (Model.CurrentSearch != null)
   { %>
                    <li><%= Html.RouteRefLink("Back to search results", SearchRoutes.Results, null, new { @class = "current-search-action back-action" })%></li>
<% }
   } %>				    
			    </ul>
		    </div>
	    </div>

        <% Html.RenderPartial("FoldersSection"); %>
        <% Html.RenderPartial("BlockListsSection"); %>

	</div>
</asp:Content>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <% if (Model.BlockList.BlockListType == BlockListType.Temporary) { %>
        <div class="blocklist-current_aspx blocklist_aspx">
    <% } else { %>
        <div class="blocklist-all_aspx blocklist_aspx">
    <% } %>

        <% Html.RenderPartial("CandidateResults", Model); %>
    </div>

    <script type="text/javascript">
        (function($) {

            $(".js_folders_collapsible").makeFoldersSectionCollapsible(true);
            $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(true);
            
            initializeFolders(candidateContext);
            initializeBlockLists();
            initializeActions(candidateContext);
            $(".js_results-count").initializeBlockListResultsCount(candidateContext);

            $("#blocklist-header-text").html($("#results-header-text").html());

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


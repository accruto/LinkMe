<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

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
    </mvc:RegisterJavaScripts>

    <script language="javascript" type="text/javascript">
    
        var candidateContext = {
            isAnonymous: <%= CurrentEmployer == null ? "true" : "false" %>
        };
    
        (function($) {
            Array.prototype.max = function() {
                return Math.max.apply(Math, this);
            };

            formatSavedSearch = function() {
                var heightArray = [];
                var listHeight = "";
                $("#saved-searches-data").find(".list-holder").each(function() {
                    listHeight = $(this).css("height");
					//only for IE7
					if (listHeight == "auto") {
						listHeight = $(this)[0].clientHeight - 15;
						heightArray.push(listHeight + 8);
					}
					else heightArray.push(parseInt(listHeight.substring(0, (listHeight.length - 2))) + 8);
                });
                heightArray.push(145); /* Minimum height */
                listHeight = heightArray.max();
                $("#saved-searches-data").find(".list-holder").css("height", listHeight);
				if ($.browser.msie && $.browser.version.indexOf("7") == 0) {
					$(".manage-saved-searches-container").addClass("IE7");
					$(".list-holder").addClass("IE7");
				}
            }

            displaySavedSearches = function(items) {
                $.get(
                    '<%=Html.RouteRefUrl(SearchRoutes.PartialSearches)%>',
                    items == null ? { moreItems: 10} : { Items: items, moreItems: 10 },
                    function(data) {
                        $("#saved-searches-data").html(data);
                        $("#saved-searches-data .js_ellipsis").customEllipsis(45);
                        formatSavedSearch();
                        $(".js_view-all").click(function() {
                            if ($(this).find(".icon").hasClass("down-icon")) {
                                displaySavedSearches(null);
                            }
                            else {
                                displaySavedSearches(10);
                            }
                        });
                    }
                );
            }
        })(jQuery);    
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">New candidate search</li>
    </ul>
</asp:Content>
                
<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">

    <% Html.RenderPartial("FoldersSection"); %>
    <% Html.RenderPartial("BlockListsSection"); %>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Find candidates</h1>
    </div>
    
    <div class="forms_v2">
    
<% using (Html.RenderForm())
   { %>
        <div class="section">
            <div class="section-content">
                <% Html.RenderPartial("KeywordLocationSearch", new KeywordLocationSearchModel { Criteria = Model.Criteria, CanSearchByName = Model.CanSearchByName, Distances = Model.Distances, DefaultDistance = Model.DefaultDistance, Countries = Model.Countries, DefaultCountry = Model.DefaultCountry }); %>
            </div>
            <% Html.RenderPartial("SearchTipsOverlay", null); %>
        </div>                  
        <% if (Model.CreateEmailAlert) { %>
            <input type="hidden" name="createEmailAlert" value="True" />
            <div class="overlay createemailalertprompt">
                <div class="titlebar">
                    <div class="title">Create new email alert</div>
                    <div class="icon close black"></div>
                </div>
                <div class="desc">
                    <span>Start a new search here.</span>
                    <span>You'll create an email alert on the next page</span>
                </div>
                <div class="button ok"></div>
            </div>
        <% } %>
<% } %>

        <div class="saved-searches-container new_search section js_searches_collapsible">
            <div class="m-section-title"><div class="saved-searches"></div>
                <h1> Saved searches
                    <img src="<%= Images.Help %>"
                     class="help js_tooltip"
                     data-tooltip="You can save any of your searches for future use, giving you one click-access to the latest candidates." />
                </h1>
            </div>
            <div class="minilist_section-content section-content">
<%  if (CurrentEmployer == null)
    { %>
                <div id="saved-searches-data">
                    <span class="no-saved-searches">When you login as an employer or recruiter, you can access your saved searches quickly from here.</span>
                </div>
<%  }
    else
    { %>
                <div id="saved-searches-data">
                </div>
<%  } %>                    
            </div>            
        </div>
        
        <div class="get-most-container section new_search js_get-most_collapsible">
            <div class="m-section-title"><div class="get-most"></div>
                <h1>Get the most out of LinkMe</h1>
            </div>
            <div class="section-content">
                <% Html.RenderPartial("GetMostOutOfLinkMe"); %>
            </div>
        </div>
        
<%  Html.RenderPartial("Overlays"); %>

        <script language="javascript">
            (function($) {
            
<%  if (CurrentEmployer != null)
    { %>
                $(".js_searches_collapsible").makeMainSectionCollapsible(function() { displaySavedSearches(10); });
<%  }
    else
    { %>                
                $(".js_searches_collapsible").makeMainSectionCollapsible(null);
<%  } %>
                $(".js_get-most_collapsible").makeMainSectionCollapsible();

                $(".js_folders_collapsible").makeFoldersSectionCollapsible(false);
                $(".js_blocklists_collapsible").makeBlockListsSectionCollapsible(false);

                initializeFolders(candidateContext);
                initializeBlockLists();

                $(".js_tooltip").addTooltip();
            })(jQuery);
            $(document).ready(function () {
                //hide job ads section in folder section for a new search
                $(".jobads_ascx").hide();
                if ($(".overlay.createemailalertprompt").length > 0)
                    $(".overlay.createemailalertprompt").dialog({
                        modal: true,
                        width: 580,
                        height: 160,
                        closeOnEscape: false,
                        resizable: false,
                        position : ["center", 175],
                        dialogClass: "createemailalertprompt-dialog"
                    }).find(".button.close, .button.ok").click(function() {
                        $(".overlay.createemailalertprompt").dialog("close");
                    });
            });
        </script>
    </div>
    
</asp:Content>


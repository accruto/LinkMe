<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CandidateListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Query.Search.Members"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models"%>

<script language="javascript" type="text/javascript">

    function onMouseOverCommunity(element) {
        (function($) {
            $(element).closest(".community-partner_outer-container").find(".community-partner_container").show();
        })(jQuery);
    }

    function onMouseOutCommunity(element) {
        (function($) {
            $(element).closest(".community-partner_outer-container").find(".community-partner_container").hide();
        })(jQuery);
    }
</script>

<input type="hidden" id="DetailLevel" name="DetailLevel" value="<% = Model.Presentation.DetailLevel.ToString("G") %>" />

<div id="results-header">
    <% Html.RenderPartial("Header", Model); %>
</div>

<div class="results-count_save-search_holder js_results-count">
    <div class="results-count_save-search_header">
        <span class="results-count"></span>
        <span class="save-search">
			<div></div>
            <a href="javascript:void(0);">Add&nbsp;<span class="total-count"></span>&nbsp;result<span class="total-suffix"></span> to folder</a>
        </span>
        <span class="restore-blocklist">
            <a href="javascript:void(0);" class="restore-icon">Restore&nbsp;<span class="total-count"></span>&nbsp;from blocklist</a>
        </span>
        <div class="clear-current-flags">
            <div class="text"><div></div><a href="javascript:void(0);" class="js_clear-current-flags">Clear all candidate flags</a></div>
            <div class="sub-text">in this <% if (Model is ManageCandidatesListModel) {%>category<% } else { %>search<% } %></div>
        </div>
    </div>
</div>

<%  if (Model.Recovery != null && Model.Recovery.SpellingSuggestions != null && Model.Recovery.SpellingSuggestions.Count > 0)
    { %>
<div class="spelling-container">
    <span class="did-you-mean">Did you mean:</span>
<%      foreach (var suggestion in Model.Recovery.SpellingSuggestions)
        { %>
        <a href="<%= suggestion.Criteria.GetSearchUrl() %>"><%= suggestion.GetDisplayHtml() %></a>
<%      } %>    
</div>
<%  } %>

<% Html.RenderPartial("SortOrder", new MemberSearchSortModel {Criteria = Model.Criteria.SortCriteria, SortOrders = Model.SortOrders}); %>

<ul class="js_tabs tabs-nav">
	<li id="ExpandedTab" class="<%= Model.Presentation.DetailLevel == DetailLevel.Expanded ? "js_default-selected-tab" : "" %>">
		<a id="hlExpandedView" href="javascript:void(0);"><span data-tab-shorttext="Expanded">Expanded view</span></a>
	</li>
	<li id="CompactTab" class="<%= Model.Presentation.DetailLevel == DetailLevel.Compact ? "js_default-selected-tab" : "" %>">
		<a id="hlCompactView" href="javascript:void(0);"><span data-tab-shorttext="Compact">Compact view</span></a>
	</li>
</ul>

<% Html.RenderPartial("SubTabs", Model);
var listModel = "search-result";
if (Model is ManageCandidatesListModel) listModel = "manage-candidate";
else if (Model is SuggestedCandidatesListModel) listModel = "suggest-candidate";
%>

<div class="tabs-container <%= listModel %>"> <%--n.b. this div is for styling only --%>
    <div class="results_section section">
        <div class="candidate-restored"><span class="candidate-name"></span> has been restored to your search results.</div>
        <div id="search-results-container">
            <% Html.RenderPartial("CandidateList", Model); %>
        </div>
        <div id="results-overlay" class="ajax-overlay" style="display: none;">
            <div class="loading"></div>
            <div class="error" style="display: none;">
                <p>Your search couldn't be processed.</p>
                <ul class="action-list">
                    <li><a id="results-overlay-retry" href="javascript:void(0);">Try again</a></li>
                    <li><a id="results-overlay-no-retry" href="javascript:void(0);">Close this box</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>

<%  Html.RenderPartial("Overlays"); %>

<script type="text/javascript">
    (function($) {
        /* Initialization for Send Message Actions */
        $('#file_upload').fileUploadAction();
        initEmailRTE();
        $("#insert-pf").click(function() {
            var nameImgUrl = ($(this).closest(".personalization-holder").find(".personalization-field-list").val() == "first") ? candidateFirstNameImgUrl : candidateLastNameImgUrl;
            var nameClass = ($(this).closest(".personalization-holder").find(".personalization-field-list").val() == "first") ? "first-name" : "last-name";
            var nameImgTag = '<img src="' + nameImgUrl + '" class="' + nameClass + '" />';
            tinyMCE.execCommand('mceInsertContent', false, nameImgTag);
            return false;
        });

        // Clearing flags in current search
        
<%  if (CurrentEmployer != null)
    { %>        
        $(".js_clear-current-flags").click(function() {
            removeCurrentCandidatesFromFlagList();
        });
<%  }
    else
    { %>
        $(".js_clear-current-flags").click(function() {
            showLoginOverlay("flag");
        });
<%  } %>        
        $(document).ready(function() {
			if ($.browser.msie) {
				if ($.browser.version.indexOf("7") == 0) {
					$(".js_clear-current-flags").addClass("IE7");
					$(".tabs-container").addClass("IE7").addClass("search-result");
					$(".categories-tab-container").addClass("IE7").find("div").addClass("IE7").end().find("span").addClass("IE7");
					$(".tabs-nav").addClass("IE7");
				}
			}
		});
    })(jQuery);
</script>

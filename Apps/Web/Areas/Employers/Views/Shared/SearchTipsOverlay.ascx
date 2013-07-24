<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div class="overlay-container forms_v2">
    <div class="tips-overlay shadow" style="display:none;">
        <div class="overlay">
            <div class="overlay-title"><span class="overlay-title-text">Search Tips</span><span class="close-icon"></span></div>
            <div class="overlay-content">
                <div class="overlay-text">
                    <ul>
                        <li>Start broad: Use advanced filters on the search results page to refine your results further</li>
                        <li>Use "quotes" to force a search for an exact phrase</li>
                        <li>Add more keywords to narrow your search results</li>
                        <li>Use the special words OR and NOT to broaden your search or exclude certain keywords</li>
                        <li>Use saved searches and email alerts to ensure you hear about new candidates first</li>
                    </ul>
                    <span class="link-to-pdf">More <%= Html.RouteRefLink("detailed", SearchRoutes.Tips)%> search tips</span>
                </div>
            </div>
        </div>
    </div>
</div>

<script language="javascript">
    (function($) {
        $(".js_search-tips").makeSearchTipsOverlay();
    })(jQuery);
</script>
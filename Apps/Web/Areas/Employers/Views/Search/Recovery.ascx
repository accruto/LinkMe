<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Query.Search.Members"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Query.Search.Members"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<%  if (Model.IsNewSearch)
    {
        if (Model.Results.TotalCandidates == 0)
        { %>
<!-- Zero search results -->

<br />
<div class="no-results">
    No candidates match your current search requirements.
<%          if (Model.Recovery == null || Model.Recovery.MoreResultsSuggestions.IsNullOrEmpty())
            { %>
    <br /><br />
    Please try a <a href='<%=Html.RouteRefUrl(SearchRoutes.Search)%>'>new search</a>.
<%          }
            else
            { %>
    <br /><br />
    To see results:
    <br /><br />
    1. You can try one of the following search suggestions:
    <ul>
<%              foreach (var suggestion in Model.Recovery.MoreResultsSuggestions)
                { %>
                    <li><%= suggestion.GetDisplayHtml() %></li>
<%              } %>                
    </ul>
    <br />
    or 2. Try a <a href='<%=Html.RouteRefUrl(SearchRoutes.Search)%>'>new search</a>.
<%          } %>
</div>

<script type="text/javascript">
    (function($) {
        $(".js_modify-search").makeSearchOverlay();
    })(jQuery);
</script>
<%      }
        else
        {
            if (Model.Recovery != null && !Model.Recovery.MoreResultsSuggestions.IsNullOrEmpty())
            { %>
<!-- Low number of search results -->            

<br />
<div class="low-results">
    Do you want to see more candidates?
    <br /><br />
    1. You can try one of the following search suggestions:
    <ul>
<%              foreach (var suggestion in Model.Recovery.MoreResultsSuggestions)
                { %>
                    <li><%= suggestion.GetDisplayHtml() %></li>
<%              } %>                
    </ul>
    <br />
    or 2. <a href="javascript:void(0);" class="js_modify-search">Modify</a> your search.
    <br /><br />
</div>

<script type="text/javascript">
    (function($) {
        $(".js_modify-search").makeSearchOverlay();
    })(jQuery);
</script>
<%          }
        }
    }
    else
    {
        if (Model.Results.TotalCandidates == 0)
        { %>
<!-- Zero results in current search set -->

<br />
<div class="no-results">
    No candidates match your current search requirements.
    <br /><br />
    You can either broaden your search criteria by changing some of the filters you have set, or you can
    <a href="javascript:void(0);" class="js_modify-search">modify</a> your search.
</div>

<script type="text/javascript">
    (function($) {
        $(".js_modify-search").makeSearchOverlay();
    })(jQuery);
</script>
<%      }
    } %>

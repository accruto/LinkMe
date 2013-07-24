<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Shared.Pagination" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Views"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<%  if (Model.Results.TotalCandidates > 0)
    { %>
 
<script language="javascript" type="text/javascript">

    function onChangePage(page) {
        (function($) {
            $("#Page").val(page);
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            <% if (Model is ManageCandidatesListModel) { %>
			$("#CategoryTabPagger").attr($(".category-indicator").attr("currentCategory") + "-Items", ($("#Items").length > 0) ? $("#Items").val() : "25");
			$("#CategoryTabPagger").attr($(".category-indicator").attr("currentCategory") + "-Page", page);
            <% } %>
            updateResults(false);
        })(jQuery);
    }

</script>
 
<div class="pagination-results-container">
    <div class="pagination-results-holder"><%= Model.GetPaginationResultsText() %> <span class="start"><%=Start%></span> - <span class="end"><%=End%></span> of <span class="total"><%=Total%></span><%= Model.GetPaginationResultsSuffix() %></div>
    <%= Html.ItemsPerPageField(Model, "Items", m => m.Presentation.Pagination.Items ?? Model.Presentation.ItemsPerPage[0], Model.Presentation.ItemsPerPage).WithLabel("").WithCssPrefix("pagination-results")%>
</div>

<div class="pagination-container">
    <div class="pagination-holder">
<%  if (CurrentPage == 1)
    { %>
        <div class="first">First</div>
        <div class="previous">&lt;&nbsp;<span class="underline">Previous</span></div>
<%  }
    else
    { %>
        <a class="first active secondary" onclick="onChangePage(1);return false;" href="<%= GetUrl(1) %>">First</a>
        <a class="previous active" onclick="onChangePage(<%= CurrentPage - 1 %>);return false;" href="<%= GetUrl(CurrentPage - 1) %>">&lt;&nbsp;<span class="underline">Previous</span></a>
<%  } %>
        
        <div class="page-holder">
            <div class="page-numbers">
<%  if (FirstPage > 1)
    { %>
                <span class='pagination-ellipsis'>...</span>
<%  } %>
<%  for (var page = FirstPage; page <= LastPage; page++)
    {
        if (page == CurrentPage)
        { %>
                <div class="page-selected"><%= page %></div>
<%      }
        else
        { %>
                <a class="page" onclick="onChangePage(<%= page %>);return false;" href="<%= GetUrl(page) %>"><%=page%></a>
<%      }
    }
    if (LastPage < TotalPages)
    { %>
                <span class='pagination-ellipsis'>...</span>
<%  } %>
            </div>
        </div>
        
<%  if (CurrentPage == TotalPages)
    { %>
        <div class="next"><span class="underline">Next</span>&nbsp;&gt;</div>
        <div class="last">Last</div>
<%  }
    else
    { %>
        <a class="next active" onclick="onChangePage(<%= (CurrentPage + 1) %>);return false;" href="<%= GetUrl(CurrentPage + 1) %>"><span class="underline">Next</span>&nbsp;&gt;</a>
        <a class="last active secondary" onclick="onChangePage(<%= TotalPages %>);return false;" href="<%= GetUrl(TotalPages) %>">Last</a>
<%  } %>
    </div>
    <input type="hidden" name="Page" id="Page" value="<%= CurrentPage %>" />
</div>

<script language="javascript" type="text/javascript">
    (function($) {
    
        // Updating results on change of results per page.
        
        $("#Items").change(function() {
            onChangePage(1);
        });

        // Formatting digits for results count
        $(".pagination-results-holder").find(".start").replaceWithFormattedDigits();
        $(".pagination-results-holder").find(".end").replaceWithFormattedDigits();
        $(".pagination-results-holder").find(".total").replaceWithFormattedDigits();
        
    })(jQuery);
</script>
<%  } %>
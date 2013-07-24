<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Searches.Pagination" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>

<%  if (Total > 0)
    {
        var route = Model.Type == SearchesType.Recent
            ? SearchRoutes.PartialRecentSearches
            : SearchRoutes.PartialSavedSearches; %>

<div class="pagination-container">
    <div class="pagination-holder">
<%      if (CurrentPage == 1)
        { %>
            <div class="page first" page="1">First</div>
            <div class="page previous" page="<%= CurrentPage - 1 %>">&lt;&nbsp;<span class="underline">Previous</span></div>
<%      }
        else
        { %>
            <%= Html.RouteRefLink("First", route, new { page = 1 }, new { @class = "page first active secondary", page = 1 })%>
            <a class="page previous active" href="<%= Html.RouteRefUrl(route, new { page = CurrentPage - 1 }) %>" page="<%= CurrentPage - 1 %>">&lt;&nbsp;<span>Previous</span></a>
<%      } %>
        <div class="page-holder">
            <div class="page-numbers">
<%      if (FirstPage > 1)
        { %>
                <span class='pagination-ellipsis'>...</span>
<%      }
        
        for (var page = FirstPage; page <= LastPage; page++)
        {
            if (page == CurrentPage)
            { %>
                <div class="page page-selected"><%= page %></div>
<%          }
            else
            { %>
                <%= Html.RouteRefLink(page.ToString(), route, new { page }, new { @class = "page", page }) %>
<%          }
        }
        
        if (LastPage < TotalPages)
        { %>
                <span class='pagination-ellipsis'>...</span>
<%      } %>
            </div>
        </div>
<%      if (CurrentPage == TotalPages)
        { %>
        <div class="page next" page="<%= CurrentPage + 1 %>"><span class="underline">Next</span>&nbsp;&gt;</div>
        <div class="page last" page="<%= TotalPages %>">Last</div>
<%      }
        else
        { %>
        <a class="page next active" href="<%= Html.RouteRefUrl(route, new { page = CurrentPage + 1 }) %>" page="<%= CurrentPage + 1 %>"><span>Next</span>&nbsp;&gt;</a>
        <%= Html.RouteRefLink("Last", route, new { page = TotalPages }, new { @class = "page last active secondary", page = TotalPages }) %>
<%      } %>
    </div>
</div>
<%  } %>
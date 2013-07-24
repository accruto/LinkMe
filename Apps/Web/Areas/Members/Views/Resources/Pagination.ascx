<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.Pagination" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>

<% if (Model.Item2 > 0)
   { %>
<div class="pagination-container">
    <div class="pagination-holder">
<%  if (CurrentPage == 1)
    { %>
        <div class="page first<%=CurrentPage == 1 ? "" : " active secondary"%>" page="1">First</div>
        <div class="page previous<%=CurrentPage == 1 ? "" : " active"%>" page="<%= (CurrentPage - 1) %>">&lt;&nbsp;<span class="underline">Previous</span></div>
<%  }
    else
    { %>
        <a href="<%= Model.Item1.Criteria.GenerateUrl(1, Model.Item1.Categories) %>" class="page first<%= CurrentPage == 1 ? "" : " active secondary" %>" page="1">First</a>
        <a href="<%= Model.Item1.Criteria.GenerateUrl(CurrentPage - 1, Model.Item1.Categories) %>" class="page previous<%= CurrentPage == 1 ? "" : " active" %>" page="<%= CurrentPage - 1 %>">&lt;&nbsp;Previous</a>
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
            <div class="page page-selected"><%= page %></div>
<%      }
        else
        { %>
            <a href="<%= Model.Item1.Criteria.GenerateUrl(page, Model.Item1.Categories) %>" class="page" page="<%= page %>"><%= page %></a>
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
        <div class="page next<%= CurrentPage == TotalPages ? "" : " active" %>" page="<%= (CurrentPage + 1) %>"><span class="underline">Next</span>&nbsp;&gt;</div>
        <div class="page last<%= CurrentPage == TotalPages ? "" : " active secondary" %>" page="<%= TotalPages %>">Last</div>
<%  }
    else
    { %>
        <a href="<%= Model.Item1.Criteria.GenerateUrl(CurrentPage + 1, Model.Item1.Categories) %>" class="page next<%= CurrentPage == TotalPages ? "" : " active" %>" page="<%= CurrentPage + 1 %>">Next&nbsp;&gt;</a>
        <a href="<%= Model.Item1.Criteria.GenerateUrl(TotalPages, Model.Item1.Categories) %>" class="page last<%= CurrentPage == TotalPages ? "" : " active secondary" %>" page="<%= TotalPages %>">Last</a>
<%  } %>
    </div>
</div>
<% } %>
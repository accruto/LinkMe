<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Shared.Pagination" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<div class="pg <%= Total > 0 ? "show" : "hide" %>">
    <div class="itemsperpage">
        <span class="total">Jobs <%= Start %> - <%= End %> of <%= Total %></span>
        <span class="right">results per page</span>
        <%= Html.DropDownListField(Model.Presentation, "ItemsPerPage", p => p.Pagination.Items, Model.Presentation.ItemsPerPage.Cast<int?>()).WithLabel("Show") %>
    </div>

    <div class="pagination-container">
        <div class="pagination-holder">
            <div class="page first secondary<%= CurrentPage == 1 ? " active" : ""%>" page="1">First</div>
            <div class="page previous<%= CurrentPage == 1 ? " active" : ""%>" page="<%= (CurrentPage - 1) %>">&lt;&nbsp;<span class="underline">Previous</span></div>
            <a class="page first secondary<%= CurrentPage != 1 ? " active" : "" %>" href="<%= GetUrl(1) %>" page="1">First</a>
            <a class="page previous<%= CurrentPage != 1 ? " active" : "" %>" href="<%= GetUrl(CurrentPage - 1) %>" page="<%= CurrentPage - 1 %>">&lt;&nbsp;<span>Previous</span></a>

            <div class="page-holder">
                <div class="page-numbers">
                    <span class='pagination-ellipsis left<%= FirstPage > 1 ? " active" : "" %>'>...</span>
    <%  for (var page = FirstPage; page <= LastPage; page++)
        {
            if (page == CurrentPage)
            { %>
                    <div class="page current" page="<%= page %>"><%= page %></div>
                    <a class="page" href="<%= GetUrl(page) %>" page="<%= page %>"><%= page %></a>
    <%      }
            else
            { %>
                    <a class="page active" href="<%= GetUrl(page) %>" page="<%= page %>"><%= page %></a>
<%          }
        } %>
                    <span class='pagination-ellipsis right<%= LastPage < TotalPages ? " active" : "" %>'>...</span>
                </div>
            </div>
            
            <div class="page next<%= CurrentPage == TotalPages ? " active" : "" %>" page="<%= (CurrentPage + 1) %>"><span class="underline">Next</span>&nbsp;&gt;</div>
            <div class="page last secondary<%= CurrentPage == TotalPages ? " active" : "" %>" page="<%= TotalPages %>">Last</div>
            <a class="page next<%= CurrentPage != TotalPages ? " active" : "" %>" href="<%= GetUrl(CurrentPage + 1) %>" page="<%= CurrentPage + 1 %>"><span class="underline">Next</span>&nbsp;&gt;</a>
            <a class="page last secondary<%= CurrentPage != TotalPages ? " active" : "" %>" href="<%= GetUrl(TotalPages) %>" page="<%= TotalPages %>">Last</a>
        </div>
    </div>
</div>
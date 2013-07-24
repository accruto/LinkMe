<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.Pager" %>

<% if (Model.TotalPages > 1) { %>

    <div class="pagination">

        <% if (Model.CurrentPage > 1) { %>
            <%= Html.Link(GetUrl(Model.CurrentPage - 1), "Prev") %>
        <% } %>

        <% for (var page = 1; page <= Model.TotalPages; ++page) {
            if (Model.CurrentPage == page) { %>
                <span class="Curr"><%= page %></span>
            <% } else { %>
                <%= Html.Link(GetUrl(page), page.ToString()) %>
            <% }
        } %>
        
        <% if (Model.CurrentPage < Model.TotalPages) { %>
            <%= Html.Link(GetUrl(Model.CurrentPage + 1), "Next") %>
        <% } %>

    </div>

<% } %>
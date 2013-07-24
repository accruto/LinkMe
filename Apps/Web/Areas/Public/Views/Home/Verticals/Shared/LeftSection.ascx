<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.LeftSection" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>

<h1>Employers / Recruiters</h1>
<div class="fixed content" style="width: 230px; padding-top: 10px; padding-left: 20px;" runat="server">
    <p>Search <strong><%= Model.FeaturedStatistics.Members.ToString("N0") %></strong> candidates.</p>
    <ul class="horizontal_action-list action-list">
        <li><a href="<%= EmployerUrl %>"><strong>Search candidates now</strong></a></li>
        <li><a href="<%= EmployerUrl %>">Log in</a></li>
    </ul>
    <p><strong>Access candidates you won't find anywhere else.</strong></p>
</div>

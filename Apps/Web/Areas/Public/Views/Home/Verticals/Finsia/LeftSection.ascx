<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Finsia.LeftSection" %>

<div class="home-editable-section-title">
    <h1>Search Finance Jobs</h1>
</div>
<div class="home-editable-section-content">
    <ul>
<%  foreach (var search in Searches)
    { %>
        <li><a href="<%= search.Item2 %>"><%= search.Item1 %></a></li>
<%  } %>        
    </ul>
</div>

<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Ahri.LeftSection" %>

<div class="home-editable-section-title">
    <h1>Search HR & Recruitment Jobs</h1>
</div>
<div class="home-editable-section-content">
    <ul>
<%  foreach (var search in Searches)
    { %>
        <li><a href="<%= search.Item2 %>"><%= search.Item1 %></a></li>
<%  } %>        
    </ul>
</div>


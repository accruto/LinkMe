<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Scouts.LeftSection" %>

<div class="home-editable-section-title">
    <h1>Search Jobs Now</h1>
</div>
<div class="home-editable-section-content">
    <ul>
<%  foreach (var search in Searches)
    { %>
        <li><a href="<%= search.Item2 %>"><%= search.Item1 %></a></li>
<%  } %>        
    </ul>
</div>

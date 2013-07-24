<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Finsia.RightSection" %>

<div class="home-editable-section-title">
    <h1>Join Finsia's online groups</h1>
</div>    

<div class="home-editable-section-content">
<%  foreach (var group in Groups)
    { %>
    <p><a href="<%= group.Item2 %>" target="_blank"><%= group.Item1 %></a></p>
<%  } %>
</div>            

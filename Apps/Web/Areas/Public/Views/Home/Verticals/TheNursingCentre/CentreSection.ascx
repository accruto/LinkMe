<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.TheNursingCentre.CentreSection" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Latest Jobs</h1>
    </div>
    <div class="home-editable-section-content">
        <ul>
<%  foreach (var search in Searches)
    { %>
            <li><a href="<%= search.Item2 %>"><%= search.Item1 %></a></li>
<%  } %>        
        </ul>
        <p>&nbsp;</p>
        <p><a href="<%= new ReadOnlyApplicationUrl("~/search/jobs") %>" target="_parent"><strong>Find your&nbsp;nursing job here!</strong></a>&nbsp;</p>
    </div>        
</div>

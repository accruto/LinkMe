<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared.CentreSection" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Join our Discussion Groups</h1>
    </div>
    <div class="home-editable-section-content">
        <ul>
            <li><a href="<%= new ReadOnlyApplicationUrl("~/groups/formula-saea-group") %>">Join the Formula SAE-A Group</a></li>
            <li><a href="<%= new ReadOnlyApplicationUrl("~/groups/consulting-engineers-group") %>">Join the Consulting Engineers Group</a></li>
            <li><a href="<%= new ReadOnlyApplicationUrl("~/groups/assessing-engineers-group") %>">Join the Assessing Engineers Group</a></li>
            <li><a href="<%= new ReadOnlyApplicationUrl("~/groups/hybrids-group") %>">Join the Hybrids Group</a></li>
            <li><a href="<%= new ReadOnlyApplicationUrl("~/groups/renewable-fuels-group") %>">Join the Renewable Fuels Group</a></li>
        </ul>
    </div>        
</div>

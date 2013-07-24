<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>Become an AHRI member</h1>
    </div>
    <div class="home-editable-section-content">
        <div style="text-align: center">
            <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/ahri/img/ahrijoin.gif") %>" alt="" />
        </div>
    </div>        
</div>

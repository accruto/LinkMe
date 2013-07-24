<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="tile centre-tile">
    <div class="home-editable-section-title">
	    <h1>National E-News</h1>
    </div>
    <div class="home-editable-section-content">
        <div style="text-align: center;">
            <a href="http://www.scouts.com.au/main.asp?sKeyword=SOZ_E_NEWS">
                <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/scouts/img/eNewsBanner.jpg") %>" border="0" alt="Scouts e-News" />
            </a>
            <br />
            <br />
            <a href="http://www.scouts.com.au/main.asp?sKeyword=SOZ_E_NEWS">Current issue<br /></a>
        </div>
    </div>        
</div>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="home-editable-section-title">
    <h1>Proudly Supported By</h1>
</div>    

<div class="home-editable-section-content">
    <p align="center">
        <a style="border: none" href="http://www.vic.gov.au/">
            <img style="margin-top: 28px" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/sae/img/vic-government.png") %>" alt="Victoria - The Place To Be" />
        </a>
    </p>
</div>            

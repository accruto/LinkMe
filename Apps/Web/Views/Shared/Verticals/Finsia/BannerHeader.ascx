<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="community" id="community-header">
    <div id="finsiaContainer">
        <a class="logo" href="http://www.finsia.com/">
            <img src="<%= new ReadOnlyApplicationUrl("~/themes/communities/finsia/img/v2/FINSIA_Strapline_169x97_R.gif") %>" alt="" />
        </a>
        <a class="home" href="http://www.finsia.com/">Finsia.com</a>
        <span class="site-name">Finsia Career Network</span>
    </div>
</div>

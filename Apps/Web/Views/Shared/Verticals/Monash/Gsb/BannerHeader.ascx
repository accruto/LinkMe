<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>

<div class="community" id="community-header">
    <div style="width: 950px; margin-left: auto; margin-right: auto;">
        <div style="float:left;">
            <a href="http://www.buseco.monash.edu.au/student/sdo/index.html">
                <img width="950px" style="border-style:none;" src="<%= new ReadOnlyApplicationUrl("~/themes/communities/monash/gsb/img/buseco-header.jpg") %>" alt="Monash Business and Economics" />
            </a>
        </div>
    </div>
</div>

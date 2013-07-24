<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.AdministratorHeader" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Security"%>

<div id="header-links-container">
    <script type="text/javascript">
        function loadPage(url) {
            window.location = url;
        }
    </script>
    <div id="header-links">    
        <div class="left-section">
            <div id="logo" onclick="javascript:loadPage('<%= HomeUrl %>');"></div>
            <% Html.RenderPartial("AdministratorNav"); %>
        </div>
        <div class="right-section">
            <div id="action-links">
                <div class="rhs-links">
                    <div class="user-name_holder">Logged in as <div class="user-name"><%= Html.TruncateForDisplay(Context.User.FullName(), 20)%></div></div>                        
                    <div class="logout action-link" onclick="javascript:loadPage('<%= LogOutUrl %>');"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="header-links-container-shadow"></div>



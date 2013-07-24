<%@ Control Language="C#" Inherits="LinkMe.Web.Views.Shared.EmployerHeader" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Agents.Security"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<% Html.RenderPartial("EmployerHeaderScript"); %>

<div id="header-links-container">
    <script type="text/javascript">
        function loadPage(url) {
            window.location = url;
        }
    </script>
    <div id="header-links">
        <div class="left-section">
<%  if (CurrentEmployer != null)
    { %>
            <div id="logo" onclick="javascript:loadPage('<%= HomeUrl %>');"></div>
<%  }
    else
    { %>
            <div id="logo" onclick="javascript:loadPage('<%= AnonymousHomeUrl %>');"></div>
<%  } %>

<%  Html.RenderPartial("EmployerNav"); %>
        </div>
        <div class="right-section">
            <div id="action-links">
                <div class="rhs-links">
<%  if (CurrentEmployer != null)
    { %>
                    <div class="user-name_holder">Logged in as <div class="user-name"><%= Html.TruncateForDisplay(Context.User.FullName(), 20)%></div></div>                        
                    <div class="logout action-link" onclick="javascript:loadPage('<%= LogOutUrl %>');"></div>
                    <div class="account action-link" onclick="javascript:loadPage('<%= AccountUrl %>');"></div>
<%  }
    else
    { %>
                    <div class="member switch-link" onclick="javascript:loadPage('<%= MemberHomeUrl %>');"></div>
<%  } %>
                </div>
            </div>
        </div>        
    </div>
</div>
<div id="header-links-container-shadow"></div>

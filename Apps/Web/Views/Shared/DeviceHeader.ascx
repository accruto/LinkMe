<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<header id="header">
    <div class="divider one"></div>
    <div class="divider two"></div>
    <div class="divider three"></div>
    <div class="container">
<%  if (CurrentMember == null)
    { %>
        <div class="wrapper logo">
            <a class="logo" href="<%= Html.RouteRefUrl(LinkMe.Web.Areas.Public.Routes.HomeRoutes.Home) %>"></a>
        </div>
<%      if (!ClientUrl.Equals(LoginsRoutes.LogIn.GenerateUrl()))
        { %>
        <div class="wrapper button">
            <a class="login" href="<%= Html.RouteRefUrl(LoginsRoutes.LogIn) %>"></a>
        </div>
<%      }
        if (!ClientUrl.Equals(JoinRoutes.Join.GenerateUrl()))
        { %>
        <div class="wrapper button">
            <a class="join" href="<%= Html.RouteRefUrl(JoinRoutes.Join) %>"></a>
        </div>
<%      }
    }
    else
    { %>
        <div class="wrapper logo">
            <a class="logo" href="<%= Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.HomeRoutes.Home) %>"></a>
        </div>
        <div class="wrapper button">
            <a class="searches" href="<%= Html.RouteRefUrl(SearchRoutes.Searches) %>"></a>
        </div>
        <div class="wrapper button">
            <a class="jobs" href="<%= Html.RouteRefUrl(JobAdsRoutes.Jobs) %>"></a>
        </div>
<%  } %>
    </div>
    <div class="divider four"></div>
</header>
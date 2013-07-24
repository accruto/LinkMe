<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<footer id="footer">
    <div class="divider">
        <div class="one"></div>
        <div class="two"></div>
        <div class="three"></div>
        <div class="four"></div>
    </div>
    <div class="container">
        <div class="loginorout wrapper">
<%  if (CurrentMember == null)
    { %>
            <%= Html.RouteRefLink("Login", LoginsRoutes.LogIn, new { returnUrl = ClientUrl }, new { @class = "login" })%>
            <span class="or">or</span>
            <%= Html.RouteRefLink("Join", JoinRoutes.Join, null, new { @class = "join" })%>
<%  }
    else
    { %>
            <%= Html.RouteRefLink("Logout", LoginsRoutes.LogOut, null, new { @class = "logout"}) %>
<%  } %>
        </div>
        <div class="wrapper">
            <%= Html.RouteRefLink("Standard site", SupportRoutes.SwitchBrowser, new { mobile = false, returnUrl = Context.GetClientUrl().AsNonReadOnly() }, new { @class = "standardsite" })%>
        </div>
        <div class="wrapper">
            <%= Html.RouteRefLink("LinkMe terms and conditions", SupportRoutes.Terms, null, new { @class = "tandc" }) %>
        </div>
    </div>
</footer>
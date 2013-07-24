<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LoginModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Logins"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceLogin) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceLogin) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <%
        var returnUrl = Request[LinkMe.Apps.Asp.Constants.ReturnUrlParameter]; //should put in to new MobileModel when it's ready
    %>
    <div class="login">
        <div class="title">
<%  if (Model.Reason == null)
    { %>
            Login to your LinkMe account
<%  }
    else
    {
        switch (Model.Reason.Value)
        {
            case LoginReason.AddJobAd: %>
                Save jobs to your LinkMe account. Login below or <%= Html.RouteRefLink("join", JoinRoutes.Join, new { returnUrl, Reason = Model.Reason.Value }) %>
<%              break;
            
            case LoginReason.Apply: %>
                To apply for this job please login to your LinkMe account
<%              break;
            
            case LoginReason.SaveSearch: %>
                Save a favourite search to your LinkMe account. Login below or <%= Html.RouteRefLink("join", JoinRoutes.Join, new { returnUrl, Reason = Model.Reason.Value })%>
<%              break;
                
            default: %>
                Login to your LinkMe account
<%              break;
        }
    } %>
        </div>
        <div class="errorinfo">
            Login failed. Please try again
            <ul>
            </ul>
        </div>
		<%= Html.TextBoxField(Model.Login, "LoginId", l => l.LoginId).WithLabel("Email address").WithAttribute("autocapitalize", "off").WithAttribute("autocorrect", "off").WithAttribute("type", "email") %>
		<%= Html.PasswordField(Model.Login, l => l.Password).WithLabel("Password") %>
		<%= Html.CheckBoxField(Model.Login, l => l.RememberMe).WithLabelOnRight("Remember me")%>
		<%= Html.RouteRefLink("Forgot password?", AccountsRoutes.NewPassword, new { returnUrl }, new { id = "ForgotPassword" })%>
        <div class="button login" data-url="<%= Html.RouteRefUrl(AccountsRoutes.ApiLogIn) %>" data-returnurl="<%= string.IsNullOrEmpty(returnUrl) ? Html.RouteRefUrl(LinkMe.Web.Areas.Members.Routes.HomeRoutes.Home).ToString() : returnUrl %>">LOGIN</div>
        <div class="join">Don't have a LinkMe account?&nbsp;<%= Model.Reason.HasValue ? Html.RouteRefLink("Join now", JoinRoutes.Join, new { returnUrl, Reason = Model.Reason.Value }) : Html.RouteRefLink("Join now", JoinRoutes.Join, new { returnUrl })%></div>
    </div>
</asp:Content>
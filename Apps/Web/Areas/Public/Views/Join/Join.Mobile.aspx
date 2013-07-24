<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Public.Models.Join.JoinModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Logins" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceJoin) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceJoin) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <%
        var returnUrl = Request[LinkMe.Apps.Asp.Constants.ReturnUrlParameter]; //should put in to new MobileModel when it's ready
    %>
    <div class="join">
        <div class="title">
<%  if (Model.Reason == null)
    { %>
            Create a LinkMe account
<%  }
    else
    {
        switch (Model.Reason.Value)
        {
            case LoginReason.AddJobAd: %>
                Save jobs to your LinkMe account. Join below or <%= Html.RouteRefLink("login", LoginsRoutes.LogIn, new { returnUrl, Reason = Model.Reason.Value })%>
<%              break;
            
            case LoginReason.Apply: %>
                You will need to apply for this job from your computer. <span class="small">Please create a LinkMe account to save your job and apply later.</span>
<%              break;
            
            case LoginReason.SaveSearch: %>
                Save a favourite search to your LinkMe account. Join below or <%= Html.RouteRefLink("login", LoginsRoutes.LogIn, new { returnUrl, Reason = Model.Reason.Value })%>
<%              break;
                
            default: %>
                Create a LinkMe account
<%              break;
        }
    } %><br/><span class="pink">All fields are required</span>
        </div>
        <div class="errorinfo">
            Join failed. Please try again
            <ul>
            </ul>
        </div>
        <%= Html.TextBoxField("FirstName", "").WithLabel("First name") %>
        <%= Html.TextBoxField("LastName", "").WithLabel("Last name") %>
		<%= Html.TextBoxField("EmailAddress", "").WithLabel("Email address").WithAttribute("autocapitalize", "off").WithAttribute("autocorrect", "off").WithAttribute("type", "email") %>
		<%= Html.PasswordField("JoinPassword").WithLabel("Password") %>
        <%= Html.PasswordField("JoinConfirmPassword").WithLabel("Comfirm password") %>
        <div class="button join" data-url="<%= Html.RouteRefUrl(JoinRoutes.ApiJoin) %>" data-returnurl="<%= string.IsNullOrEmpty(returnUrl) ? Html.RouteRefUrl(AccountsRoutes.Welcome).ToString() : returnUrl %>">JOIN</div>
        <div class="tandc">By registering, you accept LinkMe's <%= Html.RouteRefLink("term and conditions", SupportRoutes.Terms) %></div>
    </div>
</asp:Content>
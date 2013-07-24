<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<Member>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Applications.Facade"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Activation email sent</h1>
    </div>
    
    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm())
    { %>        
    	    <p>An activation email has been sent to <strong><%= Model.EmailAddresses[0].Address %></strong></p>
    	    <p>Please check that email account and click on the link in the email to activate your account.</p>
    	    <p>Is that email address incorrect? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, new { returnUrl = AccountsRoutes.ActivationSent.GenerateUrl() }, new { id = "ChangeEmailAddress" }) %></p>
	        <p>
	            If you are experiencing other problems trying to access
	            our site, please contact our Member Support Hotline on <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallHtml %>
	            or <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.InternationalHtml %>.
	        </p>
	        <%= UserProfileFacade.GetDeveloperActivationLink(Model) %>
<%  } %>	        
        </div>
        <div class="section-foot"></div>
    </div>

</asp:Content>


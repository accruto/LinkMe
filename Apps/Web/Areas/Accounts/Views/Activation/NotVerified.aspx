<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<Member>" %>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
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
        <h1>Email addresses not verified</h1>
    </div>
    
    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm())
    {
        var primaryEmailAddress = Model.GetPrimaryEmailAddress();
        var secondaryEmailAddress = Model.GetSecondaryEmailAddress(); %>        
<%      if (secondaryEmailAddress == null)
        { %>
	        <p>
	            We are having trouble sending emails to you at <b><%= primaryEmailAddress.Address %></b>.
	            It is important that you specify your email address so that employers and recruiters may contact you.
            </p>
	        <p>Is this email address correct? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, new { returnUrl = AccountsRoutes.NotVerified.GenerateUrl() }, new { id = "ChangeEmailAddress" }) %></p>
	        <p>If correct, click this button and we will resend a verification email.</p>
<%      }
        else
        { %>
	        <p>
	            We are having trouble sending emails to you at <b><%= primaryEmailAddress.Address %></b> and <b><%= secondaryEmailAddress.Address %></b>.
	            It is important that you specify your email addresses so that employers and recruiters may contact you.
            </p>
	        <p>Are these email addresses correct? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, new { returnUrl = AccountsRoutes.NotVerified.GenerateUrl() }, new { id = "ChangeEmailAddress" }) %></p>
	        <p>If correct, click this button and we will resend verification emails.</p>
<%      } %>
    	    <p>
        	    <input type="submit" class="resend-verification-email-button" id="ResendVerificationEmail" value="" name="" />
    	    </p>
    	    <p>
    	        <a href="<%= Context.GetReturnUrl() %>">Continue</a>.
    	    </p>
	        <p>
	            If you are experiencing other problems trying to access
	            our site, please contact our Member Support Hotline on <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallHtml %>
	            or <%= LinkMe.Apps.Agents.Constants.PhoneNumbers.InternationalHtml %>.
	        </p>
<%  } %>	        
        </div>
        <div class="section-foot"></div>
    </div>

</asp:Content>


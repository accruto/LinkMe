<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Domain.Contacts.Member>" %>
<%@ Import Namespace="LinkMe.Apps.Agents" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #mainbody .title {
	        font-size : 2.25em;
	        font-weight : Bold;
	        color : #464646;
	        line-height : 1.2em;
	        margin : 0 0.3333333333333333em 0.5em;
        }
        p {
            font-size: 1.75em;
            color: #464646;
            line-height: 1.2em;
            margin : 0 0.3333333333333333em 2em;
        }
        a {
            float: none;
            color: #307EEC;
            text-decoration: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="title">Activation email sent</div>
    <p>An activation email has been sent to <strong><%= Model.EmailAddresses[0].Address %></strong></p>
    <p>Please check that email account and click on the link in the email to activate your account.</p>
    <p>Is that email address incorrect? <%= Html.RouteRefLink("Change my email address", AccountsRoutes.ChangeEmail, new { returnUrl = AccountsRoutes.ActivationSent.GenerateUrl() }, new { id = "ChangeEmailAddress" }) %></p>
	<p>
	    If you are experiencing other problems trying to access
	    our site, please contact our Member Support Hotline on <%= Constants.PhoneNumbers.FreecallHtml %>
	    or <%= Constants.PhoneNumbers.InternationalHtml %>.
	</p>
</asp:Content>
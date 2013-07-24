<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ChangeEmailModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Applications.Facade"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
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
        <h1>Change my email address</h1>
    </div>
    
    <div class="section forms_v2">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm(Context.GetClientUrl()))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
    	    <p>Your email address is currently <strong><%= Model.EmailAddress %></strong>.</p>
            <%= Html.EmailAddressTextBoxField(Model, m => m.EmailAddress).WithLabel("Email address").WithIsRequired() %>
            <br />
	        <p>Note: This new email address will become your new username.</p>
	        <p>Please remember to use your new email address next time you log in.</p>
            <%= Html.EmailAddressTextBoxField(Model, m => m.SecondaryEmailAddress).WithLabel("Secondary email address") %>
            <br />
            <p>Used in case your primary email address isn't working.</p>
	        <%= Html.ButtonsField().Add(new SubmitButton("send", "", "resend-activation-email-button button")).Add(new CancelButton()) %>
<%      }
    } %>	        
        </div>
        <div class="section-foot"></div>
    </div>

</asp:Content>


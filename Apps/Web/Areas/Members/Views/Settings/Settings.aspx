<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SettingsModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Settings"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Account settings</h1>
    </div>

    <div class="forms_v2">

        <div class="section">
        	<div class="section-content">
        	    <% Html.RenderPartial("VisibilityLink"); %>
        	</div>
        </div>
        
        <div class="section">
        	<div class="section-content">
        	
<%  using (Html.RenderRouteForm(SettingsRoutes.Settings, "ContactForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (Model.CanEditContactDetails)
            { %>        
                <h2>Contact details</h2>
            
                <%=Html.NameTextBoxField(Model, m => m.FirstName)
                   .WithLabel("First name").WithIsRequired()%>
                <%=Html.NameTextBoxField(Model, m => m.LastName)
                   .WithLabel("Last name").WithIsRequired()%>
                <%=Html.EmailAddressTextBoxField(Model, m => m.EmailAddress)
                   .WithLabel("Email address / Username").WithIsRequired()
                   .WithExampleText("The email address you specify is also your unique username that you will use to log in. It is important that you specify your regular email account so that employers and recruiters may contact you.")%>
                <%=Html.EmailAddressTextBoxField(Model, m => m.SecondaryEmailAddress)
                   .WithLabel("Secondary email address")
                   .WithExampleText("Used in case your primary email address isn't working.") %>
                <%=Html.ButtonsField().Add(new SaveButton()).Add(new CancelButton()) %>
<%          }
            else
            { %>
                <h2>Contact details</h2>
            
                <%=Html.NameTextBoxField(Model, m => m.FirstName)
                   .WithLabel("First name").WithIsReadOnly() %>
                <%=Html.NameTextBoxField(Model, m => m.LastName)
                    .WithLabel("Last name").WithIsReadOnly()%>
                <%=Html.EmailAddressTextBoxField(Model, m => m.EmailAddress)
                    .WithLabel("Email address").WithIsReadOnly()%>
<%          }
        }
    } %>

        	</div>
        </div>

<% if (Model.CanEditContactDetails)
   {%>        
        <div class="section">
            <div class="section-content">
                <ul class="action-list">
                    <li><%= Html.RouteRefLink("Change my password", AccountsRoutes.ChangePassword, null)%></li>
                    <li><%= Html.RouteRefLink("Deactivate my account", SettingsRoutes.Deactivate, null)%></li>
                </ul>
            </div>
        </div>

<% } %>

        <div class="section">
        
            <div class="section-title">
                <h1>Communication settings</h1>
            </div>
            
            <div class="section-content">
            
                <p>
                    Control the nature and frequency of emails you receive from us, employers and other members.
                </p>
                
<% using (Html.RenderRouteForm(SettingsRoutes.Communications, "CommunicationsForm"))
   {
       using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
       { %>
                <h2>Regular emails</h2>
                
        <% foreach (var category in Model.PeriodicCategories)
           { %>
           
                <%= Html.DropDownListField(
                        category,
                        category.Item1.Name,
                        c => c.Item2 != null ? c.Item2.Value : c.Item1.DefaultFrequency,
                        category.Item1.AvailableFrequencies)
                    .WithLabel(category.Item1.GetDisplayText()) %>

        <% } %>
        
                <h2>Notifications</h2>
                
        <% foreach (var category in Model.NotificationCategories)
           { %>
           
                <%= Html.CheckBoxField(
                        category,
                        category.Item1.Name,
                        c => c.Item2 == null || c.Item2.Value != Frequency.Never)
                    .WithLabelOnRight(category.Item1.GetDisplayText()) %>

        <% } %>
        
        <%= Html.ButtonsField().Add(new SaveButton("CommunicationsSave")).Add(new CancelButton("CommunicationsCancel")) %>
    <% }
   } %>
            
            </div>
        </div>
        
    </div>
    
</asp:Content>

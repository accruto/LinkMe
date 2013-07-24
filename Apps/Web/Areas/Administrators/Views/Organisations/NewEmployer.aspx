<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<NewEmployerModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink(Model.Organisation.FullName, OrganisationsRoutes.Edit, new { id = Model.Organisation.Id }) %></li>
                <li><%= Html.RouteRefLink("Credits", OrganisationsRoutes.Credits, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Employers", OrganisationsRoutes.Employers, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Reports", OrganisationsRoutes.Reports, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Communications", OrganisationsRoutes.Communications, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>New login</h1>
    </div>
    
    <div class="form">

<% using (Html.RenderForm())
   {
       using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
       { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
            
<%          if (Model.Organisation.IsVerified)
            { %>
       
                <div class="section-title">
                    <h2>Login details</h2>
                </div>
            
                <%= Html.TextBoxField(Model.Employer, l => l.LoginId).WithLabel("Username").WithIsRequired()%>
                <%= Html.PasswordField(Model.Employer, l => l.Password).WithIsRequired()%>
       
                <div class="section-title">
                    <h2>Contact details</h2>
                </div>
            
                <%= Html.EmailAddressTextBoxField(Model.Employer, l => l.EmailAddress).WithLabel("Email address").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model.Employer, l => l.FirstName).WithLabel("First name").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model.Employer, l => l.LastName).WithLabel("Last name").WithIsRequired()%>
                <%= Html.PhoneNumberTextBoxField(Model.Employer, l => l.PhoneNumber).WithLabel("Phone number").WithIsRequired()%>
            
                <div class="section-title">
                    <h2>Position</h2>
                </div>
            
                <%= Html.TextBoxField(Model.Employer, l => l.JobTitle).WithLabel("Job title")%>
                <%= Html.RadioButtonsField(Model.Employer, l => l.SubRole).WithLabel("Role").WithIsRequired()%>
                <%= Html.IndustryField(Model.Employer, l => l.IndustryId, Model.Employer.Industries).WithLabel("Industry")%>
            
                <%= Html.ButtonsField(new CreateButton(), new CancelButton()) %>
<%          }
            else
            { %>
                <%= Html.ButtonsField(new CancelButton()) %>
<%          } %>

            </div>
            <div class="section-foot"></div>
        </div>
<%     }
   } %>

    </div>
        
</asp:Content>

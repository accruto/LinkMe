<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<AffiliationEnquiry>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Recruiters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Affiliations.Communities"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.Employer) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Preferred Employment Partner application</h1>
    </div>
    
    <div class="forms_v2">

        <% using (Html.RenderForm()) {%>
        
            <% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft)) { %>
                   <%= Html.TextBoxField(Model, m => m.CompanyName).WithLabel("Company name").WithIsRequired() %>
                   <%= Html.TextBoxField(Model, m => m.EmailAddress).WithLabel("Email").WithIsRequired() %>
                   <%= Html.TextBoxField(Model, m => m.FirstName).WithLabel("First name").WithIsRequired() %>
                   <%= Html.TextBoxField(Model, m => m.LastName).WithLabel("Last name").WithIsRequired() %>
                   <%= Html.TextBoxField(Model, m => m.JobTitle).WithLabel("Job title") %>
                   <%= Html.TextBoxField(Model, m => m.PhoneNumber).WithLabel("Phone number").WithIsRequired() %>
                   <%= Html.ButtonsField().Add(new ApplyNowButton()) %>
            <% } %>

        <% } %>
        
    </div>
        
</asp:Content>
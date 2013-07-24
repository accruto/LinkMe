<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ConvertModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Affiliations.Communities"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Verticals.Models"%>
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
        <h1>Convert your community account</h1>
    </div>

    <div class="forms_v2">

        <div class="section">
        	<div class="section-content">
        	
<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
                <p>
                    To convert your <%= Model.Community.Name.Replace(" -", ",") %> account into a full LinkMe account we need to
                    confirm we have the correct account.
                </p>
                <p>
                    Please enter your <%= Model.Community.GetShortDisplayText() %> email address, your name, and the full job title and company name
                    for any one of the jobs in your profile.  Note that we require an <b>exact</b> match of both job title
                    and company name for security purposes.
                </p>
                <p>
                    <%= Html.RouteRefLink("Forgot your details?", SupportRoutes.ContactUs) %>
                    Contact us if you are having trouble converting your account.
                </p>

                <%= Html.EmailAddressTextBoxField(Model, m => m.EmailAddress)
                    .WithLabel("Email address").WithIsRequired() %>
                <%= Html.NameTextBoxField(Model, m => m.FirstName)
                    .WithLabel("First name").WithIsRequired() %>
                <%= Html.NameTextBoxField(Model, m => m.LastName)
                    .WithLabel("Last name").WithIsRequired() %>
                <%= Html.TextBoxField(Model, m => m.JobTitle)
                    .WithLabel("Job title").WithIsRequired() %>
                <%= Html.TextBoxField(Model, m => m.JobCompany)
                    .WithLabel("Company").WithIsRequired() %>
                   
                <br />
                <p>
                    Please nominate a new email address (not your <%= Model.Community.GetShortDisplayText() %> email address)
                    and password for your LinkMe account.  Your chosen email address is also your
                    LinkMe username.
                </p>
                
                <%= Html.EmailAddressTextBoxField(Model, m => m.NewEmailAddress)
                    .WithLabel("New email address").WithIsRequired()
                    .WithExampleText("The email address you specify is also your unique username that you will use to log in. It is important that you specify your regular email account so that employers and recruiters may contact you.") %>
                <%= Html.PasswordField(Model, m => m.Password)
                    .WithLabel("New password").WithIsRequired() %>
                <%= Html.PasswordField(Model, m => m.ConfirmPassword)
                    .WithLabel("Confirm password").WithIsRequired() %>
                <%= Html.ButtonsField().Add(new SaveButton()) %>
<%      }
    } %>

        	</div>
        </div>

    </div>
    
</asp:Content>

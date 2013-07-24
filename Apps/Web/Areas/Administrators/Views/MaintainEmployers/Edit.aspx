<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<UserModel<IEmployer, EmployerLoginModel>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Employers"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.RenderScripts(ScriptBundles.Administrators) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink("View credits", EmployersRoutes.Credits, new { id = Model.User.Id }) %></li>
                <li><%= Html.RouteRefLink("View credit usage", EmployersRoutes.Usage, new { id = Model.User.Id }) %></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Employer account</h1>
    </div>
    
    <div class="form">

<% using (Html.RenderRouteForm(EmployersRoutes.Edit, new { id = Model.User.Id }, "ContactForm"))
 {
     using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
     { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Details</h1>
                </div>
            
                <%= Html.NameTextBoxField(Model.User, e => e.FirstName)
                    .WithLabel("First name") %>
                <%= Html.NameTextBoxField(Model.User, e => e.LastName)
                    .WithLabel("Last name") %>
                <%= Html.EmailAddressTextBoxField(Model.User, "EmailAddress", e => e.EmailAddress == null ? null : e.EmailAddress.Address)
                    .WithLabel("Email address") %>
                <%= Html.PhoneNumberTextBoxField(Model.User, "PhoneNumber", e => e.PhoneNumber == null ? null : e.PhoneNumber.Number)
                    .WithLabel("Phone number") %>
                    
<%      if (Model.User.Organisation == null || Model.User.Organisation.IsVerified)
        { %>
                <%= Html.TextBoxField(Model.User, "OrganisationName", e => e.Organisation == null ? "" : e.Organisation.FullName)
                    .WithLabel("Organisation")
                    .WithLargestWidth() %>
<%      }
        else
        { %>
                <%= Html.RouteLinkField(Model.User.Organisation.FullName, OrganisationsRoutes.Edit, new { id = Model.User.Organisation.Id })
                    .WithLabel("Organisation")%>
<%      } %>

                <%= Html.TextBoxField(Model.UserLogin, e => e.LoginId)
                    .WithLabel("Username") %>
                <%= Html.ButtonsField(new SaveButton()) %>
            </div>    
            <div class="section-foot"></div>
        </div>
  <% }
 } %>

<% using (Html.RenderRouteForm(EmployersRoutes.Enable, new { id = Model.User.Id }, "EnableForm"))
 {
     using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
     { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Account</h1>
                </div>

                <%= Html.TextBoxField(Model.User, "IsEnabled", e => e.IsEnabled ? "Yes" : "No")
                    .WithLabel("Enabled").WithIsReadOnly() %>
                
                <%= Model.User.IsEnabled ? Html.ButtonsField(new DisableButton()) : Html.ButtonsField(new EnableButton()) %>
            </div>    
            <div class="section-foot"></div>
        </div>
<%  }
 } %>

<% using (Html.RenderRouteForm(EmployersRoutes.ChangePassword, new { id = Model.User.Id }, "ChangePasswordForm"))
 {
     using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
     { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Change password</h1>
                </div>

                <p>The employer will need to change their password on their next login.</p>
                
                <%= Html.TextBoxField(Model, m => m.UserLogin.Password)
                    .WithLabel("New password")
                    .WithExampleText("A new temporary password.") %>
                <%= Html.CheckBoxesField(Model)
                    .Add("Send new password email", m => m.UserLogin.SendPasswordEmail)
                    .WithExampleText("If this is checked then an email will be sent to the employer.") %>
                
                <%= Html.ButtonsField(new ChangeButton())%>
            </div>
            <div class="section-foot"></div>
        </div>
  <% }
} %>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.administrators.ready({
                urls: {
                    apiOrganisationsPartialMatchesUrl: '<%= Html.RouteRefUrl(OrganisationsRoutes.ApiPartialMatches) %>?name=',
                    apiLocationPartialMatchesUrl: '<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) %>?countryId=1&location='
                },
            });
        });
    </script>

</asp:Content>

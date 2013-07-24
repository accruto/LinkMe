<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<UserModel<Administrator, AdministratorLoginModel>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Administrators"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink("Administrators", AdministratorsRoutes.Index, null)%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Administrator account</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderRouteForm(AdministratorsRoutes.Enable, new {id = Model.User.Id}, "ContactForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
           
                <h1>Contact details</h1>
            
                <%= Html.NameTextBoxField(Model.User, e => e.FullName)
                    .WithLabel("Name").WithIsReadOnly() %>
                <%= Html.EmailLinkField(Model.User.EmailAddress.Address, Model.User.EmailAddress.Address)
                    .WithLabel("Email address") %>

            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

<%  using (Html.RenderRouteForm(AdministratorsRoutes.Enable, new { id = Model.User.Id }, "EnableForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <h1>Account details</h1>
                
                <%= Html.TextBoxField(Model.UserLogin, e => e.LoginId).WithLabel("Username")
                    .WithIsReadOnly() %>
                <%= Html.TextBoxField(Model.User, "IsEnabled", e => e.IsEnabled ? "Yes" : "No")
                    .WithLabel("Enabled").WithIsReadOnly() %>
                
                <%= Model.User.IsEnabled ? Html.ButtonsField(new DisableButton()) : Html.ButtonsField(new EnableButton()) %>
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

<%  using (Html.RenderRouteForm(AdministratorsRoutes.ChangePassword, new { id = Model.User.Id }, "ChangePasswordForm"))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <h1>Change password</h1>
        
                <%= Html.PasswordField(Model, m => m.UserLogin.Password)
                    .WithLabel("New password")
                    .WithExampleText("The password must be at least 8 characters long and must contain at least one upper-case letter, one lower-case letter and one digit.") %>
                
                <%= Html.ButtonsField(new ChangeButton()) %>
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

    </div>

</asp:Content>

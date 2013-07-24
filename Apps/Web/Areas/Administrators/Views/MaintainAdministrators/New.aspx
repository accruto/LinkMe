<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<CreateAdministratorModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Administrators"%>
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
        <h1>New administrator account</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
       
                <div class="section-title">
                    <h2>Login details</h2>
                </div>
            
                <%= Html.TextBoxField(Model, l => l.LoginId).WithLabel("Username").WithIsRequired()%>
                <%= Html.PasswordField(Model, l => l.Password)
                    .WithIsRequired()
                    .WithExampleText("The password must be at least 8 characters long and must contain at least one upper-case letter, one lower-case letter and one digit.") %>
                
                <div class="section-title">
                    <h2>Contact details</h2>
                </div>
            
                <%= Html.EmailAddressTextBoxField(Model, l => l.EmailAddress).WithLabel("Email address").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model, l => l.FirstName).WithLabel("First name").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model, l => l.LastName).WithLabel("Last name").WithIsRequired()%>
            
                <%= Html.ButtonsField(new CreateButton(), new CancelButton()) %>
            </div>
            <div class="section-foot"></div>
        </div>

<%      }
    } %>

    </div>
        
</asp:Content>

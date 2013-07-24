<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<NewCustodianModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Communities"%>
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
                <li><%= Html.RouteRefLink(Model.Community.Name, CommunitiesRoutes.Edit, new { id = Model.Community.Id }) %></li>
                <li><%= Html.RouteRefLink("Custodians", CommunitiesRoutes.Custodians, new { id = Model.Community.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>New custodian account</h1>
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
            
                <%= Html.TextBoxField(Model.Custodian, l => l.LoginId).WithLabel("Username").WithIsRequired()%>
                <%= Html.PasswordField(Model.Custodian, l => l.Password).WithIsRequired()%>
       
                <div class="section-title">
                    <h2>Contact details</h2>
                </div>
            
                <%= Html.EmailAddressTextBoxField(Model.Custodian, l => l.EmailAddress).WithLabel("Email address").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model.Custodian, l => l.FirstName).WithLabel("First name").WithIsRequired()%>
                <%= Html.NameTextBoxField(Model.Custodian, l => l.LastName).WithLabel("Last name").WithIsRequired()%>
            
                <%= Html.ButtonsField().Add(new CreateButton()).Add(new CancelButton()) %>

            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>

    </div>
        
</asp:Content>

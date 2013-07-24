<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<UnsubscribeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Models"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
        <%= Html.StyleSheet(StyleSheets.LoginOrJoin) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
<%  if (Model.Category == null)
    { %>
	    <h1>Unsubscribe</h1>
<%  }
    else
    { %>
	    <h1>Unsubscribe: <%= Model.Category.GetDisplayText() %></h1>
<%  } %>
    </div>

    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm(ViewData.GetClientUrl()))
    {
        if (!Model.HasUnsubscribed)
        {
            if (ViewData.ModelState.IsValid)
            { %>
            <p>
                Please confirm that you would like to unsubscribe from these types of emails.
            </p>
	        <%= Html.ButtonsField().Add(new UnsubscribeButton()) %>
<%          }
            else
            { %>
            <p>
                The details for unsubscribing you from this email are not recognised.
            </p>
            <br />
<%          }
        } %>
        
            <br />
            <p>
                You can also manage all your communication settings
                <%= CurrentRegisteredUser == null
                    ? MvcHtmlString.Create("by logging in")
                    : CurrentEmployer != null
                        ? Html.RouteRefLink("here", LinkMe.Web.Areas.Employers.Routes.SettingsRoutes.Settings)
                        : Html.RouteRefLink("here", LinkMe.Web.Areas.Members.Routes.SettingsRoutes.Settings)%>.
            </p>

            <br />
<%  } %>
        
        </div>
        <div class="section-foot"></div>
    </div>
    
<%  if (CurrentRegisteredUser == null)
    { %>
    <div class="main_columns columns forms_v2">
        <div class="column">
            <div class="login_section px335_shadowed_section shadowed_section section">
                <div class="section-head"></div>
                <div class="section-body" heightgroup="0">
                    <div class="section-title">
                        <h2>Log in to your account</h2>
                    </div>
                    <div class="section-content">
                        <% Html.RenderPartial("Login", Model.Login); %>
                    </div>
                </div>
                <div class="section-foot"></div>
            </div>
        </div>
    </div>
<%  } %>

</asp:Content>


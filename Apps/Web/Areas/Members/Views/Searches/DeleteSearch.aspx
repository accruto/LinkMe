<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<DeleteSearchModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
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
	    <h1>Unsubscribe</h1>
    </div>

    <div class="section">
        <div class="section-head"></div>
        <div class="section-body">
<%  using (Html.RenderForm(ViewData.GetClientUrl()))
    {
        if (!Model.HasDeleted)
        {
            if (ViewData.ModelState.IsValid)
            { %>
            <p>
                Please confirm that you would like to unsubscribe from receiving emails
                for these jobs: <%= Model.Search.Criteria.GetDisplayHtml() %>.
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
<%  } %>
        
        </div>
        <div class="section-foot"></div>
    </div>
    
</asp:Content>


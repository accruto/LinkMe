<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ConvertedModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Affiliations.Communities"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Verticals.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

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

                <p>
                    Your <%= Model.Community.GetShortDisplayText() %> account has been converted into a full LinkMe account.
                    We have sent an email to your new email address asking you to activate the account.
                    You can use your email address and password to log in <%= Html.RouteRefLink("here", HomeRoutes.Home) %>.
                </p>

        	</div>
        </div>

    </div>
    
</asp:Content>

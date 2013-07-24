<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="System.Web.Mvc.ViewPage<ObjectNotFoundModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Errors.Models.Errors"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts ID="RegisterJavaScripts1" runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

</asp:Content>
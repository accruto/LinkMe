<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FaqListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Faqs"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.Block.Page)%>
        <%= Html.RenderStyles(StyleBundles.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Support.Faqs)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.RenderScripts(ScriptBundles.Support.Faqs)%>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <% Html.RenderPartial("SubHeader"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="faqs">
        <% Html.RenderPartial("SearchBar", Model); %>
        <% Html.RenderPartial("LeftBar", Model); %>
        <div id="results">
            <div class="topbar"></div>
            <div class="bg">
                <% Html.RenderPartial("SearchFaqList", Model); %>
            </div>
            <div class="bottombar"></div>
        </div>
    </div>
    <% Html.RenderPartial("Init"); %>
</asp:Content>
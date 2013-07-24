<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<ResourceListModel<LinkMe.Domain.Resources.Video>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Views"%>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <% Page.Title = "Jobs - Online Job Search for Jobs, Employment &amp; Careers in Australia"; %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Resources)%>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.RenderScripts(ScriptBundles.Resources)%>
        <%= Html.JavaScript(JavaScripts.PlusOne) %>
        <%= Html.JavaScript(JavaScripts.SwfObject) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <% Html.RenderPartial("Search", Model.Categories); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="leftcontent">
        <% Html.RenderPartial("UserStatus", Model.ResumePercentComplete.ToString()); %>
        <% Html.RenderPartial("SideNav", Model.Categories); %>
        <% Html.RenderPartial("HelpfulDocs"); %>
    </div>
    <div class="maincontent">
        <% Html.RenderPartial("Tabs", Model.Presentation); %>
        <div class="tabcontent">
            <div class="topbar"></div>
            <div class="bg">
                <% Html.RenderPartial("PartialVideos", Model); %>
            </div>
            <div class="bottombar"></div>
        </div>
        <div class="relatedcontent"></div>
        <div class="recentcontent"></div>
        <% Html.RenderPartial("Overlays", Model.Categories); %>
    </div>
    <% Html.RenderPartial("RightContent", Model); %>
</asp:Content>
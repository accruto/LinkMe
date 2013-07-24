<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.CandidateConnect)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.CandidateConnect) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg">
        <div class="slidesshow">
            <div class="slides">
                <div class="slide status"></div>
                <div class="slide call"></div>
                <div class="slide filter"></div>
                <div class="slide favorite"></div>
            </div>
            <div class="balls">
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
            </div>
            <a class="appstore" target="_blank" href="<%= Model.AppStoreUrl %>"></a>
            <div class="descs">
                <div class="desc status"></div>
                <div class="desc call"></div>
                <div class="desc filter"></div>
                <div class="desc favorite"></div>
            </div>
        </div>
    </div>
</asp:Content>
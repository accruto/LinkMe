<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<ActivationModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Models"%>
<%@ Import Namespace="LinkMe.Web.Guests"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
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
        <h1>Email verification</h1>
    </div>
    
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

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
        <%= Html.StyleSheet(StyleSheets.Support) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>Privacy statement</h1>
    </div>
    
	<div class="first_section section forms_v2">
		<div class="section-content">
		
            <ul class="terms-links">
                <li><%= Html.RouteRefLink("General terms and conditions", SupportRoutes.Terms)%></li>
                <li><%= Html.RouteRefLink("Member terms and conditions", SupportRoutes.MemberTerms)%></li>
                <li><%= Html.RouteRefLink("Employer terms and conditions", SupportRoutes.EmployerTerms)%></li>
            </ul>

		    <% Html.RenderPartial("PrivacyContent", true); %>
	    </div>
    </div>

</asp:Content>


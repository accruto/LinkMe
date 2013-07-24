<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Blank.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Support) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title terms-detail">
        <h1>Employer terms and conditions</h1>
    </div>
    
	<div class="first_section section forms_v2 terms-detail">
		<div class="section-content">
		
            <ul>
                <li><%= Html.RouteRefLink("General terms and conditions", SupportRoutes.TermsDetail)%></li>
                <li><%= Html.RouteRefLink("Privacy statement", SupportRoutes.PrivacyDetail)%></li>
            </ul>
            
		    <% Html.RenderPartial("EmployerTermsContent", false); %>
            
        </div>
    </div>
    
</asp:Content>
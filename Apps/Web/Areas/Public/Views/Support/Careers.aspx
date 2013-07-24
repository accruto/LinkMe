<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Query.Search.JobAds"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.SidebarAbsent) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1>Careers at LinkMe</h1>
    </div>
    
	<div class="first_section section forms_v2">
		<div class="section-content">

            <ul class="terms-links">
                <li><%= Html.RouteRefLink("About LinkMe", SupportRoutes.AboutUs)%></li>
            </ul>
            
	    </div>
    </div>

	<div class="section forms_v2">
	
	    <div class="section-title">
	        <h2>Why work at LinkMe</h2>
	    </div>
	    
		<div class="section-content">

            <p>
	            LinkMe started out with the goal of providing all kinds of people the means with which they can achieve their career goals. 
	            Our small team has constantly expanded as we take ever growing strides towards achieving our original goal, and if you are 
	            looking for a new career challenge then perhaps LinkMe can help you achieve your own goals directly.
            </p>
        
	    </div>
    </div>

	<div class="section forms_v2">

	    <div class="section-title">
	        <h2>Working at LinkMe</h2>
	    </div>
	    
		<div class="section-content">
            
            <p>
	            If you are looking for a new job, and feel that you could benefit from, and contribute to our great LinkMe team, 
	            <%= Html.RouteRefLink("feel free to contact us", SupportRoutes.ContactUs) %>,
	            or apply directly for one of our jobs listed below 
	            (bonus points if you are already a LinkMe member).
            </p>
            <p>
	            <%= Html.RouteRefLink("View the jobs available at LinkMe", SearchRoutes.Search, new {Advertiser="LinkMe"})%>
            </p>
            
	    </div>
    </div>

</asp:Content>


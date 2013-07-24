<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Web"%>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.Employer) %>
        <%= Html.StyleSheet(StyleSheets.GuestEmployerHome) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

	<div id="body-content">
		<div class="inner">
			<img id="heading-icon" src="<%=new ReadOnlyApplicationUrl("~/content/images/employers/enquiries/icon_find-you.png")%>" />
			<h1 class="join-the-monash-gsb-career-portal">Join the Monash Business and Economics Career Portal as a 'Preferred
			Employment Partner' to connect with Monash Business and Economics students and graduates.</h1>
			<img src="<%=new ReadOnlyApplicationUrl("~/content/images/employers/enquiries/diagram_how-gsb-career-portal-works.png")%>"
			alt="Monash Business and Economics student and graduates post their resumes to the Monash Business and Economics Career Portal. Preferred employers enjoy free access to Monash Business and Economics resumes." />
			<h2 class="our-students-and-graduates-include">Our students and graduates include:</h2>
			<div class="checklist-columns">
				<ul class="checklist">
					<li>MBAs</li>
					<li>Masters of Marketing</li>
					<li>Masters of Business Law</li>
				</ul>
				<ul class="last_checklist checklist">
					<li>Masters of Human Resource Management</li>
					<li>Masters of Applied Finance</li>
					<li>Masters of Diplomacy and Trade</li>
				</ul>
			</div>
			<p><small>For a complete list of degrees offered at Monash Business and Economics please visit
			<a class="offsite" href="http://www.gsb.monash.edu.au/programs/offerings/">http://www.gsb.monash.edu.au/programs/offerings/</a></small></p>
			<div class="apply-now_boxed-action boxed-action">
				<a href="<%= Html.RouteRefUrl(EnquiriesRoutes.Apply) %>"><img class="boxed-action-icon" src="<%=new ReadOnlyApplicationUrl("~/content/images/employers/enquiries/icon_apply-now-41x43.png")%>" width="41" height="43" alt="" />Apply now</a> to become a Monash Business and Economics 'Preferred Employment Partner'.
			</div>
			<p>Monash Business and Economics and LinkMe have combined to provide the <em>Monash Business and Economics Career Portal</em> which
			gives you direct access, at no cost, to a huge database of Monash Business and Economics students and graduates
			for employment opportunities.</p>
			<p>Our students and graduates are highly skilled, enthusiastic and strongly motivated by career
			advancement. We engage regularly with them, to ensure their details and resumes are fresh and
			accurate.</p>
			<div class="boxed-actions-container">
				<ul class="boxed-actions">
					<li>Already joined? <a href="<%= Context.GetEmployerLoginUrl() %>">Login here</a>.</li>
				</ul>
			</div>
		</div>
	</div>

</asp:Content>
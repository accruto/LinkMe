<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Agents"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Resources</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("LeftSideBar"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="shadowed-section shadowed_section section">
        <div class="section-head"></div>
        <div class="section-body">
            <div class="section-title">
                <h1>Getting the best from LinkMe</h1>
            </div>
    	    <p>If you aren't finding the candidates you are looking for, you could try the suggestions in these...</p>
	        <p><a href="<%= new ReadOnlyApplicationUrl("~/resources/employer/search/SearchManual.pdf") %>">Hints and tips</a></p>
        </div>
        <div class="section-foot"></div>
	</div>

    <div class="shadowed-section shadowed_section section">
        <div class="section-head"></div>
        <div class="section-body">
	        <div class="section-title">
	            <h1>Applicant Tracking System (ATS) Partners</h1>
	        </div>

	        <p>
                Applicant Tracking Systems (otherwise known as an ATS), allow recruiters and employers to better
                manage the entire recruitment process, by allowing them to multi-post job ads to various job boards.
                LinkMe integrates with the following ATS partners which means that when clients post jobs through
                these ATS', they are automatically posted to the LinkMe job board.
            </p>
            <p>
                LinkMe also integrates with different Multiposting services, which allows job advertisers to post
                multiple ads at once.
            </p>
            <p>
                For enquiries on how we can help, please contact us at <%= Constants.PhoneNumbers.FreecallHtml %>.
            </p>

            <h3>Currently Integrated</h3>
            <ul style="list-style-type: none; margin-left: 0px;">
                <li>AdLogic</li>
                <li>Bond Adapt</li>
                <li>BroadBean</li>
                <li>Fast Track</li>
                <li>Digital Motor Works</li>
                <li>Personnel Concept (in house ATS)</li>
                <li>Job Adder</li>
                <li>Page Up People</li>
                <li>Recruit Asp</li>
                <li>Recruit Live</li>
                <li>Turbo Recruit</li>
                <li>3 Hats</li>
            </ul>
            
            <h3>Pending</h3>
            <ul style="list-style-type: none; margin-left: 0px;">
                <li>Arbita Integrator</li>
                <li>Job Serve</li>
                <li>Nga Dot Net</li>
                <li>ResourceWare</li>
                <li>Staff Cv</li>
                <li>Taleo Software</li>
                <li>CRIS Recruitment Solutions</li>
            </ul>
	    </div>
        <div class="section-foot"></div>
	</div>
</asp:Content>


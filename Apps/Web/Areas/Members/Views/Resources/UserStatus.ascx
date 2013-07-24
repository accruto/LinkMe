<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<String>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Shared.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<div class="userstatus">
    <div class="leftbar"></div>
    <div class="bg <%= CurrentMember == null ? "notloggedin" : "loggedin" %>">
        <% if (CurrentMember == null) { %>
            <%= Html.LoginLink("Login or Join", Context, new { @class = "loginbutton" })%>
            <div class="line"></div>
            <div class="text">Search thousands of jobs, or upload your resume and let employers find you</div>
            <div class="line"></div>
            <div class="searchjobs"><div class="icon"></div><a href="<%= new ReadOnlyApplicationUrl(false, "~/search/jobs") %>">Search for jobs</a></div>
            <div class="uploadresume"><div class="icon"></div><%= Html.RouteRefLink("Upload your resume", JoinRoutes.Join)%></div>
        <% } else { %>
            <div class="hello">Hi <%= CurrentMember.FirstName %></div>
            <div class="line"></div>
            <div class="profilecompletion" percent="<%= Model %>">
	            <span class="text">Profile completion</span>
	            <div class="completionbar">
		            <div class="bg"></div>
		            <div class="fg"></div>
	            </div>
	            <span class="percenttext"></span>
            </div>
            <div class="updateresume"><div class="icon"></div><%= Html.RouteRefLink("Update your resume", ProfilesRoutes.Profile)%></div>
            <div class="searchjobs"><div class="icon"></div><a href="<%= new ReadOnlyApplicationUrl(false, "~/search/jobs") %>">Search for jobs</a></div>
        <% } %>
    </div>
    <div class="rightbar"></div>
</div>

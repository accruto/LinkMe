<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<div class="exposeyourself">
    <div class="topbar"></div>
    <div class="bg">
        <div class="title big">Expose yourself...</div>
        <div class="title">...to the 70% of jobs that are never advertised</div>
        <img class="icon" src="<%= Images.Baby %>" />
        <div onclick="javascript:loadPage('<%= Html.RouteRefUrl(JoinRoutes.Join) %>');" class="upload-resume-button"></div>
    </div>
    <div class="bottombar">
        <div class="logo"></div>
    </div>
</div>
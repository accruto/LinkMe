<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EmployerLeftSideBar.ascx.cs" Inherits="LinkMe.Web.UI.Registered.Employers.EmployerLeftSideBar" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div>
    <div class="section">
	    <div class="section-content">
		    <ul class="plain_action-list action-list">
			    <li><a href="<%= SearchRoutes.Search.GenerateUrl() %>" class="new-search-action">New search</a></li>
			    <li><a href="<%= CandidatesRoutes.Folders.GenerateUrl() %>" class="manage-folders-action">Manage folders</a></li>
		    </ul>
	    </div>
    </div>
</div>

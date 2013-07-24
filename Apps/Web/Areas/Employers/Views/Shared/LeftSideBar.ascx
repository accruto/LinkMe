<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>

<div class="section">
	<div class="section-body">
		<ul class="plain-actions actions">
			<li><%= Html.RouteRefLink("New search", SearchRoutes.Search, null, new { @class = "new-search-action" }) %></li>
			<li><%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders, null, new { @class = "manage-folders-action" }) %></li>
		</ul>
	</div>
</div>

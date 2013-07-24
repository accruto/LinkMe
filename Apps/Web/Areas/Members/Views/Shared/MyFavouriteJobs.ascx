<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="area favourite <%= CurrentMember == null ? "" : "expanded" %>">
    <div class="titlebar">
        <div class="icon"></div>
        <div class="title">My favourite jobs</div>
        <div class="arrow"></div>
    </div>
    <div class="divider"></div>
    <div class="dragjobshere">Drag jobs here</div>
    <div class="content">
<%  if (CurrentMember != null)
    { %>
        <div class="folder flagged" id="<%= Model.Folders.FlagList.Id %>">
            <div class="icon"></div>
            <%= Html.RouteRefLink("Flagged jobs", JobAdsRoutes.FlagList, null, new { @class = "title"}) %>
            <div class="count">(<%= (from fd in Model.Folders.FolderData where fd.Key == Model.Folders.FlagList.Id select fd.Value.Count).First() %>)</div>
            <div class="icon empty"></div>
        </div>
<%      for (var i = 0; i < Model.Folders.PrivateFolders.Count; i++)
        {
            if (i == 1)
            { %>
            <div class="morefolders collapsed">
<%          } %>
                <div class="folder" id="<%= Model.Folders.PrivateFolders[i].Id %>">
                    <div class="icon"></div>
                    <a class="title" href="<%= Html.RouteRefUrl(JobAdsRoutes.Folder, new { folderId = Model.Folders.PrivateFolders[i].Id }) %>" title="<%= Model.Folders.PrivateFolders[i].Name %>"><%= Model.Folders.PrivateFolders[i].Name %></a>
                    <div class="count">(<%= (from fd in Model.Folders.FolderData where fd.Key == Model.Folders.PrivateFolders[i].Id select fd.Value.Count).First()%>)</div>
                    <div class="icon empty"></div>
                    <div class="icon rename"></div>
                </div>
<%      }
        if (Model.Folders.PrivateFolders.Count > 2)
        { %>
            </div>
            <div class="toggle collapsed">Show all folders</div>
<%      }
    } %>
    </div>
</div>
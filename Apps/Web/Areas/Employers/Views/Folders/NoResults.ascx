<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FolderListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<br /><br />
This folder does not contain any candidates.<br /><br />

<% if (Model.Folder.FolderType == FolderType.Mobile) {%>
You can add favourite candidates to this folder using the LinkMe "Candidate Connect" app on your iPhone or iPad.
<br /><br />
Alternatively, you can add candidates directly from your search results by selecting the relevant candidates and 
then using the drop-down action menus, or simply drag-and-drop the candidates directly to the folder. 
These candidates will also then appear in the "Candidate Connect" Favourites folder on your iPhone/iPad.
<% } else {%>
You can add candidates to a folder from your search results by either selecting candidates and using the
drop-down action menus, or simply drag-and-drop candidates directly to a folder.<br /><br />
<% }%>
<br /><br />
You can manage all your folders from the <%= Html.RouteRefLink("Manage folders", CandidatesRoutes.Folders)%> page.


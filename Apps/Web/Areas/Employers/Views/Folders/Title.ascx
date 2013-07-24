<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FolderListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Employers.Candidates"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Candidates"%>

<span id="results-header-text" style="display:none;"><%= Html.Encode(Model.Folder.GetNameDisplayText()) %></span>



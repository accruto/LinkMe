<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<%
    var startIndex = ((Model.Presentation.Pagination.Page ?? 1) - 1) * (Model.Presentation.Pagination.Items ?? Model.Presentation.DefaultItemsPerPage) +  1;
    var endIndex = (Model.Presentation.Pagination.Page ?? 1) * (Model.Presentation.Pagination.Items ?? Model.Presentation.DefaultItemsPerPage);
    if (endIndex > Model.Results.TotalJobAds) endIndex = Model.Results.TotalJobAds;
%>
<div class="resultscount">
    <span class="total"><b>Jobs <%= startIndex %> - <%= endIndex%></b>of<span class="count"><%= Model.Results.TotalJobAds %></span></span>
    <% if (Model.ListType == JobAdListType.SearchResult) { %>
        <div class="addtofolder">
            <div class="icon"></div>
            <div class="action">Add <%= endIndex - startIndex + 1%> jobs to a folder</div>
        </div>
        <div class="clearflags">
            <div class="icon"></div>
            <div class="action">Clear all job flags</div>
            <span>in this search</span>
        </div>
    <% } %>
    <% if (Model.ListType == JobAdListType.BlockList) { %>
        <div class="restoreall">
            <div class="icon"></div>
            <div class="action">Restore <%= endIndex - startIndex + 1%> jobs</div>
        </div>
    <% } %>
</div>
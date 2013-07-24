<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Query.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="sort">
<%  if (Model.ListType != JobAdListType.SuggestedJobs && Model.ListType != JobAdListType.SimilarJobs)
    {
        var sortCriteria = Model is JobAdSearchListModel
            ? ((JobAdSearchListModel) Model).Criteria.SortCriteria
            : ((JobAdSortListModel) Model).Criteria.SortCriteria; %>
        <%= Html.DropDownListField(Model, "SortOrder", m => sortCriteria.SortOrder, Model.SortOrders).WithLabel("Sort by").WithText(s => s.GetDisplayText()) %>
        <div class="ascending <%= sortCriteria.ReverseSortOrder ? "active" : "" %>"></div>
        <div class="descending <%= sortCriteria.ReverseSortOrder ? "" : "active" %>"></div>
        <div class="sorttext">Least to most</div>
<%  } %>
    <div class="expandalljobs">
        <div class="text expand">Expand all jobs</div>
        <div class="button expand"></div>
    </div>
</div>
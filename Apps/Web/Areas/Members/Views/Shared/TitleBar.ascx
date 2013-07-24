<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="titlebar">
    <% Html.RenderPartial("BulkAction", Model); %>
    <div class="titles">
        <div class="title info">Info</div>
        <div class="title salary">Salary</div>
        <div class="title date">Date posted</div>
        <div class="icon flagged"></div>
        <div class="title flag">Flag</div>
    </div>
</div>
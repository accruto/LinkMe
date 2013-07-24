<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="area hiddenjobs">
    <div class="icon"></div>
    <%= Html.RouteRefLink("Hidden jobs", JobAdsRoutes.BlockList, null, new { @class = "title" })%>
    <div class="count">(<%= (Model.BlockLists == null || Model.BlockLists.BlockListCount == null) ? 0 : Model.BlockLists.BlockListCount.Item2 %>)</div>
</div>
<div class="checkbox checked" id="apiorpartial"></div><span class="apiorpartialtext">Tick for API call/UnTick for Partial call</span>
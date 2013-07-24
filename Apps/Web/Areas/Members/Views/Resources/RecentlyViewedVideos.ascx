<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<div class="recentlist">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">My recently viewed videos</div>
        <div class="rightbar"></div>
    </div>
    <div class="bg">
<%  if (Model.RecentItems.Count == 0)
    { %>
            <div class="text">You haven't viewed any articles yet</div>
<%  }
    else
    {
        for (var i = 0; i < Model.RecentItems.Count && i < 5; i++)
        {
            var video = Model.RecentItems[i]; %>
                <div class="item">
                    <span><%= (i + 1) %>.</span>
                    <a href="<%= video.GenerateUrl(Model.Categories) %>" class="title" title="<%= video.Title %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialVideo, new { id = video.Id })) %>"><%= video.Title %></a>
                    <div class="date"><%= Model.LastViewedTimes[video.Id].GetRecentlyViewedDateDisplayText()%></div>
                </div>
<%      } %>
            <div class="viewmore">
                <div class="leftbar"></div>
                <div class="bg">
                    <%= Html.RouteRefLink("View more", ResourcesRoutes.RecentVideos, null, new Dictionary<string, object> { { "type", ResourceType.Video } })%>
                </div>
                <div class="rightbar"></div>
            </div>
<%  } %>
    </div>
    <div class="bottombar"></div>
</div>
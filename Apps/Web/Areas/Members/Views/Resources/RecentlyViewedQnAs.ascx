<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<div class="recentlist">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">My recently viewed questions</div>
        <div class="rightbar"></div>
    </div>
    <div class="bg">
<%  if (Model.RecentItems.Count == 0)
    { %>
            <div class="text">You haven't viewed any questions yet</div>
<%  }
    else
    {
        for (var i = 0; i < Model.RecentItems.Count && i < 5; i++)
        {
            var qna = Model.RecentItems[i]; %>
                <div class="item">
                    <span><%= (i + 1) %>.</span>
                    <a href="<%= qna.GenerateUrl(Model.Categories) %>" class="title" title="<%= qna.Title %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialQnA, new { id = qna.Id })) %>"><%= qna.Title %></a>
                    <div class="date"><%= Model.LastViewedTimes[qna.Id].GetRecentlyViewedDateDisplayText() %></div>
                </div>
<%      } %>
            <div class="viewmore">
                <div class="leftbar"></div>
                <div class="bg">
                    <%= Html.RouteRefLink("View more", ResourcesRoutes.RecentQnAs, null, new Dictionary<string, object> { { "type", ResourceType.QnA } })%>
                </div>
                <div class="rightbar"></div>
            </div>
<%  } %>
    </div>
    <div class="bottombar"></div>
</div>
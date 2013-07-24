<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<div class="recentlist">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">My recently viewed articles</div>
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
            var article = Model.RecentItems[i];
            var rating = Model.Ratings[article.Id];
            var averageRating = Math.Round(rating.AverageRating * 2, MidpointRounding.AwayFromZero) / 2; %>
                <div class="item">
                    <span><%= (i + 1) %>.</span>
                    <a href="<%= article.GenerateUrl(Model.Categories) %>" class="title" title="<%= article.Title %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticle, new { id = article.Id })) %>"><%= article.Title %></a>
                    <div class="rating">
<%          for (var j = 0; j < 5; j++)
            { %>
                            <div class="star <%= j >= averageRating ? "empty" : "" %> <%= (j < averageRating && (j + 1) > averageRating) ? "half" : "" %>"></div>
<%          } %>
                    </div>
                    <div class="date"><%= Model.LastViewedTimes[article.Id].GetRecentlyViewedDateDisplayText() %></div>
                </div>
<%      } %>
            <div class="viewmore">
                <div class="leftbar"></div>
                <div class="bg">
                    <%= Html.RouteRefLink("View more", ResourcesRoutes.RecentArticles, null, new Dictionary<string, object> { { "type", ResourceType.Article } })%>
                </div>
                <div class="rightbar"></div>
            </div>
<%  } %>
    </div>
    <div class="bottombar"></div>
</div>
<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceViewUserControl<ResourceModel<Article>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>

<%
    var article = Model.Resource;
    var rating = Model.Ratings[article.Id];
    var averageRating = Math.Round(rating.AverageRating * 2, MidpointRounding.AwayFromZero) / 2;
%>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)article)); %>
<% Html.RenderPartial("TabNumbers", Model); %>
<div class="article" viewurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiViewArticle, new { id = article.Id })) %>" url="<%= article.GenerateUrl(Model.Categories) %>">
    <div class="toparea">
        <% if (Model.Criteria != null) { %>
        <div class="back">
            <div class="icon"></div>
            <a href="<%= Model.Criteria.GenerateUrl(Model.Presentation.Pagination.Page, Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialCurrent)) %>">Back to articles</a>
        </div>
        <% } %>
    </div>
    <div class="articlearea">
        <div class="topbar"></div>
        <div class="bg">
            <div>
                <div class="title" title="<%= article.Title %>"><%= HighlightTitle(article.Title)%></div>
                <div class="rating">
                    <% for (var i = 0; i < 5; i++) { %>
                        <div class="star <%= i >= averageRating ? "empty" : "" %> <%= (i < averageRating && (i + 1) > averageRating) ? "half" : "" %>"></div>
                    <% } %>
                    <div class="ratedcount">(<%= rating.RatingCount %>)</div>
                </div>
            </div>
            <div class="content"><%= HighlightContent(article.Text)%></div>
        </div>
        <div class="bottombar"></div>
    </div>
    <div class="ratingarea">
        <div class="topbar"></div>
        <div class="bg">
            <div class="title">Rate this Article</div>
            <div class="rating" userrating="<%= rating.UserRating ?? 0 %>" rateurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiRateArticle, new { id = article.Id })) %>">
                <% for (var i = 0; i < 5; i++) { %>
                    <div class="star <%= i >= (rating.UserRating ?? 0) ? "empty" : "" %>"></div>
                <% } %>
                <div class="ratingsaved">Rating saved</div>
            </div>
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.Viewings[article.Id] %></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(article, Model.Categories)); %>
            </div>
        </div>
        <div class="bottombar"></div>
    </div>
    <% if (Model.Criteria != null) { %>
    <div class="back">
        <div class="leftbar"></div>
        <div class="bg">
            <div class="icon"></div>
            <a href="<%= Model.Criteria.GenerateUrl(Model.Presentation.Pagination.Page, Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialCurrent)) %>">Back to articles</a>
        </div>
        <div class="rightbar"></div>
    </div>
    <% } %>
</div>
<% Html.RenderPartial("RelatedContent", Model); %>
<% Html.RenderPartial("RecentlyViewedArticles", Model); %>
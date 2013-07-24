<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceItemViewUserControl<Article>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>

<%
    var article = Model.Item;
    var rating = Model.List.Ratings[article.Id];
    var averageRating = Math.Round(rating.AverageRating * 2, MidpointRounding.AwayFromZero) / 2;
    var categories = Model.List.Categories;
%>
<div class="articleitem">
    <div class="bg">
        <div class="leftside">
            <div class="toparea">
                <a class="title" href="<%= article.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticle, new { id = article.Id })) %>" title="<%= article.Title %>"><%= HighlightTitle(article.Title)%></a>
                <div class="categorylist">
                    <div class="category"><%= categories.GetCategoryBySubcategory(article.SubcategoryId).Name%></div>
                    <div class="divider"></div>
                    <div class="subcategory"><%= categories.GetSubcategory(article.SubcategoryId).Name%></div>
                </div>
            </div>
            <div class="divider"></div>
            <div class="content" title="<%= HtmlUtil.StripHtmlTags(article.Text) %>"><%= SummarizeContent(article.Text + " ")%></div>
            <div class="readfull">
                <a href="<%= article.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticle, new { id = article.Id })) %>">Read full</a>
            </div>
        </div>
        <div class="rightside">
            <div class="rating">
                <% for (var i = 0; i < 5; i++) { %>
                    <div class="star <%= i >= averageRating ? "empty" : "" %> <%= (i < averageRating && (i + 1) > averageRating) ? "half" : "" %>"></div>
                <% } %>
                <div class="ratedcount">(<%= rating.RatingCount %>)</div>
            </div>
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.List.Viewings[article.Id] %></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(article, categories)); %>
            </div>
        </div>
    </div>
</div>
<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<%
    var article = Model.TopRatedArticle;
    var qna = Model.TopViewedQnA;
%>
<div class="rightcontent">    
    <div class="button ask"></div>
    <% if (article != null)
       {
           var rating = Model.Ratings[article.Id];
           var averageRating = Math.Round(rating.AverageRating * 2, MidpointRounding.AwayFromZero) / 2; %>
    <div class="toprated">
        <div class="titlebar">
            <div class="leftbar"></div>
            <div class="bg">This month's top rated article</div>
            <div class="rightbar"></div>
        </div>
        <div class="bg">
            <div class="title" title="<%= article.Title %>"><%= article.Title%></div>
            <div class="content"><%= article.Text%></div>
            <div class="rating">
                <% for (var i = 0; i < 5; i++)
                   { %>
                    <div class="star <%= i >= averageRating ? "empty" : "" %> <%= (i < averageRating && (i + 1) > averageRating) ? "half" : "" %>"></div>
                <% } %>
            </div>
            <a href="<%= article.GenerateUrl(Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticle, new { id = article.Id })) %>">Read full</a>
        </div>
        <div class="bottombar"></div>
    </div>
    <% } %>
    <% if (qna != null) { %>
    <div class="mostpopular">
        <div class="titlebar">
            <div class="leftbar"></div>
            <div class="bg">Popular questions</div>
            <div class="rightbar"></div>
        </div>
        <div class="bg">
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.Viewings[qna.Id] %></div>
                <div class="rightbar"></div>
            </div>
            <div class="commented">
                <div class="leftbar"></div>
                <div class="number"><%= Model.Comments[qna.Id] %></div>
                <div class="rightbar"></div>
            </div>
            <div class="title" title="<%= qna.Title %>"><%= qna.Title %></div>
            <div class="answeredby"></div>
            <div class="content"><%= qna.Text%></div>
            <a href="<%= qna.GenerateUrl(Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialQnA, new { id = qna.Id })) %>">Read full</a>
        </div>
        <div class="bottombar"></div>
    </div>
    <% } %>
    <div class="RSR">
        <div class="titlebar">
            <div class="leftbar"></div>
            <div class="bg">Need a professional resume?</div>
            <div class="rightbar"></div>
        </div>
        <div class="bg"></div>
    </div>
    <% Html.RenderPartial("GoogleVerticalAds"); %>
</div>
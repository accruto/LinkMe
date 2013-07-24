<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<div class="relatedlist">
    <div class="titlebar">
        <div class="leftbar"></div>
        <div class="bg">Related Content</div>
        <div class="rightbar"></div>
    </div>
    <div class="bg">
<%  if (Model.RelatedItems.Count == 0)
    { %>
            <div class="text">There isn't any related content</div>
<%  }
    else
    {
        for (var i = 0; i < Model.RelatedItems.Count && i < 5; i++)
        {
            RouteReference partialRoute;
            ResourceType resourceType;
            if (Model.RelatedItems[i] is Article)
            {
                resourceType = ResourceType.Article;
                partialRoute = ResourcesRoutes.PartialArticle;
            }
            else if (Model.RelatedItems[i] is Video)
            {
                resourceType = ResourceType.Video;
                partialRoute = ResourcesRoutes.PartialVideo;
            }
            else
            {
                resourceType = ResourceType.QnA;
                partialRoute = ResourcesRoutes.PartialQnA;
            } %>
                <div class="item">
                    <div class="icon <%= resourceType %>"></div>
                    <span><%= (i + 1) %>.</span>
                    <a href="<%= Model.RelatedItems[i].GenerateUrl(Model.Categories) %>" class="title" title="<%= Model.RelatedItems[i].Title %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(partialRoute, new { id = Model.RelatedItems[i].Id })) %>"><%= Model.RelatedItems[i].Title %></a>
                </div>
<%      }
    } %>
    </div>
    <div class="bottombar"></div>
</div>
<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceItemViewUserControl<Video>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>

<%
    var video = Model.Item;
    var categories = Model.List.Categories;
%>
<div class="videoitem" externalvideoid="<%= video.ExternalVideoId %>" videodetailurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiVideo)) %>">
    <div class="bg">
        <div class="leftside">
            <img class="preview" />
            <a class="title" href="<%= video.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialVideo, new { id = video.Id })) %>" title="<%= video.Title %>"><%= HighlightTitle(video.Title)%></a>
            <div class="transcript" title="<%= Html.Encode(HtmlUtil.StripHtmlTags(video.Text)) %>"><%= SummarizeContent(video.Text + " ")%></div>
            <div class="duration"></div>
        </div>
        <div class="rightside">
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.List.Viewings[video.Id]%></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(video, categories)); %>
            </div>
            <div class="button watch">
                <a href="<%= video.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialVideo, new { id = video.Id })) %>">Watch now</a>
            </div>
        </div>
    </div>
</div>
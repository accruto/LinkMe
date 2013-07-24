<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceViewUserControl<ResourceModel<Video>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Helpers"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)Model.Resource)); %>
<% Html.RenderPartial("TabNumbers", Model); %>

<div class="video" viewurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiViewVideo, new { id = Model.Resource.Id })) %>" externalvideoid="<%= Model.Resource.ExternalVideoId %>" videodetailurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiVideo)) %>">
    <div class="toparea">
        <% if (Model.Criteria != null) { %>
        <div class="back">
            <div class="icon"></div>
            <a href="<%= Model.Criteria.GenerateUrl(Model.Presentation.Pagination.Page, Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialCurrent)) %>">Back to videos</a>
        </div>
        <% } %>
    </div>
    <div class="videoarea">
        <div class="topbar"></div>
        <div class="bg">
            <div class="title" title="<%= Model.Resource.Title %>"><%= HighlightTitle(Model.Resource.Title)%></div>
            <div class="divider"></div>
            <div id="ytapiplayer" url="<%= ExternalVideoHelper.GetExternalVideoUrl(Model.Resource.ExternalVideoId) %>">
                You need Flash player 8+ and JavaScript enabled to view this video.
            </div>
        </div>
        <div class="bottombar"></div>
    </div>
    <div class="socialarea">
        <div class="topbar"></div>
        <div class="bg">
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.Viewings[Model.Resource.Id]%></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(Model.Resource, Model.Categories)); %>
            </div>
        </div>
        <div class="bottombar"></div>
    </div>
    <div class="transcriptarea">
        <div class="topbar"></div>
        <div class="bg">
            <div class="title">Transcript</div>
            <div class="content"><%= HighlightContent(Model.Resource.Text)%></div>
        </div>
        <div class="bottombar"></div>
    </div>
    <% if (Model.Criteria != null) { %>
    <div class="back">
        <div class="leftbar"></div>
        <div class="bg">
            <div class="icon"></div>
            <a href="<%= Model.Criteria.GenerateUrl(Model.Presentation.Pagination.Page, Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialCurrent)) %>">Back to videos</a>
        </div>
        <div class="rightbar"></div>
    </div>
    <% } %>
</div>
<% Html.RenderPartial("RelatedContent", Model); %>
<% Html.RenderPartial("RecentlyViewedVideos", Model); %>
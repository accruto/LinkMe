<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceViewUserControl<ResourceModel<QnA>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)Model.Resource)); %>
<% Html.RenderPartial("TabNumbers", Model); %>

<div class="answeredquestion" answeredquestionid="<%= Model.Resource.Id %>" viewurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.ApiViewQnA, new { id = Model.Resource.Id })) %>" url="<%= Model.Resource.GenerateUrl(Model.Categories) %>">
    <div class="toparea">
<%  if (Model.Criteria != null)
    { %>
        <div class="back">
            <div class="icon"></div>
            <a href="<%= Model.Criteria.GenerateUrl(Model.Presentation.Pagination.Page, Model.Categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialCurrent)) %>">Back to questions</a>
        </div>
<%  } %>
    </div>
    <div class="answeredquestionarea">
        <div class="topbar"></div>
        <div class="bg">
            <div class="title" title="<%= Model.Resource.Title %>"><%= HighlightTitle(Model.Resource.Title)%></div>
            <div class="divider"></div>
            <div class="answeredby"></div>
            <div class="content"><%= HighlightContent(Model.Resource.Text)%></div>
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
            <div class="commented">
                <div class="leftbar"></div>
                <div class="number"><%= Model.Comments[Model.Resource.Id]%></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(Model.Resource, Model.Categories)); %>
            </div>
        </div>
        <div class="bottombar"></div>
    </div>
    <div class="disqusarea">
        <div class="topbar"></div>
        <div class="bg">
            <div id="disqus_thread"></div>
            <noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
            <a href="http://disqus.com" class="dsq-brlink">blog comments powered by <span class="logo-disqus">Disqus</span></a>
        </div>
        <div class="bottombar"></div>
    </div>
</div>
<% Html.RenderPartial("RelatedContent", Model); %>
<% Html.RenderPartial("RecentlyViewedQnAs", Model); %>
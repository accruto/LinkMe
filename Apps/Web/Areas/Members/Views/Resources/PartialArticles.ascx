<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceListModel<Article>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)null)); %>
<% Html.RenderPartial("TabNumbers", Model); %>

<div class="articlelist">
<%  if (Model.Results.ResourceIds.Count == 0)
    { %>
        <div class="empty">
            <div class="topbar"></div>
            <div class="bg">
                <div class="text">There are no articles in this category</div>
            </div>
            <div class="bottombar"></div>
        </div>
        <div class="back">
            <div class="leftbar"></div>
            <div class="bg">
                <div class="icon"></div>
                <%= Html.RouteRefLink("Read all articles", ResourcesRoutes.Articles) %>
            </div>
            <div class="rightbar"></div>
        </div>
<%  }
    else
    { %>
        <div class="divider"></div>

<%      foreach (var articleId in Model.Results.ResourceIds)
        {
            Html.RenderPartial("ArticleItem", new ResourceItemModel<Article> { List = Model, Item = Model.Results.Resources[articleId] });
        }
        Html.RenderPartial("Pagination", Tuple.Create((ResourceListModel)Model, Model.TotalResources[ResourceType.Article]));
    } %>
</div>
<%  Html.RenderPartial("RecentlyViewedArticles", Model); %>
<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceListModel<Video>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)null)); %>
<% Html.RenderPartial("TabNumbers", Model); %>

<div class="videolist">
<%  if (Model.Results.ResourceIds.Count == 0)
    { %>
        <div class="empty">
            <div class="topbar"></div>
            <div class="bg">
                <div class="text">You haven't viewed any videos yet</div>
            </div>
            <div class="bottombar"></div>
        </div>
        <div class="back">
            <div class="leftbar"></div>
            <div class="bg">
                <div class="icon"></div>
                <%= Html.RouteRefLink("Watch all videos", ResourcesRoutes.Videos)%>
            </div>
            <div class="rightbar"></div>
        </div>
<%  }
    else
    {
        foreach (var videoId in Model.Results.ResourceIds)
        {
            Html.RenderPartial("VideoItem", new ResourceItemModel<Video> { List = Model, Item = Model.Results.Resources[videoId] });
        }
    } %>
</div>

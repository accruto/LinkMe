<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceListModel<QnA>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

<% Html.RenderPartial("Breadcrumbs", Tuple.Create((ResourceModel)Model, (Resource)null)); %>
<% Html.RenderPartial("TabNumbers", Model); %>

<div class="answeredquestionlist">
                    
<%  if (Model.Results.ResourceIds.Count == 0)
    { %>
    <div class="empty">
        <div class="topbar"></div>
        <div class="bg">
            <div class="text">You haven't viewed any questions yet</div>
        </div>
        <div class="bottombar"></div>
    </div>
    <div class="back">
        <div class="leftbar"></div>
        <div class="bg">
            <div class="icon"></div>
            <%= Html.RouteRefLink("Read all questions", ResourcesRoutes.QnAs)%>
        </div>
        <div class="rightbar"></div>
    </div>
<%  }
    else
    {
        foreach (var qnaId in Model.Results.ResourceIds)
        {
            Html.RenderPartial("QnAItem", new ResourceItemModel<QnA> { List = Model, Item = Model.Results.Resources[qnaId] });
        }
    } %>
</div>

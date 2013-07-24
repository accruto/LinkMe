<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<ResourceModel>" %>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<%  if (Model is ResourceListModel && ((ResourceListModel)Model).TotalResources != null)
    {
        var totalResources = ((ResourceListModel) Model).TotalResources; %>
<div class="tabnumbers">
    <div class="Article"><%= "(" + totalResources[ResourceType.Article] + ")"%></div>
    <div class="Video"><%= "(" + totalResources[ResourceType.Video] + ")"%></div>
    <div class="QnA"><%= "(" + totalResources[ResourceType.QnA] + ")"%></div>
</div>        
<%  }
    else
    { %>
<div class="tabnumbers">
    <div class="Article"></div>
    <div class="Video"></div>
    <div class="QnA"></div>
</div>        
<%  } %>

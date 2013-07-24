<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<Tuple<ResourceModel, Resource>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Resources"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Resources"%>

<%
    var categories = Model.Item1.Categories;
    var item = Model.Item2;
    RouteReference route = null;
    switch(Model.Item1.Criteria.ResourceType)
    {
        case ResourceType.Article:
            route = ResourcesRoutes.Articles;
            break;
            
        case ResourceType.QnA:
            route = ResourcesRoutes.QnAs;
            break;
            
        case ResourceType.Video:
            route = ResourcesRoutes.Videos;
            break;
    }
    if (Model.Item1.Criteria.SubcategoryId != null)
        Model.Item1.Criteria.CategoryId = categories.GetCategoryBySubcategory((Guid)Model.Item1.Criteria.SubcategoryId).Id;
%>
<ul class="breadcrumbs">
    <li class="home"><%= Html.RouteRefLink("Resources", ResourcesRoutes.Resources) %></li>
<%  if (string.IsNullOrEmpty(Model.Item1.Criteria.Keywords))
    { %>
        <li class="itemtype" itemtype="<%= Model.Item1.Criteria.ResourceType %>">
            <%= Html.RouteRefLink(Model.Item1.Criteria.ResourceType == ResourceType.QnA ? "Q & A" : Model.Item1.Criteria.ResourceType + "s", route) %>
        </li>
<%      if (Model.Item1.Criteria.CategoryId != null)
        {
            var category = categories.GetCategory(Model.Item1.Criteria.CategoryId.Value); %>
            <li class="category" categoryid="<%= category.Id %>">
                <a href="<%= category.GenerateUrl(Model.Item1.Criteria.ResourceType) %>"><%= category.Name %></a>
            </li>
<%      }
        if (Model.Item1.Criteria.SubcategoryId != null)
        {
            var subcategory = categories.GetSubcategory(Model.Item1.Criteria.SubcategoryId.Value); %>
            <li class="subcategory" subcategoryid="<%= subcategory.Id %>">
                <a href="<%= subcategory.GenerateUrl(Model.Item1.Criteria.ResourceType, categories) %>"><%= subcategory.Name %></a>
            </li>
<%      }
        if (!(Model.Item1 is ResourceListModel))
        { %>
            <li class="item" title="<%= item.Title %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialArticle, new { id = item.Id })) %>">
                <a href="<%= item.GenerateUrl(categories) %>"><%= item.Title %></a>
            </li>
<%      }
    }
    else
    { %>
        <li class="keywords" keywords="<%= Model.Item1.Criteria.Keywords %>"><div class="icon search"></div><span>Search Result for "<span class="red"><%= Model.Item1.Criteria.Keywords %></span>"</span></li>
        <li class="itemtype" itemtype="<%= Model.Item1.Criteria.ResourceType %>">
            <%= Html.RouteRefLink(Model.Item1.Criteria.ResourceType == ResourceType.QnA ? "Q & A" : Model.Item1.Criteria.ResourceType + "s", route) %>
        </li>
<%      if (Model.Item1.Criteria.CategoryId != null)
        {
            var category = categories.GetCategory(Model.Item1.Criteria.CategoryId.Value); %>
            <li class="category" categoryid="<%= category.Id %>">
                <a href="<%= category.GenerateUrl(Model.Item1.Criteria.ResourceType) %>"><%= category.Name %></a>
            </li>
<%      }
        if (Model.Item1.Criteria.SubcategoryId != null)
        {
            var subcategory = categories.GetSubcategory(Model.Item1.Criteria.SubcategoryId.Value); %>
            <li class="subcategory" subcategoryid="<%= subcategory.Id %>">
                <a href="<%= subcategory.GenerateUrl(Model.Item1.Criteria.ResourceType, categories) %>"><%= subcategory.Name %></a>
            </li>
<%      }
        if (!(Model.Item1 is ResourceListModel))
        { %>
            <li class="item" title="<%= item.Title %>">
                <a href="<%= item.GenerateUrl(categories) %>"><%= item.Title %></a>
            </li>
<%      }
    } %>
</ul>
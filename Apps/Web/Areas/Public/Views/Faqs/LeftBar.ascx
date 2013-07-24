<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FaqListModel>" %>
<%@ Import namespace="LinkMe.Web.Areas.Public.Models.Faqs" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>

<div id="leftbar">
    <div class="title">Support</div>
<%  foreach (var category in Model.Categories)
    { %>
        <div class="subcategories <%= category.Name.Replace(" ", "-").ToLower() %>" id="<%= category.Id %>" <% if (!category.Id.Equals(Model.Criteria.CategoryId)) { %>style="display:none;"<% } %>>
<%      foreach (var subcategory in category.Subcategories)
        { %>
            <a href="<%= subcategory.GenerateUrl() %>" class="<%= "subcategory" + ((string.IsNullOrEmpty(Model.Criteria.Keywords) && subcategory.Id.Equals(Model.Criteria.SubcategoryId)) ? " current" : "") %>" subcategoryid="<%= subcategory.Id %>"><%= subcategory.Name %></a>
<%      } %>
        </div>
<%  } %>
    <div class="bottombar"></div>
    <div class="report">
        <span><a class="link contactus" href="javascript:void(0)">Report</a>a site issue</span>
    </div>
</div>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FaqListModel>" %>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>
<%@ Import namespace="LinkMe.Web.Areas.Public.Models.Faqs" %>

<div class="subcategory-faqlist">
    <div class="title"><%= Model.Categories.GetSubcategory(Model.Criteria.SubcategoryId.Value).Name %></div>
<%  foreach(var faqId in Model.Results.FaqIds)
    {
        var faq = Model.Results.Faqs[faqId]; %>
        <a href="<%= faq.GenerateUrl(Model.Categories) %>" class="faqitem" faqid="<%= faq.Id %>"><%= faq.Title %></a>
<%  } %>
    <div class="bottombar"></div>
</div>
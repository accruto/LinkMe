<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Public.Views.Faqs.FaqsViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes"%>

<%  var faq = Model.Results.Faqs.First().Value;
    var subcategory = Model.Categories.GetSubcategory(Model.Criteria.SubcategoryId.Value); %>

<div class="faq<%= string.IsNullOrEmpty(Model.Criteria.Keywords) ? "" : " search" %>" faqid="<%= faq.Id %>">
    <div class="titlebar">
        <div class="title" title="<%= faq.Title %>"><%= string.IsNullOrEmpty(Model.Criteria.Keywords) ? faq.Title : HighlightTitle(faq.Title)%></div>
<%  if (string.IsNullOrEmpty(Model.Criteria.Keywords))
    { %>
        <a href="<%= subcategory.GenerateUrl() %>" class="back">Back</a>
<%  }
    else
    { %>
        <a href="<%= Html.RouteRefUrl(FaqsRoutes.Search, new { categoryId = Model.Criteria.CategoryId.Value, keywords = Model.Criteria.Keywords }) %>" class="back">Back</a>
<%  } %>
    </div>
    <div class="content">
        <%= string.IsNullOrEmpty(Model.Criteria.Keywords) ? faq.Text : HighlightContent(faq.Text) %>
        <p class="noluck">
            <span>Still no luck?<a class="link contactus" href="javascript:void(0)">Email</a>LinkMe customer support</span>
        </p>
    </div>
    <div class="divider"></div>
    <div class="feedback">
        <div class="helpful">
            <div class="icon"></div>
            <div class="title">Was this article helpful?</div>
            <div class="buttons">
                <div class="button yes eighty"></div>
                <div class="button no"></div>
            </div>
        </div>
        <div class="answer">
            <div class="icon"></div>
            <div class="title">Thank you for your submission</div>
        </div>
    </div>
    <div class="bottombar"></div>
</div>
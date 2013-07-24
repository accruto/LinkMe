<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Members.Views.Resources.ResourceItemViewUserControl<QnA>"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Framework.Utility"%>

<%
    var qna = Model.Item;
    var categories = Model.List.Categories;
%>
<div class="answeredquestionitem">
    <div class="bg">
        <div class="leftside">
            <a class="title" href="<%= qna.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialQnA, new { id = qna.Id })) %>" title="<%= qna.Title %>"><%= HighlightTitle(qna.Title)%></a>
            <div class="categorylist">
                <div class="category"><%= categories.GetCategoryBySubcategory(qna.SubcategoryId).Name%></div>
                <div class="divider"></div>
                <div class="subcategory"><%= categories.GetSubcategory(qna.SubcategoryId).Name%></div>
            </div>
            <div class="divider"></div>
            <div class="content" title="<%= HtmlUtil.StripHtmlTags(qna.Text) %>"><%= SummarizeContent(qna.Text + " ")%></div>
            <div class="readfull">
                <a href="<%= qna.GenerateUrl(categories) %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(ResourcesRoutes.PartialQnA, new { id = qna.Id })) %>">Read full</a>
            </div>
        </div>
        <div class="rightside">
            <div class="viewed">
                <div class="leftbar"></div>
                <div class="number"><%= Model.List.Viewings[qna.Id]%></div>
                <div class="rightbar"></div>
            </div>
            <div class="commented">
                <div class="leftbar"></div>
                <div class="number"><%= Model.List.Comments[qna.Id]%></div>
                <div class="rightbar"></div>
            </div>
            <div class="social">
                <% Html.RenderPartial("Social", new Tuple<Resource, IList<Category>>(qna, categories)); %>
            </div>
        </div>
    </div>
</div>
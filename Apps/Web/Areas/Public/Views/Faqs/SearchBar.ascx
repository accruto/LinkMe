<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FaqListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import namespace="LinkMe.Web.Areas.Public.Models.Faqs" %>

<div id="searchbar">
    <%= Html.DropDownListField(Model.Criteria, "CategoryId", c => c.CategoryId, new[] { (Guid?)null }.Concat(Model.Categories.OrderBy(c => c.DisplayOrder).Select(c => (Guid?)c.Id)))
        .WithText(c => c == null ? "FAQs for ..." : Model.Categories.GetCategory(c.Value).Name)
        .WithLabel("")
        .WithAttribute("size", "4") %>
    <%= Html.TextBoxField("Keywords", Model.Criteria.Keywords)
        .WithAttribute("data-watermark", "Type your question here to search for FAQs")
        .WithLabel("") %>
    <div class="button search faq"></div>
</div>


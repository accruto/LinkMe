<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Public.Models.Faqs.FaqListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Domain.Resources" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Routes" %>

<script type="text/javascript">
    $(document).ready(function () {

        linkme.support.contactus.init({
            urls: {
                partialContactUs: "<%= SupportRoutes.ContactUsPartial.GenerateUrl() %>",
                apiSendContactUs: "<%= SupportRoutes.ApiSendContactUs.GenerateUrl() %>"
            },
            userType: "<%= Model.UserType %>",
            subcategoryId: "<%= Model.Criteria.SubcategoryId != null ? Model.Criteria.SubcategoryId.Value.ToString() : "" %>"
        });

        linkme.support.faqs.init({
            urls: {
                partialFaqUrl: "<%= FaqsRoutes.PartialFaq.GenerateUrl() %>",
                partialSubcategoryUrl: "<%= FaqsRoutes.PartialSubcategory.GenerateUrl() %>",
                partialSearchUrl: "<%= FaqsRoutes.PartialSearch.GenerateUrl() %>",
                hashUrl: "<%= FaqsRoutes.Hash.GenerateUrl() %>",
                apiMarkHelpful: "<%= FaqsRoutes.ApiMarkHelpful.GenerateUrl() %>",
                apiMarkNotHelpful: "<%= FaqsRoutes.ApiMarkNotHelpful.GenerateUrl() %>"
            },
            criteria: {
                category: {
                    id: "<%= Model.Criteria.CategoryId %>"
                },
                subcategory: {
                    id: "<%= Model.Criteria.SubcategoryId != null ? Model.Criteria.SubcategoryId.Value.ToString() : "" %>",
                    name: "<%= Model.Criteria.SubcategoryId != null ? Model.Categories.GetSubcategory(Model.Criteria.SubcategoryId.Value).Name : "" %>"
                },
                keywords: "<%= Model.Criteria.Keywords %>",
                faq: {
                    id: "<%= Model.IsSingleFaq ? Model.Results.Faqs.Values.First().Id.ToString() : "" %>",
                    title: "<%= Model.IsSingleFaq ? HttpUtility.HtmlDecode(Model.Results.Faqs.Values.First().Title) : "" %>"
                }
            }
        });

    });
</script>



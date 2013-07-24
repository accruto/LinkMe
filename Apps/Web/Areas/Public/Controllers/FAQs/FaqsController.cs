using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Query.Search.Resources;
using LinkMe.Query.Search.Resources.Commands;
using LinkMe.Web.Areas.Members.Controllers;
using LinkMe.Web.Areas.Public.Models.Faqs;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Public.Controllers.Faqs
{
    public class FaqsController
        : MembersController
    {
        private readonly IExecuteFaqSearchCommand _executeFaqSearchCommand;
        private readonly IFaqsQuery _faqsQuery;

        private const int TopFaqsCount = 5;
        private const string CandidatesCategory = "Candidates";
        private const string EmployersCategory = "Employers";
        private const string SecurityCategory = "Online Security";
        private const string DefaultCategory = CandidatesCategory;

        public FaqsController(IExecuteFaqSearchCommand executeFaqSearchCommand, IFaqsQuery faqsQuery)
        {
            _executeFaqSearchCommand = executeFaqSearchCommand;
            _faqsQuery = faqsQuery;
        }

        [HttpGet]
        public ActionResult Faqs()
        {
            var categories = _faqsQuery.GetCategories();

            return View(new FaqsModel
            {
                Categories = _faqsQuery.GetCategories(),
                TopCandidateFaqs = _faqsQuery.GetHelpfulFaqs(categories.Single(c => c.Name == CandidatesCategory).Id, TopFaqsCount),
                TopEmployerFaqs = _faqsQuery.GetHelpfulFaqs(categories.Single(c => c.Name == EmployersCategory).Id, TopFaqsCount),
                CandidatesSubcategory = categories.Single(c => c.Name == CandidatesCategory).Subcategories[0],
                EmployersSubcategory = categories.Single(c => c.Name == EmployersCategory).Subcategories[0],
                SecuritySubcategory = categories.Single(c => c.Name == SecurityCategory).Subcategories[0],
            });
        }

        [HttpGet]
        public ActionResult Subcategory(Guid subcategoryId)
        {
            var faqList = GetFaqList(subcategoryId);
            return faqList == null
                ? RedirectToRoute(FaqsRoutes.Faqs)
                : View(faqList);
        }

        [HttpGet]
        public ActionResult PartialSubcategory(Guid subcategoryId)
        {
            var faqList = GetFaqList(subcategoryId);
            return faqList == null
                ? (ActionResult)RedirectToRoute(FaqsRoutes.Faqs)
                : PartialView("SubcategoryFaqList", faqList);
        }

        [HttpGet]
        public ActionResult Faq(Guid id, string keywords)
        {
            var faqList = GetFaq(id, keywords);
            return faqList == null
                ? NotFound("faq", "id", id)
                : View(faqList);
        }

        [HttpGet]
        public ActionResult PartialFaq(Guid id, string keywords)
        {
            var faqList = GetFaq(id, keywords);
            return faqList == null
                ? NotFound("faq", "id", id)
                : PartialView("FaqItem", faqList);
        }

        [HttpGet]
        public ActionResult Search(Guid? categoryId, string keywords)
        {
            return View(GetFaqList(categoryId, keywords));
        }

        [HttpGet]
        public ActionResult PartialSearch(Guid? categoryId, string keywords)
        {
            return PartialView("SearchFaqList", GetFaqList(categoryId, keywords));
        }

        [HttpGet]
        public ActionResult Hash(Guid? faqId, Guid? categoryId, Guid? subcategoryId, string keywords)
        {
            // Faq.

            if (faqId != null)
            {
                var faq = _faqsQuery.GetFaq(faqId.Value);
                if (faq != null)
                {
                    var url = faq.GenerateUrl(_faqsQuery.GetCategories()).AsNonReadOnly();
                    if (!string.IsNullOrEmpty(keywords))
                        url.QueryString["keywords"] = keywords;
                    return RedirectToUrl(url);
                }
            }

            // Subcategory.

            if (subcategoryId != null)
            {
                var subcategory = _faqsQuery.GetSubcategory(subcategoryId.Value);
                if (subcategory != null)
                    return RedirectToUrl(subcategory.GenerateUrl());
            }

            // Search.

            if (!string.IsNullOrEmpty(keywords))
                return RedirectToRoute(FaqsRoutes.Search, new { categoryId, keywords });

            // If nothing then redirect to the faqs page.

            return RedirectToRoute(FaqsRoutes.Faqs);
        }

        [HttpGet]
        public ActionResult OldSubcategoryFaqs(string subcategory, string subcategoryId)
        {
            // Some bots have urls without ids. Simply redirect back to to FAQs page.

            Guid result;
            return Guid.TryParse(subcategoryId, out result)
                ? RedirectToUrl(FaqsRoutes.Subcategory.GenerateUrl(new { subcategory, subcategoryId = result }), true)
                : RedirectToUrl(FaqsRoutes.Faqs.GenerateUrl(), true);
        }

        private FaqListModel GetFaq(Guid id, string keywords)
        {
            var faq = _faqsQuery.GetFaq(id);
            if (faq == null)
                return null;

            var categories = _faqsQuery.GetCategories();
            var criteria = new FaqSearchCriteria
            {
                SubcategoryId = faq.SubcategoryId,
                CategoryId = categories.GetCategoryBySubcategory(faq.SubcategoryId).Id,
                Keywords = keywords,
            };

            return new FaqListModel
            {
                Categories = categories,
                Criteria = criteria,
                UserType = GetUserType(categories, criteria.CategoryId),
                IsSingleFaq = true,
                Results = new FaqListResultsModel
                {
                    FaqIds = new[] { faq.Id },
                    Faqs = new[] { faq }.ToDictionary(f => f.Id, f => f),
                    TotalFaqs = 1,
                },
            };
        }

        private static UserType GetUserType(IEnumerable<Category> categories, Guid? categoryId)
        {
            if (categoryId == null)
                return UserType.Member;
            return categories.First(c => c.Id == categoryId.Value).Name == "Employers"
                ? UserType.Employer
                : UserType.Member;
        }

        private FaqListModel GetFaqList(Guid subcategoryId)
        {
            var categories = _faqsQuery.GetCategories();
            var category = categories.GetCategoryBySubcategory(subcategoryId);
            if (category == null)
                return null;

            var criteria = new FaqSearchCriteria
            {
                SubcategoryId = subcategoryId,
                CategoryId = category.Id,
            };

            return GetFaqList(criteria, categories);
        }

        private FaqListModel GetFaqList(Guid? categoryId, string keywords)
        {
            var categories = _faqsQuery.GetCategories();
            var category = (categoryId == null ? null : categories.GetCategory(categoryId.Value))
                ?? categories.Single(c => c.Name == DefaultCategory);

            var criteria = new FaqSearchCriteria
            {
                CategoryId = category.Id,
                Keywords = keywords
            };

            return GetFaqList(criteria, categories);
        }

        private FaqListModel GetFaqList(FaqSearchCriteria criteria, IList<Category> categories)
        {
            var execution = _executeFaqSearchCommand.Search(criteria, null);

            return new FaqListModel
            {
                Categories = categories,
                Criteria = execution.Criteria,
                UserType = GetUserType(categories, criteria.CategoryId),
                IsSingleFaq = false,
                Results = new FaqListResultsModel
                {
                    TotalFaqs = execution.Results.ResourceIds.Count,
                    FaqIds = execution.Results.ResourceIds,
                    Faqs = _faqsQuery.GetFaqs(execution.Results.ResourceIds).ToDictionary(f => f.Id, f => f),
                },
            };
        }
    }
}
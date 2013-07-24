using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Query.Search.Resources;

namespace LinkMe.Web.Areas.Public.Models.Faqs
{
    public class FaqsModel
    {
        public IList<Faq> TopCandidateFaqs { get; set; }
        public IList<Faq> TopEmployerFaqs { get; set; }
        public IList<Category> Categories { get; set; }
        public Subcategory CandidatesSubcategory { get; set; }
        public Subcategory EmployersSubcategory { get; set; }
        public Subcategory SecuritySubcategory { get; set; }
    }

    public class FaqListResultsModel
    {
        public int TotalFaqs { get; set; }
        public IList<Guid> FaqIds { get; set; }
        public IDictionary<Guid, Faq> Faqs { get; set; }
    }

    public class FaqListModel
    {
        public IList<Category> Categories { get; set; }
        public FaqSearchCriteria Criteria { get; set; }
        public UserType UserType { get; set; }
        public bool IsSingleFaq { get; set; }
        public FaqListResultsModel Results { get; set; }
    }
}

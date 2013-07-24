using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Resources.Queries
{
    public class FaqsQuery
        : IFaqsQuery
    {
        private readonly IResourcesRepository _repository;

        public FaqsQuery(IResourcesRepository repository)
        {
            _repository = repository;
        }

        IList<Category> IFaqsQuery.GetCategories()
        {
            return _repository.GetFaqCategories();
        }

        Category IFaqsQuery.GetCategory(UserType userType)
        {
            return _repository.GetFaqCategory(userType);
        }

        Subcategory IFaqsQuery.GetSubcategory(Guid id)
        {
            return _repository.GetFaqSubcategory(id);
        }

        Faq IFaqsQuery.GetFaq(Guid id)
        {
            return _repository.GetFaq(id);
        }

        IList<Faq> IFaqsQuery.GetFaqs()
        {
            return _repository.GetFaqs();
        }

        IList<Faq> IFaqsQuery.GetFaqs(IEnumerable<Guid> ids)
        {
            return _repository.GetFaqs(ids);
        }

        IList<Faq> IFaqsQuery.GetHelpfulFaqs(Guid categoryId, int count)
        {
            return _repository.GetHelpfulFaqs(categoryId, count);
        }
    }
}

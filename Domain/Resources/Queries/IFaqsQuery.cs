using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Resources.Queries
{
    public interface IFaqsQuery
    {
        IList<Category> GetCategories();
        Category GetCategory(UserType userType);
        Subcategory GetSubcategory(Guid id);

        Faq GetFaq(Guid id);
        IList<Faq> GetFaqs();
        IList<Faq> GetFaqs(IEnumerable<Guid> ids);
        IList<Faq> GetHelpfulFaqs(Guid categoryId, int count);
    }
}

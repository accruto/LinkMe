using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;

namespace LinkMe.Query.Search.Employers
{
    public abstract class EmployerSearchCriteria
        : Criteria
    {
        protected EmployerSearchCriteria(IDictionary<string, CriteriaDescription> descriptions)
            : base(descriptions)
        {
        }

        protected override void OnCloned()
        {
            base.OnCloned();

            // Reset the id so that the clone is not identified as the old instance.

            Id = Guid.Empty;
        }
    }
}
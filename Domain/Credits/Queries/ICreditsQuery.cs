using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Queries
{
    public interface ICreditsQuery
    {
        IList<Credit> GetCredits();
        Credit GetCredit(Guid id);
        T GetCredit<T>() where T : Credit;
        Credit GetCreditByName(string name);
    }
}
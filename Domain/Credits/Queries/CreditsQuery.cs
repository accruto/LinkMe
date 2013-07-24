using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LinkMe.Domain.Credits.Queries
{
    public class CreditsQuery
        : ICreditsQuery
    {
        private readonly ICreditsRepository _repository;
        private readonly IList<Credit> _credits;
        private readonly IDictionary<Guid, Credit> _creditsById;
        private readonly IDictionary<string, Credit> _creditsByName;
        private readonly IDictionary<Type, Credit> _creditsByType;

        public CreditsQuery(ICreditsRepository repository)
        {
            _repository = repository;
            _credits = _repository.GetCredits();
            _creditsById = _credits.ToDictionary(c => c.Id);
            _creditsByName = _credits.ToDictionary(c => c.Name);
            _creditsByType = _credits.ToDictionary(c => c.GetType());
        }

        IList<Credit> ICreditsQuery.GetCredits()
        {
            return _credits;
        }

        Credit ICreditsQuery.GetCredit(Guid id)
        {
            Credit credit;
            _creditsById.TryGetValue(id, out credit);
            return credit;
        }

        Credit ICreditsQuery.GetCreditByName(string name)
        {
            Credit credit;
            _creditsByName.TryGetValue(name, out credit);
            return credit;
        }

        T ICreditsQuery.GetCredit<T>()
        {
            return GetCredit<T>();
        }

        private T GetCredit<T>()
            where T : Credit
        {
            Credit credit;
            _creditsByType.TryGetValue(typeof (T), out credit);
            return credit as T;
        }
    }
}
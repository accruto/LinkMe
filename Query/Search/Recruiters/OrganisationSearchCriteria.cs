using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;

namespace LinkMe.Query.Search.Recruiters
{
    public class OrganisationSearchCriteria
        : Criteria
    {
        private const string FullNameName = "FullName";
        private const string MatchFullNameExactlyName = "MatchFullNameExactly";
        private const string AccountManagerIdName = "AccountManagerId";
        private const string VerifiedOrganisationsName = "VerifiedOrganisations";
        private const string UnverifiedOrganisationsName = "UnverifiedOrganisations";

        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>
                                                                                             {
                                                                                                 {FullNameName, new CriteriaValueDescription<string>(null)},
                                                                                                 {MatchFullNameExactlyName, new CriteriaValueDescription<bool>(false)},
                                                                                                 {AccountManagerIdName, new CriteriaValueDescription<Guid?>(null)},
                                                                                                 {VerifiedOrganisationsName, new CriteriaValueDescription<bool>(false)},
                                                                                                 {UnverifiedOrganisationsName, new CriteriaValueDescription<bool>(false)},
                                                                                             };

        public OrganisationSearchCriteria()
            : base(_descriptions)
        {
        }

        public string FullName
        {
            get { return GetValue<string>(FullNameName); }
            set { SetValue(FullNameName, value); }
        }

        public bool MatchFullNameExactly
        {
            get { return GetValue<bool>(MatchFullNameExactlyName); }
            set { SetValue(MatchFullNameExactlyName, value); }
        }
        
        public Guid? AccountManagerId
        {
            get { return GetValue<Guid?>(AccountManagerIdName); }
            set { SetValue(AccountManagerIdName, value); }
        }

        public bool VerifiedOrganisations
        {
            get { return GetValue<bool>(VerifiedOrganisationsName); }
            set { SetValue(VerifiedOrganisationsName, value); }
        }

        public bool UnverifiedOrganisations
        {
            get { return GetValue<bool>(UnverifiedOrganisationsName); }
            set { SetValue(UnverifiedOrganisationsName, value); }
        }
    }
}
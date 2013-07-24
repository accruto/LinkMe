using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility;

namespace LinkMe.Query.Search.Employers
{
    public class OrganisationEmployerSearchCriteria
        : EmployerSearchCriteria
    {
        private const string EmployersName = "Employers";
        private const string RecruitersName = "Recruiters";
        private const string VerifiedOrganisationsName = "VerifiedOrganisations";
        private const string UnverifiedOrganisationsName = "UnverifiedOrganisations";
        private const string IndustriesName = "Industries";
        private const string MinimumLoginsName = "MinimumLogins";
        private const string MaximumLoginsName = "MaximumLogins";

        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>
        {
            { EmployersName, new CriteriaValueDescription<bool>(true) },
            { RecruitersName, new CriteriaValueDescription<bool>(true) },
            { VerifiedOrganisationsName, new CriteriaValueDescription<bool>(true) },
            { UnverifiedOrganisationsName, new CriteriaValueDescription<bool>(true) },
            { IndustriesName, new CriteriaListDescription<Guid>(g => g.ToString(), s => new Guid(s)) },
            { MinimumLoginsName, new CriteriaValueDescription<int?>(null) },
            { MaximumLoginsName, new CriteriaValueDescription<int?>(null) },
        };

        public OrganisationEmployerSearchCriteria()
            : base(_descriptions)
        {
        }

        public bool Employers
        {
            get { return GetValue<bool>(EmployersName); }
            set { SetValue(EmployersName, value); }
        }

        public bool Recruiters
        {
            get { return GetValue<bool>(RecruitersName); }
            set { SetValue(RecruitersName, value); }
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

        public IList<Guid> IndustryIds
        {
            get { return GetValue<IList<Guid>>(IndustriesName); }
            set { SetValue(IndustriesName, value); }
        }

        public int? MinimumLogins
        {
            get { return GetValue<int?>(MinimumLoginsName); }
            set { SetValue(MinimumLoginsName, value); }
        }

        public int? MaximumLogins
        {
            get { return GetValue<int?>(MaximumLoginsName); }
            set { SetValue(MaximumLoginsName, value); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as OrganisationEmployerSearchCriteria;
            if (other == null)
                return false;

            return GetType() == other.GetType()
                   && Equals(MinimumLogins, other.MinimumLogins)
                   && Equals(MaximumLogins, other.MaximumLogins)
                   && Equals(VerifiedOrganisations, other.VerifiedOrganisations)
                   && Equals(UnverifiedOrganisations, other.UnverifiedOrganisations)
                   && Equals(Employers, other.Employers)
                   && Equals(Recruiters, other.Recruiters)
                   && IndustryIds.NullableCollectionEqual(other.IndustryIds);
        }

        public override int GetHashCode()
        {
            return new object[] {MinimumLogins, MaximumLogins}.GetCollectionHashCode()
                   ^ IndustryIds.GetCollectionHashCode()
                   ^ Employers.GetHashCode()
                   ^ Recruiters.GetHashCode()
                   ^ VerifiedOrganisations.GetHashCode()
                   ^ UnverifiedOrganisations.GetHashCode();
        }
    }
}
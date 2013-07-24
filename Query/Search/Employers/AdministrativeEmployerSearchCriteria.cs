using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility;

namespace LinkMe.Query.Search.Employers
{
    public class AdministrativeEmployerSearchCriteria
        : EmployerSearchCriteria
    {
        private const string OrganisationNameName = "OrganisationName";
        private const string MatchOrganisationNameExactlyName = "MatchOrganisationNameExactly";
        private const string LoginIdName = "LoginId";
        private const string FirstNameName = "FirstName";
        private const string LastNameName = "LastName";
        private const string EmailAddressName = "EmailAddress";
        private const string IsEnabledName = "IsEnabled";
        private const string IsDisabledName = "IsDisabled";
        private const string CountName = "Count";
        private const string SortOrderName = "SortOrder";

        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>
        {
            { OrganisationNameName, new CriteriaValueDescription<string>(null) },
            { MatchOrganisationNameExactlyName, new CriteriaValueDescription<bool>(false) },
            { LoginIdName, new CriteriaValueDescription<string>(null) },
            { FirstNameName, new CriteriaValueDescription<string>(null) },
            { LastNameName, new CriteriaValueDescription<string>(null) },
            { EmailAddressName, new CriteriaValueDescription<string>(null) },
            { IsEnabledName, new CriteriaValueDescription<bool>(false) },
            { IsDisabledName, new CriteriaValueDescription<bool>(false) },
            { CountName, new CriteriaValueDescription<int?>(null) },
            { SortOrderName, new CriteriaValueDescription<EmployerSortOrder>(EmployerSortOrder.LoginId, new EnumValuePersister<EmployerSortOrder>()) },
        };

        public AdministrativeEmployerSearchCriteria()
            : base(_descriptions)
        {
        }

        public string OrganisationName
        {
            get { return GetValue<string>(OrganisationNameName); }
            set { SetValue(OrganisationNameName, value); }
        }

        public bool MatchOrganisationNameExactly
        {
            get { return GetValue<bool>(MatchOrganisationNameExactlyName); }
            set { SetValue(MatchOrganisationNameExactlyName, value); }
        }

        public string LoginId
        {
            get { return GetValue<string>(LoginIdName); }
            set { SetValue(LoginIdName, value); }
        }

        public string FirstName
        {
            get { return GetValue<string>(FirstNameName); }
            set { SetValue(FirstNameName, value); }
        }

        public string LastName
        {
            get { return GetValue<string>(LastNameName); }
            set { SetValue(LastNameName, value); }
        }

        public string EmailAddress
        {
            get { return GetValue<string>(EmailAddressName); }
            set { SetValue(EmailAddressName, value); }
        }

        public bool IsEnabled
        {
            get { return GetValue<bool>(IsEnabledName); }
            set { SetValue(IsEnabledName, value); }
        }

        public bool IsDisabled
        {
            get { return GetValue<bool>(IsDisabledName); }
            set { SetValue(IsDisabledName, value); }
        }

        public int? Count
        {
            get { return GetValue<int?>(CountName); }
            set { SetValue(CountName, value); }
        }

        public EmployerSortOrder SortOrder
        {
            get { return GetValue<EmployerSortOrder>(SortOrderName); }
            set { SetValue(SortOrderName, value); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as AdministrativeEmployerSearchCriteria;
            if (other == null)
                return false;

            return GetType() == other.GetType()
                   && Equals(OrganisationName, other.OrganisationName)
                   && Equals(MatchOrganisationNameExactly, other.MatchOrganisationNameExactly)
                   && Equals(LoginId, other.LoginId)
                   && Equals(FirstName, other.FirstName)
                   && Equals(LastName, other.LastName)
                   && Equals(EmailAddress, other.EmailAddress)
                   && IsEnabled == other.IsEnabled
                   && IsDisabled == other.IsDisabled
                   && Equals(Count, other.Count)
                   && SortOrder == other.SortOrder;
        }

        public override int GetHashCode()
        {
            return new object[] { OrganisationName, LoginId, FirstName, LastName, EmailAddress, Count }.GetCollectionHashCode()
                   ^ MatchOrganisationNameExactly.GetHashCode()
                   ^ IsEnabled.GetHashCode()
                   ^ IsDisabled.GetHashCode()
                   ^ SortOrder.GetHashCode();
        }
    }
}
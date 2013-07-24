using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility;

namespace LinkMe.Query.Search.Members
{
    public class AdministrativeMemberSearchCriteria
        : Criteria
    {
        private const string FirstNameName = "FirstName";
        private const string LastNameName = "LastName";
        private const string EmailAddressName = "EmailAddress";
        private const string CountName = "Count";

        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>
        {
            { FirstNameName, new CriteriaValueDescription<string>(null) },
            { LastNameName, new CriteriaValueDescription<string>(null) },
            { EmailAddressName, new CriteriaValueDescription<string>(null) },
            { CountName, new CriteriaValueDescription<int?>(20) },
        };

        public AdministrativeMemberSearchCriteria()
            : base(_descriptions)
        {
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

        public int? Count
        {
            get { return GetValue<int?>(CountName); }
            set { SetValue(CountName, value); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as AdministrativeMemberSearchCriteria;
            if (other == null)
                return false;

            return GetType() == other.GetType()
                   && Equals(FirstName, other.FirstName)
                   && Equals(LastName, other.LastName)
                   && Equals(EmailAddress, other.EmailAddress)
                   && Equals(Count, other.Count);
        }

        public override int GetHashCode()
        {
            return new object[] { FirstName, LastName, EmailAddress, Count }.GetCollectionHashCode();
        }
    }
}
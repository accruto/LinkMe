using System;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Linq;

namespace LinkMe.Domain.Users.Members.Views
{
    public class PersonalView
        : IMember, ICommunicationUser
    {
        private readonly Member _member;
        private readonly PersonalContactDegree _effectiveContactDegree;
        private readonly PersonalContactDegree _actualContactDegree;

        public static PersonalView DefaultView = new PersonalView(new Member(), PersonalContactDegree.Anonymous, PersonalContactDegree.Anonymous);

        public PersonalView(Member member, PersonalContactDegree effectiveContactDegree, PersonalContactDegree actualContactDegree)
        {
            _member = member;
            _effectiveContactDegree = effectiveContactDegree;
            _actualContactDegree = actualContactDegree;
        }

        public PersonalContactDegree EffectiveContactDegree
        {
            get { return _effectiveContactDegree; }
        }

        public PersonalContactDegree ActualContactDegree
        {
            get { return _actualContactDegree; }
        }

        public Guid Id
        {
            get { return _member.Id; }
        }

        public string FirstName
        {
            get { return GetFirstName(false); }
        }

        public string LastName
        {
            get { return GetLastName(false); }
        }

        public string FullName
        {
            get { return GetFullName(false); }
        }

        public ReadOnlyCollection<EmailAddress> EmailAddresses
        {
            get { return CanAccess(PersonalVisibility.EmailAddress) ? _member.EmailAddresses.ToReadOnlyCollection() : null; }
        }

        public Guid? AffiliateId
        {
            get { return null; }
        }

        string ICommunicationRecipient.FirstName
        {
            get { return GetFirstName(true); }
        }

        string ICommunicationRecipient.LastName
        {
            get { return GetLastName(true); }
        }

        string ICommunicationRecipient.FullName
        {
            get { return GetFullName(true); }
        }

        string ICommunicationRecipient.EmailAddress
        {
            get { return ((ICommunicationRecipient)_member).EmailAddress; }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get { return ((ICommunicationUser)_member).IsEmailAddressVerified; }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return _member.AffiliateId; }
        }

        public ReadOnlyCollection<PhoneNumber> PhoneNumbers
        {
            get
            {
                // If the phone numbers can't be accessed then hide the number.

                var phoneNumbers = ((IMember)_member).PhoneNumbers;
                return CanAccess(PersonalVisibility.PhoneNumbers)
                    ? phoneNumbers
                    : phoneNumbers == null
                        ? null
                        : (from p in phoneNumbers select new PhoneNumber { Number = null, Type = p.Type }).ToReadOnlyCollection();
            }
        }

        public Gender Gender
        {
            get { return CanAccess(PersonalVisibility.Gender) ? _member.Gender : Gender.Unspecified; }
        }

        public PartialDate? DateOfBirth
        {
            get { return CanAccess(PersonalVisibility.Age) ? _member.DateOfBirth : null; }
        }

        public Address Address
        {
            get { return _member.Address; }
        }

        public Guid? PhotoId
        {
            get { return CanAccess(PersonalVisibility.Photo) ? _member.PhotoId : null;  }
        }

        public VisibilitySettings VisibilitySettings
        {
            get { return _member.VisibilitySettings; }
        }

        public EthnicStatus EthnicStatus
        {
            get { return default(EthnicStatus); }
        }

        public UserType UserType
        {
            get { return _member.UserType; }
        }

        DateTime IUserAccount.CreatedTime
        {
            get { return _member.CreatedTime; }
        }

        DateTime IMember.LastUpdatedTime
        {
            get { return _member.LastUpdatedTime; }
        }

        public bool IsEnabled
        {
            get { return _member.IsEnabled; }
        }

        public bool IsActivated
        {
            get { return _member.IsActivated; }
        }

        public bool CanAccess(PersonalVisibility visibility)
        {
            return CanAccess(_effectiveContactDegree, _member.VisibilitySettings.Personal, visibility);
        }

        internal static bool CanAccess(PersonalContactDegree contactDegree, PersonalVisibilitySettings visibilitySettings, PersonalVisibility visibility)
        {
            var contactVisibility = visibilitySettings == null ? GetAnonymousVisibility(contactDegree) : GetVisibility(contactDegree, visibilitySettings);
            return contactVisibility.IsFlagSet(visibility);
        }

        private static PersonalVisibility GetVisibility(PersonalContactDegree contactDegree, PersonalVisibilitySettings visibilitySettings)
        {
            switch (contactDegree)
            {
                case PersonalContactDegree.Self:
                case PersonalContactDegree.Representee:

                    // A representative of a representee can always see everything of that representee.

                    return PersonalVisibility.All;

                case PersonalContactDegree.Representative:

                    // A representee can always see the name of their representative.

                    return visibilitySettings.FirstDegreeVisibility | PersonalVisibility.Name;

                case PersonalContactDegree.FirstDegree:
                    return visibilitySettings.FirstDegreeVisibility;

                case PersonalContactDegree.SecondDegree:
                    return visibilitySettings.SecondDegreeVisibility;

                case PersonalContactDegree.Public:
                    return visibilitySettings.PublicVisibility;
                
                default:
                    return visibilitySettings.PublicVisibility & PersonalVisibilitySettings.Anonymous;
            }
        }

        private static PersonalVisibility GetAnonymousVisibility(PersonalContactDegree contactDegree)
        {
            switch (contactDegree)
            {
                case PersonalContactDegree.Self:
                    return PersonalVisibility.All;

                default:
                    return PersonalVisibilitySettings.InvisiblePublic;
            }
        }

        private string GetFirstName(bool ignoreSettings)
        {
            return ignoreSettings
                ? _member.FirstName
                : CanAccess(PersonalVisibility.Name) ? _member.FirstName : null;
        }

        private string GetLastName(bool ignoreSettings)
        {
            return ignoreSettings
                ? _member.LastName
                : CanAccess(PersonalVisibility.Name) ? _member.LastName : null;
        }

        private string GetFullName(bool ignoreSettings)
        {
            return ignoreSettings
                ? _member.FullName
                : CanAccess(PersonalVisibility.Name) ? _member.FullName : null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinkMe.Domain;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails
{
    public class EmailUser
        : IRegisteredUser, ICommunicationUser
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _emailAddress;
        private readonly UserType _userType = UserType.Anonymous;

        public EmailUser(string emailAddress, string firstName, string lastName, UserType userType)
            : this(emailAddress, firstName, lastName)
        {
            _userType = userType;
        }

        public EmailUser(string emailAddress, string firstName, string lastName)
        {
            _emailAddress = emailAddress;
            _firstName = firstName;
            _lastName = lastName;
        }

        public EmailUser(string emailAddress)
            : this(emailAddress, null, null)
        {
        }

        Guid IHasId<Guid>.Id
        {
            get { return _id; }
        }

        string ICommunicationRecipient.FirstName
        {
            get { return _firstName; }
        }

        string ICommunicationRecipient.LastName
        {
            get { return _lastName; }
        }

        string ICommunicationRecipient.FullName
        {
            get { return GetFullName(); }
        }

        string ICommunicationRecipient.EmailAddress
        {
            get { return _emailAddress; }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return null; }
        }

        string IRegisteredUser.FirstName
        {
            get { return _firstName; }
        }

        string IRegisteredUser.LastName
        {
            get { return _lastName; }
        }

        string IRegisteredUser.FullName
        {
            get { return GetFullName(); }
        }

        UserType IUserAccount.UserType
        {
            get { return UserType.Anonymous; }
        }

        bool ICommunicationUser.IsEnabled
        {
            get { return true; }
        }

        bool ICommunicationUser.IsActivated
        {
            get { return true; }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get { return true; }
        }

        Guid? IRegisteredUser.AffiliateId
        {
            get { return null; }
        }

        DateTime IUserAccount.CreatedTime
        {
            get { return DateTime.MinValue; }
        }

        UserType ICommunicationUser.UserType
        {
            get { return _userType; }
        }

        bool IUserAccount.IsEnabled
        {
            get { return true; }
        }

        bool IUserAccount.IsActivated
        {
            get { return true; }
        }

        private string GetFullName()
        {
            if (_firstName == null)
                return null;
            return _lastName == null ? _firstName : _firstName.CombineLastName(_lastName);
        }
    }

    internal abstract class OverrideEmailUser
        : IRegisteredUser, ICommunicationRecipient
    {
        private readonly IRegisteredUser _user;
        private readonly EmailAddress _emailAddress;

        protected OverrideEmailUser(IRegisteredUser user, EmailAddress emailAddress)
        {
            _user = user;
            _emailAddress = emailAddress;
        }

        Guid IHasId<Guid>.Id
        {
            get { return _user.Id; }
        }

        string IRegisteredUser.FirstName
        {
            get { return _user.FirstName; }
        }

        string IRegisteredUser.LastName
        {
            get { return _user.LastName; }
        }

        string IRegisteredUser.FullName
        {
            get { return _user.FullName; }
        }

        UserType IUserAccount.UserType
        {
            get { return _user.UserType; }
        }

        Guid? IRegisteredUser.AffiliateId
        {
            get { return _user.AffiliateId; }
        }

        DateTime IUserAccount.CreatedTime
        {
            get { return _user.CreatedTime; }
        }

        bool IUserAccount.IsEnabled
        {
            get { return _user.IsEnabled; }
        }

        bool IUserAccount.IsActivated
        {
            get { return _user.IsActivated; }
        }

        string ICommunicationRecipient.LastName
        {
            get { return _user.LastName; }
        }

        string ICommunicationRecipient.FullName
        {
            get { return _user.FullName; }
        }

        string ICommunicationRecipient.FirstName
        {
            get { return _user.FirstName; }
        }

        string ICommunicationRecipient.EmailAddress
        {
            get { return _emailAddress == null ? null : _emailAddress.Address; }
        }

        protected EmailAddress GetEmailAddress()
        {
            return _emailAddress;   
        }
    }

    internal class EmailEmployer
        : OverrideEmailUser, IEmployer, ICommunicationUser
    {
        private readonly IEmployer _employer;

        public EmailEmployer(IEmployer employer, string emailAddress)
            : base(employer, string.IsNullOrEmpty(emailAddress) ? employer.EmailAddress : new EmailAddress { Address = emailAddress, IsVerified = true })
        {
            _employer = employer;
        }

        public EmailAddress EmailAddress
        {
            get { return GetEmailAddress(); }
        }

        public PhoneNumber PhoneNumber
        {
            get { return _employer.PhoneNumber; }
        }

        public IOrganisation Organisation
        {
            get { return _employer.Organisation; }
        }

        public IList<Industry> Industries
        {
            get { return _employer.Industries; }
        }

        UserType ICommunicationUser.UserType
        {
            get { return _employer.UserType; }
        }

        bool ICommunicationUser.IsEnabled
        {
            get { return _employer.IsEnabled; }
        }

        bool ICommunicationUser.IsActivated
        {
            get { return _employer.IsActivated; }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get { return (_employer.EmailAddress != null && _employer.EmailAddress.IsVerified); }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return _employer.Organisation.AffiliateId; }
        }
    }

    internal class EmailMember
        : OverrideEmailUser, IMember, ICommunicationUser
    {
        private readonly IMember _member;
        private readonly EmailAddress _emailaddress;

        public EmailMember(IMember member, EmailAddress emailAddress)
            : base(member, emailAddress ?? member.GetBestEmailAddress() )
        {
            _member = member;
            _emailaddress = emailAddress ?? member.GetBestEmailAddress();
        }

        public EmailAddress GetBestEmailAddress()
        {
            return GetEmailAddress();
        }

        public PhoneNumber GetBestPhoneNumber()
        {
            return _member.GetBestPhoneNumber();
        }

        public PhoneNumber GetPrimaryPhoneNumber()
        {
            return _member.GetPrimaryPhoneNumber();
        }

        public PhoneNumber GetSecondaryPhoneNumber()
        {
            return _member.GetSecondaryPhoneNumber();
        }

        public PhoneNumber GetTertiaryPhoneNumber()
        {
            return _member.GetSecondaryPhoneNumber();
        }

        public ReadOnlyCollection<EmailAddress> EmailAddresses
        {
            get
            {
                var emailAddress = GetEmailAddress();
                return emailAddress == null ? null : new ReadOnlyCollection<EmailAddress>(new List<EmailAddress> { emailAddress });
            }
        }

        public ReadOnlyCollection<PhoneNumber> PhoneNumbers
        {
            get { return _member.PhoneNumbers; }
        }

        public DateTime LastUpdatedTime
        {
            get { return _member.LastUpdatedTime; }
        }

        public Gender Gender
        {
            get { return _member.Gender; }
        }

        public PartialDate? DateOfBirth
        {
            get { return _member.DateOfBirth; }
        }

        public Address Address
        {
            get { return _member.Address; }
        }

        public Guid? PhotoId
        {
            get { return _member.PhotoId; }
        }

        public VisibilitySettings VisibilitySettings
        {
            get { return _member.VisibilitySettings; }
        }

        public EthnicStatus EthnicStatus
        {
            get { return _member.EthnicStatus; }
        }

        UserType ICommunicationUser.UserType
        {
            get { return _member.UserType; }
        }

        bool ICommunicationUser.IsEnabled
        {
            get { return _member.IsEnabled; }
        }

        bool ICommunicationUser.IsActivated
        {
            get { return _member.IsActivated; }
        }

        public bool IsEmailAddressVerified
        {
            get { return _emailaddress.IsVerified; }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return _member.AffiliateId; }
        }
    }
}

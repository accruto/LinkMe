using System;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Linq;

namespace LinkMe.Domain.Users.Members.Views
{
    public class ProfessionalView
        : IMember, ICommunicationUser
    {
        /// <summary>
        /// The maximum access permitted for an employer that has not paid to see a resume.
        /// </summary>
        private const ProfessionalVisibility NotContacted = ProfessionalVisibility.Resume | ProfessionalVisibility.Communities | ProfessionalVisibility.Salary;
        /// <summary>
        /// The access automatically granted when an Employer has paid to see contact details, regardless
        /// of the Member's settings. The "Referees" setting is not currently changeable by the Member - it's
        /// automatically granted to any paid-up Employers.
        /// </summary>
        private const ProfessionalVisibility Contacted = ProfessionalVisibility.Referees;
        /// <summary>
        /// The access automatically granted to job applicants. ProfilePhoto and Salary need to be explicitly turned on.
        /// are ignored.
        /// </summary>
        private const ProfessionalVisibility Applicant = ProfessionalVisibility.All ^ ProfessionalVisibility.ProfilePhoto ^ ProfessionalVisibility.Salary;

        private readonly Member _member;
        private int? _contactCredits;
        private ProfessionalContactDegree _effectiveContactDegree;
        private bool _isRepresented;
        private readonly bool _hasBeenAccessed;

        public ProfessionalView()
        {
            _contactCredits = 0;
            _member = new Member();
            _effectiveContactDegree = ProfessionalContactDegree.NotContacted;
            _hasBeenAccessed = false;
            _isRepresented = false;
        }

        public ProfessionalView(Member member, int? contactCredits, ProfessionalContactDegree effectiveContactDegree, bool hasBeenAccessed, bool isRepresented)
        {
            _member = member;
            _contactCredits = contactCredits;
            _effectiveContactDegree = effectiveContactDegree;
            _hasBeenAccessed = hasBeenAccessed;
            _isRepresented = isRepresented;
        }

        public int? ContactCredits
        {
            get { return _contactCredits; }
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
            get { return CanContact() == CanContactStatus.YesWithoutCredit ? _member.EmailAddresses.ToReadOnlyCollection() : null; }
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
            get { return ((ICommunicationRecipient) _member).EmailAddress; }
        }

        bool ICommunicationUser.IsEmailAddressVerified
        {
            get { return ((ICommunicationUser)_member).IsEmailAddressVerified; }
        }

        Guid? ICommunicationUser.AffiliateId
        {
            get { return _member.AffiliateId; }
        }

        public bool IsEnabled
        {
            get { return _member.IsEnabled; }
        }

        public bool IsActivated
        {
            get { return _member.IsActivated; }
        }

        public UserType UserType
        {
            get { return _member.UserType; }
        }

        public Guid? AffiliateId
        {
            get { return CanAccess(ProfessionalVisibility.Communities) ? _member.AffiliateId : null; }
        }

        public DateTime CreatedTime
        {
            get { return _member.CreatedTime; }
        }

        DateTime IMember.LastUpdatedTime
        {
            get { return _member.LastUpdatedTime; }
        }

        public ProfessionalContactDegree EffectiveContactDegree
        {
            get { return _effectiveContactDegree; }
        }

        public bool HasBeenAccessed
        {
            get { return _hasBeenAccessed; }
        }

        public bool IsRepresented
        {
            get { return _isRepresented; }
        }

        public ReadOnlyCollection<PhoneNumber> PhoneNumbers
        {
            get
            {
                // If the phone numbers can't be accessed then hide the number.

                var phoneNumbers = ((IMember) _member).PhoneNumbers;
                return CanAccess(ProfessionalVisibility.PhoneNumbers)
                    ? phoneNumbers
                    : phoneNumbers == null
                        ? null
                        : (from p in phoneNumbers select new PhoneNumber { Number = null, Type = p.Type }).ToReadOnlyCollection();
            }
        }

        internal bool HasPhoneNumbers
        {
            get { return _member.PhoneNumbers != null && _member.PhoneNumbers.Count > 0; }
        }

        public Gender Gender
        {
            get { return Gender.Unspecified; }
        }

        public PartialDate? DateOfBirth
        {
            get { return null; }
        }

        public Guid? PhotoId
        {
            get { return CanAccess(ProfessionalVisibility.ProfilePhoto) ? _member.PhotoId : null; }
        }

        public VisibilitySettings VisibilitySettings
        {
            get { return _member.VisibilitySettings; }
        }

        public EthnicStatus EthnicStatus
        {
            get { return _member.EthnicStatus; }
        }

        public Address Address
        {
            get { return _member.Address; }
        }

        public CanContactStatus CanContact()
        {
            // Equivalent to ...

            return CanAccessUsingCredit(ProfessionalVisibility.Resume);
        }

        public CanContactStatus CanContactByPhone()
        {
            var canContact = CanAccessUsingCredit(ProfessionalVisibility.PhoneNumbers);
            if (canContact == CanContactStatus.No)
                return canContact;

            return !HasPhoneNumbers ? CanContactStatus.No : canContact;
        }

        public void CopyAccess(ProfessionalView other)
        {
            _contactCredits = other._contactCredits;
            _effectiveContactDegree = other._effectiveContactDegree;
            _isRepresented = other._isRepresented;
        }

        /// <summary>
        /// Should be made protected some day ...
        /// </summary>
        public bool CanAccess(ProfessionalVisibility visibility)
        {
            // Check the effective contact degree against the settings.

            return CanAccess(_effectiveContactDegree, visibility);
        }

        private CanContactStatus CanAccessUsingCredit(ProfessionalVisibility visibility)
        {
            // Must be able to not only view the specific visibility but must also:
            // - have effectively utilised a credit as well.
            // - be able to view the resume

            var usedCredit = false;
            switch (_effectiveContactDegree)
            {
                case ProfessionalContactDegree.Applicant:
                case ProfessionalContactDegree.Contacted:
                case ProfessionalContactDegree.Self:
                    usedCredit = true;
                    break;

                case ProfessionalContactDegree.NotContacted:
                case ProfessionalContactDegree.Public:
                    break;
            }

            if (usedCredit && CanAccess(_effectiveContactDegree, visibility))
                return CanContactStatus.YesWithoutCredit;

            // If they are currently at Free but they have a credit to use and they can view after utilising that credit ...

            if (_effectiveContactDegree == ProfessionalContactDegree.NotContacted && _contactCredits != null)
            {
                if (_contactCredits.Value > 0)
                    return CanAccess(ProfessionalContactDegree.Contacted, visibility) ? CanContactStatus.YesWithCredit : CanContactStatus.No;
                if (_contactCredits.Value == 0)
                    return CanAccess(ProfessionalContactDegree.Contacted, visibility) ? CanContactStatus.YesIfHadCredit : CanContactStatus.No;
            }
            return CanContactStatus.No;
        }

        private bool CanAccess(ProfessionalContactDegree contactDegree, ProfessionalVisibilitySettings visibilitySettings, ProfessionalVisibility visibility)
        {
            var contactVisibility = visibilitySettings == null
                ? GetPublicVisibility()
                : GetVisibility(contactDegree, visibilitySettings);
            return contactVisibility.IsFlagSet(visibility);
        }

        private bool CanAccess(ProfessionalContactDegree contactDegree, ProfessionalVisibility visibility)
        {
            // Must always be able to view the resume as well as any other visibility.

            if (visibility == ProfessionalVisibility.Resume)
                return CanAccess(contactDegree, _member.VisibilitySettings.Professional, visibility);

            return CanAccess(contactDegree, _member.VisibilitySettings.Professional, visibility)
                && CanAccess(contactDegree, _member.VisibilitySettings.Professional, ProfessionalVisibility.Resume);
        }

        private ProfessionalVisibility GetVisibility(ProfessionalContactDegree degree, ProfessionalVisibilitySettings visibilitySettings)
        {
            // If the user is not enabled then no-one else should have any visibility of them unless they have
            // applied for a job.
            // If they are not activated but have already been contacted, or they have applied for a job
            // then treat them as normal.

            switch (degree)
            {
                case ProfessionalContactDegree.Self:
                    return ProfessionalVisibility.All;

                case ProfessionalContactDegree.Public:
                    return !_member.IsEnabled || !_member.IsActivated
                               ? ProfessionalVisibilitySettings.None
                               : visibilitySettings.PublicVisibility;

                case ProfessionalContactDegree.Contacted:
                    return !_member.IsEnabled
                               ? ProfessionalVisibilitySettings.None
                               : visibilitySettings.EmploymentVisibility | Contacted;

                case ProfessionalContactDegree.Applicant:
                    return visibilitySettings.EmploymentVisibility | Applicant;

                default:
                    return !_member.IsEnabled || !_member.IsActivated
                               ? ProfessionalVisibilitySettings.None
                               : visibilitySettings.EmploymentVisibility & NotContacted;
            }
        }

        private ProfessionalVisibility GetPublicVisibility()
        {
            return !_member.IsEnabled || !_member.IsActivated
                       ? ProfessionalVisibilitySettings.None
                       : NotContacted;
        }

        private string GetFirstName(bool ignoreSettings)
        {
            return ignoreSettings
                       ? _member.FirstName
                       : CanAccess(ProfessionalVisibility.Name) ? _member.FirstName : null;
        }

        private string GetLastName(bool ignoreSettings)
        {
            return ignoreSettings
                       ? _member.LastName
                       : CanAccess(ProfessionalVisibility.Name) ? _member.LastName : null;
        }

        private string GetFullName(bool ignoreSettings)
        {
            return ignoreSettings
                       ? _member.FullName
                       : CanAccess(ProfessionalVisibility.Name) ? _member.FullName : null;
        }
    }
}
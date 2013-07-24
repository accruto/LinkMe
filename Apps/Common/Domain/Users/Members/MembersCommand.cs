using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Users.Members;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Common.Domain.Users.Members
{
/*    public class MembersCommand
        : IMembersCommand
    {
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly INetworkingCommand _networkingCommand;
        private readonly ILocationQuery _locationQuery;

        [Publishes(LinkMe.Domain.Users.Members.PublishedEvents.MemberCreated)]
        public event EventHandler<EventArgs<Member>> MemberCreated;

        [Publishes(LinkMe.Domain.Users.Members.PublishedEvents.PropertiesChanged)]
        public event EventHandler<PropertiesChangedEventArgs> PropertiesChanged;

        [Publishes(LinkMe.Domain.Users.Members.PublishedEvents.EmailBounced)]
        public event EventHandler<EmailBouncedEventArgs> EmailBounced;

        public MembersCommand(ILoginCredentialsCommand loginCredentialsCommand, ICandidatesCommand candidatesCommand, INetworkingCommand networkingCommand, ILoginCredentialsQuery loginCredentialsQuery, ILocationQuery locationQuery)
        {
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _candidatesCommand = candidatesCommand;
            _networkingCommand = networkingCommand;
            _locationQuery = locationQuery;
        }

        void IMembersCommand.CreateMember(Member member)
        {
            // Set some defaults.

            member.IsEnabled = true;
            member.IsNew = true;

            // Save.

            //_userProfileBroker.SaveNewUser(member);
            _networkingCommand.CreateNetworker(new Networker {Id = member.Id});
            _candidatesCommand.CreateCandidate(new Candidate {Id = member.Id, Status = CandidateStatus.OpenToOffers});

            // Fire events.

            var handlers = MemberCreated;
            if (handlers != null)
                handlers(this, new EventArgs<Member>(member));
        }

        void IMembersCommand.CreateMember(Member member, LoginCredentials credentials)
        {
            Validate(credentials);

            // Set some defaults.

            member.IsEnabled = true;
            member.IsNew = true;

            // Save.

            //_userProfileBroker.SaveNewUser(member);
            _loginCredentialsCommand.CreateCredentials(member.Id, credentials);
            _networkingCommand.CreateNetworker(new Networker { Id = member.Id });
            _candidatesCommand.CreateCandidate(new Candidate { Id = member.Id, Status = CandidateStatus.OpenToOffers });

            // Fire events.

            var handlers = MemberCreated;
            if (handlers != null)
                handlers(this, new EventArgs<Member>(member));
        }

        void IMembersCommand.UpdateMember(Member member)
        {
            // Simply save without firing events.

            //_userProfileBroker.SaveUser(member);
        }

        void IMembersCommand.UpdateMember(Member member, string firstName, string lastName, string emailAddress)
        {
            // Keep track of changes.

            var originalFirstName = member.FirstName;
            var originalLastName = member.LastName;
            var originalEmailAddress = member.EmailAddress;

            // Change and save.

            member.EmailAddress = emailAddress;
            if (!emailAddress.Equals(originalEmailAddress, StringComparison.InvariantCultureIgnoreCase))
                member.HasEmailBounced = false;

            member.LastName = lastName;
            member.FirstName = firstName;

            //_userProfileBroker.SaveUser(member);

            // Because the email address is also the loginId that needs to be updated.
            // Check that they have login credentials first because they may only have other types of credentials.

            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            if (credentials != null)
            {
                credentials.LoginId = emailAddress;
                _loginCredentialsCommand.UpdateCredentials(member.Id, credentials, member.Id);
            }

            // Fire events.

            var events = new List<PropertyChangedEventArgs>();
            AddNameEvents(events, member, originalFirstName, originalLastName);
            AddEmailAddressEvents(events, member, originalEmailAddress);
            Fire(member.Id, events);
        }

        void IMembersCommand.UpdateMember(Member member, string firstName, string lastName, int countryId, string location, DateTime? dateOfBirth, Gender gender, EthnicStatus ethnicStatus, DisabilityStatus disabilityStatus)
        {
            // Keep track of changes.

            var originalFirstName = member.FirstName;
            var originalLastName = member.LastName;
            var originalLocation = GetOriginalLocation(member);

            // Change and save.

            member.FirstName = firstName;
            member.LastName = lastName;
            member.EthnicStatus = ethnicStatus;
            member.DisabilityStatus = disabilityStatus;
            member.Gender = gender;
            member.DateOfBirth = dateOfBirth;
            SetLocation(member, countryId, location);
            //_userProfileBroker.SaveUser(member);

            // Fire events.

            var events = new List<PropertyChangedEventArgs>();
            AddNameEvents(events, member, originalFirstName, originalLastName);
            AddLocationEvents(events, member, originalLocation);
            Fire(member.Id, events);
        }

        void IMembersCommand.UpdateMember(Member member, string firstName, string lastName, int countryId, string location, string homePhoneNumber, string mobilePhoneNumber, string workPhoneNumber)
        {
            // Keep track of changes.

            var originalFirstName = member.FirstName;
            var originalLastName = member.LastName;
            var originalHomePhoneNumber = member.HomePhoneNumber;
            var originalMobilePhoneNumber = member.MobilePhoneNumber;
            var originalWorkPhoneNumber = member.WorkPhoneNumber;
            var originalLocation = GetOriginalLocation(member);

            // Change and save.

            member.FirstName = firstName;
            member.LastName = lastName;
            member.HomePhoneNumber = homePhoneNumber;
            member.MobilePhoneNumber = mobilePhoneNumber;
            member.WorkPhoneNumber = workPhoneNumber;

            SetLocation(member, countryId, location);
            //_userProfileBroker.SaveUser(member);

            // Fire events.

            var events = new List<PropertyChangedEventArgs>();
            AddNameEvents(events, member, originalFirstName, originalLastName);
            AddPhoneNumberEvents(events, member, originalHomePhoneNumber, originalMobilePhoneNumber, originalWorkPhoneNumber);
            AddLocationEvents(events, member, originalLocation);
            Fire(member.Id, events);
        }

        void IMembersCommand.UpdateMember(Member member, string emailAddress, string homePhoneNumber, string mobilePhoneNumber, string workPhoneNumber)
        {
            // Keep track of changes.

            var originalEmailAddress = member.EmailAddress;
            var originalHomePhoneNumber = member.HomePhoneNumber;
            var originalMobilePhoneNumber = member.MobilePhoneNumber;
            var originalWorkPhoneNumber = member.WorkPhoneNumber;

            // Change and save.

            member.EmailAddress = emailAddress;
            if (!emailAddress.Equals(originalEmailAddress, StringComparison.InvariantCultureIgnoreCase))
                member.HasEmailBounced = false;

            member.HomePhoneNumber = homePhoneNumber;
            member.MobilePhoneNumber = mobilePhoneNumber;
            member.WorkPhoneNumber = workPhoneNumber;

            //_userProfileBroker.SaveUser(member);

            // Because the email address is also the loginId that needs to be updated.

            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            credentials.LoginId = emailAddress;
            _loginCredentialsCommand.UpdateCredentials(member.Id, credentials, member.Id);

            // Fire events.

            var events = new List<PropertyChangedEventArgs>();
            AddEmailAddressEvents(events, member, originalEmailAddress);
            AddPhoneNumberEvents(events, member, originalHomePhoneNumber, originalMobilePhoneNumber, originalWorkPhoneNumber);
            Fire(member.Id, events);
        }

        void IMembersCommand.SetProfilePhoto(Member member, FileReference fileReference)
        {
            // Keep track of changes.

            var originalPhotoId = member.PhotoId;

            // Change and save.

            member.PhotoId = fileReference == null ? (Guid?) null : fileReference.Id;
            //_userProfileBroker.SaveUser(member);

            // Fire events.

            var events = new List<PropertyChangedEventArgs>();
            AddPhotoEvents(events, member, originalPhotoId);
            Fire(member.Id, events);
        }

        void IMembersCommand.SetEmailBounced(Member member, string emailAddress, string reason)
        {
            // Check that the bounced email address is still current.

            if (!emailAddress.Equals(member.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
                return;
            
            // Update the flag.

            member.HasEmailBounced = true;
            //_userProfileBroker.SaveUser(member);

            var handler = EmailBounced;
            if (handler != null)
            {
                var args = new EmailBouncedEventArgs(member.Id) { EmailAddress = emailAddress, Reason = reason };
                handler(this, args);
            }
        }

        Member IMembersCommand.GetMember(Guid id)
        {
            return null; // _userProfileBroker.FindById(id) as Member;
        }

        Member IMembersCommand.GetMember(string emailAddress)
        {
            return null; // _userProfileBroker.FindByLoginId(emailAddress) as Member;
        }

        ReadOnlyCollection<Member> IMembersCommand.GetMembers(IEnumerable<Guid> ids)
        {
            return new ReadOnlyCollection<Member>(new List<Member>()); //  from i in ids select _userProfileBroker.FindById(i) as Member);.ToReadOnlyCollection();
        }

        private static void AddNameEvents(ICollection<PropertyChangedEventArgs> events, ICommunicationRecipient member, string originalFirstName, string originalLastName)
        {
            if (originalFirstName != member.FirstName)
                events.Add(new FirstNameChangedEventArgs(originalFirstName, member.FirstName));
            if (originalLastName != member.LastName)
                events.Add(new LastNameChangedEventArgs(originalLastName, member.LastName));
        }

        private static void AddEmailAddressEvents(ICollection<PropertyChangedEventArgs> events, ICommunicationRecipient member, string originalEmailAddress)
        {
            if (originalEmailAddress != member.EmailAddress)
                events.Add(new EmailAddressChangedEventArgs(originalEmailAddress, member.EmailAddress));
        }

        private static void AddPhoneNumberEvents(ICollection<PropertyChangedEventArgs> events, IMember member, string originalHomePhoneNumber, string originalMobilePhoneNumber, string originalWorkPhoneNumber)
        {
            if (originalHomePhoneNumber != member.HomePhoneNumber)
                events.Add(new HomePhoneNumberChangedEventArgs(originalHomePhoneNumber, member.HomePhoneNumber));
            if (originalMobilePhoneNumber != member.MobilePhoneNumber)
                events.Add(new MobilePhoneNumberChangedEventArgs(originalMobilePhoneNumber, member.MobilePhoneNumber));
            if (originalWorkPhoneNumber != member.WorkPhoneNumber)
                events.Add(new WorkPhoneNumberChangedEventArgs(originalWorkPhoneNumber, member.WorkPhoneNumber));
        }

        private static void AddPhotoEvents(ICollection<PropertyChangedEventArgs> events, IMember member, Guid? originalPhotoId)
        {
            var photoId = member.PhotoId;
            if (originalPhotoId != photoId)
                events.Add(new ProfilePhotoChangedEventArgs(member.Id, photoId));
        }

        private static void AddLocationEvents(ICollection<PropertyChangedEventArgs> events, IMember member, LocationReference originalLocation)
        {
            var location = member.Address == null ? null : member.Address.Location;
            if (!Equals(originalLocation, location))
                events.Add(new LocationChangedEventArgs(originalLocation, location));
        }

        private void Fire(Guid memberId, ICollection<PropertyChangedEventArgs> events)
        {
            if (events.Count > 0)
            {
                var handlers = PropertiesChanged;
                if (handlers != null)
                    handlers(this, new PropertiesChangedEventArgs(memberId, events));
            }
        }

        private static LocationReference GetOriginalLocation(IMember member)
        {
            if (member.Address == null || member.Address.Location == null)
                return null;
            return member.Address.Location.Clone();
        }

        private void SetLocation(IMember member, int countryId, string location)
        {
            _locationQuery.ResolvePostalSuburb(member.Address.Location, _locationQuery.GetCountry(countryId), location);
        }

        private void Validate(LoginCredentials credentials)
        {
            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();
        }
    }
*/
}
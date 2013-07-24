using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Test.Files;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Members;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Test.Users.Members
{
    public static class MemberAccountsTestExtensions
    {
        private static readonly ILocationQuery LocationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly IFilesCommand FilesCommand = Container.Current.Resolve<IFilesCommand>();
        private static readonly IMembersRepository MembersRepository = Container.Current.Resolve<IMembersRepository>();
        private static readonly ILoginCredentialsCommand LoginCredentialsCommand = Container.Current.Resolve<ILoginCredentialsCommand>();
        private static readonly ILoginCredentialsQuery LoginCredentialsQuery = Container.Current.Resolve<ILoginCredentialsQuery>();
        private static readonly ICandidatesCommand CandidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private static readonly IMemberAffiliationsCommand MemberAffiliationsCommand = Container.Current.Resolve<IMemberAffiliationsCommand>();

        private const string UserIdFormat = "member{0}@test.linkme.net.au";
        private const string DefaultPassword = "password";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";
        private const string DefaultPhoneNumber = "0410635666";
        private static readonly Country DefaultCountry = LocationQuery.GetCountry("Australia");
        private const string DefaultLocation = "Melbourne VIC 3000";
        private const Gender DefaultGender = Gender.Male;
        private static readonly PartialDate DefaultDateOfBirth = new PartialDate(1970, 2, 3);

        public static string GetPassword(this Member member)
        {
            return DefaultPassword;
        }

        public static string GetLoginId(this Member member)
        {
            return member.GetBestEmailAddress().Address;
        }

        public static string GetLoginId(this IMember member)
        {
            return member.GetBestEmailAddress().Address;
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, int index)
        {
            return memberAccountsCommand.CreateTestMember(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index));
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, int index, LocationReference location)
        {
            return memberAccountsCommand.CreateTestMember(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), null, location);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, int index, DateTime createTime)
        {
            return memberAccountsCommand.CreateTestMember(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), createTime, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, int index, bool activated)
        {
            return memberAccountsCommand.CreateTestMember(string.Format(UserIdFormat, index), DefaultPassword, string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), activated, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, int index, Guid? affiliateId)
        {
            return memberAccountsCommand.CreateTestMember(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), affiliateId);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, bool createKnownInvalidMember, string emailAddress)
        {
            return memberAccountsCommand.CreateTestMember(createKnownInvalidMember, emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, Guid? affiliateId)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0), affiliateId);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, bool activated)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, DefaultPassword, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0), activated, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, bool createKnownInvalidMember, string emailAddress, string firstName, string lastName)
        {
            return memberAccountsCommand.CreateTestMember(createKnownInvalidMember, emailAddress, DefaultPassword, firstName, lastName, true, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string firstName, string lastName)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, DefaultPassword, firstName, lastName, true, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string firstName, string lastName, DateTime? createTime)
        {
            return memberAccountsCommand.CreateTestMember(false, emailAddress, DefaultPassword, firstName, lastName, true, null, createTime, null);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string firstName, string lastName, DateTime? createTime, LocationReference location)
        {
            return memberAccountsCommand.CreateTestMember(false, emailAddress, DefaultPassword, firstName, lastName, true, null, createTime, location);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string firstName, string lastName, Guid? affiliateId)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, DefaultPassword, firstName, lastName, true, affiliateId);
        }

        public static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string password, string firstName, string lastName)
        {
            return memberAccountsCommand.CreateTestMember(emailAddress, password, firstName, lastName, true, null);
        }

        public static void AddTestProfilePhoto(this IMemberAccountsCommand memberAccountsCommand, Member member)
        {
            var fileReference = FilesCommand.CreateTestPhoto(0, FileType.ProfilePhoto);
            member.PhotoId = fileReference.Id;
            memberAccountsCommand.UpdateMember(member);
        }

        private static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, bool createKnownInvalidMember, string emailAddress, string password, string firstName, string lastName, bool activated, Guid? affiliateId)
        {
            return memberAccountsCommand.CreateTestMember(createKnownInvalidMember, emailAddress, password, firstName, lastName, activated, affiliateId, null, null);
        }

        private static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, string emailAddress, string password, string firstName, string lastName, bool activated, Guid? affiliateId)
        {
            return memberAccountsCommand.CreateTestMember(false, emailAddress, password, firstName, lastName, activated, affiliateId, null, null);
        }

        private static Member CreateTestMember(this IMemberAccountsCommand memberAccountsCommand, bool createKnownInvalidMember, string emailAddress, string password, string firstName, string lastName, bool activated, Guid? affiliateId, DateTime? createTime, LocationReference location)
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = true } },
                IsActivated = activated,
                IsEnabled = true,
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = DefaultPhoneNumber, Type = PhoneNumberType.Mobile } },
                FirstName = firstName,
                LastName = lastName,
                Gender = DefaultGender,
                DateOfBirth = DefaultDateOfBirth,
            };

            if (createTime.HasValue)
            {
                member.CreatedTime = createTime.Value;
            }

            var credentials = new LoginCredentials
            {
                LoginId = emailAddress,
                PasswordHash = LoginCredentials.HashToString(password)
            };

            // Deny public access to real name, because existing tests rely on this. Might need to change this later.

            member.VisibilitySettings = new VisibilitySettings();
            member.VisibilitySettings.Personal.PublicVisibility &= ~PersonalVisibility.Name;

            if (location == null)
            {
                member.Address = new Address {Location = new LocationReference()};
                LocationQuery.ResolvePostalSuburb(member.Address.Location, DefaultCountry, DefaultLocation);
            }
            else
            {
                member.Address = new Address {Location = location};
            }

            if (createKnownInvalidMember)
                CreateInvalidMember(member, credentials, affiliateId);
            else
                memberAccountsCommand.CreateMember(member, credentials, affiliateId);

            return member;
        }

        private static void CreateInvalidMember(Member member, LoginCredentials credentials, Guid? affiliateId)
        {
            // Check login credentials.

            if (LoginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            member.IsEnabled = true;
            member.AffiliateId = affiliateId;

            // Save.
            member.Prepare();

            MembersRepository.CreateMember(member);

            var candidate = new Candidate
            {
                Id = member.Id,
                Status = Defaults.CandidateStatus,
                DesiredJobTypes = Defaults.DesiredJobTypes,
                RelocationPreference = Defaults.RelocationPreference,
            };
            CandidatesCommand.CreateCandidate(candidate);

            if (affiliateId != null)
                MemberAffiliationsCommand.SetAffiliation(member.Id, affiliateId.Value);

            LoginCredentialsCommand.CreateCredentials(member.Id, credentials);

            //Update search

            //MemberSearchService.UpdateMember(member.Id);
        }

        public static void UpdateInvalidMember(this IMemberAccountsCommand memberAccountsCommand, Member member)
        {
            // Keep track of changes.

            var originalCredentials = LoginCredentialsQuery.GetCredentials(member.Id);

            // Save.

            MembersRepository.UpdateMember(member);

            originalCredentials.LoginId = member.GetBestEmailAddress().Address;
            LoginCredentialsCommand.UpdateCredentials(member.Id, originalCredentials, member.Id);
        }
    }
}

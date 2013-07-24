using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Host;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Search
{
    [TestClass]
    public abstract class ExecuteMemberSearchTests
        : TestClass
    {
        protected readonly IMemberSearchService _memberSearchService;
        protected readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private const string EmailAddressFormat = "test{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";
        private const string ObjectiveFormat = "The is the {0} objective.";

        protected ExecuteMemberSearchTests()
        {
            _memberSearchService = Resolve<MemberSearchService>();
            ((IChannelAware)_memberSearchService).OnOpen();
            _executeMemberSearchCommand = new ExecuteMemberSearchCommand(new LocalChannelManager<IMemberSearchService>(_memberSearchService));
        }

        [TestInitialize]
        public void ExecuteMemberSearchTestsInitialize()
        {
            _memberSearchService.Clear();
        }

        protected Member CreateMember(int index, Action<Member, Candidate, Resume> initialiseMember)
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = string.Format(EmailAddressFormat, index), IsVerified = true } },
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null) },
                IsEnabled = true,
                IsActivated = true,
                VisibilitySettings = new VisibilitySettings { Professional = { EmploymentVisibility = ProfessionalVisibility.All } },
            };
            var candidate = new Candidate
            {
                Status = CandidateStatus.ActivelyLooking,
            };
            var resume = new Resume
            {
                Objective = string.Format(ObjectiveFormat, index),
            };

            if (initialiseMember != null)
                initialiseMember(member, candidate, resume);

            _membersCommand.CreateMember(member);
            candidate.Id = member.Id;
            _candidatesCommand.CreateCandidate(candidate);
            _candidateResumesCommand.CreateResume(candidate, resume);

            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}
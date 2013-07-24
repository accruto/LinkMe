using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public abstract class CampaignsTaskTests
        : TaskTests
    {
        private const string EmployerUserIdFormat = "employer{0:D2}";
        private const string MemberEmailAddressFormat = "member{0:D2}@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        private const string CampaignNameFormat = "My new campaign{0:D2}";
        private const string TemplateSubjectFormat = "The subject{0:D2} of the template";
        private const string TemplateBodyFormat = "The body{0} of the template";

        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly ICampaignsRepository _repository = Resolve<ICampaignsRepository>();
        protected readonly ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected readonly ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();
        protected readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        [TestInitialize]
        public void CampaignsTaskTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void CreateCampaign(int index, CampaignCategory category, string query, out Campaign campaign, out Template template)
        {
            campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = string.Format(CampaignNameFormat, index),
                CreatedBy = Guid.NewGuid(),
                CreatedTime = DateTime.Now.AddMinutes(index),
                Category = category,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                Query = query,
            };

            _campaignsCommand.CreateCampaign(campaign);

            template = new Template
            {
                Subject = string.Format(TemplateSubjectFormat, index),
                Body = string.Format(TemplateBodyFormat, index)
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);
        }

        protected Campaign CreateCampaign(int index, CampaignCategory category, string query, CampaignStatus status, out Campaign campaign, out Template template)
        {
            CreateCampaign(index, category, query, out campaign, out template);
            campaign.Status = status;
            _repository.UpdateStatus(campaign.Id, status);
            return campaign;
        }

        protected IList<Employer> CreateEmployers(int start, int count, EmployerSubRole subRole, IOrganisation organisation, params Industry[] industries)
        {
            var employers = new List<Employer>();
            for (var index = start; index < start + count; ++index)
                employers.Add(CreateEmployer(index, subRole, organisation, industries));
            return employers;
        }

        protected IList<Employer> CreateEmployers(int start, int count, EmployerSubRole subRole)
        {
            return CreateEmployers(start, count, subRole, null, null);
        }

        protected IList<Employer> CreateEmployers(int start, int count, params Industry[] industries)
        {
            return CreateEmployers(start, count, EmployerSubRole.Employer, null, industries);
        }

        protected IList<Employer> CreateEmployers(int start, int count, IOrganisation organisation)
        {
            return CreateEmployers(start, count, EmployerSubRole.Employer, organisation, null);
        }

        protected Employer CreateEmployer(int index, EmployerSubRole subRole, IOrganisation organisation, params Industry[] industries)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(string.Format(EmployerUserIdFormat, index), _organisationsCommand.CreateTestOrganisation(0));
            employer.SubRole = subRole;
            if (organisation != null)
                employer.Organisation = organisation;
            if (industries != null)
                employer.Industries = industries.ToList();
            _employerAccountsCommand.UpdateEmployer(employer);

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, ExpiryDate = DateTime.Now.AddDays(100), InitialQuantity = 100 });
            return employer;
        }

        protected Employer CreateEmployer(int index, EmployerSubRole subRole)
        {
            return CreateEmployer(index, subRole, null, null);
        }

        protected IList<Member> CreateMembers(int start, int count, params Industry[] industries)
        {
            var members = new List<Member>();
            for (var index = start; index < start + count; ++index)
                members.Add(CreateMember(index, industries));
            return members;
        }

        protected IList<Member> CreateMembers(int start, int count)
        {
            return CreateMembers(start, count, null);
        }

        protected Member CreateMember(string emailAddress, params Industry[] industries)
        {
            var member = _memberAccountsCommand.CreateTestMember(emailAddress, FirstName, LastName);

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            if (industries != null)
            {
                candidate.Industries = industries.ToList();
                _candidatesCommand.UpdateCandidate(candidate);
            }

            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected Member CreateMember(int index)
        {
            return CreateMember(index, null);
        }

        protected Member CreateMember(int index, params Industry[] industries)
        {
            return CreateMember(string.Format(MemberEmailAddressFormat, index), industries);
        }
    }
}
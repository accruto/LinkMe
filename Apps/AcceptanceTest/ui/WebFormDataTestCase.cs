using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Files;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Networking.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;

namespace LinkMe.AcceptanceTest.ui
{
    public abstract class WebFormDataTestCase
        : WebTestClass
    {
        #region constants

        protected const string TestNetworkerUserId = "linkme1@test.linkme.net.au";
        public const string TestUserTwoId = "linkme2@test.linkme.net.au";
        public const string TestUserThreeId = "linkme3@test.linkme.net.au";
        public const string TestUserFourId = "linkme4@test.linkme.net.au";
        public const string TestUserFiveId = "linkme5@test.linkme.net.au";
        public const string TestUserSixId = "linkme6@test.linkme.net.au";
        public const string TestUserSevenId = "linkme7@test.linkme.net.au";
        public const string TestUserEightId = "linkme8@test.linkme.net.au";
        public const string TestUserNineId = "linkme9@test.linkme.net.au";

        protected const string TestEmployerUserId = "employer";
        protected const string TestEmployerEmail = "employer@test.linkme.net.au";
        
        protected const string TestAdministratorUserId = "admin";
        protected const string TestAdministratorEmail = "admin@test.linkme.net.au";

        public const string TestEmailMultipleValidFormat = "linkme8@test.linkme.net.au, linkme7@test.linkme.net.au; linkme5@test.linkme.net.au";
        public const string TestEmailMultipleInvalidFormat = "linkme8@test.linkme.net.au,, linkme75test.linkme.net.au; linkme55test.linkme.net.au";
        public const string TestEmailMultipleValidSpaceFormat = "blah@b.com; c@c.com,   x@x.com.au;y@Y.com.au";
        public const string TestEmailToInvalidFormat = "linkme5test.linkme.net.au";

        #endregion

        protected readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        protected readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        protected readonly INetworkingQuery _networkingQuery = Resolve<INetworkingQuery>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobAdIntegrationQuery _jobAdIntegrationQuery = Resolve<IJobAdIntegrationQuery>();
        protected readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();

        protected Guid PreLoadJobAds()
        {
            const string jobPosterFax = "0385089191";
            const string jobPosterPhone = "0385089111";

            var contact1 = new ContactDetails { FirstName = "Karl", LastName = "Heinz", EmailAddress = "karl.heinz@bmw.com", SecondaryEmailAddresses = null, FaxNumber = jobPosterFax, PhoneNumber = jobPosterPhone };
            var contact2 = new ContactDetails { FirstName = null, LastName = null, EmailAddress = "rani.wou@toyota.com", SecondaryEmailAddresses = null, FaxNumber = null, PhoneNumber = "" };

            var employer = _employerAccountsQuery.GetEmployer(TestEmployerUserId);

            var industries = Resolve<IIndustriesQuery>().GetIndustries();

            var melbourne = new LocationReference();
            _locationQuery.ResolveLocation(melbourne, Australia, "3000");

            // load job ads

            var newJobAd1 = CreateNewJobAd("Amazing opportunity joining a reknowned company",
                                                            JobAdStatus.Open,
                                                            employer, false, contact1,
                                                            "1st summary",
                                                            "bulletpoint1", "bulletpoint2",
                                                            "bulletpoint3", null,
                                                            "Biz Analyst",
                                                            "Biz Analyst",
                                                            JobTypes.FullTime, true,
                                                            "Ref01",
                                                            string.Empty, "45000",
                                                            "car",
                                                            new List<Industry> { industries[0], industries[1] },
                                                            "BMW", melbourne.Clone(),
                                                            DateTime.Now.AddDays(20));
            Guid jobAdId1 = newJobAd1.Id;
            newJobAd1.Integration.ExternalReferenceId =
                string.Format("ref{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond);

            var newJobAd2 = CreateNewJobAd("Work with the best", JobAdStatus.Open,
                                                        employer, false, contact2,
                                                        "2ND SUMMARY",
                                                        "BULLETPOINT1", "BULLETPOINT2",
                                                        "BULLETPOINT3", null,
                                                        "Experienced Team Leader Required - top $$$",
                                                        "Project Leader",
                                                        JobTypes.Contract, false,
                                                        "REF02",
                                                        "90000", "80000",
                                                        "MOBILE PHONE",
                                                        new List<Industry> { industries[2], industries[3] },
                                                        "TOYOTA", melbourne.Clone(), DateTime.Now.AddDays(30));

            newJobAd2.Integration.ExternalReferenceId =
                string.Format("ref{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond);
            newJobAd2.LastUpdatedTime = DateTime.Now.AddDays(5);

            _jobAdsCommand.UpdateJobAd(newJobAd2);

            return jobAdId1;
        }

        private JobAd CreateNewJobAd(string adContent, JobAdStatus adStatus, IHasId<Guid> adPoster,
            bool adHideContactDetails, ContactDetails adContactDetails, string summary, string bulletpoint1, string bulletpoint2,
            string bulletpoint3, FileReference logoImg, string adTitle, string positionTitle,
            JobTypes jobtypes, bool isResidenacyRequired, string externalRef, string maxsalary,
            string minsalary, string package, IList<Industry> reqIndustries, string companyname,
            LocationReference jobLocation, DateTime expiryDate)
        {
            var salary = SalaryExtensions.Parse(minsalary, maxsalary, SalaryRate.Year, true);
            var bulletPoints = new[] { bulletpoint1, bulletpoint2, bulletpoint3 };

            var newJobAd = new JobAd
            {
                Status = adStatus,
                PosterId = adPoster.Id,
                Visibility = 
                {
                    HideContactDetails = adHideContactDetails,
                    HideCompany = false,
                },
                ContactDetails = adContactDetails,
                Title = adTitle,
                Integration = { ExternalReferenceId = externalRef },
                LogoId = logoImg == null ? (Guid?)null : logoImg.Id,
                ExpiryTime = expiryDate,
                Description =
                {
                    CompanyName = companyname,
                    Content = adContent,
                    PositionTitle = positionTitle,
                    ResidencyRequired = isResidenacyRequired,
                    JobTypes = jobtypes,
                    Industries = reqIndustries,
                    Summary = summary,
                    Salary = salary,
                    Package = package,
                    BulletPoints = bulletPoints,
                    Location = jobLocation,
                }
            };

            _jobAdsCommand.CreateJobAd(newJobAd);
            _jobAdsCommand.OpenJobAd(newJobAd);

            return newJobAd;
        }

        protected Member PreLoadTestUserProfiles()
        {
            return PreLoadTestUserProfiles(true, true, true, true, true, true, true);
        }

        private Member PreLoadTestUserProfiles(bool admin, bool employer, bool link, bool inviters, bool fullSetOfNetworkers, bool jobs, bool references)
        {
            return PreLoadTestUserProfiles(admin, employer, link, inviters, fullSetOfNetworkers, jobs, references, false);
        }

        protected Member PreLoadTestUserProfiles(bool admin, bool createEmployer, bool link,
            bool inviters, bool fullSetOfNetworkers, bool jobs, bool references, bool resumeRequests)
        {
            #region set up 10 networkers

            Member five = null;
            Member six = null;

            Member member = _memberAccountsCommand.CreateTestMember(TestNetworkerUserId, "password", "Homer", "Simpson");
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            Member two = _memberAccountsCommand.CreateTestMember(TestUserTwoId, "password", "Two First", "Two Last");
            Member three = _memberAccountsCommand.CreateTestMember(TestUserThreeId, "password", "Three First", "Three Last");

            Member four = _memberAccountsCommand.CreateTestMember(TestUserFourId);

            if (fullSetOfNetworkers)
            {
                five = _memberAccountsCommand.CreateTestMember(TestUserFiveId);
                six = _memberAccountsCommand.CreateTestMember(TestUserSixId);
                _memberAccountsCommand.CreateTestMember(TestUserSevenId);
                _memberAccountsCommand.CreateTestMember(TestUserEightId);
                _memberAccountsCommand.CreateTestMember(TestUserNineId);
            }

            #endregion
            #region set up an employer and a job ad
            if (createEmployer)
            {
                Employer employer = _employerAccountsCommand.CreateTestEmployer(TestEmployerUserId, _organisationsCommand.CreateTestOrganisation("LinkMe Pty Ltd"));

                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1000 });
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = 1000 });

                var ja = employer.CreateTestJobAd("test job", "lol test job");
                _jobAdsCommand.PostJobAd(ja);
            }
            #endregion
            #region set up an administrator

            if (admin)
            {
                CreateAdministrator(TestAdministratorUserId);
            }

            #endregion
            #region link up some users

            if (link)
            {
                _networkingCommand.CreateFirstDegreeLink(member.Id, two.Id);
                _networkingCommand.CreateFirstDegreeLink(member.Id, three.Id);
                _networkingCommand.CreateFirstDegreeLink(member.Id, four.Id);
            }

            if (link && fullSetOfNetworkers)
            {
                _networkingCommand.CreateFirstDegreeLink(two.Id, five.Id);
                _networkingCommand.CreateFirstDegreeLink(two.Id, six.Id);
                _networkingCommand.CreateFirstDegreeLink(three.Id, six.Id);
            }

            #endregion
            return member;
        }

        public Administrator CreateAdministrator(string userid)
        {
            return _administratorAccountsCommand.CreateTestAdministrator(userid);
        }

        protected void PreLoadTestUserProfilesAndLogin()
        {
            var member = PreLoadTestUserProfiles();
            LogIn(member);
        }

        protected Employer CreateEmployer(string userId)
        {
            Employer employer = _employerAccountsCommand.CreateTestEmployer(userId, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }

        protected Employer CreateEmployer(string email, string userId)
        {
            Employer employer = _employerAccountsCommand.CreateTestEmployer(userId, _organisationsCommand.CreateTestOrganisation(0));
            employer.EmailAddress = new EmailAddress { Address = email };
            _employerAccountsCommand.UpdateEmployer(employer);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }
    }
}

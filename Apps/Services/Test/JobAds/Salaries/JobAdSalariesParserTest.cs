using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.JobAds.Salaries;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Files;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.JobAds.Salaries
{
    [TestClass]
    public class JobAdSalariesParserTest
        : TestClass
    {
        private readonly IJobAdSalariesParserCommand _jobAdSalariesParserCommand = Resolve<IJobAdSalariesParserCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        protected const string TestEmployerUserId = "employer";
        private const decimal SalaryConversionMidToMin = .8888889M;
        private const decimal SalaryConversionMidToMax = 1.111111M;
        private const decimal SalaryConversionMinToMax = 1.25M;
        private const decimal SalaryConversionMaxToMin = .8M;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestSalaryUpTo()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity up to $90k", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M * SalaryConversionMaxToMin, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryFrom()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity from $90k", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(90000M * SalaryConversionMinToMax, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryCirca()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity circa $90k", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M * SalaryConversionMidToMin, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(90000M * SalaryConversionMidToMax, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRangeKk()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $90K - $130k", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(130000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRangeK()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $90-$130k", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(130000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRange000To000()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $90,000 - $130,000", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(130000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRange000()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $90000-$130000", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(130000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRangeMissingDollarSign()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $90000-130000", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.AreEqual(90000M, ads[0].Description.ParsedSalary.LowerBound);
            Assert.AreEqual(130000M, ads[0].Description.ParsedSalary.UpperBound);
        }

        [TestMethod]
        public void TestSalaryRangeTooHigh()
        {
            var employer = CreateEmployer();

            CreateJobAd(employer, "Fantastic opportunity $900000-$1300000", null, null);
            _jobAdSalariesParserCommand.ParseJobAdSalaries(true);

            var ads = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetOpenJobAdIds());

            Assert.AreEqual(1, ads.Count);
            Assert.IsNull(ads[0].Description.ParsedSalary.LowerBound);
            Assert.IsNull(ads[0].Description.ParsedSalary.UpperBound);
        }


        private Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(TestEmployerUserId, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1000 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = 1000 });
            return employer;
        }

        private void CreateJobAd(IHasId<Guid> employer, string jobAdText, decimal? minSalary, decimal? maxSalary)
        {
            const string jobPosterFax = "0385089191";
            const string jobPosterPhone = "0385089111";

            var contact = new ContactDetails { FirstName = "Karl", LastName = "Heinz", EmailAddress = "karl.heinz@bmw.com", FaxNumber = jobPosterFax, PhoneNumber = jobPosterPhone };
            var industries = Resolve<IIndustriesQuery>().GetIndustries();
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, Australia, "3000");

            CreateNewJobAd(jobAdText, JobAdStatus.Open, employer, 
                false, contact, "1st summary", "bulletpoint1", "bulletpoint2", 
                "bulletpoint3", null, "Biz Analyst", "Biz Analyst", 
                JobTypes.FullTime, true, "Ref01", maxSalary, 
                minSalary, "car", new List<Industry> { industries[0], industries[1] }, "BMW", 
                location, DateTime.Now.AddDays(20));
            return;
        }

        private void CreateNewJobAd(string adContent, JobAdStatus adStatus, IHasId<Guid> adPoster,
            bool adHideContactDetails, ContactDetails adContactDetails, string summary, string bulletpoint1, string bulletpoint2,
            string bulletpoint3, FileReference logoImg, string adTitle, string positionTitle,
            JobTypes jobtypes, bool isResidenacyRequired, string externalRef, decimal? maxsalary,
            decimal? minsalary, string package, IList<Industry> reqIndustries, string companyname,
            LocationReference jobLocation, DateTime expiryDate)
        {
            var bulletPoints = new[] {bulletpoint1, bulletpoint2, bulletpoint3};

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
                Integration = {ExternalReferenceId = externalRef},
                LogoId = logoImg == null ? (Guid?) null : logoImg.Id,
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
                    Salary = (minsalary.HasValue || maxsalary.HasValue
                        ? new Salary
                        {
                            Currency = Currency.AUD,
                            LowerBound = minsalary,
                            UpperBound = maxsalary,
                            Rate = SalaryRate.Year,
                        }
                        : null),
                    Package = package,
                    BulletPoints = bulletPoints,
                    Location = jobLocation,
                }
            };

            _jobAdsCommand.CreateJobAd(newJobAd);

            if (adStatus == JobAdStatus.Open)
                _jobAdsCommand.OpenJobAd(newJobAd);
            else if (adStatus == JobAdStatus.Closed)
                _jobAdsCommand.CloseJobAd(newJobAd);

            return;
        }

        private Country Australia
        {
            get { return _locationQuery.GetCountry("Australia"); }
        }
    }
}

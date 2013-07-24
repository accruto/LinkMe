using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.JobAdCredits
{
    [TestClass]
    public abstract class ViewJobAdCreditsUsageTests
        : ViewCreditsUsageTests
    {
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();

        private ReadOnlyUrl _newJobAdUrl;

        private HtmlTextBoxTester _titleTextBox;
        private HtmlTextBoxTester _positionTitleTextBox;
        private HtmlTextBoxTester _bulletPoint1TextBox;
        private HtmlTextBoxTester _bulletPoint2TextBox;
        private HtmlTextBoxTester _bulletPoint3TextBox;
        private HtmlTextAreaTester _summaryTextBox;
        private HtmlTextAreaTester _contentTextBox;
        private HtmlTextBoxTester _companyNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlCheckBoxTester _residencyRequiredCheckBox;
        private HtmlCheckBoxTester _fullTimeCheckBox;
        private HtmlCheckBoxTester _hideContactDetailsCheckBox;
        private HtmlButtonTester _previewButton;
        private HtmlButtonTester _publishButton;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
            _emailServer.ClearEmails();

            _newJobAdUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");

            _titleTextBox = new HtmlTextBoxTester(Browser, "Title");
            _positionTitleTextBox = new HtmlTextBoxTester(Browser, "PositionTitle");
            _bulletPoint1TextBox = new HtmlTextBoxTester(Browser, "BulletPoint1");
            _bulletPoint2TextBox = new HtmlTextBoxTester(Browser, "BulletPoint2");
            _bulletPoint3TextBox = new HtmlTextBoxTester(Browser, "BulletPoint3");
            _summaryTextBox = new HtmlTextAreaTester(Browser, "Summary");
            _contentTextBox = new HtmlTextAreaTester(Browser, "Content");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _companyNameTextBox = new HtmlTextBoxTester(Browser, "CompanyName");
            _residencyRequiredCheckBox = new HtmlCheckBoxTester(Browser, "ResidencyRequired");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _hideContactDetailsCheckBox = new HtmlCheckBoxTester(Browser, "HideContactDetails");

            _previewButton = new HtmlButtonTester(Browser, "preview");
            _publishButton = new HtmlButtonTester(Browser, "publish");
        }

        [TestMethod]
        public void TestPostNoAllocations()
        {
            TestPost(NoAllocate);
        }

        [TestMethod]
        public void TestPostNoCredits()
        {
            TestPost(NoCredits);
        }

        [TestMethod]
        public void TestPostSomeCredits()
        {
            TestPost(SomeCredits);
        }

        [TestMethod]
        public void TestPostUnlimitedCredits()
        {
            TestPost(UnlimitedCredits);
        }

        [TestMethod]
        public void TestPostSomeCreditsNoApplicantCredits()
        {
            TestPost(SomeCreditsNoApplicantCredits);
        }

        [TestMethod]
        public void TestPostUnlimitedCreditsNoApplicantCredits()
        {
            TestPost(UnlimitedCreditsNoApplicantCredits);
        }

        private static IList<Allocation> NoAllocate(ICreditOwner owner)
        {
            return new List<Allocation>();
        }

        private IList<Allocation> NoCredits(ICreditOwner owner)
        {
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = 0 };
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null };
            _allocationsCommand.CreateAllocation(allocation1);
            _allocationsCommand.CreateAllocation(allocation2);
            return new[] { allocation1, allocation2 };
        }

        private IList<Allocation> SomeCredits(ICreditOwner owner)
        {
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = 10 };
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null };
            _allocationsCommand.CreateAllocation(allocation1);
            _allocationsCommand.CreateAllocation(allocation2);
            return new[] { allocation1, allocation2 };
        }

        private IList<Allocation> SomeCreditsNoApplicantCredits(ICreditOwner owner)
        {
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = 10 };
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = 0 };
            _allocationsCommand.CreateAllocation(allocation1);
            _allocationsCommand.CreateAllocation(allocation2);
            return new[] { allocation1, allocation2 };
        }

        private IList<Allocation> UnlimitedCredits(ICreditOwner owner)
        {
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null };
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null };
            _allocationsCommand.CreateAllocation(allocation1);
            _allocationsCommand.CreateAllocation(allocation2);
            return new[] { allocation1, allocation2 };
        }

        private IList<Allocation> UnlimitedCreditsNoApplicantCredits(ICreditOwner owner)
        {
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null };
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = 0 };
            _allocationsCommand.CreateAllocation(allocation1);
            _allocationsCommand.CreateAllocation(allocation2);
            return new[] { allocation1, allocation2 };
        }

        private void TestPost(Func<ICreditOwner, IList<Allocation>> allocate)
        {
            // Create everyone.

            var employer = CreateEmployer();
            var owner = GetCreditOwner(employer);
            var allocations = allocate(owner).ToList();

            var administrator = CreateAdministrator();

            // Check before.

            var initialJobAdAllocation = (from a in allocations where a.CreditId == _creditsQuery.GetCredit<JobAdCredit>().Id select a).SingleOrDefault();
            var initialApplicantAllocation = (from a in allocations where a.CreditId == _creditsQuery.GetCredit<ApplicantCredit>().Id select a).SingleOrDefault();

            if (employer.Id == owner.Id)
                AssertPreCredits(administrator, employer, initialJobAdAllocation, initialApplicantAllocation);
            else
                AssertPreCredits(administrator, employer, owner as Organisation, initialJobAdAllocation, initialApplicantAllocation);

            // Post a job.

            var jobAd = Post(employer);

            if (employer.Id == owner.Id)
                AssertPostCredits(administrator, employer, jobAd, initialJobAdAllocation, initialApplicantAllocation);
            else
                AssertPostCredits(administrator, employer, owner as Organisation, jobAd, initialJobAdAllocation, initialApplicantAllocation);
        }

        protected abstract Employer CreateEmployer();
        protected abstract ICreditOwner GetCreditOwner(Employer employer);

        private JobAdEntry Post(IEmployer employer)
        {
            // Apply for the job.

            LogIn(employer);
            Get(_newJobAdUrl);
            CreateJobAd(employer.EmailAddress.Address);
            _previewButton.Click();

            _publishButton.Click();
            var jobAd = _jobAdsQuery.GetJobAds<JobAdEntry>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open))[0];

            LogOut();
            return jobAd;
        }

        private void CreateJobAd(string emailAddress)
        {
            _titleTextBox.Text = "Code Monkey";
            _positionTitleTextBox.Text = "Code Monkey Position - lots of peanuts";
            _bulletPoint1TextBox.Text = "lots of peanuts";
            _bulletPoint2TextBox.Text = "and bananas";
            _bulletPoint3TextBox.Text = "and work hours";
            _summaryTextBox.Text = "Code monkey positon available - lots of hours, and bananas";
            _contentTextBox.Text = "Code monkey positon available - lots of hours, and bananas and peanuts";
            _companyNameTextBox.Text = "Acme";
            _emailAddressTextBox.Text = emailAddress;
            _hideContactDetailsCheckBox.IsChecked = false;
            _locationTextBox.Text = "Armadale, Vic";
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[1].Value };
            _residencyRequiredCheckBox.IsChecked = true;
            _fullTimeCheckBox.IsChecked = true;
        }

        private void AssertPreCredits(IUser administrator, Employer employer, Allocation jobAdAllocation, Allocation applicantAllocation)
        {
            LogIn(administrator);

            // Check that the employer has not exercised any credits.

            AssertEmployerCreditUsage<JobAdCredit>(employer, employer, false, new UsedOn[0]);

            // Check their credits.

            AssertEmployerCredits(employer, GetAllocations(jobAdAllocation, applicantAllocation).ToArray());

            // Check the allocation.

            if (jobAdAllocation != null)
                AssertEmployerCreditUsage(employer, jobAdAllocation, false, new UsedOn[0]);

            LogOut();
        }

        private void AssertPreCredits(IUser administrator, IEmployer employer, Organisation organisation, Allocation jobAdAllocation, Allocation applicantAllocation)
        {
            LogIn(administrator);

            // Check that the employer has not exercised any credits.

            AssertEmployerCreditUsage<JobAdCredit>(employer, organisation, false, new UsedOn[0]);

            // Check their credits.

            AssertOrganisationCredits(organisation, GetAllocations(jobAdAllocation, applicantAllocation).ToArray());

            // Check who has used the organisation's credits.

            AssertOrganisationCreditUsage<JobAdCredit>(organisation, employer, false, new UsedOn[0]);

            // Check the allocation.

            if (jobAdAllocation != null)
                AssertOrganisationCreditUsage(organisation, employer, jobAdAllocation, false, new UsedOn[0]);

            LogOut();
        }

        private void AssertPostCredits(IUser administrator, IEmployer employer, JobAdEntry jobAd, Allocation initialJobAdAllocation, Allocation initialApplicantAllocation)
        {
            var allocations = GetAllocations(initialJobAdAllocation, initialApplicantAllocation);

            // Determine whether allocations have been created.

            Allocation usedJobAdAllocation;
            if (initialJobAdAllocation == null)
            {
                usedJobAdAllocation = _allocationsQuery.GetAllocationsByOwnerId<JobAdCredit>(employer.Id).Single();
                allocations.Add(usedJobAdAllocation);
            }
            else
            {
                if (initialJobAdAllocation.InitialQuantity == 0)
                {
                    usedJobAdAllocation = _allocationsQuery.GetAllocationsByOwnerId<JobAdCredit>(employer.Id).Single(a => a.Id != initialJobAdAllocation.Id);
                    allocations.Add(usedJobAdAllocation);
                }
                else
                {
                    usedJobAdAllocation = initialJobAdAllocation;
                    if (usedJobAdAllocation.InitialQuantity != null && usedJobAdAllocation.InitialQuantity != 0)
                        usedJobAdAllocation.RemainingQuantity -= 1;
                }
            }

            if (initialApplicantAllocation == null)
                allocations.Add(_allocationsQuery.GetAllocationsByOwnerId<ApplicantCredit>(employer.Id).Single());
            else if (initialApplicantAllocation.InitialQuantity == 0)
                allocations.Add(_allocationsQuery.GetAllocationsByOwnerId<ApplicantCredit>(employer.Id).Single(a => a.Id != initialApplicantAllocation.Id));

            LogIn(administrator);

            // Check who the employer has exercised credits on.

            AssertEmployerCreditUsage<JobAdCredit>(employer, (ICreditOwner)employer, true, new UsedOnJobAd(jobAd));

            // Check their credits.

            AssertEmployerCredits(employer, allocations.ToArray());

            // Check the allocation.

            AssertEmployerCreditUsage(employer, usedJobAdAllocation, true, new UsedOnJobAd(jobAd));

            LogOut();
        }

        private void AssertPostCredits(IUser administrator, IEmployer employer, Organisation organisation, JobAdEntry jobAd, Allocation initialJobAdAllocation, Allocation initialApplicantAllocation)
        {
            var employerAllocations = new List<Allocation>();
            var organisationAllocations = GetAllocations(initialJobAdAllocation, initialApplicantAllocation);

            // Determine whether allocations have been created.

            Allocation usedJobAdAllocation;
            if (initialJobAdAllocation == null)
            {
                usedJobAdAllocation = _allocationsQuery.GetAllocationsByOwnerId<JobAdCredit>(employer.Id).Single();
                employerAllocations.Add(usedJobAdAllocation);
            }
            else
            {
                if (initialJobAdAllocation.InitialQuantity == 0)
                {
                    usedJobAdAllocation = _allocationsQuery.GetAllocationsByOwnerId<JobAdCredit>(employer.Id).Single(a => a.Id != initialJobAdAllocation.Id);
                    employerAllocations.Add(usedJobAdAllocation);
                }
                else
                {
                    usedJobAdAllocation = initialJobAdAllocation;
                    if (usedJobAdAllocation.InitialQuantity != null && usedJobAdAllocation.InitialQuantity != 0)
                        usedJobAdAllocation.RemainingQuantity -= 1;
                }
            }

            if (initialApplicantAllocation == null || initialApplicantAllocation.InitialQuantity == 0 || initialJobAdAllocation == null || initialJobAdAllocation.InitialQuantity == 0)
                employerAllocations.Add(_allocationsQuery.GetAllocationsByOwnerId<ApplicantCredit>(employer.Id).Single());

            LogIn(administrator);

            // Check who the employer has exercised credits on.

            AssertEmployerCreditUsage<JobAdCredit>(employer, usedJobAdAllocation.OwnerId == employer.Id ? (ICreditOwner)employer : organisation, true, new UsedOnJobAd(jobAd));

            // Check their credits.

            AssertEmployerCredits(employer, employerAllocations.ToArray());
            AssertOrganisationCredits(organisation, organisationAllocations.ToArray());

            // Check who has used the organisation's credits.

            AssertEmployerCreditUsage<JobAdCredit>(employer, null, true, new UsedOnJobAd(jobAd));
            AssertOrganisationCreditUsage<JobAdCredit>(organisation, employer, usedJobAdAllocation.OwnerId != employer.Id, usedJobAdAllocation.OwnerId != employer.Id ? (UsedOn)new UsedOnJobAd(jobAd) : null);

            // Check the allocation.

            if (usedJobAdAllocation.OwnerId == employer.Id)
                AssertEmployerCreditUsage(employer, usedJobAdAllocation, true, new UsedOnJobAd(jobAd));
            else
                AssertOrganisationCreditUsage(organisation, employer, usedJobAdAllocation, true, new UsedOnJobAd(jobAd));

            LogOut();
        }

        private static IList<Allocation> GetAllocations(Allocation jobAdAllocation, Allocation applicantAllocation)
        {
            var allocations = new List<Allocation>();
            if (jobAdAllocation != null)
                allocations.Add(jobAdAllocation);
            if (applicantAllocation != null)
                allocations.Add(applicantAllocation);
            return allocations;
        }
    }
}
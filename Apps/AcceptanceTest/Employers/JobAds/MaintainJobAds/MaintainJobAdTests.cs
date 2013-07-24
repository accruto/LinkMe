using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
    [TestClass]
    public abstract class MaintainJobAdTests
        : WebTestClass
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        protected HtmlButtonTester _previewButton;
        protected HtmlButtonTester _saveButton;

        protected HtmlButtonTester _publishButton;
        protected HtmlButtonTester _reopenButton;
        protected HtmlButtonTester _repostButton;
        protected HtmlButtonTester _editButton;

        protected HtmlTextBoxTester _titleTextBox;
        protected HtmlTextBoxTester _positionTitleTextBox;
        protected HtmlTextBoxTester _externalReferenceIdTextBox;
        protected HtmlTextBoxTester _bulletPoint1TextBox;
        protected HtmlTextBoxTester _bulletPoint2TextBox;
        protected HtmlTextBoxTester _bulletPoint3TextBox;
        protected HtmlTextAreaTester _summaryTextBox;
        protected HtmlTextAreaTester _contentTextBox;
        protected HtmlTextBoxTester _emailAddressTextBox;
        protected HtmlTextBoxTester _secondaryEmailAddressesTextBox;
        protected HtmlTextBoxTester _phoneNumberTextBox;
        protected HtmlTextBoxTester _faxNumberTextBox;
        protected HtmlCheckBoxTester _hideContactDetailsCheckBox;
        protected HtmlDropDownListTester _countryIdDropDownList;
        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlTextBoxTester _firstNameTextBox;
        protected HtmlTextBoxTester _lastNameTextBox;
        protected HtmlTextBoxTester _salaryLowerBoundTextBox;
        protected HtmlTextBoxTester _salaryUpperBoundTextBox;
        protected HtmlTextBoxTester _packageTextBox;
        protected HtmlTextBoxTester _companyNameTextBox;
        protected HtmlCheckBoxTester _hideCompanyCheckBox;
        protected HtmlListBoxTester _industryIdsListBox;
        protected HtmlCheckBoxTester _residencyRequiredCheckBox;
        protected HtmlCheckBoxTester _fullTimeCheckBox;
        protected HtmlCheckBoxTester _partTimeCheckBox;
        protected HtmlCheckBoxTester _contractCheckBox;
        protected HtmlCheckBoxTester _tempCheckBox;
        protected HtmlCheckBoxTester _jobShareCheckBox;
        protected HtmlTextBoxTester _expiryTimeTextBox;

        protected HtmlRadioButtonTester _baseFeaturePack;
        protected HtmlRadioButtonTester _featurePack1;
        protected HtmlRadioButtonTester _featurePack2;

        protected Industry _accounting;
        protected Industry _administration;

        [TestInitialize]
        public void EmployerNewJobAdTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _previewButton = new HtmlButtonTester(Browser, "preview");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _publishButton = new HtmlButtonTester(Browser, "publish");
            _reopenButton = new HtmlButtonTester(Browser, "reopen");
            _repostButton = new HtmlButtonTester(Browser, "repost");
            _editButton = new HtmlButtonTester(Browser, "edit");

            _titleTextBox = new HtmlTextBoxTester(Browser, "Title");
            _positionTitleTextBox = new HtmlTextBoxTester(Browser, "PositionTitle");
            _externalReferenceIdTextBox = new HtmlTextBoxTester(Browser, "ExternalReferenceId");
            _bulletPoint1TextBox = new HtmlTextBoxTester(Browser, "BulletPoint1");
            _bulletPoint2TextBox = new HtmlTextBoxTester(Browser, "BulletPoint2");
            _bulletPoint3TextBox = new HtmlTextBoxTester(Browser, "BulletPoint3");
            _summaryTextBox = new HtmlTextAreaTester(Browser, "Summary");
            _contentTextBox = new HtmlTextAreaTester(Browser, "Content");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _secondaryEmailAddressesTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddresses");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _faxNumberTextBox = new HtmlTextBoxTester(Browser, "FaxNumber");
            _countryIdDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _salaryUpperBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryUpperBound");
            _packageTextBox = new HtmlTextBoxTester(Browser, "Package");
            _companyNameTextBox = new HtmlTextBoxTester(Browser, "CompanyName");
            _hideCompanyCheckBox = new HtmlCheckBoxTester(Browser, "HideCompany");
            _residencyRequiredCheckBox = new HtmlCheckBoxTester(Browser, "ResidencyRequired");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _partTimeCheckBox = new HtmlCheckBoxTester(Browser, "PartTime");
            _contractCheckBox = new HtmlCheckBoxTester(Browser, "Contract");
            _tempCheckBox = new HtmlCheckBoxTester(Browser, "Temp");
            _jobShareCheckBox = new HtmlCheckBoxTester(Browser, "JobShare");
            _expiryTimeTextBox = new HtmlTextBoxTester(Browser, "ExpiryTime");
            _hideContactDetailsCheckBox = new HtmlCheckBoxTester(Browser, "HideContactDetails");

            _baseFeaturePack = new HtmlRadioButtonTester(Browser, "BaseFeaturePack");
            _featurePack1 = new HtmlRadioButtonTester(Browser, "FeaturePack1");
            _featurePack2 = new HtmlRadioButtonTester(Browser, "FeaturePack2");

            _accounting = _industriesQuery.GetIndustry("Accounting");
            _administration = _industriesQuery.GetIndustry("Administration");
        }

        protected ReadOnlyUrl GetJobAdUrl(Guid? jobAdId)
        {
            return jobAdId == null
                ? new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad")
                : new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad", new ReadOnlyQueryString("jobAdId", jobAdId.ToString()));
        }

        protected ReadOnlyUrl GetPreviewUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad/preview", new ReadOnlyQueryString("jobAdId", jobAdId.ToString()));
        }

        protected ReadOnlyUrl GetAccountUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad/account", new ReadOnlyQueryString("jobAdId", jobAdId.ToString()));
        }

        protected ReadOnlyUrl GetPaymentUrl(Guid jobAdId, JobAdFeaturePack featurePack)
        {
            return new ReadOnlyApplicationUrl(
                true,
                "~/employers/jobads/jobad/payment",
                new ReadOnlyQueryString(
                    "jobAdId", jobAdId.ToString(),
                    "featurePack", featurePack.ToString()));
        }

        protected ReadOnlyUrl GetReceiptUrl(Guid jobAdId, Guid orderId)
        {
            return new ReadOnlyApplicationUrl(
                true,
                "~/employers/jobads/jobad/receipt",
                new ReadOnlyQueryString(
                    "jobAdId", jobAdId.ToString(),
                    "orderId", orderId.ToString()));
        }

        protected ReadOnlyUrl GetSuggestedCandidatesUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/employers/candidates/suggested/" + jobAdId);
        }

        protected Employer CreateEmployer(int? credits)
        {
            return CreateEmployer(credits, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Employer CreateEmployer(int? credits, Organisation organisation)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            if (credits == null || credits.Value != 0)
            {
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = credits });
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            }
            return employer;
        }

        protected JobAd CreateJobAd(IEmployer employer, JobAdStatus status)
        {
            var jobAd = employer.CreateTestJobAd();
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            switch (status)
            {
                case JobAdStatus.Open:
                    _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
                    break;

                case JobAdStatus.Closed:
                    _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
                    _employerJobAdsCommand.CloseJobAd(employer, jobAd);
                    break;
            }

            return jobAd;
        }

        protected void EnterJobDetails()
        {
            _titleTextBox.Text = "Gorilla";
            _contentTextBox.Text = "best position after the lion";
            _emailAddressTextBox.Text = "koko@monkey.biz";
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[1].Value };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = "Camberwell VIC 3124";
        }
    }
}
using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Shared.Models;
using Constants = LinkMe.Domain.Roles.JobAds.Constants;

namespace LinkMe.Web.Areas.Employers.Models.JobAds
{
    public class LogoModel
    {
        public Guid? FileReferenceId { get; set; }
    }

    public class JobAdModel
    {
        public JobAdModel()
        {
            Status = JobAdStatus.Draft;
        }

        public Guid Id { get; set; }

        [PrepareHtml, Required, Html, StringLength(Constants.MaxTitleLength)]
        public string Title { get; set; }
        [PrepareHtml, Html, StringLength(Constants.MaxPositionTitleLength)]
        public string PositionTitle { get; set; }
        [StringLength(Constants.MaxExternalReferenceIdLength)]
        public string ExternalReferenceId { get; set; }

        [PrepareHtml, Html, ArrayLength(Constants.MaxBulletPoints), StringLength(Constants.MaxBulletPointLength)]
        public IList<string> BulletPoints { get; set; }

        [PrepareHtml, StringLength(Constants.MaxSummaryLength)]
        public string Summary { get; set; }
        [PrepareHtml, Required, StringLength(Constants.MaxContentLength)]
        public string Content { get; set; }

        [PrepareHtml, Html]
        public string CompanyName { get; set; }
        public bool HideCompany { get; set; }

        [PrepareHtml, Html, StringLength(Constants.MaxPackageLength)]
        public string Package { get; set; }
        public bool ResidencyRequired { get; set; }

        public DateTime? ExpiryTime { get; set; }

        public bool HideContactDetails { get; set; }
        [Prepare, Validate]
        public ContactDetails ContactDetails { get; set; }

        public IList<Guid> IndustryIds { get; set; }
        [Prepare, Validate]
        public LocationReference Location { get; set; }
        [Prepare, Salary]
        public Salary Salary { get; set; }
        public JobTypes JobTypes { get; set; }

        public JobAdStatus Status { get; set; }
        public LogoModel Logo { get; set; }
    }

    public class JobAdReferenceModel
    {
        public Country DefaultCountry { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<Industry> Industries { get; set; } 
    }

    public class EditJobAdModel
    {
        public bool IsNew { get; set; }
        public JobAdModel JobAd { get; set; }
        public int? JobAdCredits { get; set; }
        public JobAdReferenceModel Reference { get; set; }
    }

    public class JobAdFeaturesModel
    {
        public bool ShowLogo { get; set; }
        public bool IsHighlighted { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    public class PreviewJobAdModel
        : JobAdViewModel
    {
        public JobAdStatus Status { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool CanBeOpened { get; set; }
        public int? JobAdCredits { get; set; }
        public JobAdFeaturePack FeaturePack { get; set; }
        public IDictionary<JobAdFeaturePack, JobAdFeaturesModel> Features { get; set; }
    }

    public class PaymentJobAdModel
        : PaymentModel
    {
        public Product Product { get; set; }
    }

    public class ReceiptJobAdModel
    {
        public MemberJobAdView JobAd { get; set; }
        public OrderSummaryModel OrderSummary { get; set; }
    }
}
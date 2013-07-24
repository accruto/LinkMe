using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAdDescription
    {
        [PrepareHtml, StringLength(Constants.MaxPositionTitleLength)]
        public string PositionTitle { get; set; }
        [PrepareHtml, Html]
        public string CompanyName { get; set; }
        [PrepareHtml, StringLength(Constants.MaxSummaryLength)]
        public string Summary { get; set; }
        [PrepareHtml, Required]
        public string Content { get; set; }
        [PrepareHtml, ArrayLength(Constants.MaxBulletPoints), StringLength(Constants.MaxBulletPointLength)]
        public IList<string> BulletPoints { get; set; }
        public JobTypes JobTypes { get; set; }
        [PrepareHtml, Html, StringLength(Constants.MaxPackageLength)]
        public string Package { get; set; }
        public bool ResidencyRequired { get; set; }
        [Prepare, Validate]
        public LocationReference Location { get; set; }
        public IList<Industry> Industries { get; set; }
        [Prepare, Salary]
        public Salary Salary { get; set; }
        [Prepare, Salary]
        public Salary ParsedSalary { get; set; }
    }
}

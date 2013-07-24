using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Apps.Services.External.Jxt.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using Job=LinkMe.Apps.Services.External.Jxt.Schema.Job;
using Salary=LinkMe.Domain.Salary;

namespace LinkMe.Apps.Services.External.Jxt
{
    public class JobAdMapper
        : IJobFeedMapper<Job>
    {
        private readonly ILocationQuery _locationQuery;
        private readonly string[] _ignoredCompanyIds;
        private readonly Country _australia;
        private readonly IDictionary<string, JobTypes> _jobTypesMap;
        private readonly IDictionary<string, Industry> _industryMap;
        private const string ApplicationUrlFormat = "http://jobsjobsjobs.com.au/product_detail.aspx?prod={0}";

        private enum ClassificationTypes
        {
            WORKTYPE,
            LOCATION,
            AREA,
        }

        public JobAdMapper(ILocationQuery locationQuery, IIndustriesQuery industriesQuery, string[] ignoredCompanyIds)
        {
            _locationQuery = locationQuery;
            _ignoredCompanyIds = ignoredCompanyIds;
            _australia = _locationQuery.GetCountry("Australia");
            _industryMap = CreateIndustryMap(industriesQuery);
            _jobTypesMap = CreateJobTypesMap();
         }

        string IJobFeedMapper<Job>.GetPostId(Job post)
        {
            return post.Reference;
        }

        bool IJobFeedMapper<Job>.IsIgnored(Job post)
        {
            if (post.AdvertiserId == null || _ignoredCompanyIds == null)
                return false;

            return _ignoredCompanyIds.Contains(post.AdvertiserId);
        }

        void IJobFeedMapper<Job>.ApplyPostData(Job post, JobAd jobAd)
        {
            jobAd.Integration.ExternalApplyUrl = string.IsNullOrEmpty(post.ApplicationMethod.Value)
                ? string.Format(ApplicationUrlFormat, post.Reference)
                : post.ApplicationMethod.Value;

            jobAd.Integration.ExternalReferenceId = post.ReferenceNo;
            jobAd.Integration.IntegratorReferenceId = post.Reference;

            DateTime convertedDateTime;
            jobAd.CreatedTime = DateTime.TryParse(post.DatePosted, out convertedDateTime) ? convertedDateTime : DateTime.Now;
            jobAd.LastUpdatedTime = DateTime.Now;  

            jobAd.ExpiryTime = jobAd.CreatedTime.AddDays(45);
            jobAd.Title = HttpUtility.HtmlDecode(post.Title);

            // Company

            jobAd.Visibility.HideCompany = false;

            // In the case where the application goes to JXT (applicationmethod = jobx) use JXT as the contact company name
            jobAd.ContactDetails = new ContactDetails { CompanyName = post.ApplicationMethod.Type == "jobx" ? "JXT" : post.AdvertiserName };

            // Content

            jobAd.Description.Content = post.AdDetails;

            // JobTypes

            jobAd.Description.JobTypes = JobTypes.FullTime;

            var workType = GetClassification(post.Listing.Classifications, ClassificationTypes.WORKTYPE);

            if (!string.IsNullOrEmpty(workType) && _jobTypesMap.ContainsKey(workType))
            {
                jobAd.Description.JobTypes = _jobTypesMap[workType];
            }

            // Location
            var location = GetClassification(post.Listing.Classifications, ClassificationTypes.LOCATION);
            var area = GetClassification(post.Listing.Classifications, ClassificationTypes.AREA);

            jobAd.Description.Location = _locationQuery.ResolveLocation(_australia, ParseLocation(location, area));

            //Salary

            if (post.Salary != null)
            {
                if (!(post.Salary.HideSalaryDetails.ToLower() == "yes"))
                {
                    jobAd.Description.Salary = new Salary
                                                   {
                                                       Currency = Currency.AUD,
                                                       LowerBound = ParseSalary(post.Salary.Min),
                                                       UpperBound = ParseSalary(post.Salary.Max)
                                                   };

                    if (jobAd.Description.Salary.LowerBound.HasValue || jobAd.Description.Salary.UpperBound.HasValue)
                    {
                        jobAd.Description.Salary.Rate = SalaryRate.Year;

                        if (!string.IsNullOrEmpty(post.Salary.Type))
                        {
                            switch (post.Salary.Type.ToLower())
                            {
                                case "annual":
                                    jobAd.Description.Salary.Rate = SalaryRate.Year;
                                    break;

                                case "yearly":
                                    jobAd.Description.Salary.Rate = SalaryRate.Year;
                                    break;

                                case "hourly":
                                    jobAd.Description.Salary.Rate = SalaryRate.Hour;
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
            
            // Industries

            if (post.Categories != null && post.Categories.Classification != null)
            {
                var classification = post.Categories.Classification.Text;

                if (_industryMap.ContainsKey(classification))
                jobAd.Description.Industries = new List<Industry>{_industryMap[classification]};
            }
            
        }

        private static string GetClassification(IEnumerable<Classification> classifications, ClassificationTypes classification)
        {
            return classifications == null
                ? string.Empty
                : classifications.Where(c => c.Name == classification.ToString()).Select(c => c.Text).FirstOrDefault();
        }

        private static decimal? ParseSalary(string salaryToParse)
        {
            if (string.IsNullOrEmpty(salaryToParse))
                return null;

            decimal parsedSalary;
            if (decimal.TryParse(salaryToParse, out parsedSalary))
                return parsedSalary;

            return null;
        }

        private static string ParseLocation(string location, string area)
        {
            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(area))
                return location;

            // location should be in the form Location / Area (eg. Sydney / All Sydney)
            // Want to transform to Location (Area) (eg. Sydney (All Sydney))

            var locationParts = location.Split('/');

            if (locationParts.Length != 2)
                return location;        //can't parse

            if (!locationParts[1].Trim().Equals(area, StringComparison.InvariantCultureIgnoreCase))
                return location;        //can't parse

            // appears to be in the form Location / Area
            // return Location (Area)

            return string.Format("{0} ({1})", locationParts[0].Trim(), locationParts[1].Trim());
        }

        private static IDictionary<string, JobTypes> CreateJobTypesMap()
        {
            return new Dictionary<string, JobTypes>
                       {
                           {"Full Time", JobTypes.FullTime},
                           {"Part Time", JobTypes.PartTime},
                           {"Contract", JobTypes.Contract},
                           {"Temporary", JobTypes.Temp},
                           {"Casual", JobTypes.Temp},
                           {"Volunteer", JobTypes.PartTime},
                           {"Apprentice", JobTypes.FullTime},
                           {"Work Experience", JobTypes.Contract},
                           {"Graduate", JobTypes.FullTime},
                           {"JOB share", JobTypes.JobShare},
                           {"Trainee", JobTypes.FullTime},
                       };

        }

        private static IDictionary<string, Industry> CreateIndustryMap(IIndustriesQuery industriesQuery)
        {
            var industryMap = new[]
            {
                Tuple.Create("Accounting", "accounting"),              
                Tuple.Create("Administration", "administration"),
                Tuple.Create("Records, Information and Archives", "administration"),
                Tuple.Create("Advert/Media/Comm/Ent & Design", "advertising-media-entertainment"),
                Tuple.Create("Automotive", "automotive"),
                Tuple.Create("Banking & Finance", "banking-financial-services"),
                Tuple.Create("Call Centre & Customer Service", "call-centre-customer-service"),
                Tuple.Create("Community & Sports", "community-sport"),
                Tuple.Create("Library and Information", "community-sport"),
                Tuple.Create("Construction & Architecture", "construction"),
                Tuple.Create("Consulting", "consulting-corporate-strategy"),
                Tuple.Create("Education & Training", "education-training"),
                Tuple.Create("Apprenticeships & Traineeships", "education-training"),
                Tuple.Create("Graduate", "education-training"),
                Tuple.Create("Volunteer", "education-training"),
                Tuple.Create("Engineering", "engineering"),
                Tuple.Create("Government, Defence, Emergency", "government-defence"),
                Tuple.Create("Healthcare & Medical", "healthcare-medical-pharmaceutical"),
                Tuple.Create("Hospitality, Tourism & Travel", "hospitality-tourism"),
                Tuple.Create("HR & Recruitment", "hr-recruitment"),
                Tuple.Create("I.T. & T", "it-telecommunications"),
                Tuple.Create("Insurance & Superannuation", "insurance-superannuation"),
                Tuple.Create("Legal", "legal"),
                Tuple.Create("Manufacturing/Operations", "manufacturing-operations"),
                Tuple.Create("Mining, Oil & Gas", "mining-oil-gas"),
                Tuple.Create("Other", "other"),
                Tuple.Create("Executive", "other"),
                Tuple.Create("Primary Industry", "primary-industry"),
                Tuple.Create("Real Estate & Property", "real-estate-property"),
                Tuple.Create("Retail & Fashion", "retail-consumer-products"),
                Tuple.Create("Sales", "sales-marketing"),
                Tuple.Create("Marketing", "sales-marketing"),
                Tuple.Create("Science", "science-technology"),
                Tuple.Create("Self Employment", "self-employment"),
                Tuple.Create("Trades & Services", "trades-services"),
                Tuple.Create("Transport, Shipping, Logistics", "transport-logistics"),
                Tuple.Create("Aviation", "transport-logistics"),
            };

            return industryMap.ToDictionary(t => t.Item1, t => industriesQuery.GetIndustryByUrlName(t.Item2));
        }
    }
}

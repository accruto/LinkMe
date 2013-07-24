using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    public static class JobAdTestExtensions
    {
        private static readonly ILocationQuery LocationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        /// <summary>
        /// Creates an OPEN job ad
        /// </summary>
        /// <param name="jobAdsCommand"></param>
        /// <param name="employer"></param>
        /// <returns>Open job ad</returns>
        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, IEmployer employer)
        {
            return PostTestJobAd(jobAdsCommand, employer, JobAdStatus.Open);
        }

        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, IEmployer employer, string title)
        {
            return PostTestJobAd(jobAdsCommand, employer.CreateTestJobAd(title), JobAdStatus.Open);
        }

        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, IEmployer employer, JobAdStatus jobStatus)
        {
            return PostTestJobAd(jobAdsCommand, employer.CreateTestJobAd(), jobStatus);
        }

        private static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, JobAd jobAd, JobAdStatus jobStatus)
        {
            jobAdsCommand.CreateJobAd(jobAd);
            switch (jobStatus)
            {
                case JobAdStatus.Open:
                    jobAdsCommand.OpenJobAd(jobAd);
                    break;

                case JobAdStatus.Closed:
                    jobAdsCommand.OpenJobAd(jobAd);
                    jobAdsCommand.CloseJobAd(jobAd);
                    break;

                case JobAdStatus.Deleted:
                    jobAdsCommand.DeleteJobAd(jobAd);
                    break;

                default:
                    //do nothing - job is created in draft state
                    break;
            }

            return jobAd;
        }

        public static JobAd CreateTestJobAd(this IEmployer employer)
        {
            return employer.CreateTestJobAd("mentor");
        }

        public static JobAd CreateTestJobAd(this IEmployer employer, string title)
        {
            return employer.CreateTestJobAd(title, "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.");
        }

        public static JobAd CreateTestJobAd(this IEmployer employer, string title, string content)
        {
            var industry = IndustriesQuery.GetIndustry("Consulting & Corporate Strategy");
            return employer.CreateTestJobAd(title, content, industry);
        }

        public static JobAd CreateTestJobAd(this IEmployer employer, string title, string content, Industry industry)
        {
            var location = LocationQuery.ResolveLocation(LocationQuery.GetCountry("Australia"), "Melbourne VIC 3000");
            return employer.CreateTestJobAd(title, content, industry, location);
        }

        public static JobAd CreateTestJobAd(this IEmployer employer, string title, string content, Industry industry, LocationReference location)
        {
            return new JobAd
            {
                Status = JobAdStatus.Open,
                PosterId = employer.Id,
                Visibility =
                {
                    HideContactDetails = false,
                    HideCompany = false,
                },
                ContactDetails = new ContactDetails
                {
                    FirstName = employer.FirstName,
                    LastName = employer.LastName,
                    EmailAddress = employer.EmailAddress == null ? null : employer.EmailAddress.Address,
                    SecondaryEmailAddresses = null,
                    FaxNumber = "8508 9191",
                    PhoneNumber = employer.PhoneNumber == null ? null : employer.PhoneNumber.Number
                },
                Title = title,
                LogoId = null,
                Description = 
                {
                    Content = content,
                    CompanyName = "Great Employers",
                    PositionTitle = title,
                    ResidencyRequired = true,
                    JobTypes = JobTypes.FullTime,
                    Industries = new List<Industry> { industry },
                    Summary = "",
                    Salary = new Salary { LowerBound = 2000, UpperBound = 3000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                    Package = null,
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Location = location == null ? null : location.Clone()
                }
            };
        }
    }
}
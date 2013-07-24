using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Domain.Users.Test.Anonymous.JobAds
{
    public static class JobAdTestExtensions
    {
        private static readonly ILocationQuery LocationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, AnonymousUser user)
        {
            return PostTestJobAd(jobAdsCommand, user, JobAdStatus.Open);
        }

        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, AnonymousUser user, string title)
        {
            return PostTestJobAd(jobAdsCommand, user.CreateTestJobAd(title), JobAdStatus.Open);
        }

        public static JobAd PostTestJobAd(this IJobAdsCommand jobAdsCommand, AnonymousUser user, JobAdStatus jobStatus)
        {
            return PostTestJobAd(jobAdsCommand, user.CreateTestJobAd(), jobStatus);
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
            }

            return jobAd;
        }

        public static JobAd CreateTestJobAd(this AnonymousUser user)
        {
            return user.CreateTestJobAd("mentor");
        }

        public static JobAd CreateTestJobAd(this AnonymousUser user, string title)
        {
            return user.CreateTestJobAd(title, "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.");
        }

        public static JobAd CreateTestJobAd(this AnonymousUser user, string title, string content)
        {
            var industry = IndustriesQuery.GetIndustry("Consulting & Corporate Strategy");
            return user.CreateTestJobAd(title, content, industry);
        }

        public static JobAd CreateTestJobAd(this AnonymousUser user, string title, string content, Industry industry)
        {
            var location = LocationQuery.ResolveLocation(LocationQuery.GetCountry("Australia"), "Melbourne VIC 3000");
            return user.CreateTestJobAd(title, content, industry, location);
        }

        public static JobAd CreateTestJobAd(this AnonymousUser user, string title, string content, Industry industry, LocationReference location)
        {
            return new JobAd
            {
                Status = JobAdStatus.Open,
                PosterId = user.Id,
                Visibility =
                {
                    HideContactDetails = false,
                    HideCompany = false,
                },
                ContactDetails = new ContactDetails
                {
                    FirstName = null,
                    LastName = null,
                    EmailAddress = "employer@test.linkme.net.au",
                    SecondaryEmailAddresses = null,
                    FaxNumber = "8508 9191",
                    PhoneNumber = null
                },
                Title = title,
                LogoId = null,
                Description = 
                {
                    Content = content,
                    CompanyName = "Great users",
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
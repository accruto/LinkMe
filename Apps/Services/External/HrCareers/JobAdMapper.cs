using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Services.External.HrCareers.Schema;
using LinkMe.Domain;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Services.External.HrCareers
{
    public class JobAdMapper
    {
        private static readonly Dictionary<int, string> CategoryNames = new Dictionary<int, string>
        {
            {15, "Industrial Relations"}, 
            {19, "Generalist"},
            {20, "Recruitment Consultant"},
            {21, "Management"},
            {22, "Learning and Development"},
            {23, "Organisation Design and Development"},
            {24, "OH&S/ Risk"},
            {25, "Compliance/Diversity"},
            {26, "Graduate/Student placements"},
            {27, "Compensation/Return-to-work"},
            {28, "Remuneration & Benefits"},
            {29, "Information & Management Systems"},
            {30, "Other"},
            {31, "General HR"},
            {32, "Executive Position"},
        };

        private readonly LocationMapper _locationMapper;
        private readonly IWebSiteQuery _webSiteQuery;

        public Guid? VerticalId { private get; set; }

        public JobAdMapper(ILocationQuery locationQuery, IWebSiteQuery webSiteQuery)
        {
            _locationMapper = new LocationMapper(locationQuery);
            _webSiteQuery = webSiteQuery;
        }

        public Job Map(JobAd jobAd, IEnumerable<int> categoryIds)
        {
            var applyUrl = _webSiteQuery.GetUrl(WebSite.LinkMe, VerticalId, false, 
                "/jobs/Job.aspx", new ReadOnlyQueryString("jobAdId", jobAd.Id.ToString("N")))
                .ToString();

            var post = new Job
            {
                id = jobAd.Id.ToString("N"),
                title = jobAd.Title,
                description = GetDescription(jobAd.Description.BulletPoints, jobAd.Description.Content, applyUrl),
                reference = jobAd.Integration.ExternalReferenceId,
                //startdate = jobAd.CreatedTime, startdateSpecified = true,
                categories = categoryIds.Select(id => new JobCategory {id = id, name = CategoryNames[id]}).ToArray(),
                jobtype = MapJobType(jobAd.Description.JobTypes),
                region = _locationMapper.Map(jobAd.Description.Location),
                application = new JobApplication
                {
                    url = applyUrl,
                    emailto = (jobAd.ContactDetails != null && jobAd.ContactDetails.EmailAddress != null) ? jobAd.ContactDetails.EmailAddress : string.Empty,
                }
            };

            return post;
        }

        private static string GetDescription(IEnumerable<string> bulletPoints, string contentHtml, string applyUrl)
        {
            // Job ad link.

            var applyLink = new TagBuilder("a") { InnerHtml = "Please click here to apply." };
            applyLink.MergeAttribute("href", applyUrl);

            var applyLinkBold = new TagBuilder("b") { InnerHtml = applyLink.ToString() };
            string applyHtmlBottom = "<br/><br/>" + applyLinkBold;

            // Map bullet points.

            string bulletPointsHtml = string.Empty;

            if (bulletPoints != null)
            {
                StringBuilder listItems = bulletPoints.Aggregate(new StringBuilder(), (agg, bulletPoint) =>
                {
                    // Make sure that is looks good on Search Results page
                    char lastChar = bulletPoint[bulletPoint.Length - 1];
                    if (lastChar != '.' && lastChar != ',' && lastChar != ';' && lastChar != '!')
                        bulletPoint += ". ";
                    else
                        bulletPoint += " ";

                    var listItem = new TagBuilder("li");
                    listItem.SetInnerText(bulletPoint);
                    return agg.Append(listItem.ToString());
                });

                var list = new TagBuilder("ul") { InnerHtml = listItems.ToString() };
                bulletPointsHtml = list.ToString();
            }

            return bulletPointsHtml + contentHtml + applyHtmlBottom;
        }

        private static JobType MapJobType(JobTypes jobTypes)
        {
            if (jobTypes.IsFlagSet(JobTypes.FullTime))
                return new JobType { id = 4, name = "Full time" };

            if (jobTypes.IsFlagSet(JobTypes.PartTime))
                return new JobType { id = 1, name = "Part time" };

            if (jobTypes.IsFlagSet(JobTypes.Contract))
                return new JobType { id = 2, name = "Contract" };

            return new JobType { id = 4, name = "Full time" };
        }
    }
}

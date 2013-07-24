using System;
using System.Linq;
using System.ServiceModel.Syndication;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Services.External.PageUp
{
    public class JobFeedMapper
        : IJobFeedMapper<SyndicationItem>
    {
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly Country _australia;

        public JobFeedMapper(IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
        {
            _industriesQuery = industriesQuery;
            _locationQuery = locationQuery;
            _australia = _locationQuery.GetCountry("Australia");
        }

        string IJobFeedMapper<SyndicationItem>.GetPostId(SyndicationItem post)
        {
            return post.Id;
        }

        bool IJobFeedMapper<SyndicationItem>.IsIgnored(SyndicationItem post)
        {
            return false;
        }

        void IJobFeedMapper<SyndicationItem>.ApplyPostData(SyndicationItem post, JobAd jobAd)
        {
            jobAd.Title = post.Title.Text;
            jobAd.CreatedTime = post.PublishDate.ToLocalTime().DateTime;
            jobAd.LastUpdatedTime = DateTime.Now;

            // Truncate to the maximum size.

            jobAd.Description.Summary = string.IsNullOrEmpty(post.Summary.Text)
                ? null
                : post.Summary.Text.Length > Domain.Roles.JobAds.Constants.MaxSummaryLength
                    ? TextUtil.TruncateForDisplay(post.Summary.Text, Domain.Roles.JobAds.Constants.MaxSummaryLength)
                    : post.Summary.Text;

            string suburb = null;
            string state = null;
            string postCode = null;

            foreach (var extension in post.ElementExtensions.Where(e => e.OuterNamespace == "http://www.pageuppeople.com"))
            {
                switch (extension.OuterName)    
                {
                    case "closingDate":
                        GetValue(extension, value => jobAd.ExpiryTime = value);
                        break;

                    case "refNo":
                        jobAd.Integration.ExternalReferenceId = extension.GetObject<string>();
                        break;

                    case "applyLink":
                        jobAd.Integration.ExternalApplyUrl = extension.GetObject<string>(); 
                        break;

                    case "description":
                        jobAd.Description.Content = extension.GetObject<string>();
                        break;

                    case "workType":
                        GetValue(extension, value => jobAd.Description.JobTypes = value);
                        break;

                    case "category":
                        GetValue(extension, value => jobAd.Description.Industries = new[] { value });
                        break;

                    case "siteSuburb":
                        suburb = extension.GetObject<string>();
                        break;

                    case "siteState":
                        state = extension.GetObject<string>();
                        break;

                    case "sitePostCode":
                        postCode = extension.GetObject<string>();
                        break;
                }
            }

            jobAd.Description.Location = _locationQuery.ResolveLocation(_australia, string.Format("{0} {1} {2}",
               suburb, state, postCode));
        }

        private static bool GetValue(SyndicationElementExtension extension, Action<DateTime> assign)
        {
            var str = extension.GetObject<string>();
            if (string.IsNullOrEmpty(str))
                return false;

            assign(DateTimeOffset.Parse(str).ToLocalTime().DateTime);
            return true;
        }

        private static bool GetValue(SyndicationElementExtension extension, Action<JobTypes> assign)
        {
            var str = extension.GetObject<string>();
            if (string.IsNullOrEmpty(str))
                return false;

            assign((JobTypes)Enum.Parse(typeof(JobTypes), str));
            return true;
        }

        private bool GetValue(SyndicationElementExtension extension, Action<Industry> assign)
        {
            var str = extension.GetObject<string>();
            if (string.IsNullOrEmpty(str))
                return false;

            var industry = _industriesQuery.GetIndustry(str);
            if (industry == null)
                return false;

            assign(industry);
            return true;
        }
    }
}

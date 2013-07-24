using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds
{
    public static class JobAdsExtensions
    {
        public static JobAd CopyForPosting(this IJobAdsCommand jobAdsCommand, JobAd jobAd)
        {
            var newJobAd = new JobAd();
            //jobAd.CopyTo(newJobAd);

            // Reset the expiry time and prepare the job ad so that times are correctly set when showing it.

            newJobAd.CreatedTime = DateTime.MinValue;
            newJobAd.LastUpdatedTime = DateTime.MinValue;
            newJobAd.ExpiryTime = DateTime.MinValue;
            //jobAdsCommand.PrepareJobAd(newJobAd);

            return newJobAd;
        }

        public static string GetLocationDisplayText(this JobAd jobAd)
        {
            if (jobAd.Description.Location == null)
                return string.Empty;
            return jobAd.Description.Location.IsCountry ? jobAd.Description.Location.Country.ToString() : jobAd.Description.Location.ToString();
        }

        public static string GetLocationDisplayText(this JobAdView jobAd)
        {
            if (jobAd.Description.Location == null)
                return string.Empty;
            return jobAd.Description.Location.IsCountry ? jobAd.Description.Location.Country.ToString() : jobAd.Description.Location.ToString();
        }

        public static string GetPostedDisplayText(this JobAd jobAd)
        {
            var age = DateTime.Now - jobAd.CreatedTime;

            string rez;
            if (age.Days > 0)
            {
                var text = age.Days + (age.Days == 1 ? " day" : " days");
                if (age.Hours > 0)
                    text += string.Format(" {0} {1}", age.Hours, (age.Hours == 1 ? " hour" : " hours"));
                rez = text + " ago";
            }
            else if (age.Hours > 0)
                rez = age.Hours + (age.Hours == 1 ? " hour" : " hours") + " ago";
            else
                rez = "less than an hour ago";

            return "Posted " + rez;
        }

        public static string GetPostedDisplayText(this JobAdView jobAd)
        {
            var age = DateTime.Now - jobAd.CreatedTime;

            string rez;
            if (age.Days > 0)
            {
                var text = age.Days + (age.Days == 1 ? " day" : " days");
                if (age.Hours > 0)
                    text += string.Format(" {0} {1}", age.Hours, (age.Hours == 1 ? " hour" : " hours"));
                rez = text + " ago";
            }
            else if (age.Hours > 0)
                rez = age.Hours + (age.Hours == 1 ? " hour" : " hours") + " ago";
            else
                rez = "less than an hour ago";

            return "Posted " + rez;
        }

        public static string GetTitleAndReferenceDisplayText(this JobAdEntry jobAd)
        {
            return jobAd.Integration == null || string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId) ? jobAd.Title : jobAd.Title + " - #" + jobAd.Integration.ExternalReferenceId;
        }
        
        public static string GetContactDetailsDisplayText(this ContactDetails contactDetails)
        {
            var sb = new StringBuilder();
            if (contactDetails != null && !string.IsNullOrEmpty(contactDetails.FullName))
                sb.Append(contactDetails.FullName);

            if (contactDetails != null && !string.IsNullOrEmpty(contactDetails.CompanyName))
            {
                if (sb.Length > 0)
                    sb.Append(" at " + contactDetails.CompanyName);
                else
                    sb.Append(contactDetails.CompanyName);
            }

            if (contactDetails != null && !string.IsNullOrEmpty(contactDetails.PhoneNumber))
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(contactDetails.PhoneNumber);
            }
            
            return sb.ToString();
        }

        public static string GetJobRelativePath(this JobAd jobAd)
        {
            var sb = new StringBuilder();

            // Location.

            var location = jobAd.GetLocationDisplayText();
            sb.Append(!string.IsNullOrEmpty(location)
                ? location.EncodeUrlSegment()
                : "-");

            // Industry. If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industrySb = new StringBuilder();

            var industries = jobAd.Description.Industries;
            if (industries != null && industries.Count == 1)
            {
                var industry = industries[0];
                industrySb.Append(industry.UrlName);
            }

            sb.Append("/");
            if (industrySb.Length == 0)
                sb.Append("-");
            else
                sb.Append(industrySb);

            // Job title.

            sb.Append("/");
            sb.Append(jobAd.Title.EncodeUrlSegment());

            // Id

            sb.Append("/");
            sb.Append(jobAd.Id.ToString());

            return sb.ToString();
        }

        public static string GenerateUrl(this JobAd jobAd)
        {
            return string.Format("~/jobs/{0}/{1}/{2}/{3}", GetLocation(jobAd), GetIndustry(jobAd), GetTitle(jobAd), jobAd.Id);
        }

        private static string GetTitle(JobAdEntry jobAd)
        {
            var title = jobAd.Title.EncodeUrlSegment();
            return string.IsNullOrEmpty(title) ? "-" : title;
        }

        private static string GetIndustry(JobAd jobAd)
        {
            // If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industries = jobAd.Description.Industries;
            if (industries != null && industries.Count == 1)
                return industries[0].UrlName;
            return "-";
        }

        private static string GetLocation(JobAd jobAd)
        {
            if (jobAd.Description.Location == null)
                return "-";
            var location = jobAd.Description.Location.IsCountry
                ? jobAd.Description.Location.Country.ToString()
                : jobAd.Description.Location.ToString();
            location = location.EncodeUrlSegment();
            return string.IsNullOrEmpty(location) ? "-" : location;
        }
    }
}

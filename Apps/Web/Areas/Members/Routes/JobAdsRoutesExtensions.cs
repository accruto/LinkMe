using System;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class JobAdsRoutesExtensions
    {
        private const string ExternalApplicationIdParameter = "linkmeApplicationId";
        private const string ExternalApplicationUriParameter = "linkmeApplicationUri";
        private const string LocationKey = "locationSegment";
        private const string IndustryKey = "industrySegment";
        private const string TitleKey = "titleSegment";
        private const string JobAdIdKey = "jobAdId";
        private const string PageKey = "page";

        // Need to ensure total path length is less than MAX_PATH (260) characters.
        // Url has form: ~/jobs/{location}/{industry}/{title}/{id}/default.aspx
        // which means path has form:   C:\LinkMe\Web\jobs\{location}\{industry}\{title}\{id}\default.aspx
        // which has lengths:           {2}{1}{6}{1}{3}{1}{4}{1}{<=50}{1}{<=35}{1}{x}{1}{36}{1}{12}
        // which means x <= 260 - 156 ~= 100

        private const int MaxTitleSegmentLength = 100;

        public static MvcHtmlString JobAdsRouteLink(this HtmlHelper html, string linkText, Industry industry, object htmlAttributes)
        {
            return html.RouteRefLink(linkText, JobAdsRoutes.IndustryJobAds, GetRouteValues(industry), htmlAttributes);
        }

        public static MvcHtmlString JobAdsRouteLink(this HtmlHelper html, string linkText, IUrlNamedLocation location, object htmlAttributes)
        {
            return html.RouteRefLink(linkText, JobAdsRoutes.LocationJobAds, GetRouteValues(location), htmlAttributes);
        }

        public static MvcHtmlString JobAdsRouteLink(this HtmlHelper html, string linkText, IUrlNamedLocation location, Industry industry, object htmlAttributes)
        {
            return html.RouteRefLink(linkText, JobAdsRoutes.LocationIndustryJobAds, GetRouteValues(industry, location), htmlAttributes);
        }

        public static ReadOnlyUrl GenerateJobAdsUrl(this Industry industry)
        {
            return JobAdsRoutes.IndustryJobAds.GenerateUrl(GetRouteValues(industry));
        }

        public static ReadOnlyUrl GenerateJobAdsUrl(this IUrlNamedLocation location)
        {
            return JobAdsRoutes.LocationJobAds.GenerateUrl(GetRouteValues(location));
        }

        public static ReadOnlyUrl GenerateJobAdsUrl(this IUrlNamedLocation location, Industry industry, int? page)
        {
            return page == null || page.Value <= 1
                ? JobAdsRoutes.LocationIndustryJobAds.GenerateUrl(GetRouteValues(industry, location))
                : JobAdsRoutes.PagedLocationIndustryJobAds.GenerateUrl(GetRouteValues(industry, location, page.Value));
        }

        public static ReadOnlyUrl GenerateJobAdUrl(this JobAd jobAd)
        {
            return JobAdsRoutes.JobAd.GenerateUrl(GetRouteValues(jobAd));
        }

        public static ReadOnlyUrl GenerateJobAdUrl(this JobAdView jobAd)
        {
            return JobAdsRoutes.JobAd.GenerateUrl(GetRouteValues(jobAd));
        }

        public static ReadOnlyUrl GetExternalApplyUrl(this IJobAd jobAd, Guid? applicationId)
        {
            if (applicationId == null)
                return new ReadOnlyUrl(jobAd.Integration.ExternalApplyUrl);

            return new ReadOnlyUrl(
                jobAd.Integration.ExternalApplyUrl,
                new ReadOnlyQueryString(
                    ExternalApplicationIdParameter, applicationId.Value.ToString("n"),
                    ExternalApplicationUriParameter, Integration.Routes.JobAdsRoutes.Application.GenerateUrl(new { applicationId = applicationId.Value }).AbsoluteUri));
        }

        private static RouteValueDictionary GetRouteValues(JobAd jobAd)
        {
            return new RouteValueDictionary
            {
                { LocationKey, jobAd.Description.Location.GetUrlSegment(null) },
                { IndustryKey, jobAd.Description.Industries.GetUrlSegment(null) },
                { TitleKey, GetTitleSegment(jobAd.Title) },
                { JobAdIdKey, jobAd.Id.ToString() },
            };
        }

        private static RouteValueDictionary GetRouteValues(JobAdView jobAd)
        {
            return new RouteValueDictionary
            {
                { LocationKey, jobAd.Description.Location.GetUrlSegment(null) },
                { IndustryKey, jobAd.Description.Industries.GetUrlSegment(null) },
                { TitleKey, GetTitleSegment(jobAd.Title) },
                { JobAdIdKey, jobAd.Id.ToString() },
            };
        }

        private static string GetTitleSegment(string title)
        {
            var segment = title.GetTitleUrlSegment(null);
            return segment.Length <= MaxTitleSegmentLength
                ? segment
                : segment.Substring(0, MaxTitleSegmentLength);
        }

        private static RouteValueDictionary GetRouteValues(IUrlNamedLocation location)
        {
            return new RouteValueDictionary
            {
                {LocationKey, location.GetUrlSegment(JobAdsRoutes.SegmentSuffix)}
            };
        }

        private static RouteValueDictionary GetRouteValues(Industry industry)
        {
            return new RouteValueDictionary
            {
                {IndustryKey, industry.GetUrlSegment(JobAdsRoutes.SegmentSuffix)}
            };
        }

        private static RouteValueDictionary GetRouteValues(Industry industry, IUrlNamedLocation location)
        {
            return new RouteValueDictionary
            {
                {IndustryKey, industry.GetUrlSegment(JobAdsRoutes.SegmentSuffix)}, 
                {LocationKey, location.GetUrlSegment(JobAdsRoutes.SegmentSuffix)}
            };
        }

        private static RouteValueDictionary GetRouteValues(Industry industry, IUrlNamedLocation location, int page)
        {
            return new RouteValueDictionary
            {
                {IndustryKey, industry.GetUrlSegment(JobAdsRoutes.SegmentSuffix)},
                {LocationKey, location.GetUrlSegment(JobAdsRoutes.SegmentSuffix)},
                {PageKey, page}
            };
        }
    }
}
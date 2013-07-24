using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class CandidatesRoutesExtensions
    {
        private const string LocationKey = "locationSegment";
        private const string SalaryKey = "salarySegment";
        private const string TitleKey = "titleSegment";
        private const string CandidateIdKey = "candidateId";
        private const string PageKey = "page";

        public static MvcHtmlString CandidatesRouteLink(this HtmlHelper html, string linkText, Salary salary, object htmlAttributes)
        {
            return html.RouteRefLink(linkText, CandidatesRoutes.SalaryBandCandidates, GetRouteValues(salary), htmlAttributes);
        }

        public static MvcHtmlString CandidatesRouteLink(this HtmlHelper html, string linkText, IUrlNamedLocation location, object htmlAttributes)
        {
            return html.RouteRefLink(linkText, CandidatesRoutes.LocationCandidates, GetRouteValues(location), htmlAttributes);
        }

        public static MvcHtmlString CandidatesRouteLink(this HtmlHelper html, string linkText, IUrlNamedLocation location, Salary salary, int? page, object htmlAttributes)
        {
            if (!page.HasValue || page.Value <= 1)
                return html.RouteRefLink(linkText, CandidatesRoutes.LocationSalaryBandCandidates, GetRouteValues(location, salary), htmlAttributes);

            return html.RouteRefLink(linkText, CandidatesRoutes.PagedLocationSalaryBandCandidates, GetRouteValues(location, salary, page.Value), htmlAttributes);
        }

        public static ReadOnlyUrl GenerateCandidatesUrl(this Salary salary)
        {
            return CandidatesRoutes.SalaryBandCandidates.GenerateUrl(GetRouteValues(salary));
        }

        public static ReadOnlyUrl GenerateCandidatesUrl(this IUrlNamedLocation location)
        {
            return CandidatesRoutes.LocationCandidates.GenerateUrl(GetRouteValues(location));
        }

        public static ReadOnlyUrl GenerateCandidatesUrl(this IUrlNamedLocation location, Salary salary, int? page)
        {
            return page == null || page.Value <= 1
                ? CandidatesRoutes.LocationSalaryBandCandidates.GenerateUrl(GetRouteValues(location, salary))
                : CandidatesRoutes.PagedLocationSalaryBandCandidates.GenerateUrl(GetRouteValues(location, salary, page.Value));
        }

        public static ReadOnlyUrl GenerateCandidateUrl(this EmployerMemberView view)
        {
            return CandidatesRoutes.Candidate.GenerateUrl(GetRouteValues(view));
        }

        private static RouteValueDictionary GetRouteValues(EmployerMemberView view)
        {
            return new RouteValueDictionary
            {
                { LocationKey, (view.Address == null ? null : view.Address.Location).GetUrlSegment(null) },
                { SalaryKey, view.DesiredSalary.GetUrlSegment(null) },
                { TitleKey, view.DesiredJobTitle.GetTitleUrlSegment(null) },
                { CandidateIdKey, view.Id.ToString() },
            };
        }

        private static RouteValueDictionary GetRouteValues(IUrlNamedLocation location)
        {
            return new RouteValueDictionary
            {
                { LocationKey, location.GetUrlSegment(CandidatesRoutes.SegmentSuffix) }
            };
        }

        private static RouteValueDictionary GetRouteValues(Salary salary)
        {
            return new RouteValueDictionary
            {
                { SalaryKey, salary.GetUrlSegment(CandidatesRoutes.SegmentSuffix) }
            };
        }

        private static RouteValueDictionary GetRouteValues(IUrlNamedLocation location, Salary salary)
        {
            return new RouteValueDictionary
            {
                { SalaryKey, salary.GetUrlSegment(CandidatesRoutes.SegmentSuffix) },
                { LocationKey, location.GetUrlSegment(CandidatesRoutes.SegmentSuffix) }
            };
        }

        private static RouteValueDictionary GetRouteValues(IUrlNamedLocation location, Salary salary, int page)
        {
            return new RouteValueDictionary
               {
                   {SalaryKey, salary.GetUrlSegment(CandidatesRoutes.SegmentSuffix)},
                   {LocationKey, location.GetUrlSegment(CandidatesRoutes.SegmentSuffix)},
                   {PageKey, page}
               };
        }
    }
}

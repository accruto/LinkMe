using System;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Routes;

namespace LinkMe.Web.Areas.Members.Views.Shared
{
    public class Pagination
        : ViewUserControl<JobAdListModel>
    {
        private const int MaxPages = 11;

        protected int Start { get; private set; }
        protected int End { get; private set; }
        protected int Total { get; private set; }
        protected int CurrentPage { get; private set; }
        protected int TotalPages { get; private set; }
        protected int FirstPage { get; private set; }
        protected int LastPage { get; private set; }
        protected int ItemsPerPage { get; private set; }

        private IUrlNamedLocation _location;
        private Industry _industry;

        protected ReadOnlyUrl GetUrl(int page)
        {
            return _location != null && _industry != null
                ? _location.GenerateJobAdsUrl(_industry, page)
                : SearchRoutes.Results.GenerateUrl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Total = Model.Results.TotalJobAds;

            // Determine start and end.

            ItemsPerPage = Model.Presentation.Pagination.Items ?? Reference.DefaultItemsPerPage;

            CurrentPage = Model.Presentation.Pagination.Page ?? 1;
            TotalPages = (int)Math.Ceiling((double)Total / ItemsPerPage);

            // Only show a maximum number of page links.

            if (TotalPages <= MaxPages)
            {
                FirstPage = 1;
                LastPage = TotalPages;
            }
            else
            {
                if (CurrentPage < MaxPages / 2 + 1)
                {
                    // If at left end ...

                    FirstPage = 1;
                    LastPage = MaxPages;
                }
                else if (CurrentPage > TotalPages - MaxPages / 2)
                {
                    // If at right end ...

                    FirstPage = TotalPages - MaxPages + 1;
                    LastPage = TotalPages;
                }
                else
                {
                    // If in the middle ...

                    FirstPage = CurrentPage < MaxPages / 2 + 1 ? 1 : CurrentPage - MaxPages / 2;
                    LastPage = CurrentPage > TotalPages - MaxPages / 2 ? TotalPages : CurrentPage + MaxPages / 2;
                }
            }

            Start = ((CurrentPage - 1) * ItemsPerPage) + 1;
            End = Start + ItemsPerPage - 1;
            if (End > Total)
                End = Total;

            // Set up browse parameters.

            if (Model.ListType == JobAdListType.BrowseResult)
            {
                // Extract the location and industry from the criteria.

                var criteria = ((JobAdSearchListModel) Model).Criteria;
                var location = criteria.Location;
                if (location.IsFullyResolved)
                    _location = location.NamedLocation as IUrlNamedLocation;

                if (!criteria.IndustryIds.IsNullOrEmpty() && criteria.IndustryIds.Count == 1)
                    _industry = (from i in Model.Industries where i.Id == criteria.IndustryIds[0] select i).SingleOrDefault();
            }
        }
    }
}

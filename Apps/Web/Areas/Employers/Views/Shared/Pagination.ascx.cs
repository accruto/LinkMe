using System;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Employers.Views.Shared
{
    public class Pagination
        : ViewUserControl<CandidateListModel>
    {
        private const int MaxPages = 11;

        protected int Start { get; private set; }
        protected int End { get; private set; }
        protected int Total { get; private set; }
        protected int CurrentPage { get; private set; }
        protected int TotalPages { get; private set; }
        protected int FirstPage { get; private set; }
        protected int LastPage { get; private set; }

        private IUrlNamedLocation _location;
        private Salary _salary;

        protected ReadOnlyUrl GetUrl(int page)
        {
            return _location != null && _salary != null
                ? _location.GenerateCandidatesUrl(_salary, page)
                : SearchRoutes.Results.GenerateUrl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Total = Model.Results.TotalCandidates;

            // Determine start and end.

            var items = Model.Presentation.Pagination.Items ?? Model.Results.CandidateIds.Count;

            CurrentPage = Model.Presentation.Pagination.Page ?? 1;
            TotalPages = (int)Math.Ceiling((double)Total / items);

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

            Start = ((CurrentPage - 1) * items) + 1;
            End = Start + Model.Results.CandidateIds.Count - 1;

            // Set up browse parameters.

            if (Model is BrowseListModel)
            {
                // Extract the location and industry from the criteria.

                var location = Model.Criteria.Location;
                if (location.IsFullyResolved)
                    _location = location.NamedLocation as IUrlNamedLocation;

                _salary = Model.Criteria.Salary;
            }
        }
    }
}

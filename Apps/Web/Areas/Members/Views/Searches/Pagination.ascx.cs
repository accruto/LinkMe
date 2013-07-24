using System;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Search;

namespace LinkMe.Web.Areas.Members.Views.Searches
{
    public class Pagination
        : ViewUserControl<SearchesModel>
    {
        private const int MaxPages = 11;

        protected int Start { get; private set; }
        protected int End { get; private set; }
        protected int Total { get; private set; }
        protected int CurrentPage { get; private set; }
        protected int TotalPages { get; private set; }
        protected int FirstPage { get; private set; }
        protected int LastPage { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Total = Model.TotalItems;

            // Determine start and end.

            var itemsPerPage = Model.Pagination.Items ?? Reference.DefaultItemsPerPage;

            CurrentPage = Model.Pagination.Page ?? 1;
            TotalPages = (int)Math.Ceiling((double)Total / itemsPerPage);

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

            Start = ((CurrentPage - 1) * itemsPerPage) + 1;
            End = Start + itemsPerPage - 1;
        }
    }
}

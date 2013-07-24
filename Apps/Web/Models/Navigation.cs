using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Web.Models
{
    public abstract class Navigation
    {
        private readonly Pagination _pagination;

        protected Navigation(PresentationModel presentation)
        {
            _pagination = new Pagination
            {
                Page = presentation == null || presentation.Pagination == null ? null : presentation.Pagination.Page,
                Items = presentation == null || presentation.Pagination == null ? null : presentation.Pagination.Items
            };
        }

        public Pagination Pagination
        {
            get { return new Pagination { Page = _pagination.Page, Items = _pagination.Items }; }
        }
    }
}
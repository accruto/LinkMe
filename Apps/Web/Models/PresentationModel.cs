using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Web.Models
{
    public abstract class PresentationModel
    {
        public Pagination Pagination { get; set; }
        public IList<int> ItemsPerPage { get; set; }
        public int? DefaultItemsPerPage { get; set; }
    }
}
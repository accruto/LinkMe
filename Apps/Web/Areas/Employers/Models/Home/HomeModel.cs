using LinkMe.Apps.Asp.Security;

namespace LinkMe.Web.Areas.Employers.Models.Home
{
    public class HomeModel
    {
        public Login Login { get; set; }
        public bool AcceptTerms { get; set; }
        public ReferenceModel Reference { get; set; }
        public string AppStoreUrl { get; set; }
    }
}

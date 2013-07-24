using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Web.Areas.Shared.Models
{
    public class JobAdViewModel
    {
        public MemberJobAdView JobAd { get; set; }
        public IUser JobPoster { get; set; }
        public string OrganisationCssFile { get; set; }
    }
}
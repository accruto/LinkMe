using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class JobAdQuestionsModel
    {
        public JobAdView JobAd { get; set; }
        public InternalApplication Application { get; set; }
        public Member Member { get; set; }
        public AnonymousContact AnonymousContact { get; set; }
        public string CoverLetterText { get; set; }
    }
}

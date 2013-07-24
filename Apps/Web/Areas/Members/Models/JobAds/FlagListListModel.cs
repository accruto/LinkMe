using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class FlagListListModel
        : JobAdSortListModel
    {
        public JobAdFlagList FlagList { get; set; }
    }
}

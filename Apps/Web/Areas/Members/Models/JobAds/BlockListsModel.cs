using System;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class BlockListsModel
    {
        public JobAdBlockList BlockList { get; set; }
        public Tuple<Guid, int> BlockListCount { get; set; }
    }

    public class BlockListListModel
        : JobAdSortListModel
    {
        public JobAdBlockList BlockList { get; set; }
    }
}

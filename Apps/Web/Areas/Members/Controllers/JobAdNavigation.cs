using System;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Controllers
{
    public class FolderNavigation
        : JobAdsNavigation
    {
        public Guid FolderId { get; private set; }

        public FolderNavigation(Guid folderId, JobAdsPresentationModel presentation)
            : base(presentation)
        {
            FolderId = folderId;
        }
    }

    public class JobAdNavigation
        : JobAdsNavigation
    {
        public Guid JobAdId { get; private set; }

        public JobAdNavigation(Guid jobAdId, JobAdsPresentationModel presentation)
            : base(presentation)
        {
            JobAdId = jobAdId;
        }
    }

    public class FlagListNavigation
        : JobAdsNavigation
    {
        public FlagListNavigation(JobAdsPresentationModel presentation)
            : base(presentation)
        {
        }
    }

    public class BlockListNavigation
        : JobAdsNavigation
    {
        public BlockListNavigation(JobAdsPresentationModel presentation)
            : base(presentation)
        {
        }
    }
}

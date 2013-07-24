using System;
using System.Collections.Generic;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class FolderDataModel
    {
        public int Count { get; set; }
        public bool CanRename { get; set; }
    }

    public class FoldersModel
    {
        public JobAdFlagList FlagList { get; set; }
        public JobAdFolder MobileFolder { get; set; }
        public IList<JobAdFolder> PrivateFolders { get; set; }
        public IDictionary<Guid, FolderDataModel> FolderData { get; set; }
    }

    public class FolderListModel
        : JobAdSortListModel
    {
        public JobAdFolder Folder { get; set; }
    }
}

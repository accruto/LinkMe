using System;
using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class FolderDataModel
    {
        public int Count { get; set; }
        public bool CanRename { get; set; }
        public bool CanDelete { get; set; }
    }

    public class FoldersModel
    {
        public CandidateFlagList FlagList { get; set; }
        public CandidateFolder ShortlistFolder { get; set; }
        public IList<CandidateFolder> PrivateFolders { get; set; }
        public IList<CandidateFolder> SharedFolders { get; set; }
        public IDictionary<Guid, FolderDataModel> FolderData { get; set; }
    }

    public class FolderListModel
        : CandidateListModel
    {
        public CandidateFolder Folder { get; set; }
        public FolderDataModel FolderData { get; set; }
        public MemberSearchNavigation CurrentSearch { get; set; }
    }
}

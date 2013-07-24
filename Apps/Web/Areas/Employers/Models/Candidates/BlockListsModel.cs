using System;
using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class BlockListsModel
    {
        public CandidateBlockList TemporaryBlockList { get; set; }
        public CandidateBlockList PermanentBlockList { get; set; }
        public IDictionary<Guid, int> BlockListCounts { get; set; }
    }

    public class BlockListListModel
        : CandidateListModel
    {
        public CandidateBlockList BlockList { get; set; }
        public MemberSearchNavigation CurrentSearch { get; set; }
    }
}

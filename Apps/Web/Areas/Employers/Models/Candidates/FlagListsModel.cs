using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class FlagListListModel
        : CandidateListModel
    {
        public CandidateFlagList FlagList { get; set; }
        public MemberSearchNavigation CurrentSearch { get; set; }
    }
}

using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class ViewCandidateModel
    {
        public EmployerMemberView View { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public InternalApplication CurrentApplication { get; set; }
        public ApplicantStatus? ApplicantStatus { get; set; }
        public MemberSearchNavigation CurrentSearch { get; set; }
    }

    public class ViewCandidatesModel
    {
        public IList<Guid> CandidateIds { get; set; }
        public EmployerMemberViews Views { get; set; }
        public IDictionary<Guid, DateTime> LastUpdatedTimes { get; set; }
        public ViewCandidateModel CurrentCandidate { get; set; }
        public CandidatesNavigation CurrentCandidates { get; set; }
    }
}

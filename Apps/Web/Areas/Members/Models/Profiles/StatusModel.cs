using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class StatusModel
    {
        public CandidateStatus Status { get; set; }
    }

    public class StatusUpdatedModel
    {
        public CandidateStatus? PreviousStatus { get; set; }
        public CandidateStatus NewStatus { get; set; }
        public IDictionary<CandidateStatus, int?> ConfirmationDays { get; set; }
    }
}
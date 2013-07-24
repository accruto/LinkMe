using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Candidates
{
    public class Candidate
        : ICandidate
    {
        [IsSet]
        public Guid Id { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public CandidateStatus Status { get; set; }

        [StringLength(Constants.DesiredJobTitleMaxLength)]
        public string DesiredJobTitle { get; set; }

        public JobTypes DesiredJobTypes { get; set; }
        public Salary DesiredSalary { get; set; }

        public IList<Industry> Industries { get; set; }
        ReadOnlyCollection<Industry> ICandidate.Industries { get { return Industries == null ? null : Industries.ToReadOnlyCollection(); } }

        [Prepare, Validate]
        public IList<LocationReference> RelocationLocations { get; set; }
        ReadOnlyCollection<LocationReference> ICandidate.RelocationLocations { get { return RelocationLocations == null ? null : RelocationLocations.ToReadOnlyCollection(); } }
        public RelocationPreference RelocationPreference { get; set; }

        public EducationLevel? HighestEducationLevel { get; set; }
        public Seniority? RecentSeniority { get; set; }
        public Profession? RecentProfession { get; set; }
        public VisaStatus? VisaStatus { get; set; }

        public Guid? ResumeId { get; set; }
    }
}

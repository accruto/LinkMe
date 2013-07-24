using System;
using System.Collections.ObjectModel;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Domain.Roles.Candidates
{
    public interface ICandidate
    {
        Guid Id { get; }
        DateTime LastUpdatedTime { get; }

        CandidateStatus Status { get; }

        string DesiredJobTitle { get; }

        JobTypes DesiredJobTypes { get; }
        Salary DesiredSalary { get; }

        ReadOnlyCollection<Industry> Industries { get; }

        ReadOnlyCollection<LocationReference> RelocationLocations { get; }
        RelocationPreference RelocationPreference { get; }

        EducationLevel? HighestEducationLevel { get; }
        Seniority? RecentSeniority { get; }
        Profession? RecentProfession { get; }
        VisaStatus? VisaStatus { get; }

        Guid? ResumeId { get; }
    }
}

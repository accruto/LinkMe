using System;
using System.Collections.ObjectModel;

namespace LinkMe.Domain.Roles.Resumes
{
    public interface IJob
    {
        Guid Id { get; }
        PartialDateRange Dates { get; }
        bool IsCurrent { get; }
        string Title { get; }
        string Description { get; }
        string Company { get; }
    }

    public interface ISchool
    {
        Guid Id { get; }
        PartialCompletionDate CompletionDate { get; }
        bool IsCurrent { get; }
        string Institution { get; }
        string Degree { get; }
        string Major { get; }
        string Description { get; }
        string City { get; }
        string Country { get; }
    }

    public interface IResume
    {
        Guid Id { get; }
        bool IsEmpty { get; }
        DateTime CreatedTime { get; }
        DateTime LastUpdatedTime { get; }
        string Skills { get; }
        string Objective { get; }
        string Summary { get; }
        string Other { get; }
        string Citizenship { get; }
        string Affiliations { get; }
        string Professional { get; }
        string Interests { get; }
        string Referees { get; }
        ReadOnlyCollection<ISchool> Schools { get; }
        ReadOnlyCollection<string> Courses { get; }
        ReadOnlyCollection<string> Awards { get; }
        ReadOnlyCollection<IJob> Jobs { get; }
        ReadOnlyCollection<IJob> CurrentJobs { get; }
        IJob PreviousJob { get; }
    }
}
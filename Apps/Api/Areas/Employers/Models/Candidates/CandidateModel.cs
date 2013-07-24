using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Candidates
{
    public class SalaryModel
    {
        public decimal? LowerBound { get; set; }
        public decimal? UpperBound { get; set; }
    }

    public class JobModel
    {
        public PartialDate? StartDate { get; set; }
        public PartialDate? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
    }

    public class CandidateModel
    {
        public Guid Id { get; set; }

        public CanContactStatus CanContact { get; set; }
        public CanContactStatus CanContactByPhone { get; set; }
        public bool HasBeenViewed { get; set; }
        public bool HasBeenAccessed { get; set; }
        public bool IsInMobileFolder { get; set; }

        public string FullName { get; set; }
        public IList<string> PhoneNumbers { get; set; }

        public CandidateStatus Status { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public string Location { get; set; }
        public SalaryModel DesiredSalary { get; set; }
        public string DesiredJobTitle { get; set; }
        public JobTypes DesiredJobTypes { get; set; }
        public IList<JobModel> Jobs { get; set; }
        public string Summary { get; set; }
    }

    public class CandidateResponseModel
        : JsonResponseModel
    {
        public CandidateModel Candidate { get; set; }
    }

    public class CandidatesResponseModel
        : JsonResponseModel
    {
        public int TotalCandidates { get; set; }
        public IList<CandidateModel> Candidates { get; set; }
    }
}
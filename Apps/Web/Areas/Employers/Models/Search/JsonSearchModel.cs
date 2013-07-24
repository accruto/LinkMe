using System;
using LinkMe.Domain;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public class JsonSchoolModel
    {
        public DateTime? CompletionDate { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
    }

    public class JsonJobModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }

    public class JsonCandidateModel
    {
        public Guid Id { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public string Skills { get; set; }
        public string Objective { get; set; }
        public string Summary { get; set; }
        public string Other { get; set; }
        public string Citizenship { get; set; }
        public string Affiliations { get; set; }
        public string Professional { get; set; }
        public string Interests { get; set; }
        public string Referees { get; set; }
        public JsonSchoolModel[] Schools { get; set; }
        public string[] Courses { get; set; }
        public string[] Awards { get; set; }
        public JsonJobModel[] Jobs { get; set; }
        public CandidateStatus Status { get; set; }
        public string DesiredJobTitle { get; set; }
        public JobTypes DesiredJobTypes { get; set; }
        public string DesiredSalary { get; set; }
        public EducationLevel? HighestEducationLevel { get; set; }
        public Seniority? RecentSeniority { get; set; }
        public Profession? RecentProfession { get; set; }
        public VisaStatus? VisaStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? PhotoId { get; set; }
        public EthnicStatus EthnicStatus { get; set; }
    }

    public class JsonSearchModel
    {
        public int TotalCandidates { get; set; }
        public JsonCandidateModel[] Candidates { get; set; }
    }
}

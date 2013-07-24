using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class JobModel
    {
        [DefaultNewGuid]
        public Guid? Id { get; set; }
        public PartialDate? StartDate { get; set; }
        public PartialDate? EndDate { get; set; }
        public bool? IsCurrent { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Company { get; set; }
        public string Description { get; set; }

        public bool IsEmpty
        {
            get
            {
                return StartDate == null
                    && EndDate == null
                    && IsCurrent == null
                    && string.IsNullOrEmpty(Company)
                    && string.IsNullOrEmpty(Title)
                    && string.IsNullOrEmpty(Description);
            }
        }
    }

    public class EmploymentHistoryMemberModel
    {
        public Profession? RecentProfession { get; set; }
        public Seniority? RecentSeniority { get; set; }
        public IList<Guid> IndustryIds { get; set; }
        public IList<JobModel> Jobs { get; set; }
    }

    public class EmploymentHistoryUpdateModel
    {
        public Profession? RecentProfession { get; set; }
        public Seniority? RecentSeniority { get; set; }
        public IList<Guid> IndustryIds { get; set; }
        public JobModel Job { get; set; }
    }

    public class EmploymentHistoryReferenceModel
    {
        public IList<Industry> Industries { get; set; }
        public IList<int?> Months { get; set; }
        public IList<int?> Years { get; set; }
    }

    public class EmploymentHistoryModel
    {
        public EmploymentHistoryMemberModel Member { get; set; }
        public EmploymentHistoryReferenceModel Reference { get; set; }
    }
}
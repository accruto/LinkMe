using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class SchoolModel
    {
        [DefaultNewGuid]
        public Guid? Id { get; set; }
        public PartialDate? EndDate { get; set; }
        public bool? IsCurrent { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string Institution { get; set; }
        public string Major { get; set; }
        public string City { get; set; }
        public string Description { get; set; }

        public bool IsEmpty
        {
            get
            {
                return EndDate == null
                    && IsCurrent == null
                    && string.IsNullOrEmpty(Degree)
                    && string.IsNullOrEmpty(Institution)
                    && string.IsNullOrEmpty(Major)
                    && string.IsNullOrEmpty(City)
                    && string.IsNullOrEmpty(Description);
            }
        }
    }

    public class EducationMemberModel
    {
        public EducationLevel? HighestEducationLevel { get; set; }
        public IList<SchoolModel> Schools { get; set; }
    }

    public class EducationUpdateModel
    {
        public EducationLevel? HighestEducationLevel { get; set; }
        public SchoolModel School { get; set; }
    }

    public class EducationReferenceModel
    {
        public IList<int?> Months { get; set; }
        public IList<int?> Years { get; set; }
    }

    public class EducationModel
    {
        public EducationMemberModel Member { get; set; }
        public EducationReferenceModel Reference { get; set; }
    }
}
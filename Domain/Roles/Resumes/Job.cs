using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Resumes
{
    /// <summary>
    /// No validations are put on this class.  We have a number of resumes
    /// sourced from different places at different times.  The data is not clean enough
    /// to have validations.
    /// </summary>
    public class Job
        : IJob, ICloneable
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public PartialDateRange Dates { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

        public bool IsCurrent
        {
            get { return Dates != null && Dates.End == null; }
        }

        public Job Clone()
        {
            return new Job
            {
                Title = Title,
                Description = Description,
                Company = Company,
                Dates = Dates == null
                    ? null
                    : Dates.End == null
                        ? (Dates.Start == null ? new PartialDateRange() : new PartialDateRange(Dates.Start.Value))
                        : new PartialDateRange(Dates.Start, Dates.End.Value),
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public bool IsEmpty
        {
            get
            {
                return Dates == null
                    && string.IsNullOrEmpty(Title)
                    && string.IsNullOrEmpty(Description)
                    && string.IsNullOrEmpty(Company);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Job))
                return false;
            return Equals((Job) obj);
        }

        public bool Equals(Job other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.Title, Title)
                && Equals(other.Description, Description)
                && Equals(other.Company, Company)
                && Equals(other.Dates, Dates);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id.GetHashCode();
                result = (result*397) ^ (Title != null ? Title.GetHashCode() : 0);
                result = (result*397) ^ (Description != null ? Description.GetHashCode() : 0);
                result = (result*397) ^ (Company != null ? Company.GetHashCode() : 0);
                result = (result*397) ^ (Dates != null ? Dates.GetHashCode() : 0);
                return result;
            }
        }
    }
}
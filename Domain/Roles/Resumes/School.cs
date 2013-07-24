using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Resumes
{
    /// <summary>
    /// No validations are put on this class.  We have a number of resumes
    /// sourced from different places at different times.  The data is not clean enough
    /// to have validations.
    /// </summary>
    public class School
        : ISchool, ICloneable
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public PartialCompletionDate CompletionDate { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public bool IsCurrent
        {
            get { return CompletionDate != null && CompletionDate.End == null; }
        }

        public School Clone()
        {
            return new School
            {
                CompletionDate = CompletionDate == null
                    ? null
                    : CompletionDate.End == null
                        ? new PartialCompletionDate()
                        : new PartialCompletionDate(CompletionDate.End.Value),
                Institution  = Institution,
                Degree = Degree,
                Major = Major,
                Description = Description,
                City = City,
                Country = Country,
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
                return CompletionDate == null
                    && string.IsNullOrEmpty(Institution)
                    && string.IsNullOrEmpty(Degree)
                    && string.IsNullOrEmpty(Major)
                    && string.IsNullOrEmpty(Description)
                    && string.IsNullOrEmpty(City)
                    && string.IsNullOrEmpty(Country);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (School))
                return false;
            return Equals((School) obj);
        }

        public bool Equals(School other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.CompletionDate, CompletionDate)
                && Equals(other.Institution, Institution)
                && Equals(other.Degree, Degree)
                && Equals(other.Major, Major)
                && Equals(other.Description, Description)
                && Equals(other.City, City)
                && Equals(other.Country, Country);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (CompletionDate != null ? CompletionDate.GetHashCode() : 0);
                result = (result*397) ^ (Institution != null ? Institution.GetHashCode() : 0);
                result = (result*397) ^ (Degree != null ? Degree.GetHashCode() : 0);
                result = (result*397) ^ (Major != null ? Major.GetHashCode() : 0);
                result = (result*397) ^ (Description != null ? Description.GetHashCode() : 0);
                result = (result*397) ^ (City != null ? City.GetHashCode() : 0);
                result = (result*397) ^ (Country != null ? Country.GetHashCode() : 0);
                return result;
            }
        }
    }
}
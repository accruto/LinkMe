using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Candidates
{
    public class DiaryEntry
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? TotalHours { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DiaryEntry);
        }

        public bool Equals(DiaryEntry other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Id.Equals(Id)
                && other.IsDeleted.Equals(IsDeleted)
                && Equals(other.Title, Title)
                && Equals(other.Description, Description)
                && other.TotalHours.Equals(TotalHours);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id.GetHashCode();
                result = (result * 397) ^ (IsDeleted.GetHashCode());
                result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                result = (result*397) ^ (Description != null ? Description.GetHashCode() : 0);
                result = (result*397) ^ (TotalHours.HasValue ? TotalHours.Value : 0).GetHashCode();
                return result;
            }
        }
    }
}

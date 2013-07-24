using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Candidates
{
    public class Diary
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Diary);
        }

        public bool Equals(Diary other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

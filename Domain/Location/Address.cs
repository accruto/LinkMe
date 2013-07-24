using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Location
{
    public class Address
        : ICloneable
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required, Prepare]
        public LocationReference Location { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public bool Equals(Address obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.Id.Equals(Id)
                && Equals(obj.Line1, Line1)
                && Equals(obj.Line2, Line2)
                && Equals(obj.Location, Location);
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Line1)
                       && string.IsNullOrEmpty(Line2)
                       && Location == null;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = Id.GetHashCode();
                result = (result*397) ^ (Line1 != null ? Line1.GetHashCode() : 0);
                result = (result*397) ^ (Line2 != null ? Line2.GetHashCode() : 0);
                result = (result*397) ^ (Location != null ? Location.GetHashCode() : 0);
                return result;
            }
        }

        #region ICloneable

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Address Clone()
        {
            return new Address
            {
                Line1 = Line1,
                Line2 = Line2,
                Location = Location == null ? null : Location.Clone(),
            };
        }

        #endregion
    }
}

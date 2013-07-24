using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public enum PhoneNumberType
    {
        Home = 1,
        Work = 2,
        Mobile = 3,
    }

    public class PhoneNumber
    {
        public PhoneNumberType Type { get; set; }
        [Required, PhoneNumber]
        public string Number { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == typeof(PhoneNumber) && Equals((PhoneNumber) obj);
        }

        public bool Equals(PhoneNumber other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.Type, Type)
                && Equals(other.Number, Number);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode()*397) ^ (Number != null ? Number.GetHashCode() : 0);
            }
        }
    }
}
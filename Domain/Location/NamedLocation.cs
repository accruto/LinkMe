using System;
using System.Runtime.Serialization;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Location
{
    [Serializable]
    [KnownType(typeof(CountrySubdivision))]
    [KnownType(typeof(Locality))]
    [KnownType(typeof(Region))]
    [KnownType(typeof(GeographicalArea))]
    [KnownType(typeof(PostalCode))]
    [KnownType(typeof(PostalSuburb))]
    public abstract class NamedLocation
        : IHasId<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as NamedLocation;
            if (other == null)
                return false;
            return Id == other.Id && GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        internal abstract GeographicalArea GetGeographicalArea();

        internal virtual Region GetRegion()
        {
            return null;
        }

        internal virtual Locality GetLocality()
        {
            return null;
        }

        internal virtual PostalCode GetPostalCode()
        {
            return null;
        }

        internal virtual PostalSuburb GetPostalSuburb()
        {
            return null;
        }

        public override string ToString()
        {
            return LocationString.ToString(this);
        }
    }
}

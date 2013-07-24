
using System;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class PostalSuburb
        : NamedLocation
    {
        public PostalCode PostalCode { get; set; }
        public CountrySubdivision CountrySubdivision { get; set; }

        public string ToStringPostcodeFirst()
        {
            return LocationString.ToString(this, false);
        }

        internal override Locality GetLocality()
        {
            return PostalCode.GetLocality();
        }

        internal override PostalCode GetPostalCode()
        {
            return PostalCode;
        }

        internal override PostalSuburb GetPostalSuburb()
        {
            return this;
        }

        internal override GeographicalArea GetGeographicalArea()
        {
            return PostalCode.GetGeographicalArea();
        }
    }
}
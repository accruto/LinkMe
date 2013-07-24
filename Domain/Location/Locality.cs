using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class Locality
        : GeographicalArea
    {
        public GeoCoordinates Centroid { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }

        internal override Locality GetLocality()
        {
            return this;
        }

        internal override GeographicalArea GetGeographicalArea()
        {
            return this;
        }
    }
}

using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class CountrySubdivision
        : GeographicalArea, IUrlNamedLocation
    {
        public Country Country { get; set; }
        public IList<CountrySubdivisionAlias> Aliases { get; set; }
        public int CircleRadiusKm { get; set; }
        public int? CircleCentreId { get; set; }
        public string ShortName { get; set; }
        public string UrlName { get; set; }

        /// <summary>
        /// There is one subdivision for each country representing just this whole country.  
        /// </summary>
        /// <returns>
        /// true if this subdivision a whole country.
        /// </returns>
        public bool IsCountry
        {
            get { return string.IsNullOrEmpty(Name); }
        }

        internal override GeographicalArea GetGeographicalArea()
        {
            return this;
        }
    }
}

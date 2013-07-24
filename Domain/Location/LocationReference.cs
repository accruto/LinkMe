using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class LocationReference
        : ICloneable
    {
        private string _unstructuredLocation;

        [DefaultNewGuid]
        public Guid Id { get; set; }
        public NamedLocation NamedLocation { get; set; }
        public CountrySubdivision CountrySubdivision { get; set; }
        public Locality Locality { get; set; }

        #region Constructors

        public LocationReference()
        {
        }

        internal LocationReference(string unstructuredLocation, NamedLocation namedLocation, CountrySubdivision countrySubdivision, Locality locality)
        {
            Set(unstructuredLocation, namedLocation, countrySubdivision, locality);
        }

        public LocationReference(NamedLocation namedLocation)
        {
            this.Set(namedLocation);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            var other = obj as LocationReference;
            if (other == null)
                return false;
            return UnstructuredLocation == other.UnstructuredLocation
                && Equals(NamedLocation, other.NamedLocation) 
                && Equals(CountrySubdivision, other.CountrySubdivision)
                && Equals(Locality, other.Locality);
        }

        public override int GetHashCode()
        {
            var hashCode = (UnstructuredLocation ?? string.Empty).GetHashCode()
                ^ (NamedLocation == null ? 0 : NamedLocation.GetHashCode())
                ^ (CountrySubdivision == null ? 0 : CountrySubdivision.GetHashCode())
                ^ (Locality == null ? 0 : Locality.GetHashCode());

            return hashCode;
        }

        #endregion

        #region Properties

        public string UnstructuredLocation
        {
            get { return _unstructuredLocation; }
            set { _unstructuredLocation = value == string.Empty ? null : value; }
        }

        public Country Country
        {
            get { return CountrySubdivision == null ? null : CountrySubdivision.Country; }
        }

        public Region Region
        {
            get { return NamedLocation == null ? null : NamedLocation.GetRegion(); }
        }

        public PostalCode PostalCode
        {
            get { return NamedLocation == null ? null : NamedLocation.GetPostalCode(); }
        }

        public PostalSuburb PostalSuburb
        {
            get { return NamedLocation == null ? null : NamedLocation.GetPostalSuburb(); }
        }

        public string Postcode
        {
            get
            {
                PostalCode postalCode = PostalCode;
                return postalCode == null ? null : postalCode.Postcode;
            }
        }

        public string Suburb
        {
            get
            {
                PostalSuburb postalSuburb = PostalSuburb;
                return postalSuburb == null ? null : postalSuburb.Name;
            }
        }

        public GeographicalArea GeographicalArea
        {
            get
            {
                if (NamedLocation != null)
                    return NamedLocation.GetGeographicalArea();
                if (Locality != null)
                    return Locality.GetGeographicalArea();
                if (CountrySubdivision != null)
                    return CountrySubdivision.GetGeographicalArea();
                return null;
            }
        }

        public bool IsCountry
        {
            get
            {
                // If the unstructuredLocation is set then it is not a country.
                // If the namedLocation is not a subdivision then it is not a country.

                if (UnstructuredLocation != null || !(NamedLocation is CountrySubdivision))
                    return false;

                // The namedLocation is a subdivision (and assumed to be equal to countrySubdivision.

                return CountrySubdivision == null ? false : CountrySubdivision.IsCountry;
            }
        }

        public bool IsFullyResolved
        {
            get
            {
                // If the country is not capable of resolving a location then this location
                // is considered as fully resolved as it is going to get.

                var country = Country;
                if (country == null)
                    return false;

                if (!country.CanResolveLocations)
                    return true;

                // If the unstructured location is not set but the named location is then it is fully resolved.

                return UnstructuredLocation == null && NamedLocation != null;
            }
        }

        #endregion

        #region Internal methods used for resolving

        internal void Set(string unstructuredLocation, NamedLocation namedLocation, CountrySubdivision countrySubdivision, Locality locality)
        {
            UnstructuredLocation = unstructuredLocation == string.Empty ? null : unstructuredLocation;
            NamedLocation = namedLocation;
            CountrySubdivision = countrySubdivision;
            Locality = locality;
        }

        #endregion

        #region ICloneable

        object ICloneable.Clone()
        {
            return Clone();
        }

        public LocationReference Clone()
        {
            var location = new LocationReference();
            CopyTo(location);
            return location;
        }

        public void CopyTo(LocationReference location)
        {
            location.UnstructuredLocation = UnstructuredLocation;
            location.NamedLocation = NamedLocation == null ? null : NamedLocation.Clone();
            location.CountrySubdivision = CountrySubdivision == null ? null : CountrySubdivision.Clone();
            location.Locality = Locality == null ? null : Locality.Clone();
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a string to display to the user for suburb, country subdivision (state) and postcode of an
        /// address. Does not include the country.
        /// </summary>
        /// 
        public override string ToString()
        {
            return ToString(true, true);
        }

        /// <summary>
        /// Returns a string to display to the user for suburb, country subdivision (state) and postcode of an
        /// address. Does not include the country.
        /// </summary>
        public string ToString(bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            if (UnstructuredLocation != null)
            {
                // If the "unstructured" location is set then it wasn't completely resolved.
                // Only return it though if suburb and subdivision access is provided.

                if (hasSubdivisionAccess && hasSubSubdivisionAccess)
                    return UnstructuredLocation;

                // Check the subdivision access to determine whether to return just that.

                if (hasSubdivisionAccess)
                    return CountrySubdivision == null ? string.Empty : (CountrySubdivision.ShortName ?? string.Empty);
                return string.Empty;
            }

            return NamedLocation == null ? string.Empty : LocationString.ToString(NamedLocation, CountrySubdivision, hasSubdivisionAccess, hasSubSubdivisionAccess);
        }

        #endregion
    }
}

using System;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class PostalCode
        : NamedLocation
    {
        public Locality Locality { get; set; }

        public string Postcode
        {
            get { return Name; }
        }

        internal override Locality GetLocality()
        {
            return Locality;
        }

        internal override PostalCode GetPostalCode()
        {
            return this;
        }

        internal override GeographicalArea GetGeographicalArea()
        {
            return Locality;
        }
    }
}

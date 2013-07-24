using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class Region
        : GeographicalArea, IUrlNamedLocation
    {
        public Country Country { get; set; }
        public string UrlName { get; set; }
        public bool IsMajorCity { get; set; }
        public IList<RegionAlias> Aliases { get; set; }

        internal override Region GetRegion()
        {
            return this;
        }

        internal override GeographicalArea GetGeographicalArea()
        {
            return this;
        }
    }
}

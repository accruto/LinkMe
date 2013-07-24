using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Verticals;

namespace LinkMe.Web.Areas.Administrators.Models.Communities
{
    public class CommunityModel
    {
        public Community Community { get; set; }
        public Vertical Vertical { get; set; }
        public IList<Country> Countries { get; set; }
    }
}
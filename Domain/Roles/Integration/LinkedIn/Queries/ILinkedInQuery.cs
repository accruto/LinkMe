using System;
using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Queries
{
    public interface ILinkedInQuery
    {
        LinkedInProfile GetProfile(string linkedInId);
        LinkedInProfile GetProfile(Guid userId);

        IList<Industry> GetIndustries(string industry);
        LocationReference GetLocation(string countryIsoCode, string location);
    }
}

using System;
using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Integration.LinkedIn
{
    public class LinkedInProfile
        : IHaveLocation
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Industry> Industries { get; set; }
        public LocationReference Location { get; set; }
        public string OrganisationName { get; set; }
    }
}

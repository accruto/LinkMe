using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Web.Models.Credits
{
    public class ExercisedCreditsModel
    {
        public IList<ExercisedCredit> ExercisedCredits { get; set; }
        public IDictionary<Guid, Allocation> Allocations { get; set; }
        public IDictionary<Guid, Credit> Credits { get; set; }
        public IDictionary<Guid, JobAd> JobAds { get; set; }
        public IDictionary<Guid, Member> Members { get; set; }
    }
}
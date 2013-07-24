using System;

namespace LinkMe.Apps.Agents.Featured
{
    public class FeaturedEmployer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int LogoOrder { get; set; }
    }
}

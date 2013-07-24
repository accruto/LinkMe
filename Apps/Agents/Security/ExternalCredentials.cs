using System;

namespace LinkMe.Apps.Agents.Security
{
    public class ExternalCredentials
    {
        public Guid ProviderId { get; set; }
        public string ExternalId { get; set; }
    }
}
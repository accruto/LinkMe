using System;

namespace LinkMe.Apps.Management.Areas.Communications.Models
{
    public class CommunicationsContext
    {
        public Guid ContextId { get; set; }
        public bool IsPreview { get; set; }
        public Guid UserId { get; set; }
        public string Definition { get; set; }
        public string Category { get; set; }
    }

    public class CommunicationsModel
    {
        public string Definition { get; set; }
        public string Category { get; set; }
        public Guid ContextId { get; set; }
        public bool IsPreview { get; set; }
        public Guid UserId { get; set; }
    }
}

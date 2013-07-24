using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Query.Search
{
    public abstract class SearchExecution
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        public Guid? ChannelId { get; set; }
        public Guid? AppId { get; set; }

        public Guid? SearcherId { get; set; }
        public Guid? SearchId { get; set; }
        public string SearcherIp { get; set; }

        [DefaultNow]
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Context { get; set; }
    }
}
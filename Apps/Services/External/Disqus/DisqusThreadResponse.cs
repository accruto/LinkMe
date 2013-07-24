using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Services.External.Disqus
{
    public class DisqusThreadResponse
    {
        public int Category { get; set; }
        public int Reactions { get; set; }
        public bool CanModerate { get; set; }
        public int Author { get; set; }
        public string Forum { get; set; }
        public string Title { get; set; }
        public int Dislikes { get; set; }
        public IList<Guid> Identifiers { get; set; }
        public int UserScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Slug { get; set; }
        public bool IsClosed { get; set; }
        public int Posts { get; set; }
        public bool UserSubscription { get; set; }
        public string Link { get; set; }
        public int Likes { get; set; }
        public bool CanPost { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DisqusThreadJsonModel
    {
        public int Code { get; set; }
        public DisqusThreadResponse Response { get; set; }
    }
}

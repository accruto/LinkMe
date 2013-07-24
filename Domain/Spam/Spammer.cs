using System;

namespace LinkMe.Domain.Spam
{
    public class Spammer
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SpammerReport
    {
        public Guid ReportedByUserId { get; set; }
        public DateTime ReportedTime { get; set; }
        public Spammer Spammer { get; set; }
    }
}

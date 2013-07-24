using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Resources
{
    public class PollAnswer
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
    }

    public class Poll
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public IList<PollAnswer> Answers { get; set; }
    }

    public class PollAnswerVote
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; }
        public Guid UserId { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}

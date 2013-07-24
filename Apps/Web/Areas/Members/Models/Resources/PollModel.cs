using System;
using System.Collections.Generic;
using LinkMe.Domain.Resources;

namespace LinkMe.Web.Areas.Members.Models.Resources
{
    public class PollModel
    {
        public Poll Poll { get; set; }
        public IDictionary<Guid, int> Votes { get; set; }

        public int GetPercentage(Guid answerId)
        {
            var total = 0;
            foreach (var answer in Poll.Answers)
                total += Votes[answer.Id];

            return total > 0
                ? Votes[answerId] * 100 / total
                : 0;
        }
    }
}

using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Resources.Queries
{
    public interface IPollsQuery
    {
        Poll GetPoll(string name);
        Poll GetActivePoll();
        IDictionary<Guid, int> GetPollAnswerVotes(Guid pollId);
    }
}
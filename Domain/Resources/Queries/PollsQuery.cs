using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Resources.Queries
{
    public class PollsQuery
        : IPollsQuery
    {
        private readonly IResourcesRepository _repository;

        public PollsQuery(IResourcesRepository repository)
        {
            _repository = repository;
        }

        Poll IPollsQuery.GetPoll(string name)
        {
            return _repository.GetPoll(name);
        }

        Poll IPollsQuery.GetActivePoll()
        {
            return _repository.GetActivePoll();
        }

        IDictionary<Guid, int> IPollsQuery.GetPollAnswerVotes(Guid pollId)
        {
            return _repository.GetPollAnswerVotes(pollId);
        }
    }
}

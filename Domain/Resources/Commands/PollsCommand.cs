using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources.Commands
{
    public class PollsCommand
        : IPollsCommand
    {
        private readonly IResourcesRepository _repository;

        public PollsCommand(IResourcesRepository repository)
        {
            _repository = repository;
        }

        void IPollsCommand.CreatePoll(Poll poll)
        {
            poll.Prepare();
            if (poll.Answers != null)
            {
                foreach (var answer in poll.Answers)
                    answer.Prepare();
            }

            poll.Validate();
            if (poll.Answers != null)
            {
                foreach (var answer in poll.Answers)
                    answer.Validate();
            }

            _repository.CreatePoll(poll);
        }

        void IPollsCommand.CreatePollAnswerVote(PollAnswerVote vote)
        {
            vote.Prepare();
            vote.Validate();
            _repository.CreatePollAnswerVote(vote);
        }
    }
}

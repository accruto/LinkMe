namespace LinkMe.Domain.Resources.Commands
{
    public interface IPollsCommand
    {
        void CreatePoll(Poll poll);
        void CreatePollAnswerVote(PollAnswerVote vote);
    }
}

using System;

namespace LinkMe.Domain.Spam.Commands
{
    public interface ISpamCommand
    {
        void CreateSpammer(Spammer spammer);
        void ReportSpammer(Guid reportedById, Spammer spammer);
    }
}
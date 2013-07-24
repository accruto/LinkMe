using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Spam
{
    public interface ISpamRepository
    {
        IList<string> GetSpamText();

        void CreateSpammer(Spammer spammer);
        void ReportSpammer(SpammerReport report);

        bool IsSpammer(Spammer possibleSpammer);
        IList<SpammerReport> GetSpammerReports(Guid userId, DateTime sinceTime);
    }
}

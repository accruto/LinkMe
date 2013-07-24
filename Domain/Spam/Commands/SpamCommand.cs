using System;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Spam.Commands
{
    public class SpamCommand
        : ISpamCommand
    {
        private const int ReportIntervalDays = 30;
        private const int ReportDistinctUsers = 3;
        private readonly ISpamRepository _repository;

        public SpamCommand(ISpamRepository repository)
        {
            _repository = repository;
        }

        void ISpamCommand.CreateSpammer(Spammer spammer)
        {
            spammer.Prepare();
            spammer.Validate();
            _repository.CreateSpammer(spammer);
        }

        void ISpamCommand.ReportSpammer(Guid reportedById, Spammer spammer)
        {
            // Create a report entry.

            spammer.Prepare();
            spammer.Validate();

            var report = new SpammerReport {ReportedByUserId = reportedById, ReportedTime = DateTime.Now, Spammer = spammer};
            _repository.ReportSpammer(report);

            // Check whether this user is classified as a spammer.

            if (IsReportedSpammer(spammer))
                _repository.CreateSpammer(spammer);
        }

        private bool IsReportedSpammer(Spammer spammer)
        {
            // To be classifed as a spammer it requires 3 reports from different users
            // within the last month, and they must be specified by id.

            if (spammer.UserId == null)
                return false;

            var sinceTime = DateTime.Now.Date.AddDays(-1 * ReportIntervalDays);
            var reports = _repository.GetSpammerReports(spammer.UserId.Value, sinceTime);

            return (from r in reports select r.ReportedByUserId).Distinct().Count() >= ReportDistinctUsers;
        }
    }
}
using System;

namespace LinkMe.Domain.Spam.Data
{
    internal static class Mappings
    {
        public static SpammerEntity Map(this Spammer spammer)
        {
            return new SpammerEntity
            {
                id = Guid.NewGuid(),
                userId = spammer.UserId,
                firstName = spammer.FirstName,
                lastName = spammer.LastName,
            };
        }

        public static SpammerReportEntity Map(this SpammerReport report)
        {
            return new SpammerReportEntity
            {
                id = Guid.NewGuid(),
                reportedByUserId = report.ReportedByUserId,
                reportedTime = report.ReportedTime,
                userId = report.Spammer.UserId,
                firstName = report.Spammer.FirstName,
                lastName = report.Spammer.LastName,
            };
        }

        public static SpammerReport Map(this SpammerReportEntity entity)
        {
            return new SpammerReport
            {
                ReportedByUserId = entity.reportedByUserId,
                ReportedTime = entity.reportedTime,
                Spammer = new Spammer
                {
                    UserId = entity.userId,
                    FirstName = entity.firstName,
                    LastName = entity.lastName,
                }
            };
        }
    }
}

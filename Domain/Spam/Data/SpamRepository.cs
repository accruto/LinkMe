using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Spam.Data
{
    public class SpamRepository
        : Repository, ISpamRepository
    {
        private static readonly Func<SpamDataContext, IQueryable<string>> GetSpamText
            = CompiledQuery.Compile((SpamDataContext dc)
                => from t in dc.SpamTextEntities
                   select t.pattern);

        private static readonly Func<SpamDataContext, Guid, bool> SpammerByUserIdExists
            = CompiledQuery.Compile((SpamDataContext dc, Guid userId)
                => (from s in dc.SpammerEntities
                    where s.userId == userId
                    select s).Any());
        
        private static readonly Func<SpamDataContext, string, string, bool> SpammerByNameExists
            = CompiledQuery.Compile((SpamDataContext dc, string firstName, string lastName)
                => (from s in dc.SpammerEntities
                    where s.firstName == firstName && s.lastName == lastName
                    select s).Any());

        private static readonly Func<SpamDataContext,  Guid?, string, string, bool> SpammerExists
            = CompiledQuery.Compile((SpamDataContext dc, Guid? userId, string firstName, string lastName)
                => (from s in dc.SpammerEntities
                    where (s.userId == userId) || (s.firstName == firstName && s.lastName == lastName)
                    select s).Any());

        private static readonly Func<SpamDataContext, Guid, DateTime, IQueryable<SpammerReport>> GetSpammerReports
            = CompiledQuery.Compile((SpamDataContext dc, Guid userId, DateTime sinceTime)
                => from r in dc.SpammerReportEntities
                   where r.userId == userId
                   && r.reportedTime > sinceTime
                   orderby r.reportedTime descending
                   select r.Map());

        public SpamRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<string> ISpamRepository.GetSpamText()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSpamText(dc).ToList();
            }
        }

        void ISpamRepository.CreateSpammer(Spammer spammer)
        {
            using (var dc = CreateContext())
            {
                // Check whether the spammer already exists.

                var exists = spammer.UserId != null
                    ? SpammerByUserIdExists(dc, spammer.UserId.Value)
                    : SpammerByNameExists(dc, spammer.FirstName, spammer.LastName);

                if (!exists)
                {
                    dc.SpammerEntities.InsertOnSubmit(spammer.Map());
                    dc.SubmitChanges();
                }
            }
        }

        void ISpamRepository.ReportSpammer(SpammerReport report)
        {
            using (var dc = CreateContext())
            {
                dc.SpammerReportEntities.InsertOnSubmit(report.Map());
                dc.SubmitChanges();
            }
        }

        bool ISpamRepository.IsSpammer(Spammer possibleSpammer)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return SpammerExists(dc, possibleSpammer.UserId, possibleSpammer.FirstName, possibleSpammer.LastName);
            }
        }

        IList<SpammerReport> ISpamRepository.GetSpammerReports(Guid userId, DateTime sinceTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetSpammerReports(dc, userId, sinceTime).ToList();
            }
        }

        private SpamDataContext CreateContext()
        {
            return CreateContext(c => new SpamDataContext(c));
        }
    }
}

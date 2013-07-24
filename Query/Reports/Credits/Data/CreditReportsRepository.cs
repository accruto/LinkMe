using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Credits.Data
{
    public class CreditReportsRepository
        : ReportsRepository<CreditsDataContext>, ICreditReportsRepository
    {
        private static readonly Func<CreditsDataContext, Guid, Guid, DateRange, CreditReport> GetCreditReport
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId, Guid creditId, DateRange dateRange)
                => (from r in dc.GetCreditReport(ownerId, creditId, dateRange.Start.Value, dateRange.End.Value.AddDays(1))
                    select new CreditReport
                    {
                        OpeningCredits = r.opening,
                        CreditsAdded = r.added,
                        CreditsUsed = r.used,
                        CreditsExpired = r.expired,
                        CreditsDeallocated = r.deallocated,
                        ClosingCredits = r.closing
                    }).Single());

        public CreditReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        CreditReport ICreditReportsRepository.GetCreditReport(Guid ownerId, Guid creditId, DateRange dateRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetCreditReport(dc, ownerId, creditId, dateRange);
            }
        }

        protected override CreditsDataContext CreateDataContext(IDbConnection connection)
        {
            return new CreditsDataContext(connection);
        }
    }
}

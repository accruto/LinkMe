using System.Data;
using System.Data.Linq;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Domain.Data;

namespace LinkMe.Query.Reports.Data
{
    public abstract class ReportsRepository<TDataContext>
        : Repository
        where TDataContext : DataContext
    {
        protected ReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        protected TDataContext CreateDataContext(bool readOnly)
        {
            var dc = CreateContext(CreateDataContext);
            return readOnly ? dc.AsReadOnly() : dc;
        }

        protected abstract TDataContext CreateDataContext(IDbConnection connection);
    }
}
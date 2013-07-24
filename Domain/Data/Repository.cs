using System;
using System.Data;
using System.Data.Linq;

namespace LinkMe.Domain.Data
{
    public abstract class Repository
    {
        private readonly IDataContextFactory _dataContextFactory;

        protected Repository(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        protected TDataContext CreateContext<TDataContext>(Func<IDbConnection, TDataContext> createContext)
            where TDataContext : DataContext
        {
            return _dataContextFactory.CreateContext(createContext);
        }
    }
}

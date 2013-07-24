using System;
using System.Data;
using System.Data.Linq;

namespace LinkMe.Domain.Data
{
    public interface IDataContextFactory
    {
        TDataContext CreateContext<TDataContext>(Func<IDbConnection, TDataContext> createContext) where TDataContext : DataContext;
    }
}

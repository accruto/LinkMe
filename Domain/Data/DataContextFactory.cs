using System;
using System.Data;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Data
{
    public class DataContextFactory
        : IDataContextFactory
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private TimeSpan? _commandTimeout;

        public DataContextFactory(IDbConnectionFactory connectionFactory, TimeSpan? commandTimeout)
        {
            _connectionFactory = connectionFactory;
            _commandTimeout = commandTimeout;
        }

        TDataContext IDataContextFactory.CreateContext<TDataContext>(Func<IDbConnection, TDataContext> createContext)
        {
            var dc = createContext(_connectionFactory.CreateConnection());
            if (_commandTimeout != null)
                dc.CommandTimeout = (int)_commandTimeout.Value.TotalSeconds;
            return dc;
        }
    }
}

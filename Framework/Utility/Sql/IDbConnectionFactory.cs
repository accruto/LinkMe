using System.Data;

namespace LinkMe.Framework.Utility.Sql
{
    public interface IDbConnectionFactory
    {
        string ConnectionString { get; }
        IDbConnection CreateConnection();
    }
}

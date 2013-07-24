using System;
using System.Data;
using System.Data.SqlClient;

namespace LinkMe.Framework.Utility.Sql
{
    public class SqlConnectionFactory
        : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            _connectionString = connectionString;
        }

        string IDbConnectionFactory.ConnectionString
        {
            get { return _connectionString; }
        }

        IDbConnection IDbConnectionFactory.CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
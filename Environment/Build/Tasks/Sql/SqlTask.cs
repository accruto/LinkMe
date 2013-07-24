using System.Data;
using System.Data.SqlClient;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public abstract class SqlTask
        : Task
    {
        protected const string MasterDatabase = "master";
        private string _serverName = "(LOCAL)";
        private string _databaseName;

        public string ServerName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }

        [Required]
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        protected static SqlConnection GetSqlConnection(string serverName, string databaseName)
        {
            return new SqlConnection(string.Format("Initial Catalog={0};Data Source={1};Integrated Security=SSPI", databaseName, serverName));
        }
    }
}
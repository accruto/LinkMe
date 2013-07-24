using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public class BackupDatabase
        : SqlTask
    {
        private string _backupFilePath;

        [Required]
        public string BackupFilePath
        {
            get { return _backupFilePath; }
            set { _backupFilePath = value; }
        }

        public override bool Execute()
        {
            Log.LogMessage("Backing up database '{0}' on server '{1}' to file '{2}'.", DatabaseName, ServerName, BackupFilePath);

            using (var connection = GetSqlConnection(ServerName, MasterDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("BACKUP DATABASE {0} TO DISK = '{1}' WITH INIT", DatabaseName, BackupFilePath);
                    command.ExecuteNonQuery();
                    Log.LogMessage("Database backup complete.");
                }
            }

            return true;
        }
    }
}
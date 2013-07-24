using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public class RestoreDatabase
        : SqlTask
    {
        private string _backupFilePath;
        private string _sqlDataDirectory;

        [Required]
        public string BackupFilePath
        {
            get { return _backupFilePath; }
            set { _backupFilePath = value; }
        }

        [Required]
        public string SqlDataDirectory
        {
            get { return _sqlDataDirectory; }
            set { _sqlDataDirectory = value; }
        }

        public override bool Execute()
        {
            if (!File.Exists(BackupFilePath))
            {
                Log.LogError("The backup file to restore, '{0}', does not exist.", BackupFilePath);
                return false;
            }

            Log.LogMessage("Restoring backup file '{0}' to database '{1}' on server '{2}' (data directory = '{3}').",
                           BackupFilePath, DatabaseName, ServerName, SqlDataDirectory);

            if (!Directory.Exists(SqlDataDirectory))
            {
                Directory.CreateDirectory(SqlDataDirectory);
                Log.LogMessage("Created directory '{0}' for SQL data files.", SqlDataDirectory);
            }

            using (IDbConnection connection = GetSqlConnection(ServerName, MasterDatabase))
            {
                connection.Open();

                string primaryFile;
                IDictionary<string, string> fileMappings = GetDbFileMappings(connection, out primaryFile);

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("RESTORE DATABASE {0} FROM DISK = '{1}' WITH REPLACE",
                                                        DatabaseName, BackupFilePath);

                    foreach (KeyValuePair<string, string> kvp in fileMappings)
                    {
                        command.CommandText += string.Format(", MOVE '{0}' TO '{1}'", kvp.Key, kvp.Value);
                    }

                    command.ExecuteNonQuery();
                    Log.LogMessage("Database restore complete.");
                }

                WorkAroundStupidSql2005RestoreBug(connection, primaryFile);
            }

            return true;
        }

        private IDictionary<string, string> GetDbFileMappings(IDbConnection connection, out string primaryFile)
        {
            primaryFile = null;

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format("RESTORE FILELISTONLY FROM DISK = '{0}'", BackupFilePath);

                using (IDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, string> mappings = new Dictionary<string, string>();

                    int dataCount = 0;
                    int logCount = 0;

                    while (reader.Read())
                    {
                        string logicalName = reader.GetString(0);
                        string type = reader.GetString(2);
                        string physicalName;

                        switch (type)
                        {
                            case "D": // Data
                                physicalName = GetPhysicalName("_Data", ".mdf", ref dataCount);

                                if (primaryFile == null)
                                {
                                    primaryFile = physicalName;
                                }
                                break;

                            case "L": // Log
                                physicalName = GetPhysicalName("_Log", ".ldf", ref logCount);
                                break;

                            case "F": // Full-text catalog
                                physicalName = DatabaseName + "_" + logicalName;
                                break;

                            default:
                                throw new ApplicationException("Unexpected type of database file: " + type);
                        }

                        mappings.Add(logicalName, Path.Combine(SqlDataDirectory, physicalName));
                    }

                    return mappings;
                }
            }
        }

        private string GetPhysicalName(string suffix, string extension, ref int count)
        {
            string physicalName = DatabaseName + suffix;
            if (count++ > 0)
            {
                physicalName += count.ToString();
            }
            return physicalName + extension;
        }

        private void WorkAroundStupidSql2005RestoreBug(IDbConnection connection, string primaryFile)
        {
            // Workaround for the FT catalog restore bug described in http://support.microsoft.com/kb/910067

            if (string.IsNullOrEmpty(primaryFile))
                throw new ArgumentException("The primary database file is required.", "primaryFile");

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format("EXEC sp_detach_db '{0}';\r\n"
                                                    + "EXEC sp_attach_db '{0}', '{1}'", DatabaseName, Path.Combine(SqlDataDirectory, primaryFile));
                command.ExecuteNonQuery();
                Log.LogMessage("Database detached and reattached.");
            }
        }
    }
}
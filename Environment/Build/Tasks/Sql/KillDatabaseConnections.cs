namespace LinkMe.Environment.Build.Tasks.Sql
{
    public class KillDatabaseConnections
        : SqlTask
    {
        private const string KillScriptFormat = @"
            DECLARE @spidstr varchar(8000)
            SET @spidstr = ''

            SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; '
            FROM master..sysprocesses WHERE dbid=db_id('{0}')

            IF LEN(@spidstr) > 0 
            BEGIN
	            EXEC(@spidstr)
            END";

        public override bool Execute()
        {
            Log.LogMessage("Killing all connections to database '{0}' on server '{1}'.", DatabaseName, ServerName);

            using (var connection = GetSqlConnection(ServerName, MasterDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(KillScriptFormat, DatabaseName);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
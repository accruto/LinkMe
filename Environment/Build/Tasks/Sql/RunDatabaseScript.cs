using System;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public enum RunScriptResult
    {
        Succeeded,
        Failed,
        Skipped
    }

    [Flags]
    public enum RunScriptOptions
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Attempt to run all statements in the script even if errors occur. The default is to stop on
        /// the first error.
        /// </summary>
        ContinueOnError = 1,
        /// <summary>
        /// Use the DatabaseScript table (if it exists) to determine whether the script needs to be run
        /// and add it to the table. The default is to always run the script and not add it to the table.
        /// </summary>
        UseDatabaseScriptRecords = 2
    }

    public class RunDatabaseScript
        : RunScriptTask
    {
        private string _scriptFile;

        [Required]
        public string ScriptFile
        {
            get { return _scriptFile; }
            set { _scriptFile = value; }
        }

        public override bool Execute()
        {
            using (var connection = GetSqlConnection(ServerName, DatabaseName))
            {
                connection.Open();

                int rowsAffected;
                RunScriptResult result = RunScript(new BuildLogTextWriter(Log, false),
                                                   new BuildLogTextWriter(Log, true), connection, ScriptFile, RunScriptOptions.None,
                                                   out rowsAffected);

                if (result == RunScriptResult.Succeeded)
                    return true;

                Log.LogError("Database script '" + ScriptFile + "' failed.");
                return false;
            }
        }
    }
}
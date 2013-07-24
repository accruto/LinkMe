using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
    /// <summary>
    /// This class (the source file) is shared with the BuildUtils assembly, so it should have
    /// minimal dependencies on other assemblies or classes.
    /// </summary>
    public static class ScriptHelper
    {
        #region Nested types

        private class SqlMessageListener
        {
            private readonly List<string> _messages = new List<string>();
            private readonly TextWriter _output;

            internal SqlMessageListener(TextWriter output)
            {
                _output = output;
            }

            internal IList<string> Messages
            {
                get { return _messages; }
            }

            internal void ConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
            {
                _messages.Add(e.Message);

                _output.WriteLine();
                _output.WriteLine(e.Message);
                _output.WriteLine();
            }
        }

        #endregion

        public const string DefaultScriptListServer = "(LOCAL)";
        public const string DefaultScriptListDatabase = "LinkMe";

        // The maximum number of stored procedure parameters for SQL 2000, as emailed by MS in response to
        // support incident SRS061006600251.
        private const int MaxParametersPerCommand = 2100;

        public static string GetSqlConnectionString(string serverName, string databaseName)
        {
            return string.Format("Initial Catalog={0};Data Source={1};Integrated Security=SSPI",
                databaseName, serverName);
        }

        public static string GetSqlConnectionString(string serverName, string databaseName, string login,
            string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                return GetSqlConnectionString(serverName, databaseName);
            }
            
            return string.Format("Initial Catalog={0};Data Source={1};User ID={2};Password={3};"
                + "Persist Security Info=True;", databaseName, serverName, login, password);
        }

        /// <summary>
        /// Runs the scripts listed in install.db.txt in the specified directory. If successfull returns null, otherwise
        /// returns the path of the script file that failed.
        /// </summary>
        public static string RunScriptList(TextWriter output, TextWriter error, string listFilePath,
            string serverName, string databaseName, string login, string password)
        {
            if (string.IsNullOrEmpty(listFilePath))
                throw new ArgumentException("The script list file path must be specified.", "listFilePath");
            if (string.IsNullOrEmpty(login) ^ string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Either both the login and the password must be specified or"
                    + " neither must be specified to use integrated security.");
            }

            if (!File.Exists(listFilePath))
            {
                throw new FileNotFoundException("The script list file, '" + listFilePath + "', does not exist.",
                    listFilePath);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            output.WriteLine("Processing script list file '" + listFilePath + "'...");

            if (string.IsNullOrEmpty(serverName))
            {
                serverName = DefaultScriptListServer;
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = DefaultScriptListDatabase;
            }

            string connString = GetSqlConnectionString(serverName, databaseName, login, password);

            IList<string> scriptList = FileUtils.ReadFileList(listFilePath);
            output.WriteLine("Connecting to database '{0}' on server '{1}'...", databaseName, serverName);

            var listener = new SqlMessageListener(output);
            int totalRowsAffected = 0, succeeded = 0, skipped = 0;

            if (scriptList.Count != 0)
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.InfoMessage += listener.ConnectionInfoMessage;
                    connection.Open();

                    foreach (string filePath in scriptList)
                    {
                        int scriptFileRowsAffected;
                        RunScriptResult scriptFileResult = RunScript(output, error, connection, filePath,
                            RunScriptOptions.UseDatabaseScriptRecords, out scriptFileRowsAffected);
                        totalRowsAffected += scriptFileRowsAffected;

                        switch (scriptFileResult)
                        {
                            case RunScriptResult.Failed:
                                return filePath;

                            case RunScriptResult.Succeeded:
                                succeeded++;
                                break;

                            case RunScriptResult.Skipped:
                                skipped++;
                                break;

                            default:
                                throw new ApplicationException("Unexpected value of RunScriptResult: "
                                    + scriptFileResult);
                        }
                    }
                }
            }

            stopwatch.Stop();
            output.WriteLine("Finished processing script list file '{0}' in {1}.{2}{3} scripts succeeded,"
                + " {4} skipped. {5} rows affected.",
                listFilePath, stopwatch.Elapsed, System.Environment.NewLine, succeeded, skipped, totalRowsAffected);

            if (listener.Messages.Count > 0)
            {
                output.WriteLine();
                output.WriteLine("{0} info/warning message{1} generated:", listener.Messages.Count,
                    (listener.Messages.Count == 1 ? " was" : "s were"));
                output.WriteLine();

                foreach (string message in listener.Messages)
                {
                    output.WriteLine(message);
                }
            }

            return null;
        }

        public static RunScriptResult RunScript(TextWriter output, TextWriter error, SqlConnection connection,
            string scriptFilePath, RunScriptOptions options, out int scriptFileRowsAffected)
        {
            if (string.IsNullOrEmpty(scriptFilePath))
                throw new ArgumentException("The script file path must be specified.", "scriptFilePath");
            if (!File.Exists(scriptFilePath))
                throw new FileNotFoundException("SQL script file '" + scriptFilePath + "' does not exist.", scriptFilePath);

            using (var reader = new StreamReader(scriptFilePath))
            {
                return RunScript(output, error, connection, reader, scriptFilePath, options,
                    out scriptFileRowsAffected);
            }
        }

        /// <summary>
        /// Run the commands in an SQL script file.
        /// </summary>
        /// <param name="output">TextWriter for informational output.</param>
        /// <param name="error">TextWriter for error output.</param>
        /// <param name="connection">An open connection to run the scripts against.</param>
        /// <param name="script">The script to run.</param>
        /// <param name="scriptFilePath">The name and path of the script, used for error reporting purposes and script path substitution.</param>
        /// <param name="options">Script execution options.</param>
        /// <param name="scriptFileRowsAffected">The total number of rows affected by all commands in the script.</param>
        /// <returns>True if execution completed successfully, false if errors occurred.</returns>
        public static RunScriptResult RunScript(TextWriter output, TextWriter error, SqlConnection connection,
            TextReader script, string scriptFilePath, RunScriptOptions options, out int scriptFileRowsAffected)
        {
            scriptFileRowsAffected = 0;

            string[] commands = ReadDatabaseScript(script);
            if (commands.IsNullOrEmpty())
            {
                output.WriteLine("\tExecuting '" + scriptFilePath + "' - it contains no commands.");
                return RunScriptResult.Skipped;
            }

            int? scriptRecordId = null;
            if (options.IsFlagSet(RunScriptOptions.UseDatabaseScriptRecords))
            {
                string userMessage;
                bool shouldRun = DbScriptRegister.CheckAndAddScriptRecord(connection, scriptFilePath, commands,
                    out scriptRecordId, out userMessage);

                if (userMessage != null)
                {
                    output.WriteLine("\t" + userMessage);
                }

                if (!shouldRun)
                    return RunScriptResult.Skipped;
            }

            // Relpace $(LINKME_SCRIPT_PATH) variable with the actual script path.
            // This syntax is used for compatibility with SqlCmd utility.

            if (!string.IsNullOrEmpty(scriptFilePath))
            {
                output.WriteLine("\tExecuting '" + scriptFilePath + "'...");

                string scriptFolder = Path.GetDirectoryName(Path.GetFullPath(scriptFilePath));
                for (int i = 0; i < commands.Length; i++)
                {
                    commands[i] = commands[i].Replace("$(LINKME_SCRIPT_PATH)", scriptFolder);
                }
            }
            else
            {
                for (int i = 0; i < commands.Length; i++)
                {
                    if (commands[i].IndexOf("$(LINKME_SCRIPT_PATH)") != -1)
                    {
                        throw new ArgumentException("scriptFileName must be specified when the script"
                            + " contains $(LINKME_SCRIPT_PATH) placeholders.", "scriptFilePath");
                    }
                }
            }

            bool success = true;

            for (int i = 0; i < commands.Length; i++)
            {
                using (var command = new SqlCommand(commands[i], connection))
                {
                    command.CommandTimeout = 0; // No timeout. Some of our scripts run for a very long time!

                    int rowsAffected = 0;
                    try
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (scriptFilePath == null)
                        {
                            error.WriteLine("{0}SQL command {1} failed. Command text:{0}{2}{0}{0}SQL error message:{0}{3}",
                                System.Environment.NewLine, i + 1, commands[i], ex.Message);
                        }
                        else
                        {
                            error.WriteLine("{0}SQL command {1} in file '{4}' failed. Command text:{0}{2}"
                                + "{0}{0}SQL error message:{0}{3}",
                                System.Environment.NewLine, i + 1, commands[i], ex.Message, scriptFilePath);
                        }

                        success = false;
                        if (!options.IsFlagSet(RunScriptOptions.ContinueOnError))
                            return RunScriptResult.Failed;
                    }

                    if (rowsAffected > 0)
                    {
                        scriptFileRowsAffected += rowsAffected;
                    }
                }
            }

            output.Write("\tDone.");
            if (scriptFileRowsAffected > 0)
            {
                output.Write(" {0} row{1} affected.", scriptFileRowsAffected,
                    (scriptFileRowsAffected == 1 ? "" : "s"));
            }
            output.WriteLine();

            if (success && scriptRecordId.HasValue)
            {
                DbScriptRegister.MarkScriptSuccessful(connection, scriptRecordId.Value);
            }

            return (success ? RunScriptResult.Succeeded : RunScriptResult.Failed);
        }

        /// <summary>
        /// Reads a database script and returns an array of SQL statements. Statements must be
        /// separated by the word "GO" on a line by itself.
        /// </summary>
        public static string[] ReadDatabaseScript(TextReader reader)
        {
            var commands = new List<string>();
            var sb = new StringBuilder();

            string line = reader.ReadLine();
            while (line != null)
            {
                if (string.Compare(line.Trim(), "GO", true) == 0)
                {
                    // End of a command, add it.

                    AddCommand(commands, sb);
                }
                else
                {
                    sb.Append(line);
                    sb.Append(System.Environment.NewLine);
                }

                line = reader.ReadLine();
            }

            // Add the last command.

            AddCommand(commands, sb);

            return commands.ToArray();
        }

        internal static IDbDataParameter AddParameter(IDbCommand command, string name, DbType type,
            object value)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("The parameter name must be specified.", "name");

            Debug.Assert(name[0] == '@', "Parameter name '" + name + "' does not start with '@'.");

            IDbDataParameter parameter = command.CreateParameter();

            parameter.DbType = type;
            parameter.ParameterName = name;
            parameter.Value = (value ?? DBNull.Value);

            command.Parameters.Add(parameter);

            if (command.Parameters.Count > MaxParametersPerCommand)
            {
                throw new ApplicationException(string.Format("Too many parameters have been added to command"
                    + " '{0}. Commands with over {1} parameters may fail with an SQL \"invalid buffer\" error.",
                    command.CommandText, MaxParametersPerCommand));
            }

            return parameter;
        }

        private static void AddCommand(ICollection<string> commands, StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                string command = sb.ToString().Trim();
                if (command != "")
                {
                    commands.Add(command);
                }
                sb.Remove(0, sb.Length);
            }
        }
    }
}

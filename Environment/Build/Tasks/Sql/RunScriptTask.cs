using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public abstract class RunScriptTask
        : SqlTask
    {
        protected static RunScriptResult RunScript(TextWriter output, TextWriter error, SqlConnection connection,
                                                   string scriptFilePath, RunScriptOptions options, out int scriptFileRowsAffected)
        {
            if (string.IsNullOrEmpty(scriptFilePath))
                throw new ArgumentException("The script file path must be specified.", "scriptFilePath");
            if (!File.Exists(scriptFilePath))
                throw new FileNotFoundException("SQL script file '" + scriptFilePath + "' does not exist.", scriptFilePath);

            using (StreamReader reader = new StreamReader(scriptFilePath))
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
        private static RunScriptResult RunScript(TextWriter output, TextWriter error, SqlConnection connection,
                                                 TextReader script, string scriptFilePath, RunScriptOptions options, out int scriptFileRowsAffected)
        {
            scriptFileRowsAffected = 0;

            string[] commands = ReadDatabaseScript(script);
            if (commands == null || commands.Length == 0)
            {
                output.WriteLine("\tExecuting '" + scriptFilePath + "' - it contains no commands.");
                return RunScriptResult.Skipped;
            }

            int? scriptRecordId = null;
            if ((options & RunScriptOptions.UseDatabaseScriptRecords) == RunScriptOptions.UseDatabaseScriptRecords)
            {
                //string userMessage;
                bool shouldRun = true; // DbScriptRegister.CheckAndAddScriptRecord(connection, scriptFilePath, commands, out scriptRecordId, out userMessage);

                //                if (userMessage != null)
                //              {
                //                output.WriteLine("\t" + userMessage);
                //          }

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
                using (SqlCommand command = new SqlCommand(commands[i], connection))
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
                        if ((options & RunScriptOptions.ContinueOnError) == 0)
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
                //DbScriptRegister.MarkScriptSuccessful(connection, scriptRecordId.Value);
            }

            return (success ? RunScriptResult.Succeeded : RunScriptResult.Failed);
        }

        /// <summary>
        /// Reads a database script and returns an array of SQL statements. Statements must be
        /// separated by the word "GO" on a line by itself.
        /// </summary>
        protected static string[] ReadDatabaseScript(TextReader reader)
        {
            List<string> commands = new List<string>();
            StringBuilder sb = new StringBuilder();

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
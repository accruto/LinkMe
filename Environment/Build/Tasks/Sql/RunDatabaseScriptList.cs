using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    public class RunDatabaseScriptList
        : RunScriptTask
    {
        private const char _listFileLineCommentChar = ';';
        // Place this value in the script list (on a line by itself) to start processing from the next
        // line, rather than the start of the file (ie. ignore everything before this line).
        // Useful for restarting a long script from the point of failure while testing.
        private const string _listFileStartSentinel = "*START*";
        // Place this value in the script list (on a line by itself) to stop processing at this point.
        // Useful for testing.
        private const string _listFileEndSentinel = "*END*";
        private ITaskItem[] _scripts;

        [Required]
        public ITaskItem[] ScriptListFiles
        {
            get { return _scripts; }
            set { _scripts = value; }
        }

        public override bool Execute()
        {
            foreach (var script in _scripts)
            {
                // Check for the most common problem - missing install.db.txt

                if (!File.Exists(script.ItemSpec))
                {
                    Log.LogError("Script list file '{0}' does not exist.", script.ItemSpec);
                    return false;
                }

                var failedScript = RunScriptList(new BuildLogTextWriter(Log, false), new BuildLogTextWriter(Log, true), script.ItemSpec);
                if (failedScript != null)
                {
                    Log.LogError("Database script '" + failedScript + "' failed.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Runs the scripts listed in install.db.txt in the specified directory. If successfull returns null, otherwise
        /// returns the path of the script file that failed.
        /// </summary>
        public string RunScriptList(TextWriter output, TextWriter error, string listFilePath)
        {
            if (string.IsNullOrEmpty(listFilePath))
                throw new ArgumentException("The script list file path must be specified.", "listFilePath");
            if (!File.Exists(listFilePath))
                throw new FileNotFoundException("The script list file, '" + listFilePath + "', does not exist.", listFilePath);

            var stopwatch = Stopwatch.StartNew();
            output.WriteLine("Processing script list file '" + listFilePath + "'...");

            var scriptList = ReadFileList(listFilePath);
            output.WriteLine("Connecting to database '{0}' on server '{1}'...", DatabaseName, ServerName);

            var listener = new SqlMessageListener(output);

            int totalRowsAffected = 0, succeeded = 0, skipped = 0;
            if (scriptList.Count != 0)
            {
                using (var connection = GetSqlConnection(ServerName, DatabaseName))
                {
                    connection.InfoMessage += listener.connection_InfoMessage;
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

        private static IList<string> ReadFileList(string listFilePath)
        {
            string scriptDir = Path.GetDirectoryName(listFilePath);

            var scriptList = new List<string>();

            using (var reader = new StreamReader(listFilePath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();

                    if (string.Equals(line, _listFileStartSentinel, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Found a start sentinel - ignore everything before it.
                        scriptList.Clear();
                    }
                    else if (string.Equals(line, _listFileEndSentinel, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Found a end sentinel - ignore everything after it.
                        break;
                    }
                    else if (line.Length > 0 && line[0] != _listFileLineCommentChar)
                    {
                        string filePath = Path.Combine(scriptDir, line);

                        // Check that every file exists now, before we run any of them.

                        if (!File.Exists(filePath))
                        {
                            throw new FileNotFoundException(string.Format("File '{0}', listed in '{1}', does not exist.",
                                                                          filePath, listFilePath), filePath);
                        }

                        scriptList.Add(filePath);
                    }

                    line = reader.ReadLine();
                }
            }

            return scriptList;
        }
    }
}
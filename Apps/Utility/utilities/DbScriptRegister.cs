using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
    public static class DbScriptRegister
    {
        private const string _commandSeparator = "\r\nGO\r\n";
        private const string _tableName = "dbo.DatabaseScript";
        private const string _tableExistsStatement = "EXISTS (SELECT * FROM sys.objects WHERE"
            + " object_id = OBJECT_ID(N'" + _tableName + "') AND type in (N'U'))";

        private static readonly string _pathSeparatorRegex = "\\" + Path.DirectorySeparatorChar
            + "|\\" + Path.AltDirectorySeparatorChar;
        private static readonly Regex _releaseDirRegex = new Regex(
            "(" + _pathSeparatorRegex + @")(?<Release>\d{1,2}\.\d{1,2}(\.\d{1,2}){0,2})("
            + _pathSeparatorRegex + "|$)", RegexOptions.ExplicitCapture);

        public static bool CheckAndAddScriptRecord(IDbConnection connection, string originalPath,
            string[] commands, out int? scriptRecordId, out string userMessage)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (commands.IsNullOrEmpty())
                throw new ArgumentException("The commands must be specified.", "commands");

            string fullPath = (string.IsNullOrEmpty(originalPath) ? null : Path.GetFullPath(originalPath));
            string fileName = Path.GetFileName(fullPath);
            string filePath = Path.GetDirectoryName(fullPath);
            string scriptText = GetScriptText(commands);
            byte[] checksum = GetChecksum(scriptText);

            if (!ShouldRunScript(connection, originalPath, fileName, filePath, checksum, out userMessage))
            {
                scriptRecordId = null;
                return false;
            }

            scriptRecordId = AddScriptRecord(connection, DateTime.Now, false, fileName, filePath,
                checksum, scriptText);
            return true;
        }

        public static void MarkScriptSuccessful(IDbConnection connection, int scriptRecordId)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE " + _tableName + @"
                    SET succeeded = 1
                    WHERE [id] = @id";

                ScriptHelper.AddParameter(command, "@id", DbType.Int32, scriptRecordId);

                int affected = command.ExecuteNonQuery();
                if (affected != 1)
                {
                    throw new ApplicationException(string.Format("Failed to update the {0} table for"
                        + " script record {1} - {2} rows affected.", _tableName, scriptRecordId, affected));
                }
            }
        }

        private static bool ShouldRunScript(IDbConnection connection, string originalPath,
            string thisFileName, string thisFilePath, byte[] checksum, out string userMessage)
        {
            const string commandText = @"
                IF " + _tableExistsStatement + @"
                    SELECT TOP 1 [time], succeeded, [fileName], [filePath]
                    FROM dbo.DatabaseScript
                    WHERE [checksum] = @checksum
                    ORDER BY succeeded DESC, [time] DESC";

            userMessage = null;

            // See if there's a record of this script being run before.

            DateTime time;
            bool succeeded;
            string lastFileName, lastFilePath;

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;

                ScriptHelper.AddParameter(command, "@checksum", DbType.Binary, checksum);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return true; // No script with this checksum has not been run before.

                    time = reader.GetDateTime(0);
                    succeeded = reader.GetBoolean(1);
                    lastFileName = (reader.IsDBNull(2) ? null : reader.GetString(2));
                    lastFilePath = (reader.IsDBNull(3) ? null : reader.GetString(3));
                }
            }

            string fileNameChangedMessage = "";
            if (!string.Equals(thisFileName, lastFileName, StringComparison.InvariantCultureIgnoreCase))
            {
                fileNameChangedMessage = " (as '" + lastFileName + "')";
            }

            if (!succeeded)
            {
                userMessage = string.Format("'{0}' has been run before on {1}{2}"
                    + ", but did not succeed - running again...", originalPath, time, fileNameChangedMessage);
                return true;
            }

            // It's possible that the same script has been run before for a previous release, but we
            // want to run it again for this release. Try to handle this edge case here by looking
            // at the last directory in the script file path.

            string thisRelease = GetReleaseNumber(thisFilePath);
            string lastRelease = GetReleaseNumber(lastFilePath);

            if (lastRelease != thisRelease)
            {
                userMessage = string.Format("'{0}' has been run before on {1}{2}"
                    + ", but it was for release {3} - running again for release {4}...",
                    originalPath, time, fileNameChangedMessage, lastRelease, thisRelease);
                return true;
            }

            // OK, looks like we really don't need to run this script again.

            userMessage = string.Format("Skipping '{0}' - already run on {1}{2}.",
                originalPath, time, fileNameChangedMessage);
            return false;
        }

        private static string GetReleaseNumber(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            Match match = _releaseDirRegex.Match(path);
            if (!match.Success)
                return null;

            return match.Groups[1].Captures[0].Value;
        }

        private static int? AddScriptRecord(IDbConnection connection, DateTime time, bool succeeded,
            string fileName, string filePath, byte[] checksum, string scriptText)
        {
            // Only try to insert if the table exists.
            // For some strange reason @@IDENTITY return decimal by default, so cast to INT.

            const string commandText = @"
                IF " + _tableExistsStatement + @"
                BEGIN
                    INSERT INTO " + _tableName + @"([time], succeeded, [fileName], filePath, [checksum], [text])
                    VALUES (@time, @succeeded, @fileName, @filePath, @checksum, @text)
                    SELECT CAST(@@IDENTITY AS INT)
                END
                ELSE
                    SELECT NULL";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;

                ScriptHelper.AddParameter(command, "@time", DbType.DateTime, time);
                ScriptHelper.AddParameter(command, "@succeeded", DbType.Boolean, succeeded);
                ScriptHelper.AddParameter(command, "@fileName", DbType.String, fileName);
                ScriptHelper.AddParameter(command, "@filePath", DbType.String, filePath);
                ScriptHelper.AddParameter(command, "@checksum", DbType.Binary, checksum);
                ScriptHelper.AddParameter(command, "@text", DbType.String, scriptText);

                object id = command.ExecuteScalar();
                return (id is DBNull ? (int?)null : (int)id);
            }
        }

        private static string GetScriptText(string[] commands)
        {
            return string.Join(_commandSeparator, commands);
        }

        private static byte[] GetChecksum(string text)
        {
            SHA1 hash = SHA1.Create();
            byte[] data = Encoding.UTF8.GetBytes(text);
            return hash.ComputeHash(data);
        }
    }
}

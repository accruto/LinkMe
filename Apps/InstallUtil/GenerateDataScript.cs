using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Utilities;

namespace LinkMe.InstallUtil
{
    public static class GenerateDataScript
    {
        private static readonly Regex selectRegex = new Regex(@"^\s*SELECT\s+\*\s+FROM\s+(?<table>(\w+\.)?\w+)\s*",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// Prints to the standard output a list of INSERT statements to insert data for the result sets
        /// of one or more queries. The queries are read from standard input (separated by "GO" on a line
        /// by itself).
        /// </summary>
        public static void PrintInsertStatements(string[] args)
        {
            if (args.Length > 5)
            {
                Program.Usage();
                return;
            }

            IList<string> list = GetInsertStatements((args.Length > 1 ? args[1] : null), (args.Length > 2 ? args[2] : null),
                (args.Length > 3 ? args[3] : null), (args.Length > 4 ? args[4] : null), Console.In);

            foreach (string item in list)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Returns a list of INSERT statements to insert data for the result sets of one or more queries.
        /// </summary>
        public static IList<string> GetInsertStatements(string serverName, string databaseName, string loginId, string password,
            TextReader queryReader)
        {
            string[] queries = ScriptHelper.ReadDatabaseScript(queryReader);
            if (queries.Length == 0)
                return new string[0];

            DbTaskHelper.DefaultServerAndDbNames(ref serverName, ref databaseName);

            IList<string> statements = new List<string>();

            using (SqlConnection connection = new SqlConnection(ScriptHelper.GetSqlConnectionString(
                serverName, databaseName, loginId, password)))
            {
                connection.Open();

                foreach (string query in queries)
                {
                    statements.Add(GetInsertStatementsForQuery(connection, query));
                }
            }

            return statements;
        }

        private static string GetInsertStatementsForQuery(SqlConnection connection, string query)
        {
            Match match = selectRegex.Match(query);
            if (!match.Success)
                throw new UserException("The query must be of the form 'SELECT * FROM <table> ...'");

            string tableName = match.Groups["table"].Value;

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return "-- No rows for table '" + tableName + "'.";

                    StringBuilder sb = new StringBuilder("INSERT INTO " + tableName + "([");

                    // Get the INSERT statement with all column names.

                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        sb.Append(reader.GetName(i));
                        sb.Append("], [");
                    }

                    sb.Append(reader.GetName(reader.FieldCount - 1));
                    sb.AppendLine("])");

                    string insert = sb.ToString();
                    object[] values = new object[reader.FieldCount];
                    AppendValues(sb, reader, values);

                    int rowCount = 1;

                    while (reader.Read())
                    {
                        sb.Append(insert);
                        AppendValues(sb, reader, values);
                        rowCount++;
                    }

                    return "-- " + rowCount + " rows for table '" + tableName + "'..."
                        + System.Environment.NewLine + sb;
                }
            }
        }

        private static void AppendValues(StringBuilder sb, SqlDataReader reader, object[] values)
        {
            int valueCount = reader.GetValues(values);
            Debug.Assert(valueCount == values.Length, "valueCount == values.Length");

            sb.Append("VALUES (");

            for (int i = 0; i < values.Length - 1; i++)
            {
                sb.Append(DatabaseHelper.GetSqlScriptValue(values[i]));
                sb.Append(", ");
            }

            sb.Append(DatabaseHelper.GetSqlScriptValue(values[values.Length - 1]));
            sb.AppendLine(")");
        }
    }
}

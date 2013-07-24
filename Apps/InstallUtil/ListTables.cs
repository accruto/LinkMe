using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LinkMe.Utility.Utilities;

namespace LinkMe.InstallUtil
{
    public static class ListTables
    {
        /// <summary>
        /// Prints to the standard output a list of tables in an SQL database in the order in which they
        /// can be deleted (or data in them can be deleted), taking into account foreign keys.
        /// </summary>
        public static void PrintListInDeleteOrder(string[] args)
        {
            if (args.Length > 4)
            {
                Program.Usage();
                return;
            }

            IList<string> list = GetListInDeleteOrder((args.Length > 1 ? args[1] : null),
                (args.Length > 2 ? args[2] : null));
            string formatString = (args.Length > 3 ? args[3] : "{0}");

            foreach (string table in list)
            {
                Console.WriteLine(formatString, table);
            }
        }

        /// <summary>
        /// Returns a list of tables in an SQL database in the order in which they can be
        /// deleted (or data in them can be deleted), taking into account foreign keys.
        /// </summary>
        public static IList<string> GetListInDeleteOrder(string serverName, string databaseName)
        {
            DbTaskHelper.DefaultServerAndDbNames(ref serverName, ref databaseName);

            using (SqlConnection connection = new SqlConnection(ScriptHelper.GetSqlConnectionString(
                serverName, databaseName)))
            {
                connection.Open();

                // Get a list of all user tables.

                IList<string> tables = GetAllTables(connection);

                // For each table get the tables that reference it and move it below all those table.

                List<string> ordered = new List<string>(tables);
                Dictionary<string, IList<string>> dependenceCache = new Dictionary<string, IList<string>>();

                int i = 0;
                while (i < ordered.Count)
                {
                    IList<string> dependentTables = GetDependentTables(connection, ordered[i], dependenceCache);
                    if (!MoveBelowAll(ordered, i, dependentTables))
                    {
                        i++;
                    }
                }

                return ordered;
            }
        }

        private static bool MoveBelowAll<T>(List<T> ordered, int indexToMove, IList<T> moveBelowAll)
        {
            if (moveBelowAll.Count == 0)
                return false;

            for (int i = ordered.Count - 1; i > indexToMove; i--)
            {
                if (moveBelowAll.Contains(ordered[i]))
                {
                    ordered.Insert(i + 1, ordered[indexToMove]);
                    ordered.RemoveAt(indexToMove);
                    return true;
                }
            }

            return false;
        }

        private static IList<string> GetAllTables(SqlConnection connection)
        {
            const string commandText = @"SELECT su.[name], so.[name]
                FROM dbo.sysobjects so
                INNER JOIN dbo.sysusers su
                ON so.uid = su.uid
                WHERE so.xtype = 'U' AND so.[name] <> 'dtproperties'
                ORDER BY su.[name], so.[name]";

            return GetTableList(connection, commandText);
        }

        private static IList<string> GetDependentTables(SqlConnection connection, string dependentOn,
            Dictionary<string, IList<string>> dependenceCache)
        {
            const string commandTextFormat = @"SELECT DISTINCT su.[name], so.[name]
                FROM dbo.sysforeignkeys fk
                INNER JOIN dbo.sysobjects so
                ON fk.fkeyid = so.[id]
                INNER JOIN dbo.sysusers su
                ON so.uid = su.uid
                WHERE rkeyid = OBJECT_ID('{0}')";

            IList<string> result;
            if (dependenceCache.TryGetValue(dependentOn, out result))
                return result;

            result = GetTableList(connection, string.Format(commandTextFormat, dependentOn));
            dependenceCache.Add(dependentOn, result);

            return result;
        }

        private static IList<string> GetTableList(SqlConnection connection, string commandText)
        {
            List<string> tables = new List<string>();

            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(GetQualifiedName(reader.GetString(0), reader.GetString(1)));
                    }
                }
            }

            return tables;
        }

        private static string GetQualifiedName(string ownerName, string objectName)
        {
            return "[" + ownerName + "].[" + objectName + "]";
        }
    }
}

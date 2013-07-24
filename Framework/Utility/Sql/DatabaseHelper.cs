using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility.Sql
{
    public static class DatabaseHelper
    {
        private delegate T ExecuteCommand<T>(IDbCommand command);

        public const string TABLE_OWNER_NAME = "dbo";
        public const int MAX_BATCH_SIZE = 50;
        public const int DefaultDbTimeoutSeconds = 30;
        public const string RandomIntSql = "ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT))";

        private const string dateFormat = "yyyy-MM-dd";
        private const string timeFormat = "HH\\:mm\\:ss.fff";
        private const string dateTimeFormat = dateFormat + " " + timeFormat;
        private const int logSlowCommandThresholdMs = 10000;
        private const int maxParametersPerCommand = 2100;

        private static readonly Regex _connectionStringPasswordRegex = new Regex(@"(?<=password\s*=\s*)([^;]+)(?=;|$)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Puts the declaration and assignment of parameter values into the text of the command. This avoids the use of
        /// sp_executesql by the SqlCommand class. In SQL 2005 sp_executesql runs much slower than the equivalent "raw"
        /// query for some reason.
        /// </summary>
        public static void InlineParameters(IDbCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (command.CommandType != CommandType.Text)
                return; // Stored procedure commands could be inlined, too, but there's probably no point.

            string paramStatements = "";

            foreach (IDbDataParameter parameter in command.Parameters)
            {
                if (parameter.Value == null)
                {
                    throw new ApplicationException("Don't know what to do with null parameter '" + parameter.ParameterName
                        + "' - did you mean to make it DBNull?");
                }

                paramStatements += string.Format("DECLARE {0} {1}\r\nSET {0} = {2}\r\n\r\n",
                    parameter.ParameterName, GetParamDeclareType(parameter), GetParamDeclareValue(parameter));
            }

            command.CommandText = paramStatements + command.CommandText;
            command.Parameters.Clear();
        }

        public static bool IsFullTextSearchError(DbException exception)
        {
            return (exception != null && exception.ErrorCode == -2146232060
                && exception.Message.IndexOf("A clause of the query contained only ignored words") != -1);
        }

        public static string BlankOutConnectionStringPassword(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return connectionString;

            return _connectionStringPasswordRegex.Replace(connectionString, "*****");
        }

        public static object DbNullToNull(object value)
        {
            return (value is DBNull ? null : value);
        }

        public static T? DbNullToNull<T>(object value)
            where T : struct
        {
            return (value is DBNull ? null : (T?)value);
        }

        public static object NullToDbNull(object value)
        {
            return (value ?? DBNull.Value);
        }

        public static string GetString(IDataReader reader, int i)
        {
            return (reader.IsDBNull(i) ? null : reader.GetString(i));
        }

        public static T? GetValue<T>(IDataReader reader, int i)
            where T : struct
        {
            return (reader.IsDBNull(i) ? null : (T?)reader.GetValue(i));
        }

        public static T? GetValue<T>(object[] values, int i)
            where T : struct
        {
            object value = values[i];
            return (value == null ? null : (T?)value);
        }

        public static object GetScalar(IDbConnectionFactory connectionFactory, string sql)
        {
            using (IDbConnection connection = connectionFactory.CreateConnection())
            {
                connection.Open();
                return GetScalar(connection, sql);
            }
        }

        public static object GetScalar(IDbConnectionFactory connectionFactory, string sql, params object[] pars)
        {
            using (IDbConnection connection = connectionFactory.CreateConnection())
            {
                connection.Open();
                return GetScalar(connection, sql, pars);
            }
        }

        public static object GetScalar(IDbConnection connection, string sql)
        {
            return GetScalar(connection, DefaultDbTimeoutSeconds, sql);
        }

        public static object GetScalar(IDbConnection connection, string sql, params object[] pars)
        {
            return GetScalar(connection, DefaultDbTimeoutSeconds, sql, pars);
        }

        public static object GetScalar(IDbConnection connection, int timeoutSecs, string sql,
            params object[] parameters)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.CommandTimeout = timeoutSecs;

                if (AddCommandParameters(command, parameters))
                {
                    InlineParameters(command);
                }

                return ExecuteInternal(command, cmd => cmd.ExecuteScalar());
            }
        }

        public static int ExecuteNonQuery(IDbConnectionFactory connectionFactory, string sql, params object[] keyValuePairs)
        {
            using (IDbConnection connection = connectionFactory.CreateConnection())
            {
                connection.Open();
                return ExecuteNonQuery(connection, sql, keyValuePairs);
            }
        }

        public static int ExecuteStoredProcNonQuery(IDbConnectionFactory connectionFactory, string sql, params object[] keyValuePairs)
        {
            using (IDbConnection connection = connectionFactory.CreateConnection())
            {
                connection.Open();
                return ExecuteStoredProcNonQuery(connection, sql, keyValuePairs);
            }
        }

        public static int ExecuteNonQuery(IDbConnection connection, string sql, params object[] keyValuePairs)
        {
            return ExecuteNonQuery(CommandType.Text, connection, sql, keyValuePairs);
        }

        public static int ExecuteStoredProcNonQuery(IDbConnection connection, string sql, params object[] keyValuePairs)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, connection, sql, keyValuePairs);
        }

        private static int ExecuteNonQuery(CommandType type, IDbConnection connection, string sql,
            params object[] parameters)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandType = type;
                command.CommandText = sql;

                if (AddCommandParameters(command, parameters))
                {
                    InlineParameters(command);
                }

                try
                {
                    return ExecuteInternal(command, cmd => cmd.ExecuteNonQuery());
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("Failed to execute command '{0}'.", sql), ex);
                }
            }
        }

        /// <summary>
        /// Returns true if the specified connection is connected as a member of the sysadmin fixed server role.
        /// </summary>
        public static bool IsSysAdmin(IDbConnection connection)
        {
            return ((int)GetScalar(connection, "SELECT IS_SRVROLEMEMBER('sysadmin')") == 1);
        }

        public static IDbCommand CreateSpCommand(IDbConnection connection, string storeProcedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            IDbCommand command = connection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storeProcedureName;

            return command;
        }

        public static IDbCommand CreateTextCommand(IDbConnection connection, string commandText,
            int commandTimeoutSecs, IDbTransaction transaction)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            IDbCommand command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            command.CommandTimeout = commandTimeoutSecs;
            command.Transaction = transaction;

            return command;
        }

        public static IDbDataParameter AddParameter(IDbCommand command, string name, DbType type, object value)
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

            if (command.Parameters.Count > maxParametersPerCommand)
            {
                throw new ApplicationException(string.Format("Too many parameters have been added to command"
                    + " '{0}. Commands with over {1} parameters may fail with an SQL \"invalid buffer\" error.",
                    command.CommandText, maxParametersPerCommand));
            }

            return parameter;
        }

        public static string GetDisplayCommand(IDbCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (command.Parameters.Count == 0)
                return command.CommandText;

            // Replace parameter names with their values.

            Hashtable parameters = new Hashtable(command.Parameters.Count);
            foreach (IDataParameter param in command.Parameters)
            {
                parameters.Add(param.ParameterName, GetParamDeclareValue(param));
            }

            StringBuilder sb = new StringBuilder(command.CommandText);

            char[] charsToFind = new[] { '\'', '@' };
            bool inQuotes = false;

            int index = sb.ToString().IndexOfAny(charsToFind);
            while (index != -1)
            {
                if (sb[index] == '\'')
                {
                    inQuotes = !inQuotes;
                }
                else if (!inQuotes)
                {
                    Debug.Assert(sb[index] == '@', "sb[index] == '@'");

                    int endIndex = index;
                    while (endIndex < sb.Length - 1 && char.IsLetterOrDigit(sb[endIndex + 1]))
                    {
                        endIndex++;
                    }

                    string paramName = sb.ToString().Substring(index, endIndex - index + 1);

                    string paramValue = (string)parameters[paramName];
                    if (paramValue != null)
                    {
                        sb.Remove(index, endIndex - index + 1);
                        sb.Insert(index, paramValue);
                        index += paramValue.Length - 1;
                    }
                }

                if (index == sb.Length - 1)
                    break;

                index = sb.ToString().IndexOfAny(charsToFind, index + 1);
            }

            return sb.ToString();
        }



        public static bool AreAllNull(object[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException("The input array is null or empty.", "values");

            foreach (object value in values)
            {
                if (!(value == null || value is DBNull))
                    return false;
            }

            return true;
        }

        private static string GetParamDeclareType(IDbDataParameter param)
        {
            switch (param.DbType)
            {
                case DbType.AnsiStringFixedLength:
                    return "CHAR(" + GetParamDeclareLength(param) + ")";

                case DbType.AnsiString:
                    return "VARCHAR(" + GetParamDeclareLength(param) + ")";

                case DbType.StringFixedLength:
                    return "NCHAR(" + GetParamDeclareLength(param) + ")";

                case DbType.String:
                    // For some reason other types are defaulted to "string", so don't entirely trust the DbType.
                    // Check the type of the value.

                    if (param.Value is bool)
                        goto case DbType.Boolean;
                    else if (param.Value is byte[])
                        goto case DbType.Binary;
                    else
                        return "NVARCHAR(" + GetParamDeclareLength(param) + ")";

                case DbType.Binary:
                    return "VARBINARY(" + GetParamDeclareLength(param) + ")";

                case DbType.Boolean:
                    return "BIT";

                case DbType.Guid:
                    return "UNIQUEIDENTIFIER";

                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Time:
                    return "DATETIME";

                case DbType.Byte:
                    return "TINYINT";

                case DbType.Currency:
                    return "MONEY";

                case DbType.Decimal:
                    return "DECIMAL";

                case DbType.Double:
                case DbType.Single:
                    return "FLOAT";

                case DbType.Int16:
                    return "SMALLINT";

                case DbType.Int32:
                    return "INT";

                case DbType.Int64:
                    return "BIGINT";

                default:
                    throw new ArgumentException("Unsupported parameter type: " + param.DbType, "param");
            }
        }

        private static int GetParamDeclareLength(IDbDataParameter param)
        {
            if (param.Size != 0)
                return param.Size;

            // If the value is NULL or empty string or empty binary array that's fine, but SQL doesn't
            // accept VARCHAR(0), so return 1 instead.

            if (param.Value == null || param.Value is DBNull || (param.Value as string) == string.Empty)
                return 1;

            var array = param.Value as byte[];
            if (array != null && array.Length == 0)
                return 1;

            throw new ArgumentException(string.Format(
                "Parameter.Size for parameter '{0}' is 0, even though it's not null or empty: '{1}'.",
                param.ParameterName, param.Value), "param");
        }

        private static string GetParamDeclareValue(IDataParameter param)
        {
            if (param.Value == null)
                return null;
            else if (param.Value is DBNull)
                return "NULL";

            switch (param.DbType)
            {
                case DbType.AnsiStringFixedLength:
                case DbType.AnsiString:
                    return "'" + TextUtil.EscapeSqlText(param.Value.ToString()) + "'";

                case DbType.StringFixedLength:
                    return "N'" + TextUtil.EscapeSqlText(param.Value.ToString()) + "'";

                case DbType.String:
                    // For some reason other types are defaulted to "string", so don't entirely trust the DbType.
                    // Check the type of the value.

                    if (param.Value is bool)
                        goto case DbType.Boolean;
                    else if (param.Value is byte[])
                        goto case DbType.Binary;
                    else
                        return "N'" + TextUtil.EscapeSqlText(param.Value.ToString()) + "'";

                case DbType.Binary:
                    return GetBinaryAsSqlText((byte[])param.Value);

                case DbType.Boolean:
                    return Convert.ToBoolean(param.Value) ? "1" : "0";

                case DbType.Guid:
                    return "'" + ((Guid)param.Value).ToString("D").ToUpper() + "'";

                case DbType.Date:
                    return "'" + ((DateTime)param.Value).ToString(dateFormat) + "'";

                case DbType.DateTime:
                case DbType.DateTime2:
                    return "'" + ((DateTime)param.Value).ToString(dateTimeFormat) + "'";

                case DbType.Time:
                    return "'" + ((DateTime)param.Value).ToString(timeFormat) + "'";

                // Converstion are necessary for numeric types, because the actual value may be of a different type,
                // eg. an enum, so ToString() may return something other than the number.

                case DbType.Byte:
                    return Convert.ToByte(param.Value).ToString();

                case DbType.Int16:
                    return Convert.ToInt16(param.Value).ToString();

                case DbType.Int32:
                    return Convert.ToInt32(param.Value).ToString();

                case DbType.Int64:
                    return Convert.ToInt64(param.Value).ToString();

                case DbType.Single:
                    return Convert.ToSingle(param.Value).ToString();

                case DbType.Double:
                    return Convert.ToDouble(param.Value).ToString();

                case DbType.Currency:
                case DbType.Decimal:
                    return Convert.ToDecimal(param.Value).ToString();

                default:
                    Debug.Fail("What's the proper way to declare type " + param.DbType + " in SQL?");
                    return param.Value.ToString();
            }
        }

        private static bool ShouldInlineParameter(IDataParameter parameter)
        {
            const int maxStringLengthToInline = 500;

            if (parameter.Value is DBNull || parameter.Value is Guid)
                return true;

            var binary = parameter.Value as byte[];
            if (binary != null)
                return (binary.Length <= maxStringLengthToInline / 2);

            var value = parameter.Value as IConvertible;
            if (value == null)
                return false;

            switch (value.GetTypeCode())
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.DateTime:
                case TypeCode.DBNull:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;

                case TypeCode.String:
                    return ((string)value).Length <= maxStringLengthToInline;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the SQL script representation of the specified value, suitable for an INSERT script.
        /// </summary>
        public static string GetSqlScriptValue(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is DBNull)
                return "NULL";
            else if (value is string)
                return "'" + TextUtil.EscapeSqlText((string)value) + "'";
            else if (value is byte[])
                return GetBinaryAsSqlText((byte[])value);
            else if (value is bool)
                return ((bool)value ? "1" : "0");
            else if (value is Guid)
                return "'" + ((Guid)value).ToString("D").ToUpper() + "'";
            else if (value is DateTime)
                return "'" + ((DateTime)value).ToString(dateTimeFormat) + "'";
            else
                return value.ToString();
        }

        private static string GetBinaryAsSqlText(byte[] bytes)
        {
            if (bytes.Length == 0)
                return "''";

            StringBuilder sb = new StringBuilder("0x", bytes.Length * 2 + 2);

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public static IDataReader ExecuteReader(IDbConnection connection, string sql, params object[] parameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("The SQL to execute must be specified.", "sql");

            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            if (AddCommandParameters(command, parameters))
            {
                InlineParameters(command);
            }

            try
            {
                return ExecuteInternal(command, cmd => cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to execute command '{0}'.", sql), ex);
            }
        }

        public static IDataReader ExecuteReader(IDbConnection connection, string sql, TimeSpan? timeout, params object[] parameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("The SQL to execute must be specified.", "sql");

            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            if (timeout.HasValue)
                command.CommandTimeout = (int)timeout.Value.TotalSeconds;

            if (AddCommandParameters(command, parameters))
            {
                InlineParameters(command);
            }

            try
            {
                return ExecuteInternal(command, cmd => cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to execute command '{0}'.", sql), ex);
            }
        }

        public static string GetConnectionDisplayString(IDbConnection connection)
        {
            DbConnection dbConnection = connection as DbConnection;
            if (dbConnection != null)
                return "database '" + dbConnection.Database + "' on server '" + dbConnection.DataSource + "'";
            else
                return BlankOutConnectionStringPassword(connection.ConnectionString);
        }

        public static object TimeExecuteScalar(IDbCommand command)
        {
            return TimeExecuteInternal(command, cmd => cmd.ExecuteScalar());
        }

        public static IDataReader TimeExecuteReader(IDbCommand command)
        {
            return TimeExecuteInternal(command, cmd => cmd.ExecuteReader());
        }

        public static int TimeExecuteNonQuery(IDbCommand command)
        {
            return TimeExecuteInternal(command, cmd => cmd.ExecuteNonQuery());
        }

        private static T TimeExecuteInternal<T>(IDbCommand command, ExecuteCommand<T> executeCommand)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            Stopwatch stopwatch = Stopwatch.StartNew();
            T value;

            try
            {
                value = ExecuteInternal(command, executeCommand);
            }
            catch (Exception ex)
            {
                LogCommandIfSlow(command, stopwatch, ex);
                throw;
            }

            LogCommandIfSlow(command, stopwatch, null);
            return value;
        }

        private static T ExecuteInternal<T>(IDbCommand command, ExecuteCommand<T> execute)
        {
//            const string method = "ExecuteInternal";

            if (command == null)
                throw new ArgumentNullException("command");

            try
            {
                return execute(command);
            }
            catch (SqlException ex)
            {
                if (command.Transaction != null)
                    throw; // If in a transaction we would need to re-run the whole transaction, not just this command.

                if (ex.Number == 1205)
                {
                    // Timeout or deadlock - log and try again.

//                    if (_eventSource.IsEnabled(Event.Warning))
  //                  {
    //                    _eventSource.Raise(Event.Warning, method, "An SQL deadlock was detected. Re-trying...",
      //                      Event.Arg("CommandText", command.CommandText));
        //            }

                    return execute(command);
                }
                else
                    throw;
            }
        }

        private static void LogCommandIfSlow(IDbCommand command, Stopwatch stopwatch, Exception exception)
        {
//            const string method = "LogCommandIfSlow";

            stopwatch.Stop();

/*            if (stopwatch.ElapsedMilliseconds >= logSlowCommandThresholdMs && _eventSource.IsEnabled(Event.Warning))
            {
                string messageFormat = (exception == null ? "Database command executed for {0} s."
                    : "Database command failed after {0} ms.");
                string message = string.Format(messageFormat,
                    (float)stopwatch.ElapsedMilliseconds / 1000, command.CommandTimeout);

                var args = new EventArg[command.Parameters.Count + 2];
                args[0] = Event.Arg("CommandText", command.CommandText);
                args[1] = Event.Arg("CommandTimeout", command.CommandTimeout);

                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    var param = (IDataParameter)command.Parameters[i];
                    args[i + 2] = Event.Arg(string.Format("Parameter {0}: {1}", i, param.ParameterName),
                        param.Value);
                }

                _eventSource.Raise(Event.Warning, method, message, exception, args);
            }
*/
        }

        private static bool AddCommandParameters(IDbCommand command, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return false;

            if (parameters.Length % 2 != 0)
                throw new ArgumentException("The number of elements in the parameters array must be even.");

            bool shouldInline = true;

            for (int i = 0; i < parameters.Length - 1; i += 2)
            {
                IDbDataParameter parameter = command.CreateParameter();

                parameter.ParameterName = (string)parameters[i];
                if (string.IsNullOrEmpty(parameter.ParameterName))
                    throw new ArgumentException("The parameter name at index " + i + " is null or empty.");
                if (!parameter.ParameterName.StartsWith("@"))
                    throw new ArgumentException("The parameter name at index " + i + " does not start with '@'.");

                parameter.Value = parameters[i + 1];
                command.Parameters.Add(parameter);

                shouldInline &= ShouldInlineParameter(parameter);
            }

            return shouldInline;
        }
    }
}

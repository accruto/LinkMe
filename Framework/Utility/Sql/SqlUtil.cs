using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Sql
{
	/// <summary>
	/// Provides static methods for manipulating SQL databases.
	/// </summary>
	public sealed class SqlUtil
	{
		private static Regex m_providerRegex = null;

		private SqlUtil()
		{
		}

		/// <summary>
		/// Changes an OLE DB connection string to one that can be passed to the SqlConnection class.
		/// </summary>
		/// <param name="connectionString">The OLE DB connection string.</param>
		/// <returns>The SQL connection string.</returns>
		/// <remarks>
		/// The constructor for SqlConnection does not handle "Provider=SQLOLEDB.1" in the connection string. This
		/// method checks for this string (including variations, like "SQLOLEDB" with no version, extra spaces, etc.)
		/// and removes it, if present.
		/// </remarks>
		public static string OleDbToSqlConnectionString(string connectionString)
		{
			const string method = "OleDbToSqlConnectionString";

			if (connectionString == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

			if (m_providerRegex == null)
			{
				m_providerRegex = new Regex(Constants.Sql.ProviderRegex,
					RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
			}

			Match match = m_providerRegex.Match(connectionString);
			if (match.Success)
				return connectionString.Remove(match.Index, match.Length);
			else
				return connectionString;
		}

		/// <summary>
		/// Creates an SqlConnection object for a connection string. 
		/// </summary>
		/// <param name="connectionString">The SQL connection string.</param>
		/// <returns>An SqlConnection object for the specified connection string.</returns>
		/// <remarks>
		/// The constructor for SqlConnection does not handle "Provider=SQLOLEDB.1" in the connection string. This
		/// method checks for this string (including variations, like "SQLOLEDB" with no version, extra spaces, etc.)
		/// and removes it, if present.
		/// </remarks>
		public static SqlConnection CreateSqlConnection(string connectionString)
		{
			const string method = "CreateSqlConnection";

			if (connectionString == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

			try
			{
				return new SqlConnection(OleDbToSqlConnectionString(connectionString));
			}
			catch (System.ArgumentException ex)
			{
				throw new InvalidParameterFormatException(typeof(SqlUtil), method, "connectionString",
					connectionString, "SQL connection string", ex);
			}
		}

		/// <summary>
		/// Open a connection to the specified database, creating the database if it does not already exist.
		/// </summary>
		/// <param name="connectionString">The SQL connection string.</param>
		/// <returns>An open SqlConnection object for the specified connection string.</returns>
		/// <remarks>
		/// This method creates the database specified by Initial Catalogue in the input connection string, if the databse
		/// does not already exist, then creates and opens an SqlConnection to that database. It reuses the same
		/// connection, which is more effecient (even with connection pooling) than establishing three separate
		/// connections to check whether the database exists, create the database and finally use it.
		/// </remarks>
		public static SqlConnection CreateAndConnectToDatabase(string connectionString)
		{
			const string method = "CreateAndConnectToDatabase";

			if (connectionString == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

			// Connect to the master database.

			string databaseName = ChangeDatabaseNameInConnectionString(ref connectionString,
				Constants.Sql.MasterDatabaseName);
			SqlConnection connection = CreateSqlConnection(connectionString);

			try
			{
				// Create the specified database if it doesn't already exist.

				if (!DoesDatabaseExist(connection, databaseName))
				{
					CreateDatabase(connection, databaseName);
				}

				// Connect to the specified database.

				connection.ChangeDatabase(databaseName);
			}
			catch (System.Exception)
			{
				connection.Close(); // Close the connection, since we're not returning it.
				throw;
			}

			return connection;
		}

		/// <summary>
		/// Runs multiple SQL scripts against a database in one transaction.
		/// </summary>
		/// <param name="connectionString">Database connection string, including the database name.</param>
		/// <param name="filePaths">Absolute paths of the SQL script files to be run.</param>
		/// <remarks>
		/// This method connects to the database, runs the scripts and disconnects again. To perform multiple tasks
		/// without disconnecting create an SqlConnection by calling <see cref="CreateSqlConnection"/> then call the
		/// overload that takes an SqlConnection.
		/// </remarks>
		public static void RunDbScriptsInTransaction(string connectionString, params string[] filePaths)
		{
			using (SqlConnection connection = CreateSqlConnection(connectionString))
			{
				RunDbScriptsInTransaction(connection, filePaths);
			}
		}

		/// <summary>
		/// Runs multiple SQL scripts against a database in one transaction.
		/// </summary>
		/// <param name="connection">SQL connection pointing at the database against which the scripts should be run.</param>
		/// <param name="filePaths">Absolute paths of SQL script files to be run.</param>
		/// <remarks>
		/// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
		/// to the caller to call SqlConnection.Close().
		/// </remarks>
		public static void RunDbScriptsInTransaction(SqlConnection connection, params string[] filePaths)
		{
			const string method = "RunDbScriptsInTransaction";

			if (connection == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connection");
			if (filePaths == null)
				throw new NullParameterException(typeof(SqlUtil), method, "filePaths");

			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}

			SqlTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (string filePath in filePaths)
				{
					RunDbScript(connection, filePath, transaction);
				}
			}
			catch (System.Exception)
			{
				try
				{
					transaction.Rollback();
				}
				catch (System.Exception)
				{
				}

				throw; // Propogate the original exception, not one that may have occurred in rolling back.
			}

			transaction.Commit();
		}

		/// <summary>
		/// Runs an SQL script against a database.
		/// </summary>
		/// <param name="connection">SQL connection pointing at the database against which the scripts should be run.</param>
		/// <param name="filePath">Absolute path of the SQL script file to be run.</param>
		/// <param name="transaction">SQL transaction in which this script is to be run. Specify null to run without
		/// a transaction.</param>
		/// <remarks>
		/// This method opens the connection, if it is closed, but does not close the connection on exit. If
		/// <paramref name="transaction"/> is not null all statements in the script are run in that SQL transaction, but
		/// this method does not Commit or Abort the transaction, even if an exception is thrown. It is up to the caller
		/// to call SqlConnection.Close() and SqlTransaction.Commmit() or SqlTransaction.Abort(), as appropriate.
		/// </remarks>
		public static void RunDbScript(SqlConnection connection, string filePath, SqlTransaction transaction)
		{
			const string method = "RunDbScript";
			const int initialStringCapacity = 2048;

			if (connection == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connection");
			if (filePath == null)
				throw new NullParameterException(typeof(SqlUtil), method, "filePath");
			if (!File.Exists(filePath))
				throw new PathNotFoundException(typeof(SqlUtil), method, filePath);

			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}

			StringBuilder sb = new StringBuilder(initialStringCapacity);
			using (StreamReader reader = new StreamReader(filePath))
			{
				string line = reader.ReadLine();
				while (line != null)
				{
					string trimmed = line.TrimStart();

					// Check whether the line is the end of the command. It is if it begins with "GO",
					// followed by whitespace or the end of the line.

					if (string.Compare(trimmed, 0, "go", 0, 2, true) == 0 &&
						(trimmed.Length == 2 || char.IsWhiteSpace(trimmed, 2)))
					{
						// Execute the command read so far.

						string commandText = sb.ToString();
						SqlCommand command = new SqlCommand(commandText, connection, transaction);

						try
						{
							command.ExecuteNonQuery();
						}
						catch (System.Exception ex)
						{
							throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
						}

						// Clear the command buffer.

						sb = new StringBuilder(initialStringCapacity);
					}
					else
					{
						sb.Append(line);
						sb.Append(System.Environment.NewLine);
					}

					line = reader.ReadLine();
				}
			}
		}

		/// <summary>
		/// Creates a new database.
		/// </summary>
		/// <param name="connectionString">Connection string pointing to the database to be created.</param>
		/// <remarks>
		/// The initial catalogue specified in the connection string will be used as the name of the new database.
		/// 
		/// This method connects to the server, creates the database and disconnects again. To perform multiple tasks
		/// without disconnecting create an SqlConnection by calling <see cref="CreateSqlConnection"/> then call the
		/// overload that takes an SqlConnection.
		/// </remarks>
		public static void CreateDatabase(string connectionString)
		{
			ExecuteDatabaseCommand(connectionString, "CREATE DATABASE");
		}	

		/// <summary>
		/// Creates a new database.
		/// </summary>
		/// <param name="connection">SQL connection to the server on which the database is to be created.</param>
		/// <param name="databaseName">Name of the new database to create.</param>
		/// <remarks>
		/// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
		/// to the caller to call SqlConnection.Close().
		/// </remarks>
		public static void CreateDatabase(SqlConnection connection, string databaseName)
		{
			ExecuteDatabaseCommand(connection, databaseName, "CREATE DATABASE");
		}

		/// <summary>
		/// Deletes a database.
		/// </summary>
		/// <param name="connectionString">Connection string pointing to the database to delete.</param>
		/// <remarks>
		/// The initial catalogue specified in the connection string is the database that will be deleted.
		/// 
		/// This method connects to the server, deletes the database and disconnects again. To perform multiple tasks
		/// without disconnecting create an SqlConnection by calling <see cref="CreateSqlConnection"/> then call the
		/// overload that takes an SqlConnection.
		/// </remarks>
		public static void DeleteDatabase(string connectionString)
		{
			ExecuteDatabaseCommand(connectionString, "DROP DATABASE");
		}

		/// <summary>
		/// Deletes a database.
		/// </summary>
		/// <param name="connection">SQL connection to the server on which the database exists.</param>
		/// <param name="databaseName">Name of the database to delete.</param>
		/// <remarks>
		/// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
		/// to the caller to call SqlConnection.Close().
		/// </remarks>
		public static void DeleteDatabase(SqlConnection connection, string databaseName)
		{
			ExecuteDatabaseCommand(connection, databaseName, "DROP DATABASE");
		}

		/// <summary>
		/// Checks whether a database exists.
		/// </summary>
		/// <param name="connectionString">Connection string pointing to the database to be checked.</param>
		/// <returns>True if the database exists on the server, otherwise false.</returns>
		/// <remarks>
		/// This method connects to the server, executes a query to check look for the specified database and disconnects
		/// again. To perform multiple tasks without disconnecting create an SqlConnection by calling
		/// <see cref="CreateSqlConnection"/> then call the overload that takes an SqlConnection.
		/// </remarks>
		public static bool DoesDatabaseExist(string connectionString)
		{
			const string method = "DoesDatabaseExist";

			if (connectionString == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

			// Ignore the database name in the connection string - the "sysdatabases" table is in "master".

			string databaseName = ChangeDatabaseNameInConnectionString(ref connectionString,
				Constants.Sql.MasterDatabaseName);

			using (SqlConnection connection = CreateSqlConnection(connectionString))
			{
				return DoesDatabaseExist(connection, databaseName);
			}
		}	

		/// <summary>
		/// Checks whether a database exists.
		/// </summary>
		/// <param name="connection">SQL connection to the server to check.</param>
		/// <param name="databaseName">Name of the database to look for.</param>
		/// <returns>True if the database exists on the server, otherwise false.</returns>
		/// <remarks>
		/// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
		/// to the caller to call SqlConnection.Close().
		/// </remarks>
		public static bool DoesDatabaseExist(SqlConnection connection, string databaseName)
		{
			const string method = "DoesDatabaseExist";
			const string query = "SELECT COUNT(*) FROM sysdatabases WHERE name = @databaseName";

			if (connection == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connection");
			if (databaseName == null)
				throw new NullParameterException(typeof(SqlUtil), method, "databaseName");

			string unquotedName = GetRawDatabaseName(databaseName);

			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}

			// Change to the master database.

			string originalDb = connection.Database;
			if (originalDb != Constants.Sql.MasterDatabaseName)
			{
				connection.ChangeDatabase(Constants.Sql.MasterDatabaseName);
			}

			// Run a SELECT command to find a database with the specified name.

			try
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.CommandType = CommandType.Text;
				command.Parameters.Add("@databaseName", SqlDbType.NChar).Value = unquotedName;

				try
				{
					return ((int)command.ExecuteScalar() > 0);
				}
				catch (System.Exception ex)
				{
					throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
				}
			}
			finally
			{
				if (originalDb != Constants.Sql.MasterDatabaseName)
				{
					connection.ChangeDatabase(originalDb);
				}
			}
		}

        /// <summary>
        /// Checks whether a table exists.
        /// </summary>
        /// <param name="connectionString">Connection string pointing to the database to be checked.</param>
        /// <param name="tableName">Name of the table to check.</param>
        /// <returns>True if the table exists on the server, otherwise false.</returns>
        /// <remarks>
        /// This method connects to the server, executes a query to check look for the specified table and disconnects
        /// again. To perform multiple tasks without disconnecting create an SqlConnection by calling
        /// <see cref="CreateSqlConnection"/> then call the overload that takes an SqlConnection.
        /// </remarks>
        public static bool DoesTableExist(string connectionString, string tableName)
        {
            const string method = "DoesTableExist";

            if (connectionString == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

            using (SqlConnection connection = CreateSqlConnection(connectionString))
            {
                return DoesTableExist(connection, tableName);
            }
        }

        /// <summary>
        /// Checks whether a table within a database exists.
        /// </summary>
        /// <param name="connection">SQL connection to the server to check.</param>
        /// <param name="tableName">Name of the table to look for.</param>
        /// <returns>True if the table exists in the database, otherwise false.</returns>
        /// <remarks>
        /// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
        /// to the caller to call SqlConnection.Close().
        /// </remarks>
        public static bool DoesTableExist(SqlConnection connection, string tableName)
        {
            const string method = "DoesTableExist";

            if (connection == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connection");
            if (tableName == null)
                throw new NullParameterException(typeof(SqlUtil), method, "tableName");

            string query = "SELECT COUNT(*) FROM dbo.sysobjects WHERE id = object_id(N'" + tableName + "') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            // Run a SELECT command to find the table with the specified name.

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;

            try
            {
                return ((int)command.ExecuteScalar() > 0);
            }
            catch (System.Exception ex)
            {
                throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
            }
        }

        /// <summary>
        /// Checks whether a stored procedure exists.
        /// </summary>
        /// <param name="connectionString">Connection string pointing to the database to be checked.</param>
        /// <param name="storedProcedureName">Name of the stored procedure to check.</param>
        /// <returns>True if the stored procedure exists on the server, otherwise false.</returns>
        /// <remarks>
        /// This method connects to the server, executes a query to check look for the specified stored procedure and disconnects
        /// again. To perform multiple tasks without disconnecting create an SqlConnection by calling
        /// <see cref="CreateSqlConnection"/> then call the overload that takes an SqlConnection.
        /// </remarks>
        public static bool DoesStoredProcedureExist(string connectionString, string storedProcedureName)
        {
            const string method = "DoesStoredProcedureExist";

            if (connectionString == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

            using (SqlConnection connection = CreateSqlConnection(connectionString))
            {
                return DoesStoredProcedureExist(connection, storedProcedureName);
            }
        }

        /// <summary>
        /// Checks whether a stored procedure within a database exists.
        /// </summary>
        /// <param name="connection">SQL connection to the server to check.</param>
        /// <param name="storedProcedureName">Name of the stored procedure to look for.</param>
        /// <returns>True if the stored procedure exists in the database, otherwise false.</returns>
        /// <remarks>
        /// This method opens the connection, if it is closed, but does not close the connection on exit. It is up
        /// to the caller to call SqlConnection.Close().
        /// </remarks>
        public static bool DoesStoredProcedureExist(SqlConnection connection, string storedProcedureName)
        {
            const string method = "DoesStoredProcedureExist";

            if (connection == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connection");
            if (storedProcedureName == null)
                throw new NullParameterException(typeof(SqlUtil), method, "storedProcedureName");

            string query = "SELECT COUNT(*) FROM dbo.sysobjects WHERE id = object_id(N'" + storedProcedureName + "') AND OBJECTPROPERTY(id, N'IsProcedure') = 1";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            // Run a SELECT command to find the table with the specified name.

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;

            try
            {
                return ((int)command.ExecuteScalar() > 0);
            }
            catch (System.Exception ex)
            {
                throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
            }
        }

        public static bool DoUserTablesExist(string connectionString)
        {
            const string method = "DoUserTablesExist";

            if (connectionString == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

            using (SqlConnection connection = CreateSqlConnection(connectionString))
            {
                return DoUserTablesExist(connection);
            }
        }

        public static bool DoUserTablesExist(SqlConnection connection)
        {
            const string method = "DoUserTablesExist";

            if (connection == null)
                throw new NullParameterException(typeof(SqlUtil), method, "connection");

            string query = "SELECT COUNT(*) FROM dbo.sysobjects WHERE OBJECTPROPERTY(id, N'IsUserTable') = 1";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            // Run a SELECT command to find the table with the specified name.

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;

            try
            {
                return ((int)command.ExecuteScalar() > 0);
            }
            catch (System.Exception ex)
            {
                throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
            }
        }

        /// <summary>
		/// Escapes a string for use as a pattern that can be passed to the SQL "LIKE" operator.
		/// </summary>
		/// <param name="text">The string to escape.</param>
		/// <returns>The escaped string or null if the input string was null.</returns>
		/// <remarks>To escape the string this method replaces "[" with "[[]", "%" with "[%]" and "_" with "[_]".
		/// </remarks>
		public static string EscapeSqlLike(string text)
		{
			if (text == null || text.Length == 0)
				return text;

			StringBuilder sb = new StringBuilder(text, text.Length);
			EscapeSqlLike(sb);

			return sb.ToString();
		}

		/// <summary>
		/// Converts a regular expression to pattern than can be passed to the SQL "LIKE" operator, if possible.
		/// </summary>
		/// <param name="regex">The regular expression to convert.</param>
		/// <returns>An SQL LIKE pattern equivalent to the supplied regular expression, if one exists,
		/// otherwise null.</returns>
		public static string RegexToSqlLike(string regex)
		{
			if (regex == null || regex.Length == 0)
				return "%"; // Match anything.

			// Check for special characters that match the start and end of the string.

			StringBuilder sb = new StringBuilder(regex);

			bool matchStart = regex.StartsWith("^");
			if (matchStart)
			{
				sb.Remove(0, 1);
			}

			bool matchEnd = regex.EndsWith("$");
			if (matchEnd)
			{
				sb.Remove(sb.Length - 1, 1);
			}

			// Escape the regex as a LIKE pattern. This must be done before any real wildcards are inserted.

			EscapeSqlLike(sb);

			// Replace ".*" with "%", ".+" with "_%" and "." with "_".

			string temp = sb.ToString();
			int index = temp.IndexOf('.');

			while (index != -1)
			{
				// See if the dot is escaped, ie. has an odd number of backslahes before it.

				int slashIndex = index - 1;
				while (slashIndex > 0 && temp[slashIndex] == '\\')
					slashIndex--;

				if ((index - slashIndex) % 2 ==1)
				{
					// The dot is not escaped - process it. What follows the dot, ie. what kind of wildcard is it?

					string toReplace = ".";
					string replaceWith = "_";

					if (index < temp.Length - 1)
					{
						switch (temp[index + 1])
						{
							case '?':
								return null; // Single optional character is not supported by SQL LIKE.

							case '*':
								toReplace = ".*";
								replaceWith = "%";
								break;

							case '+':
								toReplace = ".+";
								replaceWith = "_%";
								break;
						}
					}

					// Replace.

					sb = new StringBuilder(temp);
					sb.Replace(toReplace, replaceWith, index, toReplace.Length);
					temp = sb.ToString();
				}

				if (index == temp.Length - 1)
					break; // End of the string.

				index = temp.IndexOf('.', index + 1); // Find the next dot.
			}

			// Unescape it as a regex.

			try
			{
				temp = Regex.Unescape(temp);
			}
			catch (System.ArgumentException)
			{
				return null; // String still contains regex expressions - not converted successfully.
			}

			// Append and prepend '%', as necessary.

			if (!matchStart)
			{
				if (!matchEnd)
					return "%" + temp + "%";
				else
					return "%" + temp;
			}
			else if (!matchEnd)
				return temp + "%";
			else
				return temp;
		}

		/// <summary>
		/// Creates a single SqlCommand that contains all the SQL statements from all the supplied commands.
		/// </summary>
		/// <param name="connection">The connection with which the returned SqlCommand is associated.
		/// This connection does not need to be open.</param>
		/// <param name="commands">The array of SQL commands that are to be aggregated into a single SqlCommand
		/// object. The Parameters collections of each commands will be cleared.</param>
		/// <returns>A single SqlCommand that contains all the SQL statements from all the supplied commands
		/// -or- null if the input commands array was null or empty -or- the input command if there is only
		/// one in the array.</returns>
		/// <remarks>
		/// When executing many SQL statements that accept a small amount of input it is more efficient to execute
		/// them in one "batch" - a single SQL command sent to the server that contains all the SQL statements,
		/// separated by semicolons. This method creates the batch command for the given individual command.
		/// 
		/// The CommandType of each input command must be Text or StoredProcedure - TableDirect commands are not
		/// supported. Note also that the Parameters collection of each input command will be cleared. This is
		/// because an SqlParameter can only be assigned to one SqlCommand object at a time and for efficiency
		/// this method does not attempt to clone the SqlParamater objects.
		/// 
		/// The maximum size of the commands array is specified by the <see cref="Constants.Sql.MaximumCommandBatchSize"/>
		/// constant. If the input array exceeds this length an exception is thrown.
		/// </remarks>
		public static SqlCommand CreateBatchCommand(SqlConnection connection, SqlCommand[] commands)
		{
			const string method = "CreateBatchCommand";

			if (commands == null || commands.Length == 0)
				return null;
			if (connection == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connection");

			if (commands.Length == 1)
			{
				// Only one command - no need to do any batching, but assign the connection to it.

				commands[0].Connection = connection;
				return commands[0];
			}

			if (commands.Length > Constants.Sql.MaximumCommandBatchSize)
			{
				throw new System.ApplicationException(string.Format("The number of SQL commands specified,"
					+ " {0}, is too large for one batch. The maximum batch size is {1}.",
					commands.Length, Constants.Sql.MaximumCommandBatchSize));
			}

			// Keep the parameter list in an ArrayList to preserve the order, which simplifies debugging
			// (especially when the user needs to look at SqlCommandFailedException.CommandParameters), but
			// maintain the names in a Hashtable so that duplicate names can be found efficiently.

			StringBuilder text = new StringBuilder();
			ArrayList parameters = new ArrayList();
			IDictionary parameterNames = new Hashtable();
			int paramSuffix = 0;

			// Build the command text and parameter list for this batch.

			AddCommandToBatch(commands[0], text, parameters, parameterNames, ref paramSuffix);
			for (int index = 1; index < commands.Length; index++)
			{
				text.Append(';');
				text.Append(System.Environment.NewLine);
				AddCommandToBatch(commands[index], text, parameters, parameterNames, ref paramSuffix);
			}

			// Create a batch command.

			SqlCommand batchCommand = new SqlCommand(text.ToString(), connection);
			batchCommand.CommandTimeout = Constants.Sql.BatchCommandTimeout;

			foreach (SqlParameter parameter in parameters)
			{
				batchCommand.Parameters.Add(parameter);
			}

			return batchCommand;
		}

		private static void AddCommandToBatch(SqlCommand command, StringBuilder text, ArrayList parameters,
			IDictionary parameterNames, ref int paramSuffix)
		{
			// Command text.

			switch (command.CommandType)
			{
				case CommandType.StoredProcedure:
					text.Append("EXEC ");
					break;

				case CommandType.Text:
					break;

				default:
					throw new System.NotSupportedException("Command type '" + command.CommandType.ToString()
						+ "' is not supported. Only 'StoredProcedure' and 'Text' commands are supported.");
			}

			text.Append(command.CommandText);

			// Parameters - specify them by name, so that the order does not matter and optional parameters
			// can be left unspecified anywhere in the command (not just at the end).

			bool comma = false;
			for (int index = 0; index < command.Parameters.Count; index++)
			{
				string paramText = AddParameterToBatch(command.Parameters[index], parameters, parameterNames,
					ref paramSuffix);
				if (paramText != null)
				{
					text.Append(comma ? ", " : " ");
					text.Append(paramText);
					comma = true;
				}
			}

			// Clear the Parameters collection for this command, otherwise the SqlParameter objects cannot
			// be added to another to the Parameters collection for the batch command.

			command.Parameters.Clear();
		}

		private static string AddParameterToBatch(SqlParameter parameter, ArrayList parameters,
			IDictionary parameterNames, ref int paramSuffix)
		{
			if (parameter.Value == null)
				return null; // Optional parameter that's not specified, skip it.

			// Create a unique name for this parameter.

			string paramName = parameter.ParameterName;

			string name;
			do
			{
				paramSuffix++;
				name = paramName + "_" + paramSuffix.ToString();
			}
			while (parameterNames.Contains(name));

			parameter.ParameterName = name;

			// Add it.

			parameters.Add(parameter);
			parameterNames.Add(name, null);

			return paramName + "=" + name;
		}

		private static string GetRawDatabaseName(string databaseName)
		{
			const string method = "GetRawDatabaseName";
			const int maxNameLength = 123; // Maximum DB name length when not specifying a logical log file name.

			if (databaseName == null)
				throw new NullParameterException(typeof(SqlUtil), method, "databaseName");

			string unquotedName = databaseName.Trim('[', ']', '\"', '\'');
			if (unquotedName.Length == 0 || unquotedName.Length > maxNameLength)
			{
				throw new ParameterStringLengthOutOfRangeException(typeof(SqlUtil), method, "databaseName",
					databaseName, 0, maxNameLength);
			}

			return unquotedName;
		}

		private static void ExecuteDatabaseCommand(string connectionString, string dbCommand)
		{
			const string method = "ExecuteDatabaseCommand";

			if (connectionString == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connectionString");

			// Read the database name from the connection string, but actually connect to the "master" database.

			string databaseName = ChangeDatabaseNameInConnectionString(ref connectionString,
				Constants.Sql.MasterDatabaseName);

			using (SqlConnection connection = CreateSqlConnection(connectionString))
			{
				ExecuteDatabaseCommand(connection, databaseName, dbCommand);
			}
		}	

		private static void ExecuteDatabaseCommand(SqlConnection connection, string databaseName, string dbCommand)
		{
			const string method = "ExecuteDatabaseCommand";

			if (connection == null)
				throw new NullParameterException(typeof(SqlUtil), method, "connection");

			string unquotedName = GetRawDatabaseName(databaseName);

			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}

			// Change to the master database.

			string originalDb = connection.Database;
			if (originalDb != Constants.Sql.MasterDatabaseName)
			{
				connection.ChangeDatabase(Constants.Sql.MasterDatabaseName);
			}

			// Run the database command (enclose the database name in [] in case it has spaces).

			SqlCommand command = new SqlCommand(dbCommand + " [" + unquotedName + "]", connection);
			command.CommandType = CommandType.Text;

			try
			{
				command.ExecuteNonQuery();
			}
			catch (System.Exception ex)
			{
				throw new SqlCommandFailedException(typeof(SqlUtil), method, command, ex);
			}
			finally
			{
				if (originalDb != Constants.Sql.MasterDatabaseName)
				{
					connection.ChangeDatabase(originalDb);
				}
			}
		}

		/// <summary>
		/// Changes the database name in the specified connection string and returns the old database name.
		/// </summary>
		private static string ChangeDatabaseNameInConnectionString(ref string connectionString, string newDatabaseName)
		{
			const string catalogue = "Initial Catalog=";

			Debug.Assert(connectionString != null && newDatabaseName != null, "connectionString != null && newDatabaseName != null");

			CompareInfo compareInfo = CultureInfo.CurrentCulture.CompareInfo;
			int index = compareInfo.IndexOf(connectionString, catalogue, CompareOptions.IgnoreCase);
			if (index == -1)
				return connectionString + ";" + catalogue + newDatabaseName; // Append the database name.

			int startIndex = index + catalogue.Length;
			int endIndex = compareInfo.IndexOf(connectionString, ';', startIndex);
			int length = (endIndex == -1 ? connectionString.Length - startIndex : endIndex - startIndex);
			string oldDbName = connectionString.Substring(startIndex, length);

			StringBuilder sb = new StringBuilder(connectionString);
			sb.Remove(startIndex, length);
			sb.Insert(startIndex, newDatabaseName);
			connectionString = sb.ToString();

			return oldDbName;
		}

		private static void EscapeSqlLike(StringBuilder sb)
		{
			sb.Replace("[", "[[]");
			sb.Replace("%", "[%]");
			sb.Replace("_", "[_]");
		}
	}
}

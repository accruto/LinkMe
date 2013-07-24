using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace LinkMe.Framework.Utility.Exceptions
{
	#region SqlCommandFailedException

	[System.Serializable]
	public sealed class SqlCommandFailedException
		:	UtilityException
	{
		public SqlCommandFailedException()
			:	base(m_propertyInfos)
		{
		}

		public SqlCommandFailedException(System.Type source, string method, string connectionString, string serverVersion,
			CommandType commandType, string commandText, int commandTimeout, string commandParameters,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(connectionString, serverVersion, commandType, commandText, commandTimeout, commandParameters);
		}

		public SqlCommandFailedException(System.Type source, string method, string connectionString, string serverVersion,
			CommandType commandType, string commandText, int commandTimeout, string commandParameters)
			:	base(m_propertyInfos, source, method)
		{
			Set(connectionString, serverVersion, commandType, commandText, commandTimeout, commandParameters);
		}

		public SqlCommandFailedException(string source, string method, string connectionString, string serverVersion,
			CommandType commandType, string commandText, int commandTimeout, string commandParameters,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(connectionString, serverVersion, commandType, commandText, commandTimeout, commandParameters);
		}

		public SqlCommandFailedException(string source, string method, string connectionString, string serverVersion,
			CommandType commandType, string commandText, int commandTimeout, string commandParameters)
			:	base(m_propertyInfos, source, method)
		{
			Set(connectionString, serverVersion, commandType, commandText, commandTimeout, commandParameters);
		}

		public SqlCommandFailedException(System.Type source, string method, IDbCommand command, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(command);
		}

		public SqlCommandFailedException(System.Type source, string method, IDbCommand command)
			:	base(m_propertyInfos, source, method)
		{
			Set(command);
		}

		public SqlCommandFailedException(string source, string method, IDbCommand command, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(command);
		}

		public SqlCommandFailedException(string source, string method, IDbCommand command)
			:	base(m_propertyInfos, source, method)
		{
			Set(command);
		}

		private SqlCommandFailedException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ConnectionString
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ConnectionString, string.Empty); }
		}

		public string ServerVersion
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ServerVersion, string.Empty); }
		}

		public string CommandType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.CommandType, string.Empty); }
		}

		public string CommandText
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.CommandText, string.Empty); }
		}

		public int CommandTimeout
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.CommandTimeout, string.Empty); }
		}

		public string CommandParameters
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.CommandParameters, string.Empty); }
		}

		#region Static methods

		private static string GetParameterValues(IDataParameterCollection parameters)
		{
			if (parameters.Count == 0)
				return string.Empty;

			StringBuilder sb = new StringBuilder();

			AppendParameterValue(sb, (IDataParameter)parameters[0]);
			for (int index = 1; index < parameters.Count; index++)
			{
				sb.Append(", ");
				AppendParameterValue(sb, (IDataParameter)parameters[index]);
			}

			return sb.ToString();
		}

		private static void AppendParameterValue(StringBuilder sb, IDataParameter param)
		{
			sb.Append(param.ParameterName);
			sb.Append('=');

			if (param.Value == null)
			{
				sb.Append("<unspecified>");
			}
			else if (param.Value is System.DBNull)
			{
				sb.Append("NULL");
			}
			else if (param.Value is string)
			{
				sb.Append('\'');
				sb.Append((string)param.Value);
				sb.Append('\'');
			}
			else
			{
				sb.Append(param.Value);
			}

			if (param.Direction == ParameterDirection.Output)
			{
				sb.Append(" (OUTPUT)");
			}
			else if (param.Direction == ParameterDirection.ReturnValue)
			{
				sb.Append(" (RETVAL)");
			}
		}

		#endregion

		private void Set(IDbCommand command)
		{
			// For easy of use allow the caller to pass in an IDbCommand object and set our properties from it.
			// Fire asserts if the command or its connection is null, but handle it without thrown an exception.

			if (command == null)
			{
				Debug.Fail("Null IDbCommand passed to the '" + GetType().FullName + "' exception.");
				Set(null, null, (CommandType)0, null, 0, null);
			}
			else
			{
				// Save parameters as string values.

				string commandParameters;
				try
				{
					commandParameters = GetParameterValues(command.Parameters);
				}
				catch (System.Exception ex)
				{
					commandParameters = string.Format("An exception of type '{0}' was thrown while trying"
						+ " to save parameter values.", ex.GetType().FullName);
				}

				// Save connection properties.

				IDbConnection connection = command.Connection;
				if (connection == null)
				{
					Debug.Fail("IDbCommand with null connection passed to the '" + GetType().FullName + "' exception.");
					Set(null, null, command.CommandType, command.CommandText, command.CommandTimeout, commandParameters);
				}
				else
				{
					SqlConnection sqlConnection = connection as SqlConnection;
					string serverVersion = null;
					
					if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
					{
						try
						{
							serverVersion = sqlConnection.ServerVersion;
						}
						catch (System.Exception ex)
						{
							serverVersion = string.Format("An exception of type '{0}' was thrown while trying"
								+ " to get the SQL server version.", ex.GetType().FullName);
						}
					}

					Set(connection.ConnectionString, serverVersion,  command.CommandType, command.CommandText,
						command.CommandTimeout, commandParameters);
				}
			}
		}

		private void Set(string connectionString, string serverVersion, CommandType commandType, string commandText,
			int commandTimeout, string commandParameters)
		{
			SetPropertyValue(Constants.Exceptions.ConnectionString, connectionString == null ? string.Empty : connectionString);
			SetPropertyValue(Constants.Exceptions.ServerVersion, serverVersion == null ? string.Empty : serverVersion);
			SetPropertyValue(Constants.Exceptions.CommandType, commandType.ToString());
			SetPropertyValue(Constants.Exceptions.CommandText, commandText == null ? string.Empty : commandText);
			SetPropertyValue(Constants.Exceptions.CommandTimeout, commandTimeout);
			SetPropertyValue(Constants.Exceptions.CommandParameters, commandParameters == null ? string.Empty : commandParameters);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ConnectionString, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ServerVersion, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.CommandType, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.CommandText, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.CommandTimeout, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.CommandParameters, System.TypeCode.String)
		};
	}

	#endregion
}

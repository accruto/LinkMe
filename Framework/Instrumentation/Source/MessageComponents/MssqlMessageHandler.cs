using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml;
using LinkMe.Framework.Instrumentation.Constants;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class MssqlMessageHandler 
		:	BaseMessageHandler
	{
		private readonly string _connectionString;

	    public MssqlMessageHandler(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

	    #region Static methods

		private static void AddParameter(SqlCommand command, string name, SqlDbType type, object value)
		{
			command.Parameters.Add(name, type).Value = (value ?? System.DBNull.Value);
		}

		#endregion

		protected override void HandleEventMessage(EventMessage message)
		{
			Debug.Assert(message != null, "message != null");

			SetMessageSequence(message);

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				
				InsertMessage(connection, message);
			}
		}

		protected override void HandleEventMessages(EventMessages messages)
		{
			Debug.Assert(messages != null, "messages != null");

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				foreach (EventMessage message in messages)
				{
					SetMessageSequence(message);
					InsertMessage(connection, message);
				}
			}
		}

		private static void InsertMessage(SqlConnection connection, EventMessage message)
		{
            // Serialize the exception (if any) to XML.

            string exceptionXml = null;

            if (message.Exception != null)
            {
                using (var stringWriter = new StringWriter())
                {
                    var writer = new XmlTextWriter(stringWriter);
                    message.Exception.WriteOuterXml(writer);
                    writer.Close();
                    exceptionXml = stringWriter.ToString();
                }
            }

		    // Serialize the event details (if any) to XML.

			Utility.Event.EventDetails details = message.Details;
			string detailsXml = null;

			if (details.Count > 0)
			{
				try
				{
					detailsXml = XmlSerializer.Serialize(details);
				}
				catch (System.Exception	ex)
				{
					detailsXml = Constants.MessageComponents.Mssql.DetailsErrorPrefix + ex;
				}
			}

			int messageId;
			using (var command = new SqlCommand(Sql.InsertMessage, connection))
			{
				command.CommandType = CommandType.StoredProcedure;

				// Store the message time as ticks in the database to avoid losing precision (SQL datetime
				// only goes down to 1/300th of a second. Note that the time is already in UTC.

				AddParameter(command, "@time", SqlDbType.BigInt, message.Time.Ticks);
				AddParameter(command, "@sequence", SqlDbType.Int, message.Sequence);
				AddParameter(command, "@source", SqlDbType.VarChar, message.Source);
				AddParameter(command, "@event", SqlDbType.VarChar, message.Event);
                AddParameter(command, "@type", SqlDbType.VarChar, message.Type);
				AddParameter(command, "@method", SqlDbType.VarChar, message.Method);
				AddParameter(command, "@message", SqlDbType.NText, message.Message);
				AddParameter(command, "@exception", SqlDbType.NText, exceptionXml);
                AddParameter(command, "@details", SqlDbType.NText, detailsXml);

				var idParam = new SqlParameter
                {
                    ParameterName = "@messageId",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
			    command.Parameters.Add(idParam);

				try
				{
					command.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					// Ignore errors about inserting duplicate rows - see defect 57691.

					if (ex.Number != Sql.CannotInsertDuplicateRowError)
					{
						throw new System.ApplicationException(string.Format("Failed to insert a"
							+ " message. Time: {0} UTC, source: '{1}', message: '{2}'.",
							message.Time, message.Source, message.Message), ex);
					}
				}

				if (idParam.Value == null)
				{
					throw new System.ApplicationException("The '" + Sql.InsertMessage
						+ "' stored procedure returned NULL for the '@messageId' output parameter.");
				}

				messageId = (int)idParam.Value;
			}

			InsertParameters(connection, messageId, message.Parameters);
		}

		private static void InsertParameters(SqlConnection connection, int messageId, EventParameters parameters)
		{
			int sequence = 0;

			foreach (EventParameter param in parameters)
			{
				string stringValue;
				byte[] binaryValue;
				EventParameter.ValueFormat format = param.GetStringAndBinaryValues(out stringValue, out binaryValue);

				using (var command = new SqlCommand(Sql.InsertParameter, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					AddParameter(command, "@messageId", SqlDbType.Int, messageId);
					AddParameter(command, "@sequence", SqlDbType.Int, sequence++);
						AddParameter(command, "@name", SqlDbType.VarChar, param.Name);
						AddParameter(command, "@format", SqlDbType.Int, format);
					AddParameter(command, "@type", SqlDbType.VarChar, param.Type);
					AddParameter(command, "@string", SqlDbType.NText, stringValue);
					AddParameter(command, "@binary", SqlDbType.Image, binaryValue);

					try
					{
						command.ExecuteNonQuery();
					}
					catch (SqlException ex)
					{
						// Ignore errors about inserting duplicate rows - see defect 57691.

						if (ex.Number != Sql.CannotInsertDuplicateRowError)
						{
							throw new System.ApplicationException(string.Format("Failed to insert a"
								+ " parameter for message {0}. Name: {1}, type: '{2}', format: '{3}'.",
								messageId, param.Name, param.Type, format), ex);
						}
					}
				}
			}
		}
	}
}

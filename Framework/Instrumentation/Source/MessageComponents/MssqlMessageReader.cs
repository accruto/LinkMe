using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using LinkMe.Framework.Instrumentation.Constants;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Xml;
using InvalidOperationException=System.InvalidOperationException;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class MssqlMessageReader
		:	BaseMessageReader,
            IMessageNotifyReader,
			IMessageCount
	{
        public MssqlMessageReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Nested types

		private class SqlEventMessage : EventMessage
		{
			private readonly int _id;
			private readonly MssqlMessageReader _messageReader;
			private bool _haveParameters;
            private bool _haveDetails;
            private System.Exception _detailsException;
            private readonly System.Exception _exceptionException;

			internal SqlEventMessage(MssqlMessageReader messageReader, int id, string eventName, string source,
                string type, string method, string message, System.DateTime time, string exceptionXml)
				:	base(eventName, source, type, method, message, time, null)
			{
				_id = id;
				_messageReader = messageReader;

				// Deserialize the exception immediately. This slows down the message reader a bit, but
				// storing them as XML is a huge memory overhead. If an exception occurs don't completely
				// fail, just store the exception to be re-thrown when the user accesses the Details property.

				try
				{
                    ExceptionInfo exception = (string.IsNullOrEmpty(exceptionXml) ? null :
                        ExceptionInfo.FromXmlString(exceptionXml));
                    SetException(exception);
                }
                catch (System.Exception ex)
                {
                    _exceptionException = ex;
                }
			}

            public override ExceptionInfo Exception
            {
                get
                {
                    // Throw a new exception to preserve the stack trace.

                    if (_exceptionException != null)
                        throw new InvalidOperationException("Failed to read exception.", _exceptionException);

                    return base.Exception;
                }
            }

            public override Utility.Event.EventDetails Details
			{
				get
				{
                    // Throw a new exception to preserve the stack trace.

                    if (_detailsException != null)
                        throw new InvalidOperationException("Failed to read message details.", _detailsException);

                    if (!_haveDetails)
                    {
                        try
                        {
                            // Read the details from the database.

                            Debug.Assert(_messageReader != null, "_messageReader != null");
                            SetDetails(_messageReader.ReadDetails(_id));
                            _haveDetails = true;
                        }
                        catch (System.Exception ex)
                        {
                            _detailsException = ex;
                        }
                    }

                    return base.Details;
				}
			}

			public override EventParameters Parameters
			{
				get
				{
					if (!_haveParameters)
					{
						// Read the parameters from the database.

						Debug.Assert(_messageReader != null, "_messageReader != null");
						SetParameters(_messageReader.ReadParameters(_id));
						_haveParameters = true;
					}

					return base.Parameters;
				}
			}

			internal int Id
			{
				get { return _id; }
			}

		    internal long Ticks
		    {
                get { return Time.Ticks; }
		    }
		}

        private class SqlEventMessageIdentifier
            : EventMessageIdentifier
        {
			private readonly int _id;
            private readonly long _time;

            public SqlEventMessageIdentifier(int id, long time)
            {
                _id = id;
                _time = time;
            }

            public int Id
            {
                get { return _id; }
            }

            public long Time
            {
                get { return _time; }
            }

            public override bool Equals(object obj)
            {
                if (!(obj is SqlEventMessageIdentifier))
                    return false;
                return _id == ((SqlEventMessageIdentifier)obj)._id;
            }

            public override int GetHashCode()
            {
                return _id.GetHashCode();
            }

            public override string ToString()
            {
                return "Id: " + Id + ", Time: " + Time;
            }
        }

		#endregion

		private readonly string _connectionString;
		private bool _raiseExistingRead = true;
		private long _lastReadMessageTime;
		private int _lastReadMessageSequence;
		// Strings in EventMessages read from the database are interned using this interner - this slows down
		// the reader, but saves memory.
		private readonly Interner _interner = new Interner();

	    #region IMessageCount Members

		public int GetMessageCount()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

                using (var command = new SqlCommand(Sql.GetMessageCount, connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					return (int)command.ExecuteScalar();
				}
			}
		}

		#endregion

        #region IMessageNotifyReader Members

        public EventMessage GetMessage(EventMessageIdentifier identifier)
        {
            var sqlIdentifier = identifier as SqlEventMessageIdentifier;
            if (sqlIdentifier == null)
                return null;

            EventMessage message = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(Sql.GetMessage, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@messageId", SqlDbType.Int).Value = sqlIdentifier.Id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            message = ReadMessage(reader);
                        }
                    }
                }
            }

            return message;
        }

        public EventMessages GetMessages(EventMessageIdentifier startIdentifier, EventMessageIdentifier endIdentifier)
        {
            var messages = new EventMessages();

            var sqlStartIdentifier = startIdentifier as SqlEventMessageIdentifier;
            var sqlEndIdentifier = endIdentifier as SqlEventMessageIdentifier;

            if (sqlStartIdentifier == null || sqlEndIdentifier == null)
                return messages;

            var eventFilter = GetEventFilter();
            if (eventFilter == null)
                GetMessages(messages, sqlStartIdentifier, sqlEndIdentifier);
            else
                GetMessages(messages, sqlStartIdentifier, sqlEndIdentifier, eventFilter);

            return messages;
        }

        private void GetMessages(EventMessages messages, SqlEventMessageIdentifier sqlStartIdentifier, SqlEventMessageIdentifier sqlEndIdentifier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(Sql.GetMessageTimeRange, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@fromTime", SqlDbType.BigInt).Value = sqlStartIdentifier.Time;
                    command.Parameters.Add("@toTime", SqlDbType.BigInt).Value = sqlEndIdentifier.Time;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            messages.Add(ReadMessage(reader));
                        }
                    }
                }
            }
        }

        private void GetMessages(EventMessages messages, SqlEventMessageIdentifier sqlStartIdentifier, SqlEventMessageIdentifier sqlEndIdentifier, Filter eventFilter)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(Sql.GetMessageTimeRangeFilter, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@fromTime", SqlDbType.BigInt).Value = sqlStartIdentifier.Time;
                    command.Parameters.Add("@toTime", SqlDbType.BigInt).Value = sqlEndIdentifier.Time;
                    command.Parameters.Add("@eventFilter", SqlDbType.VarChar).Value = SqlUtil.RegexToSqlLike(eventFilter.Pattern.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            messages.Add(ReadMessage(reader));
                        }
                    }
                }
            }
        }

        public EventMessageIdentifier GetIdentifier(EventMessage message)
        {
            var sqlEventMessage = (SqlEventMessage) message;
            return new SqlEventMessageIdentifier(sqlEventMessage.Id, sqlEventMessage.Ticks);
        }

        #endregion

		protected override void ReadAll()
		{
			var messages = new EventMessages();

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

                using (var command = new SqlCommand(Sql.GetAllMessages, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var time = new System.DateTime(reader.GetInt64(1));
							string exceptionXml = (reader.IsDBNull(8) ? null : reader.GetString(8));

							var message = new SqlEventMessage(null, reader.GetInt32(0),
								reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetString(6),
								reader.GetString(7), time, exceptionXml);
							message.Intern(_interner);
							SetMessageSequence(message, reader.GetInt32(2));

							messages.Add(message);
						}
					}
				}

				// Read the parameters immediately.

				foreach (SqlEventMessage message in messages)
				{
					message.SetParameters(ReadParameters(connection, message.Id));
				}
			}

			if (messages.Count > 0)
			{
				HandleEventMessages(messages);
			}
		}

		protected override void Reset()
		{
			_lastReadMessageTime = 0;
			_lastReadMessageSequence = 0;
			_raiseExistingRead = true;
		}

		protected override void Read()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				bool moreToRead = ReadMessageBatch(connection);

				// Continue reading messages until Stop() is called.

				var readHandles = new[] { StopEvent,  PauseEvent };
				var pauseHandles = new[] { StopEvent, ContinueEvent };

				bool stopping = false;
				while ( !stopping )
				{
					int timeToWait = (moreToRead ? 0 : Constants.MessageComponents.Mssql.TimeToWaitForNewMessages);
					switch ( WaitHandle.WaitAny(readHandles, timeToWait, false) )
					{
						case 0:

							// Stopping, so exit the loop.

							stopping = true;
							break;

						case 1:

							// Paused, wait for it to start again.

							switch ( WaitHandle.WaitAny(pauseHandles) )
							{
								case 0:

									// Stopping, so exit the loop.

									stopping = true;
									break;

								case 1:

									// Continuing, read whatever messages may have been added.

									moreToRead = ReadMessageBatch(connection);
									break;
							}

							break;

						case WaitHandle.WaitTimeout:

							// Read the messages.

							moreToRead = ReadMessageBatch(connection);
							break;
					}
				}
			}
		}

		protected override void ResumeReading()
		{
			_raiseExistingRead = true;
			base.ResumeReading();
		}

		protected override void StopReading()
		{
			base.StopReading();

			// Release references to all the strings interned so far.

			_interner.Clear();
		}

		private bool ReadMessageBatch(SqlConnection connection)
		{
            if (SupportsNotify)
                return ReadNotifyMessages(connection);

			var messages = new EventMessages();

            using (var command = new SqlCommand(Sql.GetMessageRange, connection))
			{
				command.CommandType = CommandType.StoredProcedure;

				// The stored procedure takes a time (in ticks) as parameter instead of message ID, because
				// messages in the database are not necessarily ordered by ID.

				command.Parameters.Add("@fromTime", SqlDbType.BigInt).Value = _lastReadMessageTime;
				command.Parameters.Add("@afterSequence", SqlDbType.Int).Value = _lastReadMessageSequence;

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						_lastReadMessageTime = reader.GetInt64(1);
						_lastReadMessageSequence = reader.GetInt32(2);

						var time = new System.DateTime(_lastReadMessageTime);
						string exceptionXml = (reader.IsDBNull(8) ? null : reader.GetString(8));

						var message = new SqlEventMessage(this, reader.GetInt32(0),
							reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetString(6),
                            reader.GetString(7), time, exceptionXml);
						message.Intern(_interner);
						SetMessageSequence(message, _lastReadMessageSequence);

						messages.Add(message);
					}
				}
			}

			// Note that the messages don't have parameters or details at this stage - those will be read as needed.

			if (messages.Count > 0)
			{
				_raiseExistingRead = true; // We have more messages.
				HandleEventMessages(messages);
				return true;
			}
		    
            if (_raiseExistingRead)
		    {
		        _raiseExistingRead = false; // Don't raise again until we've read some messages.
		        OnExistingMessagesRead(System.EventArgs.Empty);
		    }

		    return false;
		}

        private bool ReadNotifyMessages(SqlConnection connection)
        {
            var identifiers = new EventMessageIdentifiers();
            var eventFilter = GetEventFilter();

            if (eventFilter == null)
                GetMessageIdentifiers(connection, identifiers);
            else
                GetMessageIdentifiers(connection, identifiers, eventFilter);

            // Unlike reading entire messages this method always reads all the identifiers, so we don't
            // need to wait until the next read to tell the client that all messages have been read.

            if (identifiers.Count > 0)
            {
                HandleEventMessages(identifiers);
                _raiseExistingRead = true;
            }

            if (_raiseExistingRead)
            {
                _raiseExistingRead = false;
                OnExistingMessagesRead(System.EventArgs.Empty);
            }

            return false;
        }

	    private Filter GetEventFilter()
	    {
            if (Filters != null)
            {
                foreach (var filter in Filters)
                {
                    if (filter.Name == "Event" && filter.Pattern != null)
                        return filter;
                }
            }

            return null;
	    }

	    private void GetMessageIdentifiers(SqlConnection connection, EventMessageIdentifiers identifiers)
	    {
            using (var command = new SqlCommand(Sql.GetMessageIdentifierRange, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // The stored procedure takes a time (in ticks) as parameter instead of message ID, because
                // messages in the database are not necessarily ordered by ID.

                command.Parameters.Add("@fromTime", SqlDbType.BigInt).Value = _lastReadMessageTime;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _lastReadMessageTime = reader.GetInt64(1);
                        int id = reader.GetInt32(0);

                        EventMessageIdentifier identifier = new SqlEventMessageIdentifier(id, _lastReadMessageTime);
                        identifiers.Add(identifier);
                    }
                }
            }
	    }

        private void GetMessageIdentifiers(SqlConnection connection, EventMessageIdentifiers identifiers, Filter eventFilter)
        {
            using (var command = new SqlCommand(Sql.GetMessageIdentifierRangeFilter, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // The stored procedure takes a time (in ticks) as parameter instead of message ID, because
                // messages in the database are not necessarily ordered by ID.

                command.Parameters.Add("@fromTime", SqlDbType.BigInt).Value = _lastReadMessageTime;
                command.Parameters.Add("@eventFilter", SqlDbType.VarChar).Value = SqlUtil.RegexToSqlLike(eventFilter.Pattern.Value);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _lastReadMessageTime = reader.GetInt64(1);
                        int id = reader.GetInt32(0);

                        EventMessageIdentifier identifier = new SqlEventMessageIdentifier(id, _lastReadMessageTime);
                        identifiers.Add(identifier);
                    }
                }
            }
        }

        private EventParameter[] ReadParameters(int messageId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return ReadParameters(connection, messageId);
			}
		}

        private static EventParameter[] ReadParameters(SqlConnection connection, int messageId)
		{
			var parameters = new ArrayList();

            using (var command = new SqlCommand(Sql.GetParameters, connection))
			{
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.Add("@messageId", SqlDbType.Int).Value = messageId;

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string className = (reader.IsDBNull(2) ? null : reader.GetString(2));
						string stringValue = (reader.IsDBNull(3) ? null : reader.GetString(3));
						byte[] binaryValue = (reader.IsDBNull(4) ? null : (byte[])reader.GetValue(4));

						EventParameter param = EventParameter.CreateFromStringAndBinaryValues(
							reader.GetString(0), className, (EventParameter.ValueFormat)reader.GetInt32(1),
							stringValue, binaryValue);
						parameters.Add(param);
					}
				}
			}

			return (parameters.Count == 0 ? null : (EventParameter[])parameters.ToArray(typeof(EventParameter)));
		}

        private Utility.Event.EventDetails ReadDetails(int messageId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return ReadDetails(connection, messageId);
            }
        }

        private static Utility.Event.EventDetails ReadDetails(SqlConnection connection, int messageId)
        {
            var details = new Utility.Event.EventDetails();

            using (var command = new SqlCommand(Sql.GetDetails, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@messageId", SqlDbType.Int).Value = messageId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string detailsXml = reader.IsDBNull(0) ? null : reader.GetString(0);
                        XmlSerializer.Deserialize(details, detailsXml);
                    }
                }
            }

            return details;
        }

        private SqlEventMessage ReadMessage(IDataRecord reader)
        {
            var time = new System.DateTime(reader.GetInt64(1));
            string exceptionXml = (reader.IsDBNull(8) ? null : reader.GetString(8));

            var message = new SqlEventMessage(this, reader.GetInt32(0),
                                          reader.GetString(4), reader.GetString(3),
                                          reader.GetString(5), reader.GetString(6),
                                          reader.GetString(7), time, exceptionXml);
            message.Intern(_interner);
            SetMessageSequence(message, reader.GetInt32(2));

            return message;
        }
    }
}

namespace LinkMe.Framework.Instrumentation.Constants
{
	internal static class Errors
	{
		// The maximum number of errors of a particular kind (eg. errors in creating an event source, errors in
		// handling a message) to log to the internal message handler.
		internal const int MaximumErrorsToLog = 10;
	}

	internal static class Events
	{
		internal const string Unknown = "Unknown";
		internal const string CriticalError = "CriticalError";
		internal const string Error = "Error";
		internal const string Warning = "Warning";
		internal const string NonCriticalError = "NonCriticalError";
		internal const string Information = "Information";
		internal const string Trace = "Trace";
		internal const string MethodEnter = "MethodEnter";
		internal const string MethodExit = "MethodExit";
		internal const string Flow = "Flow";
		internal const string FlowEnter = "FlowEnter";
		internal const string FlowExit = "FlowExit";
        internal const string CommunicationTracking = "CommunicationTracking";
        internal const string RequestTracking = "RequestTracking";
    }

    internal static class Filters
    {
        internal const string Source = "Source";
        internal const string Event = "Event";
        internal const string Type = "Type";
        internal const string Method = "Method";
        internal const string Message = "Message";
    }

	internal static class EventDetails
	{
		internal const string ContextDetail = "ContextDetail";
		internal const string DiagnosticDetail = "DiagnosticDetail";
		internal const string EnvironmentDetail = "EnvironmentDetail";
		internal const string NetDetail = "NetDetail";
		internal const string ProcessDetail = "ProcessDetail";
		internal const string SecurityDetail = "SecurityDetail";
		internal const string ThreadDetail = "ThreadDetail";
        internal const string HttpDetail = "HttpDetail";
    }

	internal static class Xml
	{
		internal const string Namespace = LinkMe.Framework.Instrumentation.Management.Constants.Xml.Namespace;

		internal const string UtilityNamespace = LinkMe.Framework.Utility.Constants.Xml.Namespace;
		internal const string UtilityPrefix = LinkMe.Framework.Utility.Constants.Xml.Prefix;
		internal const string TypeNamespace = LinkMe.Framework.Type.Constants.Xml.Namespace;
		internal const string TypePrefix = LinkMe.Framework.Type.Constants.Xml.Prefix;

		internal const string ConfigurationElement			= "configuration";
		internal const string EventMessagesElement			= "EventMessages";
		internal const string EventMessageElement			= "EventMessage";
		internal const string SourceElement					= "Source";
		internal const string EventElement					= "Event";
		internal const string TypeElement					= "Type";
		internal const string MethodElement					= "Method";
		internal const string MessageElement				= "Message";
		internal const string TimeElement					= "Time";
		internal const string SequenceElement				= "Sequence";
        internal const string ExceptionElement              = "Exception";
		internal const string DetailsElement				= "Details";
		internal const string ParametersElement				= "Parameters";
		internal const string ParameterElement				= "Parameter";

		internal const string ClassAttribute				= "class";
		internal const string FormatAttribute				= "format";
		internal const string LinkMeFormat					= "linkme";
		internal const string SystemFormat					= "system";
		internal const string StringFormat					= "string";

		internal static class DiagnosticDetail
		{
			internal const string Name					= "DiagnosticDetail";
			internal const string StackTraceElement		= "StackTrace";
		}

        internal static class HttpDetail
        {
            internal const string Name = "HttpDetail";
            internal const string HttpMethodElement = "HttpMethod";
            internal const string UrlElement = "Url";
            internal const string RawUrlElement = "RawUrl";
            internal const string UrlReferrerElement = "UrlReferrer";
            internal const string IsAuthenticatedElement = "IsAuthenticated";
            internal const string IsLocalElement = "IsLocal";
            internal const string IsSecureConnectionElement = "IsSecureConnection";

            internal static class Headers
            {
                internal const string Name = "Headers";
                internal const string HeaderElement = "Header";
                internal const string NameAttribute = "name";
                internal const string ValueElement = "Value";
            }

            internal static class Cookies
            {
                internal const string Name = "Cookies";
                internal const string CookieElement = "Cookie";
                internal const string NameAttribute = "name";
                internal const string ValueElement = "Value";
            }

            internal static class Form
            {
                internal const string Name = "Form";
                internal const string VariableElement = "Variable";
                internal const string NameAttribute = "name";
                internal const string ValueElement = "Value";
            }

            internal static class Request
            {
                internal const string Name = "Request";

                internal static class Content
                {
                    internal const string Name = "Content";
                    internal const string EncodingNameElement = "EncodingName";
                    internal const string LengthElement = "Length";
                    internal const string TypeElement = "Type";
                }

                internal static class User
                {
                    internal const string Name = "User";
                    internal const string HostNameElement = "HostName";
                    internal const string HostAddressElement = "HostAddress";
                    internal const string AgentElement = "Agent";
                }
            }

            internal static class Session
            {
                internal const string Name = "Session";
                internal const string IdElement = "Id";
            }
        }

		internal static class FilterConfigurationHandler
		{
			internal const string PatternsElement = "patterns";
			internal const string PatternElement = "pattern";
			internal const string TypeAttribute = "type";
		}

		internal static class MsmqConfigurationHandler
		{
			internal const string Queue = "queue";
		}

		internal static class MssqlConfigurationHandler
		{
			internal const string ConnectionString = "connectionString";
			internal const string UseArchives = "useArchives";
		}

		internal static class EventFileConfigurationHandler
		{
			internal const string FileName = "file";
		}
	}

	internal static class Xsi
	{
		internal const string Prefix = LinkMe.Framework.Utility.Constants.Xsi.Prefix;
		internal const string Namespace = LinkMe.Framework.Utility.Constants.Xsi.Namespace;
	}

	internal static class Serialization
	{
		internal const string Event							= "event";
		internal const string Source						= "source";
		internal const string Method						= "method";
		internal const string Message						= "message";
		internal const string Time							= "time";
		internal const string EventDetails					= "eventDetails";
		internal const string EventDetailCount				= "eventDetailCount";
		internal const string Parameter						= "parameter";
		internal const string ParameterCount				= "parameterCount";
		internal const string EventMessage					= "message";
		internal const string EventMessageCount				= "messageCount";
	}

	public static class SystemLog
	{
		public const string LogName						= "Application";
		public const string EventSource					= "LinkMe Instrumentation";
	}

	internal static class MessageComponents
	{
		internal static class Msmq
		{
			internal const string MessageLabel = "LinkMe Instrumentation EventMessage";
			internal const string MessagesLabel = "LinkMe Instrumentation EventMessages";
			internal const string FormatNamePrefix = "direct=os:";
			internal const int MaxEventMessagesPerMqMessage = 100;
			// The maximum number of messages a message reader should try to send to its message handler at a time.
			// Note that the message reader may still send more than this number of messages in some cases.
			internal const int MessageReaderBatchSize = 100;
			internal const int TimeToWaitForNewMessages = 1000; // Wait 1 second for new messages to arrive.
		}

		internal class EventFile
		{
			private EventFile()
			{
			}

			internal const string FileFilter = "Event files (*.event)|*.event|All files (*.*)|*.*";
			internal const string FileExtension = "event";
			// The maximum number of messages a message reader should try to send to its message handler at a time.
			internal const int MessageReaderBatchSize = 100;
		}

		internal class Mssql
		{
			private Mssql()
			{
			}

			// The maximum number of messages a message reader should try to send to its message handler at a time.
			// Note that the message reader may still send more than this number of messages in some cases.
			internal const int MessageReaderBatchSize = 100;
			internal const int TimeToWaitForNewMessages = 2000; // Wait 2 seconds for new messages to arrive.
			internal const string DetailsErrorPrefix = "!ERROR: ";
		}
    }

	internal static class Sql
	{
		internal const string TablesScriptFile				= "LoggingTables.sql";
		internal const string StoredProceduresScriptFile	= "LoggingStoredProcedures.sql";
		internal const string SecurityScriptFile			= "LoggingSecurity.sql";

		internal const string InsertMessage					    = "dbo.FiStoreInsertMessage";
		internal const string InsertParameter				    = "dbo.FiStoreInsertParameter";
		internal const string GetAllMessages				    = "dbo.FiStoreGetAllMessages";
        internal const string GetMessage                        = "dbo.FiStoreGetMessage";
        internal const string GetMessageCount                   = "dbo.FiStoreGetMessageCount";
		internal const string GetMessageRange				    = "dbo.FiStoreGetMessageRange";
        internal const string GetMessageIdentifierRange         = "dbo.FiStoreGetMessageIdentifierRange";
        internal const string GetMessageIdentifierRangeFilter   = "dbo.FiStoreGetMessageIdentifierRangeFilter";
        internal const string GetMessageTimeRange               = "dbo.FiStoreGetMessageTimeRange";
        internal const string GetMessageTimeRangeFilter         = "dbo.FiStoreGetMessageTimeRangeFilter";
        internal const string GetParameters                     = "dbo.FiStoreGetParameters";
        internal const string GetDetails                        = "dbo.FiStoreGetDetails";

		internal const int CannotInsertDuplicateRowError	= 2601;
	}

	internal static class OleDb
	{
		internal const string MsSqlProviderProgID = LinkMe.Framework.Tools.Constants.OleDb.MsSqlProviderProgID;
		internal const string MsSqlProviderDisplayName = LinkMe.Framework.Tools.Constants.OleDb.MsSqlProviderDisplayName;
	}

	internal static class Setting
	{
		internal const string ConnectionString = "ConnectionString";
		internal const string Queue = "Queue";
	}
}

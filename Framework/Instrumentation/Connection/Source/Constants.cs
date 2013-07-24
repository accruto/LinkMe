namespace LinkMe.Framework.Instrumentation.Connection.Constants
{
	internal static class Module
	{
		internal const string Name = LinkMe.Framework.Instrumentation.Management.Constants.Module.Name;
		internal const string DisplayName = LinkMe.Framework.Instrumentation.Management.Constants.Module.DisplayName;
        internal const string RelativeDevFolder = "Framework\\Instrumentation";
        internal const string RelativeDevDataFolder = "Framework\\Instrumentation\\Data";
        internal const char ListSeparator = ',';
    }

    internal static class Environment
    {
        internal const string EnvironmentFileName = "LinkMe.Framework.Environment.Instrumentation.Subsystems.config";
        internal const string RelativeDevFolder = "Framework\\Environment";
    }

	internal static class Config
	{
		internal const string RootElement = LinkMe.Framework.Configuration.Constants.Config.RootElement;
		internal const string SectionElement = LinkMe.Framework.Configuration.Constants.Config.SectionElement;
		internal const string ReferencesElement = LinkMe.Framework.Configuration.Constants.Config.ReferencesElement;
		internal const string ReferenceElement = LinkMe.Framework.Configuration.Constants.Config.ReferenceElement;
		internal const string FileElement = LinkMe.Framework.Configuration.Constants.Config.FileElement;
		internal const string RepositoryElement = LinkMe.Framework.Configuration.Constants.Config.RepositoryElement;
		internal const string RepositoryNameAttribute = LinkMe.Framework.Configuration.Constants.Config.RepositoryNameAttribute;
		internal const string RepositoryTypeAttribute = LinkMe.Framework.Configuration.Constants.Config.RepositoryTypeAttribute;
        internal const string IsLocalAttribute = LinkMe.Framework.Configuration.Constants.Config.IsLocalAttribute;
        internal const string InitialisationStringElement = LinkMe.Framework.Configuration.Constants.Config.InitialisationStringElement;
	}

    internal static class Sql
    {
        internal const int DefaultGetTimeout = 30;
        internal const int DeleteTimeout = 180; // A single deletes command can do a lot, so allow plenty of time.

        internal const string TablesCreateFile = "LinkMe.Framework.Instrumentation.Data.Tables.Create.sql";
        internal const string TablesDropFile = "LinkMe.Framework.Instrumentation.Data.Tables.Drop.sql";
        internal const string FunctionsCreateFile = "LinkMe.Framework.Instrumentation.Data.Functions.Create.sql";
        internal const string FunctionsDropFile = "LinkMe.Framework.Instrumentation.Data.Functions.Drop.sql";
        internal const string StoredProceduresCreateFile = "LinkMe.Framework.Instrumentation.Data.StoredProcedures.Create.sql";
        internal const string StoredProceduresDropFile = "LinkMe.Framework.Instrumentation.Data.StoredProcedures.Drop.sql";
        internal const string SecurityCreateFile = "LinkMe.Framework.Instrumentation.Data.Security.Create.sql";
        internal const string SecurityDropFile = "LinkMe.Framework.Instrumentation.Data.Security.Drop.sql";

        internal const string StoreTablesCreateFile = "LinkMe.Framework.Instrumentation.Data.Store.Tables.Create.sql";
        internal const string StoreTablesDropFile = "LinkMe.Framework.Instrumentation.Data.Store.Tables.Drop.sql";
        internal const string StoreFunctionsCreateFile = "LinkMe.Framework.Instrumentation.Data.Store.Functions.Create.sql";
        internal const string StoreFunctionsDropFile = "LinkMe.Framework.Instrumentation.Data.Store.Functions.Drop.sql";
        internal const string StoreStoredProceduresCreateFile = "LinkMe.Framework.Instrumentation.Data.Store.StoredProcedures.Create.sql";
        internal const string StoreStoredProceduresDropFile = "LinkMe.Framework.Instrumentation.Data.Store.StoredProcedures.Drop.sql";
        internal const string StoreSecurityCreateFile = "LinkMe.Framework.Instrumentation.Data.Store.Security.Create.sql";
        internal const string StoreSecurityDropFile = "LinkMe.Framework.Instrumentation.Data.Store.Security.Drop.sql";

        internal static class Tables
        {
            internal const string Source = "FiSource";
            internal const string Message = "FiStoreMessage";
        }

        internal static class StoredProcedures
        {
            // EventDetail

            internal const string GetEventDetails = "FiEventDetailsGet";
            internal const string DeleteEventDetail = "FiEventDetailDelete";
            internal const string UpdateEventDetail = "FiEventDetailUpdate";

            // EventType

            internal const string GetEventTypes = "FiEventTypesGet";
            internal const string DeleteEventType = "FiEventTypeDelete";
            internal const string UpdateEventType = "FiEventTypeUpdate";

            // Namespace

            internal const string GetNamespaces = "FiNamespacesGet";
            internal const string DeleteNamespace = "FiNamespaceDelete";
            internal const string UpdateNamespace = "FiNamespaceUpdate";

            // SourceTypes

            internal const string GetSourceTypes = "FiSourceTypesGet";
            internal const string DeleteSourceType = "FiSourceTypeDelete";
            internal const string UpdateSourceType = "FiSourceTypeUpdate";

            // Sources

            internal const string GetSources = "FiSourcesGet";
            internal const string DeleteSource = "FiSourceDelete";
            internal const string UpdateSource = "FiSourceUpdate";

            // MessageHandlerTypes

            internal const string GetMessageHandlerTypes = "FiMessageHandlerTypesGet";
            internal const string DeleteMessageHandlerType = "FiMessageHandlerTypeDelete";
            internal const string UpdateMessageHandlerType = "FiMessageHandlerTypeUpdate";

            // MessageHandlers

            internal const string GetMessageHandlers = "FiMessageHandlersGet";
            internal const string DeleteMessageHandler = "FiMessageHandlerDelete";
            internal const string UpdateMessageHandler = "FiMessageHandlerUpdate";

            // MessageReaderTypes

            internal const string GetMessageReaderTypes = "FiMessageReaderTypesGet";
            internal const string DeleteMessageReaderType = "FiMessageReaderTypeDelete";
            internal const string UpdateMessageReaderType = "FiMessageReaderTypeUpdate";

            // MessageReaders

            internal const string GetMessageReaders = "FiMessageReadersGet";
            internal const string DeleteMessageReader = "FiMessageReaderDelete";
            internal const string UpdateMessageReader = "FiMessageReaderUpdate";

            // GetAllMessages

            internal const string StoreGetAllMessages = "FiStoreGetAllMessages";
        }
    }

	internal static class RepositoryType
	{
		internal class ConfigurationFileConnection
		{
			private ConfigurationFileConnection()
			{
			}

            internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.Name;
			internal const string FileFilter = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.FileFilter;
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.FileExtension;
		}

		internal class NetAssemblyReader
		{
			private NetAssemblyReader()
			{
			}

			internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.Name;
			internal const string FileFilter = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileFilter;
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileExtension;
			internal const string StateKeyAppDomain = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.StateKeyAppDomain;
		}

        internal class SqlConnection
        {
            private SqlConnection()
            {
            }

            internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.SqlConnection.Name;
            internal const string FileFilter = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileFilter;
            internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileExtension;
            internal const string StateKeyAppDomain = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.StateKeyAppDomain;
        }
    }

	internal static class StoreType
	{
		internal static class SqlStoreConnection
		{
			internal const string Name = LinkMe.Framework.Configuration.Constants.StoreType.SqlStoreConnection.Name;
			internal const string DisplayName = LinkMe.Framework.Configuration.Constants.StoreType.SqlStoreConnection.DisplayName;
            internal const string StoreDisplayName = LinkMe.Framework.Configuration.Constants.StoreType.SqlStoreConnection.StoreDisplayName;
            internal const string Description = LinkMe.Framework.Configuration.Constants.StoreType.SqlStoreConnection.Description;
		}

        internal static class MsmqStoreConnection
        {
            internal const string Name = LinkMe.Framework.Configuration.Constants.StoreType.MsmqStoreConnection.Name;
            internal const string DisplayName = LinkMe.Framework.Configuration.Constants.StoreType.MsmqStoreConnection.DisplayName;
            internal const string StoreDisplayName = LinkMe.Framework.Configuration.Constants.StoreType.MsmqStoreConnection.StoreDisplayName;
            internal const string Description = LinkMe.Framework.Configuration.Constants.StoreType.MsmqStoreConnection.Description;
        }
	}

	internal static class Wmi
	{
        internal const string MofFileName = "LinkMe.Framework.Instrumentation.mof";

		internal const string ParentProperty = "Parent";
		internal const string NameProperty = "Name";
        internal const string DisplayNameProperty = "DisplayName";

		internal static class Element
		{
			internal const string Class = "LinkMe_InstrumentationElement";
		}
		
		internal static class Namespace
		{
			internal const string Class = "LinkMe_InstrumentationNamespace";
			internal const string EnabledEventsProperty = "EnabledEvents";
			internal const string MixedEventsProperty = "MixedEvents";
		}

		internal static class Source
		{
			internal const string Class = "LinkMe_InstrumentationSource";
			internal const string EnabledEventsProperty = "EnabledEvents";
		}

		internal static class EventType
		{
			internal const string Class = "LinkMe_InstrumentationEventType";
			internal const string EventDetailsProperty = "EventDetails";
			internal const string IsEnabledProperty = "IsEnabled";
		}

		internal static class EventStatusChange
		{
			internal const string Class = "LinkMe_InstrumentationEventStatusChange";
			internal const string FullNameProperty = "FullName";
			internal const string ElementTypeProperty = "ElementType";
		}
	}
}

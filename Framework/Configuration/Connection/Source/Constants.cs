namespace LinkMe.Framework.Configuration.Connection.Constants
{
	internal static class Module
	{
		internal const string DisplayName = LinkMe.Framework.Configuration.Constants.Module.DisplayName;
        internal const string RelativeDevFolder = "Framework\\Configuration";
        internal const string RelativeDevDataFolder = "Framework\\Configuration\\Data";
    }

    internal static class Environment
    {
        internal const string EnvironmentFileName = "LinkMe.Framework.Environment.config";
        internal const string RelativeDevFolder = "Framework\\Environment";
        internal const string NewDomainFileName = "LinkMe.Framework.Environment.NewDomain.config";
        internal const string NewDomainName = "NewDomain";
    }

    internal static class LinkType
    {
        internal static class WmiLinkType
        {
            internal const string Name = LinkMe.Framework.Configuration.Constants.LinkType.WmiLinkType.Name;
            internal const string DisplayName = LinkMe.Framework.Configuration.Constants.LinkType.WmiLinkType.DisplayName;
        }

        internal static class SqlLinkType
        {
            internal const string Name = LinkMe.Framework.Configuration.Constants.LinkType.SqlLinkType.Name;
            internal const string DisplayName = LinkMe.Framework.Configuration.Constants.LinkType.SqlLinkType.DisplayName;
        }
    }

    internal static class RepositoryType
	{
        internal static class WmiConnection
        {
            internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.WmiConnection.Name;
        }

        internal static class ConfigurationFileConnection
		{
			internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.Name;
            internal const string DisplayName = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.DisplayName;
            internal const string FileFilter = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.FileFilter;
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.FileExtension;
		}

		internal static class NetAssemblyReader
		{
			internal const string Name = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.Name;
            internal const string DisplayName = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.DisplayName;
            internal const string FileFilter = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileFilter;
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.FileExtension;
			internal const string StateKeyAppDomain = LinkMe.Framework.Configuration.Constants.RepositoryType.NetAssemblyReader.StateKeyAppDomain;
		}
	}

	internal static class StoreType
	{
/*
		internal static class ObjectFileConnection
		{
			private ObjectFileConnection()
			{
			}

			internal const string Name = LinkMe.Framework.Configuration.Constants.StoreType.ObjectFileConnection.Name;
			internal const string FileFilter = LinkMe.Framework.Configuration.Constants.StoreType.ObjectFileConnection.FileFilter;
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.StoreType.ObjectFileConnection.FileExtension;
		}
*/
    }

	internal static class Config
	{
		internal const string RootElement = LinkMe.Framework.Configuration.Constants.Config.RootElement;
		internal const string SectionElement = LinkMe.Framework.Configuration.Constants.Config.SectionElement;
		internal const string EnvironmentRepositoryElement = LinkMe.Framework.Configuration.Constants.Config.EnvironmentRepositoryElement;
		internal const string EnvironmentElement = LinkMe.Framework.Configuration.Constants.Config.EnvironmentElement;
		internal const string ReferencesElement = LinkMe.Framework.Configuration.Constants.Config.ReferencesElement;
		internal const string ReferenceElement = LinkMe.Framework.Configuration.Constants.Config.ReferenceElement;
		internal const string FileElement = LinkMe.Framework.Configuration.Constants.Config.FileElement;
		internal const string RepositoryElement = LinkMe.Framework.Configuration.Constants.Config.RepositoryElement;
		internal const string RepositoryNameAttribute = LinkMe.Framework.Configuration.Constants.Config.RepositoryNameAttribute;
		internal const string RepositoryTypeAttribute = LinkMe.Framework.Configuration.Constants.Config.RepositoryTypeAttribute;
		internal const string InitialisationStringElement = LinkMe.Framework.Configuration.Constants.Config.InitialisationStringElement;
        internal const string IsLocalAttribute = LinkMe.Framework.Configuration.Constants.Config.IsLocalAttribute;
    }

	internal static class Wmi
	{
	    internal const string EnvironmentNamespace = @"\\.\root\LinkMe";
        internal const string EnvironmentMofFileName = "LinkMe.Framework.Configuration.Environment.mof";

        internal const string MofFileName = "LinkMe.Framework.Configuration.mof";

		internal const string NameProperty = "Name";
		internal const string ParentElementProperty = "ParentElement";
		internal const string ParentProperty = "Parent";
		internal const string DescriptionProperty = "Description";
		internal const string DisplayNameProperty = "DisplayName";
		internal const string TypeProperty = "Type";
		internal const string ValueProperty = "Value";
		internal const string RepositoryTypeProperty = "RepositoryType";
		internal const string StoreTypeProperty = "StoreType";
		internal const string InitialisationStringProperty = "InitialisationString";
		internal const string RepositoryInitialisationStringProperty = "RepositoryInitialisationString";
        internal const string IsLocalProperty = "IsLocal";
        internal const string IsReadOnlyProperty = "IsReadOnly";
		internal const string IgnoreVersionsProperty = "IgnoreVersions";

		internal static class Element
		{
			internal const string Class = "LinkMe_ConfigurationElement";
		}

        internal static class Computer
        {
            internal const string Class = "LinkMe_ConfigurationComputer";
        }

        internal static class Domain
		{
			internal const string Class = "LinkMe_ConfigurationDomain";
		}

		internal static class Variable
		{
			internal const string Class = "LinkMe_ConfigurationVariable";
		}

		internal static class Module
		{
			internal const string Class = "LinkMe_ConfigurationModule";
			internal const string ExtensionClassProperty = "ExtensionClass";
		}

		internal static class Default
		{
			internal const string Class = "LinkMe_ConfigurationDefault";
			internal const string DomainProperty = "Domain";
		}

		internal static class RepositoryType
		{
			internal const string Class = "LinkMe_ConfigurationRepositoryType";
			internal const string RepositoryDisplayNameProperty = "RepositoryDisplayName";
			internal const string RepositoryTypeClassProperty = "RepositoryTypeClass";
			internal const string IsFileRepositoryProperty = "IsFileRepository";
		    internal const string IsVisibleProperty = "IsVisible";
		}

		internal static class Repository
		{
			internal const string Class = "LinkMe_ConfigurationRepository";
		}

		internal static class RepositoryReference
		{
			internal const string Class = "LinkMe_ConfigurationRepositoryReference";
		}

		internal static class StoreType
		{
			internal const string Class = "LinkMe_ConfigurationStoreType";
			internal const string StoreDisplayNameProperty = "StoreDisplayName";
			internal const string StoreTypeClassProperty = "StoreTypeClass";
            internal const string StoreTypeExtensionClassProperty = "StoreTypeExtensionClass";
            internal const string IsFileStoreProperty = "IsFileStore";
		}

		internal static class Store
		{
			internal const string Class = "LinkMe_ConfigurationStore";
		}

        internal static class EnvironmentDefault
        {
            internal const string Class = "LinkMe_ConfigurationEnvironmentDefault";
            internal const string RepositoryTypeProperty = "RepositoryType";
            internal const string InitialisationStringProperty = "InitialisationString";
        }
	}

	internal static class Sql
	{
        internal const string TablesCreateFile = "LinkMe.Framework.Configuration.Data.Tables.Create.sql";
        internal const string TablesDropFile = "LinkMe.Framework.Configuration.Data.Tables.Drop.sql";
        internal const string FunctionsCreateFile = "LinkMe.Framework.Configuration.Data.Functions.Create.sql";
        internal const string FunctionsDropFile = "LinkMe.Framework.Configuration.Data.Functions.Drop.sql";
        internal const string StoredProceduresCreateFile = "LinkMe.Framework.Configuration.Data.StoredProcedures.Create.sql";
        internal const string StoredProceduresDropFile = "LinkMe.Framework.Configuration.Data.StoredProcedures.Drop.sql";
        internal const string SecurityCreateFile = "LinkMe.Framework.Configuration.Data.Security.Create.sql";
        internal const string SecurityDropFile = "LinkMe.Framework.Configuration.Data.Security.Drop.sql";

		internal const int DefaultGetTimeout = 30;
		internal const int DeleteTimeout = 180; // A single deletes command can do a lot, so allow plenty of time.

        internal static class Tables
        {
            internal const string Domain = "FcDomain";
        }

		internal static class StoredProcedures
		{
            // Computer

            internal const string GetComputers                              = "FcGetComputers";
            internal const string DeleteComputer                            = "FcDeleteComputer";
            internal const string UpdateComputer                            = "FcUpdateComputer";

            // Variable

			internal const string GetDomainVariables						= "FcGetDomainVariables";
			internal const string DeleteDomainVariable						= "FcDeleteDomainVariable";
			internal const string UpdateDomainVariable						= "FcUpdateDomainVariable";
			internal const string GetModuleVariables						= "FcGetModuleVariables";
			internal const string DeleteModuleVariable						= "FcDeleteModuleVariable";
			internal const string UpdateModuleVariable						= "FcUpdateModuleVariable";
            internal const string GetVariables                              = "FcGetVariables";
            internal const string DeleteVariable                            = "FcDeleteVariable";
            internal const string UpdateVariable                            = "FcUpdateVariable";

            // Domain

			internal const string GetDomains								= "FcGetDomains";
			internal const string DeleteDomain								= "FcDeleteDomain";
			internal const string UpdateDomain								= "FcUpdateDomain";

			// Module

			internal const string GetModules								= "FcGetModules";
			internal const string DeleteModule								= "FcDeleteModule";
			internal const string UpdateModule								= "FcUpdateModule";

			// RepositoryType

			internal const string GetRepositoryTypes						= "FcGetRepositoryTypes";
			internal const string DeleteRepositoryType						= "FcDeleteRepositoryType";
			internal const string UpdateRepositoryType						= "FcUpdateRepositoryType";

			// Repository

			internal const string GetRepositories							= "FcGetRepositories";
			internal const string DeleteRepository							= "FcDeleteRepository";
			internal const string UpdateRepository							= "FcUpdateRepository";

			// StoreType

			internal const string GetStoreTypes								= "FcGetStoreTypes";
			internal const string DeleteStoreType							= "FcDeleteStoreType";
			internal const string UpdateStoreType							= "FcUpdateStoreType";

			// Store

			internal const string GetStores									= "FcGetStores";
			internal const string DeleteStore								= "FcDeleteStore";
			internal const string UpdateStore								= "FcUpdateStore";
		}
	}

    internal static class OleDb
	{
		internal const string MsSqlProviderDisplayName = LinkMe.Framework.Tools.Constants.OleDb.MsSqlProviderDisplayName;
		internal const string MsSqlProviderProgID = LinkMe.Framework.Tools.Constants.OleDb.MsSqlProviderProgID;
	}
}


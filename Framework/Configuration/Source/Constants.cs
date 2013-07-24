namespace LinkMe.Framework.Configuration.Constants
{
	public static class Module
	{
		public const string Name = "Configuration";
		public const string DisplayName = Name;
	}

    public static class RepositoryType
	{
		public static class WmiConnection
		{
			public const string Name = "WmiConnection";
			public const string DisplayName = "WMI Namespace";
			public const string RepositoryDisplayName = "WMI Namespace";
			public const string Description = "";
			public const string DefaultNamespace = @"\\.\root\LinkMe";
			public const string DefaultClass = "LinkMe_ConfigurationElement";
            public const string DefaultRepositoryClass = "LinkMe_ConfigurationEnvironmentDefault";
		    public const string DefaultRepositoryTypeProperty = "RepositoryType";
		    public const string DefaultRepositoryInitialisationStringProperty = "Initialisationstring";
        }

        public static class ConfigurationFileConnection
		{
            public const string Name = "ConfigurationFileConnection";
            public const string DisplayName = "Configuration File";
            public const string RepositoryDisplayName = "Configuration File";
            public const string Description = "";
			public const string FileFilter = "Configuration files (*.config)|*.config|Configuration source files (*.acs)|*.acs|All files (*.*)|*.*";
			public const string FileExtension = "config";
		}

		public static class NetAssemblyReader
		{
			public const string Name = "NetAssemblyReader";
			public const string DisplayName = ".NET Assembly";
			public const string RepositoryDisplayName = ".NET Assembly";
			public const string Description = "";
			public const string FileFilter = "Assemblies (*.dll;*.exe)|*.dll;*.exe|All files (*.*)|*.*";
			public const string FileExtension = "dll";
			public const string StateKeyAppDomain = "NetAssemblyReaderDomain";
		}

		public static class NetAssemblyGenerator
		{
			public const string Name = "NetAssemblyGenerator";
			public const string DisplayName = ".NET Assembly Generator";
			public const string RepositoryDisplayName = ".NET Assembly";
			public const string Description = "";
			internal const string FileFilter = "Assemblies (*.dll)|*.dll|All files (*.*)|*.*";
			public const string FileExtension = "dll";
		}

		public static class SqlConnection
		{
			public const string Name = "SqlConnection";
			public const string DisplayName = "SQL Server Database";
			public const string RepositoryDisplayName = "SQL Server Database";
			public const string Description = "";
		}

		public static class MemoryReader
		{
			public const string Name = "MemoryReader";
			public const string DisplayName = "Memory Reader";
			public const string RepositoryDisplayName = "Memory";
			public const string Description = "";
		}

/*
        internal static class AggregateRepositoryReader
		{
			private AggregateRepositoryReader()
			{
			}

			internal const string Name = "AggregateRepositoryReader";
			internal const string DisplayName = "LinkMe Aggregate Repository Reader";
			internal const string RepositoryDisplayName = "Aggregated Repositories";
			internal const string Description = "";
		}

		internal static class DistributeRepositoryReader
		{
			private DistributeRepositoryReader()
			{
			}

			internal const string Name = "DistributeRepositoryReader";
			internal const string DisplayName = "LinkMe Distribute Repository Reader";
			internal const string RepositoryDisplayName = "Distributed Repositories";
			internal const string Description = "";
		}
*/
    }

	public static class StoreType
	{
        public static class SqlStoreConnection
        {
            public const string Name = "SqlStoreConnection";
            public const string DisplayName = "SQL Server Database";
            public const string StoreDisplayName = "SQL Server Database";
            public const string Description = "";
        }

        public static class MsmqStoreConnection
        {
            public const string Name = "MsmqStoreConnection";
            public const string DisplayName = "MSMQ Store";
            public const string StoreDisplayName = "MSMQ Queue";
            public const string Description = "";
        }

        public static class ObjectFileConnection
		{
			public const string Name = "ObjectFileConnection";
            public const string DisplayName = "Object File Connection";
            public const string StoreDisplayName = "Object File";
            public const string Description = "";
			public const string FileFilter = "Object files (*.config)|*.config|Object source files (*.acs)|*.acs|All files (*.*)|*.*";
			public const string FileExtension = "config";
		}
    }

    public static class LinkType
    {
        public static class WmiLinkType
        {
            public const string Name = "WmiLinkType";
            public const string DisplayName = Constants.RepositoryType.WmiConnection.DisplayName;
        }

        public static class SqlLinkType
        {
            public const string Name = "SqlLinkType";
            public const string DisplayName = Constants.RepositoryType.SqlConnection.DisplayName;
        }

        public static class MsmqLinkType
        {
            public const string Name = "MsmqLinkType";
            public const string DisplayName = Constants.StoreType.MsmqStoreConnection.DisplayName;
        }
    }

    public static class Config
	{
        public const string RootElement = "configuration";
        public const string SectionElement = "linkme.framework";
        public const string EnvironmentRepositoryElement = "environment.repository";
        public const string EnvironmentRepositorySection = SectionElement + "/" + EnvironmentRepositoryElement;
        public const string EnvironmentElement = "environment";
        public const string EnvironmentSection = SectionElement + "/" + EnvironmentElement;
        public const string ReferencesElement = "references";
        public const string ReferenceElement = "reference";
        public const string FileElement = "file";
        public const string RepositoryElement = "repository";
        public const string RepositoryNameAttribute = "name";
        public const string RepositoryTypeAttribute = "repositoryType";
        public const string InitialisationStringElement = "initialisationString";
        public const string IsLocalAttribute = "isLocal";
        public const string StoreElement = "store";
        public const string StoreNameAttribute = "name";
        public const string StoreTypeAttribute = "storeType";
	}

    public static class Validation
	{
		internal const string NamePattern = "[a-zA-Z][0-9a-zA-Z_]*";
        public const string CompleteNamePattern = "^" + NamePattern + "$";
		internal const string FullNamePattern = NamePattern + "(\\." + NamePattern + ")*";
        public const string CompleteFullNamePattern = "^" + FullNamePattern + "$";
		internal const string CapturingFullNamePattern = "^(?:(?<Namespace>" + NamePattern
			+ "(?:\\." + NamePattern + ")*)\\.)?(?<Name>" + NamePattern + ")";
		internal const string CompleteCapturingFullNamePattern = "^" + CapturingFullNamePattern + "$";
		internal const string DisplayNamePattern = "[\\.a-zA-Z][ \\.0-9a-zA-Z]*";
        public const string CompleteDisplayNamePattern = "^" + DisplayNamePattern + "$";

		internal const string VersionPattern = "[0-9]+\\.[0-9]+(?:\\.[0-9]+\\.[0-9]+)?";
		public const string CompleteVersionPattern = "^" + VersionPattern + "$";
		public const string QualifiedReferenceVersionPattern = ", Version=" + VersionPattern;
		internal const string QualifiedReferencePattern = FullNamePattern + "(" + QualifiedReferenceVersionPattern + ")?";
		public const string CompleteQualifiedReferencePattern = "^" + QualifiedReferencePattern + "$";
		internal const string CompleteXsiTypePattern = "^" + FullNamePattern + "(-" + VersionPattern + ")?" + "$";
		internal const string RelativeQualifiedReferencePattern = NamePattern + "(" + QualifiedReferenceVersionPattern + ")?";
		internal const string CompleteRelativeQualifiedReferencePattern = "^" + RelativeQualifiedReferencePattern + "$";

		// ^(?:(?<Namespace>[a-zA-Z][0-9a-zA-Z_]*(?:\.[a-zA-Z][0-9a-zA-Z_]*)*)\.)?(?<Name>[a-zA-Z][0-9a-zA-Z_]*)(?:,\sVersion=(?<Version>[0-9]+\.[0-9]+(?:\.[0-9]+\.[0-9]+)?))?$
		internal const string CompleteCapturingQualifiedReferencePattern = "^" + CapturingFullNamePattern
			+ "(?:, Version=(?<Version>" + VersionPattern + "))?" + "$";
		internal const string CompleteCapturingXsiTypePattern = "^" + CapturingFullNamePattern
			+ "(?:-(?<Version>" + VersionPattern + "))?" + "$";
		internal const string ElementReferencePattern = QualifiedReferencePattern + ", " + NamePattern + "=" + NamePattern;
		internal const string CompleteElementReferencePattern = "^" + ElementReferencePattern + "$";
		internal const string CompleteCapturingElementReferencePattern = "^(?<Element>" + NamePattern +")=(?<Name>" + NamePattern + "), (?<CatalogueName>" + QualifiedReferencePattern +")";

        internal const string PathNamePattern = "[0-9a-zA-Z][\\-0-9a-zA-Z_]*";
        internal const string PathPattern = "/" + "(" + PathNamePattern + ")?" + "(/" + PathNamePattern + ")*";
        internal const string CompletePathPattern = "^" + PathPattern + "$";
        internal const string RelativePathPattern = PathNamePattern + "(/" + PathNamePattern + ")*";
        internal const string CompleteRelativePathPattern = "^" + RelativePathPattern + "$";
        internal const string CapturingPathPattern = "^(?:(?<Parent>" + PathPattern
            + "(?:/" + PathNamePattern + ")*)/)?(?<Name>" + PathNamePattern + ")";
    }

	internal static class Exceptions
	{
		internal const string ElementKey = "ElementKey";
		internal const string ElementReference = "ElementReference";
		internal const string ElementType = "ElementType";
		internal const string ExistingElementKey = "ExistingElementKey";
		internal const string ExistingElementType = "ExistingElementType";
		internal const string ParentReference = "ParentReference";
		internal const string ParentElementType = "ParentElementType";
		internal const string Module = "Module";
		internal const string RepositoryType = "RepositoryType";
		internal const string StoreType = "StoreType";
		internal const string InitialisationString = "InitialisationString";
		internal const string XmlData = "XmlData";
        internal const string Name = "Name";
        internal const string Value = "Value";
	}

	internal static class Xsi
	{
		internal const string Prefix = LinkMe.Framework.Utility.Constants.Xsi.Prefix;
		internal const string Namespace = LinkMe.Framework.Utility.Constants.Xsi.Namespace;
		internal const string TypeAttribute = LinkMe.Framework.Utility.Constants.Xsi.TypeAttribute;
	}

	internal static class Xmlns
	{
		internal const string Prefix = LinkMe.Framework.Utility.Constants.Xmlns.Prefix;
		internal const string Namespace = LinkMe.Framework.Utility.Constants.Xmlns.Namespace;
	}
}


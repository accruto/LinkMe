namespace LinkMe.Framework.Instrumentation.Management.Constants
{
	public static class Module
	{
        public const string Name = "Instrumentation";
		public const string DisplayName = "Instrumentation";
	}

	internal static class Collections
	{
		internal const string Namespaces = "Namespaces";
		internal const string Sources = "Sources";
		internal const string EventTypes = "EventTypes";
		internal const string ChildMessageHandlers = "ChildMessageHandlers";
		internal const string MessageReaderTypes = "MessageReaderTypes";
		internal const string MessageReaders = "MessageReaders";
		internal const string Instances = "Instances";
	}

    public static class Xml
	{
        public const string Namespace = LinkMe.Framework.Utility.Constants.Xml.RootNamespace + "/Instrumentation";
	}

	internal static class Validation
	{
		internal const string CompleteVersionPattern = LinkMe.Framework.Configuration.Constants.Validation.CompleteVersionPattern;
		internal const string CompleteNamePattern = LinkMe.Framework.Configuration.Constants.Validation.CompleteNamePattern;
		internal const string CompleteFullNamePattern = LinkMe.Framework.Configuration.Constants.Validation.CompleteFullNamePattern;
		internal const string CompleteDisplayNamePattern = LinkMe.Framework.Configuration.Constants.Validation.CompleteDisplayNamePattern;
		internal const string CompleteQualifiedReferencePattern = LinkMe.Framework.Configuration.Constants.Validation.CompleteQualifiedReferencePattern;
		internal const string QualifiedReferenceVersionPattern = LinkMe.Framework.Configuration.Constants.Validation.QualifiedReferenceVersionPattern;
	}
}


namespace LinkMe.Framework.Type.Constants
{
	public static class DateTime
	{
		public const string ParseFormat = "dd/MM/yyyy HH:mm:ss.nnnnnnnnn";
        public const int ParseFormatMinLength = 29;
        public const string SubsecondsParseFormat = "nnnnnnnnn";
	}

    public static class Date
	{
        public const string ParseFormat = "dd/MM/yyyy";
        public const int ParseFormatLength = 10;
	}

    public static class TimeOfDay
	{
        public const string ParseFormat = "HH:mm:ss.nnnnnnnnn";
		internal const int ParseFormatLength = 18;
        public const string SubsecondsParseFormat = "nnnnnnnnn";
	}

    public static class TimeSpan
	{
        public const string ParseFormat = "p d HH:mm:ss.nnnnnnnnn";
		internal const int ParseFormatMinLength = 20;
		internal const int ParseFormatDayLength = 8;
        public const string SignParseFormat = "p";
        public const string DaysParseFormat = "d";
        public const string SubsecondsParseFormat = "nnnnnnnnn";
	}

    public static class Xml
	{
        public const string Namespace = LinkMe.Framework.Utility.Constants.Xml.RootNamespace + "/Type";
        public const string Prefix = "lmt";

		internal static class Exception
		{
			internal const string PropertiesElement = "Properties";
		}
	}

	internal static class Xsi
	{
		internal const string Prefix = LinkMe.Framework.Utility.Constants.Xsi.Prefix;
		internal const string Namespace = LinkMe.Framework.Utility.Constants.Xsi.Namespace;
		internal const string NilAttribute = LinkMe.Framework.Utility.Constants.Xsi.NilAttribute;
		internal const string TypeAttribute = LinkMe.Framework.Utility.Constants.Xsi.TypeAttribute;
	}

	internal static class Exceptions
	{
		internal const string ResourceBaseName = "LinkMe.Framework.Type.Source.Exceptions.Exceptions";
		internal const string TypeName = "TypeName";
		internal const string XsiTypeName = "XsiTypeName";
	}

	internal class Serialization
	{
		private Serialization()
		{
		}

		internal static class Exception
		{
			internal const string Properties = "Properties";
		}
	}

	internal static class TimeZone
	{
		internal const string Win95Key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Time Zones";
		internal const string WinNTKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones";
	}
}


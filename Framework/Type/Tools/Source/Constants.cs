namespace LinkMe.Framework.Type.Tools.Constants
{
	internal static class DateTime
	{
		internal const string ParseFormat = LinkMe.Framework.Type.Constants.DateTime.ParseFormat;
		internal const int ParseFormatMinLength = LinkMe.Framework.Type.Constants.DateTime.ParseFormatMinLength;
		internal const string SubsecondsParseFormat = LinkMe.Framework.Type.Constants.DateTime.SubsecondsParseFormat;
		internal const string SubsecondsCustomFormat = "X";
	}

	internal static class Date
	{
		internal const string ParseFormat = LinkMe.Framework.Type.Constants.Date.ParseFormat;
		internal const int ParseFormatLength = LinkMe.Framework.Type.Constants.Date.ParseFormatLength;
	}

	internal static class TimeOfDay
	{
		internal const string ParseFormat = LinkMe.Framework.Type.Constants.TimeOfDay.ParseFormat;
		internal const string SubsecondsParseFormat = LinkMe.Framework.Type.Constants.TimeOfDay.SubsecondsParseFormat;
		internal const string SubsecondsCustomFormat = "X";
	}

	internal static class TimeSpan
	{
		internal const string ParseFormat = LinkMe.Framework.Type.Constants.TimeSpan.ParseFormat;
		internal const string SignParseFormat = LinkMe.Framework.Type.Constants.TimeSpan.SignParseFormat;
		internal const string SignCustomFormat = "XXX";
		internal const string DaysParseFormat = LinkMe.Framework.Type.Constants.TimeSpan.DaysParseFormat;
		internal const string DaysCustomFormat = "XX";
		internal const string SubsecondsParseFormat = LinkMe.Framework.Type.Constants.TimeSpan.SubsecondsParseFormat;
		internal const string SubsecondsCustomFormat = "X";
	}

    internal static class Win32
    {
        internal static class Messages
        {
            internal const int WM_KEYUP = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_KEYUP;
        }
    }
}

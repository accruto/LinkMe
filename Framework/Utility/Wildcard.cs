using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility
{
	[System.Serializable]
	public class Wildcard
		:	Regex
	{
		public Wildcard(string pattern)
			:	base(WildcardToRegex(pattern))
		{
		}
 
		public Wildcard(string pattern, RegexOptions options)
			:	base(WildcardToRegex(pattern), options)
		{
		}
 
		// The base deserialization constructor is private for some stupid reason, so "re-implement" it here.

		protected Wildcard(SerializationInfo info, StreamingContext context)
			: base(info.GetString("pattern"), (RegexOptions) info.GetInt32("options"))
		{
		}

		public static string WildcardToRegex(string pattern)
		{
			return "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
		}
	}
}

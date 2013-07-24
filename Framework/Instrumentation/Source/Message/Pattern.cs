using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Instrumentation.Message
{
	public enum PatternType
	{
		Exact,
		Regex
	}

	/// <summary>
	/// Represents a pattern used in a filter.
	/// </summary>
	public class Pattern
	{
		private readonly PatternType _type;
		private string _value;
		private Regex _regex;

		public Pattern(PatternType type, string value)
		{
			_type = type;
			_value = value;
		}

		public PatternType Type
		{
			get { return _type; }
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		private Regex Regex
		{
			get
			{
				if (_regex == null)
				{
					lock (this)
					{
						if (_regex == null)
						{
							_regex = new Regex(_value, RegexOptions.Compiled);
						}
					}
				}

				return _regex;
			}
		}

		public bool IsMatch(string input)
		{
			switch (_type)
			{
				case PatternType.Exact:
					return (input == _value);

				case PatternType.Regex:
					return Regex.IsMatch(input);

				default:
					Debug.Fail("Unexpeced value of PatternType: " + _type);
					return false;
			}
		}
	}
}
using System.Diagnostics;

namespace LinkMe.Framework.Type
{
	internal sealed class DateTimeHelper
	{
		// Parse

		public const string TimeZoneParseFormat = "(+|-)hh:mm";
		public const int TimeZoneParseFormatLength = 6;

		public const string DateXmlFormat = "CCYY-MM-DD";
		public const int DateXmlFormatLength = 10;

		public const string DateTimeXmlFormat = "CCYY-MM-DDThh:mm:ss[.nnnnnnnnn]("
			+ TimeZoneParseFormat + "|Z [timeZoneName])";
		public const int DateTimeXmlFormatMinLength = 20;

		public const string TimeOfDayXmlFormat = "hh:mm:ss[.nnnnnnnnn]";
		public const int TimeOfDayXmlFormatMinLength = 8;

		public const string TimeSpanXmlFormat = "[-]PnDTnHnMnS.nnnnnnnnn";
		public const int TimeSpanXmlFormatMinLength = 3;

		// Max & Min

		public const int MinYear = 1;
		public const int MaxYear = 9999;
		public const int MinMonth = 1;
		public const int MaxMonth = 12;
		public const int MinDay = 1;
		public const int MaxDay = 31;
		public const int MinHour = 0;
		public const int MaxHour = 23;
		public const int MinMinute = 0;
		public const int MaxMinute = 59;
		public const int MinSecond = 0;
		public const int MaxSecond = 59;
		public const int MinMillisecond = 0;
		public const int MaxMillisecond = 999;
		public const int MinMicrosecond = 0;
		public const int MaxMicrosecond = 999;
		public const int MinNanosecond = 0;
		public const int MaxNanosecond = 999;

		public const int MinTimeZone = -840;
		public const int MaxTimeZone = 840;

		// Conversion

		public const int Days2Hours = 24;
		public const long Days2Nanoseconds = 86400000000000;
		public const int Hours2Minutes = 60;
		public const long Hours2Nanoseconds = 3600000000000;
		public const int Minutes2Seconds = 60;
		public const long Minutes2Nanoseconds = 60000000000;
		public const int Seconds2Milliseconds = 1000;
		public const long Seconds2Nanoseconds = 1000000000;
		public const int Milliseconds2Microseconds = 1000;
		public const int Milliseconds2Nanoseconds = 1000000;
		public const int Microseconds2Nanoseconds = 1000;
		public const int Nanoseconds2Ticks = 100;

		private DateTimeHelper()
		{
		}

		/// <summary>
		/// Add two values of a "place" (such as hours, minutes, seconds, etc.) in a 
		/// date/time value. If the result is greater than what the place can store
		/// carry over into the next place. All values should be whole numbers.
		/// </summary>
		public static int AddPlaceValues(double value1, double value2, int carryLimit,
			out double carry)
		{
			// Round the result, in case the values are not exact whole numbers
			// (they should be close).
			double result = System.Math.Round(value1 + value2);
			carry = System.Math.Floor(result / carryLimit);
			result = result % carryLimit;

			// Make sure the result is positive (negative input is allowed)

			if (result < 0)
			{
				result += carryLimit;
				carry--;
			}

			// Sanity check - result should be an exact whole number
			Debug.Assert(result == (double)(int)result, "AddPlaceValues may have returned"
				+ " incorrect integer value for " + result.ToString());

			return (int)result;
		}

		/// <summary>
		/// Returns the string representation of the absolute value of the specified
		/// integer, padded to the specified number of digits.
		/// </summary>
		public static string Digits(int val, int digits)
		{
			return System.Math.Abs(val).ToString().PadLeft(digits, '0');
		}
	}
}

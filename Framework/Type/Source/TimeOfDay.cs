using System.Runtime.Serialization;
using System.Text;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	[System.Serializable]
	public struct TimeOfDay
		:	System.IComparable,
			ISerializable
	{
		// Constant values for date to pass to our base System.DateTime. This is roughly
		// in the middle of the range of values it supports, which allows adding the
		// largest possible values to TimeOfDay without the System.DateTime throwing
		// an exception (eg. TimeOfDay.AddHours(1000000)).

		private const int Year = 5000;
		private const int Month = 1;
		private const int Day = 1;

		/// <summary>
		/// Represents the smallest possible value of TimeOfDay.
		/// </summary>
		public static readonly TimeOfDay MinValue = new TimeOfDay(
			DateTimeHelper.MinHour, DateTimeHelper.MinMinute, DateTimeHelper.MinSecond,
			DateTimeHelper.MinMillisecond, DateTimeHelper.MinMicrosecond,
			DateTimeHelper.MinNanosecond);
		/// <summary>
		/// Represents the largest possible value of TimeOfDay.
		/// </summary>
		public static readonly TimeOfDay MaxValue = new TimeOfDay(
			DateTimeHelper.MaxHour, DateTimeHelper.MaxMinute, DateTimeHelper.MaxSecond,
			DateTimeHelper.MaxMillisecond, DateTimeHelper.MaxMicrosecond,
			DateTimeHelper.MaxNanosecond);

		private static readonly System.DateTime DefaultBaseValue =
			new System.DateTime(Year, Month, Day, 0, 0, 0, 0);

		/// <summary>
		/// Occurs when when the TimeOfDay wraps around as a result of a calculation,
		/// that is, the resulting TimeOfDay is on a different day.
		/// </summary>
		public event TimeOfDayWrapAroundEventHandler WrapAround;

		private readonly System.DateTime m_base;
		private readonly int m_microsecond;
		private readonly int m_nanosecond;
		// Set to true by every constructor, except the implicit default constructor
		private readonly bool m_initialised;

		#region Constructors

		public TimeOfDay(int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
			: this(new System.DateTime(1, 1, 1, hour, minute, second, millisecond),
				microsecond, nanosecond)
		{
		}

		public TimeOfDay(int hour, int minute, int second)
			: this(new System.DateTime(1, 1, 1, hour, minute, second), 0, 0)
		{
		}

		private TimeOfDay(SerializationInfo info, StreamingContext context)
		{
			m_base = info.GetDateTime("m_base");
			m_microsecond = info.GetInt32("m_microsecond");
			m_nanosecond = info.GetInt32("m_nanosecond");

			WrapAround = null;
			m_initialised = true;
		}

		private TimeOfDay(System.DateTime baseValue, int microsecond, int nanosecond)
		{
			const string method = ".ctor";

			if (microsecond < DateTimeHelper.MinMicrosecond || microsecond > DateTimeHelper.MaxMicrosecond)
				throw new ParameterOutOfRangeException(typeof(TimeOfDay), method,
					"microsecond", microsecond, DateTimeHelper.MinMicrosecond, DateTimeHelper.MaxMicrosecond);
			if (nanosecond < DateTimeHelper.MinNanosecond || nanosecond > DateTimeHelper.MaxNanosecond)
				throw new ParameterOutOfRangeException(typeof(TimeOfDay), method,
					"nanosecond", nanosecond, DateTimeHelper.MinNanosecond, DateTimeHelper.MaxNanosecond);

			// Ensure that date is always set to 01/01/5000
			m_base = new System.DateTime(Year, Month, Day, baseValue.Hour,
				baseValue.Minute, baseValue.Second, baseValue.Millisecond);

			m_microsecond = microsecond;
			m_nanosecond = nanosecond;

			WrapAround = null;
			m_initialised = true;
		}

		#endregion

		#region IComparable Members

		int System.IComparable.CompareTo(object other)
		{
			const string method = "IComparable.CompareTo";

			if (!(other is TimeOfDay))
				throw new InvalidParameterTypeException(typeof(TimeOfDay), method, "other", typeof(TimeOfDay), other);

			return Compare(this, (TimeOfDay)other);
		}

		#endregion

		#region ISerializable Members

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_base", BaseValue);
			info.AddValue("m_microsecond", m_microsecond);
			info.AddValue("m_nanosecond", m_nanosecond);
		}

		#endregion

		/// <summary>
		/// Gets a TimeOfDay that is the current time on this computer.
		/// </summary>
		public static TimeOfDay Now
		{
			get
			{
				return DateTime.Now.TimeOfDay;
			}
		}

		#region Access properties

		public int Hour
		{
			get
			{
				return BaseValue.Hour;
			}
		}

		public int Minute
		{
			get
			{
				return BaseValue.Minute;
			}
		}

		public int Second
		{
			get
			{
				return BaseValue.Second;
			}
		}

		public int Millisecond
		{
			get
			{
				return BaseValue.Millisecond;
			}
		}

		public int Microsecond
		{
			get
			{
				return m_microsecond;
			}
		}

		public int Nanosecond
		{
			get
			{
				return m_nanosecond;
			}
		}

		#endregion

		/// <summary>
		/// Private property wrapper for m_base, which ensures that it's initialised
		/// even when the default constructor is used.
		/// </summary>
		private System.DateTime BaseValue
		{
			get
			{
				if (m_initialised)
					return m_base;
				else
					return DefaultBaseValue;
			}
		}

		/// <summary>
		/// A private property that's called by the Visual Studio .NET debugger to display
		/// this object's value in the Watch window. This needs to be configured in
		/// Microsoft Visual Studio .NET 2003\Common7\Packages\Debugger\mcee_cs.dat
		/// </summary>
		private string DebuggerDisplayValue
		{
			get
			{
				return ToString();
			}
		}

		#region Operators

		public static bool operator==(TimeOfDay t1, TimeOfDay t2)
		{
			return Equals(t1, t2);
		}

		public static bool operator!=(TimeOfDay t1, TimeOfDay t2)
		{
			return !Equals(t1, t2);
		}

		public static bool operator<(TimeOfDay t1, TimeOfDay t2)
		{
			return (Compare(t1, t2) < 0);
		}

		public static bool operator>(TimeOfDay t1, TimeOfDay t2)
		{
			return (Compare(t1, t2) > 0);
		}

		public static bool operator<=(TimeOfDay t1, TimeOfDay t2)
		{
			return (Compare(t1, t2) <= 0);
		}

		public static bool operator>=(TimeOfDay t1, TimeOfDay t2)
		{
			return (Compare(t1, t2) >= 0);
		}

		public static TimeOfDay operator+(TimeOfDay t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		public static TimeOfDay operator-(TimeOfDay t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		public static TimeSpan operator-(TimeOfDay t1, TimeOfDay t2)
		{
			return t1.Subtract(t2);
		}

		#endregion

		#region Static methods

		public static int Compare(TimeOfDay t1, TimeOfDay t2)
		{
			// Base value
			int result = System.DateTime.Compare(t1.BaseValue, t2.BaseValue);

			// Microsecond
			if (result == 0)
			{
				result = t1.m_microsecond - t2.m_microsecond;
			}

			// Nanosecond
			if (result == 0)
			{
				result = t1.m_nanosecond - t2.m_nanosecond;
			}

			return System.Math.Sign(result);
		}

		public static bool Equals(TimeOfDay t1, TimeOfDay t2)
		{
			return (Compare(t1, t2) == 0);
		}

		public static TimeOfDay Parse(string value)
		{
			const string method = "Parse";

			// Check that there is something there and of the right length.

			if ( value == null )
				throw new NullParameterException(typeof(TimeOfDay), method, "value");

			if ( value.Length != Constants.TimeOfDay.ParseFormatLength )
				throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, Constants.TimeOfDay.ParseFormat);

			// Check for separators in the right places

			if ( value[2] != ':' || value[5] != ':' || value[8] != '.' )
				throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, Constants.TimeOfDay.ParseFormat);

			// Extract each part.

			int hour = ParseInt(value, 0, 2, false);
			int minute = ParseInt(value, 3, 2, false);
			int second = ParseInt(value, 6, 2, false);
			int millisecond = ParseInt(value, 9, 3, false);
			int microsecond = ParseInt(value, 12, 3, false);
			int nanosecond = ParseInt(value, 15, 3, false);
			return new TimeOfDay(hour, minute, second, millisecond, microsecond, nanosecond);
		}

		/// <summary>
		/// Converts the XML representation of a time of day in XSD time format
		/// (http://www.w3.org/TR/xmlschema-2/#time) to a TimeOfDay object.
		/// </summary>
		internal static TimeOfDay FromXml(string value)
		{
			const string method = "FromXml";

			// Check that there is something there and of the right length.

			if (value == null)
				throw new NullParameterException(typeof(TimeOfDay), method, "value");

			if (value.Length < DateTimeHelper.TimeOfDayXmlFormatMinLength)
				throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, DateTimeHelper.TimeOfDayXmlFormat);

			// Check for separators in the right places.

			if (value[2] != ':' || value[5] != ':')
				throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, DateTimeHelper.TimeOfDayXmlFormat);

			// Extract each part, except for fractional seconds.

			int hour = ParseInt(value, 0, 2, true);
			int minute = ParseInt(value, 3, 2, true);
			int second = ParseInt(value, 6, 2, true);
			int millisecond = 0;
			int microsecond = 0;
			int nanosecond = 0;

			// Fractional seconds

			if (value.Length > DateTimeHelper.TimeOfDayXmlFormatMinLength)
			{
				if (value[8] != '.')
					throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, DateTimeHelper.TimeOfDayXmlFormat);

				int index = 9;

				while (index < value.Length && System.Char.IsDigit(value, index))
				{
					index++;
				}

				if (index == 9 || index < value.Length)
					throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "value", value, DateTimeHelper.TimeOfDayXmlFormat);

				string fraction = value.Substring(9, index - 9);

				if (fraction.Length < 9)
				{
					fraction = fraction.PadRight(9, '0');
				}

				millisecond = System.Int32.Parse(fraction.Substring(0, 3));
				microsecond = System.Int32.Parse(fraction.Substring(3, 3));
				nanosecond = System.Int32.Parse(fraction.Substring(6, 3));

				// XSD time type allows arbitrary precision, so extra decimal
				// places are valid. Just ignore them.
			}

			return new TimeOfDay(hour, minute, second, millisecond, microsecond, nanosecond);
		}

		internal static TimeOfDay Read(System.IO.BinaryReader reader)
		{
			int hour = reader.ReadInt32();
			int minute = reader.ReadInt32();
			int second = reader.ReadInt32();
			int millisecond = reader.ReadInt32();
			int microsecond = reader.ReadInt32();
			int nanosecond = reader.ReadInt32();

			return new TimeOfDay(hour, minute, second, millisecond, microsecond, nanosecond);
		}

		private static int ParseInt(string str, int startIndex, int length, bool xmlFormat)
		{
			const string method = "ParseInt";

			string toParse = str.Substring(startIndex, length);

			foreach (char c in toParse)
			{
				if (!System.Char.IsDigit(c))
					throw new InvalidParameterFormatException(typeof(TimeOfDay), method, "str", str, xmlFormat ? DateTimeHelper.TimeSpanXmlFormat : Constants.TimeOfDay.ParseFormat);
			}

			return int.Parse(toParse);
		}

		#endregion

		public override bool Equals(object other)
		{
			if (!(other is TimeOfDay))
				return false;
			return Equals(this, (TimeOfDay)other);
		}

		public override int GetHashCode()
		{
			return BaseValue.GetHashCode() ^ m_microsecond.GetHashCode() ^ m_nanosecond.GetHashCode();
		}

		public override string ToString()
		{
			const int outputLength = Constants.TimeOfDay.ParseFormatLength;

			StringBuilder sb = new StringBuilder(outputLength);

			// Hour

			sb.Append(DateTimeHelper.Digits(Hour, 2));
			sb.Append(':');

			// Minute

			sb.Append(DateTimeHelper.Digits(Minute, 2));
			sb.Append(':');

			// Second

			sb.Append(DateTimeHelper.Digits(Second, 2));
			sb.Append('.');

			// Millisecond

			sb.Append(DateTimeHelper.Digits(Millisecond, 3));

			// Microsecond

			sb.Append(DateTimeHelper.Digits(Microsecond, 3));

			// Nanosecond

			sb.Append(DateTimeHelper.Digits(Nanosecond, 3));
			return sb.ToString();
		}

		#region Operations

		public TimeOfDay AddHours(double hours)
		{
			double whole = System.Math.Floor(hours);
			double fractional = hours - whole;

			TimeOfDay result = AddNanosecondsInternal(
				fractional * DateTimeHelper.Hours2Nanoseconds);
			result = new TimeOfDay(result.BaseValue.AddHours(whole),
				result.m_microsecond, result.m_nanosecond);

			return result.CheckForWrapAround();
		}

		public TimeOfDay AddMinutes(double minutes)
		{
			double whole = System.Math.Floor(minutes);
			double fractional = minutes - whole;

			TimeOfDay result = AddNanosecondsInternal
				(fractional * DateTimeHelper.Minutes2Nanoseconds);
			result = new TimeOfDay(result.BaseValue.AddMinutes(whole),
				result.m_microsecond, result.m_nanosecond);

			return result.CheckForWrapAround();
		}

		public TimeOfDay AddSeconds(double seconds)
		{
			double whole = System.Math.Floor(seconds);
			double fractional = seconds - whole;

			TimeOfDay result = AddNanosecondsInternal
				(fractional * DateTimeHelper.Seconds2Nanoseconds);
			result = new TimeOfDay(result.BaseValue.AddSeconds(whole),
				result.m_microsecond, result.m_nanosecond);

			return result.CheckForWrapAround();
		}

		public TimeOfDay AddMilliseconds(double milliseconds)
		{
			double whole = System.Math.Floor(milliseconds);
			double fractional = milliseconds - whole;

			TimeOfDay result = AddNanosecondsInternal
				(fractional * DateTimeHelper.Milliseconds2Nanoseconds);
			result = result.AddMillisecondsInternal(whole);

			return result.CheckForWrapAround();
		}

		public TimeOfDay AddMicroseconds(double microseconds)
		{
			double whole = System.Math.Floor(microseconds);
			double fractional = microseconds - whole;

			TimeOfDay result = AddNanosecondsInternal
				(fractional * DateTimeHelper.Microseconds2Nanoseconds);
			result = result.AddMicrosecondsInternal(whole);

			return result.CheckForWrapAround();
		}

		public TimeOfDay AddNanoseconds(double nanoseconds)
		{
			TimeOfDay result = AddNanosecondsInternal(System.Math.Round(nanoseconds));
			return result.CheckForWrapAround();
		}

		public TimeOfDay Add(TimeSpan timeSpan)
		{
			TimeOfDay result = new TimeOfDay(BaseValue.Add(timeSpan.ToSystemTimeSpan()),
				m_microsecond, m_nanosecond);

			result = result.AddMicrosecondsInternal(timeSpan.Microseconds);
			result = result.AddNanosecondsInternal(timeSpan.Nanoseconds);

			return result.CheckForWrapAround();
		}

		public TimeOfDay Subtract(Type.TimeSpan timeSpan)
		{
			return Add(timeSpan.Negate());
		}

		public Type.TimeSpan Subtract(TimeOfDay other)
		{
			Type.TimeSpan result = BaseValue.Subtract(other.BaseValue);

			Type.TimeSpan microNano = new Type.TimeSpan(0, 0, 0, 0, 0,
				other.m_microsecond, other.m_nanosecond);

			return result.Subtract(microNano);
		}

		#endregion

		/// <summary>
		/// Returns the XML representation of the object in XSD time format
		/// (http://www.w3.org/TR/xmlschema-2/#time).
		/// </summary>
		internal string ToXml()
		{
			const int outputLength = 18;

			StringBuilder sb = new StringBuilder(outputLength);

			// Hour
			sb.Append(DateTimeHelper.Digits(Hour, 2));
			sb.Append(':');

			// Minute
			sb.Append(DateTimeHelper.Digits(Minute, 2));
			sb.Append(':');

			// Second
			sb.Append(DateTimeHelper.Digits(Second, 2));

			// Fractional seconds (if any) to a maximum of 9 decimal places
			long nanoseconds = (long) Nanosecond
				+ (long)Microsecond * DateTimeHelper.Microseconds2Nanoseconds
				+ (long)Millisecond * DateTimeHelper.Milliseconds2Nanoseconds;

			if (nanoseconds != 0)
			{
				sb.Append('.');
				sb.Append(DateTimeHelper.Digits(Millisecond, 3));
				sb.Append(DateTimeHelper.Digits(Microsecond, 3));
				sb.Append(DateTimeHelper.Digits(Nanosecond, 3));
				return sb.ToString().TrimEnd('0');
			}
			else
			{
				return sb.ToString();
			}
		}

		internal void Write(System.IO.BinaryWriter writer)
		{
			writer.Write(Hour);
			writer.Write(Minute);
			writer.Write(Second);
			writer.Write(Millisecond);
			writer.Write(Microsecond);
			writer.Write(Nanosecond);
		}

		#region Add*Internal methods

		// Add*Internal methods add values assuming that the TimeOfDay is in UTC and that
		// the values to be added are whole numbers (double type is used instead of int
		// to allow larger values and avoid unnecessary casting).

		private TimeOfDay AddMillisecondsInternal(double milliseconds)
		{
			return new TimeOfDay(BaseValue.AddMilliseconds(milliseconds), m_microsecond,
				m_nanosecond);
		}

		private TimeOfDay AddMicrosecondsInternal(double microseconds)
		{
			double carry;
			int newValue = DateTimeHelper.AddPlaceValues(m_microsecond, microseconds,
				DateTimeHelper.Milliseconds2Microseconds, out carry);

			TimeOfDay result = new TimeOfDay(BaseValue, newValue, m_nanosecond);

			if (carry == 0)
				return result;
			else
				return result.AddMillisecondsInternal(carry);
		}

		private TimeOfDay AddNanosecondsInternal(double nanoseconds)
		{
			double carry;
			int newValue = DateTimeHelper.AddPlaceValues(m_nanosecond, nanoseconds,
				DateTimeHelper.Microseconds2Nanoseconds, out carry);

			TimeOfDay result = new TimeOfDay(BaseValue, m_microsecond, newValue);

			if (carry == 0)
				return result;
			else
				return result.AddMicrosecondsInternal(carry);
		}

		#endregion

		private TimeOfDay CheckForWrapAround()
		{
			if (BaseValue.Day == Day && BaseValue.Month == Month && BaseValue.Year == Year)
				return this;
			else
			{
				// Raise an event to tell the caller that we wrapped around
				System.TimeSpan wrapped = new System.DateTime(BaseValue.Year, BaseValue.Month,
					BaseValue.Day) - new System.DateTime(Year, Month, Day);
				OnWrapAround(new TimeOfDayWrapAroundEventArgs(wrapped.Days));

				// Return a proper TimeOfDay value (with BaseValue set to the constant date)
				return new TimeOfDay(BaseValue, m_microsecond, m_nanosecond);
			}
		}

		private void OnWrapAround(TimeOfDayWrapAroundEventArgs e)
		{
			if (WrapAround != null)
			{
				WrapAround(this, e);
			}
		}
	}

	public class TimeOfDayWrapAroundEventArgs
		:	System.EventArgs
	{
		private int m_days;

		internal TimeOfDayWrapAroundEventArgs(int days)
		{
			m_days = days;
		}

		/// <summary>
		/// The number of days gained or lost as a result of the wrap-around.
		/// </summary>
		public int Days
		{
			get
			{
				return m_days;
			}
		}
	}

	public delegate void TimeOfDayWrapAroundEventHandler(object sender, TimeOfDayWrapAroundEventArgs e);
}

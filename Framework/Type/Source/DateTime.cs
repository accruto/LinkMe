using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	[System.Serializable]
	public struct DateTime
		:	System.IComparable,
			System.IConvertible,
			ISerializable
	{
		// System.DateTime.MaxValue cannot be used for our constant, because it uses
		// ticks to store microseconds and we don't, so
		// System.DateTime.MaxValue != new System.DateTime(9999, 12, 31, 23, 59, 59, 999)

		/// <summary>
		/// Represents the smallest possible value of DateTime. This value is in UTC.
		/// </summary>
		public static readonly DateTime MinValue = new DateTime(
			DateTimeHelper.MinYear, DateTimeHelper.MinMonth, DateTimeHelper.MinDay,
			DateTimeHelper.MinHour, DateTimeHelper.MinMinute, DateTimeHelper.MinSecond,
			DateTimeHelper.MinMillisecond, DateTimeHelper.MinMicrosecond,
			DateTimeHelper.MinNanosecond, Type.TimeZone.UTC);
		/// <summary>
		/// Represents the largest possible value of DateTime. This value is in UTC.
		/// </summary>
		public static readonly DateTime MaxValue = new DateTime(
			DateTimeHelper.MaxYear, DateTimeHelper.MaxMonth, DateTimeHelper.MaxDay,
			DateTimeHelper.MaxHour, DateTimeHelper.MaxMinute, DateTimeHelper.MaxSecond,
			DateTimeHelper.MaxMillisecond, DateTimeHelper.MaxMicrosecond,
			DateTimeHelper.MaxNanosecond, Type.TimeZone.UTC);

		/// <summary>
		/// Returns the Type of CurrentTimeZone
		/// </summary>
		private static readonly string m_currentTimeZoneTypeName =
			System.TimeZone.CurrentTimeZone.GetType().FullName;
		/// <summary>
		/// Returns the Type of TimeZone
		/// </summary>
		private static readonly string m_linkmeTypeTimeZoneTypeName = typeof(TimeZone).FullName;

		// m_base is in local time. m_universal is the UTC equivalent. We need store it,
		// because local time is ambiguous during the change from daylight time to standard
		// time (since the same hour occurs twice).
		private readonly System.DateTime m_base;
		private readonly System.DateTime m_universal;
		// The total nanoseconds to add to m_base, ie. including microseconds. Valid values: 0-1000000.
		private readonly int m_nanosecond;
		private readonly System.TimeZone m_timeZone;

		#region Constructors

		/// <summary>
		/// Constructs a DateTimetype
		/// </summary>
		/// <param name="year">A 4-digit number</param>
		/// <param name="month">Month between 1 & 12</param>
		/// <param name="day">Day between 1 & 31</param>
		/// <param name="hour">Hour between 1 & 24</param>
		/// <param name="minute">Minute between 1 & 60</param>
		/// <param name="second">Second between 1 & 60</param>
		/// <param name="millisecond">Millisecond between 0 & 999</param>
		/// <param name="microsecond">Microsecond between 0 & 999</param>
		/// <param name="nanosecond">Nanosecond between 0 & 999</param>
		/// <param name="timeZone">Time Zone for this instance of DateTime</param>
		public DateTime(int year, int month, int day, int hour, int minute, int second,
			int millisecond, int microsecond, int nanosecond, System.TimeZone timeZone)
			: this(new System.DateTime(year, month, day, hour, minute, second, millisecond),
			microsecond, nanosecond, timeZone, false)
		{
		}

		public DateTime(int year, int month, int day, int hour, int minute, int second,
			int millisecond, int microsecond, int nanosecond)
			: this(new System.DateTime(year, month, day, hour, minute, second, millisecond),
			microsecond, nanosecond, System.TimeZone.CurrentTimeZone, false)
		{
		}

		public DateTime(int year, int month, int day, int hour, int minute, int second,
			System.TimeZone timeZone)
			: this(new System.DateTime(year, month, day, hour, minute, second), 0, 0,
			timeZone, false)
		{
		}

		public DateTime(int year, int month, int day, int hour, int minute, int second)
			: this(new System.DateTime(year, month, day, hour, minute, second), 0, 0,
			System.TimeZone.CurrentTimeZone, false)
		{
		}

		public DateTime(int year, int month, int day, System.TimeZone timeZone)
			: this(new System.DateTime(year, month, day), 0, 0, timeZone, false)
		{
		}

		public DateTime(int year, int month, int day)
			: this(new System.DateTime(year, month, day), 0, 0,
			System.TimeZone.CurrentTimeZone, false)
		{
		}

		public DateTime(Date date, TimeOfDay time, System.TimeZone timeZone)
			: this(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second,
			time.Millisecond, time.Microsecond, time.Nanosecond, timeZone)
		{
		}

		public DateTime(Date date, TimeOfDay time)
			: this(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second,
			time.Millisecond, time.Microsecond, time.Nanosecond,
			System.TimeZone.CurrentTimeZone)
		{
		}

		/// <summary>
		/// Constructor for recreating object during de-serialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			m_universal = info.GetDateTime("m_universal");
			m_nanosecond = info.GetInt32("m_nanosecond");

			bool isCurrentTimeZone = info.GetBoolean("isCurrentTimeZone");
			if (isCurrentTimeZone)
			{
				m_timeZone = System.TimeZone.CurrentTimeZone;
			}
			else
			{
				m_timeZone = (System.TimeZone)info.GetValue("m_timeZone", typeof(System.TimeZone));
			}
			Debug.Assert(m_timeZone != null, "m_timeZone != null");

			m_base = m_timeZone.ToLocalTime(m_universal);
		}

		/// <summary>
		/// If <c>isUniversalTime</c> is true then <c>baseValue</c> is taken to represent universal time.
		/// Otherwise it's taken to represent time in the time zone specified by <c>timeZone</c>.
		/// Universal time should be passed in whenever possible to avoid ambiguities
		/// during the hour that occurs twice when changing from daylight time to standard
		/// time.
		/// </summary>
		private DateTime(System.DateTime baseValue, int microsecond, int nanosecond,
			System.TimeZone timeZone, bool isUniversalTime)
			: this(baseValue, microsecond * DateTimeHelper.Microseconds2Nanoseconds + nanosecond,
				timeZone, isUniversalTime)
		{
			const string method = ".ctor";

			// Check that microsecond and nanosecond are valid values - the rest have
			// already been checked by System.DateTime.

			if (microsecond < DateTimeHelper.MinMicrosecond || microsecond > DateTimeHelper.MaxMicrosecond)
				throw new ParameterOutOfRangeException(typeof(DateTime), method,
					"microsecond", microsecond, DateTimeHelper.MinMicrosecond, DateTimeHelper.MaxMicrosecond);
			if (nanosecond < DateTimeHelper.MinNanosecond || nanosecond > DateTimeHelper.MaxNanosecond)
				throw new ParameterOutOfRangeException(typeof(DateTime), method,
					"nanosecond", nanosecond, DateTimeHelper.MinNanosecond, DateTimeHelper.MaxNanosecond);
		}

		// This constructor is for cloning. The nanosecond parameter is the total of microseconds and
		// nanoseconds (ie. 0 to 1000000).
		private DateTime(System.DateTime baseValue, int nanosecond, System.TimeZone timeZone,
			bool isUniversalTime)
		{
			Debug.Assert(nanosecond >= 0 && nanosecond < 1000000, "The nanosecond value is invalid -"
				+ " an exception should be thrown immediately after this.");

			if (isUniversalTime)
			{
				m_universal = baseValue;
				m_base = timeZone.ToLocalTime(baseValue);
			}
			else
			{
				m_base = baseValue;
				m_universal = timeZone.ToUniversalTime(baseValue);
			}

			m_nanosecond = nanosecond;
			m_timeZone = timeZone;
		}

		#endregion

		#region IComparable Members

		/// <summary>
		/// CompareTo method implementation of IComparable for specific implementation
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		int System.IComparable.CompareTo(object other)
		{
			const string method = "IComparable.CompareTo";

			if (!(other is DateTime))
				throw new InvalidParameterTypeException(typeof(DateTime), method, "other", typeof(DateTime), other);

			return Compare(this, (DateTime)other);
		}

		#endregion

		#region IConvertible Members

		System.TypeCode System.IConvertible.GetTypeCode()
		{
			return System.TypeCode.DateTime;
		}

		bool System.IConvertible.ToBoolean(System.IFormatProvider provider)
		{
			return System.Convert.ToBoolean(ToSystemDateTime(), provider);
		}

		char System.IConvertible.ToChar(System.IFormatProvider provider)
		{
			return System.Convert.ToChar(ToSystemDateTime(), provider);
		}

		sbyte System.IConvertible.ToSByte(System.IFormatProvider provider)
		{
			return System.Convert.ToSByte(ToSystemDateTime(), provider);
		}

		byte System.IConvertible.ToByte(System.IFormatProvider provider)
		{
			return System.Convert.ToByte(ToSystemDateTime(), provider);
		}

		short System.IConvertible.ToInt16(System.IFormatProvider provider)
		{
			return System.Convert.ToInt16(ToSystemDateTime(), provider);
		}

		ushort System.IConvertible.ToUInt16(System.IFormatProvider provider)
		{
			return System.Convert.ToUInt16(ToSystemDateTime(), provider);
		}

		int System.IConvertible.ToInt32(System.IFormatProvider provider)
		{
			return System.Convert.ToInt32(ToSystemDateTime(), provider);
		}

		uint System.IConvertible.ToUInt32(System.IFormatProvider provider)
		{
			return System.Convert.ToUInt32(ToSystemDateTime(), provider);
		}

		long System.IConvertible.ToInt64(System.IFormatProvider provider)
		{
			return System.Convert.ToInt64(ToSystemDateTime(), provider);
		}

		ulong System.IConvertible.ToUInt64(System.IFormatProvider provider)
		{
			return System.Convert.ToUInt64(ToSystemDateTime(), provider);
		}

		float System.IConvertible.ToSingle(System.IFormatProvider provider)
		{
			return System.Convert.ToSingle(ToSystemDateTime(), provider);
		}

		double System.IConvertible.ToDouble(System.IFormatProvider provider)
		{
			return System.Convert.ToDouble(ToSystemDateTime(), provider);
		}

		decimal System.IConvertible.ToDecimal(System.IFormatProvider provider)
		{
			return System.Convert.ToDecimal(ToSystemDateTime(), provider);
		}

		System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider provider)
		{
			return ToSystemDateTime();
		}

		string System.IConvertible.ToString(System.IFormatProvider provider)
		{
			return System.Convert.ToString(ToSystemDateTime(), provider);
		}

		object System.IConvertible.ToType(System.Type type, System.IFormatProvider provider)
		{
			return System.Convert.ChangeType(ToSystemDateTime(), type, provider);
		}

		#endregion

		#region ISerializable Members

		/// <summary>
		/// GetObjectData implementation of ISerializable interface
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_universal", Universal);
			info.AddValue("m_nanosecond", m_nanosecond);

			bool isCurrentTimeZone = (TimeZone == System.TimeZone.CurrentTimeZone);
			info.AddValue("isCurrentTimeZone", isCurrentTimeZone);

			if (!isCurrentTimeZone)
			{
				info.AddValue("m_timeZone", TimeZone);
			}
		}

		#endregion

		/// <summary>
		/// Gets a DateTime that is the current local date and time on this computer.
		/// </summary>
		public static DateTime Now
		{
			get { return FromSystemDateTime(System.DateTime.Now, System.TimeZone.CurrentTimeZone); }
		}

		/// <summary>
		/// Gets a DateTime that is the current local date and time on this computer
		/// expressed as the Coordinated Universal Time (UTC).
		/// </summary>
		public static DateTime UtcNow
		{
			get { return FromSystemDateTime(System.DateTime.UtcNow, Type.TimeZone.UTC); }
		}

		#region Access properties

		/// <summary>
		/// Gets year part of the DateTime
		/// </summary>
		public int Year
		{
			get { return m_base.Year; }
		}

		/// <summary>
		/// Gets the month part of the DateTime
		/// </summary>
		public int Month
		{
			get { return m_base.Month; }
		}

		/// <summary>
		/// Get the number of days of this DateTime instance
		/// </summary>
		public int Day
		{
			get { return m_base.Day; }
		}

		/// <summary>
		/// Get the number of hours of this DateTime instance
		/// </summary>
		public int Hour
		{
			get { return m_base.Hour; }
		}

		/// <summary>
		/// Get the number of minutes of this DateTime instance
		/// </summary>
		public int Minute
		{
			get { return m_base.Minute; }
		}

		/// <summary>
		/// Gets the number of seconds of this DateTime instance
		/// </summary>
		public int Second
		{
			get { return m_base.Second; }
		}

		/// <summary>
		/// Gets the number of milliseconds of this DateTime instance
		/// </summary>
		public int Millisecond
		{
			get { return m_base.Millisecond; }
		}

		/// <summary>
		/// Gets the number of micro-seconds of this DateTime instance
		/// </summary>
		public int Microsecond
		{
			get { return m_nanosecond / DateTimeHelper.Microseconds2Nanoseconds; }
		}

		/// <summary>
		/// Gets the number of nano-seconds of this DateTime instance
		/// </summary>
		public int Nanosecond
		{
			get { return m_nanosecond % DateTimeHelper.Microseconds2Nanoseconds; }
		}

		/// <summary>
		/// Gets the day of week of this DateTime instance. FYI: uska belt toot gaya hai
		/// </summary>
		public System.DayOfWeek DayOfWeek
		{
			get { return m_base.DayOfWeek; }
		}

		/// <summary>
		/// Gets the day of year of this DateTime instance
		/// </summary>
		public int DayOfYear
		{
			get { return m_base.DayOfYear; }
		}

		/// <summary>
		/// Gets the timezone of this DateTime instance
		/// </summary>
		public System.TimeZone TimeZone
		{
			get { return (IsInitialised ? m_timeZone : Type.TimeZone.UTC); }
		}

		/// <summary>
		/// Gets the Date object from this instance after removing all time information
		/// </summary>
		public Date Date
		{
			get { return new Date(Year, Month, Day); }
		}

		/// <summary>
		/// Gets the time-of-day from this instance after removing all date information
		/// </summary>
		public TimeOfDay TimeOfDay
		{
			get { return new TimeOfDay(Hour, Minute, Second, Millisecond, Microsecond, Nanosecond); }
		}

		#endregion

		/// <summary>
		/// Returns true if a constructor other than the implicit default constructor was used to create
		/// this instance.
		/// </summary>
		private bool IsInitialised
		{
			// m_timeZone should never be null, except when the default constructor is used.
			get { return (m_timeZone != null); }
		}

		/// <summary>
		/// Private property wrapper for m_universal, which ensures that it's initialised
		/// even when the default constructor is used.
		/// </summary>
		private System.DateTime Universal
		{
			get { return (IsInitialised ? m_universal : TimeZone.ToUniversalTime(m_base)); }
		}

		/// <summary>
		/// A private property that's called by the Visual Studio .NET debugger to display
		/// this object's value in the Watch window. This needs to be configured in
		/// Microsoft Visual Studio .NET 2003\Common7\Packages\Debugger\mcee_cs.dat
		/// </summary>
		private string DebuggerDisplayValue
		{
			get { return ToString(); }
		}

		#region Operators

		// Operator overloading for comparison, equality & addition/subtraction of DateTime

		public static bool operator==(DateTime t1, DateTime t2)
		{
			return Equals(t1, t2);
		}

		public static bool operator!=(DateTime t1, DateTime t2)
		{
			return !Equals(t1, t2);
		}

		public static bool operator<(DateTime t1, DateTime t2)
		{
			return (Compare(t1, t2) < 0);
		}

		public static bool operator>(DateTime t1, DateTime t2)
		{
			return (Compare(t1, t2) > 0);
		}

		public static bool operator<=(DateTime t1, DateTime t2)
		{
			return (Compare(t1, t2) <= 0);
		}

		public static bool operator>=(DateTime t1, DateTime t2)
		{
			return (Compare(t1, t2) >= 0);
		}

		public static DateTime operator+(DateTime t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		public static DateTime operator-(DateTime t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		public static TimeSpan operator-(DateTime t1, DateTime t2)
		{
			return t1.Subtract(t2);
		}

		#endregion

		#region Static methods

		public static int Compare(DateTime t1, DateTime t2)
		{
			// Base value

			int result = System.DateTime.Compare(t1.Universal, t2.Universal);
			if (result == 0)
			{
				result = t1.m_nanosecond - t2.m_nanosecond;
			}

			return System.Math.Sign(result);
		}

		public static bool Equals(DateTime t1, DateTime t2)
		{
			return (Compare(t1, t2) == 0);
		}

		/// <summary>
		/// Returns a DateTime equivalent to the specified System.DateTime. You must also
		/// specify a time zone, because System.DateTime may represent times in the local
		/// time zone as well as in UTC.
		/// </summary>
		public static DateTime FromSystemDateTime(System.DateTime dateTime,
			System.TimeZone timeZone)
		{
			System.DateTime baseValue = new System.DateTime(dateTime.Year,
				dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
				dateTime.Second, dateTime.Millisecond);

			int diffTicks = (int)(dateTime.Ticks - baseValue.Ticks);
			int microseconds = diffTicks / 10;
			int nanoseconds = (diffTicks % 10) * 100;

			return new DateTime(baseValue, microseconds, nanoseconds, timeZone, false);
		}

		public static DateTime Parse(string value)
		{
			const string method = "Parse";

			// Check that there is something there and of the right length.

			if ( value == null )
				throw new NullParameterException(typeof(DateTime), method, "value");

			if ( value.Length < Constants.DateTime.ParseFormatMinLength )
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, Constants.DateTime.ParseFormat);

			// Check for separators in the right places.

			if ( value[2] != '/' || value[5] != '/' || value[10] != ' ' || value[13] != ':' || value[16] != ':' || value[19] != '.' )
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, Constants.DateTime.ParseFormat);

			// Extract each part.

			int year = ParseInt(value, 6, 4, false);
			int month = ParseInt(value, 3, 2, false);
			int day = ParseInt(value, 0, 2, false);
			int hour = ParseInt(value, 11, 2, false);
			int minute = ParseInt(value, 14, 2, false);
			int second = ParseInt(value, 17, 2, false);
			int millisecond = ParseInt(value, 20, 3, false);
			int microsecond = ParseInt(value, 23, 3, false);
			int nanosecond = ParseInt(value, 26, 3, false);

			// Time zone

			if ( value.Length > Constants.DateTime.ParseFormatMinLength )
			{
				if ( value[29] != ' ' )
					throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, Constants.DateTime.ParseFormat);
				Type.TimeZone timeZone = Type.TimeZone.Parse(value.Substring(30));

				return new DateTime(year, month, day, hour, minute, second, millisecond, microsecond, nanosecond, timeZone);
			}
			else
			{
				return new DateTime(year, month, day, hour, minute, second, millisecond, microsecond, nanosecond);
			}
		}

		/// <summary>
		/// Converts the XML representation of a date, time and time zone to a DateTime
		/// object. The input string can be a valid XSD dateTime
		/// (http://www.w3.org/TR/xmlschema-2/#dateTime) OR an XSD dateTime in UTC
		/// (with time zone designator 'Z'), followed by a space and a time zone name or
		/// abbreviation.
		/// </summary>
		/// <remarks>
		/// Valid input examples:
		/// 
		/// "2004-03-24T17:04:00.56Z" (valid XSD dateTime). The resulting DateTime will have
		/// TimeZone set to UTC.
		/// 
		/// "2004-03-24T17:04:00-06:00" (the same DateTime in a UTC-06:00 time zone).
		/// The resulting DateTime will have TimeZone set to a time zone 6 hours behind
		/// UTC with no daylight savings.
		/// 
		/// "2004-03-24T06:04:00Z AEST" (the same DateTime in AEST). The resulting DateTime
		/// will have TimeZone set to AUS Eastern Standard Time.
		/// 
		/// "2004-03-24T06:04:00Z AUS Eastern Standard Time" (same as above with the full
		/// time zone name).
		/// 
		/// "2004-03-24T06:04:00Z +11:00" (the same DateTime in a UTC+11:00 time zone).
		/// </remarks>
		internal static DateTime FromXml(string value)
		{
			const string method = "FromXml";

			// Check that there is something there and of the right length.

			if (value == null)
				throw new NullParameterException(typeof(DateTime), method, "value");

			if (value.Length < DateTimeHelper.DateTimeXmlFormatMinLength)
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

			// Check for separators in the right places.

			if (value[4] != '-' || value[7] != '-' || value[10] != 'T' || value[13] != ':'
				|| value[16] != ':')
			{
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);
			}

			// Extract each part, except for fractional seconds.

			int year = ParseInt(value, 0, 4, true);
			int month = ParseInt(value, 5, 2, true);
			int day = ParseInt(value, 8, 2, true);
			int hour = ParseInt(value, 11, 2, true);
			int minute = ParseInt(value, 14, 2, true);
			int second = ParseInt(value, 17, 2, true);
			int millisecond = 0;
			int microsecond = 0;
			int nanosecond = 0;

			int offset;

			// Fractional seconds

			if (value[19] == '.')
			{
				int index = 20;

				while (index < value.Length && System.Char.IsDigit(value, index))
				{
					index++;
				}

				if (index == 20)
					throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

				string fraction = value.Substring(20, index - 20);

				if (fraction.Length < 9)
				{
					fraction = fraction.PadRight(9, '0');
				}

				millisecond = System.Int32.Parse(fraction.Substring(0, 3));
				microsecond = System.Int32.Parse(fraction.Substring(3, 3));
				nanosecond = System.Int32.Parse(fraction.Substring(6, 3));

				// XSD dateTime type allows arbitrary precision, so extra decimal
				// places are valid. Just ignore them.

				offset = index;
			}
			else
			{
				offset = 19;
			}

			// Time zone

			// A time zone must be specified
			if (value.Length == offset)
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

			if (value[offset] == 'Z')
			{
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second,
					millisecond, microsecond, nanosecond, Type.TimeZone.UTC);

				if (value.Length == offset + 1)
					return dateTime; // Leave the time zone as UTC
				else
				{
					// We should have a space followed by a time zone name (or abbreviation)
					if (value[offset + 1] != ' ')
						throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

					// Convert to the specified time zone

					string timeZoneString = value.Substring(offset + 2);
					if (timeZoneString.Length == 0)
						throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

					Type.TimeZone timeZone = Type.TimeZone.Parse(timeZoneString);

					return dateTime.ToTimeZone(timeZone);
				}
			}
			else
			{
				// Parse the rest of the string as a time zone - it must begin with
				// '+' or '-', a time zone name is not acceptable here.

				if (value[offset] != '+' && value[offset] != '-')
					throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, DateTimeHelper.DateTimeXmlFormat);

				Type.TimeZone timeZone = Type.TimeZone.Parse(value.Substring(offset));

				return new DateTime(year, month, day, hour, minute, second,
					millisecond, microsecond, nanosecond, timeZone);
			}
		}

		internal void Write(System.IO.BinaryWriter writer)
		{
			writer.Write(Year);
			writer.Write(Month);
			writer.Write(Day);
			writer.Write(Hour);
			writer.Write(Minute);
			writer.Write(Second);
			writer.Write(Millisecond);
			writer.Write(Microsecond);
			writer.Write(Nanosecond);

			System.Type timeZoneType = TimeZone.GetType();
			writer.Write(timeZoneType.FullName);

			if (TimeZone != System.TimeZone.CurrentTimeZone)
			{
				TimeZone tz = TimeZone as TimeZone;
				if (tz == null)
					throw new System.InvalidOperationException("The time zone cannot be written to a binary stream, because its"
						+ " type, '" + TimeZone.GetType() + "', is not supported. Only "
						+ m_linkmeTypeTimeZoneTypeName + " and " + m_currentTimeZoneTypeName
						+ " are supported.");

				tz.Write(writer);
			}
		}

		private static int ParseInt(string str, int startIndex, int length, bool xmlFormat)
		{
			const string method = "ParseInt";

			string toParse = str.Substring(startIndex, length);

			foreach (char c in toParse)
			{
				if (!System.Char.IsDigit(c))
					throw new InvalidParameterFormatException(typeof(DateTime), method, "str", str, xmlFormat ? DateTimeHelper.DateTimeXmlFormat : Constants.DateTime.ParseFormat);
			}

			return int.Parse(toParse);
		}

		#endregion

		public override bool Equals(object other)
		{
			if (other is DateTime)
				return Equals(this, (DateTime)other);
			else
				return false;
		}

		public override int GetHashCode()
		{
			// Use the UTC value, so we don't have to hash the time zone
			return Universal.GetHashCode() ^ m_nanosecond.GetHashCode();
		}

		/// <summary>
		/// Returns the String representation of a DateTime
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			// The length of the output string not including the time zone, which is variable
			const int dateTimeLength = Constants.DateTime.ParseFormatMinLength;

			// Specify the exact length of the string as StringBuilder capacity, otherwise the resulting
			// string takes up extra memory (the "left-over" capacity).

			string timeZoneName = Type.TimeZone.ToString(TimeZone);
			StringBuilder sb = new StringBuilder(dateTimeLength + 1 + timeZoneName.Length);

			// Day

			sb.Append(DateTimeHelper.Digits(Day, 2));
			sb.Append('/');

			// Month

			sb.Append(DateTimeHelper.Digits(Month, 2));
			sb.Append('/');

			// Year

			sb.Append(DateTimeHelper.Digits(Year, 4));
			sb.Append(' ');

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

			// Time zone

			sb.Append(' ');
			sb.Append(timeZoneName);
			return sb.ToString();
		}

		#region Time zone conversions

		public DateTime ToTimeZone(System.TimeZone timeZone)
		{
			try
			{
				return new DateTime(Universal, m_nanosecond, timeZone, true);
			}
			catch (System.ArgumentOutOfRangeException ex)
			{
				throw new System.InvalidOperationException("The DateTime '" + ToString() + "' cannot be converted to time zone '"
					+ Type.TimeZone.ToString(timeZone) + "', because the result would be"
					+ " less than DateTime.MinValue or greater than DateTime.MaxValue.", ex);
			}
		}

		public DateTime ToLocalTime()
		{
			return ToTimeZone(System.TimeZone.CurrentTimeZone);
		}

		public DateTime ToUniversalTime()
		{
			try
			{
				return new DateTime(Universal, m_nanosecond, Type.TimeZone.UTC, true);
			}
			catch (System.ArgumentOutOfRangeException ex)
			{
				throw new System.InvalidOperationException("The DateTime '" + ToString() + "' cannot be converted to universal"
					+ " time, because the result would be less than"
					+ " DateTime.MinValue or greater than DateTime.MaxValue.", ex);
			}
		}

		#endregion

		#region Operations

		// All operations must be performed on the UTC value and then converted back to local
		// time - see http://msdn.microsoft.com/library/en-us/dndotnet/html/datetimecode.asp

		public DateTime AddYears(int years)
		{
			DateTime result = new DateTime(Universal.AddYears(years), m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		public DateTime AddMonths(int months)
		{
			DateTime result = new DateTime(Universal.AddMonths(months), m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		// All Add* methods that take a double as input split the value into its whole
		// and fractional parts. The whole part can be added using the corresponding
		// System.DateTime.Add* method, but the fractional part needs to be added using
		// our own method so that microseconds and nanoseconds are updated.

		/// <summary>
		/// Adds days to this instance of DateTime
		/// </summary>
		/// <param name="days"></param>
		/// <returns></returns>
		public DateTime AddDays(double days)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(days);
			double fractional = days - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Days2Nanoseconds);
			result = new DateTime(result.m_base.AddDays(whole), result.m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds hours to this instance of DateTime
		/// </summary>
		/// <param name="hours"></param>
		/// <returns></returns>
		public DateTime AddHours(double hours)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(hours);
			double fractional = hours - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Hours2Nanoseconds);
			result = new DateTime(result.m_base.AddHours(whole), result.m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds minutes to this instance of DateTime
		/// </summary>
		/// <param name="minutes"></param>
		/// <returns></returns>
		public DateTime AddMinutes(double minutes)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(minutes);
			double fractional = minutes - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Minutes2Nanoseconds);
			result = new DateTime(result.m_base.AddMinutes(whole), result.m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds seconds to this instance of DateTime
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public DateTime AddSeconds(double seconds)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(seconds);
			double fractional = seconds - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Seconds2Nanoseconds);
			result = new DateTime(result.m_base.AddSeconds(whole), result.m_nanosecond, Type.TimeZone.UTC, true);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds milliseconds to this instance of DateTime
		/// </summary>
		/// <param name="milliseconds"></param>
		/// <returns></returns>
		public DateTime AddMilliseconds(double milliseconds)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(milliseconds);
			double fractional = milliseconds - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Milliseconds2Nanoseconds);
			result = result.AddMillisecondsInternal(whole);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds microseconds to this instance of DateTime
		/// </summary>
		/// <param name="microseconds"></param>
		/// <returns></returns>
		public DateTime AddMicroseconds(double microseconds)
		{
			DateTime universal = ToUniversalTime();

			double whole = System.Math.Floor(microseconds);
			double fractional = microseconds - whole;

			DateTime result = universal.AddNanosecondsInternal
				(fractional * DateTimeHelper.Microseconds2Nanoseconds);
			result = result.AddMicrosecondsInternal(whole);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds nanoseconds to this instance of DateTime
		/// </summary>
		/// <param name="nanoseconds"></param>
		/// <returns></returns>
		public DateTime AddNanoseconds(double nanoseconds)
		{
			DateTime universal = ToUniversalTime();
			DateTime result = universal.AddNanosecondsInternal(System.Math.Round(nanoseconds));
			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Adds a timespan to this instance of DateTime
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <returns></returns>
		public DateTime Add(TimeSpan timeSpan)
		{
			DateTime result = new DateTime(Universal.Add(timeSpan.ToSystemTimeSpan()), m_nanosecond,
				Type.TimeZone.UTC, true);

			result = result.AddMicrosecondsInternal(timeSpan.Microseconds);
			result = result.AddNanosecondsInternal(timeSpan.Nanoseconds);

			return result.ToTimeZone(TimeZone);
		}

		/// <summary>
		/// Subtracts a timespan from this instance of DateTime
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <returns>DateTime equivalent of the difference</returns>
		public DateTime Subtract(Type.TimeSpan timeSpan)
		{
			return Add(timeSpan.Negate());
		}

		/// <summary>
		/// Subtracts another DateTime from this instance of DateTime
		/// </summary>
		/// <param name="other"></param>
		/// <returns>TimeSpan equivalent of the difference</returns>
		public Type.TimeSpan Subtract(DateTime other)
		{
			Type.TimeSpan result = Universal.Subtract(other.Universal);
			result = result.Add(new Type.TimeSpan(0, 0, 0, 0, 0, 0, m_nanosecond));

			if (other.m_nanosecond == 0)
				return result;
			else
			{
				Type.TimeSpan microNano = new Type.TimeSpan(0, 0, 0, 0, 0, 0, other.m_nanosecond);
				return result.Subtract(microNano);
			}
		}

		public System.DateTime ToSystemDateTime()
		{
			// Add the microseconds and nanoseconds to the returned value as ticks, so that it keeps
			// as much precision as possible.

			return (m_nanosecond == 0 ? m_base :
				m_base.AddTicks(m_nanosecond / DateTimeHelper.Nanoseconds2Ticks));
		}

		#endregion

		/// <summary>
		/// Returns the XML representation of the object in a format similar to XSD dateTime
		/// (http://www.w3.org/TR/xmlschema-2/#dateTime). The returned value is always in UTC.
		/// If xsdCompliant is true then the returned value complies with XSD dateTime
		/// exactly, but time zone information is lost. Otherwise the name of the time zone
		/// is appended to the value, so it is not a valid XSD dateTime.
		/// </summary>
		internal string ToXml(bool xsdCompliant)
		{
			// The length of the output string not including the time zone, which is variable
			const int dateTimeLength = 30;

			// Get the time zone name, unless the user specified that the output should
			// comply with XSD dateTime format or the time zone is, in fact, UTC.

			string timeZoneName = null;
			int outputLength = dateTimeLength;

			if (!xsdCompliant && !TimeZone.Equals(Type.TimeZone.UTC))
			{
				timeZoneName = Type.TimeZone.ToString(TimeZone);
				outputLength += timeZoneName.Length + 1;
			}

			StringBuilder sb = new StringBuilder(outputLength);

			// Year
			sb.Append(DateTimeHelper.Digits(Universal.Year, 4));
			sb.Append('-');

			// Month
			sb.Append(DateTimeHelper.Digits(Universal.Month, 2));
			sb.Append('-');

			// Day
			sb.Append(DateTimeHelper.Digits(Universal.Day, 2));
			sb.Append('T');

			// Hour
			sb.Append(DateTimeHelper.Digits(Universal.Hour, 2));
			sb.Append(':');

			// Minute
			sb.Append(DateTimeHelper.Digits(Universal.Minute, 2));
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
				string ns = nanoseconds.ToString();
				ns = ns.PadLeft(9, '0');
				sb.Append(ns.TrimEnd('0'));
			}

			sb.Append('Z'); // Specifies that this value is in UTC

			if (timeZoneName != null)
			{
				sb.Append(' ');
				sb.Append(timeZoneName);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Read method implementation of IBinarySerializable
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		internal static DateTime Read(System.IO.BinaryReader reader)
		{
			int year = reader.ReadInt32();
			int month = reader.ReadInt32();
			int day = reader.ReadInt32();
			int hour = reader.ReadInt32();
			int minute = reader.ReadInt32();
			int second = reader.ReadInt32();
			int millisecond = reader.ReadInt32();
			int microsecond = reader.ReadInt32();
			int nanosecond = reader.ReadInt32();

			System.TimeZone timeZone;
			string timeZoneType  = reader.ReadString();

			if (timeZoneType == m_currentTimeZoneTypeName)
			{
				timeZone = System.TimeZone.CurrentTimeZone;
			}
			else if (timeZoneType == m_linkmeTypeTimeZoneTypeName)
			{
				timeZone = Type.TimeZone.Read(reader);
			}
			else
				throw new System.InvalidOperationException("The time zone cannot be read from a binary stream, because its"
					+ " type, '" + timeZoneType + "', is not supported. Only "
					+ m_linkmeTypeTimeZoneTypeName + " and " + m_currentTimeZoneTypeName
					+ " are supported.");

			return new DateTime(year, month, day, hour, minute, second, millisecond,
				microsecond, nanosecond, timeZone);
		}

		#region Add*Internal methods

		// Add*Internal methods add values assuming that the DateTime is in UTC and that
		// the values to be added are whole numbers (double type is used instead of int
		// to allow larger values and avoid unnecessary casting).

		/// <summary>
		/// Adds milliseconds to this instance of DateTime
		/// </summary>
		/// <param name="milliseconds"></param>
		/// <returns></returns>
		private DateTime AddMillisecondsInternal(double milliseconds)
		{
			return new DateTime(m_base.AddMilliseconds(milliseconds), m_nanosecond, Type.TimeZone.UTC, true);
		}

		/// <summary>
		/// Adds microseconds to this instance of DateTime
		/// </summary>
		/// <param name="microseconds"></param>
		/// <returns></returns>
		private DateTime AddMicrosecondsInternal(double microseconds)
		{
			double carry;
			int newValue = DateTimeHelper.AddPlaceValues(Microsecond, microseconds,
				DateTimeHelper.Milliseconds2Microseconds, out carry);

			DateTime result = new DateTime(m_base, newValue, Nanosecond, Type.TimeZone.UTC, true);

			if (carry == 0)
				return result;
			else
				return result.AddMillisecondsInternal(carry);
		}

		/// <summary>
		/// Adds nanoseconds to this instance of DateTime
		/// </summary>
		/// <param name="nanoseconds"></param>
		/// <returns></returns>
		private DateTime AddNanosecondsInternal(double nanoseconds)
		{
			double carry;
			int newValue = DateTimeHelper.AddPlaceValues(m_nanosecond, nanoseconds,
				DateTimeHelper.Milliseconds2Nanoseconds, out carry);

			DateTime result = new DateTime(m_base, newValue, Type.TimeZone.UTC, true);

			if (carry == 0)
				return result;
			else
				return result.AddMillisecondsInternal(carry);
		}

		#endregion
	}
}
using System.Runtime.Serialization;
using System.Text;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	[System.Serializable]
	public struct TimeSpan
		:	System.IComparable,
			ISerializable
	{
		// Set the minimum value using the number of ticks equal to the 10675199 days, 2 hours, 48 minutes,
		// 05.477999999 seconds. The absolute value of number ticks in System.TimeSpan.MinValue is 1 greater
		// than in System.TimeSpan.MaxValue, so System.TimeSpan.MinValue.Negate() throws an OverflowException.

		private const long m_maxTicks = 9223372036854770000;

		public static readonly TimeSpan MinValue = new TimeSpan(new System.TimeSpan(-m_maxTicks),
			-DateTimeHelper.MaxMicrosecond, -DateTimeHelper.MaxNanosecond);
		public static readonly TimeSpan Zero = new TimeSpan(System.TimeSpan.Zero, 0, 0);
		public static readonly TimeSpan MaxValue = new TimeSpan(new System.TimeSpan(m_maxTicks),
			DateTimeHelper.MaxMicrosecond, DateTimeHelper.MaxNanosecond);

		private readonly System.TimeSpan m_base;
		private readonly int m_microseconds;
		private readonly int m_nanoseconds;

		#region Constructors

		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds, int microseconds, int nanoseconds)
			: this(new System.TimeSpan(days, hours, minutes, seconds, milliseconds), microseconds, nanoseconds)
		{
		}

		public TimeSpan(int hours, int minutes, int seconds, int milliseconds, int microseconds, int nanoseconds)
			: this(new System.TimeSpan(0, hours, minutes, seconds, milliseconds), microseconds, nanoseconds)
		{
		}

		public TimeSpan(int days, int hours, int minutes, int seconds)
			: this(days, hours, minutes, seconds, 0, 0, 0)
		{
		}

		public TimeSpan(int hours, int minutes, int seconds)
			: this(0, hours, minutes, seconds, 0, 0, 0)
		{
		}

		private TimeSpan(SerializationInfo info, StreamingContext context)
		{
			m_base = (System.TimeSpan)info.GetValue("m_base", typeof(System.TimeSpan));
			m_microseconds = info.GetInt32("m_microseconds");
			m_nanoseconds = info.GetInt32("m_nanoseconds");
		}

		private TimeSpan(System.TimeSpan baseValue, int microseconds, int nanoseconds)
		{
//			const string method = ".ctor";

			// If microsecond value is too large update the milliseconds (same with nanoseconds).
			// This is the behaviour of System.TimeSpan. Store microseconds as Int64 in
			// case the addition of nanoseconds pushes it over Int32.MaxValue.

			long carry = System.Math.DivRem(nanoseconds, DateTimeHelper.Microseconds2Nanoseconds, out nanoseconds);
			long totalMicroseconds = microseconds + carry;
			carry = System.Math.DivRem(totalMicroseconds, (long)DateTimeHelper.Milliseconds2Microseconds,
				out totalMicroseconds);
			microseconds = (int)totalMicroseconds; // We're back in Int32 range

			// Add carried over milliseconds to the base TimeSpan value
//			try
//			{
				baseValue = (carry == 0 ? baseValue : baseValue.Add(
					new System.TimeSpan(0, 0, 0, 0, (int)carry)));
//			}
//			catch (OverflowException)
//			{
//				throw new ParameterOutOfRangeException(typeof(TimeSpan), method,
//					"value", baseValue.ToString() + " + " + carry.ToString() + " ms");
//			}

			// Make sure all parts (base TimeSpan, microseconds, nanoseconds) have the same sign

			int sign = System.Math.Sign(baseValue.TotalMilliseconds);
			if (sign == 0)
			{
				sign = System.Math.Sign(microseconds);
			}

			if (sign != 0)
			{
				if (nanoseconds != 0 && System.Math.Sign(nanoseconds) != sign)
				{
					nanoseconds += DateTimeHelper.Microseconds2Nanoseconds * sign;
					microseconds -= sign;
				}

				if (microseconds != 0 && System.Math.Sign(microseconds) != sign)
				{
					microseconds += DateTimeHelper.Milliseconds2Microseconds * sign;
					baseValue = baseValue.Subtract(new System.TimeSpan(0, 0, 0, 0, sign));
				}
			}

			m_base = baseValue;
			m_microseconds = microseconds;
			m_nanoseconds = nanoseconds;
		}

		#endregion

		#region Operators

		public static bool operator==(TimeSpan t1, TimeSpan t2)
		{
			return Equals(t1, t2);
		}

		public static bool operator!=(TimeSpan t1, TimeSpan t2)
		{
			return !Equals(t1, t2);
		}

		public static bool operator<(TimeSpan t1, TimeSpan t2)
		{
			return (Compare(t1, t2) < 0);
		}

		public static bool operator>(TimeSpan t1, TimeSpan t2)
		{
			return (Compare(t1, t2) > 0);
		}

		public static bool operator<=(TimeSpan t1, TimeSpan t2)
		{
			return (Compare(t1, t2) <= 0);
		}

		public static bool operator>=(TimeSpan t1, TimeSpan t2)
		{
			return (Compare(t1, t2) >= 0);
		}

		public static TimeSpan operator+(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		public static TimeSpan operator-(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		public static TimeSpan operator+(TimeSpan t)
		{
			return t;
		}

		public static TimeSpan operator-(TimeSpan t)
		{
			return t.Negate();
		}

		// Type.TimeSpan has greater precision than System.TimeSpan. Therefore conversion
		// from System.TimeSpan to Type.TimeSpan can be implicit.
		public static implicit operator TimeSpan(System.TimeSpan ts)
		{
			return new TimeSpan(ts, 0, 0);
		}

		#endregion

		#region IComparable Members

		int System.IComparable.CompareTo(object other)
		{
			const string method = "IComparable.CompareTo";

			if (!(other is TimeSpan))
				throw new InvalidParameterTypeException(typeof(TimeSpan), method, "other", typeof(TimeSpan), other);

			return Compare(this, (TimeSpan)other);
		}

		#endregion

		#region ISerializable Members

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_base", m_base);
			info.AddValue("m_microseconds", m_microseconds);
			info.AddValue("m_nanoseconds", m_nanoseconds);
		}

		#endregion

		#region Access properties

		public int Days
		{
			get
			{
				return m_base.Days;
			}
		}

		public int Hours
		{
			get
			{
				return m_base.Hours;
			}
		}

		public int Minutes
		{
			get
			{
				return m_base.Minutes;
			}
		}

		public int Seconds
		{
			get
			{
				return m_base.Seconds;
			}
		}

		public int Milliseconds
		{
			get
			{
				return m_base.Milliseconds;
			}
		}

		public int Microseconds
		{
			get
			{
				return m_microseconds;
			}
		}

		public int Nanoseconds
		{
			get
			{
				return m_nanoseconds;
			}
		}

		// Total* values must be decimal, because double data type doesn't have enough
		// precision to store the maximum possible values (TimeSpan.MaxValue).

		public decimal TotalDays
		{
			get
			{
				return TotalHours / DateTimeHelper.Days2Hours;
			}
		}

		public decimal TotalHours
		{
			get
			{
				return TotalMinutes / DateTimeHelper.Hours2Minutes;
			}
		}

		public decimal TotalMinutes
		{
			get
			{
				return TotalSeconds / DateTimeHelper.Minutes2Seconds;
			}
		}

		public decimal TotalSeconds
		{
			get
			{
				return TotalMilliseconds / DateTimeHelper.Seconds2Milliseconds;
			}
		}

		public decimal TotalMilliseconds
		{
			get
			{
				return (decimal)m_base.TotalMilliseconds
					+ (decimal)m_microseconds / DateTimeHelper.Milliseconds2Microseconds
					+ (decimal)m_nanoseconds / DateTimeHelper.Milliseconds2Nanoseconds;
			}
		}

		public decimal TotalMicroseconds
		{
			get
			{
				return (decimal)m_base.TotalMilliseconds * DateTimeHelper.Milliseconds2Microseconds
					+ m_microseconds + (decimal)m_nanoseconds / DateTimeHelper.Microseconds2Nanoseconds;
			}
		}

		public decimal TotalNanoseconds
		{
			get
			{
				return (decimal)m_base.TotalMilliseconds * DateTimeHelper.Milliseconds2Nanoseconds
					+ m_microseconds * DateTimeHelper.Microseconds2Nanoseconds + m_nanoseconds;
			}
		}

		#endregion

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

		#region Static methods

		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			return System.Decimal.Compare(t1.TotalNanoseconds, t2.TotalNanoseconds);
		}

		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return (Compare(t1, t2) == 0);
		}

		public static TimeSpan Parse(string value)
		{
			const string method = "Parse";

			bool isNegative = false;
			int offset = 0;

			// Check that there is something there.

			if ( value == null )
				throw new NullParameterException(typeof(TimeSpan), method, "value");

			if ( value.Length < Constants.TimeSpan.ParseFormatMinLength )
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, Constants.TimeSpan.ParseFormat);

			// Check for a sign at the beginning.

			if ( value[0] == '-' )
			{
				if ( value[1] != ' ' )
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, Constants.TimeSpan.ParseFormat);
				isNegative = true;
				offset = 2;
			}
			else if ( value[0] == '+' )
			{
				if ( value[1] != ' ' )
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, Constants.TimeSpan.ParseFormat);
				offset = 2;
			}

			// Days

			int index = value.IndexOf(' ', offset);
			if ( index > Constants.TimeSpan.ParseFormatDayLength + offset || index == -1 )
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, Constants.TimeSpan.ParseFormat);

			int days = ParseInt(value, offset, index - offset);
			offset = index;

			// Check for remaining separators now, since the rest of the values are
			// constant in length.

			if ( value[offset + 3] != ':' || value[offset + 6] != ':' || value[offset + 9] != '.' )
				throw new InvalidParameterFormatException(typeof(DateTime), method, "value", value, Constants.TimeSpan.ParseFormat);

			// Check that the string is the right length.

			if ( value.Length != offset + 19 )
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, Constants.TimeSpan.ParseFormat);

			// Extract the rest of the values.

			int hours = ParseInt(value, offset + 1, 2);
			int minutes = ParseInt(value, offset + 4, 2);
			int seconds = ParseInt(value, offset + 7, 2);
			int milliseconds = ParseInt(value, offset + 10, 3);
			int microseconds = ParseInt(value, offset + 13, 3);
			int nanoseconds = ParseInt(value, offset + 16, 3);

			// Set all values

			TimeSpan span = new TimeSpan(days, hours, minutes, seconds, milliseconds, microseconds, nanoseconds);
			return isNegative ? span.Negate() : span;
		}

		/// <summary>
		/// Converts the XML representation of a time span in XSD duration format
		/// (http://www.w3.org/TR/xmlschema-2/#duration) to a TimeSpan object.
		/// </summary>
		internal static TimeSpan FromXml(string value)
		{
			const string method = "FromXml";

			// Check that there is something there.

			if (value == null)
				throw new NullParameterException(typeof(TimeSpan), method, "value");

			if (value.Length < DateTimeHelper.TimeSpanXmlFormatMinLength)
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

			bool isNegative = false;
			int offset = 0;

			// Check for a sign at the beginning.

			if (value[0] == '-')
			{
				isNegative = true;
				offset = 1;
			}

			// Check for duration designator

			if (value[offset] != 'P')
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

			int days = 0;
			int hours = 0;
			int minutes = 0;
			int seconds = 0;
			int milliseconds = 0;
			int microseconds = 0;
			int nanoseconds = 0;

			// Days

			string daysString = GetPlaceString(value, ref offset);

			if (daysString.Length > 0)
			{
				if (value[offset++] != 'D')
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

				try
				{
					days = System.Int32.Parse(daysString);
				}
				catch (System.SystemException)
				{
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);
				}
			}

			// Time

			if (value.Length > offset)
			{
				// Check for time designator

				if (value[offset] != 'T')
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

				// Hours, minutes and seconds

				int placeIndex = 0; // Before hours

				do
				{
					int placeValue;
					try
					{
						placeValue = System.Int32.Parse(GetPlaceString(value, ref offset));
					}
					catch (System.SystemException)
					{
						throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);
					}

					switch (value[offset])
					{
						case 'H':
							if (placeIndex < 1)
							{
								placeIndex = 1; // After hours
							}
							else
								throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

							hours = placeValue;
							break;

						case 'M':
							if (placeIndex < 2)
							{
								placeIndex = 2; // After minutes
							}
							else
								throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

							minutes = placeValue;
							break;

						case 'S':
						case '.':
							if (placeIndex < 3)
							{
								placeIndex = 3; // After seconds
							}
							else
								throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

							seconds = placeValue;

							if (value[offset] == '.')
							{
								// Fractional seconds

								string fraction = GetPlaceString(value, ref offset);
								if (fraction.Length == 0)
									throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);

								if (fraction.Length < 9)
								{
									fraction = fraction.PadRight(9, '0');
								}

								milliseconds = System.Int32.Parse(fraction.Substring(0, 3));
								microseconds = System.Int32.Parse(fraction.Substring(3, 3));
								nanoseconds = System.Int32.Parse(fraction.Substring(6, 3));

								// XSD duration type allows arbitrary precision, so
								// extra decimal places are valid. Just ignore them.

								// We still need an 'S' at the end - check for it

								if (value[offset] != 'S')
									throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);
							}

							break;

						default:
							throw new InvalidParameterFormatException(typeof(TimeSpan), method, "value", value, DateTimeHelper.TimeSpanXmlFormat);
					}
				}
				while (offset != value.Length - 1);
			}

			// Set all values
			TimeSpan span = new TimeSpan(days, hours, minutes, seconds, milliseconds,
				microseconds, nanoseconds);

			return (isNegative ? span.Negate() : span);
		}

		internal static TimeSpan Read(System.IO.BinaryReader reader)
		{
			int days = reader.ReadInt32();
			int hours = reader.ReadInt32();
			int minutes = reader.ReadInt32();
			int seconds = reader.ReadInt32();
			int milliseconds = reader.ReadInt32();
			int microseconds = reader.ReadInt32();
			int nanoseconds = reader.ReadInt32();

			return new TimeSpan(days, hours, minutes, seconds, milliseconds, microseconds,
				nanoseconds);
		}

		private static int ParseInt(string str, int startIndex, int length)
		{
			const string method = "ParseInt";

			string toParse = str.Substring(startIndex, length);

			foreach (char c in toParse)
			{
				if (!System.Char.IsDigit(c))
					throw new InvalidParameterFormatException(typeof(TimeSpan), method, "str", str, Constants.TimeSpan.ParseFormat);
			}

			return int.Parse(toParse);
		}

		private static string GetPlaceString(string str, ref int offset)
		{
			const string method = "GetPlaceString";

			int index = ++offset;

			// Go past all the digits
			while (index < str.Length && System.Char.IsDigit(str, index))
			{
				index++;
			}

			// We should not be at the end of the string
			if (index == str.Length)
				throw new InvalidParameterFormatException(typeof(TimeSpan), method, "str", str, DateTimeHelper.TimeSpanXmlFormat);

			// Parse the digits as an integer
			string place = str.Substring(offset, index - offset);
			offset = index;

			return place;
		}

		#endregion

		public override bool Equals(object other)
		{
			if (!(other is TimeSpan))
				return false;
			return Equals(this, (TimeSpan) other);
		}

		public override int GetHashCode()
		{
			return TotalNanoseconds.GetHashCode();
		}

		public override string ToString()
		{
			// Length of the output not including the days (which is variable) and the optional - sign.
			const int timeLength = Constants.TimeSpan.ParseFormatMinLength - 1;

			string days = System.Math.Abs(Days).ToString();
			int outputLength = timeLength + days.Length;

			StringBuilder sb;

			// Sign

			if (TotalNanoseconds < 0)
			{
				sb = new StringBuilder("- ", outputLength + 2);
			}
			else
			{
				sb = new StringBuilder(outputLength);
			}

			// Days

			sb.Append(days);
			sb.Append(" ");

			// Hours

			sb.Append(DateTimeHelper.Digits(Hours, 2));
			sb.Append(":");

			// Minutes

			sb.Append(DateTimeHelper.Digits(Minutes, 2));
			sb.Append(":");

			// Seconds

			sb.Append(DateTimeHelper.Digits(Seconds, 2));
			sb.Append(".");

			// Milliseconds

			sb.Append(DateTimeHelper.Digits(Milliseconds, 3));

			// Microseconds

			sb.Append(DateTimeHelper.Digits(Microseconds, 3));

			// Nanoseconds

			sb.Append(DateTimeHelper.Digits(Nanoseconds, 3));
			return sb.ToString();
		}

		#region Operations

		public TimeSpan Add(TimeSpan ts)
		{
			return new TimeSpan(m_base.Add(ts.m_base), m_microseconds + ts.m_microseconds,
				m_nanoseconds + ts.m_nanoseconds);
		}

		public TimeSpan Subtract(TimeSpan ts)
		{
			return Add(ts.Negate());
		}

		public TimeSpan Duration()
		{
			return new TimeSpan(m_base.Duration(), System.Math.Abs(m_microseconds), System.Math.Abs(m_nanoseconds));
		}

		public TimeSpan Negate()
		{
			return new TimeSpan(m_base.Negate(), m_microseconds * -1, m_nanoseconds * -1);
		}

		#endregion

		internal System.TimeSpan ToSystemTimeSpan()
		{
			return m_base;
		}

		/// <summary>
		/// Returns the XML representation of the object in XSD duration format
		/// (http://www.w3.org/TR/xmlschema-2/#duration).
		/// </summary>
		internal string ToXml()
		{
			// Length of the output not including the days (which is variable) and the optional - sign.
			const int timeLength = 22;

			if (TotalNanoseconds == 0)
				return "P0D"; // 0 days - the shortest XML representation of duration 0.

			string days = System.Math.Abs(Days).ToString();
			int outputLength = timeLength + days.Length;

			StringBuilder sb;

			// Sign
			if (TotalNanoseconds < 0)
			{
				sb = new StringBuilder("-", outputLength + 1);
			}
			else
			{
				sb = new StringBuilder(outputLength);
			}

			sb.Append('P'); // Duration designator

			// Days
			if (Days != 0)
			{
				sb.Append(days);
				sb.Append('D');
			}

			if (TotalHours % DateTimeHelper.Days2Hours != 0)
			{
				sb.Append('T'); // Time designator

				// Hours
				if (Hours != 0)
				{
					sb.Append(System.Math.Abs(Hours));
					sb.Append('H');
				}

				// Minutes
				if (Minutes != 0)
				{
					sb.Append(System.Math.Abs(Minutes));
					sb.Append('M');
				}

				// Seconds, including decimal digits (there should never be more than 9 places)
				decimal secondsValue = System.Math.Abs(TotalSeconds) % DateTimeHelper.Minutes2Seconds;
				if (secondsValue != 0)
				{
					sb.Append(secondsValue.ToString());
					sb.Append('S');
				}
			}

			return sb.ToString();
		}

		internal void Write(System.IO.BinaryWriter writer)
		{
			writer.Write(Days);
			writer.Write(Hours);
			writer.Write(Minutes);
			writer.Write(Seconds);
			writer.Write(Milliseconds);
			writer.Write(Microseconds);
			writer.Write(Nanoseconds);
		}
	}
}

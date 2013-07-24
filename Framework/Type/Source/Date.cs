using System.Runtime.Serialization;
using System.Text;
using System.IO;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Data type to implement Date structure
	/// </summary>
	[System.Serializable]
	public struct Date
		:	System.IComparable,
		System.IConvertible,
		ISerializable
	{
		/// <summary>
		/// Represents the smallest possible value of Date.
		/// </summary>
		public static readonly Date MinValue = new Date(DateTimeHelper.MinYear,
			DateTimeHelper.MinMonth, DateTimeHelper.MinDay);
		/// <summary>
		/// Represents the largest possible value of Date.
		/// </summary>
		public static readonly Date MaxValue = new Date(DateTimeHelper.MaxYear,
			DateTimeHelper.MaxMonth, DateTimeHelper.MaxDay);
		/// <summary>
		/// Internally delegate the responsibility to .net DateTime 
		/// </summary>
		private readonly System.DateTime m_base;

		#region Constructors

		/// <summary>
		/// Constructs a Date type
		/// </summary>
		/// <param name="year">Year part of the date</param>
		/// <param name="month">Month part of the date</param>
		/// <param name="day">Day part of the date</param>
		public Date(int year, int month, int day)
			: this(new System.DateTime(year, month, day))
		{
		}

		/// <summary>
		/// Specific implementation of constructor used during de-serialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private Date(SerializationInfo info, StreamingContext context)
		{
			m_base = info.GetDateTime("m_base");
		}

		/// <summary>
		/// Constructing Type.Date from System.DateTime, ensuring time is always set to "00:00:00"
		/// </summary>
		/// <param name="baseValue"></param>
		private Date(System.DateTime baseValue)
		{
			// Ensure that time is always set to zero
			m_base = new System.DateTime(baseValue.Year, baseValue.Month, baseValue.Day);
		}

		#endregion

		#region IComparable Members

		/// <summary>
		/// CompareTo method implementation of IComparable interface
		/// </summary>
		/// <param name="other">Another datetime to compare to</param>
		/// <returns></returns>
		int System.IComparable.CompareTo(object other)
		{
			const string method = "IComparable.CompareTo";

			if (!(other is Date))
				throw new InvalidParameterTypeException(typeof(Date), method, "other", typeof(Date), other);

			return Compare(this, (Date)other);
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
		/// GetObjectData method implementation of ISerializable for specific implementation
		/// of .net serialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_base", m_base);
		}

		#endregion

		/// <summary>
		/// Gets a Date that is the current local date on this computer.
		/// </summary>
		public static Date Today
		{
			get
			{
				return new Date(System.DateTime.Today);
			}
		}

		public System.DateTime ToSystemDateTime()
		{
			return new System.DateTime(m_base.Year, m_base.Month, m_base.Day);
		}

		#region Access properties

		/// <summary>
		/// Gets the year part of this instance
		/// </summary>
		public int Year
		{
			get
			{
				return m_base.Year;
			}
		}

		/// <summary>
		/// Gets the month part of this instance
		/// </summary>
		public int Month
		{
			get
			{
				return m_base.Month;
			}
		}

		/// <summary>
		/// Gets the day part of this instance
		/// </summary>
		public int Day
		{
			get
			{
				return m_base.Day;
			}
		}

		/// <summary>
		/// Gets the day of week of this instance. Returns an enum value between 0 to 6 
		/// starting from Sunday
		/// </summary>
		public System.DayOfWeek DayOfWeek
		{
			get
			{
				return m_base.DayOfWeek;
			}
		}

		/// <summary>
		/// Gets the day of year of this instance. It returns an int between 1 to 366
		/// </summary>
		public int DayOfYear
		{
			get
			{
				return m_base.DayOfYear;
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

		#region Operators
		
		// Operator overloads for comparison, equality & addition/subtraction

		public static bool operator==(Date t1, Date t2)
		{
			return Equals(t1, t2);
		}

		public static bool operator!=(Date t1, Date t2)
		{
			return !Equals(t1, t2);
		}

		public static bool operator<(Date t1, Date t2)
		{
			return (Compare(t1, t2) < 0);
		}

		public static bool operator>(Date t1, Date t2)
		{
			return (Compare(t1, t2) > 0);
		}

		public static bool operator<=(Date t1, Date t2)
		{
			return (Compare(t1, t2) <= 0);
		}

		public static bool operator>=(Date t1, Date t2)
		{
			return (Compare(t1, t2) >= 0);
		}

		public static Date operator+(Date t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		public static Date operator-(Date t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		public static TimeSpan operator-(Date t1, Date t2)
		{
			return t1.Subtract(t2);
		}

		#endregion

		#region Static methods

		/// <summary>
		/// Compares the two date literals
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns>Number of days between both dates</returns>
		public static int Compare(Date t1, Date t2)
		{
			return System.DateTime.Compare(t1.m_base, t2.m_base);
		}

		/// <summary>
		/// Checks the equality of two date literals
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns>Returns true if date are same</returns>
		public static bool Equals(Date t1, Date t2)
		{
			return (Compare(t1, t2) == 0);
		}

		/// <summary>
		/// Converts the specified String representation of a date to its Date equivalent
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Date Parse(string value)
		{
			const string method = "Parse";

			// Check that there is something there and of the right length.

			if ( value == null )
				throw new NullParameterException(typeof(Date), method, "value");

			if ( value.Length != Constants.Date.ParseFormatLength )
				throw new InvalidParameterFormatException(typeof(Date), method, "value", value, Constants.Date.ParseFormat);

			// Check for separators in the right places

			if ( value[2] != '/' || value[5] != '/' )
				throw new InvalidParameterFormatException(typeof(Date), method, "value", value, Constants.Date.ParseFormat);

			// Extract each part.

			int year = ParseInt(value, 6, 4, false);
			int month = ParseInt(value, 3, 2, false);
			int day = ParseInt(value, 0, 2, false);
			return new Date(year, month, day);
		}

		/// <summary>
		/// Converts the XML representation of a date in XSD date format
		/// (http://www.w3.org/TR/xmlschema-2/#date) to a Date object.
		/// </summary>
		internal static Date FromXml(string value)
		{
			const string method = "FromXml";

			// Check that there is something there and of the right length.

			if (value == null)
				throw new NullParameterException(typeof(Date), method, "value");

			if (value.Length != DateTimeHelper.DateXmlFormatLength)
				throw new InvalidParameterFormatException(typeof(Date), method, "value", value, DateTimeHelper.DateXmlFormat);

			// Check for separators in the right places.

			if (value[4] != '-' || value[7] != '-')
				throw new InvalidParameterFormatException(typeof(Date), method, "value", value, DateTimeHelper.DateXmlFormat);

			// Extract each part.

			int year = ParseInt(value, 0, 4, true);
			int month = ParseInt(value, 5, 2, true);
			int day = ParseInt(value, 8, 2, true);

			return new Date(year, month, day);
		}

		/// <summary>
		/// Read method implementation of IBinarySerializable interface
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		internal static Date Read(System.IO.BinaryReader reader)
		{
			int year = reader.ReadInt32();
			int month = reader.ReadInt32();
			int day = reader.ReadInt32();

			return new Date(year, month, day);
		}

		/// <summary>
		/// Returns int value of the sub-string of str with startIndex & lenght. 
		/// It throws exception, if the part in subject is not numeric
		/// </summary>
		/// <param name="str"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		/// <param name="xmlFormat"></param>
		/// <returns></returns>
		private static int ParseInt(string str, int startIndex, int length, bool xmlFormat)
		{
			const string method = "ParseInt";

			string toParse = str.Substring(startIndex, length);

			foreach (char c in toParse)
			{
				if (!System.Char.IsDigit(c))
					throw new InvalidParameterFormatException(typeof(Date), method, "str", str, xmlFormat ? DateTimeHelper.DateXmlFormat : Constants.Date.ParseFormat);
			}

			return int.Parse(toParse);
		}

		/// <summary>
		/// Returns the number of days in the specified month of the specified year
		/// </summary>
		/// <param name="year">A 4-digit year</param>
		/// <param name="month">month</param>
		/// <returns>Number of days in that month-year</returns>
		public static int DaysInMonth(int year, int month)
		{
			return System.DateTime.DaysInMonth(year, month);
		}

		/// <summary>
		/// Returns an indication whether the specified year is a leap year
		/// </summary>
		/// <param name="year">A 4-digit year</param>
		/// <returns>true if year is a leap year; otherwise, false</returns>
		public static bool IsLeapYear(int year)
		{
			return System.DateTime.IsLeapYear(year);
		}

		#endregion

		/// <summary>
		/// Returns a value indicating whether an instance of Date 
		/// is equal to a specified object
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if (!(other is Date))
				return false;
			return Equals(this, (Date)other);
		}

		/// <summary>
		/// Returns the hash code for this instance
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return m_base.GetHashCode();
		}

		/// <summary>
		/// Converts the value of this instance to its equivalent String representation
		/// </summary>
		/// <returns>A String representation of value of this instance</returns>
		public override string ToString()
		{
			const int outputLength = Constants.Date.ParseFormatLength;

			StringBuilder sb = new StringBuilder(outputLength);

			// Day

			sb.Append(DateTimeHelper.Digits(m_base.Day, 2));
			sb.Append('/');

			// Month

			sb.Append(DateTimeHelper.Digits(m_base.Month, 2));
			sb.Append('/');

			// Year

			sb.Append(DateTimeHelper.Digits(m_base.Year, 4));
			return sb.ToString();
		}

		#region Operations

		/// <summary>
		/// Adds the specified number of years to the value of this instance
		/// </summary>
		/// <param name="years">A number of years. value can be negative or positive</param>
		/// <returns>A Date whose value is the sum of the date represented 
		/// by this instance and the number of years represented by value</returns>
		public Date AddYears(int years)
		{
			return new Date(m_base.AddYears(years));
		}

		/// <summary>
		/// Adds the specified number of months to the value of this instance
		/// </summary>
		/// <param name="years">A number of months. value can be negative or positive</param>
		/// <returns>A Date whose value is the sum of the date represented 
		/// by this instance and the number of months represented by value</returns>
		public Date AddMonths(int months)
		{
			return new Date(m_base.AddMonths(months));
		}

		/// <summary>
		/// Adds the specified number of days to the value of this instance
		/// </summary>
		/// <param name="years">A number of days. value can be negative or positive</param>
		/// <returns>A Date whose value is the sum of the date represented 
		/// by this instance and the number of days represented by value</returns>
		public Date AddDays(int days)
		{
			return new Date(m_base.AddDays(days));
		}

		/// <summary>
		/// Adds the value of the specified TimeSpan instance to the value of this instance
		/// </summary>
		/// <param name="timeSpan">A TimeSpan that contains the interval to add</param>
		/// <returns>A Date whose value is the sum of the date represented by 
		/// this instance and the time interval (only days) represented by value</returns>
		public Date Add(TimeSpan timeSpan)
		{
			return new Date(m_base.AddDays(timeSpan.Days));
		}

		/// <summary>
		/// Subtracts the specified time or duration from this instance
		/// </summary>
		/// <param name="timeSpan">An interval of time</param>
		/// <returns>A Date equal to the date represented by this instance 
		/// minus the time interval represented by value</returns>
		public Date Subtract(Type.TimeSpan timeSpan)
		{
			return new Date(m_base.AddDays(-timeSpan.Days));
		}

		/// <summary>
		/// Subtracts the specified date from this instance
		/// </summary>
		/// <param name="other">A instance of Date</param>
		/// <returns>A TimeSpan interval equal to the date represented 
		/// by this instance minus the date represented by value</returns>
		public Type.TimeSpan Subtract(Date other)
		{
			return m_base.Subtract(other.m_base);
		}

		#endregion

		/// <summary>
		/// Returns the XML representation of the object in XSD date format
		/// (http://www.w3.org/TR/xmlschema-2/#date).
		/// </summary>
		internal string ToXml()
		{
			const int outputLength = 10;

			StringBuilder sb = new StringBuilder(outputLength);

			// Year
			sb.Append(DateTimeHelper.Digits(Year, 4));
			sb.Append('-');

			// Month
			sb.Append(DateTimeHelper.Digits(Month, 2));
			sb.Append('-');

			// Day
			sb.Append(DateTimeHelper.Digits(Day, 2));

			return sb.ToString();
		}

		/// <summary>
		/// Write method implementation of IBinarySerializable
		/// </summary>
		/// <param name="writer"></param>
		internal void Write(System.IO.BinaryWriter writer)
		{
			writer.Write(Year);
			writer.Write(Month);
			writer.Write(Day);
		}
	}
}

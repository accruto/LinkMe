using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Represents a time zone.
	/// </summary>
	[System.Serializable]
	public class TimeZone
		:	System.TimeZone,
			ISerializable
	{
		/// <summary>
		/// The minimum (farthest behind UTC) supported time zone.
		/// </summary>
		public static readonly TimeZone MinValue = new TimeZone(TimeZoneInfo.MaxBias);
		/// <summary>
		/// The maximum (farthest ahead of UTC) supported time zone.
		/// </summary>
		public static readonly TimeZone MaxValue = new TimeZone(TimeZoneInfo.MinBias);
		/// <summary>
		/// The time zone for Coordinated Universal Time.
		/// </summary>
		public static readonly TimeZone UTC = new TimeZone(TimeZoneInfo.UTC);
		/// <summary>
		/// All time zones defined on the system.
		/// </summary>
		public static readonly TimeZones TimeZones =
			new TimeZones(ConstructFromCollection(TimeZoneInfo.SystemTimeZoneInfos));

		/// <summary>
		/// Points to the TimeZoneInfo class having meta-data for current TimeZone
		/// </summary>
		private readonly TimeZoneInfo m_info;
		private Hashtable m_cachedDaylightChanges = null; // Don't create until needed.

		#region Constructors

		/// <summary>
		/// Returns a time zone for the specified standard time zone name.
		/// </summary>
		public TimeZone(string standardName)
		{
			m_info = TimeZoneInfo.GetTimeZoneInfo(standardName);
		}

		/// <summary>
		/// Returns a time zone with the specified bias and no daylight savings. For example,
		/// TimeZone(-600) will return a UTC+10:00 time zone, the same as
		/// TimeZone("West Pacific Standard Time") - not the same as TimeZone("AUS Eastern Standard Time"),
		/// because the latter has daylight savings.
		/// </summary>
		public TimeZone(int bias)
		{
			const string method = ".ctor";

			if (bias == 0)
			{
				// Special case - return UTC for bias 0
				m_info = TimeZoneInfo.UTC;
			}
			else
			{
				if (bias < DateTimeHelper.MinTimeZone || bias > DateTimeHelper.MaxTimeZone)
					throw new ParameterOutOfRangeException(typeof(TimeZone), method,
						"bias", bias, DateTimeHelper.MinTimeZone, DateTimeHelper.MaxTimeZone);

				m_info = TimeZoneInfo.CreateTimeZoneInfoForBias(bias);
			}
		}

		protected TimeZone(SerializationInfo info, StreamingContext context)
		{
			bool isSystemTimeZone = info.GetBoolean("isSystemTimeZone");
			if (isSystemTimeZone)
			{
				m_info = TimeZoneInfo.GetTimeZoneInfo(info.GetString("timeZoneKey"));
			}
			else
			{
				m_info = TimeZoneInfo.CreateTimeZoneInfoForBias(info.GetInt32("bias"));
			}
		}

		private TimeZone(TimeZoneInfo info)
		{
			m_info = info;
		}

		#endregion

		#region ISerializable Members

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// There is no need to store the whole m_info class. If it's a time zone
			// defined in the system registry we can always look it up by the name. If
			// it was created by the user we can re-create it from the bias. m_info will
			// need to be stored if a constructor is added that allows the user to create
			// a time zone with daylight savings or a custom name.

			info.AddValue("isSystemTimeZone", m_info.IsSystemTimeZone);
			if (m_info.IsSystemTimeZone)
			{
				info.AddValue("timeZoneKey", TimeZoneKey);
			}
			else
			{
				info.AddValue("bias", m_info.Bias);
			}
		}

		#endregion

		/// <summary>
		/// Gets the daylight saving time zone name.
		/// </summary>
		public override string DaylightName
		{
			get
			{
				return m_info.DaylightName;
			}
		}

		/// <summary>
		/// Gets the standard time zone name.
		/// </summary>
		public override string StandardName
		{
			get
			{
				return m_info.StandardName;
			}
		}

		/// <summary>
		/// Gets the Standard TimeZone abbreviation
		/// </summary>
		public string StandardAbbreviation
		{
			get
			{
				return m_info.StandardAbbreviation;
			}
		}

		/// <summary>
		/// Private property used to identify the time zone and to display it in the
		/// debugger watch window.
		/// </summary>
		private string TimeZoneKey
		{
			get
			{
				return (m_info.StandardAbbreviation == null ?
					m_info.StandardName : m_info.StandardAbbreviation);
			}
		}

		#region Static methods

		/// <summary>
		/// Converts the string representation of a time zone, which may be a full name,
		/// abbreviated name or bias in the format "(+|-)hh:mm" to a TimeZone object.
		/// </summary>
		public static TimeZone Parse(string value)
		{
			const string method = "Parse";

			if (value == null)
				throw new NullParameterException(typeof(TimeZone), method, "value");

			if (value.Length == DateTimeHelper.TimeZoneParseFormatLength &&
				(value[0] == '+' || value[0] == '-'))
			{
				// It's a bias expressed as hours and minutes
				return ParseBias(value);
			}
			else
			{
				// It must be a time zone name
				return new TimeZone(value);
			}
		}

		/// <summary>
		/// Returns all time zones with the specified bias.
		/// </summary>
		public static TimeZone[] GetTimeZonesForBias(int bias)
		{
			return ConstructFromCollection(TimeZoneInfo.GetTimeZoneInfos(bias));
		}

		/// <summary>
		/// Returns the abbreviated name of the specified time zone if it has one,
		/// otherwise the full standard name. This method can be used to get a
		/// string representation of System.TimeZone consistent with that of Type.TimeZone.
		/// </summary>
		public static string ToString(System.TimeZone timeZone)
		{
			if (timeZone is TimeZone)
				return timeZone.ToString();

			string abbrev = TimeZoneInfo.FullNameToAbbreviation(timeZone.StandardName);
			return (abbrev == null ? timeZone.StandardName : abbrev);
		}

		/// <summary>
		/// Read method implementation of IBinarySerializable interface
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		internal static TimeZone Read(System.IO.BinaryReader reader)
		{
			bool isSystemTimeZone = reader.ReadBoolean();
			if (isSystemTimeZone)
			{
				string timeZoneKey = reader.ReadString();
				return new TimeZone(TimeZoneInfo.GetTimeZoneInfo(timeZoneKey));
			}
			else
			{
				int bias = reader.ReadInt32();
				return new TimeZone(TimeZoneInfo.CreateTimeZoneInfoForBias(bias));
			}
		}

		/// <summary>
		/// Parses the bias information from String and creates the TimeZone corresponding to it.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static TimeZone ParseBias(string value)
		{
			const string method = "ParseBias";

			// Check for separators in the right places

			if (value[3] != ':')
				throw new InvalidParameterFormatException(typeof(TimeZone), method, "value", value, DateTimeHelper.TimeZoneParseFormat);

			int sign; // The sign of the bias is the opposite of what's in the string
			switch (value[0])
			{
				case '+':
					sign = -1;
					break;

				case '-':
					sign = 1;
					break;

				default:
					throw new InvalidParameterFormatException(typeof(TimeZone), method, "value", value, DateTimeHelper.TimeZoneParseFormat);
			}

			int hours = ParseInt(value, 1, 2);
			int minutes = ParseInt(value, 4, 2);
			int bias = (hours * DateTimeHelper.Hours2Minutes + minutes) * sign;

			return new TimeZone(bias);
		}
	
		/// <summary>
		/// Constructs a TimeZone array out of TimeZoneInfo collection
		/// </summary>
		/// <param name="tzInfoCollection"></param>
		/// <returns></returns>
		private static TimeZone[] ConstructFromCollection(TimeZoneInfos tzInfoCollection)
		{
			TimeZone[] timeZones = new TimeZone[tzInfoCollection.Count];

			int index = 0;
			foreach (TimeZoneInfo tzinfo in tzInfoCollection)
			{
				timeZones[index++] = new TimeZone(tzinfo);
			}

			return timeZones;
		}

		/// <summary>
		/// Gets the changed DateTime
		/// </summary>
		/// <param name="sysInfo"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		private static System.DateTime GetChangeDateTime(SystemTime sysInfo, int year)
		{
			const int DaysInWeek = 7;

			// Initialise to the first day of the month when the change occurs and the
			// correct time.
			System.DateTime change = new System.DateTime(year, sysInfo.wMonth, 1, sysInfo.wHour,
				sysInfo.wMinute, sysInfo.wSecond);

			// Work out the day from wDayOfWeek and wDay: first get to the right day of the
			// week (SystemTime.wDayOfWeek), then add the number of weeks from the start of
			// the month (SystemTime.wDay).
			int daysToAdd = (sysInfo.wDayOfWeek - (int)change.DayOfWeek + DaysInWeek)
				% DaysInWeek + (sysInfo.wDay - 1) * DaysInWeek;
			change = change.AddDays(daysToAdd);

			// If wDay is 5 we might have gone into the next month. In that case go back a week.
			if (sysInfo.wDay == 5 && change.Month != sysInfo.wMonth)
				return change.AddDays(DaysInWeek * -1);
			else
				return change;
		}

		private static int ParseInt(string str, int startIndex, int length)
		{
			const string method = "ParseInt";

			string toParse = str.Substring(startIndex, length);

			foreach (char c in toParse)
			{
				if (!System.Char.IsDigit(c))
					throw new InvalidParameterFormatException(typeof(TimeZone), method, "str", str, DateTimeHelper.TimeZoneParseFormat);
			}

			return int.Parse(toParse);
		}

		#endregion

		/// <summary>
		/// Returns the daylight saving time period for a particular year.
		/// </summary>
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (m_info.DaylightDate.wMonth == 0)
			{
				// No daylight savings in this time zone
				return new DaylightTime(System.DateTime.MinValue, System.DateTime.MinValue,
					System.TimeSpan.Zero);
			}
			else
			{
				// Look up result in the cache.

				DaylightTime result = null;
				if (m_cachedDaylightChanges != null)
				{
					result = (DaylightTime)m_cachedDaylightChanges[year];
				}

				if (result == null)
				{
					// Calculate daylight savings from the stored time zone information
					result = new DaylightTime(GetChangeDateTime(m_info.DaylightDate, year),
						GetChangeDateTime(m_info.StandardDate, year),
						new System.TimeSpan(0, m_info.DaylightBias * -1, 0));

					if (m_cachedDaylightChanges == null)
					{
						m_cachedDaylightChanges = new Hashtable();
					}

					m_cachedDaylightChanges.Add(year, result);
				}

				return result;
			}
		}

		/// <summary>
		/// Returns the coordinated universal time (UTC) offset for the specified local time.
		/// </summary>
		public override System.TimeSpan GetUtcOffset(System.DateTime time)
		{
			// Calculate the bias for the current time (which may vary during daylight savings
			// time) and reverse the sign. ("Bias" for UTC+0100 is -60, for example, but we
			// want to return +1 hour.)

			int bias = m_info.Bias + (IsDaylightSavingTime(time) ?
				m_info.DaylightBias : m_info.StandardBias);

			return new System.TimeSpan(0, bias * -1, 0);
		}

		/// <summary>
		/// Returns the local time that corresponds to a specified coordinated universal
		/// time (UTC).
		/// </summary>
		public override System.DateTime ToLocalTime(System.DateTime time)
		{
			// Add the UTC offset

			System.TimeSpan offsetBefore = GetUtcOffset(time);
			System.DateTime local = time.Add(offsetBefore);

			DaylightTime daylight = GetDaylightChanges(time.Year);

			// Check whether the new time falls into the changeover period between
			// standard time and daylight savings time and adjust accordingly.

			if (local >= daylight.Start && local <= daylight.Start + daylight.Delta)
				return local.Add(daylight.Delta);
			else
			{
				// Get the new offset and if it's different from the original one
				// (which would happen when going from daylight savings time to
				// standard time) adjust accordingly.

				System.TimeSpan offsetAfter = GetUtcOffset(local);
				return local.Add(offsetAfter - offsetBefore);
			}
		}

		/// <summary>
		/// Returns the abbreviated name of the time zone if it has one, otherwise the
		/// full standard name.
		/// </summary>
		public override string ToString()
		{
			return TimeZoneKey;
		}

		/// <summary>
		/// Overridden implementation of the Equals method to provide specific functionality
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			TimeZone other = obj as TimeZone;
			if (other == null)
				return false;
			else
			{
				return m_info.Equals(other.m_info);
			}
		}

		/// <summary>
		/// Overridden implementation of the GetHashCode method to provide specific functionality
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return m_info.GetHashCode();
		}

		/// <summary>
		/// Write method implementation of the IBinarySerializable interface
		/// </summary>
		/// <param name="writer"></param>
		internal virtual void Write(System.IO.BinaryWriter writer)
		{
			writer.Write(m_info.IsSystemTimeZone);
			if (m_info.IsSystemTimeZone)
			{
				writer.Write(TimeZoneKey);
			}
			else
			{
				writer.Write(m_info.Bias);
			}
		}
	}

	/// <summary>
	/// A read-only collection of TimeZone objects.
	/// </summary>
	[System.Serializable]
	public sealed class TimeZones : ICollection
	{
		private TimeZone[] m_values;

		internal TimeZones(TimeZone[] values)
		{
			m_values = values;
		}

		#region ICollection Members

		public int Count
		{
			get { return m_values.Length; }
		}

		public bool IsSynchronized
		{
			get { return m_values.IsSynchronized; }
		}

		public object SyncRoot
		{
			get { return m_values.Length; }
		}

		void ICollection.CopyTo(System.Array array, int index)
		{
			m_values.CopyTo(array, index);
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return m_values.GetEnumerator();
		}

		#endregion

		public TimeZone this[int index]
		{
			get { return m_values[index]; }
		}

		public void CopyTo(TimeZones[] array, int index)
		{
			m_values.CopyTo(array, index);
		}
	}
}

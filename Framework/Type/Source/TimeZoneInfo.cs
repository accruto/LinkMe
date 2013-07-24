using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Win32;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// A simlified version of Win32 SYSTEMTYPE structure
	/// (used only for time zone information in this case).
	/// </summary>
	internal struct SystemTime
	{
		//public readonly short wYear;
		public readonly short wMonth;
		public readonly short wDayOfWeek;
		public readonly short wDay;
		public readonly short wHour;
		public readonly short wMinute;
		public readonly short wSecond;
		//public readonly short wMilliseconds;

		/// <summary>
		/// Create an instance of a class similar to SYSTEMTYPE
		/// </summary>
		/// <param name="month"></param>
		/// <param name="dayOfweek"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		public SystemTime(short month, short dayOfweek, short day, short hour,
			short minute, short second)
		{
			wMonth = month;
			wDayOfWeek = dayOfweek;
			wDay = day;
			wHour = hour;
			wMinute = minute;
			wSecond = second;
		}

		/// <summary>
		/// Overridden the default implementation to provide specific functionality
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is SystemTime))
				return false;

			SystemTime other = (SystemTime)obj;

			return (wMonth == other.wMonth && wDayOfWeek == other.wDayOfWeek
				&& wDay == other.wDay && wHour == other.wHour && wMinute == other.wMinute
				&& wSecond == other.wSecond);
		}

		/// <summary>
		/// Overridden the default implementation to provide specific functionality
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return wMonth.GetHashCode() ^ wDayOfWeek.GetHashCode() ^ wDay.GetHashCode()
				^ wHour.GetHashCode() ^ wMinute.GetHashCode() ^ wSecond.GetHashCode();
		}
	}

	/// <summary>
	/// Class to encapsulate the information pertaining a particular TimeZone
	/// </summary>
	internal class TimeZoneInfo
	{
		public const int MinBias = -840;
		public const int MaxBias = 840;

		public static readonly TimeZoneInfo UTC = new TimeZoneInfo(
			"Coordinated Universal Time", "UTC", 0, true);

		// A dictionary of time zone abbreviations. The abbreviation is the key and the
		// full standard name (as specified in the registry) is the value.
		private static readonly TwoWayStringDictionary m_abbreviations = new TwoWayStringDictionary();
		// Time zones defined in the system registry, keyed by standard name.
		private static TimeZoneInfoNameDictionary m_systemTimeZones = new TimeZoneInfoNameDictionary();
		// A cache of time zones created by the user, keyed by bias.
		private static TimeZoneInfoBiasDictionary m_customTimeZones = new TimeZoneInfoBiasDictionary();

		private readonly int m_bias;
		private readonly string m_standardName;
		private readonly string m_standardAbbreviation = null;
		private readonly SystemTime m_standardDate;
		private readonly int m_standardBias = 0;
		private readonly string m_daylightName;
		private readonly SystemTime m_daylightDate;
		private readonly int m_daylightBias = 0;
		private readonly bool m_isSystemTimeZone;

		#region Constructors

		/// <summary>
		/// Static constructor to load all the supported TimeZones on the system.
		/// </summary>
		static TimeZoneInfo()
		{
			// Time zone abbreviations taken from http://www.timeanddate.com/library/abbreviations/timezones/

			m_abbreviations.Add("ACST", "AUS Central Standard Time");
			m_abbreviations.Add("AEST", "AUS Eastern Standard Time");
			/*
			 * These all need to be checked
			 * 
			m_abbreviations.Add("AKST", "Alaskan Standard Time");
			m_abbreviations.Add("AST", "Atlantic Standard Time");
			m_abbreviations.Add("AWST", "W. Australia Standard Time");
			m_abbreviations.Add("CET", "Central Europe Standard Time");
			m_abbreviations.Add("CST", "Central America Standard Time");
			m_abbreviations.Add("EET", "E. Europe Standard Time");
			m_abbreviations.Add("EST", "Eastern Standard Time");
			m_abbreviations.Add("GMT", "GMT Standard Time");
			m_abbreviations.Add("HAST", "Hawaiian Standard Time");
			m_abbreviations.Add("MST", "Mountain Standard Time");
			m_abbreviations.Add("NST", "Newfoundland Standard Time");
			m_abbreviations.Add("PST", "Pacific Standard Time");
			*/
			m_abbreviations.Add(UTC.StandardAbbreviation, UTC.StandardName);
			/*
			m_abbreviations.Add("WST", "W. Australia Standard Time");
			*/

			// Read all the time zones in the system registry

			ReadAllTimeZones();
		}

		/// <summary>
		/// Constructs a TimeZoneInfo with the specified name and bias and no daylight savings.
		/// </summary>
		private TimeZoneInfo(string name, string abbreviation, int bias, bool isSystemTimeZone)
		{
			m_standardName = name;
			m_standardAbbreviation = abbreviation;
			m_daylightName = name;
			m_bias = bias;
			m_isSystemTimeZone = isSystemTimeZone;
		}

		/// <summary>
		/// Constructs a TimeZoneInfo with the specified parameters
		/// </summary>
		/// <param name="standardName">TimeZone - Standard Name</param>
		/// <param name="daylightName">TimeZone - Daylight Savings Name</param>
		/// <param name="bias">Difference between UTC & current TimeZone</param>
		/// <param name="standardDate">Standard Date</param>
		/// <param name="standardBias">Standard Bias</param>
		/// <param name="daylightDate">Daylight savings Date</param>
		/// <param name="daylightBias">Daylight savings Bias</param>
		private TimeZoneInfo(string standardName, string daylightName, int bias, SystemTime standardDate,
			int standardBias, SystemTime daylightDate, int daylightBias, bool isSystemTimeZone)
		{
			m_standardName = standardName;
			m_daylightName = daylightName;
			m_bias = bias;
			m_standardDate = standardDate;
			m_standardBias = standardBias;
			m_daylightDate = daylightDate;
			m_daylightBias = daylightBias;
			m_isSystemTimeZone = isSystemTimeZone;

			m_standardAbbreviation = FullNameToAbbreviation(standardName);
		}

		#endregion

/*
		// The current implementation always returns objects from a static collection,
		// so that they have reference equality. If this changes (eg. a public constructor
		// is added or the class is made [Serializable] then uncomment the
		// implementations of Equals() and GetHashCode() methods below.

		public override bool Equals(object obj)
		{
			TimeZoneInfo other = obj as TimeZoneInfo;
			if (other == null)
				return false;

			// There is no need to compare the standard abbreviation, because it's
			// always derived from the standard name.

			return (Bias == other.Bias && StandardName == other.StandardName
				&& StandardDate.Equals(other.StandardDate)
				&& StandardBias == other.StandardBias && DaylightName == other.DaylightName
				&& DaylightDate.Equals(other.DaylightDate)
				&& DaylightBias == other.DaylightBias);
		}

		public override int GetHashCode()
		{
			return Bias.GetHashCode() ^ StandardName.GetHashCode()
				^ StandardDate.GetHashCode() ^ StandardBias.GetHashCode()
				^ DaylightName.GetHashCode() ^ DaylightDate.GetHashCode()
				^ DaylightBias.GetHashCode();
		}
*/

		/// <summary>
		/// A collection of TimeZoneInfo structures for every time zone configured on the system.
		/// </summary>
		public static TimeZoneInfos SystemTimeZoneInfos
		{
			get
			{
				return m_systemTimeZones.Values;
			}
		}

		/// <summary>
		/// Constructs a TimeZoneInfo for the specified bias with no daylight savings.
		/// </summary>
		public static TimeZoneInfo CreateTimeZoneInfoForBias(int bias)
		{
			TimeZoneInfo tzinfo = m_customTimeZones[bias];

			if (tzinfo == null)
			{
				tzinfo = new TimeZoneInfo(BiasToString(bias), null, bias, false);
				m_customTimeZones.Add(bias, tzinfo);
			}

			return tzinfo;
		}

		/// <summary>
		/// Returns the time zone for the specified standard name or abbreviation.
		/// Throws an exception if no such time zone is defined.
		/// </summary>
		public static TimeZoneInfo GetTimeZoneInfo(string standardNameOrAbbreviation)
		{
			TimeZoneInfo result = m_systemTimeZones[standardNameOrAbbreviation];

			if (result == null)
			{
				// Try to look up the abbreviation instead
				string name = m_abbreviations[standardNameOrAbbreviation];
				if (name != null)
				{
					result = m_systemTimeZones[name];
				}
			}

			if (result == null)
				throw new System.ApplicationException("Unable to find time zone with standard name"
					+ " or abbreviation '" + standardNameOrAbbreviation + "'.");
			else
				return result;
		}

		/// <summary>
		/// Returns all time zones with the specified bias.
		/// </summary>
		public static TimeZoneInfos GetTimeZoneInfos(int bias)
		{
			TimeZoneInfos results = new TimeZoneInfos();

			foreach (TimeZoneInfo tzinfo in SystemTimeZoneInfos)
			{
				if (tzinfo.Bias == bias)
				{
					results.Add(tzinfo);
				}
			}

			if (results.Count == 0)
				throw new System.ApplicationException("Unable to find time zones with bias '"
					+ bias.ToString() + "'.");

			return results;
		}

		/// <summary>
		/// Returns Abbreviation of the TimeZone with specified Full Name
		/// </summary>
		/// <param name="fullName">Full TimeZone Name</param>
		/// <returns></returns>
		public static string FullNameToAbbreviation(string fullName)
		{
			return m_abbreviations.GetKeyForValue(fullName);
		}

		/// <summary>
		/// Returns the bias information in string format
		/// </summary>
		/// <param name="bias"></param>
		/// <returns></returns>
		public static string BiasToString(int bias)
		{
			int minutes;
			int hours = System.Math.DivRem(System.Math.Abs(bias), DateTimeHelper.Hours2Minutes, out minutes);

			// The displayed (intuitive) sign is the opposite of the bias sign
			string sign = (bias > 0 ? "-" : "+");

			return sign + DateTimeHelper.Digits(hours, 2)
				+ ":" + DateTimeHelper.Digits(minutes, 2);
		}

		/// <summary>
		/// Loads all the TimeZones configured in the Windows Registry.
		/// </summary>
		private static void ReadAllTimeZones()
		{
			try
			{
				m_systemTimeZones.Add(UTC.StandardName, UTC);

				string timeZonesKey;

				switch (System.Environment.OSVersion.Platform)
				{
					case System.PlatformID.Win32NT:
						timeZonesKey = Constants.TimeZone.WinNTKey;
						break;

					case System.PlatformID.Win32Windows:
						timeZonesKey = Constants.TimeZone.Win95Key;
						break;

					default:
						throw new System.ApplicationException("Platform '" + System.Environment.OSVersion.Platform
							+ "' is not supported.");
				}

				using (RegistryKey allTimeZones = Registry.LocalMachine.OpenSubKey(timeZonesKey))
				{
					string[] subkeys = allTimeZones.GetSubKeyNames();
					foreach (string subkey in subkeys)
					{
						using (RegistryKey timeZoneKey = allTimeZones.OpenSubKey(subkey))
						{
							TimeZoneInfo tzinfo = ReadTimeZoneFromRegistryKey(timeZoneKey);
							// Fix: The check is being done to cater to Mexico Standard Time 2 bug in 
							// particular version of Windows XP OS. 
							// Knowledge Base article: http://support.microsoft.com/?kbid=311884
							if (m_systemTimeZones[tzinfo.StandardName] == null)
								m_systemTimeZones.Add(tzinfo.StandardName, tzinfo);
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException("Failed to read time zone definitions from"
					+ " the registry.", ex);
			}
		}

		private static TimeZoneInfo ReadTimeZoneFromRegistryKey(RegistryKey timeZoneKey)
		{
			try
			{
				// Read time zone names (standard and daylight)

				string standardName = (string)timeZoneKey.GetValue("Std");
				if (standardName == null)
					throw new System.ApplicationException("The key does not contain an 'Std' value.");

				string daylightName = (string)timeZoneKey.GetValue("Dlt");
				if (daylightName == null)
					throw new System.ApplicationException("The key does not contain an 'Dlt' value.");

				// Read time zone information - documented in MS KB article Q115231

				object registryValue = timeZoneKey.GetValue("TZI");
				if (registryValue == null)
					throw new System.ApplicationException("The key does not contain a 'TZI' value.");

				// Create a binary reader on the registry value

				Stream stream = new MemoryStream((byte[])registryValue);
				System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);

				// Read values from the binary stream

				int bias = reader.ReadInt32();
				int standardBias = reader.ReadInt32();
				int daylightBias = reader.ReadInt32();
				SystemTime standardDate = ReadSystemTimeFromBinary(reader);
				SystemTime daylightDate = ReadSystemTimeFromBinary(reader);

				stream.Close();

				return new TimeZoneInfo(standardName, daylightName, bias, standardDate,
					standardBias, daylightDate, daylightBias, true);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException("Failed to read time zone data from"
					+ " registry key '" + timeZoneKey.Name + "'.", ex);
			}
		}

		/// <summary>
		/// It provides functionality to BinarySerialize the TimeZone information
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		private static SystemTime ReadSystemTimeFromBinary(System.IO.BinaryReader reader)
		{
			reader.ReadInt16(); // SystemTime.wYear
			SystemTime time = new SystemTime(reader.ReadInt16(), reader.ReadInt16(),
				reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
			reader.ReadInt16(); // SystemTime.wMilliseconds

			return time;
		}

		#region Access properties
		
		/// <summary>
		/// Difference in time between current TimeZoneInfo and UTC
		/// </summary>
		public int Bias
		{
			get { return m_bias; }
		}

		/// <summary>
		/// Standard name of the TimeZone
		/// </summary>
		public string StandardName
		{
			get { return m_standardName; }
		}

		/// <summary>
		/// Standard abbreviation of the TimeZone
		/// </summary>
		public string StandardAbbreviation
		{
			get { return m_standardAbbreviation; }
		}

		/// <summary>
		/// Date for the Standard TimeZone
		/// </summary>
		public SystemTime StandardDate
		{
			get { return m_standardDate; }
		}

		/// <summary>
		/// Bias for the Standard TimeZone
		/// </summary>
		public int StandardBias
		{
			get { return m_standardBias; }
		}

		/// <summary>
		/// Daylight savings name of TimeZone
		/// </summary>
		public string DaylightName
		{
			get { return m_daylightName; }
		}

		/// <summary>
		/// Daylight savings Date of TimeZone
		/// </summary>
		public SystemTime DaylightDate
		{
			get { return m_daylightDate; }
		}

		/// <summary>
		/// Daylight savings Bias of TimeZone
		/// </summary>
		public int DaylightBias
		{
			get { return m_daylightBias; }
		}

		/// <summary>
		/// True if this time zone was defined in the system registry, false if it was created from
		/// information supplied by the user.
		/// </summary>
		public bool IsSystemTimeZone
		{
			get { return m_isSystemTimeZone; }
		}

		#endregion
	}

	/// <summary>
	/// Collection having TimeZone informations
	/// </summary>
	internal class TimeZoneInfos : CollectionBase
	{
		public void Add(TimeZoneInfo value)
		{
			List.Add(value);
		}
	}

	/// <summary>
	/// Dictionary to store TimeZoneInfo objects with key as Timezone name
	/// </summary>
	internal class TimeZoneInfoNameDictionary : DictionaryBase
	{
		public void Add(string name, TimeZoneInfo value)
		{
			Dictionary.Add(name, value);
		}

		public TimeZoneInfo this[string name]
		{
			get { return (TimeZoneInfo)Dictionary[name]; }
		}

		public TimeZoneInfos Values
		{
			get
			{
				TimeZoneInfos values = new TimeZoneInfos();

				foreach (TimeZoneInfo tzinfo in Dictionary.Values)
				{
					values.Add(tzinfo);
				}

				return values;
			}
		}
	}

	/// <summary>
	/// Dictionary to store TimeZoneInfo objects with key as bias
	/// </summary>
	internal class TimeZoneInfoBiasDictionary : DictionaryBase
	{
		public void Add(int bias, TimeZoneInfo value)
		{
			Dictionary.Add(bias, value);
		}

		public TimeZoneInfo this[int bias]
		{
			get { return (TimeZoneInfo)Dictionary[bias]; }
		}
	}

	/// <summary>
	/// Stores pairs of strings so that either of the one of them can be used to look up
	/// the other.
	/// </summary>
	internal class TwoWayStringDictionary : StringDictionary
	{
		private StringDictionary m_reverse = new StringDictionary();

		public override void Add(string key, string value)
		{
			m_reverse.Add(value, key);
			base.Add(key, value);
		}

		public override void Clear()
		{
			m_reverse.Clear();
			base.Clear();
		}

		public override void Remove(string key)
		{
			m_reverse.Remove(this[key]);
			base.Remove(key);
		}

		public string GetKeyForValue(string value)
		{
			return m_reverse[value];
		}
	}
}

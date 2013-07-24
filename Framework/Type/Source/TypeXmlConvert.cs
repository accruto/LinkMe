using System.Xml;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Provides static methods for converting between LinkMe primitive types and XML.
	/// </summary>
	public sealed class TypeXmlConvert
	{
		/// <summary>
		/// Private construtor because it is a helper class with all static methods
		/// </summary>
		private TypeXmlConvert()
		{
		}

		#region Conversions from object to XML

		/// <summary>
		/// Various methods to return Xml (xsd-compliant) format of the primitive type data
		/// </summary>
		/// <param name="value">data to be converted in Xml</param>
		/// <param name="type">primitive type (data type of value)</param>
		/// <returns>String in Xml data format</returns>
		public static string ToString(object value, PrimitiveType type)
		{
			const string method = "ToString";

			// Need to ensure that the value corresponds to the primiteive type before converting it.

			switch ( type )
			{
				case PrimitiveType.String:
					return (string) TypeConvert.ToType(value, type);
				case PrimitiveType.Byte:
					return ToString(TypeConvert.ToByte(value));
				case PrimitiveType.Int16:
					return ToString(TypeConvert.ToInt16(value));
				case PrimitiveType.Int32:
					return ToString(TypeConvert.ToInt32(value));
				case PrimitiveType.Int64:
					return ToString(TypeConvert.ToInt64(value));
				case PrimitiveType.Single:
					return ToString(TypeConvert.ToSingle(value));
				case PrimitiveType.Double:
					return ToString(TypeConvert.ToDouble(value));
				case PrimitiveType.Decimal:
					return ToString(TypeConvert.ToDecimal(value));
				case PrimitiveType.Boolean:
					return ToString(TypeConvert.ToBoolean(value));
				case PrimitiveType.Guid:
					return ToString(TypeConvert.ToGuid(value));
				case PrimitiveType.Date:
					return ToString(TypeConvert.ToDate(value));
				case PrimitiveType.DateTime:
					return ToString(TypeConvert.ToDateTime(value));
				case PrimitiveType.TimeOfDay:
					return ToString(TypeConvert.ToTimeOfDay(value));
				case PrimitiveType.TimeSpan:
					return ToString(TypeConvert.ToTimeSpan(value));
				default:
					throw new InvalidParameterValueException(typeof(XmlConvert), method, "type", typeof(PrimitiveType), type);
			}
		}

		public static string ToString(object value)
		{
			const string method = "ToString";

			if ( value == null )
				throw new NullParameterException(typeof(TypeXmlConvert), method, "value");

			if ( value is string )
				return (string) value;
			if ( value is byte )
				return ToString((byte) value);
			if ( value is short )
				return ToString((short) value);
			if ( value is int )
				return ToString((int) value);
			if ( value is long )
				return ToString((long) value);
			if ( value is float )
				return ToString((float) value);
			if ( value is double )
				return ToString((double) value);
			if ( value is decimal )
				return ToString((decimal) value);
			if ( value is bool )
				return ToString((bool) value);
			if ( value is System.Guid )
				return ToString((System.Guid) value);
			if ( value is Date )
				return ToString((Date) value);
			if ( value is DateTime )
				return ToString((DateTime) value);
			if ( value is TimeOfDay )
				return ToString((TimeOfDay) value);
			if ( value is TimeSpan )
				return ToString((TimeSpan) value);
			else
				throw new InvalidParameterValueException(typeof(XmlConvert), method, "value", value.GetType(), value);
		}

		public static string ToString(byte value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(short value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(int value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(long value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(float value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(double value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(decimal value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(bool value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(System.Guid value)
		{
			return System.Xml.XmlConvert.ToString(value);
		}

		public static string ToString(Date value)
		{
			return value.ToXml();
		}

		public static string ToString(DateTime value)
		{
			return value.ToXml(false);
		}

		public static string ToString(DateTime value, bool xsdCompliant)
		{
			return value.ToXml(xsdCompliant);
		}

		public static string ToString(TimeOfDay value)
		{
			return value.ToXml();
		}

		public static string ToString(TimeSpan value)
		{
			return value.ToXml();
		}

		#endregion

		#region Conversions from XML to object

		public static bool CanConvert(string value, PrimitiveType toType)
		{
			if ( value == null )
				return false;

			// Try to do the conversion
			
			try
			{
				ToType(value, toType);
				return true;
			}
			catch ( System.Exception )
			{
				return false;
			}
		}

		/// <summary>
		/// Helper method for conversion of xml data into primitive type
		/// </summary>
		/// <param name="value"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object ToType(string value, PrimitiveType type)
		{
			const string method = "ToType";

			if ( value == null )
				return null;

			switch (type)
			{
				case PrimitiveType.String:
					return value;
				case PrimitiveType.Byte:
					return ToByte(value);
				case PrimitiveType.Int16:
					return ToInt16(value);
				case PrimitiveType.Int32:
					return ToInt32(value);
				case PrimitiveType.Int64:
					return ToInt64(value);
				case PrimitiveType.Single:
					return ToSingle(value);
				case PrimitiveType.Double:
					return ToDouble(value);
				case PrimitiveType.Decimal:
					return ToDecimal(value);
				case PrimitiveType.Boolean:
					return ToBoolean(value);
				case PrimitiveType.Guid:
					return ToGuid(value);
				case PrimitiveType.Date:
					return ToDate(value);
				case PrimitiveType.DateTime:
					return ToDateTime(value);
				case PrimitiveType.TimeOfDay:
					return ToTimeOfDay(value);
				case PrimitiveType.TimeSpan:
					return ToTimeSpan(value);
				default:
					throw new InvalidParameterValueException(typeof(XmlConvert), method, "type", typeof(PrimitiveType), type);
			}
		}

		public static byte ToByte(string value)
		{
			return System.Xml.XmlConvert.ToByte(value);
		}

		public static short ToInt16(string value)
		{
			return System.Xml.XmlConvert.ToInt16(value);
		}

		public static int ToInt32(string value)
		{
			return System.Xml.XmlConvert.ToInt32(value);
		}

		public static long ToInt64(string value)
		{
			return System.Xml.XmlConvert.ToInt64(value);
		}

		public static float ToSingle(string value)
		{
			return System.Xml.XmlConvert.ToSingle(value);
		}

		public static double ToDouble(string value)
		{
			return System.Xml.XmlConvert.ToDouble(value);
		}

		public static decimal ToDecimal(string value)
		{
			return System.Xml.XmlConvert.ToDecimal(value);
		}

		public static bool ToBoolean(string value)
		{
			return System.Xml.XmlConvert.ToBoolean(value);
		}

		public static System.Guid ToGuid(string value)
		{
			return System.Xml.XmlConvert.ToGuid(value);
		}

		public static Date ToDate(string value)
		{
			return Date.FromXml(value);
		}

		public static DateTime ToDateTime(string value)
		{
			return DateTime.FromXml(value);
		}

		public static TimeOfDay ToTimeOfDay(string value)
		{
			return TimeOfDay.FromXml(value);
		}

		public static TimeSpan ToTimeSpan(string value)
		{
			return TimeSpan.FromXml(value);
		}

		#endregion
	}
}
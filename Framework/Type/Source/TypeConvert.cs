using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Provides static methods for converting between primitive types and checking
	/// whether an explicit or implicit conversion between the specified types is possible.
	/// </summary>
	public sealed class TypeConvert
	{
		private TypeConvert()
		{
		}

		#region CanConvert

		public static bool CanConvert(PrimitiveType fromType, PrimitiveType toType)
		{
			return CanConvertImplicitly(fromType, toType)
				|| CanConvertExplicitly(fromType, toType);
		}

		public static bool CanConvert(object value, PrimitiveType toType)
		{
			if ( value == null )
				return false;
			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(value.GetType());
			if ( !CanConvert(primitiveTypeInfo.PrimitiveType, toType) )
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
		/// To verify whether conversion is possible implicity between the two provided primitive types
		/// </summary>
		/// <param name="fromType">Conversion from primitive type</param>
		/// <param name="toType">Conversion to primitive type</param>
		/// <returns>true for implicit conversion possible, else false.</returns>
		public static bool CanConvertImplicitly(PrimitiveType fromType, PrimitiveType toType)
		{
			if ( fromType == toType )
				return true;

			switch ( fromType )
			{
				case PrimitiveType.Byte:
					return toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Int16:
					return toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Int32:
					return toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Int64:
					return toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Single:
					return toType == PrimitiveType.Double;

				default:
					return false;
			}
		}

		/// <summary>
		/// To verify whether conversion is possible explicity between the two provided primitive types
		/// </summary>
		/// <param name="fromType">Conversion from primitive type</param>
		/// <param name="toType">Conversion to primitive type</param>
		/// <returns>true for explicit conversion possible, else false.</returns>
		public static bool CanConvertExplicitly(PrimitiveType fromType, PrimitiveType toType)
		{
			if ( fromType == PrimitiveType.String ^ toType == PrimitiveType.String )
				return true;

			switch ( fromType )
			{
				case PrimitiveType.Byte:
					return toType == PrimitiveType.Boolean;

				case PrimitiveType.Int16:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte;

				case PrimitiveType.Int32:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16;

				case PrimitiveType.Int64:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32;

				case PrimitiveType.Single:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Double:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Decimal;

				case PrimitiveType.Decimal:
					return toType == PrimitiveType.Boolean
						|| toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double;

				case PrimitiveType.Date:
					return toType == PrimitiveType.DateTime;

				case PrimitiveType.DateTime:
					return toType == PrimitiveType.Date
						|| toType == PrimitiveType.TimeOfDay;

				case PrimitiveType.Boolean:
					return toType == PrimitiveType.Byte
						|| toType == PrimitiveType.Int16
						|| toType == PrimitiveType.Int32
						|| toType == PrimitiveType.Int64
						|| toType == PrimitiveType.Single
						|| toType == PrimitiveType.Double
						|| toType == PrimitiveType.Decimal;

				default:
					return false;
			}
		}

		#endregion

		#region ToType
		/// <summary>
		/// Converts the value into the provided primitive type
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <param name="type">primitive type to which value is to be converted</param>
		/// <returns>Returns primitive type with the supplied value. 
		/// Returns null if the value was null. Throws error if value cannot be not
		/// compatible with the primitive.</returns>
		public static object ToType(object value, PrimitiveType type)
		{
			const string method = "ToType";

			if ( value == null )
				return null;

			switch ( type )
			{
				case PrimitiveType.String:
					return ToString(value);
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
					throw new InvalidParameterValueException(typeof(TypeConvert), method, "type", typeof(PrimitiveType), type);
			}
		}

		#endregion

		#region ToString

		/// <summary>
		/// Various overloaded methods to convert any primitive type to string
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>String equivalent of the value</returns>
		public static string ToString(byte value)
		{
			return value.ToString();
		}

		public static string ToString(short value)
		{
			return value.ToString();
		}

		public static string ToString(int value)
		{
			return value.ToString();
		}

		public static string ToString(long value)
		{
			return value.ToString();
		}

		public static string ToString(float value)
		{
			return value.ToString();
		}

		public static string ToString(double value)
		{
			return value.ToString();
		}

		public static string ToString(decimal value)
		{
			return value.ToString();
		}

		public static string ToString(Date value)
		{
			return value.ToString();
		}

		public static string ToString(DateTime value)
		{
			return value.ToString();
		}
	
		public static string ToString(TimeOfDay value)
		{
			return value.ToString();
		}
	
		public static string ToString(TimeSpan value)
		{
			return value.ToString();
		}

		public static string ToString(System.Guid value)
		{
			return value.ToString();
		}

		public static string ToString(bool value)
		{
			return value.ToString();
		}

		public static string ToString(object value)
		{
			const string method = "ToString";

            if (value == null)
                return null; //throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return (System.String) value;
			if ( value is System.Byte )
				return ToString((System.Byte) value);
			if ( value is System.Int16 )
				return ToString((System.Int16) value);
			if ( value is System.Int32 )
				return ToString((System.Int32) value);
			if ( value is System.Int64 )
				return ToString((System.Int64) value);
			if ( value is System.Single )
				return ToString((System.Single) value);
			if ( value is System.Double )
				return ToString((System.Double) value);
			if ( value is System.Decimal )
				return ToString((System.Decimal) value);
			if ( value is System.Boolean )
				return ToString((System.Boolean) value);
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
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToByte

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to byte
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Byte equivalent of the value</returns>
		public static byte ToByte(short value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(int value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(long value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(float value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(double value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(decimal value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(bool value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(string value)
		{
			return System.Convert.ToByte(value);
		}

		public static byte ToByte(object value)
		{
			const string method = "ToByte";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToByte((System.String) value);
			if ( value is System.Byte )
				return (System.Byte) value;
			if ( value is System.Int16 )
				return ToByte((System.Int16) value);
			if ( value is System.Int32 )
				return ToByte((System.Int32) value);
			if ( value is System.Int64 )
				return ToByte((System.Int64) value);
			if ( value is System.Single )
				return ToByte((System.Single) value);
			if ( value is System.Double )
				return ToByte((System.Double) value);
			if ( value is System.Decimal )
				return ToByte((System.Decimal) value);
			if ( value is System.Boolean )
				return ToByte((System.Boolean) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToInt16

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Int16
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Int16 equivalent of the value</returns>
		public static short ToInt16(int value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(long value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(float value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(double value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(decimal value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(bool value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(string value)
		{
			return System.Convert.ToInt16(value);
		}

		public static short ToInt16(byte value)
		{
			return value;
		}

		public static short ToInt16(System.Enum value)
		{
			const string method = "ToInt16";

			System.Type underlyingType = System.Enum.GetUnderlyingType(value.GetType());
			if ( underlyingType == typeof(System.Int16) )
				return (System.Int16) (System.Object) value;
			else if ( underlyingType == typeof(System.Int32) )
				return (System.Int16) (System.Int32) (System.Object) value;
			else if ( underlyingType == typeof(System.Int64) )
				return (System.Int16) (System.Int64) (System.Object) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		public static short ToInt16(object value)
		{
			const string method = "ToInt16";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToInt16((string) value);
			if ( value is System.Byte )
				return ToInt16((System.Byte) value);
			if ( value is System.Int16 )
				return (System.Int16) value;
			if ( value is System.Int32 )
				return ToInt16((System.Int32) value);
			if ( value is System.Int64 )
				return ToInt16((System.Int64) value);
			if ( value is System.Single )
				return ToInt16((System.Single) value);
			if ( value is System.Double )
				return ToInt16((System.Double) value);
			if ( value is System.Decimal )
				return ToInt16((System.Decimal) value);
			if ( value is System.Boolean )
				return ToInt16((System.Boolean) value);
			if ( value is System.Enum )
				return ToInt16((System.Enum) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToInt32

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Int32
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Int32 equivalent of the value</returns>
		public static int ToInt32(long value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(float value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(double value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(decimal value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(bool value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(string value)
		{
			return System.Convert.ToInt32(value);
		}

		public static int ToInt32(byte value)
		{
			return value;
		}

		public static int ToInt32(short value)
		{
			return value;
		}

		public static int ToInt32(System.Enum value)
		{
			const string method = "ToInt32";

			System.Type underlyingType = System.Enum.GetUnderlyingType(value.GetType());
			if ( underlyingType == typeof(System.Int16) )
				return (System.Int32) (System.Int16) (System.Object) value;
			else if ( underlyingType == typeof(System.Int32) )
				return (System.Int32) (System.Object) value;
			else if ( underlyingType == typeof(System.Int64) )
				return (System.Int32) (System.Int64) (System.Object) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		public static int ToInt32(object value)
		{
			const string method = "ToInt32";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToInt32((System.String) value);
			if ( value is System.Byte )
				return ToInt32((System.Byte) value);
			if ( value is System.Int16 )
				return ToInt32((System.Int16) value);
			if ( value is System.Int32 )
				return (System.Int32) value;
			if ( value is System.Int64 )
				return ToInt32((System.Int64) value);
			if ( value is System.Single )
				return ToInt32((System.Single) value);
			if ( value is System.Double )
				return ToInt32((System.Double) value);
			if ( value is System.Decimal )
				return ToInt32((System.Decimal) value);
			if ( value is System.Boolean )
				return ToInt32((System.Boolean) value);
			if ( value is System.Enum )
				return ToInt32((System.Enum) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToInt64

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Int64
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Int64 equivalent of the value</returns>
		public static long ToInt64(float value)
		{
			return System.Convert.ToInt64(value);
		}

		public static long ToInt64(double value)
		{
			return System.Convert.ToInt64(value);
		}

		public static long ToInt64(decimal value)
		{
			return System.Convert.ToInt64(value);
		}

		public static long ToInt64(bool value)
		{
			return System.Convert.ToInt64(value);
		}

		public static long ToInt64(string value)
		{
			return System.Convert.ToInt64(value);
		}

		public static long ToInt64(byte value)
		{
			return value;
		}

		public static long ToInt64(short value)
		{
			return value;
		}

		public static long ToInt64(int value)
		{
			return value;
		}

		public static long ToInt64(System.Enum value)
		{
			const string method = "ToInt64";

			System.Type underlyingType = System.Enum.GetUnderlyingType(value.GetType());
			if ( underlyingType == typeof(System.Int16) )
				return (System.Int64) (System.Int16) (System.Object) value;
			else if ( underlyingType == typeof(System.Int32) )
				return (System.Int64) (System.Int32) (System.Object) value;
			else if ( underlyingType == typeof(System.Int64) )
				return (System.Int64) (System.Object) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		public static long ToInt64(object value)
		{
			const string method = "ToInt64";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToInt64((System.String) value);
			if ( value is System.Byte )
				return ToInt64((System.Byte) value);
			if ( value is System.Int16 )
				return ToInt64((System.Int16) value);
			if ( value is System.Int32 )
				return ToInt64((System.Int32) value);
			if ( value is System.Int64 )
				return (System.Int64) value;
			if ( value is System.Single )
				return ToInt64((System.Single) value);
			if ( value is System.Double )
				return ToInt64((System.Double) value);
			if ( value is System.Decimal )
				return ToInt64((System.Decimal) value);
			if ( value is System.Boolean )
				return ToInt64((System.Boolean) value);
			if ( value is System.Enum )
				return ToInt64((System.Enum) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToSingle

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Single
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Single equivalent of the value</returns>
		public static float ToSingle(double value)
		{
			return System.Convert.ToSingle(value);
		}

		public static float ToSingle(decimal value)
		{
			return System.Convert.ToSingle(value);
		}

		public static float ToSingle(bool value)
		{
			return System.Convert.ToSingle(value);
		}

		public static float ToSingle(string value)
		{
			return System.Convert.ToSingle(value);
		}

		public static float ToSingle(byte value)
		{
			return value;
		}

		public static float ToSingle(short value)
		{
			return value;
		}

		public static float ToSingle(int value)
		{
			return value;
		}

		public static float ToSingle(long value)
		{
			return value;
		}

		public static float ToSingle(object value)
		{
			const string method = "ToSingle";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToSingle((System.String) value);
			if ( value is System.Byte )
				return ToSingle((System.Byte) value);
			if ( value is System.Int16 )
				return ToSingle((System.Int16) value);
			if ( value is System.Int32 )
				return ToSingle((System.Int32) value);
			if ( value is System.Int64 )
				return ToSingle((System.Int64) value);
			if ( value is System.Single )
				return (System.Single) value;
			if ( value is System.Double )
				return ToSingle((System.Double) value);
			if ( value is System.Decimal )
				return ToSingle((System.Decimal) value);
			if ( value is System.Boolean )
				return ToSingle((System.Boolean) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToDouble

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Double
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Double equivalent of the value</returns>
		public static double ToDouble(decimal value)
		{
			return System.Convert.ToDouble(value);
		}

		public static double ToDouble(bool value)
		{
			return System.Convert.ToDouble(value);
		}

		public static double ToDouble(string value)
		{
			return System.Convert.ToDouble(value);
		}

		public static double ToDouble(byte value)
		{
			return value;
		}

		public static double ToDouble(short value)
		{
			return value;
		}

		public static double ToDouble(int value)
		{
			return value;
		}

		public static double ToDouble(long value)
		{
			return value;
		}

		public static double ToDouble(float value)
		{
			return value;
		}

		public static double ToDouble(object value)
		{
			const string method = "ToDouble";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToDouble((System.String) value);
			if ( value is System.Byte )
				return ToDouble((System.Byte) value);
			if ( value is System.Int16 )
				return ToDouble((System.Int16) value);
			if ( value is System.Int32 )
				return ToDouble((System.Int32) value);
			if ( value is System.Int64 )
				return ToDouble((System.Int64) value);
			if ( value is System.Single )
				return ToDouble((System.Single) value);
			if ( value is System.Double )
				return (System.Double) value;
			if ( value is System.Decimal )
				return ToDouble((System.Decimal) value);
			if ( value is System.Boolean )
				return ToDouble((System.Boolean) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToDecimal

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Decimal
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Decimal equivalent of the value</returns>
		public static decimal ToDecimal(float value)
		{
			return System.Convert.ToDecimal(value);
		}

		public static decimal ToDecimal(double value)
		{
			return System.Convert.ToDecimal(value);
		}

		public static decimal ToDecimal(bool value)
		{
			return System.Convert.ToDecimal(value);
		}

		public static decimal ToDecimal(string value)
		{
			return System.Convert.ToDecimal(value);
		}

		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		public static decimal ToDecimal(short value)
		{
			return value;
		}

		public static decimal ToDecimal(int value)
		{
			return value;
		}

		public static decimal ToDecimal(long value)
		{
			return value;
		}

		public static decimal ToDecimal(object value)
		{
			const string method = "ToDecimal";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToDecimal((System.String) value);
			if ( value is System.Byte )
				return ToDecimal((System.Byte) value);
			if ( value is System.Int16 )
				return ToDecimal((System.Int16) value);
			if ( value is System.Int32 )
				return ToDecimal((System.Int32) value);
			if ( value is System.Int64 )
				return ToDecimal((System.Int64) value);
			if ( value is System.Single )
				return ToDecimal((System.Single) value);
			if ( value is System.Double )
				return ToDecimal((System.Double) value);
			if ( value is System.Decimal )
				return (System.Decimal) value;
			if ( value is System.Boolean )
				return ToDecimal((System.Boolean) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToDate

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Date
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Type.Date equivalent of the value</returns>
		public static Date ToDate(DateTime value)
		{
			return value.Date;
		}

		public static Date ToDate(string value)
		{
			return Date.Parse(value);
		}

		public static Date ToDate(object value)
		{
			const string method = "ToDate";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToDate((System.String) value);
			if ( value is Date )
				return (Date) value;
			if ( value is DateTime )
				return ToDate((DateTime) value);
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToDateTime

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to DateTiem
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Type.DateTime equivalent of the value</returns>
		public static DateTime ToDateTime(Date value)
		{
			return new DateTime(value, new TimeOfDay(0, 0, 0));
		}

		public static DateTime ToDateTime(string value)
		{
			return DateTime.Parse(value);
		}

		public static DateTime ToDateTime(System.DateTime value)
		{
			return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, 0, 0);
		}

		public static DateTime ToDateTime(object value)
		{
			const string method = "ToDateTime";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToDateTime((System.String) value);
			if ( value is Date )
				return ToDateTime((Date) value);
			if ( value is System.DateTime )
				return ToDateTime((System.DateTime) value);
			if ( value is DateTime )
				return (DateTime) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}
	
		#endregion

		#region ToTimeOfDay

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to TimeOfDay
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>TimeOfDay equivalent of the value</returns>
		public static TimeOfDay ToTimeOfDay(DateTime value)
		{
			return value.TimeOfDay;
		}

		public static TimeOfDay ToTimeOfDay(string value)
		{
			return TimeOfDay.Parse(value);
		}

		public static TimeOfDay ToTimeOfDay(object value)
		{
			const string method = "ToTimeOfDay";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToTimeOfDay((System.String) value);
			if ( value is DateTime )
				return ToTimeOfDay((DateTime) value);
			if ( value is TimeOfDay )
				return (TimeOfDay) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToTimeSpan

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to TimeSpan
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>TimeSpan equivalent of the value</returns>
		public static TimeSpan ToTimeSpan(string value)
		{
			return TimeSpan.Parse(value);
		}

		public static TimeSpan ToTimeSpan(object value)
		{
			const string method = "ToTimeSpan";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToTimeSpan((System.String) value);
			if ( value is TimeSpan )
				return (TimeSpan) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToGuid

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Guid
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Guid equivalent of the value</returns>
		public static System.Guid ToGuid(string value)
		{
			return new System.Guid(value);
		}

		public static System.Guid ToGuid(object value)
		{
			const string method = "ToGuid";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToGuid((System.String) value);
			if ( value is System.Guid )
				return (System.Guid) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion

		#region ToBoolean

		/// <summary>
		/// Various overloaded methods for implicit/explicit conversion from primitive type value to Boolean
		/// </summary>
		/// <param name="value">value to be converted</param>
		/// <returns>Boolean equivalent of the value</returns>
		public static bool ToBoolean(byte value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(short value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(int value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(long value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(float value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(double value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(decimal value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(string value)
		{
			return System.Convert.ToBoolean(value);
		}

		public static bool ToBoolean(object value)
		{
			const string method = "ToBoolean";

			if ( value == null )
				throw new NullParameterException(typeof(TypeConvert), method, "value");

			if ( value is System.String )
				return ToBoolean((System.String) value);
			if ( value is System.Byte )
				return ToBoolean((System.Byte) value);
			if ( value is System.Int16 )
				return ToBoolean((System.Int16) value);
			if ( value is System.Int32 )
				return ToBoolean((System.Int32) value);
			if ( value is System.Int64 )
				return ToBoolean((System.Int64) value);
			if ( value is System.Single )
				return ToBoolean((System.Single) value);
			if ( value is System.Double )
				return ToBoolean((System.Double) value);
			if ( value is System.Decimal )
				return ToBoolean((System.Decimal) value);
			if ( value is System.Boolean )
				return (System.Boolean) value;
			else
				throw new InvalidParameterValueException(typeof(TypeConvert), method, "value", value.GetType(), value);
		}

		#endregion
	}
}

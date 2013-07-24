using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// TypeClone is a helper class for cloning value type data.
	/// </summary>
	public sealed class TypeClone
	{
		private TypeClone()
		{
		}

		/// <summary>
		/// Clones the supplied primitive value-typed data.
		/// </summary>
		/// <param name="value">The value to be cloned</param>
		/// <returns>Cloned object</returns>
		public static object Clone(object value)
		{
			const string method = "Clone";

			if ( value == null )
				return null;
			if ( value is System.String )
				return value;
			if ( value is System.Byte )
				return (System.Byte) value;
			if ( value is System.Int16 )
				return (System.Int16) value;
			if ( value is System.Int32 )
				return (System.Int32) value;
			if ( value is System.Int64 )
				return (System.Int64) value;
			if ( value is System.Single )
				return (System.Single) value;
			if ( value is System.Double )
				return (System.Double) value;
			if ( value is System.Decimal )
				return (System.Decimal) value;
			if ( value is System.Boolean )
				return (System.Boolean) value;
			if ( value is System.Guid )
				return (System.Guid) value;
			if ( value is Date )
				return (Date) value;
			if ( value is DateTime )
				return (DateTime) value;
            if (value is System.DateTime)
                return DateTime.FromSystemDateTime((System.DateTime) value, System.TimeZone.CurrentTimeZone);
			if ( value is TimeOfDay )
				return (TimeOfDay) value;
			if ( value is TimeSpan )
				return (TimeSpan) value;
			else
				throw new InvalidParameterValueException(typeof(TypeClone), method, "value", value.GetType(), value);
		}
	}
}

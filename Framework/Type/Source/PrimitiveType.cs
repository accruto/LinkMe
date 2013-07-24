namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Primitive types supported by the framework.
	/// </summary>
	public enum PrimitiveType
	{
		None		= 0x0000,
		String		= 0x0001,
		Byte		= 0x0002,
		Int16		= 0x0004,
		Int32		= 0x0008,
		Int64		= 0x0010,
		Single		= 0x0020,
		Double		= 0x0040,
		Decimal		= 0x0080,
		Boolean		= 0x0100,
		Guid		= 0x0200,
		Date		= 0x0400,
		DateTime	= 0x0800,
		TimeOfDay	= 0x1000,
		TimeSpan	= 0x2000
	}
}
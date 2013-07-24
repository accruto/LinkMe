namespace LinkMe.Framework.Tools.Net
{
	[System.Flags]
	public enum WrappedValueFormats
	{
		None = 0,
		Object = 1,
		Xml = 2,
		String = 4,
		All = Object | Xml | String
	}
}

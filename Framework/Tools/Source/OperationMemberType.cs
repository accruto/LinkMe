namespace LinkMe.Framework.Tools
{
	/// <summary>
	/// The type of member that an operation represents - method, property get or property set.
	/// </summary>
	public enum OperationMemberType
	{
		None,
		Method,
		PropertyGet,
		PropertySet
	}
}
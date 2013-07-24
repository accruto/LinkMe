namespace LinkMe.Framework.Tools.ObjectProperties
{
	/// <summary>
	/// Represents the parent control of an ElementPropertyPage, which may be an ObjectPropertySheet or
	/// another ElementPropertyPage.
	/// </summary>
	public interface IPropertyPageParent
	{
		bool IsReadOnly { get; }
	}
}

namespace LinkMe.Framework.Tools.ObjectProperties
{
	public interface IObjectPropertyInfo
		:	IElementPropertyInfo
	{
		/// <summary>
		/// The actual object that this information is for.
		/// </summary>
		object Object { get; }
		object CloneObject();
		ObjectPropertySettings Settings { get; set; }
		bool IsEditMode { get; set; }
	}
}

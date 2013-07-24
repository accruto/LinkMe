namespace LinkMe.Framework.Tools.ObjectBrowser
{
	public interface INamespaceBrowserInfo : IElementBrowserInfo
	{
		/// <summary>
		/// The parent namespace or null if this is a root-level namespace (ie. directly under a repository).
		/// </summary>
		INamespaceBrowserInfo Parent { get; }
		/// <summary>
		/// The parent repository.
		/// </summary>
		IRepositoryBrowserInfo Repository { get; }
		/// <summary>
		/// Child namespaces, if any. May be null.
		/// </summary>
		INamespaceBrowserInfo[] Namespaces { get; }
		/// <summary>
		/// The types contained in this namespace. May be null.
		/// </summary>
		ITypeBrowserInfo[] Types { get; }
		/// <summary>
		/// True if the namespace contains at least one namespace or type.
		/// </summary>
		bool HasChildren { get; }
		/// <summary>
		/// True to automatically update the checked state of child and parent nodes when this namespace node
		/// is checked or unchecked by the user.
		/// </summary>
		bool AutoCheckRelatives { get; }

/*		/// <summary>
		/// Affects the behaviour of GetCheckedElements() for this namespace if it's checked. The namespace
		/// itself is only included if this value is true.
		/// </summary>
		bool IncludeThisInChecked { get; }
		/// <summary>
		/// Affects the behaviour of GetCheckedElements() for this namespace if it's checked. The contents
		/// (children) of the namespace are only included if this value is true.
		/// </summary>
		bool IncludeContentsInChecked { get; }
*/
	}
}

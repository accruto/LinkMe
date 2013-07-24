namespace LinkMe.Framework.Tools.ObjectBrowser
{
	public interface ITypeBrowserInfo : IElementBrowserInfo
	{
		/// <summary>
		/// The namespace that this type belongs to. May be null if the type is contained directly under
		/// a repository.
		/// </summary>
		INamespaceBrowserInfo Namespace { get; }
		/// <summary>
		/// The repository that contains this type.
		/// </summary>
		IRepositoryBrowserInfo Repository { get; }
		/// <summary>
		/// The base types of this type, if any. May be null.
		/// </summary>
		ITypeBrowserInfo[] BaseTypes { get; }
		/// <summary>
		/// True if the BaseTypes property will return one or more base types.
		/// </summary>
		bool HasBaseTypes { get; }
		/// <summary>
		/// The members of this type, if any. Return null to indicate that this kind of type cannot contain
		/// any members - this grays out the members TreeView. Return an empty array to indicate that
		/// it can, but doesn't.
		/// </summary>
		IMemberBrowserInfo[] Members { get; }
	}
}

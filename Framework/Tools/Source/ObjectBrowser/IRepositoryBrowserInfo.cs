namespace LinkMe.Framework.Tools.ObjectBrowser
{
	public interface IRepositoryBrowserInfo : IElementBrowserInfo
	{
		/// <summary>
		/// The namespaces contained in this repository. May be null.
		/// </summary>
		INamespaceBrowserInfo[] Namespaces { get; }
		/// <summary>
		/// The types contained directly in this repository (not under a namespace). May be null.
		/// </summary>
		ITypeBrowserInfo[] Types { get; }
		/// <summary>
		/// True if the repository contains at least one namespace or type.
		/// </summary>
		bool HasChildren { get; }
		/// <summary>
		/// The manager to be used for this repository. The object browser propogates the manager object
		/// it receives from the user code to each repository via this property.
		/// </summary>
		ObjectBrowserManager Manager { get; set; }

		/// <summary>
		/// Returns true if this object and the specified IRepositoryBrowserInfo object refer to the same
		/// repository.
		/// </summary>
		bool RepositoryEquals(IRepositoryBrowserInfo other);
		/// <summary>
		/// Called by the object browser when the contents of the repository are being refreshed. If the
		/// repository has cached its contents it should clear the cache in this method.
		/// </summary>
		void OnRefresh();
	}
}

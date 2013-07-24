using System;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	public interface IMemberBrowserInfo : IElementBrowserInfo
	{
		/// <summary>
		/// The type that this member belongs to.
		/// </summary>
		ITypeBrowserInfo Type { get; }
	}
}

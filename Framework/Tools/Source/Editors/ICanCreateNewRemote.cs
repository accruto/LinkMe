using System;

using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// An editor that can also create a new value in a remote AppDomain.
	/// </summary>
	public interface ICanCreateNewRemote : ICanCreateNew
	{
		/// <summary>
		/// Creates and returns a wrapper that exists in the specified AppDomain and contains a new value of the
		/// specified type.
		/// </summary>
		GenericWrapper CreateNew(string typeName, AppDomain remoteDomain);
	}
}

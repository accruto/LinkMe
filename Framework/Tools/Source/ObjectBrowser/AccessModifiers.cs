using System;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// C# access modifiers in order of priority: public, protected internal, protected, internal, private.
	/// </summary>
	[Flags]
	public enum AccessModifiers
	{
		Unknown = 0,
		Public = 1,
		ProtectedInternal = 2,
		Protected = 4,
		Internal = 8,
		Private = 16
	}
}
namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	// Order of priority: class, interface, struct, enum, delegate
	internal enum ObjectType
	{
		Class = 1,
		Interface = 2,
		Struct = 3,
		Enum = 4,
		Delegate = 5
	}
}
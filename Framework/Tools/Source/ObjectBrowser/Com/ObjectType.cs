namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	// Order of priority: class, interface, struct, enum, union. Module and alias should not be shown at all.
	internal enum ObjectType
	{
		Class = 1,
		Interface = 2,
		Struct = 3,
		Enum = 4,
		Union = 5,
		Module = 6,
		Alias = 7
	}
}
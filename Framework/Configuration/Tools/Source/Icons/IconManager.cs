namespace LinkMe.Framework.Configuration.Tools
{
	public enum Icons
	{
        Computer,
        Domain,
		Environment,
		Module,
		Repository,
		RepositoryType,
		Store,
		StoreType,
		Variable,
		Catalogue,
		Directory,
		Namespace,
		Instance,
		Folder,
        Link
	}

	[System.Flags]
	public enum IconMask
	{
		ReadOnly = 0x01,
		Large = 0x02,
		Disconnected = 0x04
	}

	public sealed class IconManager
	{
		private IconManager()
		{
		}

		public static string GetResourceName(Icons icon)
		{
			return GetResourceName(icon, (IconMask) 0);
		}

		public static string GetResourceName(Icons icon, bool isReadOnly)
		{
			return GetResourceName(icon, isReadOnly ? IconMask.ReadOnly : (IconMask) 0);
		}

		public static string GetResourceName(Icons icon, bool isReadOnly, IconMask mask)
		{
			if ( isReadOnly )
				mask |= IconMask.ReadOnly;
			else
				mask = mask & ~IconMask.ReadOnly;
			return GetResourceName(icon, mask);
		}

		public static string GetResourceName(Icons icon, IconMask mask)
		{
			return Constants.Icon.ResourcePrefix
				+ icon.ToString()
				+ ((mask & IconMask.ReadOnly) == IconMask.ReadOnly ? IconMask.ReadOnly.ToString() : string.Empty)
				+ ((mask & IconMask.Large) == IconMask.Large ? IconMask.Large.ToString() : string.Empty)
				+ ((mask & IconMask.Disconnected) == IconMask.Disconnected ? IconMask.Disconnected.ToString() : string.Empty)
				+ Constants.Icon.ResourceSuffix;
		}

		public static string GetResourceName(string iconResourceName, IconMask mask)
		{
			if ( !iconResourceName.EndsWith(Constants.Icon.ResourceSuffix) )
				return iconResourceName;

			return iconResourceName.Substring(0, iconResourceName.Length - Constants.Icon.ResourceSuffix.Length)
				+ ((mask & IconMask.ReadOnly) == IconMask.ReadOnly ? IconMask.ReadOnly.ToString() : string.Empty)
				+ ((mask & IconMask.Large) == IconMask.Large ? IconMask.Large.ToString() : string.Empty)
				+ ((mask & IconMask.Disconnected) == IconMask.Disconnected ? IconMask.Disconnected.ToString() : string.Empty)
				+ Constants.Icon.ResourceSuffix;
		}
	}
}

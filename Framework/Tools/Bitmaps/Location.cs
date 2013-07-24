using System;

namespace LinkMe.Framework.Tools.Bitmaps
{
	/// <summary>
	/// This class is used with ToolboxBitmapAttributes. When a file is included as an embedded resource the
	/// name of the resource is [DefaultNamespace].[FolderPath].[FileName], so that the namespace hierarchy
	/// has to exactly match the folder hierarchy - an extra level like "Source" prevents it from working.
	/// 
	/// To apply a ToolboxBitmapAttribute to a control add the bitmap as an embedded resource to the same
	/// folder as this class and specify its type as the first parameter. For example:
	/// 
	/// [ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "ObjectBrowser.bmp")]
	/// </summary>
	internal sealed class Location
	{
		private Location()
		{
		}
	}
}

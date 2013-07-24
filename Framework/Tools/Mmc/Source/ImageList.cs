using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LinkMe.Framework.Tools.Mmc
{
	internal class ImageList
		:	Images
	{
		#region Constructors

		internal ImageList()
		{
		}

		#endregion

		/// <summary>
		/// This method loads all images of the list into the unmanaged image list
		/// specified.
		/// </summary>
		internal void LoadImageList(IImageList imageList)
		{
			LoadImageList(imageList, 0);
		}

		/// <summary>
		/// This method loads all images of the list into the unmanaged image list
		/// specified.
		/// </summary>
		internal void LoadImageList(IImageList imageList, int baseCookie)
		{
			for ( int index = 0; index < Count; ++index )
			{
				if ( this[index].Image != null )
					imageList.ImageListSetIcon(GetIconHandle(index), (baseCookie << 16) + index);
			}
		}
	}
}

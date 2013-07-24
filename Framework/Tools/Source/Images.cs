using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools
{
	public class Images
		:	IDisposable
	{
		#region Constructors

		public Images()
		{
		}

		~Images()
		{
			DisposeInternal();
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			DisposeInternal();
			GC.SuppressFinalize(this);
		}

		#endregion

		public int Count
		{
			get { return m_list.Count; }
		}

		/// <summary>
		/// Appends an image to the list.
		/// </summary>
		public int AddImage(object image)
		{
			return m_list.Add(new ImageEntry(image));
		}

		/// <summary>
		/// This overloaded method appends an image specified by name to the list. The image is loaded
		/// from an embedded resource.
		/// </summary>
		public int AddResource(string name)
		{
			Debug.Assert (!m_loadedImages.ContainsKey(name), "The ImageList already contains the key '"
				+ name + "' - check for this before adding an image.");

			int index = m_list.Add(new ImageEntry(FindImage(name)));
			m_loadedImages.Add(name, index);

			return index;
		}

		public int IndexOf(string name)
		{
			object index = m_loadedImages[name];
			return (index == null ? -1 : (int)index);
		}

		/// <summary>
		/// This method inserts an image specified by name at the specified index. The image is loaded
		/// from an embedded resource.
		/// </summary>
		public void InsertResource(int index, string name)
		{
			m_list.Insert(index, new ImageEntry(FindImage(name)));
		}

		/// <summary>
		/// This overloaded method replaces an image at the specified index
		/// </summary>
		public void Replace(int index, Image image)
		{
			this[index] = new ImageEntry(image);
		}

		/// <summary>
		/// This method replaces an image specified by name at the specified index.
		/// The image is loaded from an embedded resource
		/// </summary>
		public void Replace(int index, string name)
		{
			this[index] = new ImageEntry(FindImage(name));
		}

		/// <summary>
		/// This method creates a bitmap handle to the specified image. The image may be either an
		/// icon or a bitmap. The handle will be deleted automatically by the destructor.
		/// </summary>
		public IntPtr GetBitmapHandle(int index)
		{
			ImageEntry entry = this[index];
			if ( entry.Image != null && entry.BitmapHandle == IntPtr.Zero )
			{
				Bitmap bm;
				if ( entry.Image is Icon )
					bm = ((Icon) entry.Image).ToBitmap();
				else
					bm = (Bitmap) entry.Image;

				entry.BitmapHandle = GetCompatibleBitmapHandle(bm);
				this[index] = entry;
			}

			return entry.BitmapHandle;
		}

		public Icon GetIcon(int index)
		{
			ImageEntry entry = this[index];
			if ( entry.Image != null && entry.Image is Icon )
				return (Icon) entry.Image;
			else
				return null;
		}

		/// <summary>
		/// This method creates an icon handle to the specified image. The image may be either an icon
		/// or a bitmap. The handle will be deleted automatically by the destructor.
		/// </summary>
		public IntPtr GetIconHandle(int index)
		{
			ImageEntry entry = this[index];
			if ( entry.Image != null && entry.IconHandle == IntPtr.Zero )
			{
				if ( entry.Image is Icon )
					entry.IconHandle = ((Icon) entry.Image).Handle;
				else
					entry.IconHandle = ((Bitmap) entry.Image).GetHicon();

				this[index] = entry;
			}

			return entry.IconHandle;
		}

		/// <summary>
		/// This method deletes the bitmap and/or icon handle of the image entry.
		/// </summary>
		private void DeleteHandle(int index)
		{
			ImageEntry entry = this[index];
			if ( entry.Image != null )
			{
				if ( entry.BitmapHandle != IntPtr.Zero )
					UnsafeNativeMethods.DeleteObject(new HandleRef(entry.Image, entry.BitmapHandle));
				if ( entry.IconHandle != IntPtr.Zero && entry.Image is System.Drawing.Bitmap)
				{
					SafeNativeMethods.DestroyIcon(new HandleRef(entry.Image, entry.IconHandle));
				}
			}
		}

		/// <summary>
		/// If we were to call Bitmap.GetHbitmap, we'll get back an HBitmap that is intended for
		/// 32-bit displays. MMC expects the HBITMAP it gets to be compatible with the screen.
		/// So, if the current video display is not set to 32 bit, MMC will fail to show the bitmap.
		/// So what do we do? We translate the bitmaps into the color depth that the screen is
		/// currently displaying.
		/// </summary>
		private static IntPtr GetCompatibleBitmapHandle(Bitmap bm)
		{
			IntPtr hFinalBitmap, hBitmap;
			Bitmap newBm = new Bitmap(bm);
			hFinalBitmap = hBitmap = newBm.GetHbitmap();

			// Get a DC that is compatible with the display.

			IntPtr hdc = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, IntPtr.Zero));

			// Get the BITMAP information.

			DIBSECTION ds = new DIBSECTION();
			UnsafeNativeMethods.GetObject(new HandleRef(newBm, hBitmap), Marshal.SizeOf(typeof(DIBSECTION)), ref ds);

			// Create BITMAPINFO structures and put in the appropriate values.

			BITMAPINFO bmiOld = new BITMAPINFO();
			bmiOld.Header = ds.Bmih;
			BITMAPINFO bmiNew = new BITMAPINFO();
			bmiNew.Header = ds.Bmih;

			// The old color depth is always 32. Get the color depth the screen supports and compare it with the old one.

			bmiNew.Header.BitCount = (ushort) UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, hdc),
				12 /* BITSPIXEL */);
			if ( bmiNew.Header.BitCount != bmiOld.Header.BitCount )
			{
				// Create a bitmap handle with the color depth of the current screen.

				IntPtr bits;
				hFinalBitmap = SafeNativeMethods.CreateDIBSection(new HandleRef(null, hdc), ref bmiNew,
					1 /* DIB_PAL_COLORS */, out bits, IntPtr.Zero, 0);

				// Translate the 32bpp pixels to something the screen can show.

				UnsafeNativeMethods.SetDIBits(new HandleRef(null, hdc), new HandleRef(null, hFinalBitmap), 0,
					ds.Bmih.Height, ds.Bm.Bits, ref bmiOld, 1 /* DIB_PAL_COLORS */);

				// Delete old bitmap.

				UnsafeNativeMethods.DeleteObject(new HandleRef(bm, hBitmap));
			}

			// Cleanup

			UnsafeNativeMethods.DeleteDC(new HandleRef(null, hdc));

			return hFinalBitmap;
		}

		private void DisposeInternal()
		{
			if (m_list != null)
			{
				for ( int index = 0; index < m_list.Count; ++index )
					DeleteHandle(index);

				m_list = null;
			}
		}

		protected ImageEntry this[int index]
		{
			get { return (ImageEntry) m_list[index]; }
			set 
			{
				if ( index < 0 || index >= m_list.Count )
					throw new ArgumentOutOfRangeException("index");

				DeleteHandle(index);
				m_list[index] = value;
			}
		}

		/// <summary>
		/// This method searches the assemblies of the current application domain for the image
		/// resource specified and loads the image found.
		/// </summary>
		public static object FindImage(string name)
		{
			if ( name == null || name.Length == 0 )
				return null;

			try
			{
				object image = null;

				// Check this assembly.

				if ( (image = LoadImage(Assembly.GetExecutingAssembly(), name)) != null )
					return image;

				// Check the calling assembly.

				if ( (image = LoadImage(Assembly.GetCallingAssembly(), name)) != null )
					return image;

				// Check assemblies in the AppDomain.

				foreach ( Assembly assembly in AppDomain.CurrentDomain.GetAssemblies() )
				{
					if ( !(assembly is System.Reflection.Emit.AssemblyBuilder) )
					{
						if ( (image = LoadImage(assembly, name)) != null )
							return image;
					}
				}
			}
			catch ( System.Exception e )
			{
				throw new ApplicationException("Failed to find image '" + name + "' in AppDomain assemblies.", e);
			}

			return null;
		}

		/// <summary>
		/// This method searches the resource names of the specified assembly for the image resource
		/// specified and loads the image found. The image resource may either be a bitmap or an icon.
		/// </summary>
		private static object LoadImage(Assembly assembly, string name)
		{
			Stream stream = null;

			try
			{
				stream = assembly.GetManifestResourceStream(name);
			}
			catch ( System.NotSupportedException )
			{
				// Dynamically generated assemblies don't support GetManifestResourceStream().
			}

			if ( stream != null )
			{
				if ( string.Compare(Path.GetExtension(name), ".ico", true) == 0 )
					return new Icon(stream);
				else
					return new Bitmap(stream);
			}

			return null;
		}

		protected struct ImageEntry
		{
			/// <summary>
			/// This hold the image object.
			/// </summary>
			public object Image;
			/// <summary>
			/// This hold a WIN32 icon handle of the image object. If the managed image object is a
			/// bitmap, this handle was created by CreateIconIndirect and must be deleted by the
			/// destructor. Otherwise, the icon handle refers of the managed object and must not be
			/// deleted by the destructor.
			/// </summary>
			public IntPtr IconHandle;
			/// <summary>
			/// This hold a WIN32 bitmap handle of the image object. The handle must be deleted by the
			/// destructor in any case, because the unmanaged bitmap is an object of it's own.
			/// </summary>
			public IntPtr BitmapHandle;

			/// <summary>
			/// This contructor creates a new image entry.
			/// </summary>
			public ImageEntry(object image)
			{
				Image = image;
				IconHandle = IntPtr.Zero;
				BitmapHandle = IntPtr.Zero;
			}
		}

		private ArrayList m_list = new ArrayList();
		private Hashtable m_loadedImages = new Hashtable();
	}
}

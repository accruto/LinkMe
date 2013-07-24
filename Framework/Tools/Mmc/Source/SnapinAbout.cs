using System;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// Provides information to add Snapin dialog about the snapin.  
	/// Also used in snapin registration.  Implements ISnapinAbout
	/// to communicate with MMC.
	/// </summary>
	public class SnapinAbout
		:	ISnapinAbout
	{
		public SnapinAbout()
		{
			// Take the provider and version information from the
			// snapin attribute on the associated snapin class.

			object[] attributes = GetType().GetCustomAttributes(typeof(AboutSnapinAttribute), true);
			if ( attributes.Length > 0 )
			{
				AboutSnapinAttribute aboutAttribute = (AboutSnapinAttribute) attributes[0];
				attributes = aboutAttribute.SnapinType.GetCustomAttributes(typeof(SnapinInfoAttribute), true);
				if ( attributes.Length != 0 )
				{
					SnapinInfoAttribute attribute = (SnapinInfoAttribute) attributes[0];

					m_provider = attribute.Provider;
					if ( attribute.Version != null )
						m_version = attribute.Version;
					else
						m_version = GetType().Module.Assembly.GetName().Version.ToString();
				}
			}

			m_mask = Color.Black;
			m_folderIcons.InsertResource(m_mainIconIndex, string.Empty);
			m_folderIcons.InsertResource(m_smallOpenImageIndex, string.Empty);
			m_folderIcons.InsertResource(m_smallClosedImageIndex, string.Empty);
			m_folderIcons.InsertResource(m_largeImageIndex, string.Empty);
		}

		public string Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		public string Provider
		{
			get { return m_provider; }
			set { m_provider = value; }
		}

		public string Version
		{
			get { return m_version; }
			set { m_version = value; }
		}

		public string MainIconName 
		{
			get
			{
				return m_mainIconName;
			}
			set 
			{ 
				m_mainIconName = value; 
				m_folderIcons.Replace(m_mainIconIndex, m_mainIconName);
			}
		}

		public string SmallOpenImageName
		{
			get
			{
				return m_smallOpenName;
			}
			set 
			{ 
				m_smallOpenName = value; 
				m_folderIcons.Replace(m_smallOpenImageIndex, m_smallOpenName);
			}
		}

		public string SmallClosedImageName 
		{
			get
			{
				return m_smallClosedName;
			}
			set 
			{ 
				m_smallClosedName = value; 
				m_folderIcons.Replace(m_smallClosedImageIndex, m_smallClosedName);
			}
		}

		public string LargeImageName 
		{
			get
			{
				return m_largeName;
			}
			set 
			{ 
				m_largeName = value; 
				m_folderIcons.Replace(m_largeImageIndex, m_largeName);
			}
		}

		public Color ImageColorMask
		{
			get { return m_mask; }
			set { m_mask = value; }
		}

		#region ISnapinAbout Implementation
     
		public void GetSnapinDescription(out IntPtr lpDescription)
		{
			lpDescription = Marshal.StringToCoTaskMemUni(Description);
		}

		public void GetProvider(out IntPtr pName)
		{
			pName = Marshal.StringToCoTaskMemUni(Provider);
		}

		public void GetSnapinVersion(out IntPtr lpVersion)
		{
			lpVersion = Marshal.StringToCoTaskMemUni(Version);
		}

		public void GetSnapinImage(out IntPtr hAppIcon)
		{
			hAppIcon = m_folderIcons.GetIconHandle(m_mainIconIndex);
		}

		public void GetStaticFolderImage(out IntPtr hSmallImage, out IntPtr hSmallImageOpen, out IntPtr hLargeImage, out uint cMask)
		{
			hSmallImage = m_folderIcons.GetBitmapHandle(m_smallClosedImageIndex);
			hSmallImageOpen = m_folderIcons.GetBitmapHandle(m_smallOpenImageIndex);
			hLargeImage = m_folderIcons.GetBitmapHandle(m_largeImageIndex);
			cMask = (uint) m_mask.ToArgb();
		}

		#endregion

		private string m_description;
		private string m_provider;
		private string m_version;
		private string m_mainIconName;
		private string m_smallOpenName;
		private string m_smallClosedName;
		private string m_largeName;
		private ImageList m_folderIcons = new ImageList();
		private const int m_mainIconIndex = 0;
		private const int m_smallOpenImageIndex = 1;
		private const int m_smallClosedImageIndex = 2;
		private const int m_largeImageIndex = 3;
		private Color m_mask;
	}
}

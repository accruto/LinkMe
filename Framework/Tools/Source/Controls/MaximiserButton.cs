using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A graphical button that implements the IChangeDisplayMode interface.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "MaximiserButton.bmp")]
	public class MaximiserButton : Button, IChangeDisplayMode
	{
		private const int ImageIndexMaximise = 0;
		private const int ImageIndexMinimise = 1;

		private bool m_fullSize = false;
		private ImageList imgIcons = new ImageList();

		public MaximiserButton()
		{
			ResourceManager resources = new ResourceManager(typeof(MaximiserButton));

			imgIcons.ImageSize = new Size(32, 32);
			imgIcons.ImageStream = ((ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
			imgIcons.TransparentColor = Color.Transparent;

			ImageIndex = 0;
			ImageList = imgIcons;

			Name = "MaximiserButton";
			Size = new System.Drawing.Size(36, 36);
		}

		#region IChangeDisplayMode Members

		public event DisplayModeChangedEventHandler DisplayModeChanged;

		public void OnForcedRestore()
		{
			m_fullSize = false;
			ImageIndex = ImageIndexMaximise;
		}

		#endregion

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return string.Empty; }
			set
			{
				// Do not set the value.
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				imgIcons.Dispose();
			}

			base.Dispose(disposing);
		}

		protected void OnDisplayModeChanged(DisplayModeChangedEventArgs e)
		{
			if (DisplayModeChanged != null)
			{
				DisplayModeChanged(this, e);
			}
		}

		protected override void OnClick(EventArgs e)
		{
			OnDisplayModeChanged(new DisplayModeChangedEventArgs(!m_fullSize));

			m_fullSize = !m_fullSize;
			ImageIndex = (m_fullSize ? ImageIndexMinimise : ImageIndexMaximise);

			base.OnClick(e);
		}
	}
}

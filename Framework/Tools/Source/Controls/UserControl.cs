using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Provides base functionality for user controls.
	/// </summary>
	public class UserControl : System.Windows.Forms.UserControl
	{
		public event EventHandler MinimumSizeChanged;

		private Size m_minimumSize = Size.Empty;
		// The minimum size that the user has set (the current MinimumSize value may have been set by
		// EnforceUserControlMinimumSize()).
		private Size m_userMinimumSize = Size.Empty;

		public UserControl()
		{
		}

		/// <summary>
		/// The minimum size of this control, used by Form.EnforceUserControlMinimumSize(). The default is
		/// (0, 0) - no minimum.
		/// </summary>
		public override Size MinimumSize
		{
			get { return m_minimumSize; }
			set 
			{
				m_userMinimumSize = value;
				MinimumSizeInternal = value;
			}
		}

		private Size MinimumSizeInternal
		{
			set 
			{
				if (value != m_minimumSize)
				{
					m_minimumSize = value; 
					OnMinimumSizeChanged(EventArgs.Empty);
				}
			}
		}

		internal static bool GetMinimumSizeRecursive(Control control, Size parentSize,
			ref int minimumWidth, ref int minimumHeight)
		{
			UserControl userControl = control as UserControl;
			if (userControl != null)
			{
				userControl.EnforceUserControlMinimumSize();

				Size minControlSize = GetMinimumSizeForUserControl(userControl, parentSize);
				minimumWidth = Math.Max(minimumWidth, minControlSize.Width);
				minimumHeight = Math.Max(minimumHeight, minControlSize.Height);
				
				return true;
			}
			else
			{
				bool result = false;
				foreach (Control childControl in control.Controls)
				{
					result = GetMinimumSizeRecursive(childControl, parentSize, ref minimumWidth, ref minimumHeight)
						|| result;
				}

				return result;
			}
		}

		internal static Size GetMinimumSizeForUserControl(UserControl control, Size parentSize)
		{
			Size minSize = new Size();

			if (control.MinimumSize.Width != 0 && ((control.Anchor & (AnchorStyles.Left | AnchorStyles.Right))
				== (AnchorStyles.Left | AnchorStyles.Right) || control.Dock == DockStyle.Fill
				|| control.Dock == DockStyle.Top || control.Dock == DockStyle.Bottom))
			{
				// The width of this control changes with the parent and it has a minimum width set - calculate
				// the minimum width of the parent that would give the required minimum width for the control.

				minSize.Width = parentSize.Width - (control.Width - control.MinimumSize.Width);
			}

			if (control.MinimumSize.Height != 0 && ((control.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom))
				== (AnchorStyles.Top | AnchorStyles.Bottom) || control.Dock == DockStyle.Fill
				|| control.Dock == DockStyle.Left || control.Dock == DockStyle.Right))
			{
				// The height of this control changes with the parent and it has a minimum height set - calculate
				// the minimum height of the parent that would give the required minimum height for the control.

				minSize.Height = parentSize.Height - (control.Height - control.MinimumSize.Height);
			}

			return minSize;
		}

		protected virtual void OnMinimumSizeChanged(EventArgs e)
		{
			if (MinimumSizeChanged != null)
			{
				MinimumSizeChanged(this, e);
			}
		}

		protected void EnforceUserControlMinimumSize()
		{
			int minimumWidth = m_userMinimumSize.Width;
			int minimumHeight = m_userMinimumSize.Height;

			foreach (Control childControl in Controls)
			{
				GetMinimumSizeRecursive(childControl, Size, ref minimumWidth, ref minimumHeight);
			}

			MinimumSizeInternal = new Size(minimumWidth, minimumHeight);
		}
	}
}

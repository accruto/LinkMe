using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An enhanced ListView control. Does not toggle checkboxes when items are double-clicked.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ListView))]
	public class ListView : System.Windows.Forms.ListView
	{
		public event EventHandler ColumnHeaderResized;

		private const bool m_defaultAutoResizeLastColumn = false;

		private bool m_ignoreNextCheck = false;
		private bool m_autoResizeLastColumn = m_defaultAutoResizeLastColumn;

		public ListView()
		{
		}

		/// <summary>
		/// Indicates whether the right-most column is automatically resized to fill all available space.
		/// The default is false.
		/// </summary>
		[DefaultValue(m_defaultAutoResizeLastColumn)]
		public bool AutoResizeLastColumn
		{
			get { return m_autoResizeLastColumn; }
			set { m_autoResizeLastColumn = value; }
		}

		[Browsable(false)]
		public IntPtr HeaderHandle
		{
			get
			{
				if (IsHandleCreated)
				{
					return UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle),
						Constants.Win32.Messages.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
				}
				else
					return IntPtr.Zero;
			}
		}

		#region Static methods

		public static int GetIndexToSelectAfterDeletingSelection(System.Windows.Forms.ListView listView)
		{
			if (listView == null)
				throw new ArgumentNullException("listView");

			int count = listView.SelectedIndices.Count;
			if (count == 0)
				return -1;

			int newCount = listView.Items.Count - count;
			if (newCount == 0)
				return -1;

			int newIndex = listView.SelectedIndices[count - 1] - count + 1;

			if (newIndex < 0)
				return 0;
			else if (newIndex >= newCount)
				return newCount - 1;
			else
				return newIndex;
		}

		#endregion

		protected override void OnItemCheck(ItemCheckEventArgs e)
		{
			if (m_ignoreNextCheck)
			{
				// This check event if from a double-click - ignore it.
				e.NewValue = e.CurrentValue;
				m_ignoreNextCheck = false;
			}
			else
			{
				base.OnItemCheck(e);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			DoAutoResizeLastColumn();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Constants.Win32.Messages.WM_LBUTTONDBLCLK)
			{
				Point mouse = PointToClient(MousePosition);
				if (GetItemAt(mouse.X, mouse.Y) != null)
				{
					// Double-clicked on an item - ignore the ItemCheck event resulting from this.
					m_ignoreNextCheck = true;
				}
			}
			else if (m.Msg == Constants.Win32.Messages.WM_NOTIFY)
			{
				Win32.NMHDR nmhdr = (Win32.NMHDR)m.GetLParam(typeof(Win32.NMHDR));
				WmNotify((IntPtr)nmhdr.hwndFrom, nmhdr.code);
			}

			// If the base ListView WndProc processes WM_SETCURSOR the cursor is not actually changed - see
			// http://www.dotnet247.com/247reference/msgs/52/260249.aspx
			// Set the cursor manually to work around this.

			if (m.Msg == Constants.Win32.Messages.WM_SETCURSOR)
			{
				WmSetCursor(ref m);
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		protected virtual void OnColumnHeaderResized(EventArgs e)
		{
			if (ColumnHeaderResized != null)
			{
				ColumnHeaderResized(this, e);
			}
		}

		private void WmNotify(IntPtr senderHandle, int notifyCode)
		{
			switch (notifyCode)
			{
				case Constants.Win32.Notifications.HDN_ENDTRACKA:
				case Constants.Win32.Notifications.HDN_ENDTRACKW:
				case Constants.Win32.Notifications.HDN_DIVIDERDBLCLICKA:
				case Constants.Win32.Notifications.HDN_DIVIDERDBLCLICKW:
					// The user has finished resizing a column header or has double-clicked on a divider, which
					// will cause columns to resize - auto-resize the last column.

					if (senderHandle == HeaderHandle)
					{
						DoAutoResizeLastColumn();
						OnColumnHeaderResized(EventArgs.Empty);
					}
					break;

				default:
					// Do nothing.
					break;
			}
		}

		private void WmSetCursor(ref Message m)
		{
			if (m.WParam == Handle && ((int)m.LParam & 0xffff) == 1)
			{
				IntPtr ptr = (Cursor == null ? IntPtr.Zero : Cursor.Handle);
				UnsafeNativeMethods.SetCursor(new HandleRef(Cursor, ptr));
			}
			else
			{
				DefWndProc(ref m);
			}
		}

		private void DoAutoResizeLastColumn()
		{
			if (!AutoResizeLastColumn || Columns.Count == 0 || View != View.Details)
				return;

			int lastColumn = Columns.Count - 1;
			if (!UnsafeNativeMethods.PostMessage(new HandleRef(this, Handle), Constants.Win32.Messages.LVM_SETCOLUMNWIDTH,
				(IntPtr)lastColumn, (IntPtr)Constants.Win32.LVSCW_AUTOSIZE_USEHEADER))
			{
				Debug.Fail(string.Format("PostMessage({0}, LVM_SETCOLUMNWIDTH, {1}, LVSCW_AUTOSIZE_USEHEADER) failed.",
					Handle.ToString(), lastColumn.ToString()));
			}
		}
	}
}

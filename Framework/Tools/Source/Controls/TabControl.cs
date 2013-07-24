using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An enhanced TabControl control that allows cancelling selection changes and hiding tab pages, while still
	/// ensuring that they are disposed when the TabControl is disposed.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.TabControl))]
	public class TabControl : System.Windows.Forms.TabControl
	{
		#region Nested types

		public new class ControlCollection : System.Windows.Forms.TabControl.ControlCollection
		{
			private TabControl m_owner;

			public ControlCollection(TabControl owner)
				: base(owner)
			{
				Debug.Assert(owner != null, "owner != null");
				m_owner = owner;
			}

			public override void Remove(Control value)
			{
				// If the owner TabControl does not have the focus it's going to steal it - defect 46697.
				// To work around this save the focus and restore it again after removing the control.

				Control activeControl = null;
				System.Windows.Forms.Form parentForm = null;

				if (!m_owner.ContainsFocus)
				{
					parentForm = m_owner.FindForm();
					if (parentForm != null)
					{
						activeControl = parentForm.ActiveControl;
					}
				}

				base.Remove(value);

				if (activeControl != null)
				{
					parentForm.ActiveControl = activeControl;
				}
			}
		}

		#endregion

		public event TabPageIndexEventHandler SelectedIndexChanging;

		private ArrayList m_hiddenPages = new ArrayList();

		public TabControl()
		{
		}

		/// <summary>
		/// Removes a tab page, but keeps a reference to it to allow showing it again later.
		/// </summary>
		public void HidePage(TabPage page)
		{
			if (page == null)
				throw new ArgumentNullException("page");

			if (TabPages.Contains(page))
			{
				TabPages.Remove(page);
				m_hiddenPages.Add(page);
			}
		}

		/// <summary>
		/// Restores a previously hidden tab page.
		/// </summary>
		public void ShowHiddenPage(TabPage page)
		{
			if (page == null)
				throw new ArgumentNullException("page");

			if (m_hiddenPages.Contains(page))
			{
				m_hiddenPages.Remove(page);
			}

			if (!TabPages.Contains(page))
			{
				TabPages.Add(page);
			}
		}

		/// <summary>
		/// Determines which tab, if any, is at a specified screen position.
		/// </summary>
		/// <returns>The index of the tab or -1 if no tab is at the specified position</returns>
		public int HitTest(Point position)
		{
			Win32.TCHITTESTINFO hitInfo = new Win32.TCHITTESTINFO(position);
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle),
				Constants.Win32.Messages.TCM_HITTEST, IntPtr.Zero, ref hitInfo).ToInt32();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			// Raise the SelectedIndexChanging event when the user changes tabs using the keyboard. This is
			// implemented in the base TabControl.OnKeyDown() method (it doesn't use the native Tab Control
			// functionality), so we have to handle it here as well.

			bool processed = false;

			if (e.KeyCode == Keys.Tab && (e.Modifiers & Keys.Control) == Keys.Control)
			{
				bool shift = ((e.Modifiers & Keys.Shift) == Keys.Shift);
				int newIndex = (shift ? (SelectedIndex + TabCount - 1) % TabCount : (SelectedIndex + 1) % TabCount);

				TabPageIndexEventArgs args = new TabPageIndexEventArgs(newIndex);
				OnSelectedIndexChanging(args);

				if (args.Cancel)
				{
					e.Handled = true;
					processed = true;
				}
			}

			if (!processed)
			{
				base.OnKeyDown(e);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Dispose of the hidden pages. This won't be done by base.Dispose(), because those pages
				// are not members of the TabPages collection.

				foreach (TabPage hiddenPage in m_hiddenPages)
				{
					hiddenPage.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		protected override System.Windows.Forms.Control.ControlCollection CreateControlsInstance()
		{
			return new TabControl.ControlCollection(this);
		}

		protected virtual void OnSelectedIndexChanging(TabPageIndexEventArgs e)
		{
			if (SelectedIndexChanging != null)
			{
				SelectedIndexChanging(this, e);
			}
		}

		protected override void WndProc(ref Message m)
		{
			const int wmReflectNotify = Constants.Win32.Messages.WM_REFLECT + Constants.Win32.Messages.WM_NOTIFY;

			bool processed = false;

			switch (m.Msg)
			{
				case Constants.Win32.Messages.WM_NOTIFY:
				case wmReflectNotify:
					Win32.NMHDR nmhdr = (Win32.NMHDR)m.GetLParam(typeof(Win32.NMHDR));
					if (nmhdr.code == Constants.Win32.Notifications.TCN_SELCHANGING)
					{
						processed = WmSelChanging(ref m);
					}
					break;
			}

			if (!processed)
			{
				base.WndProc(ref m);
			}
		}

		private bool WmSelChanging(ref Message m)
		{
			// When the TCN_SELCHANGING notification is received raise an event that allows user code to
			// cancel the selection change.

			int newIndex = HitTest(PointToClient(Control.MousePosition));
			if (newIndex == -1)
				return false;

			TabPageIndexEventArgs args = new TabPageIndexEventArgs(newIndex);
			OnSelectedIndexChanging(args);

			m.Result = (args.Cancel ? new IntPtr(1) : IntPtr.Zero);

			return args.Cancel;
		}
	}
}

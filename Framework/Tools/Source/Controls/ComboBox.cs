using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A combo box that allows refreshing item text and selecting text with Control-A.
	/// Also contains the ability to set it to read-only.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	public class ComboBox
		:	System.Windows.Forms.ComboBox //, IReadOnlySettable
	{
		private int m_previousSelectedIndex = -1;
		private bool m_manualRefresh = false;
		private bool m_readOnly = false;
		private bool m_droppedDown = false;
		private int m_selectedIndex = -1;

		public ComboBox()
		{
			// Create the control immediately - we need that to listen for CB_RESETCONTENT messages.
			CreateControl();
		}

		public new void RefreshItem(int index)
		{
			m_manualRefresh = true;
			base.RefreshItem(index);
			m_manualRefresh = false;
		}

		[Browsable(true), DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return m_readOnly;
			}
			set
			{
				m_readOnly = value;

				// Send the textbox portion of the combo the readonly message.

				IntPtr childWindow = UnsafeNativeMethods.GetWindow(Handle, Constants.Win32.GW_CHILD);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, childWindow),
					Constants.Win32.Messages.EM_SETREADONLY, (IntPtr) (value ? 1 : 0), IntPtr.Zero);

				// If text was typed or pasted into the textbox, the context menu will 
				// have the undo activated. When the text box is in the readonly state 
				// the undo will still be active from the right click context menu 
				// allowing the user to restore the previous value. This sendmessage 
				// will clear the undo buffer which will clear the undo.
				
				UnsafeNativeMethods.SendMessage(new HandleRef(this,
					UnsafeNativeMethods.GetWindow(new HandleRef(this, Handle), Constants.Win32.GW_CHILD)),
					Constants.Win32.Messages.EM_EMPTYUNDOBUFFER, (IntPtr) (value ? 1 : 0), IntPtr.Zero);

				// The dropdown may have been dropped before the readonly is set.

                m_droppedDown = false;
				Refresh();
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			// Select all on Control-A. Note that we must set Handled = true in
			// the KeyPress event (not KeyDown), otherwise a beep is generated
			// (as for an unhandled key).

			if (e.KeyChar == (char)1)
			{
				SelectAll();
				e.Handled = true;
			}

			base.OnKeyPress(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			// The up and down arrow keys cause the combobox to change selection to the next 
			// or previous in the list. The page up and page down keys change the selection 
            // by one page at a time as defined by the size of the dropdown list. Setting 
			// e.Handled to true if any of these keys is pressed stops the selection change 
			// when readonly. The alt down arrow combination is allowed since it drops the listbox.

			if ( m_readOnly )
			{
				switch ( e.KeyCode )
				{
					case Keys.Up:
					case Keys.PageUp:
					case Keys.PageDown:
					case Keys.Right:
					case Keys.Left:
						e.Handled = true;
						break;

					default:
						if ( e.KeyCode == Keys.Down && ((ModifierKeys & Keys.Alt) != Keys.Alt) )
							e.Handled = true;
						break;
				}
			}

			base.OnKeyDown(e);
		}

		public new int SelectedIndex
		{
			get
			{
				return m_selectedIndex;
			}
			set
			{
				m_selectedIndex = value;
				base.SelectedIndex = value;
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.SelectedIndex != m_previousSelectedIndex)
			{
				base.OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(m_previousSelectedIndex));
			}

			m_selectedIndex = base.SelectedIndex;
			m_previousSelectedIndex = base.SelectedIndex;
		}

		protected override void OnSelectionChangeCommitted(EventArgs e)
		{
			// The combobox default behavior when pressing F4 is to drop the listbox.
			// If F4 is immediately pressed a second time the OnSelectionChangeCommitted 
			// event fires regardless of whether a change has been made or not. When 
			// readonly we don't want a change event to fire. This code will stop it.

			if ( !m_readOnly )
				base.OnSelectionChangeCommitted(e);
		}

		protected override void OnDropDown(EventArgs e)
		{
			m_droppedDown = true;
			base.OnDropDown (e);
		}

		protected override void WndProc(ref Message m)
		{
			if ( m_readOnly && m_droppedDown )
			{
				// Intercepting message 273 when readonly and the listbox is dropped 
				// keeps the user from selecting an item in the list and having it update
				// the text value of the combo as well as firing the associated changed events.
				// Since we intercept a windows message we will have to manually bring up
				// the listbox.
				// msg 305 (0x131)   =  an item was clicked from the dropdown list
				// msg 273 (0x111)   = (WM_COMMAND) follows dropdown list click and all other actions?
				// msg 8465 (0x2111) = (WM_REFLECT + WM_COMMAND) subsequent command after the 273

				if ( m.Msg == 273 )
				{
					m_droppedDown = false;

					// Bring up the dropdown.

					UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), Constants.Win32.Messages.CB_SHOWDROPDOWN, (IntPtr) 0, (IntPtr) 0);
					return;
				}
			}

			base.WndProc(ref m);

			if ((m.Msg == Constants.Win32.Messages.CB_DELETESTRING && !m_manualRefresh)
				|| m.Msg == Constants.Win32.Messages.CB_RESETCONTENT)
			{
				// When an item is deleted (or all items are cleared) OnSelectedIndexChanged is not called, but
				// we still need to update our previous selected index value.
				m_previousSelectedIndex = SelectedIndex;
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A RichTextBox that allows creating hyperlinks.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.RichTextBox))]
	public class RichTextBox : System.Windows.Forms.RichTextBox, IReadOnlySettable
	{
		private bool m_acceptsControlTab = true;
		private bool m_ignoreTextChanged = false;

		public bool AcceptsControlTab
		{
			get { return m_acceptsControlTab; }
			set { m_acceptsControlTab = value; }
		}

		public void SelectedTextToLink(bool hyperlink)
		{
			Win32.CHARFORMAT2W format = Win32.CHARFORMAT2W.Create();

			format.dwMask = Constants.Win32.CFM_LINK;
			format.dwEffects = (hyperlink ? Constants.Win32.CFE_LINK : 0);

			IntPtr result = UnsafeNativeMethods.SendMessage(new HandleRef(this, Handle),
				Constants.Win32.Messages.EM_SETCHARFORMAT, (IntPtr)Constants.Win32.SCF_SELECTION, ref format);

			if (result == (IntPtr)0)
				throw new Win32Exception();
		}

		/// <summary>
		/// Set the text colour to <paramref name="color"/> for all the text in the textbox.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		public void SetTextColor(Color color)
		{
			SetTextColor(color, 0, TextLength);
		}

		/// <summary>
		/// Set the text colour to <paramref name="color"/> for <paramref name="length"/> characters starting
		/// from <paramref name="startIndex"/>.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		/// <param name="startIndex">The index of the first character to which the colour applies.</param>
		/// <param name="length">The number of characters following <paramref name="startIndex"/> to which the
		/// colour applies.</param>
		public void SetTextColor(Color color, int startIndex, int length)
		{
			int oldStart = SelectionStart;
			int oldLength = SelectionLength;

			try
			{
				SelectionStart = startIndex;
				SelectionLength = length;
				SelectionColor = color;
			}
			finally
			{
				SelectionStart = oldStart;
				SelectionLength = oldLength;
			}		
		}

		/// <summary>
		/// Selects <paramref name="length"/> characters starting from <paramref name="startIndex"/> and sets
		/// their colour.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		/// <param name="startIndex">The index of the first character to select.</param>
		/// <param name="length">The number of characters following <paramref name="startIndex"/> to select.</param>
		public void SelectAndSetTextColor(Color color, int startIndex, int length)
		{
			SelectionStart = startIndex;
			SelectionLength = length;
			ScrollToCaret();

			m_ignoreTextChanged = true;
			try
			{
				SelectionColor = color;
			}
			finally
			{
				m_ignoreTextChanged = false;
			}
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.Tab))
				return !AcceptsControlTab;
			else
				return base.IsInputKey(keyData);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (!m_ignoreTextChanged)
			{
				base.OnTextChanged(e);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			// .NET bug workaround: auto word selection is turned ON by default, even though the AutoWordSelection
			// property returns false. The line below seems to fix the problem.

			AutoWordSelection = AutoWordSelection;
		}
	}
}

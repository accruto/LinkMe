using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A text box that allows selecting text with Control-A.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
	public class TextBox : System.Windows.Forms.TextBox, IReadOnlySettable
	{
		public TextBox()
		{
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
	}
}

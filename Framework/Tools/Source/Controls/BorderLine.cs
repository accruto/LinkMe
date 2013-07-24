using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A simple border line, the same as the border of a GroupBox.
	/// </summary>
	public class BorderLine : Control
	{
		public BorderLine()
		{
		}

		protected override Size DefaultSize
		{
			get { return new Size(200, 2); } 
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, 2);

			base.OnPaint(e);
		}
	}
}

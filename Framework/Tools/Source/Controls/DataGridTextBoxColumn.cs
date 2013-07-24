using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An enhanced DataGridTextBoxColumn.
	/// </summary>
	public class DataGridTextBoxColumn : System.Windows.Forms.DataGridTextBoxColumn
	{
		public DataGridTextBoxColumn()
		{
		}

		public DataGridTextBoxColumn(PropertyDescriptor prop)
			: base(prop)
		{
		}

		public DataGridTextBoxColumn(PropertyDescriptor prop, bool isDefault)
			: base(prop, isDefault)
		{
		}

		public DataGridTextBoxColumn(PropertyDescriptor prop, string format, bool isDefault)
			: base(prop, format, isDefault)
		{
		}

		public DataGridTextBoxColumn(PropertyDescriptor prop, string format)
			: base(prop, format)
		{
		}

		/// <summary>
		/// Returns the text bounds for the specified cell. The base implementation simply returns the cell bounds.
		/// </summary>
		public virtual Rectangle GetTextBounds(DataGridCell cell)
		{
			return DataGridTableStyle.DataGrid.GetCellBounds(cell);
		}

		protected override void EnterNullValue()
		{
			// The base class allows Control-0 to change the value even when the text box is read-only,
			// which seems to be a bug - fix it here.

			if (!TextBox.ReadOnly)
			{
				base.EnterNullValue();
			}
		}
	}
}

using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Reflection;

namespace LinkMe.Framework.Type.Tools.Controls
{
	public class DataGridPrimitiveValueColumn
		:	DataGridColumnStyle
	{
		public DataGridPrimitiveValueColumn(PrimitiveType type)
		{
			m_errorProvider = new ErrorProvider();
			m_control = new PrimitiveValueControl();
			m_control.ErrorProvider = m_errorProvider;
			m_control.PrimitiveType = type;
			m_control.Visible = false;
			m_control.IgnoreUpKey = true;
			m_control.Leave += new System.EventHandler(m_control_Leave);

			m_textbox = new DataGridTextBox();
			m_textbox.BorderStyle = BorderStyle.None;
			m_textbox.Multiline = true;
			m_textbox.AcceptsReturn = true;
			m_textbox.Visible = false;
		}

		public int PreferredRowHeight
		{
			get { return m_control.Height; }
		}

		public string RegularExpression
		{
			get { return m_control.RegularExpression; }
			set { m_control.RegularExpression = value; }
		}

		#region Overrides

		protected override void Abort(int rowNum)
		{
			Rollback();
			HideControl();
			EndEdit();
		}

		protected override bool Commit(CurrencyManager currencyManager, int rowNum)
		{
			if ( m_textbox.IsInEditOrNavigateMode )
			{
				// Read only mode so remove the text box.

				m_textbox.Bounds = Rectangle.Empty;
			}
			else
			{
				// Hide the control because it is no longer needed.

				HideControl();

				// Only continue if in fact editing is still happening.

				if ( m_inEdit )
				{
					// Base the value on the validity of the control value.

					object value = m_control.IsValid ? m_control.Value : m_originalValue;
					SetColumnValueAtRow(currencyManager, rowNum, TypeXmlConvert.ToString(value));
					EndEdit();
				}
			}
			
			return true;
		}

		protected override void Edit(CurrencyManager currencyManager, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			if ( readOnly )
			{
				Rectangle rectangle = bounds;
				m_textbox.ReadOnly = true;
				m_textbox.Text = GetText(GetColumnValueAtRow(currencyManager, rowNum));

				if ( cellIsVisible )
				{
					bounds.Offset(m_xMargin, 2 * m_yMargin);
					bounds.Width -= m_xMargin;
					bounds.Height -= 2 * m_yMargin;
					m_textbox.Bounds = bounds;
					m_textbox.Visible = true;
					m_textbox.TextAlign = Alignment;
				}
				else
				{
					m_textbox.Bounds = Rectangle.Empty;
				}

				m_textbox.RightToLeft = DataGridTableStyle.DataGrid.RightToLeft;
				m_textbox.Focus();

				if ( m_textbox.Visible )
				{
					DataGridTableStyle.DataGrid.Invalidate(rectangle);

					m_textbox.Show();
					m_textbox.BringToFront();
					m_textbox.Focus();
					m_textbox.SelectAll();
				}
			}
			else
			{
				if ( !m_inEdit )
				{
					// Store information.

					m_currentRow = rowNum;
					m_currencyManager = currencyManager;

					if ( m_control.IsValid )
						m_originalValue = m_control.Value;
					else
						m_originalValue = PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_control.PrimitiveType).Default;
	
					// Set the visual aspect of the control.

					Rectangle originalBounds = bounds;
					if ( cellIsVisible )
					{
						bounds.Offset(m_xMargin, 2 * m_yMargin);
						bounds.Width -= m_xMargin;
						bounds.Height -= 2 * m_yMargin;
						m_control.Bounds = bounds;
						m_control.Font = DataGridTableStyle.DataGrid.Font;
						m_control.Visible = true;
					}
					else
					{
						m_control.Bounds = originalBounds;
						m_control.Visible = false;
					}

					// Set the value of the control (temporarily disabling the event processing because it
					// interferes with the editing state of the column).

					m_control.ValueChanged -= new System.EventHandler(m_control_ValueChanged);
					m_control.Value = GetValue(currencyManager, rowNum);
					m_control.ValueChanged += new System.EventHandler(m_control_ValueChanged);

					// If it is visible then re-paint it.

					if ( m_control.Visible )
					{
						DataGridTableStyle.DataGrid.Invalidate(originalBounds);

						m_control.Show();
						m_control.BringToFront();
						m_control.Focus();
						m_control.SelectValue();
					}
				}
				else
				{
					// This can be called twice in the case of editing a new row.
					// Make the control visible and place on top of textbox.

					m_control.Show();
					m_control.BringToFront();
					m_control.Focus();
				}
			}
		}

		protected override void ConcedeFocus()
		{
			// Hide the controls.

			m_control.Visible = false;
			m_textbox.Bounds = Rectangle.Empty;
		}

		protected override int GetMinimumHeight()
		{
			// Set the height to the height of the control.

			return System.Math.Max(FontHeight, m_control.Height) + m_yMargin + 3;
		}

		protected override int GetPreferredHeight(Graphics graphics, object value)
		{
			int newLineIndex = 0;
			int index = 0;
			string text = this.GetText(value);
			while ( newLineIndex != -1 && newLineIndex < text.Length )
			{
				newLineIndex = text.IndexOf("\r\n", (int) (newLineIndex + 1));
				index++;
			}

			return System.Math.Max(FontHeight * index, m_control.Height) + m_yMargin;
		}

		protected override Size GetPreferredSize(Graphics graphics, object value)
		{
			Size extents = Size.Ceiling(graphics.MeasureString(GetText(value), DataGridTableStyle.DataGrid.Font));
			extents.Width += m_xMargin * 2 + DataGridTableGridLineWidth;
			extents.Height += m_yMargin + DataGridTableGridLineWidth;
			return new Size(extents.Width, System.Math.Max(extents.Height, GetMinimumHeight()));
		}

		protected override void Paint(Graphics graphics, Rectangle bounds, CurrencyManager currencyManager, int rowNum)
		{
			Paint(graphics, bounds, currencyManager, rowNum, false);
		}

		protected override void Paint(Graphics graphics, Rectangle bounds, CurrencyManager currencyManager, int rowNum, bool alignToRight)
		{
			string text = GetText(GetColumnValueAtRow(currencyManager, rowNum));
			PaintText(graphics, bounds, text, alignToRight);
		}

		protected override void Paint(Graphics graphics, Rectangle bounds, CurrencyManager currencyManager, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			string text = GetText(GetColumnValueAtRow(currencyManager, rowNum));
			PaintText(graphics, bounds, text, backBrush, foreBrush, alignToRight);
		}

		protected override void SetDataGridInColumn(DataGrid grid)
		{
			base.SetDataGridInColumn(grid);

			// Set the control.

			if ( m_control.Parent != grid )
			{
				if ( m_control.Parent != null )
					m_control.Parent.Controls.Remove(m_control);
			}

			if ( grid != null )
				grid.Controls.Add(m_control);

			// Set the text box.

			if ( m_textbox.Parent != null )
				m_textbox.Parent.Controls.Remove(m_textbox);

			if ( grid != null )
				grid.Controls.Add(m_textbox);
			m_textbox.SetDataGrid(grid);
		}

		#endregion

		private int DataGridTableGridLineWidth
		{
			get { return DataGridTableStyle.GridLineStyle == DataGridLineStyle.Solid ? 1 : 0; }
		}

		private void EndEdit()
		{
			m_textbox.IsInEditOrNavigateMode = false;
			m_inEdit = false;
			Invalidate();
		}

		private string GetText(object value)
		{
			if ( value == System.DBNull.Value )
				return NullText;

			if ( value != null && TypeXmlConvert.CanConvert((string) value, m_control.PrimitiveType) )
				return TypeXmlConvert.ToType((string) value, m_control.PrimitiveType).ToString();
			else
				return string.Empty;
		}

		private void HideControl()
		{
			if ( m_control.Focused )
				DataGridTableStyle.DataGrid.Focus();
			m_control.Visible = false;
		}

		private void Rollback()
		{
			m_control.Value = m_originalValue;
		}

		private void PaintText(Graphics graphics, Rectangle bounds, string text, bool alignToRight)
		{
			Brush backBrush = new SolidBrush(DataGridTableStyle.BackColor);
			Brush foreBrush = new SolidBrush(DataGridTableStyle.ForeColor);
			PaintText(graphics, bounds, text, backBrush, foreBrush, alignToRight);
		}

		private void PaintText(Graphics graphics, Rectangle bounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{	
			Rectangle rectangle = bounds;
			StringFormat format = new StringFormat();

			if ( alignToRight )
				format.FormatFlags = StringFormatFlags.DirectionRightToLeft;

			switch ( Alignment )
			{
				case HorizontalAlignment.Left:
					format.Alignment = StringAlignment.Near;
					break;

				case HorizontalAlignment.Right:
					format.Alignment = StringAlignment.Far;
					break;

				case HorizontalAlignment.Center:
					format.Alignment = StringAlignment.Center;
					break;
			}

			format.FormatFlags |= StringFormatFlags.NoWrap;
			graphics.FillRectangle(backBrush, rectangle);
			rectangle.Offset(0, 2 * m_yMargin);
			rectangle.Height -= 2 * m_yMargin;
			graphics.DrawString(text, DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) rectangle, format);
			format.Dispose();
		}

		private object GetValue(CurrencyManager currencyManager, int row)
		{
			DataView dataView = currencyManager.List as DataView;
			if ( dataView != null )
			{
				if ( row < dataView.Count )
				{
					string value = (string) dataView[row][MappingName];
					if ( value != string.Empty )
						return TypeXmlConvert.ToType(value, m_control.PrimitiveType);
				}

				return PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_control.PrimitiveType).Default;
			}
			else
			{
				return null;
			}
		}

		private void m_control_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_inEdit )
			{
				m_inEdit = true;
				if ( m_firstEdit )
				{
					base.ColumnStartedEditing((Control) sender);
					m_firstEdit = false;
				}
			}
		}

		private void m_control_Leave(object sender, System.EventArgs e)
		{
			if ( m_inEdit && !m_firstEdit )
			{
				if ( m_control.IsValid )
					SetColumnValueAtRow(m_currencyManager, m_currentRow, TypeXmlConvert.ToString(m_control.Value));

				m_inEdit = false;
				m_firstEdit = true;
				Invalidate();
			}

			HideControl();
		}

		private const int m_xMargin = 2;
		private const int m_yMargin = 1;
		private PrimitiveValueControl m_control;
		private DataGridTextBox m_textbox;
		private ErrorProvider m_errorProvider;
		private CurrencyManager m_currencyManager;
		private int m_currentRow;
		private object m_originalValue;
		private bool m_inEdit = false;
		private bool m_firstEdit = true;
	}
}

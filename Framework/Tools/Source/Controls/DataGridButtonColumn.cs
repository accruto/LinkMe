using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace LinkMe.Framework.Tools.Controls
{
	public class DataGridButtonColumn : DataGridTextBoxColumn
	{
		public event DataGridCellButtonClickEventHandler CellButtonClicked;

		private Bitmap m_buttonBitmap;
		private Bitmap m_buttonPressedBitmap;
		private int m_column;
		private int m_pressedRow;
		private string m_defaultText;
		private System.Windows.Forms.DataGrid m_grid;

		public DataGridButtonColumn(int column, string defaultText)
		{
			m_column = column;
			m_pressedRow = -1;
			m_defaultText = defaultText == null ? string.Empty : defaultText;

			Stream stream = GetType().Assembly.GetManifestResourceStream(Constants.Bitmap.Button);
			m_buttonBitmap = new Bitmap(stream);
			stream = GetType().Assembly.GetManifestResourceStream(Constants.Bitmap.ButtonPressed);
			m_buttonPressedBitmap = new Bitmap(stream);
		}

		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible) 
		{
		} 

		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			m_pressedRow = -1;
			Rectangle rect = new Rectangle(0, 0, 0, 0);

			System.Windows.Forms.DataGrid grid = GetGrid();
			if ( !grid.ReadOnly )
			{
				DataGrid.HitTestInfo hti = grid.HitTest(new Point(e.X, e.Y));
				bool isClickInCell = (hti.Column == m_column && hti.Row > -1);
				if ( isClickInCell )
				{
					rect = grid.GetCellBounds(hti.Row, hti.Column);
					isClickInCell = e.X > rect.Right - m_buttonBitmap.Width;
				}

				if ( isClickInCell )
				{
					Graphics graphics = Graphics.FromHwnd(grid.Handle);
					DrawButton(graphics, m_buttonBitmap, rect, GetButtonText(null, hti.Row), hti.Row);
					graphics.Dispose();
					if ( CellButtonClicked != null )
						CellButtonClicked(this, new DataGridCellButtonClickEventArgs(hti.Row, hti.Column));
				}
			}
		}

		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			Rectangle rect = new Rectangle(0, 0, 0, 0);

			System.Windows.Forms.DataGrid grid = GetGrid();
			if ( !grid.ReadOnly )
			{
				DataGrid.HitTestInfo hti = grid.HitTest(new Point(e.X, e.Y));
				bool isClickInCell = (hti.Column == m_column && hti.Row > -1);
				if ( isClickInCell )
				{
					rect = grid.GetCellBounds(hti.Row, hti.Column);
					isClickInCell = e.X > rect.Right - m_buttonBitmap.Width;
				}

				if ( isClickInCell )
				{
					Graphics graphics = Graphics.FromHwnd(grid.Handle);
					DrawButton(graphics, m_buttonPressedBitmap, rect, GetButtonText(null, hti.Row), hti.Row);
					graphics.Dispose();
					m_pressedRow = hti.Row;
				}
			}
		}

		protected override void Paint(Graphics graphics, Rectangle bounds, CurrencyManager currencyManager, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			// Draw the appropriate button.

			Bitmap bitmap = m_pressedRow == rowNum ? m_buttonPressedBitmap : m_buttonBitmap;
			DrawButton(graphics, bitmap, bounds, GetButtonText(currencyManager, rowNum), rowNum);
		}

		private string GetButtonText(CurrencyManager currencyManager, int rowNum)
		{
			if ( currencyManager != null )
			{
				string s = GetColumnValueAtRow(currencyManager, rowNum).ToString();
				if ( s == string.Empty )
					s = m_defaultText;
				return s;
			}
			else
			{
				try
				{
					return GetGrid()[rowNum, m_column].ToString();
				}
				catch ( System.IndexOutOfRangeException )
				{
					// Will be for new rows in the grid.

					return m_defaultText;
				}
			}
		}

		private void DrawButton(Graphics graphics, Bitmap bitmap, Rectangle bounds, string text, int row)
		{
			System.Windows.Forms.DataGrid grid = GetGrid();
			SizeF sz = graphics.MeasureString(text, grid.Font, bounds.Width - 4, StringFormat.GenericTypographic);

			int x = bounds.Left + Math.Max(0, (bounds.Width - (int) sz.Width) / 2);
			graphics.DrawImage(bitmap, bounds, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
			
			if ( sz.Height < bounds.Height )
			{
				int y = bounds.Top + (bounds.Height - (int) sz.Height) / 2;
				if ( m_buttonPressedBitmap == bitmap )
					x++;
				graphics.DrawString(text, grid.Font, new SolidBrush(grid.ForeColor), x, y);
			}
		}

		private System.Windows.Forms.DataGrid GetGrid()
		{
			if ( m_grid == null || m_grid != DataGridTableStyle.DataGrid )
			{
				m_grid = DataGridTableStyle.DataGrid;
				m_grid.MouseDown += new MouseEventHandler(MouseDownHandler);
				m_grid.MouseUp += new MouseEventHandler(MouseUpHandler);
			}

			return m_grid;
		}
	}

	public delegate void DataGridCellButtonClickEventHandler(object sender, DataGridCellButtonClickEventArgs e);

	public class DataGridCellButtonClickEventArgs : EventArgs
	{
		private int m_row;
		private int m_column;

		public DataGridCellButtonClickEventArgs(int row, int column)
		{
			m_row = row;
			m_column = column;
		}

		public int Row
		{
			get { return m_row; }
		}

		public int Column
		{
			get { return m_column; }
		}
	}

}

using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	internal class DataGridExpandableColumn : LinkMe.Framework.Tools.Controls.DataGridTextBoxColumn
	{
		public event MemberWrapperEventHandler Expand;
		public event MemberWrapperEventHandler Collapse;

		private const int m_expandBoxSide = 12;
		private const int m_indent = 16;

		private MemberWrapper m_currentValue = null;
		private Rectangle m_expandBoxBounds = Rectangle.Empty;

		public DataGridExpandableColumn()
		{
		}

		#region Static methods

		private static bool IsPropertyExpanded(MemberWrapper property)
		{
			if (property == null || property.Tag == null)
				return false;
			else
				return ((CellEditorData)property.Tag).IsExpanded;
		}

		private static int GetTotalButtonSpaceWidth(MemberWrapper property)
		{
			CellEditorData cellData = (CellEditorData)property.Tag;
			int level = (cellData == null ? 0 : cellData.Level);
			return level * m_indent + m_expandBoxSide + 8;
		}

		private static Rectangle GetBoxBoundsForCellBounds(Rectangle cellBounds, MemberWrapper property,
			out int totalWidth)
		{
			CellEditorData cellData = (CellEditorData)property.Tag;

			int level = (cellData == null ? 0 : cellData.Level);
			int x = cellBounds.X + 2 + level * m_indent;
			totalWidth = level * m_indent + m_expandBoxSide + 8;

			return new Rectangle(x, cellBounds.Y + 2, m_expandBoxSide, m_expandBoxSide);
		}

		private static bool CanExpandProperty(MemberWrapper property)
		{
			Debug.Assert(property != null, "property != null");

			if (!property.MayHaveChildren || !property.CanRead || !property.CanCreateWrapper
				|| property.ValueIsNullOrUnavailable || property.ValueTypeIsParseableSystemType()
				|| !(property.ValueAsString is string))
			{
				return false;
			}

			CellEditorData cellData = (CellEditorData)property.Tag;

			return !cellData.HasNoChildren;
		}

		private static MemberWrapper GetProperty(CurrencyManager source, int rowNum)
		{
			return (MemberWrapper)source.List[rowNum];
		}

		private static void PaintExpandControls(Graphics g, ref Rectangle bounds, CurrencyManager source, int rowNum,
			Brush backBrush, Brush foreBrush)
		{
			MemberWrapper property = GetProperty(source, rowNum);

			int totalWidth;
			Rectangle box = GetBoxBoundsForCellBounds(bounds, property, out totalWidth);
			Rectangle buttonBounds = new Rectangle(bounds.X, bounds.Y, totalWidth, bounds.Height);

			Pen forePen = new Pen(foreBrush);

			// Fill in the whole expand box area.

			g.FillRectangle(backBrush, buttonBounds);

			bool drawBox = CanExpandProperty(property);
			if (drawBox)
			{
				// Draw the box and the horizontal bar (-).

				g.DrawRectangle(forePen, box);

				int y = box.Y + box.Height / 2;
				g.DrawLine(forePen, box.Left + 3, y, box.Right - 3, y);

				if (IsPropertyExpanded(property))
				{
					// Expanded - draw the line going down.

					int x = box.X + box.Width / 2;
					g.DrawLine(forePen, x, box.Bottom, x, bounds.Bottom);
				}
				else
				{
					// Not expanded - draw the vertical bar to make a +.

					int x = box.X + box.Width / 2;
					g.DrawLine(forePen, x, box.Top + 3, x, box.Bottom - 3);
				}
			}

			CellEditorData cellData = (CellEditorData)property.Tag;
			if (cellData != null && cellData.Level > 0)
			{
				// Draw lines to the parent. Note that we have to go 1 pixel above the bounds to draw across
				// the horizontal grid line.

				int xLeft = box.X - m_indent + m_expandBoxSide / 2;
				int xRight = (drawBox ? box.X : box.X + m_expandBoxSide / 2);
				int yMiddle = box.Y + box.Height / 2;
				int yBottom = (cellData.IsLastInLevel ? yMiddle : bounds.Bottom);

				g.DrawLine(forePen, xLeft, yMiddle, xRight, yMiddle);
				g.DrawLine(forePen, xLeft, bounds.Top - 1, xLeft, yBottom);

				// Draw lines going past us from other ancestors to further children, unless we are the last child.

				int level = cellData.Level - 1;
				cellData = cellData.Parent;

				while (cellData.Parent != null)
				{
					if (!cellData.IsLastInLevel)
					{
						int xMiddle = bounds.X + level * m_indent - m_expandBoxSide / 2 - 2;
						g.DrawLine(forePen, xMiddle, bounds.Top - 1, xMiddle, bounds.Bottom);
					}

					cellData = cellData.Parent;
					level--;
				}
			}

			bounds = new Rectangle(bounds.X + totalWidth, bounds.Y, bounds.Width - totalWidth, bounds.Height);
		}

		#endregion

		public void Initialise()
		{
			DataGridTableStyle.DataGrid.MouseUp += new MouseEventHandler(DataGrid_MouseUp);
		}

		public override Rectangle GetTextBounds(DataGridCell cell)
		{
			DataGrid grid = DataGridTableStyle.DataGrid;

			Rectangle cellBounds = grid.GetCellBounds(cell);
			MemberWrapper property = GetProperty(TC.DataGrid.GetCurrencyManager(grid), cell.RowNumber);
			int buttonSpaceWidth = GetTotalButtonSpaceWidth(property);

			return new Rectangle(cellBounds.X + buttonSpaceWidth, cellBounds.Y,
				cellBounds.Width - buttonSpaceWidth, cellBounds.Height);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (DataGridTableStyle != null && DataGridTableStyle.DataGrid != null)
				{
					DataGridTableStyle.DataGrid.MouseUp -= new MouseEventHandler(DataGrid_MouseUp);
				}
			}

			base.Dispose(disposing);
		}

		protected override void Abort(int rowNum)
		{
			m_expandBoxBounds = Rectangle.Empty;

			base.Abort(rowNum);
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			m_expandBoxBounds = Rectangle.Empty;

			return base.Commit(dataSource, rowNum);
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
			string instantText, bool cellIsVisible)
		{
			m_currentValue = GetProperty(source, rowNum);

			// Calculate the total witdth of the expand box (with indentation) and align the text.

			int totalWidth;
			Rectangle expandBoxBounds = GetBoxBoundsForCellBounds(bounds, m_currentValue, out totalWidth);

			if (CanExpandProperty(m_currentValue))
			{
				// The actual expand box is visible - store it's bounds, so we can detect when the user clicks it.

				m_expandBoxBounds = expandBoxBounds;
			}
			else
			{
				m_expandBoxBounds = Rectangle.Empty;
			}

			base.Edit(source, rowNum, new Rectangle(bounds.X + totalWidth, bounds.Y,
				bounds.Width - totalWidth, bounds.Height), readOnly, instantText, cellIsVisible);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
			bool alignToRight)
		{
			PaintExpandControls(g, ref bounds, source, rowNum, new SolidBrush(DataGridTableStyle.ForeColor),
				new SolidBrush(DataGridTableStyle.BackColor));

			base.Paint(g, bounds, source, rowNum, alignToRight);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
			Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			PaintExpandControls(g, ref bounds, source, rowNum, backBrush, foreBrush);

			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
		}

		protected virtual void OnExpand(MemberWrapperEventArgs e)
		{
			if (Expand != null)
			{
				Expand(this, e);
			}
		}

		protected virtual void OnCollapse(MemberWrapperEventArgs e)
		{
			if (Collapse != null)
			{
				Collapse(this, e);
			}
		}

		internal void RefreshIfExpanded(CellEditorData cellData)
		{
			Debug.Assert(cellData != null, "cellData != null");

			if (cellData.IsExpanded)
			{
				// If the value has changed collapse the expanded cell. Then expand it again, unless the value was
				// set to null or has become unavailable.

				if (cellData.Wrapper.ValueIsNullOrUnavailable || cellData.Wrapper.Modified)
				{
					MemberWrapperEventArgs args = new MemberWrapperEventArgs(cellData.Wrapper);
					OnCollapse(args);
					cellData.IsExpanded = false;

					if (!cellData.Wrapper.ValueIsNullOrUnavailable)
					{
						OnExpand(args);
						cellData.IsExpanded = true;
					}
				}
			}
			else
			{
				// Needed in case the member has no children to allow the user to try expanding it again.
				cellData.Children = null;
			}
		}

		internal new void Invalidate()
		{
			base.Invalidate();
		}

		internal void ExpandRow(CurrencyManager source, int rowNum)
		{
			MemberWrapper wrapper = GetProperty(source, rowNum);
			CellEditorData cellData = (CellEditorData)wrapper.Tag;

			if (cellData.IsExpanded)
				return; // Already expanded.

			OnExpand(new MemberWrapperEventArgs(wrapper));

			if (!cellData.HasNoChildren)
			{
				cellData.IsExpanded = !cellData.IsExpanded;
			}
		}

		private void ExpandCollapseCurrent()
		{
			CellEditorData cellData = (CellEditorData)m_currentValue.Tag;

			if (cellData.IsExpanded)
			{
				OnCollapse(new MemberWrapperEventArgs(m_currentValue));
			}
			else
			{
				OnExpand(new MemberWrapperEventArgs(m_currentValue));
			}

			if (!cellData.HasNoChildren)
			{
				cellData.IsExpanded = !cellData.IsExpanded;
			}
		}

		private void DataGrid_MouseUp(object sender, MouseEventArgs e)
		{
			// If the left mouse button was released over the button then perform the click.

			if (e.Button == MouseButtons.Left && m_expandBoxBounds.Contains(e.X, e.Y))
			{
				ExpandCollapseCurrent();
			}
		}
	}
}

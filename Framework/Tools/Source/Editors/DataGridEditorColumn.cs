using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Tools.Settings;

using DataGrid = LinkMe.Framework.Tools.Controls.DataGrid;

namespace LinkMe.Framework.Tools.Editors
{
	internal class DataGridEditorColumn : LinkMe.Framework.Tools.Controls.DataGridTextBoxColumn,
		LinkMe.Framework.Tools.Controls.ICellToolTip
	{
		private const int m_buttonHeight = 16;
		private const int m_buttonWidth = 16;
		private const string m_editIconResource = "LinkMe.Framework.Tools.Source.Editors.Icons.Edit.ico";

		private static readonly Image m_editIcon;

		public event MemberWrapperEventHandler EditorCommitted;

		private EditorManager m_editorManager = null;
		private MemberWrapper m_currentValue = null;
		private Rectangle m_editButtonBounds = Rectangle.Empty;
		private bool m_mouseDownOverEditButton = false; // Left mouse down was held down over the edit button.
		private EditorDialog m_editorDialog = null;
		private WindowSettings m_editorDialogSettings = null;

		static DataGridEditorColumn()
		{
			try
			{
				Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(m_editIconResource);
				m_editIcon = Image.FromStream(stream);
			}
			catch (System.Exception)
			{
				Bitmap substituteIcon = SystemIcons.Application.ToBitmap();
				m_editIcon = new Bitmap(substituteIcon, m_buttonWidth, m_buttonHeight);
			}
		}

		public DataGridEditorColumn()
		{
		}

		#region ICellToolTip Members

        public string GetCellToolTip(int x, int y, CurrencyManager currencyManager, DataGridCell cell)
		{
			// The X and Y coordinates are relative to the cell bounds.

			if (x < m_buttonWidth && y < m_buttonHeight
                && ShouldShowButton(GetProperty(currencyManager, cell.RowNumber), cell))
				return "Click this button or press INS to open an editor window.";
			else
				return null;
		}

		#endregion

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EditorManager EditorManager
		{
			get { return m_editorManager; }
			set { m_editorManager = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WindowSettings EditorDialogSettings
		{
			get { return m_editorDialogSettings; }
			set { m_editorDialogSettings = value; }
		}

		private static void PaintButton(Graphics g, Rectangle bounds, ButtonState state)
		{
			Rectangle buttonBounds = new Rectangle(bounds.X, bounds.Y, m_buttonWidth, m_buttonHeight);
			ControlPaint.DrawButton(g, buttonBounds, state);

			g.DrawImageUnscaled(m_editIcon, bounds.X, bounds.Y);
		}

		private static MemberWrapper GetProperty(CurrencyManager source, int rowNum)
		{
			return (MemberWrapper)source.List[rowNum];
		}

		public void Initialise()
		{
			// Assign event handlers that allow the edit button to be "clicked".

			System.Windows.Forms.DataGrid grid = DataGridTableStyle.DataGrid;

			grid.MouseUp += new MouseEventHandler(DataGrid_MouseUp);
			grid.MouseMove += new MouseEventHandler(DataGrid_MouseMove);

			// The MouseUp event actually performs the click - AfterMouseDown just makes the button look pressed,
			// so even if the the DataGrid is not actually an LinkMe.Framework.Tools DataGrid the click will still work.

			DataGrid customDataGrid = grid as DataGrid;
			if (customDataGrid != null)
			{
				customDataGrid.AfterMouseDown += new MouseEventHandler(DataGrid_AfterMouseDown);
			}

			TextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
		}

		/// <summary>
		/// The text bounds don't include the button (if shown).
		/// </summary>
		public override Rectangle GetTextBounds(DataGridCell cell)
		{
			System.Windows.Forms.DataGrid grid = DataGridTableStyle.DataGrid;

			if (ShouldShowButton(GetProperty(DataGrid.GetCurrencyManager(grid), cell.RowNumber), cell))
			{
				Rectangle cellBounds = grid.GetCellBounds(cell);
				return new Rectangle(cellBounds.X + m_buttonWidth, cellBounds.Y,
					cellBounds.Width - m_buttonWidth, cellBounds.Height);
			}
			else
				return base.GetTextBounds(cell);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (DataGridTableStyle != null)
				{
					System.Windows.Forms.DataGrid grid = DataGridTableStyle.DataGrid;

					if (grid != null)
					{
						grid.MouseUp -= new MouseEventHandler(DataGrid_MouseUp);
						grid.MouseMove -= new MouseEventHandler(DataGrid_MouseMove);

						DataGrid customDataGrid = grid as DataGrid;
						if (customDataGrid != null)
						{
							customDataGrid.AfterMouseDown -= new MouseEventHandler(DataGrid_AfterMouseDown);
						}
					}
				}
			}

			base.Dispose(disposing);
		}

		protected override void Abort(int rowNum)
		{
			m_editButtonBounds = Rectangle.Empty;

			base.Abort(rowNum);
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			m_editButtonBounds = Rectangle.Empty;

			return base.Commit(dataSource, rowNum);
		}

		protected override void ConcedeFocus()
		{
			m_editButtonBounds = Rectangle.Empty;

			base.ConcedeFocus();
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
			string instantText, bool cellIsVisible)
		{
			m_currentValue = GetProperty(source, rowNum);
			bool reallyReadOnly = (readOnly || IsReallyReadOnly(m_currentValue));

			// Show the button if the property is not one that the user could edit in the text box.

			if (ShouldShowButton(m_currentValue, bounds, null))
			{
				if (cellIsVisible)
				{
					m_editButtonBounds = new Rectangle(bounds.X, bounds.Y, m_buttonWidth, bounds.Height);
				}
				else
				{
					m_editButtonBounds = Rectangle.Empty;
				}

				base.Edit(source, rowNum, new Rectangle(bounds.X + m_buttonWidth, bounds.Y,
					bounds.Width - m_buttonWidth, bounds.Height), reallyReadOnly, instantText, cellIsVisible);
			}
			else
			{
				m_editButtonBounds = Rectangle.Empty;

				base.Edit(source, rowNum, bounds, reallyReadOnly, instantText, cellIsVisible);
			}
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
			bool alignToRight)
		{
			PaintButtonIfNeeded(g, ref bounds, source, rowNum);

			base.Paint(g, bounds, source, rowNum, alignToRight);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
			Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			PaintButtonIfNeeded(g, ref bounds, source, rowNum);

			if (IsReallyReadOnly(GetProperty(source, rowNum)))
			{
				// Gray out the text for read-only properties, so that the user can easily see which ones
				// can and cannot be edited.

				foreBrush = SystemBrushes.InactiveCaption;
			}

			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
		}

		protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			// The base class handles DBNulls, but we're more likely to deal with nulls,
			// so replace null with DBNull.

			object val = base.GetColumnValueAtRow(source, rowNum);
			return (val == null ? DBNull.Value : val);
		}

		protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			MemberWrapper property = GetProperty(source, rowNum);
			if (!property.CanWrite)
				return; // Trying to set the value will just throw an exception anyway.

			if (value is DBNull)
			{
				value = null; // Replace DBNull with null to match the action in GetColumnValueAtRow().
			}

			base.SetColumnValueAtRow(source, rowNum, value);
		}

		protected virtual void OnEditorCommitted(MemberWrapperEventArgs e)
		{
			if (EditorCommitted != null)
			{
				EditorCommitted(this, e);
			}
		}

		private bool IsReallyReadOnly(MemberWrapper member)
		{
			if (ReadOnly || DataGridTableStyle.ReadOnly || DataGridTableStyle.DataGrid.ReadOnly || !member.CanWrite)
				return true;

			object stringValue = member.ValueAsString;
			return !(stringValue == null || stringValue is string);
		}

        private bool ShouldShowButton(MemberWrapper member, DataGridCell cell)
        {
            return ShouldShowButton(member, DataGridTableStyle.DataGrid.GetCellBounds(cell), null);
        }

	    private bool ShouldShowButton(MemberWrapper member, Rectangle bounds, Graphics graphics)
		{
			// Show the edit button if we have an editor for this type and it's not the default editor
			// (the default editor is most likely this one, so ignore it, just as GetEditorType() does).

			System.Type editorType = ((CellEditorData)member.Tag).GetEditorType(EditorManager);
            if (editorType == null || editorType == EditorManager.DefaultEditor)
                return false;

            if (member.IsString)
                return !CanDisplayWholeStringInCell(member, bounds, graphics);
            else
                return true; // We have an editor.
		}

        private bool CanDisplayWholeStringInCell(MemberWrapper member, Rectangle bounds, Graphics graphics)
	    {
            string valueAsString = member.ValueAsString as string;
            if (string.IsNullOrEmpty(valueAsString))
                return true;

            if (valueAsString.IndexOfAny(System.Environment.NewLine.ToCharArray()) != -1)
                return false; // Contains newlines, so can't be displayed even if it's small.

            if (graphics == null)
            {
                graphics = DataGridTableStyle.DataGrid.CreateGraphics();
            }

            SizeF requiredSize = graphics.MeasureString(valueAsString,
                DataGridTableStyle.DataGrid.Font);
            return (requiredSize.Width < bounds.Width);
        }

	    private void PaintButtonIfNeeded(Graphics g, ref Rectangle bounds, CurrencyManager source, int rowNum)
		{
			if (ShouldShowButton(GetProperty(source, rowNum), bounds, g))
			{
				ButtonState buttonState = (m_mouseDownOverEditButton && rowNum == DataGridTableStyle.DataGrid.CurrentCell.RowNumber ?
					ButtonState.Pushed : ButtonState.Normal);
				PaintButton(g, bounds, buttonState);

				bounds = new Rectangle(bounds.X + m_buttonWidth, bounds.Y, bounds.Width - m_buttonWidth, bounds.Height);
			}
		}

		private void EditButtonClicked()
		{
			Debug.Assert(m_currentValue != null, "m_currentValue != null");

			Type editorType = ((CellEditorData)m_currentValue.Tag).GetEditorType(EditorManager);
			Debug.Assert(editorType != null, "Edit button clicked, but there is no editor for value type '"
				+ m_currentValue.ValueTypeName + "'.");

			if (m_editorDialog == null)
			{
				m_editorDialog = new EditorDialog(MessageBoxButtons.OKCancel);
			}
			if (m_editorDialogSettings != null)
			{
				m_editorDialogSettings.ApplyToWindow(m_editorDialog);
			}
			m_editorDialog.ReadOnly = IsReallyReadOnly(m_currentValue);
			m_editorDialog.CurrentEditorType = editorType;

			// Keep the value in the remote AppDomain, if the editor supports this.

			if (m_editorDialog.IsRemoteEditor)
			{
				m_editorDialog.DisplayRemoteValue(m_currentValue.CreateWrapper());
			}
			else
			{
				m_editorDialog.DisplayValue(m_currentValue.GetValue());
			}

			m_editorDialog.FocusOnEditor(); // Set focus to the editor every time the dialog is shown.

			if (m_editorDialog.ShowDialog(DataGridTableStyle.DataGrid) == DialogResult.OK)
			{
				m_currentValue.SetValue(m_editorDialog.GetValue());
				OnEditorCommitted(new MemberWrapperEventArgs(m_currentValue));
			}

			if (m_editorDialogSettings == null)
			{
				m_editorDialogSettings = new WindowSettings();
			}
			m_editorDialogSettings.ReadFromWindow(m_editorDialog);
		}

		private void DataGrid_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				// If the mouse was held down over the button it's now been released, so paint the button in
				// normal state (not pused).

				if (m_mouseDownOverEditButton)
				{
					m_mouseDownOverEditButton = false;
					PaintButton(DataGridTableStyle.DataGrid.CreateGraphics(), m_editButtonBounds, ButtonState.Normal);
				}

				// If the mouse was released over the button then perform the click.

				if (m_editButtonBounds.Contains(e.X, e.Y))
				{
					EditButtonClicked();
				}
			}
		}

		private void DataGrid_AfterMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (m_editButtonBounds.Contains(e.X, e.Y))
				{
					m_mouseDownOverEditButton = true;
					PaintButton(DataGridTableStyle.DataGrid.CreateGraphics(), m_editButtonBounds, ButtonState.Pushed);
				}
			}
		}

		private void DataGrid_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && m_mouseDownOverEditButton)
			{
				ButtonState state = (m_editButtonBounds.Contains(e.X, e.Y) ? ButtonState.Pushed : ButtonState.Normal);
				PaintButton(DataGridTableStyle.DataGrid.CreateGraphics(), m_editButtonBounds, state);
			}
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			// "Click" the edit button when the user presses INS in this column.

			if (e.KeyCode == Keys.Insert && e.Modifiers == Keys.None && !m_editButtonBounds.IsEmpty)
			{
				int currentColumn = DataGridTableStyle.DataGrid.CurrentCell.ColumnNumber;
				int thisColumn = DataGridTableStyle.GridColumnStyles.IndexOf(this);
				if (currentColumn == thisColumn)
				{
					EditButtonClicked();
				}
			}
		}
	}
}

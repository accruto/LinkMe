using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using LinkMe.Framework.Tools.Settings;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An enhanced DataGrid control.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DataGrid : System.Windows.Forms.DataGrid, IReadOnlySettable
	{
		#region Nested types

		/// <summary>
		/// Stores the column widths for a DataGrid, keyed by the mapping name.
		/// </summary>
		public class ColumnWidths : ISettingsObject
		{
			private ListDictionary m_columns = new ListDictionary(); // <MappingName (string), width (int)>

			public ColumnWidths()
			{
			}

			internal ColumnWidths(DataGridTableStyle tableStyle)
			{
				Debug.Assert(tableStyle != null, "tableStyle != null");

				foreach (DataGridColumnStyle column in tableStyle.GridColumnStyles)
				{
					if (column.MappingName != null)
					{
						m_columns.Add(column.MappingName, column.Width);
					}
				}
			}

			#region ISettingsObject Members

			public bool SettingsEqual(ISettingsObject obj)
			{
				ColumnWidths other = obj as ColumnWidths;
				if (other == null)
					return false;

				if (m_columns.Count != other.m_columns.Count)
					return false;

				IDictionaryEnumerator enumerator = m_columns.GetEnumerator();
				while (enumerator.MoveNext())
				{
					object otherValue = other.m_columns[enumerator.Key];
					if (otherValue == null || (int)enumerator.Value != (int)otherValue)
						return false;
				}

				return true;
			}

			public void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix,
				string readingFromPath)
			{
				m_columns.Clear();

				XmlNodeList xmlColumnList = xmlSetting.SelectNodes(xmlPrefix + "column", xmlNsManager);
				foreach (XmlNode xmlColumn in xmlColumnList)
				{
					XmlNode xmlName = xmlColumn.Attributes["name"];
					if (xmlName == null)
						throw new System.ApplicationException("The 'column' element is missing the 'name' attribute.");

					int width = XmlConvert.ToInt32(xmlColumn.InnerText);
					m_columns.Add(xmlName.Value, width);
				}
			}

			public void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
			{
				writer.WriteStartElement(writer.LookupPrefix(xmlns), "columnWidths", xmlns);
				WriteXmlSettingContents(writer, xmlns, writingToPath);
				writer.WriteEndElement();
			}

			public void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
			{
				string prefix = writer.LookupPrefix(xmlns);

				IDictionaryEnumerator enumerator = m_columns.GetEnumerator();
				while (enumerator.MoveNext())
				{
					writer.WriteStartElement(prefix, "column", xmlns);
					writer.WriteAttributeString("name", (string)enumerator.Key);
					writer.WriteString(XmlConvert.ToString((int)enumerator.Value));
					writer.WriteEndElement();
				}
			}

			#endregion

			internal void Apply(DataGridTableStyle tableStyle)
			{
				Debug.Assert(tableStyle != null, "tableStyle != null");

				IDictionaryEnumerator enumerator = m_columns.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataGridColumnStyle column = tableStyle.GridColumnStyles[(string)enumerator.Key];
					if (column != null)
					{
						column.Width = (int)enumerator.Value;
					}
				}
			}
		}

		#endregion

		public event MouseEventHandler AfterMouseDown;

		private const bool m_defaultAutoResizeLastColumn = false;

		private bool m_autoResizeLastColumn = m_defaultAutoResizeLastColumn;
		private ToolTip m_toolTip = new ToolTip();
		private bool m_showTipsForTextColumns = true;
		private DataGridCell m_lastToolTipCell = new DataGridCell(-1, -1);
		private DataGridTableStyle m_currentTableStyle = null;
		private bool m_multiSelectRow = true;
		private int m_oldSelectedRow = -1;

		public DataGrid()
		{
		}

		/// <summary>
		/// Indicates whether the right-most column is automatically resized to fill all available space.
		/// The default is false.
		/// </summary>
		[DefaultValue(m_defaultAutoResizeLastColumn)]
		public bool AutoResizeLastColumn
		{
			get { return m_autoResizeLastColumn; }
			set { m_autoResizeLastColumn = value; }
		}

		/// <summary>
		/// Indicates whether multiple rrows can be selected at a time.
		/// The default is true.
		/// </summary>
		[DefaultValue(true)]
		public bool MultiSelectRow
		{
			get { return m_multiSelectRow; }
			set { m_multiSelectRow = value; }
		}

		/// <summary>
		/// Indicates whether tooltips are shown for text columns. The default is true.
		/// </summary>
		[DefaultValue(true)]
		public bool ShowToolTipsForTextColumns
		{
			get { return m_showTipsForTextColumns; }
			set { m_showTipsForTextColumns = value; }
		}

		/// <summary>
		/// The table style in the TableStyles collection that is currently being used by the DataGrid or null if
		/// the default table style is being used.
		/// </summary>
		[Browsable(false)]
		public DataGridTableStyle CurrentTableStyle
		{
			get { return m_currentTableStyle; }
		}

		[Browsable(false)]
		public CurrencyManager CurrencyManager
		{
			get { return GetCurrencyManager(this); }
		}

		private int BorderSize
		{
			get
			{
				switch (BorderStyle)
				{
					case BorderStyle.None:
						return 2;

					case BorderStyle.FixedSingle:
					case BorderStyle.Fixed3D:
						return 6;

					default:
						Debug.Fail("Unexpected value of BorderStyle: " + BorderStyle.ToString());
						return 0;
				}
			}
		}

		internal static CurrencyManager GetCurrencyManager(System.Windows.Forms.DataGrid grid)
		{
			object dataSource = grid.DataSource;
			return (dataSource == null ? null : (CurrencyManager)grid.BindingContext[dataSource, grid.DataMember]);
		}

		/// <summary>
		/// Returns the text value of the specified cell if its column style is DataGridTextBoxColumn, otherwise
		/// returns null.
		/// </summary>
		public string GetTextForCell(DataGridCell cell)
		{
			DataGridTableStyle tableStyle = CurrentTableStyle;
			if (tableStyle == null)
				return null;

			return GetTextForCell(cell, tableStyle.GridColumnStyles[cell.ColumnNumber] as DataGridTextBoxColumn);
		}

		/// <summary>
		/// Persists the current column widths in the DataGrid to a <see cref="ColumnWidths"/> object.
		/// </summary>
		public ColumnWidths SaveColumnWidths()
		{
			DataGridTableStyle tableStyle = CurrentTableStyle;
			return (tableStyle == null ? null : new ColumnWidths(tableStyle));
		}

		/// <summary>
		/// Reads column widths from a <see cref="ColumnWidths"/> object and applies them to the DataGrid.
		/// </summary>
		public void RestoreColumnWidths(ColumnWidths widths)
		{
			if (widths == null)
				return;

			DataGridTableStyle tableStyle = CurrentTableStyle;
			if (tableStyle == null)
				return;

			widths.Apply(tableStyle);
		}

		/// <summary>
		/// If the DataGrid is currently being edited - ends the edit and commits the changes.
		/// </summary>
		/// <remarks>
		/// The base DataGrid cclass does something like this when it loses focus (the OnLeave() method). This
		/// is the closest approximation of what OnLeave() does without calling private methods or raising the
		/// Leave event.
		/// </remarks>
		public void EndEdit()
		{
			EndEdit(null, -1, false); // The first two parameters are completely ignored!

			CurrencyManager currencyManager = CurrencyManager;
			if (currencyManager != null)
			{
				currencyManager.EndCurrentEdit();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (m_toolTip != null)
				{
					m_toolTip.Dispose();
					m_toolTip = null;
				}
			}

			base.Dispose(disposing);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			// DoAutoResizeLastColumn() must be done after base.OnResize so that the value of
			// VertScrollBar.Visible is correct.

			DoAutoResizeLastColumn();
		}

		protected override void OnDataSourceChanged(EventArgs e)
		{
			// Set the current table style.

			m_currentTableStyle = GetCurrentTableStyle();

			// .NET BUG: Workaround for a .NET DataGrid bug - without the code below if a read-only cell is selected
			// it remains visible and selected after the whole rowset is redisplayed - even after a complete repaint.

			foreach (Control childControl in Controls)
			{
				childControl.Hide();
			}

			// Resize the last column now that some data is displayed.

			DoAutoResizeLastColumn();

			base.OnDataSourceChanged(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ( !m_multiSelectRow )
			{
				// Don't call the base class if in row header.
 
				DataGrid.HitTestInfo hti = HitTest(new Point(e.X, e.Y));
				if ( hti.Type == DataGrid.HitTestType.Cell )
				{
					if ( m_oldSelectedRow > -1 )
						UnSelect(m_oldSelectedRow);
					m_oldSelectedRow = -1;
 					base.OnMouseDown(e);
 				}
				else if ( hti.Type == DataGrid.HitTestType.RowHeader )
				{
					if ( m_oldSelectedRow > -1 )
						UnSelect(m_oldSelectedRow);
					if ( (Control.ModifierKeys & Keys.Shift) == 0 )
						base.OnMouseDown(e);
					else
						CurrentCell = new DataGridCell(hti.Row, hti.Column);
					Select(hti.Row);
					m_oldSelectedRow = hti.Row;
				}
			}
			else
			{
				base.OnMouseDown(e);
			}

			OnAfterMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ( !m_multiSelectRow )
			{
				// Don't call the base class if left mouse down.
 
				if ( e.Button != MouseButtons.Left )
					base.OnMouseMove(e);
			}
			else
			{
				base.OnMouseMove(e);
			}

			ShowToolTipForCurrentCell(e.X, e.Y);
		}
		
		protected override void OnLayout(LayoutEventArgs levent)
		{
			// OnLayout seems to be the only way to resize the last column after the user has manually resized
			// a column with the mouse.

			base.OnLayout(levent);

			if (DoAutoResizeLastColumn())
			{
				// If the column width was actually changed we need to do the layout again, otherwise we get
				// problems like the horizontal scrollbar remaining.

				base.OnLayout(levent);
			}
		}

		protected virtual void OnAfterMouseDown(MouseEventArgs e)
		{
			if (AfterMouseDown != null)
			{
				AfterMouseDown(this, e);
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// .NET BUG: If the same DataGridStyle object is assigned to two DataGrids things seem to work,
			// but then the rows of one DataGrids can get assigned to the other one! At least throw a meaningful
			// exception when this occurs.

			if (m_currentTableStyle != null && m_currentTableStyle.DataGrid != null && m_currentTableStyle.DataGrid != this)
			{
				throw new System.ApplicationException(string.Format("A table style with mapping name '{0}' has"
					+ " been set as the current table style for DataGrid '{1}', but it is already assigned to"
					+ " DataGrid '{2}'. Ensure that no two DataGrids share the same DataGridTableStyle object.",
					m_currentTableStyle.MappingName, Name, m_currentTableStyle.DataGrid.Name));
			}

			try
			{
				base.OnPaint(pe);
			}
			catch (AppDomainUnloadedException ex)
			{
				// Ignore this exception, as the control may be asked to paint with an old value in an
				// AppDomain that has been unloaded before the caller sets a new (valid) value.
				Debug.WriteLine("DataGrid.OnPaint(): " + ex.Message);
			}
		}

		internal void SetToolTip(string text)
		{
			// Need to clear the tooltip first to cause a delay, consistent with how tooltips usually behave.

			m_toolTip.SetToolTip(this, string.Empty);

			if (text != null && text.Length != 0)
			{
				m_toolTip.SetToolTip(this, text);
			}
		}

		private string GetTextForCell(DataGridCell cell, System.Windows.Forms.DataGridTextBoxColumn columnStyle)
		{
			// This method emulates the behaviour of DataGridTextBoxColumn.GetText(). It's used to set the tooltip,
			// so we have to make sure the tooltip shows the same text as the cell.

			if (columnStyle == null)
				return null;

			object value;
			try
			{
				value = this[cell];
			}
			catch (IndexOutOfRangeException)
			{
				return null; // The mouse must be over the "add new row" row.
			}

			if (value == null)
				return string.Empty; // Should probably be NullText, but this is what DataGridTextBoxColumn does.
			else if (value is DBNull)
				return columnStyle.NullText;
			else if (columnStyle.Format != null && columnStyle.Format.Length != 0 && value is IFormattable)
			{
				try
				{
					return ((IFormattable)value).ToString(columnStyle.Format, columnStyle.FormatInfo);
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
			else
			{
				if (columnStyle.PropertyDescriptor != null
					&& columnStyle.PropertyDescriptor.PropertyType != typeof(object))
				{
					TypeConverter typeConverter = TypeDescriptor.GetConverter(columnStyle.PropertyDescriptor.PropertyType);
					if (typeConverter != null && typeConverter.CanConvertTo(typeof(string)))
						return (string)typeConverter.ConvertTo(value, typeof(string));
				}

				return value.ToString();
			}
		}

		private void ShowToolTipForCurrentCell(int x, int y)
		{
			HitTestInfo hitInfo = HitTest(x, y);
			Debug.Assert(hitInfo != null, "hitInfo != null");

			string cellText = string.Empty;
			bool overSameCell = false;

			if (hitInfo.Type == HitTestType.Cell)
			{
				DataGridCell hitCell = new DataGridCell(hitInfo.Row, hitInfo.Column);

				overSameCell = hitCell.Equals(m_lastToolTipCell);
				m_lastToolTipCell = hitCell;

				// Get the column style and text for the cell under the mouse pointer.

				DataGridTableStyle tableStyle = CurrentTableStyle;
				if (tableStyle != null)
				{
					DataGridColumnStyle columnStyle = tableStyle.GridColumnStyles[hitCell.ColumnNumber];

					ICellToolTip tooltipColumn = columnStyle as ICellToolTip;
					if (tooltipColumn != null)
					{
						// This column can provide its own tool tip. Make the hit coordinates relative to the cell.

						Rectangle cellBounds = GetCellBounds(hitCell);
						cellText = tooltipColumn.GetCellToolTip(x - cellBounds.X , y - cellBounds.Y,
							CurrencyManager, hitCell);
					}

					if (tooltipColumn == null || (ShowToolTipsForTextColumns && cellText == null))
					{
						System.Windows.Forms.DataGridTextBoxColumn textColumn = columnStyle as System.Windows.Forms.DataGridTextBoxColumn;
						if (textColumn != null)
						{
							cellText = GetToolTipForTextColumn(x, y, hitCell, textColumn);
						}
					}
				}
			}
			else
			{
				m_lastToolTipCell = new DataGridCell(-1, -1);
			}

			// If the tooltip text hasn't changed AND the mouse is over the same cell as before then
			// don't make any changes. This check has to be made - if SetToolTip() is called whenever the mouse
			// moves then no tooltip is displayed at all! If we check only for the same cell this doesn't allow
			// for column styles like DataGridEditorColumn where not all of the cell area is the text area. If
			// we check only the text then the tooltip doesn't move when the user moves the mouse from one cell
			// to another cell that happens to contain the same text.

			if (overSameCell && m_toolTip.GetToolTip(this) == cellText)
				return;

			SetToolTip(cellText);
		}

		/// <summary>
		/// Show a tool tip containing the text of the cell at the specified coordinates, but only if the text
		/// doesn't fit into the cell.
		/// </summary>
		private string GetToolTipForTextColumn(int x, int y, DataGridCell hitCell,
			System.Windows.Forms.DataGridTextBoxColumn columnStyle)
		{
			string cellText = string.Empty;

			// Check whether the hit was within the text area of the cell, if possible.

			DataGridTextBoxColumn customTextColumn = columnStyle as DataGridTextBoxColumn;
			if (customTextColumn != null)
			{
				Rectangle textBounds = customTextColumn.GetTextBounds(hitCell);
				if (textBounds.Contains(x, y))
				{
					cellText = GetTextForCell(hitCell, columnStyle);
				}
			}
			else
			{
				cellText = GetTextForCell(hitCell, columnStyle);
			}

			// Only display the tooltip if we have the text and it doesn't fit into the cell.

			if (cellText == null || (cellText.Length > 0 && DoesTextFitInCell(hitCell, cellText, columnStyle)))
			{
				cellText = string.Empty;
			}

			return cellText;
		}

		private bool DoesTextFitInCell(DataGridCell cell, string text,
			System.Windows.Forms.DataGridTextBoxColumn columnStyle)
		{
			Debug.Assert(text != null, "text != null");

			// If the supplied column style is our own derived DataGridTextBoxColumn class ask it for the text
			// bounds, otherwise use the cell bounds.

			DataGridTextBoxColumn customColumnStyle = columnStyle as DataGridTextBoxColumn;
			Rectangle textBounds = (customColumnStyle == null ? GetCellBounds(cell) :
				customColumnStyle.GetTextBounds(cell));

			SizeF size = new SizeF((float)textBounds.Width, (float)textBounds.Height);
			StringFormat format = new StringFormat(StringFormatFlags.NoWrap);
			int charsFitted;
			int linesFilled;

			CreateGraphics().MeasureString(text, Font, size, format, out charsFitted, out linesFilled);

			return (charsFitted == text.Length);
		}

		private bool DoAutoResizeLastColumn()
		{
			if (!AutoResizeLastColumn)
				return false;

			DataGridTableStyle tableStyle = CurrentTableStyle;
			if (tableStyle == null)
				return false;

			int width = ClientSize.Width - BorderSize + 2;
			if (VertScrollBar.Visible)
			{
				width -= VertScrollBar.Width;
			}

			if (tableStyle.RowHeadersVisible)
			{
				width -= tableStyle.RowHeaderWidth;
			}

			int column = 0;
			while (column < tableStyle.GridColumnStyles.Count - 1)
			{
				width -= tableStyle.GridColumnStyles[column++].Width;
			}

			bool changing = (tableStyle.GridColumnStyles[column].Width != width);
			if (changing)
			{
				tableStyle.GridColumnStyles[column].Width = width;
			}

			return changing;
		}

		private DataGridTableStyle GetCurrentTableStyle()
		{
			if (ListManager == null)
				return null;

			ITypedList typedList = ListManager.List as ITypedList;
			string listName = (typedList == null ? ListManager.List.GetType().Name : typedList.GetListName(null));

			return TableStyles[listName];
		}
	}
}

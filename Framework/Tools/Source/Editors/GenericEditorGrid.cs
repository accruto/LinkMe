using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Tools.Settings;

using DataGrid = LinkMe.Framework.Tools.Controls.DataGrid;
using DataGridTextBoxColumn = LinkMe.Framework.Tools.Controls.DataGridTextBoxColumn;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A grid that can edit any object by accessing its fields and properties via Reflection, similar
	/// to the Visual Studio watch window. Custom editors (IEditor implementations) may be supplied for
	/// specific types using an EditorManager object.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "GenericEditorGrid.bmp")]
	public class GenericEditorGrid : Editor, ISupportInitialize
	{
		#region Nested types

		/// <summary>
		/// Stores the expanded rows for a GenericEditorGrid, keyed by the Name property of the underlying
		/// MemberWrapper object.
		/// </summary>
		public class RowTree
		{
			private string m_name;
			private RowTree m_parent;
			private Hashtable m_children = new Hashtable();

			internal RowTree(string name, RowTree parent)
			{
				m_name = name;
				m_parent = parent;
			}

			internal string Name
			{
				get { return m_name; }
			}

			internal RowTree Parent
			{
				get { return m_parent; }
			}

			internal RowTree this[string name]
			{
				get { return (RowTree)m_children[name]; }
			}

			internal void Add(RowTree row)
			{
				m_children.Add(row.Name, row);
			}

			internal void Remove(string name)
			{
				m_children.Remove(name);
			}
		}

		/// <summary>
		/// A class for handling events from MemberWrapper objects, which are in another AppDomain. The event
		/// handlers must be public, but we don't want to actually expose them, so use public methods on a
		/// private class.
		/// </summary>
		private class Handler : MarshalByRefObject
		{
			GenericEditorGrid m_parent;

			internal Handler(GenericEditorGrid parent)
			{
				m_parent = parent;
			}

			public override object InitializeLifetimeService()
			{
				return null;
			}

			// This event handler needs to be public to be serialized.
			public void ItemValueChanged(object sender, EventArgs e)
			{
				m_parent.m_itemValueChanged = (MemberWrapper)sender;
			}
		}

		#endregion

		public const EditorValueFormat DefaultValueFormat = EditorValueFormat.CSharp;

		private const string m_nameColumnMappingName = "DisplayName";
		private const string m_memberTypeColumnMappingName = "MemberTypeName";
        private const string m_valueTypeColumnMappingName = "ValueTypeDescription";
		private const string m_editorColumnMappingName = "ValueAsString";

		private GenericWrapper m_wrapper = null;
		private MemberWrappers m_properties = null;
		private Handler m_handler;
		private MemberWrapper m_itemValueChanged = null;
		private bool m_modified = false;
		private string m_nameColumnHeader = "Member";
		private bool m_showMemberType = true;
		private bool m_initialised = false;
		private EditorValueFormat m_valueFormat = DefaultValueFormat;

		private DataGrid grid;
		private System.ComponentModel.Container components = null;

		public GenericEditorGrid()
		{
			InitializeComponent();

			m_handler = new Handler(this);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}

				if (m_properties != null)
				{
					try
					{
						foreach (MemberWrapper property in m_properties)
						{
							property.ValueChanged -= new EventHandler(m_handler.ItemValueChanged);
						}
					}
					catch (AppDomainUnloadedException)
					{
					}

					m_properties = null;
				}
			}

			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grid = new LinkMe.Framework.Tools.Controls.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.AllowSorting = false;
			this.grid.AutoResizeLastColumn = true;
			this.grid.CaptionVisible = false;
			this.grid.DataMember = "";
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grid.Location = new System.Drawing.Point(0, 0);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(584, 368);
			this.grid.TabIndex = 0;
			this.grid.Leave += new System.EventHandler(this.grid_LeftCurrentCell);
			this.grid.CurrentCellChanged += new System.EventHandler(this.grid_LeftCurrentCell);
			// 
			// GenericEditorGrid
			// 
			this.Controls.Add(this.grid);
			this.Name = "GenericEditorGrid";
			this.Size = new System.Drawing.Size(584, 368);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region IEditor Members

		public override bool Modified
		{
			get { return m_modified; }
		}

		public override bool ReadOnly
		{
			set
			{
				base.ReadOnly = value;

				grid.ReadOnly = value;
			}
		}

		public override bool SupportsEditing
		{
			get { return true; }
		}

		public override void Clear()
		{
			m_modified = false;
			m_wrapper = null;
			m_properties = null;

			grid.SetDataBinding(null, null);
		}

		public override bool CanDisplay(Type type)
		{
			if (base.CanDisplay(type))
				return true;

			return typeof(GenericWrapper).IsAssignableFrom(type) || typeof(MemberWrappers).IsAssignableFrom(type);
		}

		public override void DisplayValue(object value)
		{
			CheckValue(value);

			if (value is GenericWrapper)
			{
				DisplayValue((GenericWrapper)value);
			}
			else if (value is MemberWrappers)
			{
				DisplayValue((MemberWrappers)value);
			}
			else
			{
				Debug.Fail("Unexpected type of value: " + value.GetType().FullName);
			}
		}

		public override object GetValue()
		{
			return (m_wrapper == null ? (object)m_properties : (object)m_wrapper);
		}

		#endregion

		#region ISupportInitialize Members

		public void BeginInit()
		{
			// Do nothing.
		}

		public void EndInit()
		{
			InitialiseDataGrid();
		}

		#endregion

		[DefaultValue(DefaultValueFormat)]
		public EditorValueFormat ValueFormat
		{
			get { return m_valueFormat; }
			set
			{
				m_valueFormat = value;

				if (m_properties != null)
				{
					foreach (MemberWrapper wrapper in m_properties)
					{
						wrapper.SetValueFormat(m_valueFormat);
					}
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EditorManager EditorManager
		{
			get { return ValueColumn.EditorManager; }
			set { ValueColumn.EditorManager = value; }
		}

		public string NameColumnHeaderText
		{
			get { return m_nameColumnHeader; }
			set
			{
				m_nameColumnHeader = value;

				// If the column has already been created update it.

				DataGridTableStyle tableStyle = grid.TableStyles[typeof(MemberWrappers).Name];
				if (tableStyle != null)
				{
					DataGridColumnStyle columnStyle = tableStyle.GridColumnStyles[m_nameColumnMappingName];
					if (columnStyle != null)
					{
						columnStyle.HeaderText = value;
					}
				}
			}
		}

		public bool ShowMemberTypeColumn
		{
			get { return m_showMemberType; }
			set { m_showMemberType = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WindowSettings EditorDialogSettings
		{
			get { return ValueColumn.EditorDialogSettings; }
			set { ValueColumn.EditorDialogSettings = value; }
		}

		private DataGridExpandableColumn NameColumn
		{
			get { return (DataGridExpandableColumn)grid.TableStyles[typeof(MemberWrappers).Name].GridColumnStyles[m_nameColumnMappingName]; }
		}

		private DataGridEditorColumn ValueColumn
		{
			get
			{
				if (!m_initialised)
				{
					throw new InvalidOperationException("The GenericEditorGrid cannot be used until after EndInit()"
						+ " is called.");
				}

				return (DataGridEditorColumn)grid.TableStyles[typeof(MemberWrappers).Name].GridColumnStyles[m_editorColumnMappingName];
			}
		}

		public void DisplayValue(GenericWrapper value)
		{
			if (value == null)
				throw new ArgumentNullException("value", "This editor cannot display a null value.");

			m_wrapper = value;
			DisplayValueInternal(m_wrapper.GetMembers());
		}

		public void DisplayValue(MemberWrappers values)
		{
			if (values == null)
				throw new ArgumentNullException("values", "This editor cannot display a null value.");

			m_wrapper = null;
			DisplayValueInternal(values);
		}

		public DataGrid.ColumnWidths SaveColumnWidths()
		{
			return grid.SaveColumnWidths();
		}

		public void RestoreColumnWidths(DataGrid.ColumnWidths widths)
		{
			grid.RestoreColumnWidths(widths);
		}

		public void SaveExpandedRows(ref RowTree rows)
		{
			CurrencyManager source = grid.CurrencyManager;
			if (source == null)
				return; // No data source.

			if (rows == null)
			{
				rows = new RowTree(null, null);
			}
			RowTree parent = rows;
			int currentLevel = 0;

			for (int index = 0; index < source.List.Count; index++)
			{
				MemberWrapper wrapper = (MemberWrapper)source.List[index];
				Debug.Assert(wrapper != null, "wrapper != null");
				CellEditorData cellData = (CellEditorData)wrapper.Tag;
				Debug.Assert(cellData != null, "cellData != null");

				while (cellData.Level < currentLevel)
				{
					Debug.Assert(parent.Parent != null, "parent.Parent != null");
					parent = parent.Parent;
					currentLevel--;
				}

				if (cellData.IsExpanded)
				{
					RowTree current = parent[wrapper.Name];
					if (current == null)
					{
						current = new RowTree(wrapper.Name, parent);
						parent.Add(current);
					}

					parent = current;
					currentLevel++;
				}
				else if (parent[wrapper.Name] != null)
				{
					parent.Remove(wrapper.Name);
				}
			}
		}

		public void RestoreExpandedRows(RowTree rows)
		{
			if (rows == null)
				return;

			CurrencyManager source = grid.CurrencyManager;
			if (source == null)
				return; // No data source.

			RowTree current = rows;
			int currentLevel = 0;

			for (int index = 0; index < source.List.Count; index++)
			{
				MemberWrapper wrapper = (MemberWrapper)source.List[index];
				Debug.Assert(wrapper != null, "wrapper != null");
				CellEditorData cellData = (CellEditorData)wrapper.Tag;
				Debug.Assert(cellData != null, "cellData != null");

				if (cellData.IsExpanded || cellData.Level > currentLevel)
					continue; // Already expanded or below an expanded node for which we don't have any children listed.

				while (cellData.Level < currentLevel)
				{
					current = current.Parent;
					currentLevel--;
				}

				RowTree expanded = current[wrapper.Name];
				if (expanded != null)
				{
					// Found it - expand this row.

					NameColumn.ExpandRow(source, index);

					// Look for child expanded rows (if any).

					current = expanded;
					currentLevel++;
				}
			}
		}

		private void DisplayValueInternal(MemberWrappers values)
		{
			m_modified = false;
			m_properties = values;

			foreach (MemberWrapper wrapper in m_properties)
			{
				wrapper.SetValueFormat(m_valueFormat);
				wrapper.Tag = new CellEditorData(wrapper);
				wrapper.ValueChanged += new EventHandler(m_handler.ItemValueChanged);
			}

			grid.SetDataBinding(m_properties, null);
		}

		private void ProcessItemValueChanged(MemberWrapper sender)
		{
			m_modified = true;

			// Set the "modified" flag in each property that's visible in the grid, then clear the cache.
			// (This needs to be done for all properties, because any of them may have changed as a result of one
			// value changing.) If the value was modified then RefreshIfExpanded() collapses and re-expands the
			// row to show the new value (which may have a different number of child properties than the old value).

			foreach (MemberWrapper wrapper in m_properties)
			{
				wrapper.SetModified();
				wrapper.ClearCache();
			}
			sender.Modified = true;

			// Check all the expanded members. Any one of them may now have completely different members,
			// so they need to be collapsed and re-expanded (unless the value was cahnged to null).

			DataGridExpandableColumn column = NameColumn;

			// Cannot use foreach here, because some elements may be removed from the collection as
			// we're iterating over it.

			for (int index = 0; index < m_properties.Count; index++)
			{
				MemberWrapper wrapper = (MemberWrapper)m_properties[index];
				column.RefreshIfExpanded((CellEditorData)wrapper.Tag);
			}

			// Invalidate the column to hide any + buttons that should no longer be shown and re-display
			// the values.

			column.Invalidate();
		}

		private void InitialiseDataGrid()
		{
			DataGridTableStyle tableStyle = new DataGridTableStyle();

			DataGridExpandableColumn nameColumn = new DataGridExpandableColumn();
			nameColumn.ReadOnly = true;
			nameColumn.MappingName = m_nameColumnMappingName;
			nameColumn.HeaderText = NameColumnHeaderText;
			nameColumn.Width = 100;
			nameColumn.Expand += new MemberWrapperEventHandler(nameColumn_Expand);
			nameColumn.Collapse += new MemberWrapperEventHandler(nameColumn_Collapse);
			tableStyle.GridColumnStyles.Add(nameColumn);

			if (ShowMemberTypeColumn)
			{
				DataGridTextBoxColumn memberTypeColumn = new DataGridTextBoxColumn();
				memberTypeColumn.ReadOnly = true;
				memberTypeColumn.MappingName = m_memberTypeColumnMappingName;
				memberTypeColumn.HeaderText = "Member Type";
				memberTypeColumn.Width = 150;
				tableStyle.GridColumnStyles.Add(memberTypeColumn);
			}

			DataGridTextBoxColumn valueTypeColumn = new DataGridTextBoxColumn();
			valueTypeColumn.ReadOnly = true;
			valueTypeColumn.MappingName = m_valueTypeColumnMappingName;
			valueTypeColumn.HeaderText = "Value Type";
			valueTypeColumn.Width = 150;
			tableStyle.GridColumnStyles.Add(valueTypeColumn);

			DataGridEditorColumn editorColumn = new DataGridEditorColumn();
			editorColumn.MappingName = m_editorColumnMappingName;
			editorColumn.HeaderText = "Value";
			editorColumn.NullText = "<null>";
			tableStyle.GridColumnStyles.Add(editorColumn);

			tableStyle.HeaderForeColor = SystemColors.ControlText;
			tableStyle.RowHeadersVisible = false;
			tableStyle.MappingName = typeof(MemberWrappers).Name;
			tableStyle.DataGrid = this.grid;
			grid.TableStyles.Add(tableStyle);

			nameColumn.Initialise();
			editorColumn.Initialise();
			editorColumn.EditorCommitted += new MemberWrapperEventHandler(editorColumn_EditorCommitted);

			m_initialised = true;
		}

		private void nameColumn_Expand(object sender, MemberWrapperEventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				int index = m_properties.IndexOf(e.Wrapper);
				Debug.Assert(index != -1, "index != -1");
				Debug.Assert(!e.Wrapper.ValueIsNullOrUnavailable, "Trying to expand a null or unavailable value.");

				CellEditorData cellData = (CellEditorData)e.Wrapper.Tag;
				Debug.Assert(!cellData.IsExpanded, "Expanding an already expanded row.");

				// Create a new GenericWrapper object for the expanded value and get its properties.

				GenericWrapper genericWrapper = e.Wrapper.CreateWrapper();
				Debug.Assert(genericWrapper != null, "CreateWrapper() returned null for the member being expanded.");
				cellData.Children = genericWrapper.GetMembers();

				foreach (MemberWrapper childProperty in cellData.Children)
				{
					childProperty.SetValueFormat(m_valueFormat);
					childProperty.Tag = new CellEditorData(childProperty, cellData.Level + 1, cellData);
					childProperty.ValueChanged += new EventHandler(m_handler.ItemValueChanged);
					m_properties.Insert(++index, childProperty);
				}

				if (cellData.Children.Count == 0)
				{
					((DataGridExpandableColumn)sender).Invalidate();
				}
				else
				{
					((CellEditorData)cellData.Children[cellData.Children.Count - 1].Tag).IsLastInLevel = true;
				}
			}
		}

		private void nameColumn_Collapse(object sender, MemberWrapperEventArgs e)
		{
			int index = m_properties.IndexOf(e.Wrapper);
			Debug.Assert(index != -1, "index != -1");

			CellEditorData cellData = (CellEditorData)e.Wrapper.Tag;
			Debug.Assert(cellData.IsExpanded, "Collapsing a row that is not expanded.");

			int totalRows = cellData.TotalExpandedRows;
			for (int i = 1; i <= totalRows; i++)
			{
				m_properties[index + i].ValueChanged -= new EventHandler(m_handler.ItemValueChanged);
			}

			m_properties.RemoveRange(index + 1, totalRows);
			cellData.Children = null;
		}

		/// <summary>
		/// Handles CurrentCellChanged and Leave events for the DataGrid.
		/// </summary>
		private void grid_LeftCurrentCell(object sender, EventArgs e)
		{
			if (m_itemValueChanged != null)
			{
				// The ItemValueChanged changed event is actually processed here, after the current cell changes.
				// Otherwise row numbers change while the DataGrid is committing changes to the data source and
				// it gets confused.

				MemberWrapper item = m_itemValueChanged;
				m_itemValueChanged = null; // Current cell may change again in ProcessItemValueChanged, so set to null now.

				ProcessItemValueChanged(item);
			}
		}

		private void editorColumn_EditorCommitted(object sender, MemberWrapperEventArgs e)
		{
			// When the a value is changed in an editor refresh the grid immediately - the change is already
			// committed, so there's no need to wait for the current grid cell to change. Also raise the
			// ListChanged event, otherwise if the cell being edited is not expanded it won't be updated
			// until the user moves away from it.

			m_properties.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged,
				m_properties.IndexOf(e.Wrapper)));

			m_itemValueChanged = null;
			ProcessItemValueChanged(e.Wrapper);
		}
	}
}

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.ObjectProperties;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// The base class for all MMC snap-in nodes.
	/// </summary>
	public abstract class SnapinNode
	{
		private const char PathSeparatorChar = '\\';

		protected SnapinNode(Snapin snapin)
		{
			m_snapin = snapin;
			m_parentNode = this;
			m_cookie = snapin.Register(this);
			m_hScopeItem = IntPtr.Zero;
            m_parentHScopeItem = IntPtr.Zero;
			m_childNodes = new ArrayList(8);
			m_nodeSelected = false;
			m_control = new NodeControl(this);
			
			// Add images.
			
			m_closedImageIndex = snapin.AddImage(Constants.Icon.DefaultClosed);
			m_openImageIndex = snapin.AddImage(Constants.Icon.DefaultOpen);
		}

		public Snapin Snapin
		{
			get { return m_snapin; }
		}

		public Guid Guid
		{
			get { return m_guid; }
			set { m_guid = value; }
		}

		public IntPtr HScopeItem
		{
			get { return m_hScopeItem; }
			set { m_hScopeItem = value; }
		}

		internal IntPtr Cookie
		{
			get { return m_cookie; }
			set { m_cookie = value; }
		}

		public string DisplayName
		{
			get { return m_displayName; }
			set { m_displayName = value; }
		}

		private int ChildCount
		{
			get { return m_childNodes == null ?  0 : m_childNodes.Count; }
		}

		public bool AreMultipleResultsSelected
		{
			get { return m_areMultipleResultsSelected; }
			set { m_areMultipleResultsSelected = value; }
		}

		#region ContextMenu

		protected void AddTopMenuItem(ContextMenuItem item)
		{
			AddMenuItem(item);
			if ( m_topMenuItems == null )
				m_topMenuItems = new ArrayList();
			m_topMenuItems.Add(item);
		}

		protected void AddTaskMenuItem(ContextMenuItem item)
		{
			AddMenuItem(item);
			if ( m_taskMenuItems == null )
				m_taskMenuItems = new ArrayList();
			m_taskMenuItems.Add(item);
		}

		protected void AddViewMenuItem(ContextMenuItem item)
		{
			AddMenuItem(item);
			if ( m_viewMenuItems == null )
				m_viewMenuItems = new ArrayList();
			m_viewMenuItems.Add(item);
		}
        
		protected void AddSubMenuItem(ContextSubMenuItem item)
		{
			AddMenuItem(item);
			if ( m_subMenuItems == null )
				m_subMenuItems = new ArrayList();
			m_subMenuItems.Add(item);
		}

		protected void AddNewMenuItem(ContextMenuItem item)
		{
			if ( !IsReadOnly )
			{
				AddMenuItem(item);
				if ( m_newMenuItems == null )
					m_newMenuItems = new ArrayList();
				m_newMenuItems.Add(item);
			}
		}

		protected void AddMenuItem(ContextSubMenuItem subMenuItem, ContextMenuItem item)
		{
			AddMenuItem(item);
			subMenuItem.AddMenuItem(item);
		}

		protected void AddSubMenuItemAndChildren(ContextSubMenuItem item)
		{
			AddSubMenuItem(item);
			foreach ( ContextMenuItem childItem in item.MenuItems )
				AddMenuItem(childItem);
		}

		protected void AddTaskMenuItemAndChildren(ContextSubMenuItem item)
		{
			AddTaskMenuItem(item);
			foreach ( ContextMenuItem childItem in item.MenuItems )
				AddMenuItem(childItem);
		}

		protected void AddSeparatorMenuItem()
		{
			AddTopMenuItem(new SeparatorMenuItem());
		}

		protected void AddTaskSeparatorMenuItem()
		{
			AddTaskMenuItem(new SeparatorMenuItem());
		}

		internal virtual void OnMenuCommand(int commandId)
		{
			foreach ( ContextMenuItem item in m_menuItems )
			{
				if ( item.CommandId == commandId )
				{
					item.OnCommand(this);
					break;
				}
			}
		}

		internal void AddMenuItems(IContextMenuCallback piCallback, ref int pInsertionAllowed)
		{
			// Conditionally add menu items.

			if ( (pInsertionAllowed & (int) ContextMenuFlags.InsertionAllowedView) > 0 )
			{
				// Refresh first.

				RemoveViewMenuItems();
				if ( IsSelected )
					AddViewMenuItems();

				// Add the items.

				OnAddViewMenu(piCallback);
			}
			else
			{
				// Refresh first.

				RemoveMenuItems();
				if ( IsSelected )
					AddMenuItems();
				else
					AddResultMenuItems();

				// Add the items.

				if ( (pInsertionAllowed & (int) ContextMenuFlags.InsertionAllowedTop) > 0 )
				{
					OnAddTopMenu(piCallback);
					OnAddSubMenu(piCallback);
				}

				if ( (pInsertionAllowed & (int) ContextMenuFlags.InsertionAllowedNew) > 0 )
					OnAddNewMenu(piCallback);

				if ( (pInsertionAllowed & (int) ContextMenuFlags.InsertionAllowedTask) > 0 )
					OnAddTaskMenu(piCallback);
			}
		}

		private void AddMenuItem(ContextMenuItem item)
		{
			if ( m_menuItems == null )
				m_menuItems = new ArrayList();
			m_menuItems.Add(item);
			item.CommandId = ++m_commandId;
		}
		
		private void RemoveMenuItem(ContextMenuItem item)
		{
			if ( m_menuItems != null )
			{
				ContextSubMenuItem subMenuItem = item as ContextSubMenuItem;
				if ( subMenuItem != null )
				{
					// Remove all sub-items first.

					foreach ( ContextMenuItem subItem in subMenuItem.MenuItems )
						RemoveMenuItem(subItem);
				}

				m_menuItems.Remove(item);
			}
		}
		
		private void OnAddSubMenu(IContextMenuCallback piCallback)
		{
			if ( m_subMenuItems != null )
			{
				foreach ( ContextSubMenuItem subMenuItem in m_subMenuItems )
				{
					if ( subMenuItem.Visible )
					{
						// Add the popup.

						AddItem(piCallback, subMenuItem, InsertionPointId.PrimaryTop);

						// Iterate through each sub menu.

						foreach ( ContextMenuItem menuItem in subMenuItem.MenuItems )
						{
							if ( menuItem.Visible )
								AddItem(piCallback, subMenuItem, menuItem);
						}
					}
				}
			}
		}

		private void OnAddTopMenu(IContextMenuCallback piCallback)
		{
			OnAddMenu(piCallback, m_topMenuItems, InsertionPointId.PrimaryTop);
		}

		private void OnAddNewMenu(IContextMenuCallback piCallback)
		{
			OnAddMenu(piCallback, m_newMenuItems, InsertionPointId.PrimaryNew);
		}

		private void OnAddTaskMenu(IContextMenuCallback piCallback)
		{
			OnAddMenu(piCallback, m_taskMenuItems, InsertionPointId.PrimaryTask);
		}

		private void OnAddViewMenu(IContextMenuCallback piCallback)
		{
			OnAddMenu(piCallback, m_viewMenuItems, InsertionPointId.PrimaryView);
		}

		private void OnAddMenu(IContextMenuCallback piCallback, ArrayList menuItems, InsertionPointId insertionPointId)
		{
			if ( menuItems != null )
			{
				foreach ( ContextMenuItem item in menuItems )
				{
					if ( item.Visible )
					{
						ContextSubMenuItem subMenuItem = item as ContextSubMenuItem;
						if ( subMenuItem == null )
						{
							AddItem(piCallback, item, insertionPointId);
						}
						else
						{
							AddItem(piCallback, subMenuItem, insertionPointId);

							// Iterate through each sub menu.

							foreach ( ContextMenuItem menuItem in subMenuItem.MenuItems )
							{
								if ( menuItem.Visible )
									AddItem(piCallback, subMenuItem, menuItem);
							}
						}
					}
				}
			}
		}

		private void AddItem(IContextMenuCallback piCallback, ContextMenuItem menuItem, InsertionPointId insertionPointId)
		{
			MmcContextMenuItem newItem = new MmcContextMenuItem();
			newItem.Name = menuItem.Name;
			newItem.StatusBarText = menuItem.StatusText;
			newItem.CommandId = menuItem.CommandId;
			newItem.InsertionPointId = (uint) insertionPointId;

			if ( menuItem.IsSeparator )
				newItem.Flags = (int) ContextMenuFlags.Separator;
			else
				newItem.Flags = (menuItem.IsChecked ? (int) ContextMenuFlags.Checked : (int) ContextMenuFlags.Unchecked)
					| (menuItem.IsEnabled ? (int) ContextMenuFlags.Enabled : (int) (ContextMenuFlags.Grayed | ContextMenuFlags.Disabled));

			newItem.SpecialFlags = menuItem.IsDefault ? (int) ContextMenuFlags.SpecialDefaultItem : 0;
			piCallback.AddItem(ref newItem);
		}

		private void AddItem(IContextMenuCallback piCallback, ContextSubMenuItem subMenuItem, InsertionPointId insertionPointId)
		{
			MmcContextMenuItem item = new MmcContextMenuItem();
			item.Name = subMenuItem.Name;
			item.StatusBarText = subMenuItem.StatusText;
			item.CommandId = subMenuItem.CommandId;
			item.InsertionPointId = (uint) insertionPointId;
			item.Flags = (int) ContextMenuFlags.Popup;
			item.SpecialFlags = 0;
			piCallback.AddItem(ref item);
		}

		private void AddItem(IContextMenuCallback piCallback, ContextSubMenuItem subMenuItem, ContextMenuItem menuItem)
		{
			MmcContextMenuItem item = new MmcContextMenuItem();
			item.Name = menuItem.Name;
			item.StatusBarText = menuItem.StatusText;
			item.CommandId = menuItem.CommandId;
			item.InsertionPointId = (uint) subMenuItem.CommandId;
			item.Flags = 0;
			item.SpecialFlags = 0;
			piCallback.AddItem(ref item);
		}

		private void RemoveMenuItems()
		{
			RemoveMenuItems(m_topMenuItems);
			RemoveMenuItems(m_newMenuItems);
			RemoveMenuItems(m_subMenuItems);
			RemoveMenuItems(m_taskMenuItems);
		}

		private void RemoveViewMenuItems()
		{
			RemoveMenuItems(m_viewMenuItems);
		}

		private void RemoveMenuItems(ArrayList menuItems)
		{
			if ( menuItems != null )
			{
				foreach( ContextMenuItem item in menuItems )
					RemoveMenuItem(item);
				menuItems.Clear();
			}
		}

		protected virtual void EnableVerbs()
		{
		}

		protected void EnableDelete()
		{
			SetVerbState((MmcConsoleVerb) ConsoleVerb.Delete, (MmcButtonState) ButtonState.Enabled, true);
		}

		protected void EnableProperties()
		{
			// Only enable if one thing is selected, either the node itself or one result.

			if ( IsSelected || !AreMultipleResultsSelected )
			{
				SetVerbState((MmcConsoleVerb) ConsoleVerb.Properties, (MmcButtonState) ButtonState.Enabled, true);

				// Make Properties the default verb unless a default has already been set.

				if ( GetDefaultVerb() == (MmcConsoleVerb) ConsoleVerb.Open )
					SetDefaultVerb((MmcConsoleVerb) ConsoleVerb.Properties);
			}
		}

		protected void EnableRename()
		{
			// Only enable if one thing is selected, either the node itself or one result.

			if ( IsSelected || !AreMultipleResultsSelected )
			{
				SetVerbState((MmcConsoleVerb) ConsoleVerb.Rename, (MmcButtonState) ButtonState.Enabled, true);
			}
		}

		protected void EnableRefresh()
		{
			SetVerbState((MmcConsoleVerb) ConsoleVerb.Refresh, (MmcButtonState) ButtonState.Enabled, true);
		}

		#endregion

		#region Nodes

		private void ResetChildNodes()
		{
			DeleteChildNodes();
			AddChildNodes();
			InsertChildNodes();
		}

		private void DeleteChildNodes()
		{
			foreach ( SnapinNode node in m_childNodes )
			{
				// Delete the item and unregister the node.

				node.DeleteItem();
				Snapin.Unregister(node);
			}

			m_childNodes.Clear();
		}

		private void AddChildNodes()
		{
			using ( new LongRunningMonitor(Snapin) )
			{
				SnapinNode[] childNodes = GetChildNodes();
				if ( childNodes != null )
					m_childNodes.AddRange(childNodes);
			}
		}

		protected void DeleteChildNode(SnapinNode node)
		{
			// Delete the item and unregister the node.

			node.DeleteItem();
			Snapin.Unregister(node);
			RemoveChild(node);
		}

		protected void RenameChildNode(SnapinNode node, string newName)
		{
			// Update the display name.

			node.DisplayName = newName;
		}

		protected void InsertChildNode(SnapinNode node)
		{
			m_childNodes.Add(node);
			node.Insert(this);
		}

		private void InsertChildNode(SnapinNode node, IntPtr nextHScopeItem)
		{
			m_childNodes.Add(node);
			node.Insert(this, nextHScopeItem);
		}

		protected void ReplaceChildNode(SnapinNode node, SnapinNode newNode)
		{
			// Get information about the old node.

			IntPtr nextHScopeItem;
			IntPtr cookie;
			IConsoleNameSpace2 ns = Snapin.ConsoleNamespace;
			ns.GetNextItem(node.HScopeItem, out nextHScopeItem, out cookie);

			DeleteChildNode(node);

			if ( nextHScopeItem != IntPtr.Zero )
				InsertChildNode(newNode, nextHScopeItem);
			else
				InsertChildNode(newNode);
		}

		public string GetPathRelativeTo(SnapinNode relativeToNode)
		{
			if (relativeToNode == this)
				return string.Empty;
			else
			{
				return (m_parentNode == null || m_parentNode == this || m_parentNode == relativeToNode ?
					m_displayName : m_parentNode.GetPathRelativeTo(relativeToNode) + PathSeparatorChar + m_displayName);
			}
		}

		public SnapinNode FindNodeInTree(string path)
		{
			if (path == null)
				return null;
			else if (path == string.Empty)
				return this;

			string[] names = path.Split(PathSeparatorChar);

			SnapinNode node = this;
			int index = 0;
			while ( node != null && index < names.Length )
				node = node.FindNode(names[index++]);

			return node;
		}

		private void RemoveChild(SnapinNode node)
		{
			m_childNodes.Remove(node);
		}

		private void InsertChildNodes()
		{
			foreach (SnapinNode node in m_childNodes)
			{
				node.Insert(this);
			}
		}

		private void Insert(SnapinNode parent)
		{
			GetImageIndices();
			Snapin.EnsureImages();

			IConsoleNameSpace2 ns = Snapin.ConsoleNamespace;
			ScopeDataItem item = new ScopeDataItem();
			item.Mask = (uint) ScopeDataItemFlags.Str
				| (uint) ScopeDataItemFlags.Param
				| (uint) ScopeDataItemFlags.Parent
				| (uint) ScopeDataItemFlags.Image
				| (uint) ScopeDataItemFlags.OpenImage
				| (uint) ScopeDataItemFlags.Children;
		
			item.Image = m_closedImageIndex;
			item.OpenImage = m_openImageIndex;
			item.RelativeId  = parent.HScopeItem;
			item.DisplayName = (IntPtr)(-1);
			item.Param = Cookie;
			item.Children = HasChildren() ? 1 : 0;

			// Expand the parent node before adding the child.
			
			ns.Expand(parent.HScopeItem);
			ns.InsertItem(ref item);
			HScopeItem = item.Id;
			m_parentHScopeItem = parent.HScopeItem;
			m_parentNode = parent;
		}

		private void Insert(SnapinNode parent, IntPtr nextHScopeItem)
		{
			GetImageIndices();
			Snapin.EnsureImages();

			IConsoleNameSpace2 ns = Snapin.ConsoleNamespace;
			ScopeDataItem item = new ScopeDataItem();
			item.Mask = (uint) ScopeDataItemFlags.Str
				| (uint) ScopeDataItemFlags.Param
				| (uint) ScopeDataItemFlags.Next
				| (uint) ScopeDataItemFlags.Image
				| (uint) ScopeDataItemFlags.OpenImage
				| (uint) ScopeDataItemFlags.Children;
		
			item.Image = m_closedImageIndex;
			item.OpenImage = m_openImageIndex;
			item.RelativeId  = nextHScopeItem;
			item.DisplayName = (IntPtr)(-1);
			item.Param = Cookie;
			item.Children = HasChildren() ? 1 : 0;

			// Expand the parent node before adding the child.
			
			ns.Expand(parent.HScopeItem);
			ns.InsertItem(ref item);
			HScopeItem = item.Id;
			m_parentHScopeItem = parent.HScopeItem;
			m_parentNode = parent;
		}

		protected SnapinNode FindNode(string displayName)
		{
			// Add children if they haven't been added yet.

			if ( m_childNodes.Count == 0 )
				AddChildNodes();

			// Expand the node to insert children if necessary.

			if ( m_childNodes.Count > 0 && ((SnapinNode)m_childNodes[0]).HScopeItem == IntPtr.Zero )
				InsertChildNodes();

			foreach ( SnapinNode node in m_childNodes )
			{
				if ( node.DisplayName == displayName )
					return node;
			}

			return null;
		}

		public SnapinNode[] ChildNodes
		{
			get
			{
				// Add children if they haven't been added yet.

				if ( m_childNodes.Count == 0 )
					AddChildNodes();

				// Expand the node to insert children if necessary.

				if ( m_childNodes.Count > 0 && ((SnapinNode)m_childNodes[0]).HScopeItem == IntPtr.Zero )
					InsertChildNodes();

				return (SnapinNode[]) m_childNodes.ToArray(typeof(SnapinNode));
			}
		}

		public SnapinNode ParentNode
		{
			get { return m_parentNode; }
		}

		private void InvokeRefreshNode()
		{
			m_control.InvokeRefreshNode();
		}

		protected void InvokeRefreshResults()
		{
			m_control.InvokeRefreshNodeResults();
		}

		#endregion

		#region Items

		private void RefreshChildItems()
		{
			DeleteChildItems();
			InsertChildItems();
		}

		private void DeleteChildItems()
		{
			foreach ( SnapinNode node in m_childNodes )
				node.DeleteItem();
		}

		private void InsertChildItems()
		{
			foreach ( SnapinNode node in m_childNodes )
				node.Insert(this);
		}

		private void DeleteItem()
		{
			if ( HScopeItem != IntPtr.Zero )
				Snapin.ConsoleNamespace.DeleteItem(HScopeItem, 1);
		}

		#endregion

		#region Virtual Methods

		internal virtual void Initialize()
		{
		}

		protected virtual void AddMenuItems()
		{
		}

		protected virtual void AddViewMenuItems()
		{
		}

		protected virtual void AddResultMenuItems()
		{
		}

		protected virtual bool HasChildren()
		{
			return true;
		}

		protected virtual SnapinNode[] GetChildNodes()
		{
			return null;
		}

		protected virtual void Delete()
		{
			Debug.Fail("Delete was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");
		}

		protected virtual void Expand(bool duringStartup)
		{
		}

		protected virtual string GetDeletePromptText()
		{
			return "Are you sure you want to delete '" + m_displayName + "'?";
		}

		protected virtual void DeleteResults()
		{
		}

		protected virtual ObjectPropertyForm CreatePropertyForm()
		{
			Debug.Fail("CreatePropertyForm was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");

			return null;
		}

		protected virtual void ApplyProperties(object obj)
		{
			Debug.Fail("ApplyProperties was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");
		}

		protected virtual void Show()
		{
		}

		protected virtual void Hide()
		{
		}

		protected virtual void SelectResult()
		{
		}

		protected virtual void DeselectResult()
		{
		}

		protected virtual void SelectNode()
		{
		}

		protected virtual void DeselectNode()
		{
		}

		protected virtual bool Rename(string newName)
		{
			Debug.Fail("Rename was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");

			return false;
		}

		protected virtual bool RenameResult(string newName)
		{
			Debug.Fail("RenameResult was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");

			return false;
		}

		protected virtual string RefreshDisplayName()
		{
			return DisplayName;
		}

		public virtual bool IsReadOnly
		{
			get { return false; }
		}

		public virtual string GetStatusText()
		{
			return string.Empty;
		}

		#endregion

		private void DeleteNode()
		{
			try
			{
				// Prompt.

				if ( PromptForDelete() )
				{
					using ( new LongRunningMonitor(Snapin) )
					{
						// Let the derived class respond.

						Delete();

						// Remove from parent and remove from the display.

						m_parentNode.RemoveChild(this);
						Snapin.Unregister(this);
						DeleteItem();
					}
				}
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "Cannot delete '" + m_displayName + "'.").ShowDialog(Snapin);
			}
		}

		private bool PromptForDelete()
		{
			return Snapin.MessageBox(GetDeletePromptText(), "Confirm deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
		}

		#region Display

		protected bool IsRefresh
		{
			get { return m_isRefresh; }
		}

		public void RefreshNode()
		{
			ImageChanged();

			if ( m_snapin.CurrentScopeNode == this )
			{
				// Refresh the text of the node.

				DisplayName = RefreshDisplayName();

				m_isRefresh = true;
				try
				{
					Select();
				}
				finally
				{
					m_isRefresh = false;
				}
			}
		}

		public void RefreshResults()
		{
			if ( (m_snapin.CurrentScopeNode == this || m_snapin.CurrentScopeNode == m_parentNode) && m_snapin.ResultViewConsole != null )
			{
				// Refreshes the result pane.

				m_isRefresh = true;
				try
				{
					m_snapin.ResultViewConsole.SelectScopeItem(m_snapin.CurrentScopeNode.HScopeItem);
				}
				finally
				{
					m_isRefresh = false;
				}
			}
		}

		public void Refresh(bool childNodesChanged)
		{
			if ( childNodesChanged )
				ResetChildNodes();

			ImageChanged();

			if ( m_snapin.CurrentScopeNode == this )
			{
				Snapin.Refresh();
				RefreshChildItems();
				RefreshResults();
			}
			else
			{
				RefreshChildItems();
			}
		}

		public void Select()
		{
			if ( m_snapin.ResultViewConsole != null )
				m_snapin.ResultViewConsole.SelectScopeItem(HScopeItem);
		}

		protected virtual bool IsSelected
		{
			get { return m_nodeSelected; }
		}

		protected internal virtual bool IsResultSelected
		{
			get { return false; }
		}

		protected static string GetResultDisplay(Version version)
		{
			return version == null ? string.Empty : version.ToString();
		}

		protected static string GetYesNoResultDisplay(bool value)
		{
			return value ? "Yes" : "No";
		}

		protected internal virtual void OnRemovingNode()
		{
		}

		internal virtual void GetDisplayInfo(ref ResultDataItem item)
		{
			// Display name.

			if ( (item.Mask & (uint) ResultDataItemFlags.Str) > 0 )
				item.Str = Marshal.StringToCoTaskMemUni(m_displayName);
		
			// Image.

			if ( (item.Mask & (uint) ResultDataItemFlags.Image) > 0 )
				item.Image = ((int)Cookie << 16) + GetResultViewImageIndex();

			// Param.

			if ( (item.Mask & (uint) ResultDataItemFlags.Param) > 0 )
				item.Param = m_cookie;
		
			// Index.

			if ( (item.Mask & (uint) ResultDataItemFlags.Index) > 0 )
				item.Index = 0;
		            
			// Indent.

			if ( (item.Mask & (uint) ResultDataItemFlags.Indent) > 0 )
				item.Indent = 0;
		}

		internal void GetDisplayInfo(ref ScopeDataItem item)
		{
			// Display name.

			if ( (item.Mask & (uint) ScopeDataItemFlags.Str) > 0 )
				item.DisplayName = Marshal.StringToCoTaskMemUni(DisplayName);
		
			// State.

			if ( (item.Mask & (uint) ScopeDataItemFlags.State) > 0 )
				item.State = 0;

			// Image.

			if ( (item.Mask & (uint) ScopeDataItemFlags.Image) > 0 || (item.Mask & (uint) ScopeDataItemFlags.OpenImage) > 0 )
			{
				GetImageIndices();

				if ( (item.Mask & (uint) ScopeDataItemFlags.Image) > 0 )
					item.Image = m_closedImageIndex;
				if ( (item.Mask & (uint) ScopeDataItemFlags.OpenImage) > 0 )
					item.Image = m_openImageIndex;
			}
		
			// Children.

			if ( (item.Mask & (uint) ScopeDataItemFlags.Children) > 0 )
				item.Children = ChildCount;
		}

		internal virtual string GetResultViewType(ref int pViewOptions)
		{
			pViewOptions = 0;
			return string.Empty;		
		}

		private int GetResultViewImageIndex()
		{
			return IsUseSmallIcons() ? m_smallImageIndex : m_largeImageIndex;
		}

		protected bool IsUseSmallIcons()
		{
			IConsole2 console = Snapin.ResultViewConsole;
            if ( console != null )
			{
				IResultData resultdata = console as IResultData;
				if ( resultdata != null )
				{
					int viewType = 0;
					resultdata.GetViewMode(out viewType);
					return viewType != 0;
				}
			}
			
			// Default to small icons.

			return true;
		}

		#endregion

		#region Properties

		internal virtual void CreatePropertyForm(IPropertySheetCallback lpProvider, IntPtr handle)
		{
			// Create the property sheet on a separate thread if it doesn't already exist.

			if ( m_propertyForm == null )
			{
				Thread thread = new Thread(new ThreadStart(CreatePropertyFormThread));
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			else
			{
				m_propertyForm.Activate();
			}
		}

		private void CreatePropertyFormThread()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(Snapin.Application_ThreadException);

			m_propertyForm = CreatePropertyForm();
			if ( m_propertyForm != null )
			{
				m_propertyForm.Apply += new ApplyEventHandler(ApplyHandler);
				m_propertyForm.ShowDialog();
				m_propertyForm = null;
			}
		}

		private void ApplyHandler(object sender, ApplyEventArgs args)
		{
			ApplyProperties(args.Object);

			// Refresh the display.

			try
			{
				InvokeRefreshNode();
				InvokeRefreshResults();
			}
			catch ( System.Exception )
			{
			}
		}

		#endregion

		#region Verbs

		private void SetVerbState(MmcConsoleVerb cmd, MmcButtonState state, bool show)
		{
			GetIConsoleVerb().SetVerbState(cmd, state, Convert.ToInt32(show));
		}
		
		private MmcConsoleVerb GetDefaultVerb()
		{
			MmcConsoleVerb verb = MmcConsoleVerb.None;
			GetIConsoleVerb().GetDefaultVerb(ref verb);
			return verb;
		}

		private void SetDefaultVerb(MmcConsoleVerb verb)
		{
			GetIConsoleVerb().SetDefaultVerb(verb);
		}

		private IConsoleVerb GetIConsoleVerb()
		{
			IConsoleVerb consoleVerb;
			Snapin.ResultViewConsole.QueryConsoleVerb(out consoleVerb);
			return consoleVerb;
		}

		#endregion

		#region Events

		protected internal virtual void OnRefresh()
		{
			if ( IsSelected )
				RefreshNode();
		}

		internal void OnSelectScopeItem()
		{
			m_nodeSelected = true;

			// Give the node a chance to enable verbs and to set the status text.

			EnableVerbs();

			// Let the derived class have a chance.

			SelectNode();
		}

		internal void OnDeselectScopeItem()
		{
			m_nodeSelected = false;

			// Let the derived class have a chance.

			DeselectNode();
		}

		internal void OnDelete()
		{
			// Determine whether it is this node that is being deleted.

			if ( IsSelected )
				DeleteNode();
			else if ( IsResultSelected )
				DeleteResults();
		}

		internal void OnActivate(bool activate)
		{
		}

		internal void OnAddScopePaneImages(IImageList imageList)
		{
			foreach ( SnapinNode node in m_childNodes )
				node.OnAddScopePaneImages(imageList);
		}

		internal virtual void OnAddResultPaneImages(IImageList il)
		{
			Snapin.EnsureImages();
			Snapin.Images.LoadImageList(il, 0);
		}

		internal void OnBtnClick(int commandId)
		{
		}

		internal void OnClick()
		{
		}

		internal void OnColumnClick(int column, bool ascending)
		{
		}

		internal bool OnCutOrMove(IDataObject dataObject)
		{
			return false;
		}

		internal void OnDeselectAll()
		{
		}

		internal void OnExpand(bool duringStartup)
		{
			// Let the derived class know first that the node is being expanded. If this method fails
			// still go on to add child nodes.

			System.Exception expandEx = null;
			try
			{
				Expand(duringStartup);
			}
			catch (Exception ex)
			{
				expandEx = ex;
			}

			ImageChanged();

			// Reset all child nodes.

			if ( m_childNodes.Count == 0 )
			{
				ResetChildNodes();
			}

			if (expandEx != null)
			{
				throw expandEx;
			}
		}
        
		internal void OnCollapse()
		{
		}

		internal virtual bool OnInitOcx(object objOCX)
		{
			return false;
		}

		internal void OnUser(string key, object val)
		{
		}

		internal void OnQueryProperties()
		{
		}
		
		internal void OnMenuBtnClick(int CmdId, int x, int y)
		{
		}

		internal void OnMaximized()
		{
		}

		internal void OnMinimized()
		{
		}

		internal bool OnQueryPaste(IDataObject dataObject)
		{
			return false;
		}

		internal void OnRemoveChildren()
		{
			// Notify each node that it's about to be removed, so it can release resources, if needed.

			foreach (SnapinNode node in m_childNodes)
			{
				node.OnRemoveChildren();
				node.OnRemovingNode();
			}
		}

		internal bool OnTryRename()
		{
			return false;
		}

		internal bool OnRename(string newName)
		{
			if ( IsSelected )
			{
				// Renaming the node itself.

				if ( Rename(newName) )
				{
					DisplayName = newName;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if ( IsResultSelected )
			{
				// Renaming the result.

				return RenameResult(newName);
			}
			else
			{
				return false;
			}
		}

		internal void OnSelectResult()
		{
			SelectResult();
		}
        
		internal void OnDeselectResult()
		{
			DeselectResult();
		}

		internal void OnShow()
		{
			Show();
		}

		internal void OnHide()
		{
			Hide();
		}

		internal void OnControlbarCommand(IControlbar Controlbar, IToolbar Toolbar, MmcConsoleVerb verb)
		{
		}

		internal void OnControlbarNotify(IControlbar Controlbar, IToolbar Toolbar, bool bScope, bool bSelect)
		{
		}

		internal void OnViewChangeScope(SnapinNode node)
		{
		}
        
		internal void OnViewChangeResult(SnapinNode node)
		{
		}
        
		internal void OnContextHelp()
		{
		}

		internal void OnPrint()							
		{														
		}														

		internal bool OnPaste(IDataObject ido)
		{
			return false;
		}

		internal void OnFilterChange(MmcFilterChangeCode code, int col) 
		{
		}

		#endregion

		#region Images

		protected void AddImage(string image)
		{
			m_snapin.AddImage(image);
		}

		protected virtual string GetOpenImage()
		{
			return GetImage(); // By default use the same image when open as when closed.
		}

		/// <summary>
		/// This method must be overridden in the derived class in order to display an icon other than the
		/// default icon.
		/// </summary>
		protected virtual string GetImage()
		{
			return null;
		}

		public void ImageChanged()
		{
			// Only try to update if the item has already been inserted and the images have changed.

			if (GetImageIndices() && HScopeItem != IntPtr.Zero)
			{
				ScopeDataItem item = new ScopeDataItem();
				item.Id = HScopeItem;
				item.Mask = (uint) ScopeDataItemFlags.Image | (uint) ScopeDataItemFlags.OpenImage;
				item.Image = m_closedImageIndex;
				item.OpenImage = m_openImageIndex;

				IConsoleNameSpace2 ns = Snapin.ConsoleNamespace;
				ns.SetItem(ref item);

				// If this node is currently shown in the result view also update all the result images.
				// There is no correlation between a scope node and a result node, so all result nodes must
				// be checked.

				if (m_snapin.CurrentScopeNode == m_parentNode)
				{
					ResultNode parentResult = m_parentNode as ResultNode;
					if (parentResult != null)
					{
						parentResult.UpdateResultImages();
					}
				}
			}
		}

		private bool GetImageIndices()
		{
			bool changed = false;

			string newOpenImage = GetOpenImage();
			if (newOpenImage != null)
			{
				int newIndex = m_snapin.GetImageIndex(newOpenImage);
				if (newIndex != m_openImageIndex)
				{
					m_openImageIndex = newIndex;
					changed = true;
				}
			}

			string newClosedImage = GetImage();
			if (newClosedImage != null)
			{
				int newIndex = m_snapin.GetImageIndex(newClosedImage);
				if (newIndex != m_closedImageIndex)
				{
					m_closedImageIndex = newIndex;
					changed = true;
				}
			}

			return changed;
		}

		#endregion

		private Snapin m_snapin;
		private SnapinNode m_parentNode;
		private Guid m_guid;
		private IntPtr m_cookie;
        private IntPtr m_hScopeItem;
        private IntPtr m_parentHScopeItem;
		private ArrayList m_childNodes;
		private string m_displayName;
		private int m_commandId = 0;
		private ArrayList m_menuItems = null;	   
		private ArrayList m_topMenuItems = null;
		private ArrayList m_newMenuItems = null;
		private ArrayList m_taskMenuItems = null;
		private ArrayList m_viewMenuItems = null;
		private ArrayList m_subMenuItems;
		private ObjectPropertyForm m_propertyForm;
		private bool m_nodeSelected;
        private int m_closedImageIndex = 0;
        private int m_openImageIndex = 1;
        private int m_smallImageIndex = 0;
        private int m_largeImageIndex = 1;
		private bool m_areMultipleResultsSelected;
		private NodeControl m_control;
		private bool m_isRefresh;
	}
}

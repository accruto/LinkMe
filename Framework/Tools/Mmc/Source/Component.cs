using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	///  Component implements the IComponent, IExtendContextMenu and IExtendControlbar
	///  interfaces that control the result view side of the MMC control.  
	///  Many event notifications pass through this class on the way to the node
	/// </summary>
	internal class Component
		:	IComponent,
		IExtendContextMenu,
		IExtendControlbar,
		IExtendPropertySheet
	{
		#region Constructors

		public Component(Snapin snapin)
		{
			m_snapin = snapin;
		}

		#endregion

		public IConsole2 Console 
		{
			get { return m_console; }
		}

		#region IComponent Implementation

		public void Initialize(Object pConsole)
		{ 
			m_console = pConsole as IConsole2;
		}
        
		/// <summary>
		/// Delegate to the relevent node or handle it directly.
		/// </summary>
		public int Notify(IntPtr lpDataObject, uint notifyEvent, IntPtr arg, IntPtr param)
		{
			int hr = Constants.Win32.HResults.S_FALSE;

			try
			{
				// There are some sneaky values for the IDataObject parameters that make 
				// interop with this interface a little trick.  This is why lpDataObject is an IntPtr
				// not an actual IDataObject interface.

				if ( lpDataObject == (IntPtr) DataObject.CUSTOMOCX || lpDataObject == (IntPtr) DataObject.CUSTOMWEB )
					return hr;

				hr = Constants.Win32.HResults.S_OK;

				IDataObject dataObject = null;
				if ( lpDataObject != (IntPtr) 0 )
					dataObject = (IDataObject) Marshal.GetObjectForIUnknown(lpDataObject);

				// Get the node.

				SnapinNode node;

				if ( dataObject != null )
				{
					node = ((DataObject) dataObject).Node;
				}
				else
				{
					// Try to get the node of the selected item.

					ResultDataItem item = m_snapin.GetSelectedItem((IResultData) m_console);
					if ( item.Index != -1 )
					{
						node = m_snapin.FindNode(item.Param);
					}
					else 
					{
						node = m_snapin.CurrentScopeNode;
						if ( node == null )
							node = m_snapin.RootNode;
					}
				}

				Debug.WriteLine("IComponent.Notify: " + ((MmcNotify) notifyEvent) + " " + node.DisplayName);

				// Dispatch the event to the node.

				switch ( (MmcNotify) notifyEvent )
				{
					case MmcNotify.Activate:

						node.OnActivate(arg.ToInt32() != 0);
						break;

					case MmcNotify.AddImages:

						// arg actually contains the IImageList interface. We need to 
						// marshall that manually as well.

						IImageList imageList = (IImageList) Marshal.GetObjectForIUnknown(arg);

						// Param contains the HScopeItem so use it to get the node it represents.

						node = m_snapin.FindNodeByHScopeItem(param);
                        if ( node != null )
                            node.OnAddResultPaneImages(imageList);
						break;

					case MmcNotify.BtnClick:

						node.OnBtnClick(param.ToInt32());
						break;

					case MmcNotify.Click:

						node.OnClick();
						break;

					case MmcNotify.ColumnClick:

						node.OnColumnClick((int) arg, (int) param == 0);
						break;

					case MmcNotify.CutOrMove:

						dataObject = (IDataObject) Marshal.GetObjectForIUnknown(arg);
						if ( !node.OnCutOrMove(dataObject) )
							hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.DblClick:

						// Check if the default verb is enabled. If it is let MMC invoke it, otherwise
						// say that the action was handled. This prevents it from asking for a property page
						// when the Properties verb is disabled.

						IConsoleVerb consoleVerb;
						Console.QueryConsoleVerb(out consoleVerb);

						MmcConsoleVerb defaultVerb = MmcConsoleVerb.None;
						consoleVerb.GetDefaultVerb(ref defaultVerb);

						int state = 0;
						consoleVerb.GetVerbState(defaultVerb, MmcButtonState.Enabled, ref state);

						hr = (state == 0 ? Constants.Win32.HResults.S_OK : Constants.Win32.HResults.S_FALSE);
						break;

					case MmcNotify.Delete:

						node.OnDelete();
						break;

					case MmcNotify.DeselectAll:

						node.OnDeselectAll();
						break;

					case MmcNotify.Expand:

						if ( arg == IntPtr.Zero )
							node.OnExpand(false);
						else
							node.OnCollapse();
						break;

					case MmcNotify.MenuBtnClick:

						MenuButtonData data = (MenuButtonData)(Marshal.PtrToStructure(param, typeof(MenuButtonData)));
						node.OnMenuBtnClick(data.CommandId, data.X, data.Y);
						break;

					case MmcNotify.Minimized:

						if ( arg.ToInt32() == 0 )
							node.OnMaximized();
						else
							node.OnMinimized();
						break;

					case MmcNotify.Paste:

						dataObject = (IDataObject) Marshal.GetObjectForIUnknown(arg);
						if ( node.OnPaste(dataObject) )
							param = Marshal.GetIUnknownForObject(dataObject);
						else
							hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.PropertyChange:

						break;

					case MmcNotify.QueryPaste:

						dataObject = (IDataObject) Marshal.GetObjectForIUnknown(arg);
						if ( !node.OnQueryPaste(dataObject) )
							hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.Refresh:

						node.OnRefresh();
						break;

					case MmcNotify.Print:

						node.OnPrint();					
						break;							

					case MmcNotify.Rename:

                        if (arg == IntPtr.Zero)
						{
							// Check.

							if ( !node.OnTryRename() )
								hr = Constants.Win32.HResults.S_FALSE;
						}
						else
						{
							if ( node.OnRename(Marshal.PtrToStringAuto(param)) )
								SetStatusText(node.GetStatusText());
							else
								hr = Constants.Win32.HResults.S_FALSE;
						}

						break;

					case MmcNotify.Select:

						bool scope = ((short) arg) != 0;
						bool select = ((((int) arg) >> 16) & 0xffff) != 0;

						if ( scope )
						{
							if ( select )
							{
								if ( m_scopeItemChanging )
								{
									m_scopeItemChanging = false;
									m_snapin.CurrentScopeNode = node;
								}

								// Defect 54593: for some reason when multiple result items are selected and the
								// user clicks on the scope node MMC does NOT send a "deselect result" notification
								// (if a single result item is selected then it does). Work around this.

								if ( node.IsResultSelected )
									node.OnDeselectResult();

								node.OnSelectScopeItem();
								SetStatusText(node.GetStatusText());
							}
							else
							{
								node.OnDeselectScopeItem();
							}
						}
						else
						{
							if ( select )
								node.OnSelectResult();
							else
								node.OnDeselectResult();
						}

						break;

					case MmcNotify.Show:

                        bool selecting = (arg != IntPtr.Zero);
						if ( selecting )
						{
							m_scopeItemChanging = true;
							m_snapin.FindNodeByHScopeItem(param).OnShow();
						}
						else
						{
							m_snapin.FindNodeByHScopeItem(param).OnHide();
						}

						break;

					case MmcNotify.ViewChange:

						dataObject = null;
						if ( param != (IntPtr) 0 )
							dataObject = (IDataObject) Marshal.GetObjectForIUnknown(param);
						SnapinNode nodeChanged = dataObject == null ? m_snapin.RootNode : ((DataObject) dataObject).Node;

                        if (arg != IntPtr.Zero)
							node.OnViewChangeScope(nodeChanged);
						else
							node.OnViewChangeResult(nodeChanged);
						break;

					case MmcNotify.SnapinHelp:

						hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.ContextHelp:

						node.OnContextHelp();
						break;

					case MmcNotify.InitOcx:

						// param is the IUnknown of the OCX.

						object ocx = Marshal.GetObjectForIUnknown(param);
						if ( node.OnInitOcx(ocx) )
							hr = Constants.Win32.HResults.S_OK;
						else
							hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.FilterChange:

						node.OnFilterChange((MmcFilterChangeCode) arg.ToInt32(), param.ToInt32());                        
						hr = Constants.Win32.HResults.S_OK;
						break;

					case MmcNotify.GetFilterMenu:

						hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.FilterOperator:

						hr = Constants.Win32.HResults.S_FALSE;
						break;

					case MmcNotify.ColumnsChanged:        

						hr = Constants.Win32.HResults.S_OK;
						break;

					default:

						// The notification is not supported.

						hr = Constants.Win32.HResults.S_FALSE;
						break;
				}
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(m_snapin);
				hr = Constants.Win32.HResults.S_FALSE;
			}
               
			return hr;
		}

		/// <summary>
		/// Called at shutdown
		/// </summary>
		public void Destroy(IntPtr cookie)
		{
			try
			{
				if ( m_console != null )
				{
					Marshal.ReleaseComObject(m_console);
					m_console = null;
				}

				if ( m_controlbar != null )
				{
					Marshal.ReleaseComObject(m_controlbar);
					m_controlbar = null;
				}

				if ( m_toolbar != null )
				{
					Marshal.ReleaseComObject(m_toolbar);
					m_toolbar = null;
				}
			}
			catch ( Exception )
			{
			}
		}
        
		/// <summary>
		/// Called when MMC wants an IDataObject for a node by cookie 
		/// </summary>
		/// <param name="cookie"></param>
		/// <param name="type"></param>
		/// <param name="ppDataObject"></param>
		public void QueryDataObject(IntPtr cookie, uint type, out IDataObject ppDataObject)
		{
			ppDataObject = null;

			try
			{
				// Check if this a MULTI_SELECT_COOKIE then just find
				// first selected item and return the node it depends to.
				if ( cookie == (IntPtr) CookieType.MultiSelectCookie )
				{


					ResultDataItem rdi = m_snapin.GetSelectedItem((IResultData)m_console);
					SnapinNode node = m_snapin.FindNode(rdi.Param);
					ppDataObject = new DataObject(node);
					node.AreMultipleResultsSelected = true;
					m_areMultipleResultsSelected = true;
				}
				else
				{
					SnapinNode node = m_snapin.FindNode(cookie);
					ppDataObject = new DataObject(node);
					node.AreMultipleResultsSelected = false;
					m_areMultipleResultsSelected = false;
				}
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(m_snapin);
			}
		}
        
		/// <summary>
		/// Called to get Result view type information.
		/// </summary>
		/// <param name="cookie"></param>
		/// <param name="ppViewType"></param>
		/// <param name="pViewOptions"></param>
		/// <returns></returns>
		public int GetResultViewType(IntPtr cookie, out IntPtr ppViewType, out int pViewOptions)
		{
			pViewOptions = 0;

			SnapinNode node = m_snapin.FindNode(cookie);
			if ( node != null )
			{
				ppViewType = Marshal.StringToCoTaskMemUni(node.GetResultViewType(ref pViewOptions));
				return Constants.Win32.HResults.S_OK;
			}
			else
			{
				ppViewType = IntPtr.Zero;
				return Constants.Win32.HResults.S_FALSE;
			}
		}

		/// <summary>
		/// Called to get result view information.
		/// </summary>
		/// <param name="resultDataItem"></param>
		public void GetDisplayInfo(ref ResultDataItem resultDataItem)
		{
			m_snapin.FindNode(resultDataItem.Param).GetDisplayInfo(ref resultDataItem);
		}

		/// <summary>
		/// Called to compare 2 nodes - we use cookie values to test equality
		/// </summary>
		public int CompareObjects(IDataObject lpDataObject1, IDataObject lpDataObject2)
		{
			DataObject object1 = (DataObject) lpDataObject1;
			DataObject object2 = (DataObject) lpDataObject2;
			return object1.Node.Cookie != object2.Node.Cookie ? Constants.Win32.HResults.S_FALSE : Constants.Win32.HResults.S_OK;
		}

		#endregion

		#region IExtendContextMenu Implementation

		/// <summary>
		/// This function allows us to add items to the context menus 
		/// </summary>
		public void AddMenuItems(IntPtr piDataObject, IContextMenuCallback piCallback, ref int pInsertionAllowed)
		{
			try
			{
				if ( piDataObject == (IntPtr)DataObject.CUSTOMOCX || piDataObject == (IntPtr)DataObject.CUSTOMWEB)
					return;

				DataObject dataObject;
				if ( m_areMultipleResultsSelected )
				{
					ResultDataItem item = m_snapin.GetSelectedItem((IResultData) m_console);
					dataObject = new DataObject(m_snapin.FindNode(item.Param));
				}
				else
				{
					dataObject = (DataObject)(IDataObject) Marshal.GetObjectForIUnknown(piDataObject);
				}

				if ( dataObject != null && dataObject.Node != null )
					dataObject.Node.AddMenuItems(piCallback, ref pInsertionAllowed);
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(m_snapin);
			}
		}

		/// <summary>
		/// Called menu item is invoked.  We delegate to the node 
		/// to let it handle it.
		/// </summary>
		/// <param name="lCommandID"></param>
		/// <param name="piDataObject"></param>
		public void Command(int lCommandID, IntPtr piDataObject)
		{
			try
			{
				if ( piDataObject == (IntPtr)DataObject.CUSTOMOCX || piDataObject == (IntPtr)DataObject.CUSTOMWEB )
					return;

				DataObject dataObject;
				if ( m_areMultipleResultsSelected )
				{
					ResultDataItem item = m_snapin.GetSelectedItem((IResultData) m_console);
					dataObject = new DataObject(m_snapin.FindNode(item.Param));
				}
				else
				{
					dataObject = (DataObject)(IDataObject) Marshal.GetObjectForIUnknown(piDataObject);
				}

				if ( dataObject != null && dataObject.Node != null )
					dataObject.Node.OnMenuCommand(lCommandID);
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(m_snapin);
			}
		}

		#endregion

		#region IExtendControlbar Implementation

		/// <summary>
		/// Called to set and reset the control bar interface.
		/// Implemented by Alexander Kachalkov
		/// </summary>
		public void SetControlbar(IControlbar pControlbar)
		{
			if ( m_toolbar != null )
				m_toolbar = null;
			if ( m_controlbar != null )
				m_controlbar = null;
			if ( pControlbar == null )
				return;

			m_controlbar = pControlbar;
			object pToolBar;
			m_controlbar.Create(MmcControlType.Toolbar, this, out pToolBar);
			m_toolbar = (IToolbar) pToolBar;

			// Get an Hicon for the bitmap.
			
			IntPtr hBitmap = m_snapin.Images.GetBitmapHandle(m_toolbarImageIndex);
			if ( hBitmap == IntPtr.Zero || m_toolbarButtons == null )
				return;

			m_toolbar.AddBitmap(m_toolbarButtons.Length, hBitmap, 16, 16, Color.Black.ToArgb());
			for ( int index = 0; index <= m_toolbarButtons.Length - 1; ++index )
				m_toolbar.InsertButton(0, ref m_toolbarButtons[index]);
		}

		/// <summary>
		/// Called when nodes are selected and controlbar commands fire.
		/// </summary>
		public int ControlbarNotify(uint notifyEvent, IntPtr arg, IntPtr param)
		{
			int hr = Constants.Win32.HResults.S_OK;

			switch ( (MmcNotify) notifyEvent )
			{
				case MmcNotify.Select:

					bool scope = ((short)arg) != 0;
					bool select = ((((int)arg) >> 16) & 0xffff) != 0;

					if ( (int) param > 0 )
					{
						DataObject dataObject = (IDataObject) Marshal.GetObjectForIUnknown(param) as DataObject;
						SnapinNode node = dataObject == null ? m_snapin.RootNode : dataObject.Node;
						node.OnControlbarNotify(m_controlbar, m_toolbar, scope, select);
					}
					break;

				case MmcNotify.BtnClick:

					if ( (int) arg > 0 )
					{
						IDataObject dataObject = (IDataObject) Marshal.GetObjectForIUnknown(arg);
						SnapinNode node = dataObject == null ? m_snapin.RootNode : ((DataObject) dataObject).Node;
						node.OnControlbarCommand(m_controlbar, m_toolbar, (MmcConsoleVerb)param.ToInt32());
					}
					break;

				default:

					hr = Constants.Win32.HResults.S_FALSE;
					break;
			}

			return hr;
		}

		#endregion

		#region IExtendPropertySheet implementation

		/// <summary>
		/// MMC wants a property sheet created of the given node 
		/// </summary>
		public int CreatePropertyPages(IPropertySheetCallback lpProvider, IntPtr handle, IDataObject lpDataObject)
		{
			DataObject dataObject = (DataObject) lpDataObject;
	    
			// Let's see if this node has property pages.

			dataObject.Node.CreatePropertyForm(lpProvider, handle);

			// Don't use the MMC property page facilities.

			return Constants.Win32.HResults.S_FALSE;
		}

		/// <summary>
		/// MMC calls this at initialization time to find out if the node  
		/// supports property pages - this is the one and only time to respond.
		/// </summary>
		public int QueryPagesFor(IDataObject lpDataObject)
		{
			// This snapin does have property pages, so we should return S_OK
			// (which will happen automatically). If the snapin didn't have
			// any property pages, then we should return S_FALSE

			DataObject dataObject = (DataObject)lpDataObject;
			dataObject.Node.OnQueryProperties();
			return Constants.Win32.HResults.S_OK;
		}

		#endregion

		internal void SetStatusText(string text)
		{
			m_console.SetStatusText(text);
		}

		private Snapin m_snapin;
		private IConsole2 m_console;
		private IControlbar m_controlbar;
		private IToolbar m_toolbar;
		private MmcButton[] m_toolbarButtons = null;
		private int m_toolbarImageIndex = 0;
		private bool m_areMultipleResultsSelected = false;

		// When the user left-clicks on a scope node first a MMCN_SHOW notification is sent and then a MMNC_SELECT.
		// When they right-click on a scope node only an MMNC_SELECT notification is sent. We need to distinguish
		// between the two so that the Snapin.CurrentScopeItem property is only changed when left-clicking.
		// Do this by settings this flag in MMNC_SHOW is sent and checking it in MMNC_SELECT.
		private bool m_scopeItemChanging = false;
	}
}

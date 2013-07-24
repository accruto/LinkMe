using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Windows.Forms;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Mmc
{
	public class Snapin
		:	Control,
			IComponentData,
			IExtendPropertySheet,
			IExtendContextMenu,
			IPersistStream		//Something bad is happening, need to fix at some stage
	{
		#region Constructors

		public Snapin()
		{
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		#endregion

		#region SnapinState

		/// <summary>
		/// Permanent snap-in state, persisted to the MSC file.
		/// </summary>
		public SnapinState PersistentState
		{
			get { return m_persistentState; }
		}

		/// <summary>
		/// Temporary snap-in state, lost when the Snapin object is destroyed.
		/// </summary>
		public SnapinState TemporaryState
		{
			get { return m_temporaryState; }
		}

		#endregion

		#region MainWindow

		public IWin32Window GetMainWindow()
		{
			IntPtr hwnd = IntPtr.Zero;
			m_console.GetMainWindow(ref hwnd);
			return new Win32Window(hwnd);
		}

		#endregion

		#region MessageBox

		public DialogResult MessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			uint style = (uint)buttons | (uint)icon | (uint)defaultButton | (uint)options;
			int retval = (int)DialogResult.None;
			m_console.MessageBox(text, caption, style, ref retval);
			return (DialogResult)retval;
		}

		public DialogResult MessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return MessageBox(text, caption, buttons, icon, defaultButton, 0);
		}

		public DialogResult MessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);
		}

		public DialogResult MessageBox(string text, string caption, MessageBoxButtons buttons)
		{
			return MessageBox(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
		}

		public DialogResult MessageBox(string text, string caption)
		{
			return MessageBox(text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
		}

		public DialogResult MessageBox(string text)
		{
			return MessageBox(text, null, MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
		}

		#endregion

		#region Selection

		public string SaveSelection()
		{
			return CurrentScopeNode == null ? null : CurrentScopeNode.GetPathRelativeTo(RootNode);
		}

		public void RestoreSelection(string path)
		{
			SnapinNode node = RootNode.FindNodeInTree(path);
			if ( node != null )
				node.Select();
		}

		#endregion

		internal protected void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			ExceptionDialog dialog = new ExceptionDialog(e.Exception, "The following exception has occurred in the snap-in:");
			dialog.ShowDialog(this);
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			// .NET bug: if an AppDomain is unloaded after it has accessed WMI using System.Management.Scope then
			// a ThreadAbortException occurs. This is fixed in .NET 2.0 - just ignore it for now.

			if (e.ExceptionObject is Exception)
			{
				if (!(e.ExceptionObject is ThreadAbortException))
				{
					new ExceptionDialog((Exception)e.ExceptionObject,
						"The following unhandled exception has occurred in the snap-in:").ShowDialog(this);
				}
			}
			else
			{
				MessageBox("An unhandled exception of type '" + e.ExceptionObject.GetType().FullName
					+ "' (which is not derived from System.Exception) has occurred in the default domain.",
					"Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		#region MMC Properties

		internal IConsole2 ResultViewConsole
		{
			get { return m_component == null ? null : m_component.Console; }
		}

		internal IConsoleNameSpace2 ConsoleNamespace
		{
			get { return m_consoleNamespace; }
		}

		#endregion

		#region SelectedItem

		internal ResultDataItem GetSelectedItem(IResultData resultData)
		{
			// Start at the first item.

			return GetSelectedItem(resultData, -1);
		}

		internal ResultDataItem GetSelectedItem(IResultData resultData, int index)
		{
			ResultDataItem item = new ResultDataItem();
			item.Mask = (uint) ResultDataItemFlags.State;
			item.Col = 0;
			item.Index = index;
			item.State = (uint) ListViewItemState.Selected;

			try
			{
				resultData.GetNextItem(ref item);
			}
			catch ( COMException )
			{
			}

			return item;
		}

		#endregion

		#region Node

		/// <summary>
		/// Get/set the root node.
		/// </summary>
		public SnapinNode RootNode
		{
			get { return (SnapinNode) m_nodes[0]; }
			set
			{
				m_nodes[0] = value;
				CurrentScopeNode = value;
			}
		}

		/// <summary>
		/// The scope node that is currently selected for this snapin.
		/// </summary>
		public SnapinNode CurrentScopeNode 
		{
			get { return m_currentScopeNode; }
			set { m_currentScopeNode = value; }
		}

		/// <summary>
		/// Find a node with the given cookie in list.
		/// </summary>
		internal SnapinNode FindNode(IntPtr cookie)
		{
			// Some nodes use the high order word for item id
			// so mask off this word to get the "real" cookie.

			int realCookie = (int)cookie & 0xffff;
			SnapinNode node = (SnapinNode) m_nodes[realCookie];
			if ( node == null )
				throw new ApplicationException("Failed to find Node with cookie " + realCookie);
			return node;
		}

		/// <summary>
		/// Find a node by the MMC defined HScope
		/// </summary>
		internal SnapinNode FindNodeByHScopeItem(IntPtr hScopeItem)
		{
			foreach ( SnapinNode node in m_nodes.Values )
			{
				if ( node.HScopeItem == hScopeItem )
					return node;
			}
			
			return null;
		}

		/// <summary>
		/// Called be SnapinNode constructor to register itself back here for  
		/// centralized lookup when MMC want to talk to a Node
		/// </summary>
		internal IntPtr Register(SnapinNode newNode)
		{
			// Return the index in the array.
			int id = m_nodeId++;
			m_nodes.Add(id, newNode);
			return (IntPtr)id;
		}
		
		/// <summary>
		/// Unregister the node.
		/// </summary>
		/// <param name="node"></param>
		internal void Unregister(SnapinNode node)
		{
			m_nodes.Remove(node.Cookie);
		}

		#endregion

		#region Images

		internal ImageList Images
		{
			get { return m_images; }
		}

		/// <summary>
		/// Add an image based on the embedded resource name.
		/// </summary>
		internal short AddImage(string iconResourceName)
		{
			int index = m_images.IndexOf(iconResourceName);
            Debug.Assert(index < short.MaxValue, "index < short.MaxValue");

            if (index == -1)
			{
				index = m_images.AddResource(iconResourceName);
				m_imagesDirty = true;
			}

			return (short)index;
		}

		internal int GetImageIndex(string iconResourceName)
		{
			return m_images.IndexOf(iconResourceName);
		}

		internal void EnsureImages()
		{
			if ( m_imagesDirty && m_console != null )
			{
				IImageList imageList;
				m_console.QueryScopeImageList(out imageList);
				m_images.LoadImageList(imageList);
				m_imagesDirty = false;
			}
		}

		#endregion

		#region Guid

		/// <summary>
		/// The guid for the snapin.
		/// </summary>
		public Guid Guid
		{
			get 
			{
				GuidAttribute[] attributes = (GuidAttribute[]) GetType().GetCustomAttributes(typeof(GuidAttribute), true);
				if ( attributes.Length == 0 )
					throw new ApplicationException("Failed to find GuidAttribute on SnapinBase class");
				return new Guid(attributes[0].Value);
			}
		}

		#endregion

		#region IComponentData Implementation

		/// <summary>
		/// Called by MMC with the Console interface on startup we do snapin one time init stuff here 
		/// </summary>
		/// <param name="pUnknown">Implements IConsole()2 and IConsoleNameSpace2 interfaces</param>
		void IComponentData.Initialize(object pUnknown)
		{
			// Cache references to MMC interfaces.

			m_console = (IConsole2)pUnknown;
			m_consoleNamespace = (IConsoleNameSpace2)pUnknown;

			// Allow initialisation for each node.

			foreach ( SnapinNode node in m_nodes.Values )
				node.Initialize();

			// Add images needed for the snapin.

			IImageList imageList;
			m_console.QueryScopeImageList(out imageList);
			m_images.LoadImageList(imageList);
			RootNode.OnAddScopePaneImages(imageList);
		}

		/// <summary>
		/// Give MMC the IComponent implementation when asked.
		/// </summary>
		void IComponentData.CreateComponent(out IComponent ppComponent)
		{
			// Make sure we don't already have a component created.

			if ( m_component == null )
				m_component = new Component(this);
			ppComponent = m_component;

			// Going to use this call to indicate that things have started.
			// It is soon after this call that things are shown on the screen
			// and it serves the purpose of indicating a change for the nodes
			// expanding.

			m_started = true;
			m_oneMoreNodeDuringStartup = true;
		}

		/// <summary>
		/// This notify is primarily responsible for inserting data items into 
		/// the console namespace.
		/// </summary>
		int IComponentData.Notify(IDataObject lpDataObject, uint notifyEvent, IntPtr arg, IntPtr param)
		{
			int hr = Constants.Win32.HResults.S_OK;

			try
			{
				// Use the root node if none is supplied.

                DataObject dataObject = (DataObject) lpDataObject;
				SnapinNode node = dataObject == null ? RootNode : dataObject.Node;

				Debug.WriteLine("IComponentData.Notify: " + ((MmcNotify) notifyEvent) + " " + node.DisplayName);

				switch ( (MmcNotify) notifyEvent )
				{
					case MmcNotify.Expand:

						// Pass onto the node.

						if ( (int) arg != 0 )
						{
							if ( node.HScopeItem == IntPtr.Zero )
								node.HScopeItem = param;
							CurrentScopeNode = node;

							bool duringStartup = !m_started || m_oneMoreNodeDuringStartup;
							m_oneMoreNodeDuringStartup = false;

							node.OnExpand(duringStartup);
						}
						else
						{
							node.OnCollapse();
						}

						break;

					case MmcNotify.Delete:

						// Pass onto the node.

						node.OnDelete();
						break;

					case MmcNotify.BtnClick:

						// Pass onto the node.

						node.OnBtnClick(param.ToInt32());
						break;

					case MmcNotify.Rename:

						// Pass onto the node.

						if ( (int) arg == 0 )
						{
							// Check first.

							if ( !node.OnTryRename() )
								hr = Constants.Win32.HResults.S_FALSE;
						}
						else
						{
							// Pass the name.

							if ( node.OnRename(Marshal.PtrToStringAuto(param)) )
								m_component.SetStatusText(node.GetStatusText());
							else
								hr = Constants.Win32.HResults.S_FALSE;
						}

						break;

					case MmcNotify.RemoveChildren:

						node = FindNodeByHScopeItem(arg);
						node.OnRemoveChildren();
						break;

					default:

						// Didn't handle the message.

						hr = Constants.Win32.HResults.S_FALSE;
						break;
				}
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(this);
				hr = Constants.Win32.HResults.S_FALSE;
			}

			return hr;
		}

		/// <summary>
		///  Called by MMC to cleanup.
		/// </summary>
		void IComponentData.Destroy()
		{
			try
			{
				if ( m_console != null )
				{
					Marshal.ReleaseComObject(m_console);
					m_console = null;
				}

				if ( m_consoleNamespace != null )
				{
					Marshal.ReleaseComObject(m_consoleNamespace);
					m_consoleNamespace = null;
				}

				m_images.Dispose();
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// MMC wants a data object for a specific node.
		/// </summary>
		void IComponentData.QueryDataObject(IntPtr cookie, uint type, out IDataObject ppDataObject)
		{
			ppDataObject = new DataObject(FindNode(cookie)); 
		}

		/// <summary>
		/// Provides scope pane info to MMC.
		/// </summary>
		void IComponentData.GetDisplayInfo(ref ScopeDataItem item)
		{
			FindNode(item.Param).GetDisplayInfo(ref item);
		}
   		
		/// <summary>
		/// This method will compare two data objects based on underlying cookie value 
		/// that the nodes contain
		/// </summary>
		int IComponentData.CompareObjects(IDataObject lpDataObject1, IDataObject lpDataObject2)
		{
			DataObject object1 = (DataObject) lpDataObject1;
			DataObject object2 = (DataObject) lpDataObject2;
			return object1.Node.Cookie != object2.Node.Cookie ? Constants.Win32.HResults.S_FALSE : Constants.Win32.HResults.S_OK;
		}

		#endregion

		#region IExtendPropertySheet Implementation

		/// <summary>
		/// MMC wants a property sheet created of the given node.
		/// </summary>
		int IExtendPropertySheet.CreatePropertyPages(IPropertySheetCallback lpProvider, IntPtr handle, IDataObject lpDataObject)
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
		int IExtendPropertySheet.QueryPagesFor(IDataObject lpDataObject)
		{
			// This snapin does have property pages, so we should return S_OK
			// (which will happen automatically). If the snapin didn't have
			// any property pages, then we should return S_FALSE.

			DataObject dataObject = (DataObject)lpDataObject;
			dataObject.Node.OnQueryProperties();

			// Always return OK.

			return Constants.Win32.HResults.S_OK;
		}

		#endregion

		#region IExtendContextMenu Implementation

		/// <summary>
		/// Add menu items based on MMC's insertion rules.
		/// </summary>
		void IExtendContextMenu.AddMenuItems(IntPtr piDataObject, IContextMenuCallback piCallback, ref int pInsertionAllowed)
		{
			try
			{
				DataObject dataObject = (DataObject)(IDataObject) Marshal.GetObjectForIUnknown(piDataObject);
				if ( dataObject.Node != null )
					dataObject.Node.AddMenuItems(piCallback, ref pInsertionAllowed);
			}
			catch( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(this);
			}
		}

		/// <summary>
		/// Called when user invokes a menu command.
		/// </summary>
		void IExtendContextMenu.Command(int lCommandID, IntPtr piDataObject)
		{
			try
			{
				DataObject dataObject = (DataObject)(IDataObject)Marshal.GetObjectForIUnknown(piDataObject);
				if ( dataObject.Node != null )
					dataObject.Node.OnMenuCommand(lCommandID);
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(this);
			}
		}

		#endregion

 		/// <summary>
		/// Auto Register the snap-in with MMC 
		/// </summary>
		[ComRegisterFunction]
		public static void RegisterSnapIn(Type type)
		{
			SnapinRegistrar.RegisterAssembly(type.Assembly);
		}

		/// <summary>
		/// Unregister the snap-in with MMC
		/// </summary>
		[ComUnregisterFunction]
		public static void UnregisterSnapIn(Type type)
		{
			SnapinRegistrar.UnRegisterAssembly(type.Assembly);
		}

		#region IPersist Implementation

		void IPersist.GetClassID(out Guid pClassID) 
		{			
			pClassID = Guid;
		}

		#endregion

		#region IPersistStream Implementation

		void IPersistStream.GetClassID(out Guid pClassID) 
		{			
			pClassID = Guid;
		}
		
		int IPersistStream.IsDirty()
		{
			if ( m_persistentState != null )
				return m_persistentState.IsDirty ? Constants.Win32.HResults.S_OK : Constants.Win32.HResults.S_FALSE;
			else 
				return Constants.Win32.HResults.S_FALSE;
		}

		void IPersistStream.Load(IStream pStm) 
		{
			try 
			{
				// Load the state itself.

				ComStream stream = new ComStream(pStm);
				m_persistentState.ReadState(stream);

				// Give the snapin a chance to load now that the state is loaded.

				LoadState();
			}
			catch ( Exception )
			{
			}
		}

		void IPersistStream.Save(IStream pStm, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty) 
		{			
			try 
			{
				// Ask the snapin to save all its state.
				
				SaveState();

				// Save the state itself.

				ComStream stream = new ComStream(pStm);
				m_persistentState.WriteState(stream);
				if ( fClearDirty )
					m_persistentState.IsDirty = false;
			}
			catch ( Exception )
			{
			}
		}
	
		void IPersistStream.GetSizeMax(out long pcbSize) 
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				m_persistentState.WriteState(stream);
				pcbSize = stream.Length;
			}
		}

		protected virtual void LoadState()
		{
		}

		protected virtual void SaveState()
		{
		}

		#endregion

		private SnapinNode m_currentScopeNode;
		private SnapinState m_persistentState = new SnapinState();
		private SnapinState m_temporaryState = new SnapinState();
		private Component m_component = null;
		private IConsole2 m_console = null;
		private IConsoleNameSpace2 m_consoleNamespace = null; 
		private HybridDictionary m_nodes = new HybridDictionary(8);
		private int m_nodeId = 0;
		private ImageList m_images = new ImageList();
		private bool m_started = false;
		private bool m_oneMoreNodeDuringStartup = false;
		private bool m_imagesDirty = false;
	}
}


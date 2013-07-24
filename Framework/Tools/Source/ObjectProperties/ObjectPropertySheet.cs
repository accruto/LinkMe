using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;

using UserControl = LinkMe.Framework.Tools.Controls.UserControl;
using TreeView = LinkMe.Framework.Tools.Controls.TreeView;
using Splitter = LinkMe.Framework.Tools.Controls.Splitter;
using RichTextBox = LinkMe.Framework.Tools.Controls.RichTextBox;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "ObjectProperties.bmp")]
	public class ObjectPropertySheet
		:	UserControl,
			IPropertyPageParent
	{
		/// <summary>
		/// Raised when the object is changed in some way.
		/// </summary>
		public event EventHandler ElementChanged;
		public event EventHandler Enable;
		public event EventHandler Disable;
		/// <summary>
		/// Raised when a new page is added and being displayed for the first time.
		/// </summary>
		public event EventHandler PageAdded;

		// imgToolBarIcons

		private const int ImageIndexShowNonPublic = 3;
		private const int ImageIndexHideNonPublic = 4;

		private IObjectPropertyInfo m_objectInfo = null;
		private object m_originalObject = null;
		private IElementPropertyInfo m_selectedElement = null;
		private ObjectPropertySettings m_settings = null;
		private bool m_isReadOnly = false;
		private object m_initialSelectedElement = null;
		private ArrayList m_propertyPages = new ArrayList();
		private ElementPropertyPage m_currentPage;
		private bool m_allowEdit = true;
		private bool m_validateSelect = true;
		private MenuItem m_deleteMenuItem = null;

		// Controls

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel panContents;
		private Splitter splitVertical;
		private System.Windows.Forms.ImageList imgToolBarIcons;
		private System.Windows.Forms.ToolBar tbToolBar;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.GroupBox grpSeparator;
		private System.Windows.Forms.ContextMenu mnuElements;
		private System.Windows.Forms.Panel panDetails;
		private System.Windows.Forms.ToolBarButton tbbNew;
		private System.Windows.Forms.ToolBarButton tbbDelete;
		private LinkMe.Framework.Tools.Controls.TreeView tvwElements;

		#region Constructors

		public ObjectPropertySheet()
		{
			InitializeComponent();
			splitVertical.Initialise();
		}

		[Browsable(false)]
		public object Object
		{
			get { return m_objectInfo == null ? null : m_objectInfo.Object; }
		}

		#endregion

		#region Component Designer generated code

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

				if (m_settings != null)
				{
					m_settings.Refresh -= new EventHandler(m_settings_Refresh);
					m_settings = null;
				}

				// Dispose of all repositories that support IDisposable (the manager may not have taken
				// care of all of them).

				m_selectedElement = null;

				TreeView.ClearTagsRecursive(tvwElements.Nodes);
			}

			base.Dispose( disposing );
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ObjectPropertySheet));
			this.panContents = new System.Windows.Forms.Panel();
			this.panDetails = new System.Windows.Forms.Panel();
			this.grpSeparator = new System.Windows.Forms.GroupBox();
			this.lblName = new System.Windows.Forms.Label();
			this.splitVertical = new LinkMe.Framework.Tools.Controls.Splitter();
			this.tvwElements = new LinkMe.Framework.Tools.Controls.TreeView();
			this.mnuElements = new System.Windows.Forms.ContextMenu();
			this.tbToolBar = new System.Windows.Forms.ToolBar();
			this.tbbNew = new System.Windows.Forms.ToolBarButton();
			this.tbbDelete = new System.Windows.Forms.ToolBarButton();
			this.imgToolBarIcons = new System.Windows.Forms.ImageList(this.components);
			this.panContents.SuspendLayout();
			this.SuspendLayout();
			// 
			// panContents
			// 
			this.panContents.Controls.Add(this.panDetails);
			this.panContents.Controls.Add(this.grpSeparator);
			this.panContents.Controls.Add(this.lblName);
			this.panContents.Controls.Add(this.splitVertical);
			this.panContents.Controls.Add(this.tvwElements);
			this.panContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panContents.Location = new System.Drawing.Point(0, 26);
			this.panContents.Name = "panContents";
			this.panContents.Size = new System.Drawing.Size(650, 379);
			this.panContents.TabIndex = 0;
			// 
			// panDetails
			// 
			this.panDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.panDetails.Location = new System.Drawing.Point(206, 24);
			this.panDetails.Name = "panDetails";
			this.panDetails.Size = new System.Drawing.Size(444, 352);
			this.panDetails.TabIndex = 9;
			this.panDetails.SizeChanged += new System.EventHandler(this.panDetails_SizeChanged);
			// 
			// grpSeparator
			// 
			this.grpSeparator.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.grpSeparator.Location = new System.Drawing.Point(206, 371);
			this.grpSeparator.Name = "grpSeparator";
			this.grpSeparator.Size = new System.Drawing.Size(444, 8);
			this.grpSeparator.TabIndex = 8;
			this.grpSeparator.TabStop = false;
			// 
			// lblName
			// 
			this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblName.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblName.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.lblName.Location = new System.Drawing.Point(206, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(444, 20);
			this.lblName.TabIndex = 7;
			// 
			// splitVertical
			// 
			this.splitVertical.Location = new System.Drawing.Point(200, 0);
			this.splitVertical.Name = "splitVertical";
			this.splitVertical.Size = new System.Drawing.Size(6, 379);
			this.splitVertical.TabIndex = 1;
			this.splitVertical.TabStop = false;
			this.splitVertical.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitVertical_SplitterMoved);
			// 
			// tvwElements
			// 
			this.tvwElements.ContextMenu = this.mnuElements;
			this.tvwElements.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwElements.HideSelection = false;
			this.tvwElements.ImageIndex = -1;
			this.tvwElements.LabelEdit = true;
			this.tvwElements.Location = new System.Drawing.Point(0, 0);
			this.tvwElements.Name = "tvwElements";
			this.tvwElements.SelectedImageIndex = -1;
			this.tvwElements.Size = new System.Drawing.Size(200, 379);
			this.tvwElements.TabIndex = 0;
			this.tvwElements.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvwElements_BeforeLabelEdit);
			this.tvwElements.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwElements_AfterSelect);
			this.tvwElements.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwElements_BeforeSelect);
			this.tvwElements.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvwElements_AfterLabelEdit);
			// 
			// mnuElements
			// 
			this.mnuElements.Popup += new System.EventHandler(this.mnuElements_Popup);
			// 
			// tbToolBar
			// 
			this.tbToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.tbbNew,
																						 this.tbbDelete});
			this.tbToolBar.ButtonSize = new System.Drawing.Size(16, 16);
			this.tbToolBar.Divider = false;
			this.tbToolBar.DropDownArrows = true;
			this.tbToolBar.Enabled = false;
			this.tbToolBar.ImageList = this.imgToolBarIcons;
			this.tbToolBar.Location = new System.Drawing.Point(0, 0);
			this.tbToolBar.Name = "tbToolBar";
			this.tbToolBar.ShowToolTips = true;
			this.tbToolBar.Size = new System.Drawing.Size(650, 26);
			this.tbToolBar.TabIndex = 0;
			this.tbToolBar.Wrappable = false;
			this.tbToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbToolBar_ButtonClick);
			// 
			// tbbNew
			// 
			this.tbbNew.Enabled = false;
			this.tbbNew.ImageIndex = 0;
			// 
			// tbbDelete
			// 
			this.tbbDelete.Enabled = false;
			this.tbbDelete.ImageIndex = 1;
			// 
			// imgToolBarIcons
			// 
			this.imgToolBarIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgToolBarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBarIcons.ImageStream")));
			this.imgToolBarIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ObjectPropertySheet
			// 
			this.Controls.Add(this.panContents);
			this.Controls.Add(this.tbToolBar);
			this.Name = "ObjectPropertySheet";
			this.Size = new System.Drawing.Size(650, 405);
			this.panContents.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if ( tvwElements.Nodes.Count > 0 )
			{
				TreeNode initialSelectedNode = FindNodeForElement(tvwElements.Nodes[0], m_initialSelectedElement);

				// Make sure everything is visible.

				tvwElements.ExpandAll();
				tvwElements.Focus();
				tvwElements.Select();

				if ( initialSelectedNode == null )
				{
					m_validateSelect = false;
					tvwElements.SelectedNode = tvwElements.Nodes[0];
					m_validateSelect = true;

					if ( !IsEditMode )
						tvwElements.SelectedNode.BeginEdit();
				}
				else
				{
					m_validateSelect = false;
					tvwElements.SelectedNode = initialSelectedNode;
					m_validateSelect = true;
				}
			}
		}

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IObjectPropertyInfo ObjectInfo
		{
			get { return (IObjectPropertyInfo) m_objectInfo; }
			set
			{
				if (value != null)
				{
					if (Settings == null)
					{
						throw new InvalidOperationException("You must assign a value to the Settings property"
							+ " before adding repositories.");
					}
				}
				else
				{
					throw new ArgumentNullException("value");
				}

				using (new LongRunningMonitor(this))
				{
					DisposeObject();
					m_objectInfo = value;

					// Take a copy of the object for comparisons later.

					m_originalObject = m_objectInfo.CloneObject();

					RefreshDisplay();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObjectPropertySettings Settings
		{
			get { return m_settings; }
			set
			{
				if (value == m_settings)
					return;

				if (m_settings != null)
				{
					throw new InvalidOperationException("A settings object has already been set for this"
						+ " object browser and cannot be changed.");
				}

				m_settings = value;

				tbToolBar.Enabled = (m_settings != null);

				if (m_settings != null)
				{
					// Create copies of the supplied image lists using the ImageStream, so we can add some of
					// our own icons to them.

					tvwElements.ImageList = CopyImageList(m_settings.ElementIcons);
					m_settings.Refresh += new EventHandler(m_settings_Refresh);
				}
			}
		}

		[Browsable(false)]
		public bool IsReadOnly
		{
			get { return m_isReadOnly; }
			set { m_isReadOnly = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object InitialSelectedElement
		{
			get { return m_initialSelectedElement; }
			set { m_initialSelectedElement = value; }
		}

		#endregion

		#region Static methods

		private void DisposeObject()
		{
			IDisposable disposable = m_objectInfo as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		private static ImageList CopyImageList(Images source)
		{
			Debug.Assert(source != null, "source != null");

			ImageList list = new ImageList();
			for ( int index = 0; index < source.Count; ++index )
			{
				Icon icon = source.GetIcon(index);
				list.Images.Add(icon);
			}

			return list;

/*			if (source.ImageStream == null)
				return new ImageList();

			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, source.ImageStream);

				stream.Position = 0;

				ImageList copy = new ImageList();
				copy.ImageStream = (ImageListStreamer)formatter.Deserialize(stream);

				return copy;
			}
*/
		}

		private static TreeNode GetRootNode(TreeNode node)
		{
			TreeNode tempNode = node;

			while (tempNode.Parent != null)
			{
				tempNode = tempNode.Parent;
			}

			return tempNode;
		}

		private TreeNode FindNodeForElement(TreeNode node, object element)
		{
			if ( node.Tag is IObjectPropertyInfo )
			{
				IObjectPropertyInfo objectInfo = (IObjectPropertyInfo) node.Tag;
				if ( object.Equals(objectInfo.Object, element) )
					return node;
			}
			else if ( node.Tag is IElementPropertyInfo )
			{
				IElementPropertyInfo elementInfo = (IElementPropertyInfo) node.Tag;
				if ( object.Equals(elementInfo.Element, element) )
					return node;
			}

			// Iterate.

			foreach ( TreeNode childNode in node.Nodes )
			{
				TreeNode foundNode = FindNodeForElement(childNode, element);
				if ( foundNode != null )
					return foundNode;
			}

			return null;
		}

		private static void SavePropertyPage(ElementPropertyPage page)
		{
			try
			{
				page.OnSave();
			}
			catch (System.Exception ex)
			{
				if (page.IsValid)
				{
					throw new ApplicationException("A property page of type '" + page.GetType().FullName
						+ "' threw an exception in OnSave() when IsValid returned true.", ex);
				}
				else
					throw;
			}
		}

		#endregion

		internal void OnElementChanged()
		{
			OnElementChanged(EventArgs.Empty);
		}

		internal void OnEnable()
		{
			OnEnable(EventArgs.Empty);
		}

		internal void OnDisable()
		{
			OnDisable(EventArgs.Empty);
		}

		protected virtual void OnElementChanged(EventArgs e)
		{
			if ( ElementChanged != null )
				ElementChanged(this, e);
		}

		protected virtual void OnEnable(EventArgs e)
		{
			if ( Enable != null )
				Enable(this, e);
		}

		protected virtual void OnDisable(EventArgs e)
		{
			if ( Disable != null )
				Disable(this, e);
		}

		protected virtual void OnPageAdded(EventArgs e)
		{
			if ( PageAdded != null )
				PageAdded(this, e);
		}

		private void RefreshDisplay()
		{
			tvwElements.BeginUpdate();

			try
			{
				AddNodeForObject();
				tvwElements.SelectedNode = (tvwElements.Nodes.Count > 0 ? tvwElements.Nodes[0] : null);
			}
			finally
			{
				tvwElements.EndUpdate();
			}
		}

		private void RefreshNode(TreeNode node)
		{
			node.Nodes.Clear();
			IElementPropertyInfo elementInfo = node.Tag as IElementPropertyInfo;
			foreach ( IElementPropertyInfo childElementInfo in elementInfo.Elements )
				AddNodeForElement(node, childElementInfo);
		}

		private void AddNodeForObject()
		{
			if ( m_objectInfo != null )
			{
				tvwElements.Nodes.Clear();

				// Add the node.

				TreeNode node = tvwElements.Nodes.Add(m_objectInfo.Name);
				node.Tag = m_objectInfo;
				node.ImageIndex = m_objectInfo.ImageIndex;
				node.SelectedImageIndex = node.ImageIndex;

				// Add the page for this element.

				AddPage(m_objectInfo.PropertyPage);

				// Iterate.

				foreach ( IElementPropertyInfo elementInfo in m_objectInfo.Elements )
					AddNodeForElement(node, elementInfo);
			}
		}

		private TreeNode AddNodeForElement(TreeNode parentNode, IElementPropertyInfo elementInfo)
		{
			// Add the node.

			TreeNode newNode = new TreeNode(elementInfo.Name, elementInfo.ImageIndex, elementInfo.ImageIndex);
			newNode.Tag = elementInfo;
			parentNode.Nodes.Add(newNode);

			// Add the page for this element.

			AddPage(elementInfo.PropertyPage);

			// Iterate.

			foreach ( IElementPropertyInfo childElementInfo in elementInfo.Elements )
				AddNodeForElement(newNode, childElementInfo);
			return newNode;
		}

		private void ClearCurrentPage()
		{
			if ( m_currentPage != null )
			{
				m_currentPage.Visible = false;
				m_currentPage = null;
			}
		}

		private void DisplayElement(IElementPropertyInfo element)
		{
			Debug.Assert(element != null, "element != null");

			// Set the label.

			lblName.Text = element.Name;

			// Property page control.

			ClearCurrentPage();
			ElementPropertyPage page = element.PropertyPage;
			if ( page != null )
			{
				bool pageAdded = AddPage(page);
				page.Size = panDetails.Size;
				page.Visible = true;
				m_currentPage = page;

				// Now that everything is set fire the event if needed.

				if ( pageAdded )
					OnPageAdded(EventArgs.Empty);
			}
		}

		private bool AddPage(ElementPropertyPage page)
		{
			if ( page == null )
				return false;

			// Check whether it is already there.

			if ( panDetails.Controls.Contains(page) )
				return false;

			page.PropertyPageParent = this;
			page.Location = new Point(0, 0);
			page.Visible = false;
			panDetails.Controls.Add(page);
			m_propertyPages.Add(page);
			return true;
		}

		private void ElementNodeSelected(TreeNode node)
		{
			Debug.Assert(node != null, "node != null");

			IElementPropertyInfo previousElement = m_selectedElement;

			if (node.Tag is IElementPropertyInfo)
			{
				DisplayElement((IElementPropertyInfo)node.Tag);
				m_selectedElement = (IElementPropertyInfo)node.Tag;
			}
		}

		private void OnRefresh()
		{
		}

		#region Event Handlers

		private void tbToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if ( e.Button == tbbNew )
			{
				if ( tvwElements.SelectedNode != null )
					OnNew(tvwElements.SelectedNode);
			}
			else if ( e.Button == tbbDelete )
			{
				if ( tvwElements.SelectedNode != null )
					OnDelete(tvwElements.SelectedNode);
			}
		}

		private void tvwMembers_NodeActivate(object sender, TreeViewEventArgs e)
		{
		}

		private void m_settings_Refresh(object sender, EventArgs e)
		{
		}

		#endregion

		private void splitVertical_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			int newLeft = splitVertical.Location.X + 10;
			int newWidth = ClientSize.Width - splitVertical.Location.X - 15;

			lblName.Left = newLeft;
			lblName.Width = newWidth;
			grpSeparator.Left = newLeft;
			grpSeparator.Width = newWidth;
			panDetails.Left = newLeft;
			panDetails.Width = newWidth;
		}

		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				// Check all pages.

				foreach ( ElementPropertyPage page in m_propertyPages )
				{
					if ( !page.IsValid )
						return false;
				}

				return true;
			}
		}

		[Browsable(false)]
		public bool IsChanged
		{
			get
			{
				if ( m_currentPage != null )
				{
					try
					{
						SavePropertyPage(m_currentPage);
					}
					catch (System.Exception)
					{
						// Assume the page was valid when it was first shown, so if saving throws an exception
						// then it has changed.

						return true;
					}
				}

				return !object.Equals(m_originalObject, m_objectInfo.CloneObject());
			}
		}

		[Browsable(false)]
		public bool IsEditMode
		{
			get { return m_objectInfo == null ? true : m_objectInfo.IsEditMode; }
		}

		public bool OnApply()
		{
			// Let each page deal with the apply.

			try
			{
				foreach ( ElementPropertyPage page in m_propertyPages )
					SavePropertyPage(page);
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The following exception has occurred:").ShowDialog();
				return false;
			}

			// Reset.

			m_originalObject = m_objectInfo.CloneObject();
			return true;
		}

		private void tvwElements_BeforeLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( !m_allowEdit || IsReadOnly || (IsEditMode && e.Node.Parent == null) )
			{
				e.CancelEdit = true;
			}
			else
			{
				IElementPropertyInfo propertyInfo = e.Node.Tag as IElementPropertyInfo;
				if (propertyInfo == null || !propertyInfo.CanRenameElement)
				{
					e.CancelEdit = true;
				}
				else
				{
					// Disable the Delete menu item while editing, so that the Del key doesn't activate it.

					if (m_deleteMenuItem != null)
					{
						m_deleteMenuItem.Enabled = false;
					}
				}
			}
		}

		private void tvwElements_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			try
			{
				// Re-enable the Delete menu item.

				if (m_deleteMenuItem != null)
				{
					m_deleteMenuItem.Enabled = true;
				}

				string newName = e.Label;
				if ( newName != null )
				{
					IElementPropertyInfo elementInfo = e.Node.Tag as IElementPropertyInfo;
					if ( elementInfo != null )
					{
						// Make sure any changes in the property page is captured.

						if ( m_currentPage != null )
							SavePropertyPage(m_currentPage);
						elementInfo.RenameElement(newName);
						foreach ( IElementPropertyInfo childElementInfo in elementInfo.Elements )
							childElementInfo.RefreshParentElement(elementInfo.Element);

						// Refresh the nodes.

						DisplayElement(elementInfo);
						OnElementChanged();
					}
				}
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The following exception occurred:").ShowDialog();
				e.CancelEdit = true;
				e.Node.BeginEdit();
			}
		}

		private void mnuElements_Popup(object sender, System.EventArgs e)
		{
			SetContextMenu(tvwElements.ContextNode);
		}
		
		private void SetContextMenu(TreeNode node)
		{
			// Clear what may already be there.

			mnuElements.MenuItems.Clear();
			m_deleteMenuItem = null;

			if ( !IsReadOnly )
			{
				// Grab the node and the element info.

				if ( node == null )
					return;

				IElementPropertyInfo elementInfo = node.Tag as IElementPropertyInfo;
				if ( elementInfo == null )
					return;

				// Add the menu items.

				if ( elementInfo.CanCreateElement )
				{
					mnuElements.MenuItems.Add(new MenuItem("New " + elementInfo.CreateElementName,
						new System.EventHandler(NewHandler), Shortcut.Ins));
				}

				if ( elementInfo.Views != null && elementInfo.Views.Length > 0 )
				{
					MenuItem mnuView = new MenuItem("View");
					string currentView = elementInfo.CurrentView;

					foreach ( string view in elementInfo.Views )
					{
						MenuItem menu = new MenuItem(view, new System.EventHandler(ViewHandler));

						if ( currentView == null )
						{
							menu.Checked = true;
							currentView = view;
						}
						else
						{
							menu.Checked = view == currentView;
						}
						
						mnuView.MenuItems.Add(menu);
					}
					mnuElements.MenuItems.Add(mnuView);
				}

				if ( elementInfo.CanDeleteElement )
				{
					m_deleteMenuItem = new MenuItem("Delete", new System.EventHandler(DeleteHandler), Shortcut.Del);
					mnuElements.MenuItems.Add(m_deleteMenuItem);
				}

				if ( elementInfo.CanRenameElement )
				{
					mnuElements.MenuItems.Add(new MenuItem("Rename", new System.EventHandler(RenameHandler),
						Shortcut.F2));
				}
			}
		}

		private void NewHandler(object sender, EventArgs e)
		{
			TreeNode node = tvwElements.ContextNode;
			if (node == null)
				return;

			OnNew(node);
		}

		private void OnNew(TreeNode node)
		{
			IElementPropertyInfo elementInfo = node.Tag as IElementPropertyInfo;
			if ( elementInfo == null )
				return;

			// Create a new element.

			IElementPropertyInfo newElementInfo = elementInfo.CreateElement();
			OnElementChanged();

			// Add a new node.

			TreeNode newNode = AddNodeForElement(node, newElementInfo);
			newNode.ExpandAll();
			tvwElements.Focus();
			tvwElements.Select();
			tvwElements.SelectedNode = newNode;

			if ( newElementInfo.CanRenameElement )
				newNode.BeginEdit();
		}

		private void RenameHandler(object sender, EventArgs e)
		{
			TreeNode node = tvwElements.ContextNode;
			if (node == null)
				return;

			node.BeginEdit();
		}

		private void ViewHandler(object sender, EventArgs e)
		{
			// Change the view.

			MenuItem menu = sender as MenuItem;
			if ( menu == null )
				return;

			TreeNode node = tvwElements.ContextNode;
			if (node == null)
				return;

			IElementPropertyInfo elementInfo = node.Tag as IElementPropertyInfo;
			if ( elementInfo == null )
				return;

			// Make sure any changes in the page are captured.

			if ( m_currentPage != null )
				SavePropertyPage(m_currentPage);

			// Reshow the element.

			elementInfo.ViewElement(menu.Text);
			DisplayElement(elementInfo);
		}

		private void DeleteHandler(object sender, EventArgs e)
		{
			TreeNode node = tvwElements.ContextNode;
			if (node == null)
				return;

			OnDelete(node);
		}

		private void OnDelete(TreeNode node)
		{
			// Prompt first.

			string promptText = "Are you sure you want to delete '" + node.Text + "'?";
			if ( MessageBox.Show(promptText, "Confirm deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK )
				return;

			// Delete the element itself.

			IElementPropertyInfo elementInfo = node.Tag as IElementPropertyInfo;
			if ( elementInfo == null )
				return;
			elementInfo.DeleteElement();
			OnElementChanged();

			// Delete the node.

			ClearCurrentPage();
			TreeNode parentNode = node.Parent;
			tvwElements.Nodes.Remove(node);
			tvwElements.Focus();
			tvwElements.Select();
			tvwElements.SelectedNode = parentNode;
		}

		private void tvwElements_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			IElementPropertyInfo elementInfo = e.Node.Tag as IElementPropertyInfo;
			DisplayElement(elementInfo);

			// Update the toolbar and context menu.

			UpdateToolbarButtons(elementInfo);
			SetContextMenu(tvwElements.SelectedNode);
		}

		private void tvwElements_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( m_validateSelect )
			{
				// Check that the page is valid before allowing the user to move away.

				if ( m_currentPage != null )
				{
					if ( !m_currentPage.IsValid )
					{
						m_currentPage.SelectInvalid();
						e.Cancel = true;
					}
					else
					{
						// Make sure any changes in the page are saved.

						SavePropertyPage(m_currentPage);
					}
				}
			}
		}

		private void panDetails_SizeChanged(object sender, System.EventArgs e)
		{
			if ( m_currentPage != null )
				m_currentPage.Size = panDetails.Size;
		}

		private void UpdateToolbarButtons(IElementPropertyInfo elementInfo)
		{
			if ( IsReadOnly )
			{
				tbbNew.Enabled = false;
				tbbDelete.Enabled = false;
			}
			else
			{
				tbbNew.Enabled = elementInfo.CanCreateElement;
				tbbDelete.Enabled = elementInfo.CanDeleteElement;
			}
		}
	}
}

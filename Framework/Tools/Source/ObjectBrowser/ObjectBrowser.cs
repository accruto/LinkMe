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

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// An object browser control. This control uses interfaces to retrieve the data it displays, so it can be used
	/// for different kinds of repositories and types. For example, there is an implementation of these
	/// interfaces for .NET assemblies, which provides a .NET object browser similar to the Visual Studio.NET one.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "ObjectBrowser.bmp")]
	public class ObjectBrowser : UserControl
	{
		#region Nested Types

		public class SelectionInfo
		{
			internal string TypePath;
			internal bool TypeExpanded;
			internal string[] TagTypes;
			internal bool RestoreExpansion;
			internal int RootNodeIndex;
			internal string MemberPath;

			internal SelectionInfo(string typePath, bool typeExpanded, string[] tagTypes, int rootNodeIndex,
				string memberPath)
			{
				TypePath = typePath;
				TypeExpanded = typeExpanded;
				TagTypes = tagTypes;
				RootNodeIndex = rootNodeIndex;
				MemberPath = memberPath;
			}

			public override bool Equals(object obj)
			{
				SelectionInfo other = obj as SelectionInfo;
				if (other == null)
					return false;

				if (TypePath != other.TypePath || RootNodeIndex != other.RootNodeIndex ||
					MemberPath != other.MemberPath)
				{
					return false;
				}

				if (RestoreExpansion)
				{
					if (!other.RestoreExpansion || TypeExpanded != other.TypeExpanded)
						return false;
				}

				if (TagTypes.Length != other.TagTypes.Length)
					return false;

				for (int index = 0; index < TagTypes.Length; index++)
				{
					if (TagTypes[index] != other.TagTypes[index])
						return false;
				}

				return true;
			}

			public override int GetHashCode()
			{
				int hashCode = TypeExpanded.GetHashCode() ^ RestoreExpansion.GetHashCode()
					^ RootNodeIndex.GetHashCode();

				if (TypePath != null)
				{
					hashCode ^= TypePath.GetHashCode();
				}
				if (TagTypes != null)
				{
					hashCode ^= TagTypes.GetHashCode();
				}
				if (MemberPath != null)
				{
					hashCode ^= MemberPath.GetHashCode();
				}

				return hashCode;
			}
		}

		private class BasesNodeTag
		{
			internal static BasesNodeTag Value = new BasesNodeTag();

			private BasesNodeTag()
			{
			}
		}

		private class PlaceholderTag
		{
			internal bool AutoCheckChildren;

			internal PlaceholderTag(bool autoCheckChildren)
			{
				AutoCheckChildren = autoCheckChildren;
			}
		}

		#endregion

		/// <summary>
		/// Raised when the object selected in the left pane changes. Note that this event is not raised
		/// if a different tree node is selected, but the content of the node is the same as that of the
		/// previously selected node.
		/// </summary>
		public event EventHandler TypeSelectionChanged;
		/// <summary>
		/// Raised when the object selected in the right pane changes.
		/// </summary>
		public event EventHandler MemberSelectionChanged;
		/// <summary>
		/// Raised when the user activates a node in the right pane by double-clicking it or pressing Enter.
		/// </summary>
		public event MemberEventHandler MemberActivate;
		/// <summary>
		/// Raised after the selection passed to RestoreSelection() is actually restored. This may not happen
		/// immediately, because the entire control may be invisible when RestoreSelection() is called.
		/// </summary>
		public event EventHandler SelectionRestored;

		private const string NodeTextRepositoryChildrenPlaceholder = "Repository Children Placeholder";
		private const string NodeTextNamespaceChildrenPlaceholder = "Namespace Children Placeholder";
		private const string NodeTextTypeChildrenPlaceholder = "Type Children Placeholder";

		// imgToolBarIcons

		private const int ImageIndexShowNonPublic = 3;
		private const int ImageIndexHideNonPublic = 4;

		// An array of the repositories (eg. .NET assemblies) to be displayed in the control.
		private ArrayList m_repositories = new ArrayList();
		private IElementBrowserInfo m_selectedType = null;
		private IMemberBrowserInfo m_selectedMember = null;
		private bool m_ignoreTypeSelectionChanges = false;
		private bool m_ignoreMemberSelectionChanges = false;
		private ObjectBrowserSettings m_settings = null;
		private ObjectBrowserManager m_manager = null;
		private ArrayList m_selectionHistory = new ArrayList();
		// Specifies where in the history the current selection is. When the selection changes (except
		// by navigating back/forward) this is set to the last item in m_selectionHistory.
		private int m_historyIndex = -1;
		private SelectionInfo m_selectWhenShown = null;
		private bool m_ignoreTypeAfterCheck = false;

		// Controls

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel panContents;
		private TreeView tvwMembers;
		private TreeView tvwTypes;
		private Splitter splitVertical;
		private RichTextBox txtDescription;
		private Splitter splitHorizontal;
		private System.Windows.Forms.ToolBarButton tbbTypeOrder;
		private System.Windows.Forms.ImageList imgToolBarIcons;
		private System.Windows.Forms.ToolBar tbToolBar;
		private System.Windows.Forms.ContextMenu mnuTypeOrder;
		private System.Windows.Forms.MenuItem mnuTypeOrderObjectAccess;
		private System.Windows.Forms.MenuItem mnuTypeOrderObjectType;
		private System.Windows.Forms.MenuItem mnuTypeOrderAlphabetical;
		private System.Windows.Forms.ContextMenu mnuMemberOrder;
		private System.Windows.Forms.MenuItem mnuMemberOrderAlphabetical;
		private System.Windows.Forms.MenuItem mnuMemberOrderMemberType;
		private System.Windows.Forms.MenuItem mnuMemberOrderMemberAccess;
		private System.Windows.Forms.ToolBarButton tbbMemberOrder;
		private System.Windows.Forms.ToolBarButton tbbShowNonPublic;
		private System.Windows.Forms.ToolBarButton toolSep2;
		private System.Windows.Forms.ToolBarButton tbbGoBack;
		private System.Windows.Forms.ToolBarButton tbbGoForward;
		private System.Windows.Forms.ImageList imgBaseIcons;
		private System.Windows.Forms.ToolBarButton tbbSep1;

		#region Constructors

		public ObjectBrowser()
		{
			InitializeComponent();

			splitHorizontal.Initialise();
			splitVertical.Initialise();

			tvwTypes.VisibleChanged += new EventHandler(treeView_VisibleChanged);
			tvwMembers.VisibleChanged += new EventHandler(treeView_VisibleChanged);
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
					m_settings.ShowMembersChanged -= new EventHandler(m_settings_ShowMembersChanged);
					m_settings.TypeOrderChanged -= new EventHandler(m_settings_TypeOrderChanged);
					m_settings.MemberOrderChanged -= new EventHandler(m_settings_MemberOrderChanged);
					m_settings.ShowNonPublicChanged -= new EventHandler(m_settings_ShowNonPublicChanged);
					m_settings = null;
				}

				// The manager object is "owned" by this instance of the object browser, so it's we can
				// dispose of it (unlike the settings object, which may be shared by multiple instances).

				if (m_manager != null)
				{
					m_manager.Dispose();
					m_manager = null;
				}

				// Dispose of all repositories that support IDisposable (the manager may not have taken
				// care of all of them).

				DisposeRepositories();

				m_selectedType = null;
				m_selectedMember = null;
				m_selectionHistory = null;
				m_selectWhenShown = null;
				txtDescription.Tag = null;

				TreeView.ClearTagsRecursive(tvwTypes.Nodes);
				TreeView.ClearTagsRecursive(tvwMembers.Nodes);

				mnuTypeOrder.Dispose();
				mnuMemberOrder.Dispose();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ObjectBrowser));
			this.panContents = new System.Windows.Forms.Panel();
			this.tvwMembers = new LinkMe.Framework.Tools.Controls.TreeView();
			this.splitVertical = new LinkMe.Framework.Tools.Controls.Splitter();
			this.tvwTypes = new LinkMe.Framework.Tools.Controls.TreeView();
			this.splitHorizontal = new LinkMe.Framework.Tools.Controls.Splitter();
			this.txtDescription = new LinkMe.Framework.Tools.Controls.RichTextBox();
			this.tbToolBar = new System.Windows.Forms.ToolBar();
			this.tbbTypeOrder = new System.Windows.Forms.ToolBarButton();
			this.mnuTypeOrder = new System.Windows.Forms.ContextMenu();
			this.mnuTypeOrderAlphabetical = new System.Windows.Forms.MenuItem();
			this.mnuTypeOrderObjectType = new System.Windows.Forms.MenuItem();
			this.mnuTypeOrderObjectAccess = new System.Windows.Forms.MenuItem();
			this.tbbMemberOrder = new System.Windows.Forms.ToolBarButton();
			this.mnuMemberOrder = new System.Windows.Forms.ContextMenu();
			this.mnuMemberOrderAlphabetical = new System.Windows.Forms.MenuItem();
			this.mnuMemberOrderMemberType = new System.Windows.Forms.MenuItem();
			this.mnuMemberOrderMemberAccess = new System.Windows.Forms.MenuItem();
			this.tbbSep1 = new System.Windows.Forms.ToolBarButton();
			this.tbbShowNonPublic = new System.Windows.Forms.ToolBarButton();
			this.toolSep2 = new System.Windows.Forms.ToolBarButton();
			this.tbbGoBack = new System.Windows.Forms.ToolBarButton();
			this.tbbGoForward = new System.Windows.Forms.ToolBarButton();
			this.imgToolBarIcons = new System.Windows.Forms.ImageList(this.components);
			this.imgBaseIcons = new System.Windows.Forms.ImageList(this.components);
			this.panContents.SuspendLayout();
			this.SuspendLayout();
			// 
			// panContents
			// 
			this.panContents.Controls.Add(this.tvwMembers);
			this.panContents.Controls.Add(this.splitVertical);
			this.panContents.Controls.Add(this.tvwTypes);
			this.panContents.Controls.Add(this.splitHorizontal);
			this.panContents.Controls.Add(this.txtDescription);
			this.panContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panContents.Location = new System.Drawing.Point(0, 26);
			this.panContents.Name = "panContents";
			this.panContents.Size = new System.Drawing.Size(760, 374);
			this.panContents.TabIndex = 0;
			// 
			// tvwMembers
			// 
			this.tvwMembers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwMembers.ImageIndex = -1;
			this.tvwMembers.Location = new System.Drawing.Point(386, 0);
			this.tvwMembers.Name = "tvwMembers";
			this.tvwMembers.SelectedImageIndex = -1;
			this.tvwMembers.Size = new System.Drawing.Size(374, 331);
			this.tvwMembers.TabIndex = 2;
			this.tvwMembers.NodeActivate += new System.Windows.Forms.TreeViewEventHandler(this.tvwMembers_NodeActivate);
			this.tvwMembers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMembers_AfterSelect);
			this.tvwMembers.Enter += new System.EventHandler(this.tvwMembers_Enter);
			// 
			// splitVertical
			// 
			this.splitVertical.Location = new System.Drawing.Point(380, 0);
			this.splitVertical.Name = "splitVertical";
			this.splitVertical.Size = new System.Drawing.Size(6, 331);
			this.splitVertical.TabIndex = 1;
			this.splitVertical.TabStop = false;
			// 
			// tvwTypes
			// 
			this.tvwTypes.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwTypes.HideSelection = false;
			this.tvwTypes.ImageIndex = -1;
			this.tvwTypes.Location = new System.Drawing.Point(0, 0);
			this.tvwTypes.Name = "tvwTypes";
			this.tvwTypes.SelectedImageIndex = -1;
			this.tvwTypes.Size = new System.Drawing.Size(380, 331);
			this.tvwTypes.TabIndex = 0;
			this.tvwTypes.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwTypes_BeforeExpand);
			this.tvwTypes.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwTypes_AfterCheck);
			this.tvwTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwTypes_AfterSelect);
			this.tvwTypes.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwTypes_BeforeCheck);
			this.tvwTypes.Enter += new System.EventHandler(this.tvwTypes_Enter);
			// 
			// splitHorizontal
			// 
			this.splitHorizontal.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitHorizontal.Location = new System.Drawing.Point(0, 331);
			this.splitHorizontal.Name = "splitHorizontal";
			this.splitHorizontal.Size = new System.Drawing.Size(760, 6);
			this.splitHorizontal.TabIndex = 3;
			this.splitHorizontal.TabStop = false;
			// 
			// txtDescription
			// 
			this.txtDescription.AcceptsControlTab = true;
			this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
			this.txtDescription.DetectUrls = false;
			this.txtDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtDescription.Location = new System.Drawing.Point(0, 337);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.Size = new System.Drawing.Size(760, 37);
			this.txtDescription.TabIndex = 4;
			this.txtDescription.Text = "";
			this.txtDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtDescription_LinkClicked);
			// 
			// tbToolBar
			// 
			this.tbToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.tbbTypeOrder,
																						 this.tbbMemberOrder,
																						 this.tbbSep1,
																						 this.tbbShowNonPublic,
																						 this.toolSep2,
																						 this.tbbGoBack,
																						 this.tbbGoForward});
			this.tbToolBar.ButtonSize = new System.Drawing.Size(16, 16);
			this.tbToolBar.Divider = false;
			this.tbToolBar.DropDownArrows = true;
			this.tbToolBar.Enabled = false;
			this.tbToolBar.ImageList = this.imgToolBarIcons;
			this.tbToolBar.Location = new System.Drawing.Point(0, 0);
			this.tbToolBar.Name = "tbToolBar";
			this.tbToolBar.ShowToolTips = true;
			this.tbToolBar.Size = new System.Drawing.Size(760, 26);
			this.tbToolBar.TabIndex = 0;
			this.tbToolBar.Wrappable = false;
			this.tbToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbToolBar_ButtonClick);
			// 
			// tbbTypeOrder
			// 
			this.tbbTypeOrder.DropDownMenu = this.mnuTypeOrder;
			this.tbbTypeOrder.ImageIndex = 0;
			this.tbbTypeOrder.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.tbbTypeOrder.Text = "Alphabetically";
			this.tbbTypeOrder.ToolTipText = "Type order";
			// 
			// mnuTypeOrder
			// 
			this.mnuTypeOrder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuTypeOrderAlphabetical,
																						 this.mnuTypeOrderObjectType,
																						 this.mnuTypeOrderObjectAccess});
			// 
			// mnuTypeOrderAlphabetical
			// 
			this.mnuTypeOrderAlphabetical.Checked = true;
			this.mnuTypeOrderAlphabetical.Index = 0;
			this.mnuTypeOrderAlphabetical.RadioCheck = true;
			this.mnuTypeOrderAlphabetical.Text = "&Sort Alphabetically";
			this.mnuTypeOrderAlphabetical.Click += new System.EventHandler(this.mnuTypeOrderAlphabetical_Click);
			// 
			// mnuTypeOrderObjectType
			// 
			this.mnuTypeOrderObjectType.Index = 1;
			this.mnuTypeOrderObjectType.RadioCheck = true;
			this.mnuTypeOrderObjectType.Text = "Sort By Object &Type";
			this.mnuTypeOrderObjectType.Click += new System.EventHandler(this.mnuTypeOrderObjectType_Click);
			// 
			// mnuTypeOrderObjectAccess
			// 
			this.mnuTypeOrderObjectAccess.Index = 2;
			this.mnuTypeOrderObjectAccess.RadioCheck = true;
			this.mnuTypeOrderObjectAccess.Text = "Sort By Object Acc&ess";
			this.mnuTypeOrderObjectAccess.Click += new System.EventHandler(this.mnuTypeOrderObjectAccess_Click);
			// 
			// tbbMemberOrder
			// 
			this.tbbMemberOrder.DropDownMenu = this.mnuMemberOrder;
			this.tbbMemberOrder.ImageIndex = 0;
			this.tbbMemberOrder.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.tbbMemberOrder.ToolTipText = "Member order";
			// 
			// mnuMemberOrder
			// 
			this.mnuMemberOrder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuMemberOrderAlphabetical,
																						   this.mnuMemberOrderMemberType,
																						   this.mnuMemberOrderMemberAccess});
			// 
			// mnuMemberOrderAlphabetical
			// 
			this.mnuMemberOrderAlphabetical.Checked = true;
			this.mnuMemberOrderAlphabetical.Index = 0;
			this.mnuMemberOrderAlphabetical.RadioCheck = true;
			this.mnuMemberOrderAlphabetical.Text = "&Sort Alphabetically";
			this.mnuMemberOrderAlphabetical.Click += new System.EventHandler(this.mnuMemberOrderAlphabetical_Click);
			// 
			// mnuMemberOrderMemberType
			// 
			this.mnuMemberOrderMemberType.Index = 1;
			this.mnuMemberOrderMemberType.RadioCheck = true;
			this.mnuMemberOrderMemberType.Text = "Sort By Member &Type";
			this.mnuMemberOrderMemberType.Click += new System.EventHandler(this.mnuMemberOrderMemberType_Click);
			// 
			// mnuMemberOrderMemberAccess
			// 
			this.mnuMemberOrderMemberAccess.Index = 2;
			this.mnuMemberOrderMemberAccess.RadioCheck = true;
			this.mnuMemberOrderMemberAccess.Text = "Sort By Member Acc&ess";
			this.mnuMemberOrderMemberAccess.Click += new System.EventHandler(this.mnuMemberOrderMemberAccess_Click);
			// 
			// tbbSep1
			// 
			this.tbbSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbShowNonPublic
			// 
			this.tbbShowNonPublic.ImageIndex = 3;
			this.tbbShowNonPublic.Pushed = true;
			this.tbbShowNonPublic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tbbShowNonPublic.ToolTipText = "Non-public types and members are shown";
			// 
			// toolSep2
			// 
			this.toolSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbGoBack
			// 
			this.tbbGoBack.Enabled = false;
			this.tbbGoBack.ImageIndex = 5;
			this.tbbGoBack.ToolTipText = "Back";
			// 
			// tbbGoForward
			// 
			this.tbbGoForward.Enabled = false;
			this.tbbGoForward.ImageIndex = 6;
			this.tbbGoForward.ToolTipText = "Forward";
			// 
			// imgToolBarIcons
			// 
			this.imgToolBarIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgToolBarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBarIcons.ImageStream")));
			this.imgToolBarIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imgBaseIcons
			// 
			this.imgBaseIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgBaseIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgBaseIcons.ImageStream")));
			this.imgBaseIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ObjectBrowser
			// 
			this.Controls.Add(this.panContents);
			this.Controls.Add(this.tbToolBar);
			this.Name = "ObjectBrowser";
			this.Size = new System.Drawing.Size(760, 400);
			this.panContents.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IRepositoryBrowserInfo[] Repositories
		{
			get { return (IRepositoryBrowserInfo[])m_repositories.ToArray(typeof(IRepositoryBrowserInfo)); }
			set
			{
				if (value != null && value.Length > 0)
				{
					if (Settings == null)
					{
						throw new InvalidOperationException("You must assign a value to the Settings property"
							+ " before adding repositories.");
					}
					if (Manager == null)
					{
						throw new InvalidOperationException("You must assign a value to the Manager property"
							+ " before adding repositories.");
					}
				}

				using (new LongRunningMonitor(this))
				{
					DisposeRepositories();

					m_repositories = (value == null ? new ArrayList() : new ArrayList(value));

					foreach (IRepositoryBrowserInfo repository in m_repositories)
					{
						repository.Manager = Manager;
					}

					// Clear the history, since it referred to the old repositories.
					ResetSelectionHistory();

					RefreshRepositoryDisplay();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObjectBrowserSettings Settings
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

					tvwTypes.ImageList = CopyImageList(m_settings.TypeIcons);
					tvwTypes.ImageList.Images.Add(imgBaseIcons.Images[0]);
					tvwTypes.CheckBoxes = m_settings.ShowTypeCheckBoxes;

					tvwMembers.ImageList = CopyImageList(m_settings.MemberIcons);

					ShowHideMembers();

					mnuTypeOrderObjectAccess.Visible = m_settings.ObjectAccessSupported;
					mnuMemberOrderMemberAccess.Visible = m_settings.ObjectAccessSupported;
					tbbShowNonPublic.Visible = m_settings.ObjectAccessSupported;

					SetTypeOrderUI(m_settings.TypeOrder);
					SetMemberOrderUI(m_settings.MemberOrder);
					SetShowNonPublicUI(m_settings.ShowNonPublic);

					m_settings.Refresh += new EventHandler(m_settings_Refresh);
					m_settings.ShowMembersChanged += new EventHandler(m_settings_ShowMembersChanged);
					m_settings.TypeOrderChanged += new EventHandler(m_settings_TypeOrderChanged);
					m_settings.MemberOrderChanged += new EventHandler(m_settings_MemberOrderChanged);
					m_settings.ShowNonPublicChanged += new EventHandler(m_settings_ShowNonPublicChanged);

					if (m_manager != null)
					{
						m_manager.SetSettings(m_settings);
					}
				}
			}
		}

		/// <summary>
		/// The object browser cache object created by the settings object.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObjectBrowserManager Manager
		{
			get { return m_manager; }
			set
			{
				if (m_manager != null)
				{
					m_manager.Dispose();
				}

				m_manager = value;

				if (m_manager != null)
				{
					m_manager.SetSettings(m_settings);
				}
			}
		}

		/// <summary>
		/// The type currently selected in the types TreeView. This property is not affected by which TreeView
		/// has the focus - if there is a node selected in the types TreeView and it's a type (rather than
		/// a repository or a namespace) then this value is non-null.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IElementBrowserInfo SelectedType
		{
			get { return m_selectedType; }
			set { tvwTypes.SelectedNode = (value == null ? null : FindContentInTypeTree(value, false)); }
		}

		/// <summary>
		/// The member currently selected in the member TreeView. This property depends on which TreeView
		/// has the focus - if it's the types TreeView then this property returns null. (That is, the
		/// type as a whole is selected, not any particular member of the type.)
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IMemberBrowserInfo SelectedMember
		{
			get { return m_selectedMember; }
			set
			{
				if (value == null)
				{
					tvwMembers.SelectedNode = null;
				}
				else
				{
					// First see if the right type is already selected.

					TreeNode member = FindMemberNode(value);
					if (member == null)
					{
						// Find the type first, then the node.

						TreeNode type = FindContentInTypeTree(value.Type, false);
						if (type != null)
						{
							tvwTypes.SelectedNode = type;
							member = FindMemberNode(value);
						}
					}

					tvwMembers.SelectedNode = member;
				}
			}
		}

		/// <summary>
		/// The type currently selected in the types TreeView. This property is not affected by which TreeView
		/// has the focus - if there is a node selected in the types TreeView and it's a type (rather than
		/// a repository or a namespace) then this value is non-null.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SelectedTypeChecked
		{
			get
			{
				if (Settings.ShowTypeCheckBoxes)
					return (tvwTypes.SelectedNode == null ? false : tvwTypes.SelectedNode.Checked);
				else
					return false;
			}
			set
			{
				if (Settings.ShowTypeCheckBoxes && tvwTypes.SelectedNode != null)
				{
					tvwTypes.SelectedNode.Checked = value;
				}
			}
		}

		#endregion

		#region Static methods

		private static void DisposeRepository(object repository)
		{
			IDisposable disposable = repository as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		private static ImageList CopyImageList(ImageList source)
		{
			Debug.Assert(source != null, "source != null");

			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, source.ImageStream);

				stream.Position = 0;

				ImageList copy = new ImageList();
				copy.ImageStream = (ImageListStreamer)formatter.Deserialize(stream);

				return copy;
			}
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

		private static void AddBaseNodesForType(TreeNode nodeType, ITypeBrowserInfo type)
		{
			ITypeBrowserInfo[] baseTypes = type.BaseTypes;
			if (baseTypes != null)
			{
				foreach (ITypeBrowserInfo baseType in baseTypes)
				{
					TreeNode nodeBase = nodeType.Nodes.Add(baseType.NodeText);

					nodeBase.Tag = baseType;
					nodeBase.ImageIndex = baseType.ImageIndex;
					nodeBase.SelectedImageIndex = nodeBase.ImageIndex;

					if (baseType.HasBaseTypes)
					{
						// Add a dummy node so that the TreeView allows the user to expand this type.
						TreeNode placeholder = new TreeNode(NodeTextTypeChildrenPlaceholder);
						placeholder.Tag = new PlaceholderTag(false);
						nodeBase.Nodes.Add(placeholder);
					}
				}
			}
		}

		private static bool NodeChildIsPlaceholder(TreeNode node)
		{
			if (node.Nodes.Count != 1)
				return false;
			else
				return (node.Nodes[0].Tag is PlaceholderTag);
		}

		#endregion

		public void AddRepository(IRepositoryBrowserInfo repository)
		{
			if (repository == null)
				throw new ArgumentNullException("repository");
			if (Settings == null)
			{
				throw new InvalidOperationException("You must assign a value to the Settings property"
					+ " before adding repositories.");
			}
			if (Manager == null)
			{
				throw new InvalidOperationException("You must assign a value to the Manager property"
					+ " before adding repositories.");
			}

			try
			{
				foreach (IRepositoryBrowserInfo existing in Repositories)
				{
					if (existing.RepositoryEquals(repository))
						throw new ApplicationException("The repository has already been added.");
				}

				m_repositories.Add(repository);
				repository.Manager = Manager;
				AddNodeForRepository(repository);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to add repository '" + repository.DisplayName + "'.", ex);
			}
		}

		public void RemoveRepository(IRepositoryBrowserInfo repository)
		{
			if (repository == null)
				throw new ArgumentNullException("repository");

			try
			{
				DisposeRepository(repository);

				m_repositories.Remove(repository);

				TreeNode repositoryNode = FindRepositoryNode(repository, false);
				Debug.Assert(repositoryNode != null, "repositoryNode != null");

				RemoveRepositoryFromSelectionHistory(repositoryNode.Index);

				// When the node is removed the selection may change, but this happens BEFORE node
				// indexes are updated. We want to process the selection after that, so that the
				// correct node index is added to the selection history.

				m_ignoreTypeSelectionChanges = true;
				try
				{
					tvwTypes.Nodes.Remove(repositoryNode);
					if (tvwTypes.SelectedNode == null)
					{
						txtDescription.Text = string.Empty;
						ClearMembers();
					}
					else
					{
						ProcessNodeSelected(tvwTypes.SelectedNode);
					}
				}
				finally
				{
					m_ignoreTypeSelectionChanges = false;
				}

				if (m_selectedType == repository)
				{
					m_selectedType = null;
					OnTypeSelectionChanged(EventArgs.Empty);
				}
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to remove repository '" + repository.DisplayName + "'.", ex);
			}
		}

		public SelectionInfo SaveSelection()
		{
			if (tvwTypes.SelectedNode == null)
				return null;
			else
			{
				TreeNode node = tvwTypes.SelectedNode;

				// Store the type of each node in the tree as well as its name (as an assembly qualified reference).
				// This allows for different kinds of nodes that have the same node text. If two nodes have the
				// same text AND the same type then there's nothing we can do - just select the first one we find.
				// Note that the name or type of the root node is not stored - we store its index instead, assuming
				// that root nodes are never re-ordered.

				ArrayList tagTypes = new ArrayList();
				for (TreeNode tempNode = node; tempNode.Parent != null; tempNode = tempNode.Parent)
				{
					tagTypes.Insert(0, (tempNode.Tag == null ? null : tempNode.Tag.GetType().AssemblyQualifiedName));
				}

				return new SelectionInfo(node.FullPath, node.IsExpanded, (string[])tagTypes.ToArray(typeof(string)),
					GetRootNode(node).Index, (m_selectedMember == null ? null : tvwMembers.SelectedNode.FullPath));
			}
		}

		public void RestoreSelection(SelectionInfo selectionInfo)
		{
			if (selectionInfo == null)
				return;

			RestoreSelectionInternal(selectionInfo);

			// The control may not be visible yet, so the selection may not have actually been restored yet.
			// We cannot rely on SaveSelection() to return the same result here, so we pass the selection to
			// AddSelectionToHistory().

			AddSelectionToHistory(selectionInfo);
		}

		public bool GoBack()
		{
			if (m_selectionHistory.Count == 0 || m_historyIndex == 0)
				return false;

			Debug.Assert(m_historyIndex < m_selectionHistory.Count, "m_historyIndex < m_selectionHistory.Count");
			RestoreSelectionInternal((SelectionInfo)m_selectionHistory[--m_historyIndex]);

			tbbGoBack.Enabled = (m_historyIndex > 0);
			tbbGoForward.Enabled = true;

			return true;
		}

		public bool GoForward()
		{
			if (m_selectionHistory.Count == 0 || m_historyIndex == m_selectionHistory.Count - 1)
				return false;

			Debug.Assert(m_historyIndex < m_selectionHistory.Count, "m_historyIndex < m_selectionHistory.Count");
			RestoreSelectionInternal((SelectionInfo)m_selectionHistory[++m_historyIndex]);

			tbbGoBack.Enabled = true;
			tbbGoForward.Enabled = (m_historyIndex < m_selectionHistory.Count - 1);

			return true;
		}

		public IElementBrowserInfo[] GetCheckedElements()
		{
			if (!Settings.ShowTypeCheckBoxes)
				return null;

			ArrayList elements = new ArrayList();

			foreach (TreeNode node in tvwTypes.Nodes)
			{
				AddCheckedNodes(elements, node);
			}

			return (IElementBrowserInfo[])elements.ToArray(typeof(IElementBrowserInfo));
		}

		public void SetTypeElementChecked(IElementBrowserInfo element, bool check)
		{
			if (element == null)
				throw new ArgumentNullException("element");
			if (!Settings.ShowTypeCheckBoxes)
			{
				throw new InvalidOperationException("Unable to set the checked status for a type element"
					+ " because checkboxes are not shown (ShowTypeCheckboxes is false).");
			}

			TreeNode node = FindContentInTypeTree(element, false);
			if (node == null)
			{
				throw new ArgumentException("Unable to set the checked status for element '"
					+ element.DisplayName + "', because it could not be found in the type tree.");
			}

			node.Checked = check;
		}

		public void RefreshRepositories()
		{
			if (Manager != null)
			{
				for (int index = 0; index < m_repositories.Count; index++)
				{
					m_repositories[index] = Manager.RefreshRepository((IRepositoryBrowserInfo)m_repositories[index]);
				}

				RefreshRepositoriesInternal(true);
			}
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			if (Settings != null)
			{
				e.Effect = Manager.OnDragOver(e.AllowedEffect, e.KeyState, e.Data);
			}

			base.OnDragOver(e);
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			if (Settings != null)
			{
				IRepositoryBrowserInfo[] repositories = Manager.OnDragDrop(e.Effect, e.KeyState, e.Data);

				if (repositories != null)
				{
					using (new LongRunningMonitor(this))
					{
						foreach (IRepositoryBrowserInfo repository in repositories)
						{
							try
							{
								AddRepository(repository);
							}
							catch (System.Exception)
							{
								// Ignore exceptions in this case as the user could accidently drag in all
								// sorts of garbage. If they want to see the exceptions they need to open the
								// same files in the usual way.
							}
						}
					}
				}
			}

			base.OnDragDrop(e);
		}

		protected virtual void OnMemberActivate(MemberEventArgs e)
		{
			if (MemberActivate != null)
			{
				MemberActivate(this, e);
			}
		}

		protected virtual void OnMemberSelectionChanged(EventArgs e)
		{
			if (MemberSelectionChanged != null)
			{
				MemberSelectionChanged(this, e);
			}
		}

		protected virtual void OnTypeSelectionChanged(EventArgs e)
		{
			if (TypeSelectionChanged != null)
			{
				TypeSelectionChanged(this, e);
			}
		}

		protected virtual void OnSelectionRestored(EventArgs e)
		{
			if (SelectionRestored != null)
			{
				SelectionRestored(this, e);
			}
		}

		private void AddCheckedNodes(ArrayList elements, TreeNode node)
		{
			bool searchChildren = false;

			if (node.Checked)
			{
				if (node.Tag is ITypeBrowserInfo)
				{
					elements.Add(node.Tag);
				}
				else if (node.Tag is INamespaceBrowserInfo)
				{
					INamespaceBrowserInfo ns = (INamespaceBrowserInfo)node.Tag;
					elements.Add(ns);
					searchChildren = true;

/*					if (ns.IncludeThisInChecked)
					{
						elements.Add(ns);
					}

					if (ns.IncludeContentsInChecked)
					{
						searchChildren = true;
					}
*/
				}
				else if (node.Tag is IRepositoryBrowserInfo)
				{
					searchChildren = true;
				}
				else
				{
					Debug.Fail("Unexpected checked node tag type: " + node.Tag.GetType().FullName);
				}
			}
			else
			{
				searchChildren = true;
			}

			if (searchChildren)
			{
				if (node.Checked)
				{
					AddChildNodesForNode(node);
				}
				else if (NodeChildIsPlaceholder(node))
					return; // The node is not checked and its children haven't been expanded, so they can't be checked.

				foreach (TreeNode childNode in node.Nodes)
				{
					AddCheckedNodes(elements, childNode);
				}
			}
		}

		private void SetTabIndexes(bool membersFirst)
		{
			// Set up the tab indexes so that either the members TreeView or the types TreeView comes first
			// (depending on the membersFirst parameter). This is necessary to set the focus before the
			// control is shown - simply calling Focus() doesn't work then.

			if (membersFirst)
			{
				tvwTypes.TabIndex = 3;
				splitVertical.TabIndex = 4;
				tvwMembers.TabIndex = 0;
				splitHorizontal.TabIndex = 1;
				txtDescription.TabIndex = 2;
			}
			else
			{
				tvwTypes.TabIndex = 0;
				splitVertical.TabIndex = 1;
				tvwMembers.TabIndex = 2;
				splitHorizontal.TabIndex = 3;
				txtDescription.TabIndex = 4;
			}
		}

		private void ResetSelectionHistory()
		{
			m_selectionHistory.Clear();
			m_historyIndex = -1;
		}

		private void AddSelectionToHistory()
		{
			SelectionInfo selection = SaveSelection();
			if (selection != null)
			{
				AddSelectionToHistory(selection);
			}
		}

		private void AddSelectionToHistory(SelectionInfo selection)
		{
			Debug.Assert(selection != null, "selection != null");

			if (m_selectionHistory.Count > 0)
			{
				// If this selection is the same as the last one in the history list then don't add a
				// duplicate.

				if (object.Equals(m_selectionHistory[m_selectionHistory.Count - 1], selection))
					return;
			}

			m_selectionHistory.Add(selection);
			// Current history index is one past the end of the list, ie. we need to go back one to be
			// in the history list at all.
			m_historyIndex = m_selectionHistory.Count - 1;

			tbbGoBack.Enabled = (m_historyIndex > 0);
			tbbGoForward.Enabled = false;
		}

		private void RestoreSelectionInternal(SelectionInfo selectionInfo)
		{
			Debug.Assert(selectionInfo != null, "selectionInfo != null");

			if (!tvwTypes.Visible || !tvwMembers.Visible)
			{
				// We can't restore the selection now, because the TreeViews are not visible. Save it for later.

				m_selectWhenShown = selectionInfo;
				return;
			}

			using (new LongRunningMonitor(this))
			{
				// Don't let this selection change be hanled by the AfterSelect event, because we don't want to
				// add it to the history (unlike when a user manually selects something). Instead display the
				// type manually and ignore the event.

				m_ignoreTypeSelectionChanges = true;

				try
				{
					tvwTypes.SelectedNode = FindNodeByPath(tvwTypes.Nodes, selectionInfo.TypePath, true,
						selectionInfo.TagTypes, selectionInfo.RootNodeIndex);
					Debug.Assert(tvwTypes.SelectedNode != null, "tvwTypes.SelectedNode != null");
				}
				finally
				{
					m_ignoreTypeSelectionChanges = false;
				}

				TypeNodeSelected(tvwTypes.SelectedNode);

				if (selectionInfo.RestoreExpansion && tvwTypes.SelectedNode.FullPath == selectionInfo.TypePath &&
					tvwTypes.SelectedNode.IsExpanded != selectionInfo.TypeExpanded)
				{
					if (selectionInfo.TypeExpanded)
					{
						tvwTypes.SelectedNode.Expand();
					}
					else
					{
						tvwTypes.SelectedNode.Collapse();
					}
				}

				// Same thing as above for the members TreeView - display the member manually and ignore the
				// AfterSelect event.

				if (tvwTypes.SelectedNode.FullPath == selectionInfo.TypePath)
				{
					SetMemberSelection(selectionInfo.MemberPath);
				}
				else
				{
					SetMemberSelection(null);
				}

				if (tvwMembers.SelectedNode != null)
				{
					MemberNodeSelected(tvwMembers.SelectedNode);
					tvwMembers.Focus();
					SetTabIndexes(true);
				}
				else
				{
					tvwTypes.Focus();
					SetTabIndexes(false);
				}

				OnSelectionRestored(EventArgs.Empty);
			}
		}

		private void SetMemberSelection(string memberPath)
		{
			m_ignoreMemberSelectionChanges = true;

			try
			{
				if (memberPath == null)
				{
					tvwMembers.SelectedNode = null;
				}
				else
				{
					tvwMembers.SelectedNode = FindNodeByPath(tvwMembers.Nodes, memberPath, false, null);
				}
			}
			finally
			{
				m_ignoreMemberSelectionChanges = false;
			}
		}

		private void RefreshRepositoryDisplay()
		{
			m_ignoreTypeSelectionChanges = true;

			try
			{
				tvwTypes.Nodes.Clear();
			}
			finally
			{
				m_ignoreTypeSelectionChanges = false;
			}

			ClearMembers();

			tvwTypes.BeginUpdate();

			try
			{
				foreach (IRepositoryBrowserInfo repository in m_repositories)
				{
					AddNodeForRepository(repository);
				}

				tvwTypes.SelectedNode = (tvwTypes.Nodes.Count > 0 ? tvwTypes.Nodes[0] : null);
			}
			finally
			{
				tvwTypes.EndUpdate();
			}
		}

		private void AddNodeForRepository(IRepositoryBrowserInfo repository)
		{
			TreeNode nodeRepository = tvwTypes.Nodes.Add(repository.NodeText);

			nodeRepository.Tag = repository;
			nodeRepository.ImageIndex = repository.ImageIndex;
			nodeRepository.SelectedImageIndex = nodeRepository.ImageIndex;

			if (m_settings.ShowTypeCheckBoxes)
			{
				nodeRepository.Checked = repository.Checked;
			}

			if (repository.HasChildren)
			{
				// Add a dummy node so that the TreeView allows the user to expand this repository.
				TreeNode placeholder = new TreeNode(NodeTextRepositoryChildrenPlaceholder);
				placeholder.Tag = new PlaceholderTag(false);
				nodeRepository.Nodes.Add(placeholder);
			}
		}

		private void DisposeRepositories()
		{
			if (m_repositories != null)
			{
				foreach (object repository in m_repositories)
				{
					DisposeRepository(repository);
				}

				m_repositories = null;
			}
		}

		private void DisplayElement(IElementBrowserInfo element)
		{
			Debug.Assert(element != null, "element != null");

			DescriptionText description = element.Description;

			if (description == null)
			{
				txtDescription.Rtf = string.Empty;
			}
			else
			{
				description.SetText(txtDescription);
			}
		}

		private void DisplayType(ITypeBrowserInfo type)
		{
			DisplayElement(type);

			if (!m_settings.ShowMembers)
				return; // The whole members TreeView is hidden - nothing else to do.

			tvwMembers.BeginUpdate();

			try
			{
				ClearMembers();

				IMemberBrowserInfo[] members = type.Members;

				// If the Members property returns null this means that this kind of type cannot have members,
				// so gray out the members TreeView. If it returns an empty array that means the type could
				// have members, but doesn't.

				if (members == null)
				{
					tvwMembers.BackColor = SystemColors.InactiveBorder;
				}
				else
				{
					tvwMembers.BackColor = SystemColors.Window;

					foreach (IMemberBrowserInfo member in members)
					{
						int imageIndex = member.ImageIndex;
						TreeNode nodeMember = new TreeNode(member.NodeText, imageIndex, imageIndex);
						nodeMember.Tag = member;

						tvwMembers.Nodes.Add(nodeMember);
					}
				}
			}
			finally
			{
				tvwMembers.EndUpdate();
			}
		}

		private void ClearMembers()
		{
			m_ignoreMemberSelectionChanges = true;
			tvwMembers.Nodes.Clear();
			m_ignoreMemberSelectionChanges = false;
		}

		private TreeNode FindRepositoryNode(IRepositoryBrowserInfo repository, bool addIfNotFound)
		{
			Debug.Assert(repository != null, "repository != null");

			foreach (TreeNode nodeRepository in tvwTypes.Nodes)
			{
				if (object.Equals(nodeRepository.Tag, repository))
					return nodeRepository;
			}

			// The assembly is not current displayed - add it and try again

			if (addIfNotFound)
			{
				AddRepository(repository);
				return FindRepositoryNode(repository, false);
			}
			else
			{
				Debug.Fail("Failed to find node for repository '" + repository.DisplayName + "'.");
				return null;
			}
		}

		private TreeNode FindNamespaceNodeInRepository(TreeNode nodeRepository, INamespaceBrowserInfo ns,
			bool approximate)
		{
			Debug.Assert(nodeRepository != null, "nodeRepository != null");

			// Build the "path" of namespaces using the Parent property.

			ArrayList pathList = new ArrayList();
			for (INamespaceBrowserInfo tempNs = ns; tempNs != null; tempNs = tempNs.Parent)
			{
				pathList.Add(tempNs);
			}
			Debug.Assert(pathList.Count > 0, "pathList.Count > 0");

			// Look for the top-level namespace.

			int index = pathList.Count - 1;

			TreeNode currentNode = null;
			object currentNs = pathList[index];
			AddChildNodesForRepository(nodeRepository);

			foreach (TreeNode nodeNamespace in nodeRepository.Nodes)
			{
				if (object.Equals(nodeNamespace.Tag, currentNs))
				{
					currentNode = nodeNamespace;
					break;
				}
			}

			if (currentNode == null)
				return null; // Top-level namespace not found.

			// Look for child namespaces.

			index--;
			while (index >= 0)
			{
				currentNs = pathList[index];
				AddChildNodesForNamespace(currentNode, false);

				TreeNode foundChild = null;
				foreach (TreeNode nodeChild in currentNode.Nodes)
				{
					if (object.Equals(nodeChild.Tag, currentNs))
					{
						foundChild = nodeChild;
						break;
					}
				}

				if (foundChild == null)
					return (approximate ? currentNode : null); // Child namespace not found.

				currentNode = foundChild;
				index--;
			}

			Debug.Assert(currentNode != null, "currentNode != null");
			return currentNode; // Found.
		}

		private TreeNode FindTypeNode(TreeNode nodeContainer, ITypeBrowserInfo type, bool containerIsRepository)
		{
			Debug.Assert(nodeContainer != null, "nodeContainer != null");

			if (containerIsRepository)
			{
				AddChildNodesForRepository(nodeContainer);
			}
			else
			{
				AddChildNodesForNamespace(nodeContainer, false);
			}

			foreach (TreeNode nodeType in nodeContainer.Nodes)
			{
				if (object.Equals(nodeType.Tag, type))
					return nodeType;
			}

			return null;
		}

		private TreeNode FindContentInTypeTree(object content, bool approximate)
		{
			// If approximate is true return the closest parent node found (eg. the assembly
			// that contains the type instead of the type). If false return either the exact
			// node or null.

			if (content is ITypeBrowserInfo)
			{
				ITypeBrowserInfo type = (ITypeBrowserInfo)content;

				TreeNode nodeRepository = FindRepositoryNode(type.Repository, true);
				Debug.Assert(nodeRepository != null, "nodeRepository != null");

				INamespaceBrowserInfo ns = type.Namespace;

				if (ns == null)
				{
					// Null namespace, so the type must be directly under a repository.

					TreeNode nodeType = FindTypeNode(nodeRepository, type, true);
					return (nodeType == null && approximate ? nodeRepository : nodeType);
				}
				else
				{
					// Find the namespace, then the type under the namespace.

					TreeNode nodeNamespace = FindNamespaceNodeInRepository(nodeRepository, ns, approximate);

					if (nodeNamespace == null)
						return (approximate ? nodeRepository : null);
					else
					{
						TreeNode nodeType = FindTypeNode(nodeNamespace, type, false);
						return (nodeType == null && approximate ? nodeNamespace : nodeType);
					}
				}
			}
			else if (content is INamespaceBrowserInfo)
			{
				INamespaceBrowserInfo ns = (INamespaceBrowserInfo)content;
				TreeNode nodeRepository = FindRepositoryNode(ns.Repository, true);
				Debug.Assert(nodeRepository != null, "nodeRepository != null");

				TreeNode nodeNamespace = FindNamespaceNodeInRepository(nodeRepository, ns, approximate);
				return (nodeNamespace == null && approximate ? nodeRepository : nodeNamespace);
			}
			else if (content is IRepositoryBrowserInfo)
				return FindRepositoryNode((IRepositoryBrowserInfo)content, true);
			else
			{
				Debug.Fail("Unsupported type of type tree content: '"
					+ content.GetType().FullName + "'.");
				return null;
			}
		}

		private TreeNode FindMemberNode(IMemberBrowserInfo memberInfo)
		{
			foreach (TreeNode nodeMember in tvwMembers.Nodes)
			{
				if (object.Equals(nodeMember.Tag, memberInfo))
					return nodeMember;
			}

			return null;
		}

		private void AddNodesForNamespaces(TreeNode nodeParent, INamespaceBrowserInfo[] namespaces)
		{
			if (namespaces == null)
				return;

			foreach (INamespaceBrowserInfo ns in namespaces)
			{
				TreeNode nodeNamespace = new TreeNode(ns.NodeText, ns.ImageIndex, ns.ImageIndex);
				nodeNamespace.Tag = ns;

				if (m_settings.ShowTypeCheckBoxes)
				{
					nodeNamespace.Checked = ns.Checked;
				}

				nodeParent.Nodes.Add(nodeNamespace);

				if (nodeParent.Checked)
				{
					if (!(nodeParent.Tag is INamespaceBrowserInfo) || ((INamespaceBrowserInfo)nodeParent.Tag).AutoCheckRelatives)
					{
						nodeNamespace.Checked = true;
					}
				}

				if (ns.HasChildren)
				{
					// Add a dummy node so that the TreeView allows the user to expand this namespace.
					TreeNode placeholder = new TreeNode(NodeTextNamespaceChildrenPlaceholder);
					placeholder.Tag = new PlaceholderTag(nodeNamespace.Checked);
					nodeNamespace.Nodes.Add(placeholder);
				}
			}
		}

		private void AddNodesForTypes(TreeNode nodeParent, ITypeBrowserInfo[] types)
		{
			if (types == null)
				return;

			foreach (ITypeBrowserInfo type in types)
			{
				TreeNode nodeType = new TreeNode(type.NodeText, type.ImageIndex, type.ImageIndex);
				nodeType.Tag = type;

				if (m_settings.ShowTypeCheckBoxes)
				{
					nodeType.Checked = type.Checked;
				}

				nodeParent.Nodes.Add(nodeType);

				if (nodeParent.Checked)
				{
					nodeType.Checked = true;
				}

				if (type.HasBaseTypes)
				{
					int imageCount = tvwTypes.ImageList.Images.Count;
					TreeNode nodeBases = new TreeNode("Bases", imageCount - 1, imageCount - 1);
					nodeBases.Tag = BasesNodeTag.Value;
					nodeType.Nodes.Add(nodeBases);

					// Add a dummy node so that the TreeView allows the user to expand the Bases node.
					TreeNode placeholder = new TreeNode(NodeTextTypeChildrenPlaceholder);
					placeholder.Tag = new PlaceholderTag(false);
					nodeBases.Nodes.Add(placeholder);
				}
			}
		}

		private void AddChildNodesForRepository(TreeNode nodeRepository)
		{
			// If the repository node contains a placeholder for namespaces replace it with the real children.

			if (nodeRepository.Nodes.Count == 1 && nodeRepository.Nodes[0].Text == NodeTextRepositoryChildrenPlaceholder)
			{
				tvwTypes.BeginUpdate();

				try
				{
					IRepositoryBrowserInfo repository = (IRepositoryBrowserInfo)nodeRepository.Tag;
					nodeRepository.Nodes.Clear();

					AddNodesForNamespaces(nodeRepository, repository.Namespaces);
					AddNodesForTypes(nodeRepository, repository.Types);
				}
				finally
				{
					tvwTypes.EndUpdate();
				}
			}
		}

		private void AddChildNodesForNamespace(TreeNode nodeNamespace, bool force)
		{
			// If the namespace node contains a placeholder for types replace it with the real children.

			if (force || nodeNamespace.Nodes.Count == 1 && nodeNamespace.Nodes[0].Text == NodeTextNamespaceChildrenPlaceholder)
			{
				tvwTypes.BeginUpdate();

				try
				{
					nodeNamespace.Nodes.Clear();

					INamespaceBrowserInfo nsInfo = (INamespaceBrowserInfo)nodeNamespace.Tag;

					// Regardless of the type order setting always add namespaces before types - sort of
					// like directories appear before files in "dir" output.

					AddNodesForNamespaces(nodeNamespace, nsInfo.Namespaces);
					AddNodesForTypes(nodeNamespace, nsInfo.Types);
				}
				finally
				{
					tvwTypes.EndUpdate();
				}
			}
		}

		private void SetTypeOrderUI(TypeOrder value)
		{
			mnuTypeOrderAlphabetical.Checked = (value == TypeOrder.Alphabetical);
			mnuTypeOrderObjectType.Checked = (value == TypeOrder.ObjectType);
			mnuTypeOrderObjectAccess.Checked = (value == TypeOrder.ObjectAccess);
			tbbTypeOrder.ImageIndex = (int)Settings.TypeOrder;
		}

		private void SetMemberOrderUI(MemberOrder value)
		{
			mnuMemberOrderAlphabetical.Checked = (value == MemberOrder.Alphabetical);
			mnuMemberOrderMemberType.Checked = (value == MemberOrder.MemberType);
			mnuMemberOrderMemberAccess.Checked = (value == MemberOrder.MemberAccess);
			tbbMemberOrder.ImageIndex = (int)value;
		}

		private void SetShowNonPublicUI(bool value)
		{
			tbbShowNonPublic.Pushed = value;
			tbbShowNonPublic.ImageIndex = (value ? ImageIndexShowNonPublic : ImageIndexHideNonPublic);
			tbbShowNonPublic.ToolTipText = (value ? "Non-public types and members are shown" :
				"Non-public types and members are hidden");
		}

		private void ShowHideMembers()
		{
			splitVertical.SecondPaneVisible = m_settings.ShowMembers;
			tbbMemberOrder.Visible = m_settings.ShowMembers;
		}

		private void AddChildNodesForNode(TreeNode node)
		{
			// Child nodes for repositories and namespaces are not added automatically - add them now
			// (unless this has already been done for this node).

			if (node.Tag is IRepositoryBrowserInfo)
			{
				AddChildNodesForRepository(node);
			}
			else if (node.Tag is INamespaceBrowserInfo)
			{
				AddChildNodesForNamespace(node, false);
			}
			else if (node.Tag is BasesNodeTag)
			{
				// This is a "Bases" node. If it contains a placeholder - replace it with the real children.

				if (node.Nodes.Count > 0 && node.Nodes[0].Text == NodeTextTypeChildrenPlaceholder)
				{
					tvwTypes.BeginUpdate();

					try
					{
						node.Nodes.Clear();
						AddBaseNodesForType(node, (ITypeBrowserInfo)node.Parent.Tag);
					}
					finally
					{
						tvwTypes.EndUpdate();
					}
				}
			}
			else if (node.Tag is ITypeBrowserInfo)
			{
				// This is a type node. If it contains a placeholder that means it is itself a base type node,
				// so we don't need to add the "Bases" container node again - just add the base types directly.

				if (node.Nodes.Count > 0 && node.Nodes[0].Text == NodeTextTypeChildrenPlaceholder)
				{
					tvwTypes.BeginUpdate();

					try
					{
						node.Nodes.Clear();
						AddBaseNodesForType(node, (ITypeBrowserInfo)node.Tag);
					}
					finally
					{
						tvwTypes.EndUpdate();
					}
				}
			}
			else
			{
				Debug.Fail("Expanded TreeNode with unexpected content type: '"
					+ node.Tag.GetType() + "'.");
			}
		}

		private TreeNode FindNodeByPath(TreeNodeCollection nodes, string path, bool expand, string[] tagTypes)
		{
			string[] nodeNames = path.Split('\\');

			Debug.Assert(tagTypes == null || tagTypes.Length == nodeNames.Length,
				"tagTypes == null || tagTypes.Length == nodeNames.Length");

			TreeNode foundNode = null;
			int index = 0;

			foreach (string name in nodeNames)
			{
				string tagType = (tagTypes == null ? null : tagTypes[index++]);
				bool childFound = false;

				foreach (TreeNode childNode in nodes)
				{
					// Check the node text and the tag type (if supplied).

					if (childNode.Text == name
						&& (tagType == null || childNode.Tag == null || tagType == childNode.Tag.GetType().AssemblyQualifiedName))
					{
						foundNode = childNode;

						if (expand)
						{
							AddChildNodesForNode(foundNode); // Replace placeholders with real element nodes.
						}

						nodes = childNode.Nodes;
						childFound = true;
						break;
					}
				}

				if (!childFound)
					return foundNode;
			}

			return foundNode;
		}

		private void RemoveRepositoryFromSelectionHistory(int removedNodeIndex)
		{
			int index = 0;
			while (index < m_selectionHistory.Count)
			{
				SelectionInfo selInfo = (SelectionInfo)m_selectionHistory[index];

				if (selInfo.RootNodeIndex == removedNodeIndex)
				{
					m_selectionHistory.RemoveAt(index);
				}
				else
				{
					if (selInfo.RootNodeIndex > removedNodeIndex)
					{
						selInfo.RootNodeIndex--;
					}

					index++;
				}

				if (index <= m_historyIndex)
				{
					m_historyIndex--;
				}
			}

			tbbGoBack.Enabled = (m_historyIndex >= 0);
			tbbGoForward.Enabled = (m_historyIndex < m_selectionHistory.Count - 1);

			if (m_selectWhenShown != null)
			{
				if (m_selectWhenShown.RootNodeIndex == removedNodeIndex)
				{
					m_selectWhenShown = null;
				}
				else if (m_selectWhenShown.RootNodeIndex > removedNodeIndex)
				{
					m_selectWhenShown.RootNodeIndex--;
				}
			}
		}

		private TreeNode FindNodeByPath(TreeNodeCollection nodes, string path, bool expand,
			string[] tagTypes, int firstLevelIndex)
		{
			// This overload allows finding the top-level node by index rather than by path.

			TreeNode topNode;
			try
			{
				topNode = nodes[firstLevelIndex];
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}

			int index = path.IndexOf('\\');
			if (index == -1)
				return topNode;
			else
			{
				if (expand)
				{
					AddChildNodesForNode(topNode); // Replace the namespace placeholder with actual namespace nodes.
				}

				TreeNode childNode = FindNodeByPath(topNode.Nodes, path.Substring(index + 1), expand, tagTypes);
				return (childNode == null ? topNode : childNode);
			}
		}

		private void ProcessNodeSelected(TreeNode node)
		{
			using (new LongRunningMonitor(this))
			{
				TypeNodeSelected(node);
				AddSelectionToHistory();
			}
		}

		private void TypeNodeSelected(TreeNode node)
		{
			Debug.Assert(node != null, "node != null");

			IElementBrowserInfo previousType = m_selectedType;

			if (node.Tag is ITypeBrowserInfo)
			{
				DisplayType((ITypeBrowserInfo)node.Tag);
				m_selectedType = (IElementBrowserInfo)node.Tag;
			}
			else if (node.Tag is INamespaceBrowserInfo)
			{
				DisplayElement((INamespaceBrowserInfo)node.Tag);
				m_selectedType = (IElementBrowserInfo)node.Tag;
				ClearMembers();
				tvwMembers.BackColor = SystemColors.InactiveBorder;
			}
			else if (node.Tag is IRepositoryBrowserInfo)
			{
				DisplayElement((IRepositoryBrowserInfo)node.Tag);
				m_selectedType = (IElementBrowserInfo)node.Tag;
				ClearMembers();
				tvwMembers.BackColor = SystemColors.InactiveBorder;
			}
			else
			{
				m_selectedType = null;
				ClearMembers();
				tvwMembers.BackColor = SystemColors.InactiveBorder;
				Debug.Assert(node.Tag is BasesNodeTag, "Selected a node with a '"
					+ node.Tag.GetType().FullName + "' tag.");
			}

			if (m_selectedType != previousType)
			{
				OnTypeSelectionChanged(EventArgs.Empty);
			}

			if (m_selectedMember != null)
			{
				m_selectedMember = null;
				OnMemberSelectionChanged(EventArgs.Empty);
			}
		}

		private void MemberNodeSelected(TreeNode node)
		{
			Debug.Assert(node != null, "node != null");

			IMemberBrowserInfo previousMember = m_selectedMember;

			m_selectedMember = node.Tag as IMemberBrowserInfo;
			if (m_selectedMember != null)
			{
				DisplayElement(m_selectedMember);
			}

			if (m_selectedMember != previousMember)
			{
				OnMemberSelectionChanged(EventArgs.Empty);
			}
		}

		private void CheckAllChildren(TreeNode node, bool check)
		{
			if (!(node.Tag is ITypeBrowserInfo))
			{
				foreach (TreeNode childNode in node.Nodes)
				{
					childNode.Checked = check;

					if (childNode.Tag is PlaceholderTag)
					{
						((PlaceholderTag)childNode.Tag).AutoCheckChildren = check;
					}
					else
					{
						((IElementBrowserInfo)childNode.Tag).Checked = check;
					}

					CheckAllChildren(childNode, check);
				}
			}
		}

		private void RefreshRepositoriesInternal(bool refreshContent)
		{
			using (new LongRunningMonitor(this))
			{
				// Save selection

				SelectionInfo savedSelection = SaveSelection();
				if (savedSelection != null)
				{
					savedSelection.RestoreExpansion = true;
				}

				// Re-display everything in the types TreeView.

				if (refreshContent)
				{
					OnRefresh();
				}

				RefreshRepositoryDisplay();

				// Restore selection

				if (savedSelection != null)
				{
					RestoreSelectionInternal(savedSelection);
				}
			}
		}

		private void OnRefresh()
		{
			Manager.ClearCache();
			
			// Tell each repository about the refresh - some may not have been told by the manager.
			// Eg. in NetAssemblyManager if ClearCache is called twice the first call clears the cached
			// assemblies, so the second call does nothing (even though assemblies may have cached namespaces).

			foreach (IRepositoryBrowserInfo repository in m_repositories)
			{
				repository.OnRefresh();
			}
		}

		private void FollowLink(RichTextBox textBox, string linkText)
		{
			IDictionary links = (IDictionary)textBox.Tag;

			object linkTarget = links[linkText];
			Debug.Assert(linkTarget != null, "Link target '" + linkText + "' is missing from the links dictionary.");

			TreeNode nodeFound = FindContentInTypeTree(linkTarget, false);
			if (nodeFound == null)
			{
				Debug.Fail("Failed to navigate to link target '" + linkTarget.ToString() + "'.");
				return;
			}

			tvwTypes.SelectedNode = nodeFound;

			if (linkTarget is INamespaceBrowserInfo)
			{
				// Automatically expand namespaces - the user probably wants to see the members.
				tvwTypes.SelectedNode.Expand();
			}

			tvwTypes.Focus();
		}

		#region Event Handlers

		private void tvwTypes_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			try
			{
				using (new LongRunningMonitor(this))
				{
					AddChildNodesForNode(e.Node);
				}
			}
			catch (System.Exception ex)
			{
				// Catch the exception here and cancel expanding the node. If an exception is thrown out of
				// this method the node expands (even with Cancel = true), which makes no sense since it only
				// contains a placeholder.

				e.Cancel = true;
				Application.OnThreadException(ex);
			}
		}

		private void tvwTypes_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (m_ignoreTypeSelectionChanges)
				return;

			ProcessNodeSelected(e.Node);
		}

		private void tvwMembers_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (m_ignoreMemberSelectionChanges)
				return;

			using (new LongRunningMonitor(this))
			{
				MemberNodeSelected(e.Node);
				AddSelectionToHistory();
			}
		}

		private void tvwTypes_Enter(object sender, EventArgs e)
		{
			if (tvwTypes.SelectedNode != null && tvwTypes.SelectedNode.Tag is ITypeBrowserInfo)
			{
				DisplayElement(m_selectedType);
			}

			tvwMembers.HideSelection = true;

			if (m_selectedMember != null)
			{
				m_selectedMember = null;
				OnMemberSelectionChanged(EventArgs.Empty);
			}
		}

		private void tvwMembers_Enter(object sender, EventArgs e)
		{
			IMemberBrowserInfo previousMember = m_selectedMember;

			m_selectedMember = (tvwMembers.SelectedNode == null ? null : tvwMembers.SelectedNode.Tag as IMemberBrowserInfo);
			if (m_selectedMember != null)
			{
				DisplayElement(m_selectedMember);
			}

			tvwMembers.HideSelection = false;

			if (m_selectedMember != previousMember)
			{
				OnMemberSelectionChanged(EventArgs.Empty);
			}
		}

		private void mnuTypeOrderAlphabetical_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.TypeOrder = TypeOrder.Alphabetical;
			}
		}

		private void mnuTypeOrderObjectType_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.TypeOrder = TypeOrder.ObjectType;
			}
		}

		private void mnuTypeOrderObjectAccess_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.TypeOrder = TypeOrder.ObjectAccess;
			}
		}

		private void mnuMemberOrderAlphabetical_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.MemberOrder = MemberOrder.Alphabetical;
			}
		}

		private void mnuMemberOrderMemberType_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.MemberOrder = MemberOrder.MemberType;
			}
		}

		private void mnuMemberOrderMemberAccess_Click(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				Settings.MemberOrder = MemberOrder.MemberAccess;
			}
		}

		private void tbToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == tbbShowNonPublic)
			{
				using (new LongRunningMonitor(this))
				{
					Settings.ShowNonPublic = tbbShowNonPublic.Pushed;
				}
			}
			else if (e.Button == tbbGoBack)
			{
				GoBack();
			}
			else if (e.Button == tbbGoForward)
			{
				GoForward();
			}
			else
			{
				Debug.Assert(e.Button == tbbTypeOrder || e.Button == tbbMemberOrder,
					"ButtonClick not handled for button '" + e.Button.Text + "'.");
			}
		}

		private void txtDescription_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				FollowLink((RichTextBox)sender, e.LinkText);
			}
		}

		private void tvwMembers_NodeActivate(object sender, TreeViewEventArgs e)
		{
			Debug.Assert(e.Node.Tag is IMemberBrowserInfo, "e.Node.Tag is MemberInfo");
			OnMemberActivate(new MemberEventArgs((IMemberBrowserInfo)e.Node.Tag));
		}

		private void m_settings_Refresh(object sender, EventArgs e)
		{
			RefreshRepositoriesInternal(true);
		}

		private void m_settings_ShowMembersChanged(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				if (Manager != null)
				{
					Manager.OnShowMembersChanged();
				}

				ShowHideMembers();
			}
		}

		private void m_settings_TypeOrderChanged(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				try
				{
					if (Manager != null)
					{
						Manager.OnTypeOrderChanged();
					}

					RefreshRepositoriesInternal(false);
				}
				finally
				{
					SetTypeOrderUI(Settings.TypeOrder);
				}
			}
		}

		private void m_settings_MemberOrderChanged(object sender, EventArgs e)
		{
			using (new LongRunningMonitor(this))
			{
				try
				{
					if (Manager != null)
					{
						Manager.OnMemberOrderChanged();
					}

					if (tvwTypes.SelectedNode != null && tvwTypes.SelectedNode.Tag is ITypeBrowserInfo)
					{
						string memberSelection = (tvwMembers.SelectedNode == null ? null :
							tvwMembers.SelectedNode.FullPath);
						DisplayType((ITypeBrowserInfo)tvwTypes.SelectedNode.Tag);
						SetMemberSelection(memberSelection);
					}
					else
					{
						ClearMembers();
					}
				}
				finally
				{
					SetMemberOrderUI(Settings.MemberOrder);
				}
			}
		}

		private void m_settings_ShowNonPublicChanged(object sender, EventArgs e)
		{
			Debug.Assert(Settings.ObjectAccessSupported, "ShowNonPublicChanged event fired when"
				+ " Settings.ObjectAccessSupported is false.");

			try
			{
				if (Manager != null)
				{
					Manager.OnShowNonPublicChanged();
				}

				RefreshRepositoriesInternal(false);
			}
			finally
			{
				SetShowNonPublicUI(Settings.ShowNonPublic);
			}
		}

		private void treeView_VisibleChanged(object sender, EventArgs e)
		{
			if (m_selectWhenShown != null && tvwTypes.Visible && tvwMembers.Visible)
			{
				RestoreSelectionInternal(m_selectWhenShown);
				m_selectWhenShown = null;

				tvwTypes.VisibleChanged -= new EventHandler(treeView_VisibleChanged);
				tvwMembers.VisibleChanged -= new EventHandler(treeView_VisibleChanged);
			}
		}

		private void tvwTypes_BeforeCheck(object sender, TreeViewCancelEventArgs e)
		{
			// Don't allow base type nodes or the "Bases" container node to be checked.

			TreeNode parent = e.Node.Parent;
			if (parent != null && (parent.Tag is ITypeBrowserInfo || parent.Tag is BasesNodeTag))
			{
				e.Cancel = true;
			}
		}

		private void tvwTypes_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (m_ignoreTypeAfterCheck)
				return;

			// Set the "checked" flag in the element info, which will preseve the checked state even if the
			// nodes is re-created by a refresh.

			((IElementBrowserInfo)e.Node.Tag).Checked = e.Node.Checked;

			if (e.Node.Tag is INamespaceBrowserInfo && !((INamespaceBrowserInfo)e.Node.Tag).AutoCheckRelatives)
				return;

			Debug.Assert(m_settings.ShowTypeCheckBoxes, "AfterCheck event fired when ShowTypeCheckBoxes is false.");

			// Check/uncheck all the children of this node.

			m_ignoreTypeAfterCheck = true;

			try
			{
				// Set the Checked property on the child nodes as well, unless this is a type. The child nodes
				// of a type are its base types and we don't want to check those.

				CheckAllChildren(e.Node, e.Node.Checked);

				// Whenever a node is checked all its nodes should also be checked (except base types).
				// Update the checked state of the checked node's parent.

				TreeNode parent = e.Node.Parent;

				if (e.Node.Checked)
				{
					while (parent != null)
					{
						bool allChildrenChecked = true;

						foreach (TreeNode child in parent.Nodes)
						{
							if (!child.Checked)
							{
								allChildrenChecked = false;
								break;
							}
						}

						if (allChildrenChecked)
						{
							parent.Checked = true;
							((IElementBrowserInfo)parent.Tag).Checked = true;
							parent = parent.Parent;
						}
						else
						{
							parent = null;
						}
					}
				}
				else
				{
					while (parent != null)
					{
						parent.Checked = false;
						((IElementBrowserInfo)parent.Tag).Checked = false;
						parent = parent.Parent;
					}
				}
			}
			finally
			{
				m_ignoreTypeAfterCheck = false;
			}
		}

		#endregion
	}
}

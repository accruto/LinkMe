using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class FileManager : FileManagerControl, IPostBackEventHandler
    {
        #region Fields

        private FileView _fileView;
        private FolderTree _folderTree;
        private Menu _toolBar;
        private BorderedPanelStyle _addressBarStyle;
        private BorderedPanelStyle _addressTextBoxStyle;
        private BorderedPanelStyle _toolbarStyle;
        private BorderedPanelStyle _toolbarButtonStyle;
        private BorderedPanelStyle _toolbarButtonHoverStyle;
        private BorderedPanelStyle _toolbarButtonPressedStyle;
        private IList<CustomToolbarButton> _customToolbarButtons;

        private BorderedPanelStyle _defaultToolbarStyle;
        private BorderedPanelStyle _defaultToolbarButtonStyle;
        private BorderedPanelStyle _defaultToolbarButtonHoverStyle;
        private BorderedPanelStyle _defaultToolbarButtonPressedStyle;

        private bool showToolBar = true;
        private bool showAddressBar = true;
        private bool showUploadBar = true;
        private bool showSelectBar = false;

        #endregion

        #region Events

        [Category("Action")]
        public event CommandEventHandler ToolbarCommand;
        public event EventHandler SelectCommand;
        public event EventHandler CancelCommand;

        #endregion

        #region Properties

        public bool ShowToolBar
        {
            get { return showToolBar; }
            set { showToolBar = value; }
        }

        public bool ShowAddressBar
        {
            get { return showAddressBar; }
            set { showAddressBar = value; }
        }

        public bool ShowUploadBar
        {
            get { return showUploadBar; }
            set { showUploadBar = value; }
        }

        public bool ShowSelectBar
        {
            get { return showSelectBar; }
            set { showSelectBar = value; }
        }

        public CommandOptions CommandOptions
        {
            get { return Controller.CommandOptions; }
        }

        [MergableProperty(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Behavior")]
        [Localizable(false)]
        [Themeable(false)]
        public IList<CustomToolbarButton> CustomToolbarButtons
        {
            get
            {
                if (_customToolbarButtons == null)
                    _customToolbarButtons = new List<CustomToolbarButton>();
                return _customToolbarButtons;
            }
        }

        FileView FileView
        {
            get
            {
                EnsureChildControls();
                return _fileView;
            }
        }

        FolderTree FolderTree
        {
            get
            {
                EnsureChildControls();
                return _folderTree;
            }
        }

        public override FileManagerItemInfo[] SelectedItems
        {
            get
            {
                return FileView.SelectedItems;
            }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string SplitterImageUrl
        {
            get { return ViewState["SplitterImageUrl"] == null ? String.Empty : (string)ViewState["SplitterImageUrl"]; }
            set { ViewState["SplitterImageUrl"] = value; }
        }

        // TODO
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        internal BorderedPanelStyle AddressBarStyle
        {
            get
            {
                if (_addressBarStyle == null)
                {
                    _addressBarStyle = new BorderedPanelStyle();
                    _addressBarStyle.PaddingLeft = Unit.Pixel(3);
                    _addressBarStyle.PaddingTop = Unit.Pixel(2);
                    _addressBarStyle.PaddingBottom = Unit.Pixel(2);
                    _addressBarStyle.PaddingRight = Unit.Pixel(2);
                    if (IsTrackingViewState)
                        ((IStateManager)_addressBarStyle).TrackViewState();
                }
                return _addressBarStyle;
            }
        }

        // TODO
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        internal BorderedPanelStyle AddressTextBoxStyle
        {
            get
            {
                if (_addressTextBoxStyle == null)
                {
                    _addressTextBoxStyle = new BorderedPanelStyle();
                    _addressTextBoxStyle.Font.Names = new string[] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
                    _addressTextBoxStyle.Font.Size = FontUnit.Parse("11px", null);
                    _addressTextBoxStyle.Width = Unit.Percentage(98);
                    _addressTextBoxStyle.BorderStyle = BorderStyle.Solid;
                    _addressTextBoxStyle.BorderWidth = Unit.Pixel(1);
                    _addressTextBoxStyle.BorderColor = Color.FromArgb(0xACA899);
                    _addressTextBoxStyle.PaddingLeft = Unit.Pixel(2);
                    _addressTextBoxStyle.PaddingTop = Unit.Pixel(2);
                    _addressTextBoxStyle.PaddingBottom = Unit.Pixel(2);
                    if (IsTrackingViewState)
                        ((IStateManager)_addressTextBoxStyle).TrackViewState();
                }
                return _addressTextBoxStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public BorderedPanelStyle ToolbarStyle
        {
            get
            {
                if (_toolbarStyle == null)
                {
                    _toolbarStyle = new BorderedPanelStyle();
                    if (IsTrackingViewState)
                        ((IStateManager)_toolbarStyle).TrackViewState();
                }
                return _toolbarStyle;
            }
        }

        bool ToolbarStyleCreated
        {
            get { return _toolbarStyle != null; }
        }

        BorderedPanelStyle DefaultToolbarStyle
        {
            get
            {
                if (_defaultToolbarStyle == null)
                {
                    _defaultToolbarStyle = new BorderedPanelStyle();
                    _defaultToolbarStyle.ForeColor = Color.Black;
                    _defaultToolbarStyle.Font.Names = new string[] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
                    _defaultToolbarStyle.Font.Size = FontUnit.Parse("11px", null);
                    _defaultToolbarStyle.Height = Unit.Pixel(24);
                    _defaultToolbarStyle.BackImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbg.gif").ToString();
                    _defaultToolbarStyle.BackColor = Color.FromArgb(0xEBEADB);
                    _defaultToolbarStyle.PaddingLeft = Unit.Pixel(3);
                    _defaultToolbarStyle.PaddingRight = Unit.Pixel(3);
                    _defaultToolbarStyle.PaddingTop = Unit.Pixel(2);
                }
                return _defaultToolbarStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public BorderedPanelStyle ToolbarButtonStyle
        {
            get
            {
                if (_toolbarButtonStyle == null)
                {
                    _toolbarButtonStyle = new BorderedPanelStyle();
                    if (IsTrackingViewState)
                        ((IStateManager)_toolbarButtonStyle).TrackViewState();
                }
                return _toolbarButtonStyle;
            }
        }

        bool ToolbarButtonStyleCreated
        {
            get { return _toolbarButtonStyle != null; }
        }

        BorderedPanelStyle DefaultToolbarButtonStyle
        {
            get
            {
                if (_defaultToolbarButtonStyle == null)
                {
                    _defaultToolbarButtonStyle = new BorderedPanelStyle();
                    _defaultToolbarButtonStyle.OuterBorderStyle = OuterBorderStyle.AllSides;
                    _defaultToolbarButtonStyle.OuterBorderTopWidth = Unit.Pixel(3);
                    _defaultToolbarButtonStyle.OuterBorderLeftWidth = Unit.Pixel(3);
                    _defaultToolbarButtonStyle.OuterBorderRightWidth = Unit.Pixel(3);
                    _defaultToolbarButtonStyle.OuterBorderBottomWidth = Unit.Pixel(3);
                    _defaultToolbarButtonStyle.ForeColor = Color.Black;
                }
                return _defaultToolbarButtonStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public BorderedPanelStyle ToolbarButtonHoverStyle
        {
            get
            {
                if (_toolbarButtonHoverStyle == null)
                {
                    _toolbarButtonHoverStyle = new BorderedPanelStyle();
                    if (IsTrackingViewState)
                        ((IStateManager)_toolbarButtonHoverStyle).TrackViewState();
                }
                return _toolbarButtonHoverStyle;
            }
        }

        bool ToolbarButtonHoverStyleCreated
        {
            get { return _toolbarButtonHoverStyle != null; }
        }

        BorderedPanelStyle DefaultToolbarButtonHoverStyle
        {
            get
            {
                if (_defaultToolbarButtonHoverStyle == null)
                {
                    _defaultToolbarButtonHoverStyle = new BorderedPanelStyle();
                    _defaultToolbarButtonHoverStyle.ForeColor = Color.Black;
                    _defaultToolbarButtonHoverStyle.BackColor = Color.FromArgb(0xf5f5ef);
                    _defaultToolbarButtonHoverStyle.OuterBorderLeftBottomCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_LB.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderRightBottomCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_RB.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderLeftTopCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_LT.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderRightTopCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_RT.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderTopImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_T.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderBottomImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_B.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderLeftImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_L.gif").ToString();
                    _defaultToolbarButtonHoverStyle.OuterBorderRightImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtnhover_R.gif").ToString();
                }
                return _defaultToolbarButtonHoverStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public BorderedPanelStyle ToolbarButtonPressedStyle
        {
            get
            {
                if (_toolbarButtonPressedStyle == null)
                {
                    _toolbarButtonPressedStyle = new BorderedPanelStyle();
                    if (IsTrackingViewState)
                        ((IStateManager)_toolbarButtonPressedStyle).TrackViewState();
                }
                return _toolbarButtonPressedStyle;
            }
        }

        bool ToolbarButtonPressedStyleCreated
        {
            get { return _toolbarButtonPressedStyle != null; }
        }

        BorderedPanelStyle DefaultToolbarButtonPressedStyle
        {
            get
            {
                if (_defaultToolbarButtonPressedStyle == null)
                {
                    _defaultToolbarButtonPressedStyle = new BorderedPanelStyle();
                    _defaultToolbarButtonPressedStyle.ForeColor = Color.Black;
                    _defaultToolbarButtonPressedStyle.BackColor = Color.FromArgb(0xe3e3db);
                    _defaultToolbarButtonPressedStyle.OuterBorderLeftBottomCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_LB.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderRightBottomCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_RB.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderLeftTopCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_LT.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderRightTopCornerImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_RT.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderTopImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_T.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderBottomImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_B.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderLeftImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_L.gif").ToString();
                    _defaultToolbarButtonPressedStyle.OuterBorderRightImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/toolbarbtndown_R.gif").ToString();
                }
                return _defaultToolbarButtonPressedStyle;
            }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public bool UseLinkToOpenItem
        {
            get { return FileView.UseLinkToOpenItem; }
            set { FileView.UseLinkToOpenItem = value; }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("_blank")]
        [Localizable(false)]
        public string LinkToOpenItemTarget
        {
            get { return FileView.LinkToOpenItemTarget; }
            set { FileView.LinkToOpenItemTarget = value; }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(FileViewRenderMode.Icons)]
        [Localizable(false)]
        public FileViewRenderMode FileViewMode
        {
            get { return FileView.View; }
            set { FileView.View = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style FileViewStyle
        {
            get { return FileView.ControlStyle; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style FolderTreeStyle
        {
            get { return FolderTree.ControlStyle; }
        }

        [Bindable(true)]
        [Category("Data")]
        [DefaultValue("[0]/")]
        [Localizable(false)]
        [Themeable(false)]
        public string Directory
        {
            get { return FileView.Directory; }
            set { FileView.Directory = value; }
        }

        public override FileManagerItemInfo CurrentDirectory
        {
            get
            {
                return FileView.CurrentDirectory;
            }
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (ShowToolBar)
                CreateToolbar();
            RegisterSplitterClientScript();
            RegisterLayoutSetupScript();

            Page.Form.Enctype = "multipart/form-data";

            Page.ClientScript.RegisterExpandoAttribute(_fileView.ClientID, "rootControl", ClientID);
        }

        private void RegisterLayoutSetupScript()
        {
            StringBuilder sb = new StringBuilder();
            if (!ShowAddressBar)
                sb.AppendLine("var addressBarHeight = 0;");
            else
                sb.AppendLine("var addressBarHeight = WebForm_GetElementPosition(WebForm_GetElementById('" + ClientID + "_AddressBar')).height;");
            if (!ShowToolBar)
                sb.AppendLine("var toolBarHeight = 0;");
            else
                sb.AppendLine("var toolBarHeight = WebForm_GetElementPosition(WebForm_GetElementById('" + ClientID + "_ToolBar')).height;");
            if (ReadOnly || !AllowUpload || !ShowUploadBar)
                sb.AppendLine("var uploadBarHeight = 0;");
            else
                sb.AppendLine("var uploadBarHeight = WebForm_GetElementPosition(WebForm_GetElementById('" + ClientID + "_UploadBar')).height;");
            sb.AppendLine("var fileManagerHeight = WebForm_GetElementPosition(WebForm_GetElementById('" + ClientID + "')).height;");
            sb.AppendLine("var requestedHeight = fileManagerHeight - addressBarHeight - toolBarHeight - uploadBarHeight;");
            sb.AppendLine("WebForm_SetElementHeight(WebForm_GetElementById('" + _fileView.ClientID + "'), requestedHeight);");
            sb.AppendLine("WebForm_SetElementHeight(WebForm_GetElementById('" + _folderTree.ClientID + "'), requestedHeight);");
            Page.ClientScript.RegisterStartupScript(typeof(FileManager), ClientID + "LayoutSetup", sb.ToString(), true);

        }

        private void RegisterSplitterClientScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var __fileView;");
            sb.AppendLine("var __fileViewWidth;");
            sb.AppendLine("var __folderTree;");
            sb.AppendLine("var __folderTreeWidth;");
            sb.AppendLine("var __clientX;");
            sb.AppendLine("var __document_onmousemove;");
            sb.AppendLine("var __document_onmouseup;");
            sb.AppendLine("function " + ClientID + "SplitterDragStart(e) {");
            sb.AppendLine("if(e == null) var e = event;");
            sb.AppendLine("__fileView = WebForm_GetElementById('" + _fileView.ClientID + "');");
            sb.AppendLine("__fileViewWidth = WebForm_GetElementPosition(__fileView).width;");
            sb.AppendLine("__folderTree = WebForm_GetElementById('" + _folderTree.ClientID + "');");
            sb.AppendLine("__folderTreeWidth = WebForm_GetElementPosition(__folderTree).width;");
            sb.AppendLine("__clientX = e.clientX;");
            sb.AppendLine("__document_onmousemove = document.onmousemove;");
            sb.AppendLine("__document_onmouseup = document.onmouseup;");
            sb.AppendLine("document.onmousemove = " + ClientID + "SplitterDrag;");
            sb.AppendLine("document.onmouseup = " + ClientID + "SplitterDragEnd;");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("function " + ClientID + "SplitterDragEnd(e) {");
            sb.AppendLine("document.onmousemove = __document_onmousemove;");
            sb.AppendLine("document.onmouseup = __document_onmouseup;");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("function " + ClientID + "SplitterDrag(e) {");
            sb.AppendLine("if(e == null) var e = event;");
            if (Controller.IsRightToLeft)
                sb.AppendLine("var __delta = __clientX - e.clientX;");
            else
                sb.AppendLine("var __delta = e.clientX - __clientX;");
            sb.AppendLine("WebForm_SetElementWidth(__fileView, __fileViewWidth - __delta);");
            sb.AppendLine("WebForm_SetElementWidth(__folderTree, __folderTreeWidth + __delta);");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            Page.ClientScript.RegisterClientScriptBlock(typeof(FileManager), ClientID + "SplitterDrag", sb.ToString(), true);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            CreateFileView();
            CreateFolderTree();
        }

        private void CreateFolderTree()
        {
            _folderTree = new FolderTree(Controller, _fileView);
            _folderTree.ID = "FolderTree";
            Controls.Add(_folderTree);
        }

        private void CreateFileView()
        {
            _fileView = new FileView(Controller);
            _fileView.ID = "FileView";
            Controls.Add(_fileView);
        }

        private void CreateToolbar() {
			_toolBar = new Menu ();
			_toolBar.EnableViewState = false;
			_toolBar.StaticEnableDefaultPopOutImage = false;
			_toolBar.DynamicEnableDefaultPopOutImage = false;
			_toolBar.Orientation = Orientation.Horizontal;
			_toolBar.SkipLinkText = String.Empty;
			_toolBar.StaticItemTemplate = new CompiledTemplateBuilder (new BuildTemplateMethod (CreateToolbarButton));
			_toolBar.DynamicItemTemplate = new CompiledTemplateBuilder (new BuildTemplateMethod (CreateToolbarPopupItem));

			// TODO
			_toolBar.DynamicMenuStyle.BackColor = Color.White;
			_toolBar.DynamicMenuStyle.BorderStyle = BorderStyle.Solid;
			_toolBar.DynamicMenuStyle.BorderWidth = Unit.Pixel (1);
			_toolBar.DynamicMenuStyle.BorderColor = Color.FromArgb (0xACA899);
			_toolBar.DynamicMenuStyle.HorizontalPadding = Unit.Pixel (2);
			_toolBar.DynamicMenuStyle.VerticalPadding = Unit.Pixel (2);

			_toolBar.DynamicMenuItemStyle.ForeColor = Color.Black;
			_toolBar.DynamicMenuItemStyle.Font.Names = new string [] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
			_toolBar.DynamicMenuItemStyle.VerticalPadding = Unit.Pixel (1);
			_toolBar.DynamicMenuItemStyle.Font.Size = FontUnit.Parse ("11px", null);

			_toolBar.DynamicHoverStyle.ForeColor = Color.White;
			_toolBar.DynamicHoverStyle.BackColor = Color.FromArgb (0x316AC5);

			Controls.Add (_toolBar);

			string clientClickFunction = "javascript:" + FileManagerController.ClientScriptObjectNamePrefix + Controller.ClientID + ".On{0}(" + FileManagerController.ClientScriptObjectNamePrefix + _fileView.ClientID + ", '{1}');return false;";

			// Copy to
			if (CommandOptions.Copy)
            {
				MenuItem itemCopyTo = new MenuItem ();
				itemCopyTo.Text = Controller.GetResourceString ("Copy", "Copy");
				itemCopyTo.ImageUrl = Controller.GetToolbarImage (ToolbarImages.Copy);
				itemCopyTo.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.SelectedItemsCopyTo, "");
				itemCopyTo.Enabled = !ReadOnly;
				_toolBar.Items.Add (itemCopyTo);
			}

			// Move to
            if (CommandOptions.Move)
            {
				MenuItem itemMoveTo = new MenuItem ();
				itemMoveTo.Text = Controller.GetResourceString ("Move", "Move");
				itemMoveTo.ImageUrl = Controller.GetToolbarImage (ToolbarImages.Move);
				itemMoveTo.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.SelectedItemsMoveTo, "");
				itemMoveTo.Enabled = !ReadOnly && AllowDelete;
				_toolBar.Items.Add (itemMoveTo);
			}

			// Delete
            if (CommandOptions.Delete)
            {
				MenuItem itemDelete = new MenuItem ();
				itemDelete.Text = Controller.GetResourceString ("Delete", "Delete");
				itemDelete.ImageUrl = Controller.GetToolbarImage (ToolbarImages.Delete);
				itemDelete.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.SelectedItemsDelete, "");
				itemDelete.Enabled = !ReadOnly && AllowDelete;
				_toolBar.Items.Add (itemDelete);
			}

			// Rename
            if (CommandOptions.Rename)
            {
				MenuItem itemRename = new MenuItem ();
				itemRename.Text = Controller.GetResourceString ("Rename", "Rename");
				itemRename.ImageUrl = Controller.GetToolbarImage (ToolbarImages.Rename);
				itemRename.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.Rename, "");
				itemRename.Enabled = !ReadOnly && AllowDelete;
				_toolBar.Items.Add (itemRename);
			}

			// NewFolder
            if (CommandOptions.NewFolder)
            {
				MenuItem itemNewFolder = new MenuItem ();
				itemNewFolder.Text = Controller.GetResourceString ("Create_New_Folder", "New Folder");
				itemNewFolder.ImageUrl = Controller.GetToolbarImage (ToolbarImages.NewFolder);
				itemNewFolder.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.NewFolder, "");
				itemNewFolder.Enabled = !ReadOnly;
				_toolBar.Items.Add (itemNewFolder);
			}

			// FolderUp
            if (CommandOptions.FolderUp)
            {
				MenuItem itemFolderUp = new MenuItem ();
				itemFolderUp.Text = Controller.GetResourceString ("Up", "Up");
				itemFolderUp.ImageUrl = Controller.GetToolbarImage (ToolbarImages.FolderUp);
				itemFolderUp.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.FileViewNavigate, "..");
				_toolBar.Items.Add (itemFolderUp);
			}

			// View
            if (CommandOptions.View)
            {
				MenuItem itemView = new MenuItem ();
				itemView.Text = Controller.GetResourceString ("View", "View");
				itemView.ImageUrl = Controller.GetToolbarImage (ToolbarImages.View);
				itemView.NavigateUrl = "javascript: return;";
				_toolBar.Items.Add (itemView);

				// Icons
				MenuItem itemViewIcons = new MenuItem ();
				itemViewIcons.Text = Controller.GetResourceString ("Icons", "Icons");
				itemViewIcons.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Icons);
				itemView.ChildItems.Add (itemViewIcons);

				// Details
				MenuItem itemViewDetails = new MenuItem ();
				itemViewDetails.Text = Controller.GetResourceString ("Details", "Details");
				itemViewDetails.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Details);
				itemView.ChildItems.Add (itemViewDetails);

				if (Controller.SupportThumbnails) {
					// Thumbnails
					MenuItem itemViewThumbnails = new MenuItem ();
					itemViewThumbnails.Text = Controller.GetResourceString ("Thumbnails", "Thumbnails");
					itemViewThumbnails.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Thumbnails);
					itemView.ChildItems.Add (itemViewThumbnails);
				}
			}

			// Refresh
            if (CommandOptions.Refresh)
            {
				MenuItem itemRefresh = new MenuItem ();
				itemRefresh.Text = Controller.GetResourceString ("Refresh", "Refresh");
				itemRefresh.Value = "Refresh";
				itemRefresh.ImageUrl = Controller.GetToolbarImage (ToolbarImages.Refresh);
				itemRefresh.NavigateUrl = String.Format (clientClickFunction, FileManagerCommands.Refresh, "");
				_toolBar.Items.Add (itemRefresh);
			}

			// Custom Buttons
			if (_customToolbarButtons != null) {
                for (int i = 0; i < _customToolbarButtons.Count; i++)
                {
                    CustomToolbarButton button = _customToolbarButtons[i];
					string postBackStatement = null;
					if (button.PerformPostBack) {
						postBackStatement = Page.ClientScript.GetPostBackEventReference (this, "Toolbar:" + i);
					}
					MenuItem item = new MenuItem ();
                    item.Text = button.Text;
                    item.ImageUrl = button.ImageUrl.ToString();
                    item.NavigateUrl = "javascript:" + button.OnClientClick + ";" + postBackStatement + ";return false;";
					_toolBar.Items.Add (item);
				}
			}
		}

        void CreateToolbarPopupItem(Control control)
        {
            MenuItemTemplateContainer container = (MenuItemTemplateContainer)control;
            MenuItem menuItem = (MenuItem)container.DataItem;
            Table t = new Table();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            t.BorderWidth = 0;
            t.Style[HtmlTextWriterStyle.Cursor] = "default";
            t.Attributes["onclick"] = menuItem.NavigateUrl;
            container.Controls.Add(t);
            TableRow r = new TableRow();
            t.Rows.Add(r);
            TableCell c1 = new TableCell();
            System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
            img1.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString();
            img1.Width = 16;
            img1.Height = 16;
            c1.Controls.Add(img1);
            r.Cells.Add(c1);
            TableCell c2 = new TableCell();
            c2.Style[HtmlTextWriterStyle.PaddingLeft] = "2px";
            c2.Style[HtmlTextWriterStyle.PaddingRight] = "2px";
            c2.Text = "&nbsp;" + menuItem.Text;
            c2.Width = Unit.Percentage(100);
            r.Cells.Add(c2);
            TableCell c3 = new TableCell();
            System.Web.UI.WebControls.Image img3 = new System.Web.UI.WebControls.Image();
            img3.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString();
            img3.Width = 16;
            img3.Height = 16;
            c3.Controls.Add(img3);
            r.Cells.Add(c3);
        }

        void CreateToolbarButton(Control control)
        {
            MenuItemTemplateContainer container = (MenuItemTemplateContainer)control;
            MenuItem menuItem = (MenuItem)container.DataItem;
            BorderedPanel panel = new BorderedPanel();
            panel.ControlStyle.CopyFrom(DefaultToolbarButtonStyle);
            if (ToolbarButtonStyleCreated)
                panel.ControlStyle.CopyFrom(ToolbarButtonStyle);
            panel.Style[HtmlTextWriterStyle.Cursor] = "default";
            if (menuItem.Enabled)
            {
                panel.HoverSyle.CopyFrom(DefaultToolbarButtonHoverStyle);
                if (ToolbarButtonHoverStyleCreated)
                    panel.HoverSyle.CopyFrom(ToolbarButtonHoverStyle);
                panel.PressedSyle.CopyFrom(DefaultToolbarButtonPressedStyle);
                if (ToolbarButtonPressedStyleCreated)
                    panel.PressedSyle.CopyFrom(ToolbarButtonPressedStyle);
                panel.Attributes["onclick"] = menuItem.NavigateUrl;
            }
            else
                panel.Style["color"] = "gray";
            container.Controls.Add(panel);
            Table t = new Table();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            t.BorderWidth = 0;
            panel.Controls.Add(t);
            TableRow r = new TableRow();
            t.Rows.Add(r);
            TableCell c1 = new TableCell();
            r.Cells.Add(c1);
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
            img.ImageUrl = ((MenuItem)container.DataItem).ImageUrl;
            c1.Controls.Add(img);
            TableCell c2 = new TableCell();
            c2.Style[HtmlTextWriterStyle.PaddingLeft] = "2px";
            c2.Style[HtmlTextWriterStyle.PaddingRight] = "2px";
            c2.Text = "&nbsp;" + menuItem.Text;
            r.Cells.Add(c2);
        }

        protected override Style CreateControlStyle()
        {
            Style style = base.CreateControlStyle();
            style.BackColor = Color.FromArgb(0xEDEBE0);
            return style;
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.Write(String.Format(System.Globalization.CultureInfo.InvariantCulture,
                "<div><table width=\"{0}\" height=\"{1}\" bgcolor=\"#f5f5f5\" bordercolor=\"#c7c7c7\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td valign=\"middle\" align=\"center\">IZWebFileManager - <b>{2}</b></td></tr></table></div>",
                    Width,
                    Height,
                    ID));
                return;
            }

            AddAttributesToRender(writer);
            RenderBeginOuterTable(writer);
            if (ShowAddressBar)
                RenderAddressBar(writer);
            if (ShowToolBar)
                RenderToolBar(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            RenderFolderTree(writer);

            writer.RenderEndTag();

            // splitter
            writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "col-resize");
            writer.AddAttribute("onmousedown", ClientID + "SplitterDragStart(event)");
            writer.AddAttribute("onselectstart", "return false");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            if (SplitterImageUrl.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(SplitterImageUrl));
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "3px");
                writer.AddAttribute(HtmlTextWriterAttribute.Height, "3px");
                writer.AddAttribute(HtmlTextWriterAttribute.Src, new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString());
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            RenderFileView(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            if (!ReadOnly && ShowSelectBar)
                RenderSelectBar(writer);
            if (!ReadOnly && AllowUpload && ShowUploadBar)
                RenderFileUploadBar(writer);
            RenderEndOuterTable(writer);
        }

        private void RenderToolBar(HtmlTextWriter writer)
        {
            BorderedPanel p = new BorderedPanel();
            p.Page = Page;
            p.ControlStyle.CopyFrom(DefaultToolbarStyle);
            if (ToolbarStyleCreated)
                p.ControlStyle.CopyFrom(ToolbarStyle);

            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "100");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_ToolBar");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            p.RenderBeginTag(writer);
            _toolBar.RenderControl(writer);
            p.RenderEndTag(writer);
            writer.RenderEndTag();
        }

        private void RenderFolderTree(HtmlTextWriter writer)
        {
            _folderTree.Width = new Unit(Width.Value * 0.25, Width.Type);
            _folderTree.Height = 100;
            _folderTree.RenderControl(writer);
        }

        private void RenderFileView(HtmlTextWriter writer)
        {
            _fileView.Width = new Unit(Width.Value * 0.75, Width.Type);
            _fileView.Height = 100;
            _fileView.RenderControl(writer);
        }

        private void RenderAddressBar(HtmlTextWriter writer)
        {
            AddressBarStyle.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_AddressBar");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);


            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, _fileView.ClientID + "_Address");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, CurrentDirectory.FileManagerPath, true);
            AddressTextBoxStyle.AddAttributesToRender(writer);
            //writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();


            writer.RenderEndTag();
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "16px");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            writer.AddAttribute(HtmlTextWriterAttribute.Src, Controller.GetToolbarImage(ToolbarImages.Process));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, _fileView.ClientID + "_ProgressImg");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        private void RenderFileUploadBar(HtmlTextWriter writer)
        {
            AddressBarStyle.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_UploadBar");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(HttpUtility.HtmlEncode(Controller.GetResourceString("Upload_File", "Upload File")));
            writer.RenderEndTag();
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_Upload");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, ClientID + "_Upload");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this, "Upload"));
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Controller.GetResourceString("Submit", "Submit"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RenderSelectBar(HtmlTextWriter writer)
        {
            AddressBarStyle.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_SelectBar");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this, "Select"));
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Controller.GetResourceString("Select", "Select"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this, "Cancel"));
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Controller.GetResourceString("Cancel", "Cancel"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RenderBeginOuterTable(HtmlTextWriter writer)
        {
            Controller.AddDirectionAttribute(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RenderEndOuterTable(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        protected override object SaveViewState()
        {
            object[] states = new object[6];

            states[0] = base.SaveViewState();
            if (_toolbarStyle != null)
                states[1] = ((IStateManager)_toolbarStyle).SaveViewState();
            if (_toolbarButtonStyle != null)
                states[2] = ((IStateManager)_toolbarButtonStyle).SaveViewState();
            if (_toolbarButtonHoverStyle != null)
                states[3] = ((IStateManager)_toolbarButtonHoverStyle).SaveViewState();
            if (_toolbarButtonPressedStyle != null)
                states[4] = ((IStateManager)_toolbarButtonPressedStyle).SaveViewState();

            for (int i = 0; i < states.Length; i++)
            {
                if (states[i] != null)
                    return states;
            }
            return null;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState == null)
                return;

            object[] states = (object[])savedState;

            base.LoadViewState(states[0]);
            if (states[1] != null)
                ((IStateManager)ToolbarStyle).LoadViewState(states[1]);
            if (states[2] != null)
                ((IStateManager)ToolbarButtonStyle).LoadViewState(states[2]);
            if (states[3] != null)
                ((IStateManager)ToolbarButtonHoverStyle).LoadViewState(states[3]);
            if (states[4] != null)
                ((IStateManager)ToolbarButtonPressedStyle).LoadViewState(states[4]);
            if (states[5] != null)
                ((IStateManager)CustomToolbarButtons).LoadViewState(states[5]);
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_toolbarStyle != null)
                ((IStateManager)_toolbarStyle).TrackViewState();
            if (_toolbarButtonStyle != null)
                ((IStateManager)_toolbarButtonStyle).TrackViewState();
            if (_toolbarButtonHoverStyle != null)
                ((IStateManager)_toolbarButtonHoverStyle).TrackViewState();
            if (_toolbarButtonPressedStyle != null)
                ((IStateManager)_toolbarButtonPressedStyle).TrackViewState();
        }

        void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "Upload")
            {
                HttpPostedFile uploadedFile = Page.Request.Files[ClientID + "_Upload"];
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    FileManagerItemInfo dir = GetCurrentDirectory();
                    Controller.ProcessFileUpload(dir, uploadedFile);
                }
            }
            else if (eventArgument == "Select")
            {
                if (SelectCommand != null)
                    SelectCommand.Invoke(this, EventArgs.Empty);
            }
            else if (eventArgument == "Cancel")
            {
                if (CancelCommand != null)
                    CancelCommand.Invoke(this, EventArgs.Empty);
            }
            else if (eventArgument.StartsWith("Toolbar:", StringComparison.Ordinal))
            {
                int i = int.Parse(eventArgument.Substring(8));
                CustomToolbarButton button = CustomToolbarButtons[i];
                OnToolbarCommand(new CommandEventArgs(button.CommandName, button.CommandArgument));
            }
        }

        void OnToolbarCommand(CommandEventArgs e)
        {
            if (ToolbarCommand != null)
                ToolbarCommand(this, e);
        }

        internal override FileManagerItemInfo GetCurrentDirectory()
        {
            return Controller.ResolveFileManagerItemInfo(_fileView.Directory);
        }

        #region IPostBackEventHandler Members

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            RaisePostBackEvent(eventArgument);
        }

        #endregion
    }
}

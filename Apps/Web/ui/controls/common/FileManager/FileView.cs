using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class FileView : FileManagerControl
    {
        #region Constructors

        public FileView() : base() { InitializeDefaults(); }
        public FileView(FileManagerController controller) : base(controller) { InitializeDefaults(); }

        #endregion

        #region Fields

        BorderedPanelStyle _detailsColumnHeaderStyle;
        Style _editTextBoxStyle;
        Style _itemStyle;
        Style _selectedItemStyle;
        Style _detailsSortedColumnStyle;
        StringBuilder _initScript = new StringBuilder();
        ContextMenu _contextMenu;
        ContextMenu _selectedItemsContextMenu;

        // control state fields
        FileViewRenderMode _view = FileViewRenderMode.Icons;
        SortMode _sort = SortMode.Name;
        SortDirection _sortDirection;
        bool _showInGroups;
        string _directory;
        FileManagerItemInfo _currentDirectory;

        #endregion

        #region Properties

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public BorderedPanelStyle DetailsColumnHeaderStyle
        {
            get
            {
                if (_detailsColumnHeaderStyle == null)
                {
                    _detailsColumnHeaderStyle = new BorderedPanelStyle();
                    _detailsColumnHeaderStyle.Height = Unit.Pixel(17);
                    _detailsColumnHeaderStyle.BackColor = Color.FromArgb(0xebeadb);
                    _detailsColumnHeaderStyle.BackImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/resources.detailscolumnheader_RB.gif").ToString();
                    _detailsColumnHeaderStyle.OuterBorderLeftImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/resources.detailscolumnheader_R.gif").ToString();
                    _detailsColumnHeaderStyle.Font.Bold = false;
                    _detailsColumnHeaderStyle.PaddingLeft = Unit.Pixel(4);
                    _detailsColumnHeaderStyle.PaddingRight = Unit.Pixel(4);
                    _detailsColumnHeaderStyle.PaddingTop = Unit.Pixel(3);
                    _detailsColumnHeaderStyle.OuterBorderStyle = OuterBorderStyle.Left;
                    _detailsColumnHeaderStyle.OuterBorderLeftWidth = Unit.Pixel(2);

                    if (IsTrackingViewState)
                        ((IStateManager)_detailsColumnHeaderStyle).TrackViewState();
                }
                return _detailsColumnHeaderStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style DetailsSortedColumnStyle
        {
            get
            {
                if (_detailsSortedColumnStyle == null)
                {
                    _detailsSortedColumnStyle = new Style();
                    _detailsSortedColumnStyle.BackColor = Color.FromArgb(0xF7F7F7);

                    if (IsTrackingViewState)
                        ((IStateManager)_detailsSortedColumnStyle).TrackViewState();
                }
                return _detailsSortedColumnStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style SelectedItemStyle
        {
            get
            {
                if (_selectedItemStyle == null)
                {
                    _selectedItemStyle = new Style();
                    _selectedItemStyle.ForeColor = Color.White;
                    _selectedItemStyle.BackColor = Color.FromArgb(0x316AC5);

                    if (IsTrackingViewState)
                        ((IStateManager)_selectedItemStyle).TrackViewState();
                }
                return _selectedItemStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style ItemStyle
        {
            get
            {
                if (_itemStyle == null)
                {
                    _itemStyle = new Style();

                    if (IsTrackingViewState)
                        ((IStateManager)_itemStyle).TrackViewState();
                }
                return _itemStyle;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Appearance")]
        public Style EditTextBoxStyle
        {
            get
            {
                if (_editTextBoxStyle == null)
                {
                    _editTextBoxStyle = new Style();
                    _editTextBoxStyle.BorderStyle = BorderStyle.Solid;
                    _editTextBoxStyle.BorderWidth = Unit.Pixel(1);
                    _editTextBoxStyle.BorderColor = Color.Black;
                    _editTextBoxStyle.Font.Names = new string[] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
                    _editTextBoxStyle.Font.Size = FontUnit.Parse("11px", null);

                    if (IsTrackingViewState)
                        ((IStateManager)_editTextBoxStyle).TrackViewState();
                }
                return _editTextBoxStyle;
            }
        }

        [Bindable(true)]
        [Category("Data")]
        [DefaultValue("[0]/")]
        [Localizable(false)]
        [Themeable(false)]
        public string Directory
        {
            get { return _directory == null ? "[0]/" : _directory; }
            internal set
            {
                _directory = value;
                _currentDirectory = null;
            }
        }

        string[] selectedItemsStr;
        FileManagerItemInfo[] selectedItems;
        public override FileManagerItemInfo[] SelectedItems
        {
            get
            {
                if (selectedItems == null)
                {
                    if (selectedItemsStr == null || selectedItemsStr.Length == 0)
                        selectedItems = new FileManagerItemInfo[0];
                    else
                    {
                        ArrayList selectedItemsArray = new ArrayList();
                        FileManagerItemInfo dir = GetCurrentDirectory();
                        foreach (string name in selectedItemsStr)
                        {
                            FileManagerItemInfo itemInfo = ResolveFileManagerItemInfo(name);
                            selectedItemsArray.Add(itemInfo);
                        }
                        selectedItems = (FileManagerItemInfo[])selectedItemsArray.ToArray(typeof(FileManagerItemInfo));
                    }
                }
                return selectedItems;
            }
        }

        public override FileManagerItemInfo CurrentDirectory
        {
            get
            {
                if (_currentDirectory == null)
                    _currentDirectory = Controller.ResolveFileManagerItemInfo(Directory);
                return _currentDirectory;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(false)]
        private bool ShowInGroups
        {
            get { return _showInGroups; }
            set { _showInGroups = value; }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(FileViewRenderMode.Icons)]
        [Localizable(false)]
        public FileViewRenderMode View
        {
            get { return _view; }
            set { _view = value; }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(SortMode.Name)]
        [Localizable(false)]
        public SortMode Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(SortDirection.Ascending)]
        [Localizable(false)]
        public SortDirection SortDirection
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public bool UseLinkToOpenItem
        {
            get { return (bool)(ViewState["UseLinkToOpenItem"] ?? false); }
            set { ViewState["UseLinkToOpenItem"] = value; }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("_blank")]
        [Localizable(false)]
        public string LinkToOpenItemTarget
        {
            get { return (string)(ViewState["LinkToOpenItemTarget"] ?? "_blank"); }
            set { ViewState["LinkToOpenItemTarget"] = value; }
        }

        internal string LinkToOpenItemClass
        {
            get { return "linkToOpenItem" + ClientID; }
        }

        #endregion

        #region Methods

        private void InitializeDefaults()
        {
            LinkToOpenItemTarget = "_blank";
        }

        protected internal override string RenderContents()
        {
            StringBuilder sb = new StringBuilder();
            HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(sb, null));
            RenderContents(writer);
            return sb.ToString();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (DesignMode)
                return;

            DirectoryInfo directoryInfo = GetCurrentDirectory().Directory;

            FileViewRender render = FileViewRender.GetRender(this);
            DirectoryProvider provider = new DirectoryProvider(directoryInfo, Sort, SortDirection);

            render.RenderBeginList(writer);

            //FileViewItem upDirectory = new FileViewUpDirectoryItem(directoryInfo.Parent, this);
            //render.RenderItem(output, upDirectory);

            if (ShowInGroups)
            {
                GroupInfo[] groups = provider.GetGroups();
                foreach (GroupInfo group in groups)
                {
                    render.RenderBeginGroup(writer, group);
                    foreach (FileSystemInfo fsi in provider.GetFileSystemInfos(group))
                    {
                        FileViewItem item = new FileViewItem(fsi, this);
                        render.RenderItem(writer, item);
                    }

                    render.RenderEndGroup(writer, group);
                }
            }
            else
            {
                foreach (FileSystemInfo fsi in provider.GetFileSystemInfos())
                {
                    if (fsi is FileInfo)
                    {
                        string ext = fsi.Extension.ToLower(CultureInfo.InvariantCulture).TrimStart('.');
                        if (Controller.HiddenFilesArray.Contains(ext))
                            continue;
                    }
                    FileViewItem item = new FileViewItem(fsi, this);

                    render.RenderItem(writer, item);
                }
            }

            render.RenderEndList(writer);
            RenderInitScript(writer);
        }

        private void RenderInitScript(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_InitScript");
            output.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            output.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            output.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Write(_initScript.ToString());
            output.Write(FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".Items = new Array(" + String.Join(",", (string[])itemIds.ToArray(typeof(string))) + ");\r\n");
            output.Write(FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".ClearSelectedItems();\r\n");
            output.RenderEndTag();
        }

        internal override FileManagerItemInfo GetCurrentDirectory()
        {
            return Controller.ResolveFileManagerItemInfo(Directory);
        }

        int _itemIndex;
        ArrayList itemIds = new ArrayList();
        internal void RenderItemBeginTag(HtmlTextWriter output, FileViewItem item)
        {
            string id = ClientID + "_Item_" + _itemIndex;
            item.ClientID = id;

            int fileType = -2; //is Directory
            if (item.FileSystemInfo is FileInfo)
            {
                FileInfo file = (FileInfo)item.FileSystemInfo;
                FileType ft = Controller.GetFileType(file);
                if (ft != null)
                    fileType = Controller.FileTypes.IndexOf(ft);
                else
                    fileType = -1;
            }

            itemIds.Add(id);

            output.AddAttribute(HtmlTextWriterAttribute.Id, id);
            output.RenderBeginTag(HtmlTextWriterTag.Div);

            // trace init script 
            _initScript.AppendLine("var " + id + " = document.getElementById('" + id + "');");
            _initScript.AppendLine(FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".InitItem(" + id + ",'" + FileManagerController.EncodeURIComponent(item.Name) + "'," + (item.IsDirectory ? "true" : "false") + "," + (item.CanBeRenamed ? "true" : "false") + "," + "false" + "," + fileType + ");");

            _itemIndex++;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        internal void RenderItemEndTag(HtmlTextWriter output)
        {
            output.RenderEndTag();
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "100");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_ContextMenu");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            _contextMenu.RenderControl(writer);
            writer.RenderEndTag();

            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "100");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_SelectedItemsContextMenu");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            _selectedItemsContextMenu.RenderControl(writer);
            writer.RenderEndTag();

            RenderFocusTextBox(writer);
            RenderEditTextBox(writer);

            base.RenderBeginTag(writer);
        }

        void RenderEditTextBox(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "100");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Controller.ClientID + "_TextBox");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        private void RenderFocusTextBox(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "1px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "transparent");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_Focus");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }


        internal override FileManagerItemInfo ResolveFileManagerItemInfo(string path)
        {
            string fileManagerPath;
            if (String.IsNullOrEmpty(path))
                fileManagerPath = Directory;
            else if (path[0] == '[' || path[0] == '/')
                fileManagerPath = path;
            else
                fileManagerPath = VirtualPathUtility.AppendTrailingSlash(Directory) + path;

            return Controller.ResolveFileManagerItemInfo(fileManagerPath);
        }

        protected override object SaveViewState()
        {
            object[] states = new object[6];

            states[0] = base.SaveViewState();
            if (_detailsColumnHeaderStyle != null)
                states[1] = ((IStateManager)_detailsColumnHeaderStyle).SaveViewState();
            if (_detailsSortedColumnStyle != null)
                states[2] = ((IStateManager)_detailsSortedColumnStyle).SaveViewState();
            if (_editTextBoxStyle != null)
                states[3] = ((IStateManager)_editTextBoxStyle).SaveViewState();
            if (_itemStyle != null)
                states[4] = ((IStateManager)_itemStyle).SaveViewState();
            if (_selectedItemStyle != null)
                states[5] = ((IStateManager)_selectedItemStyle).SaveViewState();

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
                ((IStateManager)DetailsColumnHeaderStyle).LoadViewState(states[1]);
            if (states[2] != null)
                ((IStateManager)DetailsSortedColumnStyle).LoadViewState(states[2]);
            if (states[3] != null)
                ((IStateManager)EditTextBoxStyle).LoadViewState(states[3]);
            if (states[4] != null)
                ((IStateManager)ItemStyle).LoadViewState(states[4]);
            if (states[5] != null)
                ((IStateManager)SelectedItemStyle).LoadViewState(states[5]);
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_detailsColumnHeaderStyle != null)
                ((IStateManager)_detailsColumnHeaderStyle).TrackViewState();
            if (_detailsSortedColumnStyle != null)
                ((IStateManager)_detailsSortedColumnStyle).TrackViewState();
            if (_editTextBoxStyle != null)
                ((IStateManager)_editTextBoxStyle).TrackViewState();
            if (_itemStyle != null)
                ((IStateManager)_itemStyle).TrackViewState();
            if (_selectedItemStyle != null)
                ((IStateManager)_selectedItemStyle).TrackViewState();
        }

        protected override object SaveControlState()
        {
            RegisterHiddenField("View", View.ToString());
            RegisterHiddenField("Sort", Sort.ToString());
            RegisterHiddenField("SortDirection", SortDirection.ToString());
            RegisterHiddenField("ShowInGroups", ShowInGroups ? "true" : "false");
            RegisterHiddenField("Directory", FileManagerController.EncodeURIComponent(CurrentDirectory.FileManagerPath));
            RegisterHiddenField("SelectedItems", "");

            return new object[] { base.SaveControlState() };
        }

        protected override void LoadControlState(object savedState)
        {
            base.LoadControlState(((object[])savedState)[0]);

            View = (FileViewRenderMode)Enum.Parse(typeof(FileViewRenderMode), GetValueFromHiddenField("View"));
            Sort = (SortMode)Enum.Parse(typeof(SortMode), GetValueFromHiddenField("Sort"));
            SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), GetValueFromHiddenField("SortDirection"));
            ShowInGroups = bool.Parse(GetValueFromHiddenField("ShowInGroups"));
            Directory = HttpUtility.UrlDecode(GetValueFromHiddenField("Directory"));

            string[] selectedItemsEncoded = GetValueFromHiddenField("SelectedItems").Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            ArrayList selectedItemsArray = new ArrayList();
            foreach (string pathEncoded in selectedItemsEncoded)
            {
                string path = HttpUtility.UrlDecode(pathEncoded);
                selectedItemsArray.Add(path);
            }

            selectedItemsStr = (string[])selectedItemsArray.ToArray(typeof(string));
        }

        string GetInitInstanceScript()
        {

            StringBuilder sb = new StringBuilder();
            string fileView = FileManagerController.ClientScriptObjectNamePrefix + ClientID;
            sb.AppendLine("var " + fileView + "=new FileView('" + ClientID + "','" + Controller.ClientID + "','" + ItemStyle.RegisteredCssClass + "','" + SelectedItemStyle.RegisteredCssClass + "','" + EditTextBoxStyle.RegisteredCssClass + "');");
            sb.AppendLine(fileView + ".Initialize();");

            return sb.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ((LinkMePage)Page).AddJavaScriptReference(JavaScripts.FileView);
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.Header.StyleSheet.RegisterStyle(ItemStyle, this);
            Page.Header.StyleSheet.RegisterStyle(SelectedItemStyle, this);
            Page.Header.StyleSheet.RegisterStyle(EditTextBoxStyle, this);

            if (UseLinkToOpenItem)
            {
                Style linkStyle = new Style();
                linkStyle.Font.Underline = false;
                Page.Header.StyleSheet.CreateStyleRule(linkStyle, this, "a." + LinkToOpenItemClass);

                Style hoverLinkStyle = new Style();
                hoverLinkStyle.Font.Underline = true;
                Page.Header.StyleSheet.CreateStyleRule(hoverLinkStyle, this, "a." + LinkToOpenItemClass + ":hover");

                Style itemLinkStyle = new Style();
                itemLinkStyle.ForeColor = ItemStyle.ForeColor;
                if (itemLinkStyle.ForeColor == Color.Empty)
                    itemLinkStyle.ForeColor = ForeColor;
                Page.Header.StyleSheet.CreateStyleRule(itemLinkStyle, this, "." + ItemStyle.RegisteredCssClass + " a." + LinkToOpenItemClass);

                Style selectedItemLinkStyle = new Style();
                selectedItemLinkStyle.ForeColor = SelectedItemStyle.ForeColor;
                if (selectedItemLinkStyle.ForeColor == Color.Empty)
                    selectedItemLinkStyle.ForeColor = ForeColor;
                Page.Header.StyleSheet.CreateStyleRule(selectedItemLinkStyle, this, "." + SelectedItemStyle.RegisteredCssClass + " a." + LinkToOpenItemClass);
            }

            Page.ClientScript.RegisterStartupScript(typeof(FileView), ClientID, GetInitInstanceScript(), true);

            CreateContextMenu();
            CreateSelectedItemsContextMenu();
        }

        internal string GetSortEventReference(SortMode sort)
        {
            return Controller.GetCommandEventReference(this, FileManagerCommands.FileViewSort.ToString(), "'" + sort.ToString() + "'");
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".SetFocus()");
            Controller.AddDirectionAttribute(writer);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "20");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
        }

        void CreateContextMenu()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.EnableViewState = false;
            _contextMenu.StaticEnableDefaultPopOutImage = false;
            _contextMenu.DynamicEnableDefaultPopOutImage = false;
            _contextMenu.Orientation = Orientation.Horizontal;
            _contextMenu.SkipLinkText = String.Empty;
            _contextMenu.StaticItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(CreateContextMenuRootItem));
            _contextMenu.DynamicItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(CreateContextMenuPopupItem));
            SetContextMenuStyle(_contextMenu);
            Controls.Add(_contextMenu);

            // Root
            MenuItem root = new MenuItem();
            root.Text = "_contextMenu";
            root.NavigateUrl = "javascript: return;";
            _contextMenu.Items.Add(root);

            string clientClickFunction = "javascript:" + FileManagerController.ClientScriptObjectNamePrefix + Controller.ClientID + ".On{0}(" + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ", '{1}');return false;";

            // View
            MenuItem itemView = new MenuItem();
            itemView.Text = GetResourceString("View", "View");
            itemView.Value = "View";
            itemView.NavigateUrl = "javascript: return;";
            root.ChildItems.Add(itemView);

            // Icons
            MenuItem itemViewIcons = new MenuItem();
            itemViewIcons.Text = GetResourceString("Icons", "Icons");
            itemViewIcons.Value = "Icons";
            itemViewIcons.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Icons);
            itemView.ChildItems.Add(itemViewIcons);

            // Details
            MenuItem itemViewDetails = new MenuItem();
            itemViewDetails.Text = GetResourceString("Details", "Details");
            itemViewDetails.Value = "Details";
            itemViewDetails.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Details);
            itemView.ChildItems.Add(itemViewDetails);

            if (Controller.SupportThumbnails)
            {
                // Thumbnails
                MenuItem itemViewThumbnails = new MenuItem();
                itemViewThumbnails.Text = GetResourceString("Thumbnails", "Thumbnails");
                itemViewThumbnails.Value = "Thumbnails";
                itemViewThumbnails.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewChangeView, FileViewRenderMode.Thumbnails);
                itemView.ChildItems.Add(itemViewThumbnails);
            }

            root.ChildItems.Add(new MenuItem("__separator__", "__separator__", null, "javascript: return;"));

            // Arrange Icons By
            MenuItem itemArrangeIconsBy = new MenuItem();
            itemArrangeIconsBy.Text = GetResourceString("Arrange_Icons_By", "Arrange Icons By");
            itemArrangeIconsBy.Value = "Arrange_Icons_By";
            itemArrangeIconsBy.NavigateUrl = "javascript: return;";
            root.ChildItems.Add(itemArrangeIconsBy);

            // Name
            MenuItem itemArrangeIconsByName = new MenuItem();
            itemArrangeIconsByName.Text = GetResourceString("Name", "Name");
            itemArrangeIconsByName.Value = "Name";
            itemArrangeIconsByName.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewSort, SortMode.Name);
            itemArrangeIconsBy.ChildItems.Add(itemArrangeIconsByName);

            // Size
            MenuItem itemArrangeIconsBySize = new MenuItem();
            itemArrangeIconsBySize.Text = GetResourceString("Size", "Size");
            itemArrangeIconsBySize.Value = "Size";
            itemArrangeIconsBySize.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewSort, SortMode.Size);
            itemArrangeIconsBy.ChildItems.Add(itemArrangeIconsBySize);

            // Type
            MenuItem itemArrangeIconsByType = new MenuItem();
            itemArrangeIconsByType.Text = GetResourceString("Type", "Type");
            itemArrangeIconsByType.Value = "Type";
            itemArrangeIconsByType.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewSort, SortMode.Type);
            itemArrangeIconsBy.ChildItems.Add(itemArrangeIconsByType);

            // Modified
            MenuItem itemArrangeIconsByModified = new MenuItem();
            itemArrangeIconsByModified.Text = GetResourceString("Date_Modified", "Date Modified");
            itemArrangeIconsByModified.Value = "Modified";
            itemArrangeIconsByModified.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.FileViewSort, SortMode.Modified);
            itemArrangeIconsBy.ChildItems.Add(itemArrangeIconsByModified);

            // Refresh
            MenuItem itemRefresh = new MenuItem();
            itemRefresh.Text = Controller.GetResourceString("Refresh", "Refresh");
            itemRefresh.Value = "Refresh";
            itemRefresh.ImageUrl = Controller.GetToolbarImage(ToolbarImages.Refresh);
            itemRefresh.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.Refresh, "");
            root.ChildItems.Add(itemRefresh);

            root.ChildItems.Add(new MenuItem("__separator__", "__separator__", null, "javascript: return;"));

            // New
            MenuItem itemNew = new MenuItem();
            itemNew.Text = GetResourceString("Create", "New");
            itemNew.Value = "Create";
            itemNew.NavigateUrl = "javascript: return;";
            itemNew.Enabled = !ReadOnly;
            root.ChildItems.Add(itemNew);

            if (!ReadOnly)
            {
                // New Folder
                MenuItem itemFolder = new MenuItem();
                itemFolder.Text = GetResourceString("New_Folder", "Folder");
                itemFolder.Value = "New_Folder";
                itemFolder.ImageUrl = Controller.GetFolderSmallImage();
                itemFolder.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.NewFolder, "");
                itemNew.ChildItems.Add(itemFolder);
            }

            // client script
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function " + ClientID + "_ShowContextMenu(x,y) {");

            string s = new ApplicationUrl("~/ui/images/controls/FileManager/Bullet.gif").ToString();
            sb.AppendLine("var bulletImgSrc = '" + new ApplicationUrl("~/ui/images/controls/FileManager/Bullet.gif").ToString() + "';");
            sb.AppendLine("var emptyImgSrc = '" + new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString() + "';");

            sb.AppendLine("var nameImg = WebForm_GetElementById('" + ClientID + "CMIMGName');");
            sb.AppendLine("var sizeImg = WebForm_GetElementById('" + ClientID + "CMIMGSize');");
            sb.AppendLine("var typeImg = WebForm_GetElementById('" + ClientID + "CMIMGType');");
            sb.AppendLine("var modifiedImg = WebForm_GetElementById('" + ClientID + "CMIMGModified');");
            sb.AppendLine("var iconsImg = WebForm_GetElementById('" + ClientID + "CMIMGIcons');");
            sb.AppendLine("var detailsImg = WebForm_GetElementById('" + ClientID + "CMIMGDetails');");
            if (Controller.SupportThumbnails)
                sb.AppendLine("var thumbnailsImg = WebForm_GetElementById('" + ClientID + "CMIMGThumbnails');");

            sb.AppendLine("nameImg.src = emptyImgSrc;");
            sb.AppendLine("sizeImg.src = emptyImgSrc;");
            sb.AppendLine("typeImg.src = emptyImgSrc;");
            sb.AppendLine("modifiedImg.src = emptyImgSrc;");
            sb.AppendLine("iconsImg.src = emptyImgSrc;");
            sb.AppendLine("detailsImg.src = emptyImgSrc;");
            if (Controller.SupportThumbnails)
                sb.AppendLine("thumbnailsImg.src = emptyImgSrc;");

            sb.AppendLine("var sort = " + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".GetSort();");
            sb.AppendLine("switch(sort) {");
            sb.AppendLine("case '" + SortMode.Name + "':");
            sb.AppendLine("nameImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            sb.AppendLine("case '" + SortMode.Type + "':");
            sb.AppendLine("typeImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            sb.AppendLine("case '" + SortMode.Size + "':");
            sb.AppendLine("sizeImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            sb.AppendLine("case '" + SortMode.Modified + "':");
            sb.AppendLine("modifiedImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            sb.AppendLine("}");

            sb.AppendLine("var view = " + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".GetView();");
            sb.AppendLine("switch(view) {");
            sb.AppendLine("case '" + FileViewRenderMode.Icons + "':");
            sb.AppendLine("iconsImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            sb.AppendLine("case '" + FileViewRenderMode.Details + "':");
            sb.AppendLine("detailsImg.src = bulletImgSrc;");
            sb.AppendLine("break;");
            if (Controller.SupportThumbnails)
            {
                sb.AppendLine("case '" + FileViewRenderMode.Thumbnails + "':");
                sb.AppendLine("thumbnailsImg.src = bulletImgSrc;");
                sb.AppendLine("break;");
            }
            sb.AppendLine("}");


            sb.AppendLine("var node = WebForm_GetElementById('" + ClientID + "_ContextMenu')");
            sb.AppendLine("WebForm_SetElementX(node, x)");
            sb.AppendLine("WebForm_SetElementY(node, y)");
            sb.AppendLine("Menu_HoverStatic(WebForm_GetElementById('" + _contextMenu.ClientID + "n0'));");
            sb.AppendLine("}");
            Page.ClientScript.RegisterClientScriptBlock(typeof(FileView), ClientID + "_ShowContextMenu", sb.ToString(), true);
        }

        void CreateSelectedItemsContextMenu()
        {
            _selectedItemsContextMenu = new ContextMenu();
            _selectedItemsContextMenu.EnableViewState = false;
            _selectedItemsContextMenu.StaticEnableDefaultPopOutImage = false;
            _selectedItemsContextMenu.DynamicEnableDefaultPopOutImage = false;
            _selectedItemsContextMenu.Orientation = Orientation.Horizontal;
            _selectedItemsContextMenu.SkipLinkText = String.Empty;
            _selectedItemsContextMenu.StaticItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(CreateContextMenuRootItem));
            _selectedItemsContextMenu.DynamicItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(CreateContextMenuPopupItem));
            SetContextMenuStyle(_selectedItemsContextMenu);
            Controls.Add(_selectedItemsContextMenu);

            // Root
            MenuItem root = new MenuItem();
            root.Text = "_contextMenu";
            root.NavigateUrl = "javascript: return;";
            _selectedItemsContextMenu.Items.Add(root);

            string clientClickFunction = "javascript:" + FileManagerController.ClientScriptObjectNamePrefix + Controller.ClientID + ".On{0}(" + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ", '{1}');return false;";

            StringBuilder sbCommands = new StringBuilder();

            MenuItem itemOpen = new MenuItem();
            itemOpen.Text = GetResourceString("Open", "Open");
            itemOpen.Value = "Open";
            itemOpen.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.ExecuteCommand, "0:-1");
            root.ChildItems.Add(itemOpen);

            MenuItem itemDownload = new MenuItem();
            itemDownload.Text = GetResourceString("Download", "Download");
            itemDownload.Value = "Download";
            itemDownload.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.ExecuteCommand, "0:-2");
            root.ChildItems.Add(itemDownload);
            sbCommands.AppendLine(ClientID + "_SetCommandVisible('" + ClientID + "CMDDownload', (" + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".SelectedItems.length==1) && (" + FileManagerController.ClientScriptObjectNamePrefix + ClientID + ".SelectedItems[0].FileType!=-2));");

            root.ChildItems.Add(new MenuItem("__separator__", "__separator__", null, "javascript: return;"));

            // Copy to

            bool addSeparator = false;
            if (Controller.CommandOptions.Copy)
            {
                MenuItem itemCopyTo = new MenuItem();
                itemCopyTo.Text = GetResourceString("Copy", "Copy");
                itemCopyTo.Value = "Copy";
                itemCopyTo.ImageUrl = Controller.GetToolbarImage(ToolbarImages.Copy);
                itemCopyTo.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.SelectedItemsCopyTo, "");
                itemCopyTo.Enabled = !ReadOnly;
                root.ChildItems.Add(itemCopyTo);
                addSeparator = true;
            }

            // Move to

            if (Controller.CommandOptions.Move)
            {
                MenuItem itemMoveTo = new MenuItem();
                itemMoveTo.Text = GetResourceString("Move", "Move");
                itemMoveTo.Value = "Move";
                itemMoveTo.ImageUrl = Controller.GetToolbarImage(ToolbarImages.Move);
                itemMoveTo.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.SelectedItemsMoveTo, "");
                itemMoveTo.Enabled = !ReadOnly && AllowDelete;
                root.ChildItems.Add(itemMoveTo);
                addSeparator = true;
            }

            if (addSeparator)
                root.ChildItems.Add(new MenuItem("__separator__", "__separator__", null, "javascript: return;"));

            // Delete

            if (Controller.CommandOptions.Delete)
            {
                MenuItem itemDelete = new MenuItem();
                itemDelete.Text = GetResourceString("Delete", "Delete");
                itemDelete.Value = "Delete";
                itemDelete.ImageUrl = Controller.GetToolbarImage(ToolbarImages.Delete);
                itemDelete.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.SelectedItemsDelete, "");
                itemDelete.Enabled = !ReadOnly && AllowDelete;
                root.ChildItems.Add(itemDelete);
            }

            // Rename

            if (Controller.CommandOptions.Rename)
            {
                MenuItem itemRename = new MenuItem();
                itemRename.Text = GetResourceString("Rename", "Rename");
                itemRename.Value = "Rename";
                itemRename.ImageUrl = Controller.GetToolbarImage(ToolbarImages.Rename);
                itemRename.NavigateUrl = String.Format(clientClickFunction, FileManagerCommands.Rename, "");
                itemRename.Enabled = !ReadOnly && AllowDelete;
                root.ChildItems.Add(itemRename);
            }

            // client script
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function " + ClientID + "_ShowSelectedItemsContextMenu(x,y) {");
            sb.Append(sbCommands.ToString());
            sb.AppendLine("var node = WebForm_GetElementById('" + ClientID + "_SelectedItemsContextMenu')");
            sb.AppendLine("WebForm_SetElementX(node, x)");
            sb.AppendLine("WebForm_SetElementY(node, y)");
            sb.AppendLine("WebForm_GetElementById('" + _selectedItemsContextMenu.ClientID + "n0Items').style.height = \"auto\";");
            sb.AppendLine("WebForm_GetElementById('" + _selectedItemsContextMenu.ClientID + "n0Items').physicalHeight = null;");
            sb.AppendLine("Menu_HoverStatic(WebForm_GetElementById('" + _selectedItemsContextMenu.ClientID + "n0'));");
            sb.AppendLine("}");
            sb.AppendLine("function " + ClientID + "_SetCommandVisible(command, value) {");
            sb.AppendLine("var node = WebForm_GetElementById(command);");
            sb.AppendLine("var row = node.parentNode.parentNode.parentNode.parentNode.parentNode;");
            sb.AppendLine("if (value) {");
            sb.AppendLine("row.style.visibility = \"visible\";");
            sb.AppendLine("row.style.display = \"block\";");
            sb.AppendLine("row.style.position = \"static\";");
            sb.AppendLine("row.parentNode.parentNode.style.height = \"auto\";");
            sb.AppendLine("} else {");
            sb.AppendLine("row.style.visibility = \"hidden\";");
            sb.AppendLine("row.style.display = \"none\";");
            sb.AppendLine("row.style.position = \"absolute\";");
            sb.AppendLine("row.parentNode.parentNode.style.height = \"0px\";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            Page.ClientScript.RegisterClientScriptBlock(typeof(FileView), ClientID + "_ShowSelectedItemsContextMenu", sb.ToString(), true);
        }

        void SetContextMenuStyle(Menu menu)
        {
            // TODO
            menu.DynamicMenuStyle.BackColor = Color.White;
            menu.DynamicMenuStyle.BorderStyle = BorderStyle.Solid;
            menu.DynamicMenuStyle.BorderWidth = Unit.Pixel(1);
            menu.DynamicMenuStyle.BorderColor = Color.FromArgb(0xACA899);
            menu.DynamicMenuStyle.HorizontalPadding = Unit.Pixel(2);
            menu.DynamicMenuStyle.VerticalPadding = Unit.Pixel(2);

            menu.DynamicMenuItemStyle.ForeColor = Color.Black;
            menu.DynamicMenuItemStyle.Font.Names = new string[] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
            menu.DynamicMenuItemStyle.VerticalPadding = Unit.Pixel(1);
            menu.DynamicMenuItemStyle.Font.Size = FontUnit.Parse("11px", null);

            menu.DynamicHoverStyle.ForeColor = Color.White;
            menu.DynamicHoverStyle.BackColor = Color.FromArgb(0x316AC5);
        }

        void CreateContextMenuPopupItem(Control control)
        {
            MenuItemTemplateContainer container = (MenuItemTemplateContainer)control;
            MenuItem menuItem = (MenuItem)container.DataItem;
            if (menuItem.Text == "__separator__")
            {
                Table t = new Table();
                t.CellPadding = 0;
                t.CellSpacing = 0;
                t.BorderWidth = 0;
                t.BackColor = Color.FromArgb(0xACA899);
                t.Height = 1;
                t.Width = Unit.Percentage(100);
                t.Style[HtmlTextWriterStyle.Cursor] = "default";
                container.Controls.Add(t);
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString();
                img.Width = 1;
                img.Height = 1;
                c.Controls.Add(img);
                r.Cells.Add(c);
                t.Rows.Add(r);
            }
            else
            {
                Table t = new Table();
                t.CellPadding = 0;
                t.CellSpacing = 0;
                t.BorderWidth = 0;
                t.Style[HtmlTextWriterStyle.Cursor] = "default";
                if (menuItem.Enabled)
                    t.Attributes["onclick"] = menuItem.NavigateUrl;
                else
                    t.Style["color"] = "gray";
                t.Attributes["id"] = ClientID + "CMD" + menuItem.Value;
                container.Controls.Add(t);
                TableRow r = new TableRow();
                t.Rows.Add(r);
                TableCell c1 = new TableCell();
                System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                if (String.IsNullOrEmpty(menuItem.ImageUrl))
                    img1.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString();
                else
                    img1.ImageUrl = menuItem.ImageUrl;
                img1.Attributes["id"] = ClientID + "CMIMG" + menuItem.Value;
                img1.Width = 16;
                img1.Height = 16;
                c1.Controls.Add(img1);
                r.Cells.Add(c1);
                TableCell c2 = new TableCell();
                c2.Wrap = false;
                c2.Style[HtmlTextWriterStyle.PaddingLeft] = "2px";
                c2.Style[HtmlTextWriterStyle.PaddingRight] = "2px";
                c2.Text = "&nbsp;" + menuItem.Text;
                c2.Width = Unit.Percentage(100);
                r.Cells.Add(c2);
                TableCell c3 = new TableCell();
                System.Web.UI.WebControls.Image img3 = new System.Web.UI.WebControls.Image();
                if (menuItem.ChildItems.Count == 0)
                    img3.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/Empty.gif").ToString();
                else
                    if (Controller.IsRightToLeft)
                        img3.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/PopOutRtl.gif").ToString();
                    else
                        img3.ImageUrl = new ApplicationUrl("~/ui/images/controls/FileManager/PopOut.gif").ToString();
                img3.Width = 16;
                img3.Height = 16;
                c3.Controls.Add(img3);
                r.Cells.Add(c3);
            }
        }

        void CreateContextMenuRootItem(Control control)
        {
        }

        #endregion
    }
}

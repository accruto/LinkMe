using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    internal enum ToolbarImages
    {
        Copy,
        Move,
        Rename,
        Delete,
        View,
        FolderUp,
        Process,
        NewFolder,
        Refresh,
    }

    internal enum FileManagerCommands
    {
        FileViewSort,
        FileViewNavigate,
        FileViewChangeView,
        FileViewShowInGroups,
        SelectedItemsCopyTo,
        SelectedItemsMoveTo,
        SelectedItemsDelete,
        Rename,
        NewDocument,
        NewFolder,
        Refresh,
        ExecuteCommand
    }

    [PersistChildren(false)]
    [ParseChildren(true)]
    public sealed class FileManagerController : Control, ICallbackEventHandler, IPostBackEventHandler
    {
        #region Fields

        internal static readonly Unit SmallImageWidth = 16;
        internal static readonly Unit SmallImageHeight = 16;
        internal static readonly Unit LargeImageWidth = 32;
        internal static readonly Unit LargeImageHeight = 32;
        static readonly Hashtable _imageExtension;
        static readonly bool _supportThumbnails;
        const string ThumbnailHandler = "IZWebFileManagerThumbnailHandler.ashx";

        Hashtable _fileExtensions;
        Hashtable _specialFolderPaths;
        ArrayList _hiddenFilesArray;
        ArrayList _prohibitedFilesArray;
        readonly IList<FileType> _fileTypes = new List<FileType>();
        readonly IList<RootFolder> _rootFolders = new List<RootFolder>();
        readonly IList<SpecialFolder> _specialFolders = new List<SpecialFolder>();

        string _defaultFolderSmallImage;
        string _defaultFolderLargeImage;
        string _defaultFileSmallImage;
        string _defaultFileLargeImage;
        string _defaultRootFolderSmallImage;
        string _defaultRootFolderLargeImage;
        Dictionary<ToolbarImages, string> _toolbarImages;
        bool _toolbarImagesInitialized;
        bool _defailtsInitialized;

        string _callbackResult;
        string _callbackCommandArgument;
        FileManagerCommands _callbackCommand;
        FileManagerControl _callbackControl;

        private CommandOptions _commandOptions;

        #endregion

        #region Properties

        internal bool SupportThumbnails
        {
            get { return _supportThumbnails; }
        }

        public CommandOptions CommandOptions
        {
            get
            {
                if (_commandOptions == null)
                    _commandOptions = new CommandOptions();
                return _commandOptions;
            }
        }

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool AllowOverwrite
        {
            get { return ViewState["AllowOverwrite"] == null ? false : (bool)ViewState["AllowOverwrite"]; }
            set { ViewState["AllowOverwrite"] = value; }
        }

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowUpload
        {
            get { return ViewState["AllowUpload"] == null ? true : (bool)ViewState["AllowUpload"]; }
            set { ViewState["AllowUpload"] = value; }
        }

        /// <summary>
        /// When is set true, Delete, Move and Rename are not allowed, Default value it true.
        /// </summary>
        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowDelete
        {
            get { return ViewState["AllowDelete"] == null ? true : (bool)ViewState["AllowDelete"]; }
            set { ViewState["AllowDelete"] = value; }
        }

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ReadOnly
        {
            get { return ViewState["ReadOnly"] == null ? false : (bool)ViewState["ReadOnly"]; }
            set { ViewState["ReadOnly"] = value; }
        }

        [DefaultValue("IZWebFileManagerResource")]
        [Category("Data")]
        public string ResourceClassKey
        {
            get { return ((ViewState["ResourceClassKey"] == null) ? "IZWebFileManagerResource" : (string)ViewState["ResourceClassKey"]); }
            set { ViewState["ResourceClassKey"] = value; }
        }

        [DefaultValue(null)]
        [Category("Appearance")]
        [Themeable(true)]
        public CultureInfo Culture
        {
            get { return ((ViewState["Culture"] == null) ? null : (CultureInfo)ViewState["Culture"]); }
            set { ViewState["Culture"] = value; }
        }

        public Boolean IsRightToLeft { get { return CurrentUICulture.TextInfo.IsRightToLeft; } }

        [Browsable(false)]
        public CultureInfo CurrentUICulture { get { return Culture == null ? CultureInfo.CurrentUICulture : Culture; } }

        [DefaultValue("")]
        [Category("Action")]
        [Themeable(false)]
        [Localizable(false)]
        public string ClientOpenItemFunction
        {
            get { return ((ViewState["ClientOpenItemFunction"] == null) ? String.Empty : (string)ViewState["ClientOpenItemFunction"]); }
            set { ViewState["ClientOpenItemFunction"] = value; }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [Themeable(false)]
        [Localizable(false)]
        public string HiddenFiles
        {
            get { return ((ViewState["HiddenFiles"] == null) ? String.Empty : (string)ViewState["HiddenFiles"]); }
            set { ViewState["HiddenFiles"] = value; }
        }

        internal ArrayList HiddenFilesArray
        {
            get
            {
                if (_hiddenFilesArray == null)
                    _hiddenFilesArray = InitExtensionsArray(HiddenFiles);
                return _hiddenFilesArray;
            }
        }

        [DefaultValue("")]
        [Category("Behavior")]
        [Themeable(false)]
        [Localizable(false)]
        public string ProhibitedFiles
        {
            get { return ((ViewState["ProhibitedFiles"] == null) ? String.Empty : (string)ViewState["ProhibitedFiles"]); }
            set { ViewState["ProhibitedFiles"] = value; }
        }

        internal ArrayList ProhibitedFilesArray
        {
            get
            {
                if (_prohibitedFilesArray == null)
                    _prohibitedFilesArray = InitExtensionsArray(ProhibitedFiles);
                return _prohibitedFilesArray;
            }
        }

        [MergableProperty(false)]
        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Localizable(false)]
        [Themeable(false)]
        public IList<RootFolder> RootFolders
        {
            get { return _rootFolders; }
        }

        [MergableProperty(false)]
        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Localizable(false)]
        [Themeable(false)]
        public IList<SpecialFolder> SpecialFolders
        {
            get { return _specialFolders; }
        }

        [MergableProperty(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Behavior")]
        [Localizable(false)]
        [Themeable(false)]
        public IList<FileType> FileTypes
        {
            get { return _fileTypes; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string FileSmallImageUrl
        {
            get { return ViewState["FileSmallIconUrl"] == null ? String.Empty : (string)ViewState["FileSmallIconUrl"]; }
            set { ViewState["FileSmallIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string FileLargeImageUrl
        {
            get { return ViewState["FileLargeIconUrl"] == null ? String.Empty : (string)ViewState["FileLargeIconUrl"]; }
            set { ViewState["FileLargeIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string FolderSmallImageUrl
        {
            get { return ViewState["FolderSmallIconUrl"] == null ? String.Empty : (string)ViewState["FolderSmallIconUrl"]; }
            set { ViewState["FolderSmallIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string FolderLargeImageUrl
        {
            get { return ViewState["FolderLargeIconUrl"] == null ? String.Empty : (string)ViewState["FolderLargeIconUrl"]; }
            set { ViewState["FolderLargeIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string RootFolderSmallImageUrl
        {
            get { return ViewState["RootFolderSmallIconUrl"] == null ? String.Empty : (string)ViewState["RootFolderSmallIconUrl"]; }
            set { ViewState["RootFolderSmallIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string RootFolderLargeImageUrl
        {
            get { return ViewState["RootFolderLargeIconUrl"] == null ? String.Empty : (string)ViewState["RootFolderLargeIconUrl"]; }
            set { ViewState["RootFolderLargeIconUrl"] = value; }
        }

        [DefaultValue("")]
        [Bindable(true)]
        public string ImagesFolder
        {
            get { return ViewState["IconsFolder"] == null ? String.Empty : (string)ViewState["IconsFolder"]; }
            set { ViewState["IconsFolder"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string CopyImageUrl
        {
            get { return ViewState["CopyIconUrl"] == null ? String.Empty : (string)ViewState["CopyIconUrl"]; }
            set { ViewState["CopyIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string MoveImageUrl
        {
            get { return ViewState["MoveIconUrl"] == null ? String.Empty : (string)ViewState["MoveIconUrl"]; }
            set { ViewState["MoveIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string DeleteImageUrl
        {
            get { return ViewState["DeleteIconUrl"] == null ? String.Empty : (string)ViewState["DeleteIconUrl"]; }
            set { ViewState["DeleteIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string RenameImageUrl
        {
            get { return ViewState["RenameIconUrl"] == null ? String.Empty : (string)ViewState["RenameIconUrl"]; }
            set { ViewState["RenameIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string FolderUpImageUrl
        {
            get { return ViewState["FolderUpIconUrl"] == null ? String.Empty : (string)ViewState["FolderUpIconUrl"]; }
            set { ViewState["FolderUpIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string NewFolderImageUrl
        {
            get { return ViewState["NewFolderImageUrl"] == null ? String.Empty : (string)ViewState["NewFolderImageUrl"]; }
            set { ViewState["NewFolderImageUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string ViewImageUrl
        {
            get { return ViewState["ViewIconUrl"] == null ? String.Empty : (string)ViewState["ViewIconUrl"]; }
            set { ViewState["ViewIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string ProcessImageUrl
        {
            get { return ViewState["ProcessIconUrl"] == null ? String.Empty : (string)ViewState["ProcessIconUrl"]; }
            set { ViewState["ProcessIconUrl"] = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        public string RefreshImageUrl
        {
            get { return ViewState["RefreshImageUrl"] == null ? String.Empty : (string)ViewState["RefreshImageUrl"]; }
            set { ViewState["RefreshImageUrl"] = value; }
        }

        #endregion

        static FileManagerController()
        {
            _imageExtension = new Hashtable(StringComparer.OrdinalIgnoreCase);
            object o = new object();
            _imageExtension[".gif"] = o;
            _imageExtension[".jpg"] = o;
            _imageExtension[".jpeg"] = o;
            _imageExtension[".png"] = o;

            HttpHandlerActionCollection handlers = ((HttpHandlersSection)WebConfigurationManager.GetSection("system.web/httpHandlers")).Handlers;
            foreach (HttpHandlerAction action in handlers)
                if (String.Compare(action.Path, ThumbnailHandler, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _supportThumbnails = true;
                    break;
                }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            ((LinkMePage) Page).AddJavaScriptReference(JavaScripts.FileManagerController);
            ((LinkMePage)Page).AddJavaScriptReference(JavaScripts.BorderedPanel);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.ClientScript.RegisterClientScriptBlock(typeof(FileManagerController), "WebFileManager_DoCallback", GetDoCallbackScript(), true);
            Page.ClientScript.RegisterStartupScript(typeof(FileManagerController), ClientID, GetInitInstanceScript(), true);

            RegisterResources();

            EnsureDefaults();
        }

        internal void EnsureToolbarImages()
        {
            if (_toolbarImagesInitialized)
                return;
            _toolbarImagesInitialized = true;
            InitToolbarImages();
        }

        private void InitToolbarImages()
        {
            bool isImagesFolder = ImagesFolder.Length > 0;
            string imagesFolder = EnsureEndsWithSlash(ImagesFolder);
            _toolbarImages = new Dictionary<ToolbarImages, string>();

            // Copy icon
            if (CopyImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Copy] = ResolveUrl(CopyImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Copy] = ResolveUrl(imagesFolder + "Copy.gif");
            else
                _toolbarImages[ToolbarImages.Copy] = new ApplicationUrl("~/ui/images/controls/FileManager/Copy.gif").ToString();

            // Delete icon
            if (DeleteImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Delete] = ResolveUrl(DeleteImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Delete] = ResolveUrl(imagesFolder + "Delete.gif");
            else
                _toolbarImages[ToolbarImages.Delete] = new ApplicationUrl("~/ui/images/controls/FileManager/Delete.gif").ToString();

            // Move icon
            if (MoveImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Move] = ResolveUrl(MoveImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Move] = ResolveUrl(imagesFolder + "Move.gif");
            else
                _toolbarImages[ToolbarImages.Move] = new ApplicationUrl("~/ui/images/controls/FileManager/Move.gif").ToString();

            // Rename icon
            if (RenameImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Rename] = ResolveUrl(RenameImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Rename] = ResolveUrl(imagesFolder + "Rename.gif");
            else
                _toolbarImages[ToolbarImages.Rename] = new ApplicationUrl("~/ui/images/controls/FileManager/Rename.gif").ToString();

            // Rename icon
            if (NewFolderImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.NewFolder] = ResolveUrl(NewFolderImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.NewFolder] = ResolveUrl(imagesFolder + "NewFolder.gif");
            else
                _toolbarImages[ToolbarImages.NewFolder] = new ApplicationUrl("~/ui/images/controls/FileManager/NewFolder.gif").ToString();

            // View icon
            if (ViewImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.View] = ResolveUrl(ViewImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.View] = ResolveUrl(imagesFolder + "View.gif");
            else
                _toolbarImages[ToolbarImages.View] = new ApplicationUrl("~/ui/images/controls/FileManager/View.gif").ToString();

            // FolderUp icon
            if (FolderUpImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.FolderUp] = ResolveUrl(FolderUpImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.FolderUp] = ResolveUrl(imagesFolder + "FolderUp.gif");
            else
                _toolbarImages[ToolbarImages.FolderUp] = new ApplicationUrl("~/ui/images/controls/FileManager/FolderUp.gif").ToString();

            // Process icon
            if (ProcessImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Process] = ResolveUrl(ProcessImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Process] = ResolveUrl(imagesFolder + "Process.gif");
            else
                _toolbarImages[ToolbarImages.Process] = new ApplicationUrl("~/ui/images/controls/FileManager/Process.gif").ToString();

            // Refresh icon
            if (RefreshImageUrl.Length > 0)
                _toolbarImages[ToolbarImages.Refresh] = ResolveUrl(RefreshImageUrl);
            else if (isImagesFolder)
                _toolbarImages[ToolbarImages.Refresh] = ResolveUrl(imagesFolder + "Refresh.gif");
            else
                _toolbarImages[ToolbarImages.Refresh] = new ApplicationUrl("~/ui/images/controls/FileManager/Refresh.gif").ToString();
        }

        internal void EnsureDefaults()
        {
            if (_defailtsInitialized)
                return;
            _defailtsInitialized = true;
            InitDefaults();
        }

        private void InitDefaults()
        {
            bool isImagesFolder = ImagesFolder.Length > 0;
            string imagesFolder = EnsureEndsWithSlash(ImagesFolder);

            if (RootFolderSmallImageUrl.Length > 0)
                _defaultRootFolderSmallImage = ResolveUrl(RootFolderSmallImageUrl);
            else if (isImagesFolder)
                _defaultRootFolderSmallImage = ResolveUrl(imagesFolder + "RootFolderSmall.gif");
            else
                _defaultRootFolderSmallImage = new ApplicationUrl("~/ui/images/controls/FileManager/RootFolderSmall.gif").ToString();

            if (RootFolderLargeImageUrl.Length > 0)
                _defaultRootFolderLargeImage = ResolveUrl(RootFolderLargeImageUrl);
            else if (isImagesFolder)
                _defaultRootFolderLargeImage = ResolveUrl(imagesFolder + "RootFolderLarge.gif");
            else
                _defaultRootFolderLargeImage = new ApplicationUrl("~/ui/images/controls/FileManager/RootFolderLarge.gif").ToString();

            if (FolderSmallImageUrl.Length > 0)
                _defaultFolderSmallImage = ResolveUrl(FolderSmallImageUrl);
            else if (isImagesFolder)
                _defaultFolderSmallImage = ResolveUrl(imagesFolder + "FolderSmall.gif");
            else
                _defaultFolderSmallImage = new ApplicationUrl("~/ui/images/controls/FileManager/FolderSmall.gif").ToString();

            if (FolderLargeImageUrl.Length > 0)
                _defaultFolderLargeImage = ResolveUrl(FolderLargeImageUrl);
            else if (isImagesFolder)
                _defaultFolderLargeImage = ResolveUrl(imagesFolder + "FolderLarge.gif");
            else
                _defaultFolderLargeImage = new ApplicationUrl("~/ui/images/controls/FileManager/FolderLarge.gif").ToString();

            if (FileSmallImageUrl.Length > 0)
                _defaultFileSmallImage = ResolveUrl(FileSmallImageUrl);
            else if (isImagesFolder)
                _defaultFileSmallImage = ResolveUrl(imagesFolder + "FileSmall.gif");
            else
                _defaultFileSmallImage = new ApplicationUrl("~/ui/images/controls/FileManager/FileSmall.gif").ToString();

            if (FileLargeImageUrl.Length > 0)
                _defaultFileLargeImage = ResolveUrl(FileLargeImageUrl);
            else if (isImagesFolder)
                _defaultFileLargeImage = ResolveUrl(imagesFolder + "FileLarge.gif");
            else
                _defaultFileLargeImage = new ApplicationUrl("~/ui/images/controls/FileManager/FileLarge.gif").ToString();
        }

        string EnsureEndsWithSlash(string path)
        {
            if (!path.EndsWith("/"))
                return path + "/";
            return path;
        }

        private void InitFileTypes()
        {
            _fileExtensions = new Hashtable(StringComparer.OrdinalIgnoreCase);

            foreach (FileType fileType in FileTypes)
            {
                string[] exts = fileType.Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ext in exts)
                {
                    _fileExtensions[ext.Trim().Trim('.')] = fileType;
                }
            }
        }

        private void InitSpecialFolders()
        {
            _specialFolderPaths = new Hashtable(StringComparer.OrdinalIgnoreCase);

            foreach (SpecialFolder folder in SpecialFolders)
            {
                if (folder.Path.Length > 0)
                {
                    string path = Page.MapPath(folder.Path);
                    _specialFolderPaths[path] = folder;
                    if (path.EndsWith("/"))
                        path.TrimEnd('/');
                    else
                        path = path + "/";
                    _specialFolderPaths[path] = folder;
                }
            }
        }

        private static ArrayList InitExtensionsArray(string extList)
        {
            string[] exts = extList.Split(',');
            ArrayList array = new ArrayList();
            foreach (string ext in exts)
            {
                array.Add(ext.Trim().ToLower(CultureInfo.InvariantCulture).TrimStart('.'));
            }
            return array;
        }

        internal FileType GetFileType(FileInfo file)
        {
            if (_fileExtensions == null)
                InitFileTypes();
            string ext = file.Extension.TrimStart('.');
            return (FileType)_fileExtensions[ext];
        }

        public string GetResourceString(string name, string defaultValue)
        {
            try
            {
                string value = (string)HttpContext.GetGlobalResourceObject(ResourceClassKey, name, CurrentUICulture);
                if (value == null)
                    return defaultValue;
                return value;
            }
            catch (MissingManifestResourceException)
            {
                return defaultValue;
            }
        }

        internal void AddDirectionAttribute(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Dir, IsRightToLeft ? "rtl" : "ltr");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Direction, IsRightToLeft ? "rtl" : "ltr");
        }

        internal const string ClientScriptObjectNamePrefix = "WFM_";
        private string GetInitInstanceScript()
        {
            return "var " + ClientScriptObjectNamePrefix + ClientID + "=new FileManagerController('" + ClientID + "','" + UniqueID + "','" + EventArgumentSplitter + "');\r\n";
        }

        private string GetDoCallbackScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function WebFileManager_DoCallback(target, argument, clientCallback, context, clientErrorCallback) {");
            sb.AppendLine(Page.ClientScript.GetCallbackEventReference("target", "argument", "clientCallback", "context", "clientErrorCallback", false) + ";");
            sb.AppendLine("}");

            return sb.ToString();
        }

        internal string GetCommandEventReference(Control control, string command, string arg)
        {
            return ClientScriptObjectNamePrefix + ClientID + ".On" + command + "(" + ClientScriptObjectNamePrefix + control.ClientID + "," + arg + ")";
        }

        #region ICallbackEventHandler Members

        string GetCallbackResult()
        {
            EnsureDefaults();

            switch (_callbackCommand)
            {
                case FileManagerCommands.Refresh:
                    _callbackResult = _callbackControl.RenderContents();
                    break;
                case FileManagerCommands.FileViewSort:
                    _callbackResult = _callbackControl.RenderContents();
                    break;
                case FileManagerCommands.FileViewChangeView:
                    _callbackResult = _callbackControl.RenderContents();
                    break;
                case FileManagerCommands.FileViewShowInGroups:
                    _callbackResult = _callbackControl.RenderContents();
                    break;
            }

            return _callbackResult;
        }

        void RaiseCallbackEvent(string eventArgument)
        {
            if (eventArgument == null)
                return;

            // Parse eventArgument
            string[] args = eventArgument.Split(new char[] { EventArgumentSplitter }, 3);
            _callbackControl = (FileManagerControl)fileMangerControls[args[0]];
            _callbackCommand = (FileManagerCommands)Enum.Parse(typeof(FileManagerCommands), args[1]);
            _callbackCommandArgument = null;
            if (args.Length > 2)
                _callbackCommandArgument = args[2];

            switch (_callbackCommand)
            {
                case FileManagerCommands.Refresh:
                    break;
                case FileManagerCommands.FileViewSort:
                    break;
                case FileManagerCommands.FileViewChangeView:
                    break;
                case FileManagerCommands.FileViewShowInGroups:
                    break;
                case FileManagerCommands.FileViewNavigate:
                    _callbackResult = ProcessFileViewNavigate(_callbackControl.ResolveFileManagerItemInfo(DecodeURIComponent(_callbackCommandArgument)));
                    break;
                case FileManagerCommands.ExecuteCommand:
                    string[] inds = _callbackCommandArgument.Split(':');
                    _callbackResult = ProcessExecuteCommand(_callbackControl.SelectedItems, int.Parse(inds[0], null), int.Parse(inds[1], null));
                    break;
                default:
                    if (ReadOnly)
                        _callbackResult = String.Empty;
                    else
                    {
                        switch (_callbackCommand)
                        {
                            case FileManagerCommands.Rename:
                                if (!AllowDelete)
                                {
                                    _callbackResult = String.Empty;
                                    break;
                                }
                                string[] paths = _callbackCommandArgument.Split(EventArgumentSplitter);
                                _callbackResult = ProcessRename(_callbackControl.ResolveFileManagerItemInfo(DecodeURIComponent(paths[0])), DecodeURIComponent(paths[1]));
                                break;
                            case FileManagerCommands.NewFolder:
                                _callbackResult = ProcessNewFolder(_callbackControl.GetCurrentDirectory());
                                break;
                            case FileManagerCommands.SelectedItemsDelete:
                                if (!AllowDelete)
                                {
                                    _callbackResult = String.Empty;
                                    break;
                                }
                                EvaluateSelectedItemsActionCommand(LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Delete);
                                break;
                            case FileManagerCommands.SelectedItemsCopyTo:
                                EvaluateSelectedItemsActionCommand(LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Copy);
                                break;
                            case FileManagerCommands.SelectedItemsMoveTo:
                                if (!AllowDelete)
                                {
                                    _callbackResult = String.Empty;
                                    break;
                                }
                                EvaluateSelectedItemsActionCommand(LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Move);
                                break;
                            default:
                                String message = "Not Implemented Command \"{0}\"";
                                throw new ArgumentException(String.Format(null, message, _callbackCommand));
                        }
                    }
                    break;
            }
        }

        private void EvaluateSelectedItemsActionCommand(SelectedItemsAction action)
        {
            _callbackResult = ProcessSelectedItemsAction(_callbackControl.CurrentDirectory, _callbackControl.ResolveFileManagerItemInfo(DecodeURIComponent(_callbackCommandArgument)), _callbackControl.SelectedItems, action);
        }

        private string ProcessSelectedItemsAction(FileManagerItemInfo srcDir, FileManagerItemInfo destDir, FileManagerItemInfo[] items, SelectedItemsAction action)
        {
            if (items.Length == 0)
                return "";

            SelectedItemsActionCancelEventArgs cancelArg = new SelectedItemsActionCancelEventArgs(action);
            foreach (FileManagerItemInfo item in items)
            {
                cancelArg.SelectedItems.Add(item);
            }
            cancelArg.DestinationDirectory = destDir;
            OnSelectedItemsAction(cancelArg);
            if (cancelArg.Cancel)
            {
                return ClientMessageEventReference(cancelArg.ClientMessage);
            }

            StringBuilder sb = new StringBuilder();

            switch (action)
            {
                case LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Delete:
                    if (ProcessSelectedItemsDelete(items))
                        AddFolderTreeRefreshEventReference(sb, srcDir);
                    break;
                case LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Move:
                    destDir.EnsureDirectoryExists();
                    if (ProcessSelectedItemsMoveTo(destDir, items))
                    {
                        AddFolderTreeRequireRefreshEventReference(sb, srcDir, destDir);
                        AddFolderTreeNavigateEventReference(sb, srcDir);
                    }
                    break;
                case LinkMe.Web.UI.Controls.Common.FileManager.SelectedItemsAction.Copy:
                    destDir.EnsureDirectoryExists();
                    if (ProcessSelectedItemsCopyTo(destDir, items))
                    {
                        AddFolderTreeRequireRefreshEventReference(sb, srcDir, destDir);
                        AddFolderTreeNavigateEventReference(sb, srcDir);
                    }
                    break;
            }


            SelectedItemsActionEventArgs args = new SelectedItemsActionEventArgs();
            OnSelectedItemsActionComplete(args);

            sb.AppendLine(ClientRefreshEventReference);
            return sb.ToString();

        }

        private bool ProcessSelectedItemsMoveTo(FileManagerItemInfo destDir, FileManagerItemInfo[] items)
        {
            bool b = false;
            foreach (FileManagerItemInfo item in items)
            {
                if (item.Directory.Exists)
                {
                    b = true;
                    DirectoryInfo dir = item.Directory;
                    if (String.Compare(dir.Parent.FullName.TrimEnd(Path.DirectorySeparatorChar), destDir.PhysicalPath.TrimEnd(Path.DirectorySeparatorChar), true) == 0)
                        continue;

                    string newName;
                    if (AllowOverwrite)
                        newName = dir.Name;
                    else
                        newName = GetNotDuplicatedFolderName(destDir, dir.Name);
                    string newPath = Path.Combine(destDir.PhysicalPath, newName);
                    xDirectory.Copy(dir.FullName, newPath, true);
                    dir.Delete(true);
                }
                else if (item.File.Exists)
                {
                    FileInfo file = item.File;
                    if (String.Compare(file.Directory.FullName.TrimEnd(Path.DirectorySeparatorChar), destDir.PhysicalPath.TrimEnd(Path.DirectorySeparatorChar), true) == 0)
                        continue;

                    string newName;
                    if (AllowOverwrite)
                        newName = file.Name;
                    else
                        newName = GetNotDuplicatedFileName(destDir, Path.GetFileNameWithoutExtension(file.FullName), file.Extension);
                    string newPath = Path.Combine(destDir.PhysicalPath, newName);
                    file.CopyTo(newPath, AllowOverwrite);
                    file.Delete();
                }
            }
            return b;
        }

        private bool ProcessSelectedItemsCopyTo(FileManagerItemInfo destDir, FileManagerItemInfo[] items)
        {
            bool b = false;
            foreach (FileManagerItemInfo item in items)
            {
                if (item.Directory.Exists)
                {
                    b = true;
                    DirectoryInfo dir = item.Directory;
                    string newName;
                    if (AllowOverwrite && String.Compare(dir.Parent.FullName.TrimEnd(Path.DirectorySeparatorChar), destDir.PhysicalPath.TrimEnd(Path.DirectorySeparatorChar), true) != 0)
                        newName = dir.Name;
                    else
                        newName = GetNotDuplicatedFolderName(destDir, dir.Name);
                    string newPath = Path.Combine(destDir.PhysicalPath, newName);
                    xDirectory.Copy(dir.FullName, newPath, true);
                }
                else if (item.File.Exists)
                {
                    FileInfo file = item.File;
                    string newName;
                    if (AllowOverwrite && String.Compare(file.Directory.FullName.TrimEnd(Path.DirectorySeparatorChar), destDir.PhysicalPath.TrimEnd(Path.DirectorySeparatorChar), true) != 0)
                        newName = file.Name;
                    else
                        newName = GetNotDuplicatedFileName(destDir, Path.GetFileNameWithoutExtension(file.FullName), file.Extension);
                    string newPath = Path.Combine(destDir.PhysicalPath, newName);
                    file.CopyTo(newPath, AllowOverwrite);
                }
            }
            return b;
        }

        private bool ProcessSelectedItemsDelete(FileManagerItemInfo[] items)
        {
            bool b = false;
            foreach (FileManagerItemInfo item in items)
            {
                if (item.File.Exists)
                    item.File.Delete();
                else if (item.Directory.Exists)
                {
                    b = true;
                    item.Directory.Delete(true);
                }
            }
            return b;
        }

        private String ClientRefreshEventReference { get { return ClientScriptObjectNamePrefix + ClientID + ".OnRefresh(context,null)"; } }
        private string ClientMessageEventReference(string message)
        {
            string clientMessage = message;
            if (clientMessage == null || clientMessage.Length == 0)
                clientMessage = GetResourceString("CannotCompleteOperation", "Cannot Complete Operation.");

            return "alert(decodeURIComponent('" + EncodeURIComponent(clientMessage) + "'))";
        }

        private string ProcessRename(FileManagerItemInfo fileManagerItemInfo, string newName)
        {
            RenameCancelEventArgs cancelArg = new RenameCancelEventArgs();
            cancelArg.FileManagerItem = fileManagerItemInfo;
            cancelArg.NewName = newName;
            OnItemRenaming(cancelArg);
            if (cancelArg.Cancel)
            {
                return ClientMessageEventReference(cancelArg.ClientMessage);
            }

            if (cancelArg.NewName == null || cancelArg.NewName.Length == 0 || cancelArg.NewName.StartsWith("."))
            {
                return ClientMessageEventReference(GetResourceString("MustTypeFileName", "You must type a file name."));
            }

            if (!Validate(cancelArg.NewName))
            {
                return ClientMessageEventReference(GetResourceString("NotAllowedCharacters", "A file name cannot contain any of the following characters: \\/:*?\"<>|"));
            }

            if (fileManagerItemInfo.File.Exists)
            {
                string newFileExt = Path.GetExtension(cancelArg.NewName).ToLower(CultureInfo.InvariantCulture).TrimStart('.');
                if (newFileExt.Length == 0 ||
                    HiddenFilesArray.Contains(newFileExt) ||
                    ProhibitedFilesArray.Contains(newFileExt))
                    cancelArg.NewName += fileManagerItemInfo.File.Extension;
            }

            FileManagerItemInfo renamedItem = ResolveFileManagerItemInfo(fileManagerItemInfo.FileManagerPath.Substring(0, fileManagerItemInfo.FileManagerPath.LastIndexOf('/')) + "/" + cancelArg.NewName);
            if (renamedItem.Directory.Exists || renamedItem.File.Exists)
            {
                string fileExistsMessage = GetResourceString("CannotRenameFile", "Cannot rename file: A file with the name you specified already exists. Specify a different file name.");
                return ClientMessageEventReference(fileExistsMessage);
            }

            bool b = false;
            if (fileManagerItemInfo.Directory.Exists)
            {
                b = true;
                fileManagerItemInfo.Directory.MoveTo(renamedItem.PhysicalPath);
            }
            else if (fileManagerItemInfo.File.Exists)
            {
                fileManagerItemInfo.File.MoveTo(renamedItem.PhysicalPath);
            }

            RenameEventArgs arg = new RenameEventArgs();
            arg.FileManagerItem = renamedItem;
            OnItemRenamed(arg);

            StringBuilder sb = new StringBuilder();
            if (b)
                AddFolderTreeRefreshEventReference(sb, _callbackControl.CurrentDirectory);
            sb.AppendLine(ClientRefreshEventReference);
            return sb.ToString();
        }

        private static bool Validate(string name)
        {
            // \/:*?"<>|
            return !name.Contains("\\") &&
                !name.Contains("/") &&
                !name.Contains(":") &&
                !name.Contains("*") &&
                !name.Contains("?") &&
                !name.Contains("\"") &&
                !name.Contains("<") &&
                !name.Contains(">") &&
                !name.Contains("|");

        }

        internal static string DecodeURIComponent(string str)
        {
            string res = HttpUtility.UrlDecode(str);
            return res;
        }
        internal static string EncodeURIComponent(string str)
        {
            string res = HttpUtility.UrlEncode(str);
            res = res.Replace("+", "%20");
            res = res.Replace("'", "%27");
            return res;
        }
        internal static string DecodeURI(string str)
        {
            string res = HttpUtility.UrlDecode(str);
            return res;
        }
        internal static string EncodeURI(string str)
        {
            string res = HttpUtility.UrlPathEncode(str);
            res = res.Replace("+", "%20");
            return res;
        }

        string ProcessFileViewNavigate(FileManagerItemInfo item)
        {
            if (item.Directory.Exists)
            {
                StringBuilder sb = new StringBuilder();
                AddFolderTreeNavigateEventReference(sb, item);
                sb.AppendLine("var dir = '" + EncodeURIComponent(item.FileManagerPath) + "'");
                sb.AppendLine("WebForm_GetElementById(context.ClientID+'_Directory').value = dir;");
                sb.AppendLine("var address = WebForm_GetElementById(context.ClientID+'_Address');");
                sb.AppendLine("if(address) address.value = decodeURIComponent(dir);");
                sb.AppendLine(ClientRefreshEventReference);
                return sb.ToString();
            }
            else if (item.File.Exists)
            {
                return ProcessExecuteCommand(new FileManagerItemInfo[] { item }, 0, 0);
            }
            else
                return ClientMessageEventReference(GetResourceString("CannotFindFile", "Cannot find file, Make sure the path is correct."));
        }

        internal string[] GetPathHashCodes(string fileManagerPath)
        {
            fileManagerPath = VirtualPathUtility.RemoveTrailingSlash(fileManagerPath);
            List<string> list = new List<string>();
            int index = fileManagerPath.IndexOf('/', 1);
            while (index > 0)
            {
                list.Add(GetPathHashCode(fileManagerPath.Substring(0, index)));
                index = fileManagerPath.IndexOf('/', index + 1);
            }
            list.Add(GetPathHashCode(fileManagerPath));
            return list.ToArray();
        }

        internal string GetPathHashCode(string fileManagerPath)
        {
            return VirtualPathUtility.RemoveTrailingSlash(fileManagerPath).ToLower().GetHashCode().ToString("x");
        }

        private string ProcessExecuteCommand(FileManagerItemInfo[] items, int group, int index)
        {
            if (items.Length == 0)
                return "";

            if (group == 0)
            {
                FileManagerItemInfo item = items[0];

                if (item.Directory.Exists)
                    return ProcessFileViewNavigate(item);

                // Default Command
                if (index == -1)
                    return ProcessOpenCommand(item);
                else if (index == -2)
                    return ProcessDownloadCommand(item);

                FileType ft = GetFileType(item.File);
                if (ft == null)
                    return ProcessOpenCommand(item);
            }
            else
            {
            }


            return "";
        }

        private string ProcessDownloadCommand(FileManagerItemInfo item)
        {
            String script = Page.ClientScript.GetPostBackEventReference(this, "Download:" + EncodeURIComponent(item.FileManagerPath)) + ";" +
                "theForm.__EVENTTARGET.value = '';theForm.__EVENTARGUMENT.value = '';";
            return script;
        }

        private string ProcessOpenCommand(FileManagerItemInfo item)
        {
            StringBuilder sb = new StringBuilder();

            if (ClientOpenItemFunction.Length > 0)
            {
                sb.AppendLine(ClientOpenItemFunction + "('" + EncodeURI(item.VirtualPath) + "')");
            }
            else
            {
                sb.AppendLine("var href = '" + EncodeURIComponent(item.VirtualPath) + "';");
                sb.AppendLine("window.open(decodeURIComponent(href),'_blank');");
            }
            return sb.ToString();
        }

        string ResolveAbsolutePath(ref string fileManagerPath)
        {
            Regex reg = new Regex(@"^((?<rootPath>/[^/]+)|(\[(?<rootIndex>\d+)\]))(?<dirPath>/.*)?");
            Match match = reg.Match(fileManagerPath);
            if (!match.Success)
                goto _throw;
            RootFolder rootFolder = null;
            string dirPath = match.Groups["dirPath"].Value;
            string rootPath = match.Groups["rootPath"].Value;
            string rootIndex = match.Groups["rootIndex"].Value;
            if (!String.IsNullOrEmpty(rootPath))
            {
                for (int index = 0; index < RootFolders.Count; index++)
                {
                    RootFolder folder = RootFolders[index];
                    if (String.Compare(folder.TextInternal, rootPath.Substring(1), true) == 0)
                    {
                        rootFolder = folder;
                        break;
                    }
                }
            }
            else
            {
                int index = Int32.Parse(rootIndex);
                if (index >= RootFolders.Count)
                    goto _throw;
                rootFolder = RootFolders[index];
                rootPath = "/" + rootFolder.TextInternal;
            }
            if (rootFolder == null)
                goto _throw;
            dirPath = Normalize(dirPath);
            fileManagerPath = rootPath + dirPath;
            string rootVPath = VirtualPathUtility.RemoveTrailingSlash(ResolveUrl(rootFolder.Path));
            string vPath = rootVPath + dirPath;

            return vPath;

        _throw:
            throw new ArgumentException("Invalid file manager path.");
        }

        public FileManagerItemInfo ResolveFileManagerItemInfo(string fileManagerPath)
        {
            string virtualPath = ResolveAbsolutePath(ref fileManagerPath);
            string phisicalPath = Page.MapPath(virtualPath);
            return new FileManagerItemInfo(fileManagerPath, virtualPath, phisicalPath);
        }

        internal string ProcessFileUpload(FileManagerItemInfo destDir, System.Web.HttpPostedFile uploadedFile)
        {
            string script;
            if (AllowUpload)
                ProcessFileUpload(destDir, uploadedFile, out script);
            else
                script = ClientMessageEventReference(null);
            return script;
        }

        internal bool ProcessFileUpload(FileManagerItemInfo destDir, System.Web.HttpPostedFile uploadedFile, out string script)
        {
            UploadFileCancelEventArgs cancelArg = new UploadFileCancelEventArgs();
            cancelArg.DestinationDirectory = destDir;
            cancelArg.PostedFile = uploadedFile;
            OnFileUploading(cancelArg);

            if (cancelArg.Cancel)
            {
                script = ClientMessageEventReference(cancelArg.ClientMessage);
                return false;
            }

            string saveName = cancelArg.SaveName;
            string fileNameWithoutExtension = saveName.Substring(0, saveName.LastIndexOf('.'));
            string extension = saveName.Substring(saveName.LastIndexOf('.'));

            string ext = extension.ToLower(CultureInfo.InvariantCulture).TrimStart('.');
            if (HiddenFilesArray.Contains(ext) ||
                    ProhibitedFilesArray.Contains(ext))
            {
                script = ClientMessageEventReference(null);
                return false;
            }

            string newFileName = GetNotDuplicatedFileName(destDir, fileNameWithoutExtension, extension);
            FileManagerItemInfo itemInfo = ResolveFileManagerItemInfo(VirtualPathUtility.AppendTrailingSlash(destDir.FileManagerPath) + newFileName);

            uploadedFile.SaveAs(itemInfo.PhysicalPath);

            UploadFileEventArgs arg = new UploadFileEventArgs();
            arg.UploadedFile = itemInfo;
            OnFileUploaded(arg);

            script = ClientRefreshEventReference;
            return true;
        }

        private static string GetNotDuplicatedFileName(FileManagerItemInfo destDir, string fileNameWithoutExtension, string extension)
        {
            string fileName = fileNameWithoutExtension;
            int i = 1;
            string originNewFileName = fileNameWithoutExtension;
            while (File.Exists(Path.Combine(destDir.PhysicalPath, fileName + extension)))
            {
                fileName = originNewFileName + "(" + i + ")";
                i++;
            }
            return fileName + extension;
        }

        private static string GetNotDuplicatedFolderName(FileManagerItemInfo destDir, string folderName)
        {
            string newFolderName = folderName;
            int i = 1;
            string originNewFolderName = folderName;
            while (Directory.Exists(Path.Combine(destDir.PhysicalPath, newFolderName)))
            {
                newFolderName = originNewFolderName + "(" + i + ")";
                i++;
            }
            return newFolderName;
        }

        private string ProcessNewFolder(FileManagerItemInfo destDir)
        {
            NewFolderCancelEventArgs cancelArg = new NewFolderCancelEventArgs();
            cancelArg.DestinationDirectory = destDir;
            OnNewFolderCreating(cancelArg);

            if (cancelArg.Cancel)
            {
                return ClientMessageEventReference(cancelArg.ClientMessage);
            }

            string folderName = GetResourceString("New_Folder_Name", "New Folder");

            string newFolderName = GetNotDuplicatedFolderName(destDir, folderName);

            FileManagerItemInfo itemInfo = ResolveFileManagerItemInfo(VirtualPathUtility.AppendTrailingSlash(destDir.FileManagerPath) + newFolderName);
            itemInfo.EnsureDirectoryExists();

            NewFolderEventArgs arg = new NewFolderEventArgs();
            arg.NewFolder = itemInfo;
            OnNewFolderCreated(arg);

            StringBuilder sb = new StringBuilder();
            AddFolderTreeRefreshEventReference(sb, destDir);
            sb.AppendLine(ClientRefreshEventReference);
            return sb.ToString();
        }

        void AddFolderTreeNavigateEventReference(StringBuilder sb, FileManagerItemInfo itemInfo)
        {
            sb.AppendLine("var folderTree = eval('WFM_' + context.ClientID + '_FolderTree');");
            sb.AppendLine("if(folderTree) {folderTree.Navigate (['" + String.Join("','", GetPathHashCodes(itemInfo.FileManagerPath)) + "'],0);}");
        }

        void AddFolderTreeRefreshEventReference(StringBuilder sb, FileManagerItemInfo itemInfo)
        {
            sb.AppendLine("var folderTree = eval('WFM_' + context.ClientID + '_FolderTree');");
            sb.AppendLine("if(folderTree) {folderTree.Refresh ('" + GetPathHashCode(itemInfo.FileManagerPath) + "');}");
        }

        void AddFolderTreeRequireRefreshEventReference(StringBuilder sb, FileManagerItemInfo srcInfo, FileManagerItemInfo destInfo)
        {
            sb.AppendLine("var folderTree = eval('WFM_' + context.ClientID + '_FolderTree');");
            sb.AppendLine("if(folderTree) {folderTree.RequireRefresh (['" + GetPathHashCode(srcInfo.FileManagerPath) + "','" + String.Join("','", GetPathHashCodes(destInfo.FileManagerPath)) + "']);}");
        }

        #endregion


        Hashtable fileMangerControls = new Hashtable();
        internal void RegisterComponent(FileManagerControl control)
        {
            fileMangerControls[control.ClientID] = control;
        }

        internal const char EventArgumentSplitter = ':';

        internal string GetItemSmallImage(FileSystemInfo fsi)
        {
            DirectoryInfo dir = fsi as DirectoryInfo;
            if (dir != null)
                return GetFolderSmallImage(dir);
            else
                return GetFileSmallImage((FileInfo)fsi);
        }

        SpecialFolder GetSpecialFolder(DirectoryInfo dir)
        {
            if (_specialFolderPaths == null)
                InitSpecialFolders();
            return (SpecialFolder)_specialFolderPaths[dir.FullName];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dir")]
        internal string GetFolderSmallImage(DirectoryInfo dir)
        {
            if (dir == null)
                return _defaultFolderSmallImage;
            SpecialFolder folder = GetSpecialFolder(dir);
            if (folder != null && folder.SmallImageUrl != null)
                return folder.SmallImageUrl.ToString();
            return _defaultFolderSmallImage;
        }

        internal string GetFolderSmallImage()
        {
            return GetFolderSmallImage(null);
        }

        internal string GetFileSmallImage()
        {
            return GetFileSmallImage(null);
        }

        internal string GetFileSmallImage(FileInfo file)
        {
            if (file == null)
                return _defaultFileSmallImage;
            FileType fileType = GetFileType(file);
            if (fileType != null && fileType.SmallImageUrl != null)
                return fileType.SmallImageUrl.ToString();
            return _defaultFileSmallImage;
        }

        internal string GetItemLargeImage(FileSystemInfo fsi)
        {
            DirectoryInfo dir = fsi as DirectoryInfo;
            if (dir != null)
                return GetFolderLargeImage(dir);
            else
                return GetFileLargeImage((FileInfo)fsi);
        }

        internal string GetItemThumbnailImage(FileSystemInfo fsi, FileManagerItemInfo currentDirectory)
        {
            FileInfo file = fsi as FileInfo;
            if (file == null)
                return GetFolderLargeImage((DirectoryInfo)fsi);

            if (IsImage(file))
                return ResolveUrl("~/" + ThumbnailHandler + "?" + HttpUtility.UrlEncode(currentDirectory.VirtualPath + "/" + file.Name));

            return GetFileLargeImage(file);
        }

        bool IsImage(FileInfo file)
        {
            return _imageExtension[file.Extension] != null;
        }

        private string GetFileLargeImage(FileInfo file)
        {
            if (file == null)
                return _defaultFileLargeImage;
            FileType fileType = GetFileType(file);
            if (fileType != null && fileType.LargeImageUrl != null)
                return fileType.LargeImageUrl.ToString();
            return _defaultFileLargeImage;
        }
        private string GetFileLargeImage()
        {
            return GetFileLargeImage(null);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dir")]
        private string GetFolderLargeImage(DirectoryInfo dir)
        {
            if (dir == null)
                return _defaultFolderLargeImage;
            SpecialFolder folder = GetSpecialFolder(dir);
            if (folder != null && folder.LargeImageUrl != null)
                return folder.LargeImageUrl.ToString();
            return _defaultFolderLargeImage;
        }

        internal string GetRootFolderSmallImage()
        {
            return GetRootFolderSmallImage(null);
        }

        internal string GetRootFolderSmallImage(RootFolder rootFolder)
        {
            if (rootFolder == null)
                return _defaultRootFolderSmallImage;
            if (rootFolder.SmallImageUrl != null)
                return rootFolder.SmallImageUrl.ToString();
            return _defaultRootFolderSmallImage;
        }

        internal string GetToolbarImage(ToolbarImages image)
        {
            EnsureToolbarImages();
            return _toolbarImages[image];
        }

        internal void RegisterResources()
        {
            string initScript = "var " + ClientScriptObjectNamePrefix + ClientID + "DeleteConfirm" + "='" + EncodeURIComponent(GetResourceString("DeleteConfirm", "Are you sure you want to delete selected items?")) + "';\r\n" +
                "var " + ClientScriptObjectNamePrefix + ClientID + "SelectDestination" + "='" + EncodeURIComponent(GetResourceString("SelectDestination", "Select destination directory")) + "';\r\n";
            Page.ClientScript.RegisterStartupScript(typeof(FileManagerController), ClientID + "Resources", initScript, true);
        }

        #region IPostBackEventHandler Members

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            RaisePostBackEvent(eventArgument);
        }

        void RaisePostBackEvent(string eventArgument)
        {
            string[] args = eventArgument.Split(new char[] { ':' }, 2);
            switch (args[0])
            {
                case "Download":
                    string file = this.ResolveFileManagerItemInfo(DecodeURIComponent(args[1])).PhysicalPath;
                    if (File.Exists(file))
                    {
                        Page.Response.Clear();
                        Page.Response.ContentType = "application/octet-stream";
                        Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + EncodeURIComponent(Path.GetFileName(file)));
                        Page.Response.Flush();
                        Page.Response.WriteFile(file);
                    }
                    Page.Response.End();
                    break;
            }
        }

        #endregion

        #region ICallbackEventHandler Members

        string ICallbackEventHandler.GetCallbackResult()
        {
            return GetCallbackResult();
        }

        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            RaiseCallbackEvent(eventArgument);
        }

        #endregion

        static char[] path_sep = { '/' };

        static string Normalize(string path)
        {

            if (path.Length == 1) // '/' 
                return path;

            path = Canonize(path);

            int dotPos = path.IndexOf('.');
            while (dotPos >= 0)
            {
                if (++dotPos == path.Length)
                    break;

                char nextChar = path[dotPos];

                if ((nextChar == '/') || (nextChar == '.'))
                    break;

                dotPos = path.IndexOf('.', dotPos);
            }

            if (dotPos < 0)
                return path;

            bool ends_with_slash = false;

            if (path[path.Length - 1] == '/')
                ends_with_slash = true;

            string[] parts = path.Split(path_sep, StringSplitOptions.RemoveEmptyEntries);
            int end = parts.Length;

            int dest = 0;

            for (int i = 0; i < end; i++)
            {
                string current = parts[i];
                if (current == ".")
                    continue;

                if (current == "..")
                {
                    dest--;

                    if (dest < 0)
                        dest = 0;
                    continue;
                }

                parts[dest] = current;

                dest++;
            }

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < dest; i++)
            {
                str.Append('/');
                str.Append(parts[i]);
            }

            if (str.Length > 0)
            {
                if (ends_with_slash)
                    str.Append('/');
            }
            else
            {
                return "/";
            }

            return str.ToString();
        }

        static string Canonize(string path)
        {
            int index = -1;
            for (int i = 0; i < path.Length; i++)
            {
                if ((path[i] == '\\') || (path[i] == '/' && (i + 1) < path.Length && (path[i + 1] == '/' || path[i + 1] == '\\')))
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
                return path;

            StringBuilder sb = new StringBuilder(path.Length);
            sb.Append(path, 0, index);

            for (int i = index; i < path.Length; i++)
            {
                if (path[i] == '\\' || path[i] == '/')
                {
                    int next = i + 1;
                    if (next < path.Length && (path[next] == '\\' || path[next] == '/'))
                        continue;
                    sb.Append('/');
                }
                else
                {
                    sb.Append(path[i]);
                }
            }

            return sb.ToString();
        }

        #region Events

        [Category("Action")]
        public event EventHandler<RenameCancelEventArgs> ItemRenaming;

        [Category("Action")]
        public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;

        [Category("Action")]
        public event EventHandler<RenameEventArgs> ItemRenamed;

        [Category("Action")]
        public event EventHandler<NewFolderCancelEventArgs> NewFolderCreating;

        [Category("Action")]
        public event EventHandler<NewFolderEventArgs> NewFolderCreated;

        [Category("Action")]
        public event EventHandler<UploadFileCancelEventArgs> FileUploading;

        [Category("Action")]
        public event EventHandler<UploadFileEventArgs> FileUploaded;

        [Category("Action")]
        public event EventHandler<SelectedItemsActionCancelEventArgs> SelectedItemsAction;

        [Category("Action")]
        public event EventHandler<SelectedItemsActionEventArgs> SelectedItemsActionComplete;

        #endregion

        #region OnEvent Methods

        private void OnItemRenaming(RenameCancelEventArgs e)
        {
            if (ItemRenaming != null)
                ItemRenaming(this, e);
        }

        private void OnItemRenamed(RenameEventArgs e)
        {
            if (ItemRenamed != null)
                ItemRenamed(this, e);
        }

        private void OnNewFolderCreating(NewFolderCancelEventArgs e)
        {
            if (NewFolderCreating != null)
                NewFolderCreating(this, e);
        }

        private void OnNewFolderCreated(NewFolderEventArgs e)
        {
            if (NewFolderCreated != null)
                NewFolderCreated(this, e);
        }

        private void OnFileUploading(UploadFileCancelEventArgs e)
        {
            if (FileUploading != null)
                FileUploading(this, e);
        }

        private void OnFileUploaded(UploadFileEventArgs e)
        {
            if (FileUploaded != null)
                FileUploaded(this, e);
        }

        private void OnSelectedItemsAction(SelectedItemsActionCancelEventArgs e)
        {
            if (SelectedItemsAction != null)
                SelectedItemsAction(this, e);
        }

        private void OnSelectedItemsActionComplete(SelectedItemsActionEventArgs e)
        {
            if (SelectedItemsActionComplete != null)
                SelectedItemsActionComplete(this, e);
        }

        private void OnExecuteCommand(ExecuteCommandEventArgs e)
        {
            if (ExecuteCommand != null)
                ExecuteCommand(this, e);
        }

        #endregion
    }
}

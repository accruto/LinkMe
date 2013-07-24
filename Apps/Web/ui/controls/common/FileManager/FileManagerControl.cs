using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    public abstract partial class FileManagerControl : WebControl, INamingContainer
    {
        #region Constructors

        public FileManagerControl() { }
        public FileManagerControl(FileManagerController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            _controller = controller;
        }

        #endregion

        #region Fields

        FileManagerController _controller;
        static readonly Unit _defaultWidth = Unit.Pixel(600);
        static readonly Unit _defaultHeight = Unit.Pixel(400);
        static readonly Unit _defaultBorderWidth = Unit.Pixel(1);
        static readonly BorderStyle _defaultBorderStyle = BorderStyle.Solid;
        static readonly Color _defaultBorderColor = Color.FromArgb(0xACA899);
        static readonly Color _defaultBackColor = Color.White;
        static readonly Color _defaultForeColor = Color.Black;

        #endregion

        #region Properties

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool AllowOverwrite
        {
            get { return Controller.AllowOverwrite; }
            set { Controller.AllowOverwrite = value; }
        }

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowUpload
        {
            get { return Controller.AllowUpload; }
            set { Controller.AllowUpload = value; }
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
            get { return Controller.AllowDelete; }
            set { Controller.AllowDelete = value; }
        }

        [Themeable(false)]
        [Localizable(false)]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ReadOnly
        {
            get { return Controller.ReadOnly; }
            set { Controller.ReadOnly = value; }
        }

        [Browsable(false)]
        public FileManagerController Controller
        {
            get
            {
                EnsureChildControls();
                return _controller;
            }
        }

        [DefaultValue("600px")]
        public override Unit Width
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.Width;
                return _defaultWidth;
            }
            set { base.Width = value; }
        }

        [DefaultValue("400px")]
        public override Unit Height
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.Height;
                return _defaultHeight;
            }
            set { base.Height = value; }
        }

        [DefaultValue("1px")]
        public override Unit BorderWidth
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.BorderWidth;
                return _defaultBorderWidth;
            }
            set { base.BorderWidth = value; }
        }

        [DefaultValue("0xACA899")]
        public override Color BorderColor
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.BorderColor;
                return _defaultBorderColor;
            }
            set { base.BorderColor = value; }
        }

        [DefaultValue(BorderStyle.Solid)]
        public override BorderStyle BorderStyle
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.BorderStyle;
                return _defaultBorderStyle;
            }
            set { base.BorderStyle = value; }
        }

        [DefaultValue("White")]
        public override Color BackColor
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.BackColor;
                return _defaultBackColor;
            }
            set { base.BackColor = value; }
        }

        [DefaultValue("Black")]
        public override Color ForeColor
        {
            get
            {
                if (ControlStyleCreated)
                    return ControlStyle.ForeColor;
                return _defaultForeColor;
            }
            set { base.ForeColor = value; }
        }

        #endregion

        #region InitControllerEvents

        private void InitControllerEvents(FileManagerController controller)
        {
            controller.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(controller_ExecuteCommand);
            controller.FileUploaded += new EventHandler<UploadFileEventArgs>(controller_FileUploaded);
            controller.FileUploading += new EventHandler<UploadFileCancelEventArgs>(controller_FileUploading);
            controller.ItemRenamed += new EventHandler<RenameEventArgs>(controller_ItemRenamed);
            controller.ItemRenaming += new EventHandler<RenameCancelEventArgs>(controller_ItemRenaming);
            controller.NewFolderCreated += new EventHandler<NewFolderEventArgs>(controller_NewFolderCreated);
            controller.NewFolderCreating += new EventHandler<NewFolderCancelEventArgs>(controller_NewFolderCreating);
            controller.SelectedItemsActionComplete += new EventHandler<SelectedItemsActionEventArgs>(controller_SelectedItemsActionComplete);
            controller.SelectedItemsAction += new EventHandler<SelectedItemsActionCancelEventArgs>(controller_SelectedItemsAction);
        }

        void controller_SelectedItemsAction(object sender, SelectedItemsActionCancelEventArgs e)
        {
            if (SelectedItemsAction != null)
                SelectedItemsAction(this, e);
        }

        void controller_SelectedItemsActionComplete(object sender, SelectedItemsActionEventArgs e)
        {
            if (SelectedItemsActionComplete != null)
                SelectedItemsActionComplete(this, e);
        }

        void controller_NewFolderCreating(object sender, NewFolderCancelEventArgs e)
        {
            if (NewFolderCreating != null)
                NewFolderCreating(this, e);
        }

        void controller_NewFolderCreated(object sender, NewFolderEventArgs e)
        {
            if (NewFolderCreated != null)
                NewFolderCreated(this, e);
        }

        void controller_ItemRenaming(object sender, RenameCancelEventArgs e)
        {
            if (ItemRenaming != null)
                ItemRenaming(this, e);
        }

        void controller_ItemRenamed(object sender, RenameEventArgs e)
        {
            if (ItemRenamed != null)
                ItemRenamed(this, e);
        }

        void controller_FileUploading(object sender, UploadFileCancelEventArgs e)
        {
            if (FileUploading != null)
                FileUploading(this, e);
        }

        void controller_FileUploaded(object sender, UploadFileEventArgs e)
        {
            if (FileUploaded != null)
                FileUploaded(this, e);
        }

        void controller_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            if (ExecuteCommand != null)
                ExecuteCommand(this, e);
        }
        #endregion


        public virtual FileManagerItemInfo[] SelectedItems { get { return null; } }
        public virtual FileManagerItemInfo CurrentDirectory { get { return null; } }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RegisterComponent();

        }

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        protected virtual void RegisterComponent()
        {
            Controller.RegisterComponent(this);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (_controller == null)
            {
                _controller = new FileManagerController();
                _controller.ID = "Controller";
                InitControllerEvents(_controller);
                Controls.Add(_controller);
            }
        }

        #region Methods

        internal void RegisterHiddenField(string key, string value)
        {
            string id = ClientID + "_" + key;
            Page.ClientScript.RegisterHiddenField(id, value);
        }

        internal string GetValueFromHiddenField(string key)
        {
            return Page.Request.Form[ClientID + "_" + key];
        }

        protected internal virtual string RenderContents()
        {
            return null;
        }

        internal virtual FileManagerItemInfo ResolveFileManagerItemInfo(string path) { return null; }

        internal virtual FileManagerItemInfo GetCurrentDirectory() { return null; }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (!ControlStyleCreated)
                CreateControlStyle().AddAttributesToRender(writer);
        }

        protected override Style CreateControlStyle()
        {
            Style style = new Style();
            style.Font.Names = new string[] { "Tahoma", "Verdana", "Geneva", "Arial", "Helvetica", "sans-serif" };
            style.Font.Size = FontUnit.Parse("11px", null);
            style.BorderStyle = _defaultBorderStyle;
            style.BorderWidth = _defaultBorderWidth;
            style.BorderColor = _defaultBorderColor;
            style.BackColor = _defaultBackColor;
            style.ForeColor = _defaultForeColor;
            style.Width = _defaultWidth;
            style.Height = _defaultHeight;
            return style;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState == null)
                return;

            object[] state = (object[])savedState;

            base.LoadViewState(state[0]);
            if (state[1] != null)
                ((IStateManager)ControlStyle).LoadViewState(state[1]);
        }

        protected override object SaveViewState()
        {
            object[] state = new object[2];
            state[0] = base.SaveViewState();
            if (ControlStyleCreated)
                state[1] = ((IStateManager)ControlStyle).SaveViewState();

            if (state[0] != null || state[1] != null)
                return state;

            return null;
        }

        protected string GetResourceString(string name, string defaultValue)
        {
            return Controller.GetResourceString(name, defaultValue);
        }

        #endregion
        #region FileManagerController Members

        [DefaultValue("")]
        [Category("Action")]
        [Themeable(false)]
        [Localizable(false)]
        public string ClientOpenItemFunction
        {
            get { return Controller.ClientOpenItemFunction; }
            set { Controller.ClientOpenItemFunction = value; }
        }

        [DefaultValue(null)]
        [Category("Appearance")]
        [Themeable(true)]
        public System.Globalization.CultureInfo Culture
        {
            get { return Controller.Culture; }
            set { Controller.Culture = value; }
        }

        [Category("Action")]
        public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;

        [MergableProperty(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Behavior")]
        [Localizable(false)]
        [Themeable(false)]
        public IList<FileType> FileTypes
        {
            get { return Controller.FileTypes; }
        }

        [Category("Action")]
        public event EventHandler<UploadFileEventArgs> FileUploaded;

        [Category("Action")]
        public event EventHandler<UploadFileCancelEventArgs> FileUploading;

        [DefaultValue("")]
        [Category("Behavior")]
        [Themeable(false)]
        [Localizable(false)]
        public string HiddenFiles
        {
            get { return Controller.HiddenFiles; }
            set { Controller.HiddenFiles = value; }
        }

        [Category("Action")]
        public event EventHandler<RenameEventArgs> ItemRenamed;

        [Category("Action")]
        public event EventHandler<RenameCancelEventArgs> ItemRenaming;

        [Category("Action")]
        public event EventHandler<NewFolderEventArgs> NewFolderCreated;

        [Category("Action")]
        public event EventHandler<NewFolderCancelEventArgs> NewFolderCreating;

        [DefaultValue("")]
        [Category("Behavior")]
        [Themeable(false)]
        [Localizable(false)]
        public string ProhibitedFiles
        {
            get { return Controller.ProhibitedFiles; }
            set { Controller.ProhibitedFiles = value; }
        }

        [MergableProperty(false)]
        [Category("Data")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Localizable(false)]
        [Themeable(false)]
        public IList<RootFolder> RootFolders
        {
            get { return Controller.RootFolders; }
        }

        [MergableProperty(false)]
        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Localizable(false)]
        [Themeable(false)]
        public IList<SpecialFolder> SpecialFolders
        {
            get { return Controller.SpecialFolders; }
        }

        [Category("Action")]
        public event EventHandler<SelectedItemsActionEventArgs> SelectedItemsActionComplete;

        [Category("Action")]
        public event EventHandler<SelectedItemsActionCancelEventArgs> SelectedItemsAction;

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string FileSmallImageUrl
        {
            get { return Controller.FileSmallImageUrl; }
            set { Controller.FileSmallImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string FileLargeImageUrl
        {
            get { return Controller.FileLargeImageUrl; }
            set { Controller.FileLargeImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string FolderSmallImageUrl
        {
            get { return Controller.FolderSmallImageUrl; }
            set { Controller.FolderSmallImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string FolderLargeImageUrl
        {
            get { return Controller.FolderLargeImageUrl; }
            set { Controller.FolderLargeImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string RootFolderSmallImageUrl
        {
            get { return Controller.RootFolderSmallImageUrl; }
            set { Controller.RootFolderSmallImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string RootFolderLargeImageUrl
        {
            get { return Controller.RootFolderLargeImageUrl; }
            set { Controller.RootFolderLargeImageUrl = value; }
        }

        [DefaultValue("")]
        [Bindable(true)]
        [Category("Appearance")]
        public string ImagesFolder
        {
            get { return Controller.ImagesFolder; }
            set { Controller.ImagesFolder = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string DeleteImageUrl
        {
            get { return Controller.DeleteImageUrl; }
            set { Controller.DeleteImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string RenameImageUrl
        {
            get { return Controller.RenameImageUrl; }
            set { Controller.RenameImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string CopyImageUrl
        {
            get { return Controller.CopyImageUrl; }
            set { Controller.CopyImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string MoveImageUrl
        {
            get { return Controller.MoveImageUrl; }
            set { Controller.MoveImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string FolderUpImageUrl
        {
            get { return Controller.FolderUpImageUrl; }
            set { Controller.FolderUpImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string NewFolderImageUrl
        {
            get { return Controller.NewFolderImageUrl; }
            set { Controller.NewFolderImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string ViewImageUrl
        {
            get { return Controller.ViewImageUrl; }
            set { Controller.ViewImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string ProcessImageUrl
        {
            get { return Controller.ProcessImageUrl; }
            set { Controller.ProcessImageUrl = value; }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [UrlProperty]
        [Bindable(true)]
        [Category("Appearance")]
        public string RefreshImageUrl
        {
            get { return Controller.RefreshImageUrl; }
            set { Controller.RefreshImageUrl = value; }
        }

        #endregion

        		#region Events

		#endregion

		#region OnEvent Methods

		private void OnItemRenaming (RenameCancelEventArgs e) {
			if (ItemRenaming != null)
				ItemRenaming (this, e);
		}

		private void OnItemRenamed (RenameEventArgs e) {
			if (ItemRenamed != null)
				ItemRenamed (this, e);
		}

		private void OnNewFolderCreating (NewFolderCancelEventArgs e) {
			if (NewFolderCreating != null)
				NewFolderCreating (this, e);
		}

		private void OnNewFolderCreated (NewFolderEventArgs e) {
			if (NewFolderCreated != null)
				NewFolderCreated (this, e);
		}

		private void OnFileUploading (UploadFileCancelEventArgs e) {
			if (FileUploading != null)
				FileUploading (this, e);
		}

		private void OnFileUploaded (UploadFileEventArgs e) {
			if (FileUploaded != null)
				FileUploaded (this, e);
		}

		private void OnSelectedItemsAction (SelectedItemsActionCancelEventArgs e) {
			if (SelectedItemsAction != null)
				SelectedItemsAction (this, e);
		}

		private void OnSelectedItemsActionComplete (SelectedItemsActionEventArgs e) {
			if (SelectedItemsActionComplete != null)
				SelectedItemsActionComplete (this, e);
		}

		private void OnExecuteCommand (ExecuteCommandEventArgs e) {
			if (ExecuteCommand != null)
				ExecuteCommand (this, e);
		}

		#endregion
    }
}

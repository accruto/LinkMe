using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Web.UI.Controls.Common.FileManager;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class ImageContentEditor
        : ContentEditor
    {
        private readonly ImageContentItem item;
        private FileSelector selector;
        private FileManager fileManager;

        internal ImageContentEditor(ImageContentItem item)
        {
            this.item = item;
        }

        protected override void AddControls(Control container)
        {
            if (item != null)
            {
                Control properties = AddProperties(container);

                // Add a label.

                Label label = CreateLabel("Image URL");

                // Create a selector.

                Control panel = CreatePanel();

                selector = new FileSelector();
                selector.SelectClick += selector_SelectClick;
                selector.CssClass = "generic-form-input";

                Control selectorPanel = CreatePanel();
                panel.Controls.Add(selectorPanel);
                selectorPanel.Controls.Add(selector);

                // Create a file manager which will get shown when the user selects.

                fileManager = new FileManager();

                RootFolder folder = new RootFolder();
                folder.Path = item.RootFolder;
                fileManager.RootFolders.Add(folder);

                // File types supported.

                fileManager.FileTypes.Add(FileType.Image);

                fileManager.FileViewMode = FileViewRenderMode.Details;
                fileManager.ShowSelectBar = true;
                fileManager.ShowUploadBar = false;
                fileManager.ShowAddressBar = false;
                fileManager.CommandOptions.Copy = false;
                fileManager.CommandOptions.Move = false;
                fileManager.CommandOptions.NewFolder = false;
                fileManager.CommandOptions.Delete = false;
                fileManager.CommandOptions.Rename = false;

                fileManager.Width = new Unit("450px");
                fileManager.Height = new Unit("300px");

                fileManager.SelectCommand += fileManager_SelectCommand;
                fileManager.CancelCommand += fileManager_CancelCommand;

                // Don't show it yet.

                fileManager.Visible = false;

                Control fileManagerPanel = CreatePanel();
                panel.Controls.Add(fileManagerPanel);
                fileManagerPanel.Controls.Add(fileManager);

                AddProperty(properties, label, panel);
            }
        }

        protected override void UpdateEditor()
        {
            // Reset the path and don't show the file manager yet.

            if (selector != null)
                selector.Path = item != null ? item.RelativePath : string.Empty;
            if (fileManager != null)
                fileManager.Visible = false;
        }

        protected override void UpdateItem()
        {
            if (item != null)
                item.RelativePath = selector != null ? selector.Path : string.Empty;
        }

        protected override bool IsValid
        {
            get { return true; }
        }

        void fileManager_CancelCommand(object sender, System.EventArgs e)
        {
            fileManager.Visible = false;
        }

        void fileManager_SelectCommand(object sender, System.EventArgs e)
        {
            if (fileManager.SelectedItems.Length > 0)
            {
                FileManagerItemInfo itemInfo = fileManager.SelectedItems[0];
                if (!string.IsNullOrEmpty(itemInfo.FileManagerPath))
                {
                    if (selector != null)
                    {
                        string path = itemInfo.FileManagerPath;

                        // Need to take off the root folder.

                        int pos = path.IndexOf('/');
                        if (pos != -1)
                        {
                            path = path.Substring(pos + 1);
                            pos = path.IndexOf('/');
                            if (pos != -1)
                                path = path.Substring(pos + 1);
                        }

                        selector.Path = path;
                        fileManager.Visible = false;
                    }
                }
            }
        }

        private void selector_SelectClick(object sender, System.EventArgs e)
        {
            // If they try to select then make the file manager visible so they can browse.

            if (fileManager != null)
                fileManager.Visible = true;
        }
    }

    public class ImageContentEditorFactory
        : IItemContentEditorFactory
    {
        IContentEditor IItemContentEditorFactory.CreateEditor(ContentItem item)
        {
            if (item is ImageContentItem)
                return new ImageContentEditor(item as ImageContentItem);
            return null;
        }
    }
}

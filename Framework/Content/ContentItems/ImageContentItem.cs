namespace LinkMe.Framework.Content.ContentItems
{
    public class ImageContentItem
        : ContentItem
    {
        private const string RootFolderProperty = "RootFolder";
        private const string RelativePathProperty = "RelativePath";

        public string RootFolder
        {
            get { return GetField<string>(RootFolderProperty); }
            set { SetField(RootFolderProperty, value); }
        }

        public string RelativePath
        {
            get { return GetField<string>(RelativePathProperty); }
            set { SetField(RelativePathProperty, value); }
        }
    }
}

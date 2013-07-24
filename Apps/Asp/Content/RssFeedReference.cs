namespace LinkMe.Apps.Asp.Content
{
    public class RssFeedReference
        : Reference
    {
        public RssFeedReference(string path)
            : base(path)
        {
        }
    }

    public class RssFeedReferences
        : References<RssFeedReference>
    {
    }
}
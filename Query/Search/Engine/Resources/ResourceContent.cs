using LinkMe.Domain.Resources;

namespace LinkMe.Query.Search.Engine.Resources
{
    public class ResourceContent
        : Content
    {
        public Resource Resource { get; set; }
        public int Views { get; set; }

        public override bool IsSearchable
        {
            get { return true; }
        }
    }
}

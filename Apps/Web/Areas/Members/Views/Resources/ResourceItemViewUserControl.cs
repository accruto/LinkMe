using LinkMe.Domain.Resources;
using LinkMe.Query.Search.Resources;
using LinkMe.Web.Areas.Members.Models.Resources;

namespace LinkMe.Web.Areas.Members.Views.Resources
{
    public class ResourceItemViewUserControl<TResource>
        : ResourceHighlighterViewUserControl<ResourceItemModel<TResource>>
        where TResource : Resource
    {
        protected override ResourceSearchCriteria GetCriteria()
        {
            return Model.List.Criteria;
        }
    }
}

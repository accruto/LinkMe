using LinkMe.Query.Search.Resources;
using LinkMe.Web.Areas.Members.Models.Resources;

namespace LinkMe.Web.Areas.Members.Views.Resources
{
    public class ResourceViewUserControl<TModel>
        : ResourceHighlighterViewUserControl<TModel>
        where TModel : ResourceModel
    {
        protected override ResourceSearchCriteria GetCriteria()
        {
            return Model.Criteria;
        }
    }
}

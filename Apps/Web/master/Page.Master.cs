using System.Web.UI.WebControls;
using LinkMe.WebControls;

namespace LinkMe.Web.Master
{
    public partial class Page
        : LinkMeNestedMasterPage
    {
        protected override LinkMeValidationSummary ValidationSummary
        {
            get { return wcValidationSummary; }
        }

        protected override PlaceHolder NonFormPlaceHolder
        {
            get { return phNonFormContent; }
        }
    }
}

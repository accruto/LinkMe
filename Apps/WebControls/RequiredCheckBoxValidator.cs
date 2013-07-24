using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    public class RequiredCheckBoxValidator : BaseValidator
    {
        private HtmlInputCheckBox chkBox;

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate);

            if (ctrl == null)
                return false;

            chkBox = ctrl as HtmlInputCheckBox;

            return (chkBox != null);
        }

        protected override bool EvaluateIsValid()
        {
            return chkBox.Checked;
        }
    }
}

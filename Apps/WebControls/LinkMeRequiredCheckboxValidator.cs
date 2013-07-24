using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    public class LinkMeRequiredCheckboxValidator : BaseValidator
    {
        private HtmlInputCheckBox checkBoxToValidate;
        private readonly LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();
        
        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate);

            if (ctrl != null)
            {
                checkBoxToValidate = (HtmlInputCheckBox)ctrl;
                return (checkBoxToValidate != null);
            }
            else
                return false;  // raise exception
        }

        protected override bool EvaluateIsValid()
        {
            return checkBoxToValidate.Checked;
        }

        [Browsable(false)]
        public override string Text
        {
            get { return valDetails.Text; }
            set { }
        }
    }

}
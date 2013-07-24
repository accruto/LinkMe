using System;
using System.Web.UI.WebControls;
using LinkMe.Common;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Validation;
using LinkMe.WebControls;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class EmailField
        : TextBoxFieldContainer
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            valEmailAddress.ControlToValidate = ucContents.TextBoxRelativeUniqueID;

            ucContents.TextMode = TextBoxMode.SingleLine;
            ucContents.ControlCssClass = "control textbox_control";
            ucContents.TextBoxCssClass = "textbox";
            ucContents.MaxLength = LinkMe.Framework.Utility.Validation.Constants.EmailAddressMaxLength;
            
            Label = "Email";
            Description = "email";
        }

        protected override string CompulsoryCssClass
        {
            get { return "field compulsory_field textbox_field"; }
        }

        protected override string NonCompulsoryCssClass
        {
            get { return "field textbox_field"; }
        }

        public bool AllowMultiple
        {
            set
            {
                valEmailAddress.EmailAddressValidationMode = (value ?
                    EmailAddressValidationMode.MultipleEmails : EmailAddressValidationMode.SingleEmail);
            }
        }
    }
}

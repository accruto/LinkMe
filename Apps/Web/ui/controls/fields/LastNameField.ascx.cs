using System;
using System.Web.UI.WebControls;
using LinkMe.Common;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class LastNameField
        : TextBoxFieldContainer
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ucContents.TextMode = TextBoxMode.SingleLine;
            ucContents.ControlCssClass = "control textbox_control";
            ucContents.TextBoxCssClass = "textbox";
            ucContents.MaxLength = DomainConstants.PersonNameMaxLength;
            ucContents.Regex = LinkMe.Domain.RegularExpressions.CompleteLastNamePattern;
            ucContents.Label = "Last name";
            ucContents.Description = "last name";
        }

        protected override string CompulsoryCssClass
        {
            get { return "field compulsory_field textbox_field"; }
        }

        protected override string NonCompulsoryCssClass
        {
            get { return "field textbox_field"; }
        }
    }
}
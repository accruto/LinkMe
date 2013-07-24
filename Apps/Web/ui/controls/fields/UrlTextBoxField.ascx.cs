using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class UrlTextBoxField
        : TextBoxFieldContainer
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ucContents.TextMode = TextBoxMode.SingleLine;
            ucContents.ControlCssClass = "control textbox_control url_textbox_control";
            ucContents.TextBoxCssClass = "textbox url_textbox";
            ucContents.Regex = RegularExpressions.CompleteUrlPattern;
        }

        protected override string CompulsoryCssClass
        {
            get { return "field compulsory_field textbox_field url_textbox_field"; }
        }

        protected override string NonCompulsoryCssClass
        {
            get { return "field textbox_field url_textbox_field"; }
        }
    }
}
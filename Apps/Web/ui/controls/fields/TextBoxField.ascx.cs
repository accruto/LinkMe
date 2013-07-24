using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class TextBoxField
        : TextBoxFieldContainer
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ucContents.TextMode = TextBoxMode.SingleLine;
            ucContents.ControlCssClass = "control textbox_control";
            ucContents.TextBoxCssClass = "textbox";
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
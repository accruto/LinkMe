using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class MultiLineTextBoxField
        : TextBoxFieldContainer
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ucContents.TextMode = TextBoxMode.MultiLine;
            ucContents.ControlCssClass = "control textbox_control multiline_textbox_control";
            ucContents.TextBoxCssClass = "multiline_textbox textbox";
        }

        protected override string CompulsoryCssClass
        {
            get { return "field compulsory_field textbox_field multiline_textbox_field"; }
        }

        protected override string NonCompulsoryCssClass
        {
            get { return "field textbox_field multiline_textbox_field"; }
        }
    }
}
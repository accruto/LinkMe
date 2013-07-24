using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public class FileSelector : TextBox
	{
        private string buttonText;
        private HtmlInputButton button;

        public event EventHandler SelectClick;

        public string ButtonText
        {
            get { return buttonText ?? "..."; }
            set { buttonText = value; }
        }

        public virtual string Path
        {
            get { return Text; }
            set { Text = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            // Create the button for selecting.

            button = new HtmlInputButton();
            button.ServerClick += button_ServerClick;
            button.ID = ID + "-button";
            button.Value = ButtonText;
            Controls.Add(button);
			
			base.OnInit(e);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // Close off the end tag for the textbox and then show the button.

            base.RenderEndTag(writer);
            button.RenderControl(writer);
        }

        void button_ServerClick(object sender, EventArgs e)
        {
            // Raise an event.

            if (SelectClick != null)
                SelectClick.Invoke(this, EventArgs.Empty);
        } 
    }
}

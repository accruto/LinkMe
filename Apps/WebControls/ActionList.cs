using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    [ParseChildren(true, "ListItem")]
    [ToolboxData("<{0}:ActionList runat=server></{0}:ActionList>")]
    public class ActionList : WebControl, INamingContainer
    {
        private bool isOrdered = false;
        
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("If true, the ActionList will render an ordered list (<ol>). If false, an unordered list (<ul>) is rendered.")]
        [Browsable(true)]
        public bool IsOrdered
        {
            get { return isOrdered; }
            set { isOrdered = value; }
        }

        protected string DisplayTagName
        {
            get { return IsOrdered ? "ol" : "ul"; }
        }

        public Control ListItem
        {
            set { Controls.Add(value.Controls[0]); }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (GetLastVisibleControl(Controls) == null) return;    // No controls visible, omit the whole list
            writer.WriteLine("<{0} class=\"{1}\" id=\"{2}\">", DisplayTagName, CssClass, ClientID);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            bool firstVisibleItemDone = false;
            Control last = GetLastVisibleControl(Controls);

            if (last == null)
                // No controls are visible. We're done.
                return;

            foreach (Control control in Controls)
            {
                if (control.Visible)
                {
//                    Controls.Add(control);

                    bool isFirst = !firstVisibleItemDone;
                    bool isLast = (control == last);

                    StringBuilder classAttribute = new StringBuilder();
                    if (isFirst || isLast)
                    {
                        classAttribute.Append(" class=\"");

                        if (isFirst)
                        {
                            classAttribute.Append("first");
                            firstVisibleItemDone = true;
                        }
                        if (isFirst && isLast)
                            classAttribute.Append(" ");
                        if (isLast)
                            classAttribute.Append("last");

                        classAttribute.Append("\"");
                    }

                    output.WriteLine("<li{0}>", classAttribute);
                    control.RenderControl(output);
                    output.WriteLine("</li>");
                }
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (GetLastVisibleControl(Controls) == null) return;    // No controls visible, omit the whole list
            writer.WriteLine("</{0}>", DisplayTagName);
        }

        private static Control GetLastVisibleControl(ControlCollection controls)
        {
            for (int i = controls.Count-1; i >= 0 ; i-- )
            {
                if (controls[i].Visible)
                {
                    return controls[i];
                }
            }

            return null;
        }
    }
}

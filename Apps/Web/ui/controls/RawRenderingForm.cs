using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace LinkMe.Web.UI.Controls
{
    /// <summary>
    /// A form that doesn't actually render as a &lt;form&gt; element, but is only used to enable a page to contain
    /// server-side controls. It also doesn't render any hidden VIEWSTATE fields.
    /// </summary>
    public class RawRenderingForm : HtmlForm
    {
        public RawRenderingForm()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            RawControlsPage page = Page as RawControlsPage;
            if (page == null)
            {
                throw new ApplicationException("A " + typeof(RawRenderingForm).Name + " form may only be placed on a "
                    + typeof(RawControlsPage).Name + ".");
            }

            page.InServerForm = true; // Everything on this page must be inside the form.

            base.OnInit(e);
        }

        protected override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
        }

        protected override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            foreach (Control control in Controls)
            {
                control.RenderControl(writer);
            }
        }
    }
}

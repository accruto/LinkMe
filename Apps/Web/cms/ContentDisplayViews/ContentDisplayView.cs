using System;
using LinkMe.Framework.Content;
using LinkMe.Web.Cms.ContentDisplayers;

namespace LinkMe.Web.Cms.ContentDisplayViews
{
    public abstract class ContentDisplayView
        : ContentView
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Display();
        }

        protected abstract IContentDisplayer GetContentDisplayer();

        protected static IContentDisplayer CreateDisplayer(ContentItem item)
        {
            return CreateDisplayer(item, null);
        }

        protected static IContentDisplayer CreateDisplayer(ContentItem item, string templateUrl)
        {
            if (item == null)
                return null;

            // If the item has a displayer then use that.

            IContentDisplayer displayer = ContentManager.GetDisplayer(item);
            if (displayer != null)
                return displayer;

            // Return a template displayer for the item.

            return new TemplateContentDisplayer(item, templateUrl ?? ContentManager.GetDisplayerTemplateUrl(item));
        }

        protected void Display()
        {
            // All derived controls should have had time to set themselves up so get the item to display.

            IContentDisplayer displayer = GetContentDisplayer();
            if (displayer != null)
            {
                // Clear away any controls that may already exist as they are going to be replaced by this content.

                Controls.Clear();
                displayer.AddControls(this);

                // If you get an error on the previous step something like:
                //   "The Controls collection cannot be modified because the control contains code blocks (i.e. <% ... %>)."
                // what it probably means is that you have some code blocks in the default content defined on your
                // page or in your control, i.e. the controls that get cleared away above.  Not sure what the
                // underlying problem is but rework the code and move code blocks into the code behind to make it
                // all go away or put appropriate runat="server" attributes on outermost div etc elements.
            }
        }
    }
}

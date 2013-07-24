using System.Web.UI;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.UI;

namespace LinkMe.Web.Cms.ContentDisplayers
{
    public class TemplateContentDisplayer
        : IContentDisplayer
    {
        private readonly ContentItem _item;
        private readonly string _templateUrl;

        public TemplateContentDisplayer(ContentItem item, string templateUrl)
        {
            _item = item;
            _templateUrl = templateUrl;
        }

        void IContentDisplayer.AddControls(Control container)
        {
            if (!string.IsNullOrEmpty(_templateUrl))
            {
                // Load the control corresponding to the template.

                Control template = container.Page.LoadControl(_templateUrl);
                if (template != null)
                {
                    if (template is IContentItemControl)
                        ((IContentItemControl)template).Item = _item;

                    // Add it to the container.

                    container.Controls.Add(template);
                }
            }
        }
    }
}

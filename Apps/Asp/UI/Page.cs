using System;
using System.Web.UI;
using LinkMe.Apps.Asp.Content;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class Page
        : System.Web.UI.Page
    {
        private bool _inServerForm;

        public virtual void OnLoginAuthenticated()
        {
        }

        public bool InServerForm
        {
            get { return _inServerForm; }
            set { _inServerForm = value; }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            if (!_inServerForm)
                base.VerifyRenderingInServerForm(control);
        }

        public ReadOnlyUrl GetClientUrl()
        {
            return Master.GetClientUrl();
        }

        public void AddRssFeedReference(RssFeedReference reference)
        {
            Master.AddRssFeedReference(reference);
        }

        public bool UseStandardStyleSheetReferences
        {
            get { return Master.UseStandardStyleSheetReferences; }
            set { Master.UseStandardStyleSheetReferences = value; }
        }

        public void AddStyleSheetReference(StyleSheetReference reference)
        {
            Master.AddStyleSheetReference(reference);
        }

        public void AddJavaScriptReference(JavaScriptReference reference)
        {
            Master.AddJavaScriptReference(reference);
        }

        public void SetFaviconReference(FaviconReference reference)
        {
            // Don't fail if it cannot be set.

            if (base.Master != null)
                Master.SetFaviconReference(reference);
        }

        public void SetTitle(string title)
        {
            // Don't fail if it cannot be set.

            if (base.Master != null)
                Master.SetTitle(title);
        }

        public void AddTitleValue(string value)
        {
            Master.AddTitleValue(value);
        }

        public void AddMetaTag(string name, string content)
        {
            Master.AddMetaTag(name, content);
        }

        protected new IMasterPage Master
        {
            get
            {
                var master = base.Master as IMasterPage;
                if (master == null)
                {
                    if (base.Master == null)
                        throw new InvalidOperationException("Attempted to use the master page, but it is not set for a page of type " + GetType().FullName);
                    else
                        throw new InvalidOperationException("Attempted to use the master page, but it is of type" + base.Master.GetType().FullName + ", which is not derived from " + typeof(IMasterPage).FullName + ".");
                }

                return master;
            }
        }
    }
}

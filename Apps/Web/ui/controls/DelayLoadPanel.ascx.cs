using System;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls
{
    public partial class DelayLoadPanel : LinkMeUserControl
    {
        public string ContainerId { get; set; }
        public string Suffix { get; set; }
        public string UrlToGet { get; set; }
        public string SuccessString { get; set; }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddJavaScriptReference(JavaScripts.AjaxHelper);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (string.IsNullOrEmpty(ContainerId))
                throw new ApplicationException("The ContainerId property must be set.");
            if (string.IsNullOrEmpty(Suffix))
                throw new ApplicationException("The Suffix property must be set.");
            if (string.IsNullOrEmpty(UrlToGet))
                throw new ApplicationException("The UrlToGet property must be set.");
            if (string.IsNullOrEmpty(SuccessString))
                throw new ApplicationException("The SuccessString property must be set.");
 
        }
    }
}
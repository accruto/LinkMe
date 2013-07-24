using System.Web;
using LinkMe.Apps.Asp.Elements;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public class ViewMasterPage
        : System.Web.Mvc.ViewMasterPage
    {
        private ReadOnlyUrl _clientUrl;
        private readonly HeadInfo _headInfo = new HeadInfo();

        protected HeadInfo Head
        {
            get { return _headInfo; }
        }

        protected virtual ReadOnlyUrl GetClientUrl()
        {
            return _clientUrl ?? (_clientUrl = HttpContext.Current.GetClientUrl());
        }
    }
}

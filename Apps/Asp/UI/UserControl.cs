using System;
using System.Collections.Specialized;
using System.Web.UI;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class UserControl
        : System.Web.UI.UserControl
    {
        #region

        new protected Page Page
        {
            get { return (Page) base.Page; }
        }

        #endregion

        #region GetUrlForPage

        protected static ReadOnlyUrl GetUrlForPage<T>(ReadOnlyQueryString queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected static ReadOnlyUrl GetUrlForPage<T>(NameValueCollection queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected static ReadOnlyUrl GetUrlForPage<T>(params string[] queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected static ReadOnlyUrl GetUrlForPage(string page, params string[] queryString)
        {
            return NavigationManager.GetUrlForPage(page, queryString);
        }

        #endregion

        #region SetFocus

        protected virtual void SetFocusOnControl(Control toFocus)
        {
            base.Page.ClientScript.RegisterStartupScript(typeof(UserControl), "Focus", GetSetFocusScript(toFocus), true);
        }

        protected static string GetSetFocusScript(Control toFocus)
        {
            if (toFocus == null)
                throw new ArgumentNullException("toFocus");

            return string.Format("document.getElementById(\"{0}\").focus();", toFocus.ClientID);
        }

        #endregion
    }
}

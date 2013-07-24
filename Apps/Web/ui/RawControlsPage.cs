using System;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Apps.Presentation.Errors;

namespace LinkMe.Web.UI
{
    /// <summary>
    /// A page that returns the raw HTML output of the controls on it without any enclosing elements and without viewstate.
    /// Used to return HTML to be inserted into other pages using AJAX.
    /// </summary>
    public abstract class RawControlsPage : LinkMePage
    {
        protected RawControlsPage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            // The output of a page inserted via AJAX must not be cached. Firefox works fine, but IE 6 doesn't reload the contents.

            DisablePageCaching();

            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                OnLoadImpl();
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
                Response.Clear();
                Response.Write("<strong>An error has occurred on the server.</strong>");
                Response.End();
            }
        }

        protected abstract void OnLoadImpl();

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            // Don't save anything - this is purely for AJAX and no ClientState is used.
        }
    }
}

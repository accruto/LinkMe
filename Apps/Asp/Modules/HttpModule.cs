using System;
using System.Web;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Modules
{
    public abstract class HttpModule
        : IHttpModule
    {
        private const string ClientUrl = "ClientUrl";

        private HttpApplication _application;

        void IHttpModule.Init(HttpApplication application)
        {
            _application = application;
            _application.BeginRequest += BeginRequest;
            _application.AuthenticateRequest += AuthenticateRequest;
            _application.PostAuthenticateRequest += PostAuthenticateRequest;
            _application.PreRequestHandlerExecute += PreRequestHandlerExecute;

            OnInit();
        }

        void IHttpModule.Dispose()
        {
            if (_application != null)
                _application.BeginRequest -= BeginRequest;
        }

        protected virtual void OnInit()
        {
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            OnBeginRequest();
        }

        private void AuthenticateRequest(object sender, EventArgs e)
        {
            OnAuthenticateRequest();
        }

        private void PostAuthenticateRequest(object sender, EventArgs e)
        {
            OnPostAuthenticateRequest();
        }

        private void PreRequestHandlerExecute(object sender, EventArgs e)
        {
            OnPreRequestHandlerExecute();
        }

        protected virtual void OnBeginRequest()
        {
        }

        protected virtual void OnAuthenticateRequest()
        {
        }

        protected virtual void OnPostAuthenticateRequest()
        {
        }

        protected virtual void OnPreRequestHandlerExecute()
        {
        }

        protected ReadOnlyUrl GetClientUrl()
        {
            var context = HttpContext.Current;
            var clientUrl = context.Items[ClientUrl] as ReadOnlyUrl;
            if (clientUrl == null)
            {
                clientUrl = context.GetClientUrl();
                context.Items[ClientUrl] = clientUrl;
            }

            return clientUrl;
        }
    }
}

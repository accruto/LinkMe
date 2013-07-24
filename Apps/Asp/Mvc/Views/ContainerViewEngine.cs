using System.IO;
using System.Web.Compilation;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public class ContainerViewEngine
        : WebFormViewEngine
    {
        private readonly IUnityContainer _container;

        private class ContainerView
            : IView
        {
            private readonly WebFormView _view;
            private readonly IUnityContainer _container;

            public ContainerView(WebFormView view, IUnityContainer container)
            {
                _view = view;
                _container = container;
            }

            public void Render(ViewContext viewContext, TextWriter writer)
            {
                var view = BuildManager.CreateInstanceFromVirtualPath(_view.ViewPath, typeof(object));
                _container.BuildUp(view.GetType(), view);

                var viewPage = view as System.Web.Mvc.ViewPage;
                if (viewPage != null)
                {
                    viewPage.ViewData = viewContext.ViewData;
                    viewPage.RenderView(viewContext);
                    return;
                }

                var viewUserControl = view as System.Web.Mvc.ViewUserControl;
                if (viewUserControl != null)
                {
                    viewUserControl.ViewData = viewContext.ViewData;
                    viewUserControl.RenderView(viewContext);
                    return;
                }

                _view.Render(viewContext, writer);
            }
        }

        public ContainerViewEngine(IUnityContainer container)
        {
            _container = container;
        }

        protected ViewEngineResult GetViewEngineResult(ViewEngineResult result)
        {
            return result.View == null || !(result.View is WebFormView)
                ? result
                : new ViewEngineResult(new ContainerView((WebFormView)result.View, _container), this);
        }
    }
}

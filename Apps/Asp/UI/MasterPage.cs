using System;
using System.Collections.Generic;
using System.Web;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Elements;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class MasterPage
        : System.Web.UI.MasterPage, IMasterPage
    {
        private readonly HeadInfo _headInfo = new HeadInfo();
        private List<string> _titleValues;
        private bool _rendered;

        protected HeadInfo Head
        {
            get { return _headInfo; }
        }

        ReadOnlyUrl IMasterPage.GetClientUrl()
        {
            return GetClientUrl();
        }

        void IMasterPage.SetTitle(string title)
        {
            if (_titleValues != null && _titleValues.Count != 0)
                title = string.Format(title, _titleValues.ToArray());
            Page.Header.Title = title;
        }

        void IMasterPage.AddTitleValue(string value)
        {
            CheckNotRendered();

            if (_titleValues == null)
                _titleValues = new List<string>();
            _titleValues.Add(HtmlUtil.TextToHtml(value));
        }

        void IMasterPage.AddMetaTag(string name, string content)
        {
            CheckNotRendered();
            _headInfo.MetaTags[name] = content;
        }

        string IMasterPage.MainContentContainerId
        {
            get { return MainContentContainerId; }
            set { MainContentContainerId = value; }
        }

        string IMasterPage.MainContentContainerCssClass
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        void IMasterPage.AddRssFeedReference(RssFeedReference reference)
        {
            CheckNotRendered();
            _headInfo.Add(reference);
        }

        bool IMasterPage.UseStandardStyleSheetReferences
        {
            get { return _headInfo.UseStandardStyleSheetReferences; }
            set { _headInfo.UseStandardStyleSheetReferences = value; }
        }

        void IMasterPage.AddStyleSheetReference(StyleSheetReference reference)
        {
            CheckNotRendered();
            _headInfo.Add(reference);
        }

        void IMasterPage.AddJavaScriptReference(JavaScriptReference reference)
        {
            CheckNotRendered();
            _headInfo.Add(reference);
        }

        void IMasterPage.SetFaviconReference(FaviconReference reference)
        {
            CheckNotRendered();
            _headInfo.FaviconReference = reference;
        }

        public void InsertJavaScriptReferenceBeforeAll(JavaScriptReference reference)
        {
            throw new NotImplementedException();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            _rendered = true;
        }

        protected virtual ReadOnlyUrl GetClientUrl()
        {
            return HttpContext.Current.GetClientUrl();
        }

        protected virtual string MainContentContainerId
        {
            get { return null; }
            set { }
        }

        protected virtual string MainContentContainerCssClass
        {
            get { return null; }
            set { }
        }

        public static void AddStandardStyleSheetReference(StyleSheetReference reference)
        {
            HeadInfo.AddStandard(reference);
        }

        public static void AddStandardJavaScriptReference(JavaScriptReference reference)
        {
            HeadInfo.AddStandard(reference);
        }

        private void CheckNotRendered()
        {
            if (_rendered)
                throw new ApplicationException("CSS, JavaScript and RSS references and meta tags etc have already been rendered to the page. It's too late to call this now.");
        }
    }
}

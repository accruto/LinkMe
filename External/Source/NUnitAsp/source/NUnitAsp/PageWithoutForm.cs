using System;

namespace NUnit.Extensions.Asp
{
    /// <summary>
    /// Allows the entire page to act as a "container" for other ControlTesters, which is useful if
    /// there are controls on a page without a form.
    /// </summary>
    public class PageWithoutForm : Tester
    {
        private readonly HttpClient browser;

        public PageWithoutForm(HttpClient browser)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            this.browser = browser;
        }

        public override string Description
        {
            get { return "page without a form"; }
        }

        protected internal override HttpClient Browser
        {
            get { return browser; }
        }

        protected internal override string GetChildElementHtmlId(string aspId)
        {
            return aspId;
        }

        protected internal override void Submit()
        {
            throw new NotSupportedException("A form cannot be submitted on a page without a form.");
        }
    }
}

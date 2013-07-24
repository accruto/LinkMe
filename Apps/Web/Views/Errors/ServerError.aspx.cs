using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using LinkMe.Web.Areas.Errors.Models.Errors;

namespace LinkMe.Web.Views.Errors
{
    public class ServerError
        : ViewPage<ServerErrorModel>
    {
        protected string Styles { get; private set; }
        protected string ExceptionDescription { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Extract the exception pieces.

            if (!Model.ShowDetails || Model.Report == null)
                return;

            var html = GetErrorDescription(Model.Report.Exception);

            var document = new HtmlDocument();
            document.LoadHtml(html);

            Styles = document.DocumentNode.SelectSingleNode("/html/head/style").InnerText;
            ExceptionDescription = document.DocumentNode.SelectSingleNode("/html/body").InnerHtml;
        }

        protected string GetReferrerLink()
        {
            var referrer = Model.Report.ReferrerUrl;
            return referrer == null
                ? "(none)"
                : string.Format("<a href=\"{0}\">{1}</a>", referrer, HttpUtility.HtmlEncode(referrer));
        }

        private static string GetErrorDescription(Exception exception)
        {
            HttpException httpEx;

            if (exception == null)
                return "NO EXCEPTION";
            if ((httpEx = exception as HttpException) != null)
                return httpEx.GetHtmlErrorMessage();

            // Not an HTTP exception, so try a few hacks (ordered from bad to worse!) to get a nice error message.

            // Hack 1: wrap it in an HttpException.

            httpEx = new HttpException("Dummy HttpException created by the ServerError page.", exception);

            var html = httpEx.GetHtmlErrorMessage();
            if (html != null)
                return html;

            // Hack 2: use Reflection to call internal framework methods.

            try
            {
                html = GetErrorDescriptionUsingReflection(httpEx);
            }
            catch (Exception)
            {
            }
            if (html != null)
                return html;

            // Hack 3: get the string representation and HTML-encode it.

            return HttpUtility.HtmlEncode(exception.ToString());
        }

        private static string GetErrorDescriptionUsingReflection(HttpException httpEx)
        {
            var formatterType = typeof(HttpException).Assembly.GetType("System.Web.UnhandledErrorFormatter", false, false);
            if (formatterType == null)
                return null;

            var method = typeof(HttpException).GetMethod("SetFormatter", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
                return null;

            var formatter = Activator.CreateInstance(formatterType, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { httpEx.InnerException }, null);
            method.Invoke(httpEx, new[] { formatter });

            return httpEx.GetHtmlErrorMessage();
        }
    }
}

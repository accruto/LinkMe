using System.Collections.Generic;
using System.Text;
using System.Web;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Service
{
    public abstract class AutoSuggestWebServiceHandler : SimpleWebServiceHandler
    {
        protected AutoSuggestWebServiceHandler()
        {
        }

        #region Static methods

        public static string GetListHTML()
        {
            return GetListHTML(null);
        }

        public static string GetListHTML(IList<string> res)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");

            if (res != null)
            {
                foreach (string s in res)
                {
                    sb.Append("<li>");
                    sb.Append(s);
                    sb.Append("</li>");
                }
            }

            sb.Append("</ul>");
            return sb.ToString();
        }

        #endregion

        protected virtual string MaxResultsParam
        {
            get { return "maxResults"; }
        }

        protected virtual int DefaultMaxResults
        {
            get { return 10; }
        }

        protected override void ProcessRequestImpl(HttpContext context)
        {
            int maxResults = DefaultMaxResults;

            string maxResultsParam = MaxResultsParam;
            if (!string.IsNullOrEmpty(maxResultsParam))
            {
                maxResults = ParseUtil.ParseUserInputInt32Optional(context.Request[maxResultsParam],
                    maxResultsParam + " parameter", maxResults);
            }

            IList<string> suggestions = GetSuggestionList(context, maxResults);

            context.Response.ContentType = "text/html";
            context.Response.Write(GetListHTML(suggestions));
        }

        protected abstract IList<string> GetSuggestionList(HttpContext context, int maxResults);

        protected override string GetErrorContentType()
        {
            return "text/html";
        }

        protected override string GetOutputErrorMessage(string errorMessage, bool showErrorToEndUser)
        {
            return GetListHTML();
        }
    }
}

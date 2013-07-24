using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Common;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Web
{
	public partial class MonitorPage : Page
	{
		#region Nested types

		[Flags]
		private enum MonitorFailure
		{
			None = 0,
			Database = 1,
            ResumeSearchEngine = 0x20,
            MonitorPage = 0x100000 // Exception while running any of the checks
		}

		#endregion

		private const string FailureSeparator = ", ";
        private static readonly EventSource EventSource = new EventSource(typeof(MonitorPage));

		protected MonitorPage()
		{
		}

		protected override void OnLoad(EventArgs e)
		{
            const string method = "OnLoad";

            MonitorFailure result;
            string message;

            try
            {
			base.OnLoad(e);

                result = CheckMonitors(out message);
            }
            catch (Exception ex)
            {
                result = MonitorFailure.MonitorPage;
                message = "An exception occurred in MonitorPage. Check the log for details.";

                try
                {
                    EventSource.Raise(Event.Error, method, "An exception occurred in MonitorPage.", ex, new StandardErrorHandler());
                }
                catch
                {
                    // OK, something is REALLY stuffed, but swallow the exception so that the page still shows an error code.
                }
            }

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "text/plain";
			Response.Write((int)result + " " + message);
			Response.End();
		}

		private static MonitorFailure CheckMonitors(out string message)
		{
			var failure = MonitorFailure.None;
			var sb = new StringBuilder();

            if (SqlErrorMonitor.IsDatabaseTimingOut())
            {
                failure |= MonitorFailure.Database;
                AppendMessage(sb, "database commands are timing out");
            }

            var searchError = CheckResumeSearcher();
            if (searchError != null)
            {
                failure |= MonitorFailure.ResumeSearchEngine;
                AppendMessage(sb, searchError);
            }

		    if (failure == MonitorFailure.None)
			{
				message = "OK";
			}
			else
			{
				if (!Enum.IsDefined(typeof(MonitorFailure), failure))
				{
					var index = sb.ToString().LastIndexOf(FailureSeparator, StringComparison.Ordinal);
					Debug.Assert(index != -1, "index != -1");

					sb.Replace(FailureSeparator, " and ", index, FailureSeparator.Length);
				}

				sb.Append(".");
				message = sb.ToString();
			}

			return failure;
		}

		private static void AppendMessage(StringBuilder sb, string message)
		{
			Debug.Assert(!string.IsNullOrEmpty(message), "!string.IsNullOrEmpty(message)");

			if (sb.Length == 0)
			{
				sb.Append(char.ToUpper(message[0]));
				sb.Append(message, 1, message.Length - 1);
			}
			else
			{
				sb.Append(FailureSeparator);
				sb.Append(message);
			}
		}

        private static string CheckResumeSearcher()
        {
            const string method = "CheckResumeSearcher";

            try
            {
                var executeMemberSearchCommand = Container.Current.Resolve<IExecuteMemberSearchCommand>();

                // Perform a resume search for something common that should always return results.

                MemberSearchResults results;
                try
                {
                    var criteria = new MemberSearchCriteria();
                    criteria.SetKeywords("business");
                    results = executeMemberSearchCommand.Search(null, criteria, new Range(0, 1)).Results;
                }
                catch
                {
                    return "resume search failed";
                }

                return results.MemberIds.Any() ? null : "no resume search results were returned";
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Warning, method, "An error occurred in checking resume search.", ex);
                return "an error occurred in checking resume search";
            }
        }
    }
}

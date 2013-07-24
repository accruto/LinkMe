using System.Diagnostics;
using System.Web;
using LinkMe.Utility.Utilities;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Manager.Errors
{
	/// <summary>
	/// A trace listener that writes assertion failure messages to the current HTTP Response (if any).
	/// </summary>
	public class WriteToResponseListener : TraceListener
	{
		public const string SHORT_MESSAGE_START = "<b>---- Assert Short Message ----</b>"
			+ StringUtils.HTML_LINE_BREAK;
		public const string SHORT_MESSAGE_END = StringUtils.HTML_LINE_BREAK;

		internal WriteToResponseListener()
		{
		}

		public override void Write(string message)
		{
			// Ignore normal debug messages.
		}

		public override void WriteLine(string message)
		{
			// Ignore normal debug messages.
		}

		public override void Fail(string message, string detailMessage)
		{
			const string MESSAGE_FORMAT = "<font size=\"+1\" color=\"red\">---- DEBUG ASSERTION FAILED ----"
				+ "</font>" + StringUtils.HTML_LINE_BREAK  + SHORT_MESSAGE_START + "{0}"
				+ SHORT_MESSAGE_END + "<b>---- Assert Long Message ----</b>"
				+ StringUtils.HTML_LINE_BREAK + "{1}" + StringUtils.HTML_LINE_BREAK
				+ "<b>---- End Of Assert ----</b>" + StringUtils.HTML_LINE_BREAK + StringUtils.HTML_LINE_BREAK;

			HttpContext context = HttpContext.Current;
			if (context == null)
				return;

			HttpResponse response = context.Response;
			if (response == null)
				return;

			response.Write(string.Format(MESSAGE_FORMAT, message, detailMessage));
		}
	}
}

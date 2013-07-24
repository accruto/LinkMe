using System.Collections;
using System.Web;
using System.Web.UI;

namespace LinkMe.WebControls
{
	public class LinkMeRequestValidator : IValidator
	{
		public const string INVALID_DANGEROUS_REQUEST = "INVALID_DANGEROUS_REQUEST";
		public const string INVALID_DANGEROUS_REQUEST_MESSAGE = "Input can NOT contain xml, html or scripts. Please remove html tags from the following location: <BR>";

        private string errorItemText = INVALID_DANGEROUS_REQUEST_MESSAGE;
		private bool isValid = true;

		private readonly Hashtable processedFields = new Hashtable();

        public LinkMeRequestValidator()
        {
        }

	    public void Validate()
		{
            IDictionary errors = HttpContext.Current.Items[INVALID_DANGEROUS_REQUEST] as IDictionary;

			if (errors != null)
			{
				isValid = errors.Count == 0;
			}
			else
			{
				isValid = true;
			}

			if (!isValid)
			{
				foreach (string errorKey in errors.Keys)
				{
					if (!processedFields.ContainsKey(errorKey))
					{
						string message = "&nbsp;&nbsp;<LI>" + HttpContext.Current.Server.HtmlEncode((errors[errorKey] as string)) + "<BR>";
						errorItemText += message;
						processedFields.Add(errorKey, errorKey);
					}
				}
			}
		}

		public string ErrorMessage
		{
			get { return errorItemText; }
			set { errorItemText = value; }
		}

		public bool IsValid
		{
			get { return isValid; }
			set { isValid = value; }
		}
	}
}

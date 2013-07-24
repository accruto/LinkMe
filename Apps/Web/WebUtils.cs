using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Configuration;

namespace LinkMe.Web
{
	public static class WebUtils
	{
		internal static readonly string[] AllowedAttachmentExtensions = new[]
		{ ".doc", ".docx", ".rtf", ".txt", ".pdf", ".xls", ".xlsx", ".ppt", ".pptx", ".ppsx", ".zip", ".rar", ".vsd" };

	    private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();

		public static bool IsAttachmentExtensionAllowed(string fileName)
		{
			if(String.IsNullOrEmpty(fileName))
				return true;

			var ext = Path.GetExtension(fileName);
			return (ext.Length == 0 || Array.IndexOf(AllowedAttachmentExtensions, ext) != -1);
		}

		public static string[] GetDisallowedFileNames(HttpFileCollection files)
		{
			if (files == null || files.Count == 0)
				return null;

			var disallowed = new ArrayList();

			for (var index = 0; index < files.Count; index++)
			{
				var file = files.Get(index);
				if (!IsAttachmentExtensionAllowed(file.FileName))
				{
					disallowed.Add(file.FileName);
				}
			}

			return (disallowed.Count == 0 ? null : (string[])disallowed.ToArray(typeof(string)));
		}

		public static string[] GetEmptyFileNames(HttpFileCollection files)
		{
			if (files == null || files.Count == 0)
				return null;

			var empty = new ArrayList();

			for (var index = 0; index < files.Count; index++)
			{
				var file = files.Get(index);
				if (file.ContentLength == 0 && !String.IsNullOrEmpty(file.FileName))
				{
					empty.Add(file.FileName);
				}
			}

			return (empty.Count == 0 ? null : (string[])empty.ToArray(typeof(string)));
		}

	    // Major hack! Checks whether the specified control is the event target (ie. caused the postback) in the
        // specified request. Don't use this unless you're SURE you need it.
        public static bool IsEventTarget(HttpRequest request, Control control)
	    {
            if (request == null)
                throw new ArgumentNullException("request");
            if (control == null)
                throw new ArgumentNullException("control");
            if (control.Page == null)
                throw new ArgumentException("The supplied control does not have a Page set.");

            var eventTarget = request.Form[WebConstants.EVENT_TARGET_FIELD];
            if (String.IsNullOrEmpty(eventTarget))
                return false;

            eventTarget = eventTarget.Replace('$', control.Page.IdSeparator);

            return (eventTarget == control.UniqueID);
	    }

        /// <summary>
        /// Checks whether a boolean query string parameter is true or false. This check is more lenient
        /// than using ParseUtil.ParseUserInputBoolean(): it returns the default value if the parameter is
        /// not specified or is not a valid boolean.
        /// </summary>
        public static bool IsQueryStringFlagSet(HttpRequest request, string paramName, bool defaultValue)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (String.IsNullOrEmpty(paramName))
                throw new ArgumentException("The parameter name must be specified.", "paramName");
 
            if (defaultValue)
            {
                return !String.Equals(request.QueryString[paramName], Boolean.FalseString,
                    StringComparison.OrdinalIgnoreCase);
            }
            
            return String.Equals(request.QueryString[paramName], Boolean.TrueString,
                StringComparison.OrdinalIgnoreCase);
        }

        public static void SetReturnUrl(Url urlToModify, Uri returnUrl)
        {
            SetReturnUrl(urlToModify, new Url(returnUrl));
        }

        public static void SetReturnUrl(Url urlToModify, ReadOnlyUrl returnUrl)
        {
	        if (urlToModify == null)
	            throw new ArgumentNullException("urlToModify");
	        if (returnUrl == null)
	            throw new ArgumentNullException("returnUrl");

            var returnUrlInReturnUrl = returnUrl.QueryString[Apps.Asp.Constants.ReturnUrlParameter];

            // Avoid loops in return URLs - if the URL to be set has a returnUrl itself then use its return URL. Eg.
            // if you start from the member home page, click Member Settings, then Contact Us and return it will go back to the
            // member home page, not Member Settings.

            urlToModify.QueryString[Apps.Asp.Constants.ReturnUrlParameter] = string.IsNullOrEmpty(returnUrlInReturnUrl)
                ? returnUrl.PathAndQuery
                : returnUrlInReturnUrl;
        }


        public static ReadOnlyUrl GetLoginRedirectUrl(string userName)
        {
            var url = new ApplicationUrl(FormsAuthentication.GetRedirectUrl(userName, false));

            // Remove '/default.aspx' if it is there.

            if (!url.Path.EndsWith("/default.aspx", StringComparison.InvariantCultureIgnoreCase))
                return url;

            // Only remove the default.aspx to cater for the root in a dev environment.

            url.Path = url.Path.Substring(0, url.Path.Length - "default.aspx".Length);
            return url;
        }

        public static ReadOnlyUrl SetEmailBouncedNotification(ReadOnlyUrl url, HttpSessionState session)
        {
            var link = new TagBuilder("a");
            link.SetInnerText("Please check that your email address is correct.");
            link.MergeAttribute("href", new ReadOnlyApplicationUrl(true, "~/members/settings").ToString());
            var message = "Your email address has bounced.<br/>" + link;
            return session.SetNotification(url, NotificationType.Information, message);
        }

        /// <summary>
        /// Sets up a default value in a textbox control which will be removed onclick. Depends on prototype.
        /// Will register events to add defaultValue to the text of input onload and remove it onsubmit.
        /// Only sets the value if the text box is empty (it is safe to set this outside of !IsPostBack).
        /// </summary>
        /// <param name="input">A ref to the textbox.</param>
        /// <param name="defaultValue">The value to be inserted into the text box, which will be cleared onclick.</param>
        public static void SetClickAwayValueForTextBox(TextBox input, string defaultValue)
        {
            const string loadTextBoxClearerId = "LoadClearTextBoxJSFile";
            const string loadTextBoxClearerJsFunction = "LinkMeUI.JSLoadHelper.LoadTextBoxClearer();";

            // We only want to register all these functions if the values are not set on the page
            if (!String.IsNullOrEmpty(input.Text))
                return;

            // We need to provide a type to CSM, but it is seemingly arbitrary (I am a noob though),
            // so I am just going to use the same type for everything, and differentiate based on
            // the names of the controls (which is simplest).
            var typeOfAllScripts = input.Page.GetType();

            var clearInputJsFunction =
                String.Format("ClearInputOfDefaultValue('{0}', '{1}')", input.ClientID, defaultValue);

            var initInputJsFunction =
                String.Format("if ($F('{0}') == '') $('{0}').value = '{1}';", input.ClientID, defaultValue);

            var csm = input.Page.ClientScript;

            if (!csm.IsClientScriptBlockRegistered(typeOfAllScripts, loadTextBoxClearerId))
                csm.RegisterClientScriptBlock(typeOfAllScripts, loadTextBoxClearerId, loadTextBoxClearerJsFunction, true);
            
            // We use the clientid to make the script event unique to this control
            // so that we can have multiple startup and submit events fire for default values
            csm.RegisterOnSubmitStatement(typeOfAllScripts, input.ClientID, clearInputJsFunction);

            // I am doing the setting in javascript, so that if the client doesn't have javascript
            // for whatever reason, the example won't be populated, as it woudn't be cleared.
            csm.RegisterStartupScript(typeOfAllScripts, input.ClientID, initInputJsFunction, true);

            input.Attributes.Add("OnFocus", clearInputJsFunction);
        }

	    public static string GetMemberNameForDisplay(Member member, Guid? viewerMemberId)
	    {
            var view = _memberViewsQuery.GetPersonalView(viewerMemberId, member);
            return HtmlUtil.TextToHtml(view.GetFullNameDisplayText());
        }
    }
}

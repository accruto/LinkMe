using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[DefaultProperty("Text"), ToolboxData("<{0}:LinkMeOneClickButton runat=server></{0}:LinkMeOneClickButton>")]
	public class LinkMeOneClickButton : Button
	{
        private const string EVENT_TARGET_FIELD = "__EVENTTARGET";
        private const int TimeoutMs = 500;

        private string confirmationText = "";
		private bool showJavaScriptConfirmation = false;

	    public LinkMeOneClickButton()
	    {
            EnableViewState = false;
	    }

	    [DefaultValue("")]
		[Description("The text displays the confirmation message.")]
		[Category("Appearance")]
		public string ConfirmationText
		{
			get { return confirmationText; }
			set { confirmationText = value; }
		}

		[DefaultValue("true")]
		[Description("The boolean overrides the confirmation popup")]
		[Category("Appearance")]
		public bool ShowJavaScriptConfirmation
		{
			get { return showJavaScriptConfirmation; }
			set { showJavaScriptConfirmation = value; }
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // If this button is the Default button process postbacks without an event target, which
            // would happen if the client has JavaScript disabled.

            if (Page.IsPostBack)
            {
                HtmlForm parentForm = ControlUtils.GetParentForm(this, false);

                if (parentForm != null && !string.IsNullOrEmpty(parentForm.DefaultButton)
                    && parentForm.DefaultButton == UniqueID && string.IsNullOrEmpty(Page.Request.Form[EVENT_TARGET_FIELD]))
                {
                    OnClick(EventArgs.Empty);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
		{
            Debug.Assert(string.IsNullOrEmpty(OnClientClick),
                "This button does not support 'OnClientClick' (yet) - any script in that even is lost.");

		    base.OnPreRender(e);

			StringBuilder sb = new StringBuilder();
		    sb.Append("javascript: ");

            if (ConfirmationText != null && ConfirmationText != "")
			{
				if (!ShowJavaScriptConfirmation)
				{
					sb.Append("if(showConfirmation == true) { ");
				}
				sb.Append("var confirmation = (confirm('" + ConfirmationText + "'));");
				if (!ShowJavaScriptConfirmation)
				{
					sb.Append("}");
				}
				sb.Append("if(");
				if (!ShowJavaScriptConfirmation)
				{
					sb.Append("!showConfirmation || ");
				}
				sb.Append("confirmation != null && confirmation) {");
			}

            PostBackOptions options = GetPostBackOptions();

            // Don't bother disabling the button if validation fails, because we don't do a postback anyway.
            // Note that options.PerformValidation is what ASP.NET uses, not just CausesValidation.

            if (options.PerformValidation)
            {
                sb.Append("if (typeof(Page_ClientValidate) == 'function') { ");
                sb.Append("if (Page_ClientValidate() == false) { return false; }}; ");
            }

            // Case 3859 - we can't just disable the button, because IE won't submit from a disabled button
            // (Firefox will). Instead, set a (very short) timer to disable the button.
            // This is only a problem if UseSubmitBehavior is set to true - submission using JavaScript works
            // fine in both IE and FF.

            sb.Append("var disableCommand = 'document.getElementById(\\'' + this.id + '\\').disabled = true;'; ");
            sb.Append("var enableCommand = 'document.getElementById(\\'' + this.id + '\\').disabled = false;'; ");
            sb.Append("var dt = setTimeout(disableCommand, 0);");
            sb.AppendFormat("setTimeout(enableCommand, {0});", TimeoutMs);

			// Bug 912: form submission may fail with a JavaScript error if it contains a file input
			// and the file path is invalid. Catch the error and re-enable the button.

			sb.Append("try { ");
			sb.Append(Page.ClientScript.GetPostBackEventReference(options, false));
			sb.Append("; } catch (e) { clearTimeout(dt); this.disabled = false; }");

            // base.AddAttributesToRender() will add the the value of GetPostBackEventReference() to the end of
            // whatever we write here, so the form is submitted twice - case 3810. Return here to prevent this.
            sb.Append("\r\nreturn true;");

			if (ConfirmationText != null && ConfirmationText != "")
			{
				sb.Append("} else { return false; }");
			}

            Attributes["onclick"] = sb.ToString();
		}
    }
}

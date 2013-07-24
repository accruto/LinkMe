using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility;

namespace LinkMe.WebControls
{
    // READ THIS BEFORE MODIFYING!
    //
    // How LinkMeValidationSummary works: Render() and RenderEndTag() are overridden. Render() doesn't really render
    // anything, but adds custom validators, which are then processed by base.Render() to really
    // output the HTML. Render() sets the HeaderText property, which writes the start tags that apply custom styles to
    // the validation summary and RenderEndTag() the corresponding end tags. Overriding RenderBeginTag() would work just
    // as well for server-side validation, but HeaderText must be used for client-side (JavaScript) validation to work
    // (see defect 1308). Note that we CANNOT call Page.Validate() in this control, since that negates the effect
    // of setting CausesValidation="false" for a control and the effect of setting ValidationGroup name,
    // because everything is then validated anyway. Instead we need to manually iterate over the releavant validators,
    // checking IsValid to determine if error messages will need to be rendered.

    [ToolboxData("<{0}:LinkMeValidationSummary runat=server></{0}:LinkMeValidationSummary>")]
	public class LinkMeValidationSummary : ValidationSummary
	{
        private List<string> errorList;
        private List<string> infoList;
        private List<string> confirmList;
        private bool validationFailed = false;

		private const string START_TAG_INFO		= @"<div id=""info-message"">";
		private const string START_TAG_ERROR	= @"<div id=""error-message"">";
		private const string START_TAG_CONFIRM	= @"<div id=""confirm-message"">";
		private const string END_TAG			= @"</div>";

		public bool HasError()
		{
			return !errorList.IsNullOrEmpty();
		}

		public bool HasInfo()
		{
            return !infoList.IsNullOrEmpty();
		}
		
		public bool HasConfirm()
		{
            return !confirmList.IsNullOrEmpty();
		}

		public bool HasError(string errorMessage)
		{
			return (errorList != null && errorList.Contains(errorMessage));
		}

		public bool HasInfo(string infoMessage)
		{
            return (infoList != null && infoList.Contains(infoMessage));
		}
		
		public bool HasConfirm(string infoMessage)
		{
            return (confirmList != null && confirmList.Contains(infoMessage));
		}

		public void AddError(string errorMessage, bool htmlEncode)
		{
            AddMessage(errorMessage, htmlEncode, ref errorList);
		}

        public void AddInfo(string infoMessage, bool htmlEncode)
		{
            AddMessage(infoMessage, htmlEncode, ref infoList);
		}

        public void AddConfirm(string confirmMessage, bool htmlEncode)
		{
            AddMessage(confirmMessage, htmlEncode, ref confirmList);
		}

        public override void RenderEndTag(HtmlTextWriter writer)
		{
            if (ShouldRenderHeaderFooter())
            {
                writer.Write(END_TAG);
            }

            base.RenderEndTag(writer);
        }

        protected override void Render(HtmlTextWriter output)
		{
            // Need to set this even if not a postback - GET requests can also has validation errors.

            SetValidationFailed();

            // Set the header style depending on the type of message we already have, but if there is none default
            // to "error", so that client-side validation error messages are shown correctly. This breaks down if
            // there is a server-side info or confirm message AND a client-side error message. (Hopefully we don't
            // have any such pages in practice.)

            if (ShouldRenderHeaderFooter())
            {
                if (validationFailed || HasError())
                {
                    HeaderText = START_TAG_ERROR;
                }
                else if (HasConfirm())
                {
                    HeaderText = START_TAG_CONFIRM;
                }
                else if (HasInfo())
                {
                    HeaderText = START_TAG_INFO;
                }
                else
                {
                    HeaderText = START_TAG_ERROR;
                }
            }

            // Set the display.
			
			BorderWidth = 0;
			DisplayMode = ValidationSummaryDisplayMode.BulletList;

            // Add the validation messages.

            RenderMessages(errorList);
            RenderMessages(confirmList);
            RenderMessages(infoList);

		    base.Render(output);
		}

        private bool ShouldRenderHeaderFooter()
        {
            return (Enabled && ShowSummary && (validationFailed || HasError() || HasInfo() || HasConfirm()));
        }

        private void SetValidationFailed()
        {
            ValidatorCollection validators = Page.GetValidators(ValidationGroup);

            foreach (IValidator validator in validators)
            {
                if (!validator.IsValid)
                {
                    validationFailed = true;
                    break;
                }
            }
        }

        private void RenderMessages(IEnumerable<string> messageList)
        {
            if (messageList != null)
            {
                foreach (string message in messageList)
                {
                    RenderMessage(message);
                }
            }
        }
        
        private void RenderMessage(string formattedMessage)
        {
            CustomValidator validator = new CustomValidator();
            validator.ErrorMessage = formattedMessage;
            validator.Visible = false;
            validator.IsValid = false;
            validator.ValidationGroup = ValidationGroup;
            Controls.Add(validator);
        }

        private static void AddMessage(string message, bool htmlEncode, ref List<string> list)
        {
            if (htmlEncode)
            {
                message = HttpUtility.HtmlEncode(message);
            }

            if (list == null)
            {
                list = new List<string>(1);
            }

            list.Add(message);
        }
    }
}
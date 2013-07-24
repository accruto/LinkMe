using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    /// <summary>
    /// Checks that the text in the textbox to be validated is not longer than the maximum specified by the maxlength property.
    /// </summary>
    public class TextLengthValidator : LinkMeRegularExpressionValidator
    {
        private int minLength = 0;

        public TextLengthValidator()
        {
        }

        public int MinLength
        {
            get { return minLength; }
            set
            {
                if (minLength < 0)
                    throw new ArgumentOutOfRangeException("value", "The MinLength must not be less than 0.");

                minLength = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            TextBox textbox = GetTextBoxToValidate();

            // Either a MinLength must be set on this control or a MaxLength must be set on the textbox.

            if (MinLength <= 0 && textbox.MaxLength <= 0 && Enabled)
                throw new ApplicationException("The textbox to validate, '" + textbox.UniqueID + "', does not have a MaxLength set.");

            ValidationExpression = GetLengthValidationRegex(textbox);
            if (ValidationExpression.Length == 0)
            {
                EnableClientScript = false;
            }
        }

        protected override bool EvaluateIsValid()
        {
            TextBox textbox = GetTextBoxToValidate();
            string text = textbox.Text;

            // Note that if MinLength is set it is not considered an error for the textbox to be empty. This handles the case
            // of a field that is optional, but if specified must be at least x characters long. If the field is mandatory you
            // should also add a LinkMeRequiredFieldValidator.

            if (text.Length > 0 && text.Length < MinLength)
                return false;

            if (textbox.MaxLength > 0 && text.Length > textbox.MaxLength)
                return false;

            return true;
        }

        private TextBox GetTextBoxToValidate()
        {
            if (string.IsNullOrEmpty(ControlToValidate))
            {
                throw new ApplicationException("The ControlToValidate property has not been set for the '" + UniqueID +
                    "' " + GetType().Name + ".");
            }

            Control control = NamingContainer.FindControl(ControlToValidate);
            if (control == null)
            {
                throw new ApplicationException("The control to validate, '" + ControlToValidate +
                    "', could not be found.");
            }

            TextBox textBox = control as TextBox;
            if (textBox == null)
            {
                if (control is HtmlTextArea)
                {
                    throw new ApplicationException("The control to validate, '" + ControlToValidate +
                        "', is a server-side <textarea> - it must be a TextBox instead.");
                }
                else
                {
                    throw new ApplicationException("The control to validate, '" + ControlToValidate +
                        "', is not a TextBox.");
                }
            }

            return textBox;
        }

        private string GetLengthValidationRegex(TextBox textbox)
        {
            // If the textbox is multi-line or there is a minimum length set up a validation expression for client-side validation.
            // For a single-line textbox the maxlength property is enforced by the browser, so there's no need.

            if (textbox.TextMode == TextBoxMode.MultiLine || MinLength > 0)
                return GetLengthValidationRegex(MinLength, textbox.MaxLength);
            else
                return "";
        }

        private static string GetLengthValidationRegex(int minLength, int maxLength)
        {
            // JavaScript regexes interpret "." as any character EXCEPT a newline.

            return (maxLength > 0 ? @"^(.|\n){" + minLength + "," + maxLength + "}$" : @"^(.|\n){" + minLength + ",}$");
        }

#if DEBUG

        protected override bool RequiresValidationExpression
        {
            get { return false; }
        }

#endif
    }
}

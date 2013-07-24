using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class TextBoxFieldContents
        : UserControl
    {
        private bool _compulsory;
        private string _description;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /* ----------
         * PROPERTIES
         * ----------
         */

        /*
         * Label
         * 
         * The name of the field as it appears in the field's label.
         * 
         */
        public string Label
        {
            get { return lblField.Text; }
            set { lblField.Text = value; }
        }

        /*
         * Text
         * 
         * The field's value.
         * 
         */
        public string Text
        {
            get { return txtField.Text; }
            set { txtField.Text = value; }
        }

        /* 
         * Description
         * 
         * The name of the field when mentioned in text (like an error message).
         * 
         */
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;

                SetLengthErrorMessage();
                valFieldRequired.ErrorMessage = string.Format(ValidationErrorMessages.REQUIRED_FIELD_MISSING_GENERIC, "The " + value);
                valFieldRegex.ErrorMessage = string.Format(ValidationErrorMessages.INVALID_FIELD_GENERIC, value);
            }
        }

        public bool Enabled
        {
            get
            {
                return valFieldLength.Enabled || valFieldRequired.Enabled || valFieldRegex.Enabled;
            }
            set
            {
                valFieldLength.Enabled = value && (valFieldLength.MinLength > 0 || txtField.MaxLength > 0);
                valFieldRequired.Enabled = value && _compulsory;
                valFieldRegex.Enabled = value && !string.IsNullOrEmpty(valFieldRegex.ValidationExpression);
            }
        }

        public bool Required
        {
            get
            {
                return valFieldRequired.Enabled;
            }
            set
            {
                _compulsory = value;
                valFieldRequired.Enabled = value;
            }
        }

        public int MinLength
        {
            get
            {
                return valFieldLength.MinLength;
            }
            set
            {
                valFieldLength.MinLength = value;
                valFieldLength.Enabled = valFieldLength.MinLength > 0 || txtField.MaxLength > 0;
            }
        }

        public int MaxLength
        {
            get
            {
                return txtField.MaxLength;
            }
            set
            {
                txtField.MaxLength = value;
                valFieldLength.Enabled = valFieldLength.MinLength > 0 || txtField.MaxLength > 0;
                SetLengthErrorMessage();
            }
        }

        public string Regex
        {
            get
            {
                return valFieldRegex.ValidationExpression;
            }
            set
            {
                valFieldRegex.ValidationExpression = value;
                valFieldRegex.Enabled = !string.IsNullOrEmpty(valFieldRegex.ValidationExpression);
            }
        }

        public string TooltipText
        {
            get { return tooltipIcon.Text; }
            set { tooltipIcon.Text = value; }
        }

        public string Example
        {
            get
            {
                return litExample.Text;
            }
            set
            {
                litExample.Text = value;
                phExample.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public string TextBoxClientID
        {
            get { return txtField.ClientID; }
        }

        public TextBoxMode TextMode
        {
            get { return txtField.TextMode; }
            set { txtField.TextMode = value; }
        }

        public string CssClass
        {
            get { return divField.Attributes["class"]; }
            set { divField.Attributes["class"] = value; }
        }

        public string ControlCssClass
        {
            get { return divControl.Attributes["class"]; }
            set { divControl.Attributes["class"] = value; }
        }

        public string TextBoxCssClass
        {
            get { return txtField.CssClass; }
            set { txtField.CssClass = value; }
        }

        public string TextBoxRelativeUniqueID
        {
            get { return FieldInputHelper.GetRelativeId(txtField, this, IdSeparator); }
        }

        /* ----------------
         * INTERNAL METHODS
         * ----------------
         */

        private void SetLengthErrorMessage()
        {
            valFieldLength.ErrorMessage = string.Format(ValidationErrorMessages.MAX_LENGTH_EXCEEDED_FORMAT, Description, MaxLength);
        }
    }
}
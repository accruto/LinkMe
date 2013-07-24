using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class PasswordHashSaver : LinkMeUserControl, IValidator
    {
        private enum PasswordStatus
        {
            Unknown,
            Real,
            Placeholder,
            PlaceholderModified
        }

        // Use something really uncommon here that the user is exteremely unlikely to use in a password.
        private const char PasswordPlaceholderChar = '\xFFFC';

        private string _passwordHash;
        private PasswordStatus _status = PasswordStatus.Unknown;
        private string _errorMessage = "Please re-enter the password.";

        public string PasswordTextBoxes { get; set; }

        public string PasswordHash
        {
            get { return _passwordHash; }
        }

        #region IValidator Members

        public void Validate()
        {
            if (_status == PasswordStatus.Unknown)
            {
                SetStatus(FindPasswordTextBoxes()[0].Text);
            }
        }

        public bool IsValid
        {
            get { return (_status != PasswordStatus.PlaceholderModified); }
            set { throw new NotSupportedException(); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Page.Validators.Add(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack)
            {
                UpdatePasswordHash();
            }
            else
            {
                ClearHash();
            }
        }

        public void UpdatePasswordHash()
        {
            TextBox[] textboxes = FindPasswordTextBoxes();

            if (IsPasswordInputValid(textboxes))
            {
                ProcessValidPassword(textboxes);
            }
            else
            {
                ClearHash();
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            if (Page != null)
            {
                Page.Validators.Remove(this);
            }

            base.OnUnload(e);
        }

        private TextBox[] FindPasswordTextBoxes()
        {
            if (string.IsNullOrEmpty(PasswordTextBoxes))
            {
                throw new ApplicationException("The PasswordTextBoxes property must be set to one or more"
                    + " names of TextBoxes (comma-separated) containing the password to save.");
            }

            string[] ids = PasswordTextBoxes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(ids.Length > 0, "names.Length > 0");

            var textboxes = new TextBox[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                string id = ids[i].Trim();

                Control control = Parent.FindControl(id);
                if (control == null)
                {
                    throw new ApplicationException(string.Format("The PasswordTextBoxes property references a"
                        + " TextBox with ID '{0}', but there is no such control in the '{1}' control.",
                        id, Parent.UniqueID));
                }

                textboxes[i] = control as TextBox;
                if (textboxes[i] == null)
                {
                    throw new ApplicationException(string.Format("The PasswordTextBoxes property references a"
                        + " TextBox with ID '{0}', but the control with that ID is of type '{1}'.",
                        id, control.GetType().FullName));
                }
            }

            return textboxes;
        }

        private bool IsPasswordInputValid(TextBox[] textboxes)
        {
            Debug.Assert(!textboxes.IsNullOrEmpty(), "!EqualityExtensions.IsNullOrEmpty(textboxes)");

            // Check the basics - password is specified and matches.

            string password = textboxes[0].Text;
            if (string.IsNullOrEmpty(password))
                return false;

            for (int i = 1; i < textboxes.Length; i++)
            {
                if (textboxes[i].Text != password)
                    return false;
            }

            // Check any validators on the password textboxes.

            foreach (TextBox textbox in textboxes)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    var baseValidator = validator as BaseValidator;
                    if (baseValidator != null)
                    {
                        if (baseValidator.ControlToValidate == textbox.ID)
                        {
                            baseValidator.Validate();
                            if (!baseValidator.IsValid)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ProcessValidPassword(TextBox[] textboxes)
        {
            Debug.Assert(!textboxes.IsNullOrEmpty(), "!EqualityExtensions.IsNullOrEmpty(textboxes)");

            string password = textboxes[0].Text;
            SetStatus(password);

            switch (_status)
            {
                case PasswordStatus.Real:
                    // Use what's in the textboxes now (a real password) and save it to the hidden field for later.
                    SaveHash(password, textboxes);
                    break;

                case PasswordStatus.Placeholder:
                    // The password is a placeholder, so use the saved hash.
                    UseSavedHash(textboxes);
                    break;

                case PasswordStatus.PlaceholderModified:
                    // The password was a placeholder, but the user changed it. The parent page
                    // should make them re-enter it in this case.
                    ClearHash();
                    break;

                default:
                    Debug.Fail("Unexpected value of status: " + _status);
                    break;
            }
        }

        private void SetStatus(IEnumerable<char> password)
        {
            _status = PasswordStatus.Unknown;

            // If the password is all placeholder characters that's fine - use the saved hash, but if it's a mix
            // of placeholder and other characters then the user tried to modify it, so return a validation error.

            foreach (char c in password)
            {
                if (c == PasswordPlaceholderChar)
                {
                    if (_status == PasswordStatus.Real)
                    {
                        _status = PasswordStatus.PlaceholderModified;
                        break;
                    }
                    
                    _status = PasswordStatus.Placeholder;
                }
                else if (_status == PasswordStatus.Placeholder)
                {
                    _status = PasswordStatus.PlaceholderModified;
                    break;
                }
                else
                {
                    _status = PasswordStatus.Real;
                }
            }
        }

        private void SaveHash(string password, IEnumerable<TextBox> textboxes)
        {
            _passwordHash = LoginCredentials.HashToString(password);
            txtHiddenPasswordHash.Value = _passwordHash;

            // Set the textbox value to a placeholder of the same length, so it looks to the user like
            // their password is saved, but it's not actually stored in the page for better security.

            var placeholder = new string(PasswordPlaceholderChar, password.Length);
            foreach (TextBox textbox in textboxes)
            {
                SetPasswordText(textbox, placeholder);
            }
        }

        private static void SetPasswordText(WebControl textbox, string value)
        {
            textbox.Attributes.Add("value", value);
        }

        private void UseSavedHash(IEnumerable<TextBox> textboxes)
        {
            if (string.IsNullOrEmpty(txtHiddenPasswordHash.Value))
            {
                Debug.Fail("The password in the textbox is a placeholder, but no saved hash is stored.");

                _status = PasswordStatus.PlaceholderModified;
                ClearHash();

                return;
            }

            _passwordHash = txtHiddenPasswordHash.Value;

            // Set the placeholder again, otherwise the text will not be persisted between roundtrips
            // for security.

            foreach (TextBox textbox in textboxes)
            {
                SetPasswordText(textbox, textbox.Text);
            }
        }

        private void ClearHash()
        {
            _passwordHash = "";
            txtHiddenPasswordHash.Value = "";
            txtHiddenPasswordHash.Visible = false;
        }
    }
}
using System;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class Message : LinkMeUserControl
    {
        #region controls

        protected string toLabelText;

        #endregion

        #region Properties

        public bool ValidateTo
        {
            get { return reqValTo.Enabled; }
            set
            {
                reqValTo.Enabled = value;
                validatorTo.Enabled = value;
            }
        }

        public bool ValidateFrom
        {
            get { return reqValFrom.Enabled; }
            set
            {
                reqValFrom.Enabled = value;
                validatorFrom.Enabled = value;
            }
        }

        public bool IsToRequired
        {
            get { return reqValTo.Enabled; }
            set { reqValTo.Enabled = value; }
        }

        public bool IsFromRequired
        {
            get { return reqValFrom.Enabled; }
            set
            {
                reqValFrom.Enabled = value;
                lblFrom.CssClass = value ? "compulsory-small-form-label" : "small-form-label";
            }
        }

        public bool IsToEditable
        {
            get { return txtTo.Enabled; }
            set { txtTo.Enabled = value; }
        }

        public bool IsFromEditable
        {
            get { return txtFrom.Enabled; }
            set { txtFrom.Enabled = value; }
        }

        public bool IsToVisible
        {
            set { phTo.Visible = value; }
        }

        public bool IsFromVisible
        {
            set { phFrom.Visible = value; }
        }

        public string Subject
        {
            get { return txtSubject.Text; }
            set { txtSubject.Text = value; }
        }

        public bool ValidateSubject
        {
            get { return reqValSubject.Enabled; }
            set { reqValSubject.Enabled = value; }
        }

        public string Body
        {
            get { return txtBody.Text; }
            set { txtBody.Text = value; }
        }

        public string BodyRows
        {
            set { txtBody.Attributes["rows"] = value; }
        }

        public bool ValidateBody
        {
            get { return reqValBody.Enabled; }
            set { reqValBody.Enabled = value; }
        }

        public string ToLabelText
        {
            get { return toLabelText; }
            set { toLabelText = value; }
        }

        public string FromText
        {
            get { return txtFrom.Text; }
            set { txtFrom.Text = value; }
        }

        public bool IsSubjectVisible
        {
            set { phSubject.Visible = value;  }
        }

        #endregion

        public bool EnableClientValidation
        {
            set
            {
                reqValTo.EnableClientScript = value;
                reqValFrom.EnableClientScript = value;
                reqValSubject.EnableClientScript = value;
                validatorTo.EnableClientScript = value;
                validatorFrom.EnableClientScript = value;
                reqValBody.EnableClientScript = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitValidationControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                lblTo.Text = toLabelText;

                if (GrabFocusOnLoad)
                {
                    SetFocusOnControl(txtBody);
                }
            }
        }

        public bool GrabFocusOnLoad { get; set; }

        private void InitValidationControls()
		{
			reqValTo.ErrorMessage					= ValidationErrorMessages.REQUIRED_FIELD_TO;
		    reqValFrom.ErrorMessage                 = ValidationErrorMessages.REQUIRED_FIELD_FROM;
			reqValSubject.ErrorMessage				= ValidationErrorMessages.REQUIRED_FIELD_SUBJECT;
			reqValBody.ErrorMessage					= ValidationErrorMessages.REQUIRED_FIELD_BODY;
            validatorFrom.ErrorMessage              = ValidationErrorMessages.INVALID_EMAIL_FORMAT;
		}

        public void Clear(bool clearTo, bool clearSubject, bool clearBody, bool clearFrom)
        {
            if (clearTo) txtTo.Text = "";
            if (clearSubject) txtSubject.Text = "";
            if (clearBody) txtBody.Text = "";
            if (clearFrom) txtFrom.Text = "";
        }
    }
}

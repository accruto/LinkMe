using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[ToolboxData("<{0}:LinkMeRegularExpressionValidator runat=server></{0}:LinkMeRegularExpressionValidator>")]
	public class LinkMeRegularExpressionValidator : RegularExpressionValidator
	{
		private LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();
        private bool _showErrorTextInline;

        public LinkMeRegularExpressionValidator()
        {
            EnableViewState = false;
        }

	    [Browsable(false)]
		public override string Text
		{
			get { return (ShowErrorTextInline ? ErrorMessage : valDetails.Text); }
			set
			{
			}
		}

	    public bool ShowErrorTextInline
	    {
	        get { return _showErrorTextInline; }
	        set { _showErrorTextInline = value; }
	    }

	    [Bindable(false), Category("Appearance")]
		public string ErrorImage
		{
			get { return valDetails.ErrorImage; }
			set { valDetails.ErrorImage = value; }
		}

		[Bindable(false), Category("Appearance")]
		public string DesignModeText
		{
			get { return valDetails.DesignModeText; }
			set { valDetails.DesignModeText = value; }
		}

#if DEBUG
        // Check that the control has been initialised at the right time, which would allow us to disable the
        // viewstate.

        private bool initialisedInOnInit = false;

        protected virtual bool RequiresErrorMessage
        {
            get { return true; }
        }

        protected virtual bool RequiresValidationExpression
        {
            get { return Enabled; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Page.InitComplete += Page_InitComplete;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (RequiresValidationExpression)
            {
                if (string.IsNullOrEmpty(ValidationExpression))
                {
                    throw new ApplicationException("The '" + UniqueID + "' " + GetType().Name
                        + " does not have a ValidationExpression set.");
                }
                else if (!initialisedInOnInit)
                {
                    throw new ApplicationException("It appears that the '" + UniqueID
                        + "' " + GetType().Name + " was initialised in OnLoad or OnPreRender(). It must be"
                            + " initialised in OnInit() and this must be done even on postback.");
                }
            }

            if (RequiresErrorMessage && string.IsNullOrEmpty(ErrorMessage))
            {
                throw new ApplicationException("The '" + UniqueID + "' " + GetType().Name
                    + " does not have an ErrorMessage set.");
            }
        }

	    private void Page_InitComplete(object sender, EventArgs e)
	    {
            initialisedInOnInit = !string.IsNullOrEmpty(ValidationExpression);
	    }
#endif
	}
}
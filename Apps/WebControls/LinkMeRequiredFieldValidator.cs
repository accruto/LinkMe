using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[DefaultProperty("Text"), ToolboxData("<{0}:LinkMeRequiredFieldValidator runat=server></{0}:LinkMeRequiredFieldValidator>")]
	public class LinkMeRequiredFieldValidator : RequiredFieldValidator
	{
		private LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();

	    public LinkMeRequiredFieldValidator()
	    {
            EnableViewState = false;
	    }

	    [Browsable(false)]
		public override string Text
		{
			get { return valDetails.Text; }
			set{}
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Page.InitComplete += Page_InitComplete;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!initialisedInOnInit && !string.IsNullOrEmpty(ControlToValidate))
            {
                throw new ApplicationException("It appears that the '" + UniqueID
                    + "' " + GetType().Name + " was initialised in OnLoad or OnPreRender(). It must be"
                    + " initialised in OnInit() and this must be done even on postback.");
            }

            if (string.IsNullOrEmpty(ErrorMessage) && !RequiredValidatorWithErrorMessageExists())
            {
                // This is an error unless there happens to be another validator on the page for the same control,
                // which does have an error message set.

                throw new ApplicationException("The '" + UniqueID + "' " + GetType().Name
                    + " does not have an ErrorMessage set.");
            }
        }

	    private void Page_InitComplete(object sender, EventArgs e)
	    {
            initialisedInOnInit = !string.IsNullOrEmpty(ControlToValidate);
	    }

        private bool RequiredValidatorWithErrorMessageExists()
        {
            foreach (object validator in Page.Validators)
            {
                RequiredFieldValidator required = validator as RequiredFieldValidator;
                if (required != null && !string.IsNullOrEmpty(required.ErrorMessage)
                    && string.Equals(required.ControlToValidate, ControlToValidate, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

#endif
    }
}
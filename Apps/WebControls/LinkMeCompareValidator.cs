using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
	[DefaultProperty("Text"),
		ToolboxData("<{0}:LinkMeCompareValidator runat=server></{0}:LinkMeCompareValidator>")]
	public class LinkMeCompareValidator : CompareValidator
	{
		private LinkMeValidatorDetails valDetails = new LinkMeValidatorDetails();

	    public LinkMeCompareValidator()
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

            if (string.IsNullOrEmpty(ValueToCompare) && string.IsNullOrEmpty(ControlToCompare))
            {
                throw new ApplicationException("The '" + UniqueID + "' " + GetType().Name
                    + " does not have a ValueToCompare or ControlToCompare set.");
            }
            else if (!initialisedInOnInit)
            {
                throw new ApplicationException("It appears that the '" + UniqueID
                    + "' " + GetType().Name + " was initialised in OnLoad or OnPreRender(). It must be"
                    + " initialised in OnInit() and this must be done even on postback.");
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                throw new ApplicationException("The '" + UniqueID + "' " + GetType().Name
                    + " does not have an ErrorMessage set.");
            }
        }

	    private void Page_InitComplete(object sender, EventArgs e)
	    {
            initialisedInOnInit = !(string.IsNullOrEmpty(ValueToCompare) && string.IsNullOrEmpty(ControlToCompare));
	    }
#endif
    }
}
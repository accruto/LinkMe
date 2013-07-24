using System;

namespace LinkMe.Web.UI
{
    public abstract class EarlyInitControl : LinkMeUserControl
    {
        private bool initialised = false;

#if DEBUG
        private bool pageInitComplete = false;
#endif

        protected EarlyInitControl()
        {
        }

        protected void SetInitialised()
        {
#if DEBUG
            if (pageInitComplete)
            {
                throw new InvalidOperationException("The page has already finished initialising. The "
                    + GetType().BaseType.Name + " control must be initialised in OnInit() - not in OnLoad()"
                    + " or later.");
            }
#endif

            initialised = true;
        }

        protected void CheckInitialised()
        {
            if (!initialised)
            {
                throw new InvalidOperationException("The " + GetType().BaseType.Name + " control has not been"
                    + "  initialised. Note that this needs to be done in OnInit() and even on a postback.");
            }
        }

#if DEBUG
        protected override void OnInit(EventArgs e)
        {
            Page.InitComplete += Page_InitComplete;

            base.OnInit(e);
        }

        private void Page_InitComplete(object sender, EventArgs e)
        {
            pageInitComplete = true;
        }
#endif
    }
}

using System;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.UI.Unregistered.Autopeople;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class ViewCreditsExpiry
        : LinkMeUserControl
	{
        private static readonly ICreditsQuery _creditsQuery = Container.Current.Resolve<ICreditsQuery>();
        private static readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();

	    protected static ReadOnlyUrl PricingPageUrl
		{
            get { return ProductsRoutes.NewOrder.GenerateUrl(); }
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Automatically display the current user's products.

            if (LoggedInEmployer != null)
            {
                DisplayCredits(LoggedInEmployer);

                if (AutopeopleHelper.IsAutoPeople())
                {
                    phVerified.Visible = false;
                    phViewCreditsLink.Visible = false;
                }
                else
                {
                    phViewCreditsLink.Visible = true;
                    phVerified.Visible = LoggedInEmployer.Organisation.IsVerified;
                }
            }
        }

		private void DisplayCredits(IEmployer employer)
		{
            // Display these credits.

            var contactCredit = _creditsQuery.GetCredit<ContactCredit>();
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();

            var allocations = new[]
            {
                new Tuple<Credit, Allocation>(contactCredit, _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer)),
                new Tuple<Credit, Allocation>(applicantCredit, _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer)),
                new Tuple<Credit, Allocation>(jobAdCredit, _employerCreditsQuery.GetEffectiveActiveAllocation<JobAdCredit>(employer)),
            };

            rptCredits.DataSource = allocations;
			rptCredits.DataBind();
			phViewCredits.Visible = true;
		}

        protected string GetExpiryText(Allocation allocation)
        {
            return allocation == null || (allocation.RemainingQuantity != null && allocation.RemainingQuantity.Value == 0)
                ? "none available"
                : allocation.ExpiryDate.GetExpiryDateDisplayText();
        }

        protected string GetQuantityText(Allocation allocation)
        {
            return allocation == null || (allocation.RemainingQuantity != null && allocation.RemainingQuantity.Value == 0)
                ? ""
                : allocation.RemainingQuantity.GetQuantityDisplayText();
        }
    }
}

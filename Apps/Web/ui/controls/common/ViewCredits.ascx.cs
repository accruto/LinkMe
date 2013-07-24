using System;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class ViewCredits
        : LinkMeUserControl
	{
        private static readonly ICreditsQuery _creditsQuery = Container.Current.Resolve<ICreditsQuery>();
        private static readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();

		public void DisplayCredits(Employer employer)
		{
            litCaption.Text = "You currently have the following active credits:";

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
		}

	    protected static string GetItemText(Tuple<Credit, Allocation> item)
		{
	        var sb = new StringBuilder();
            sb.Append(item.Item1.ShortDescription).Append(": ");

            // Quantity.

            if (item.Item2 == null)
                sb.Append("none remaining");
            else if (item.Item2.IsUnlimited)
                sb.Append("unlimited");
            else if (item.Item2.RemainingQuantity.Value == 0)
                sb.Append("none remaining");
            else
                sb.Append(item.Item2.RemainingQuantity).Append(" remaining");

            // ExpiryDate.

            if (item.Item2 != null)
            {
                if (item.Item2.NeverExpires)
                    sb.Append(", never expiring");
                else if (item.Item2.ExpiryDate.Value != DateTime.MinValue)
                    sb.Append(", expiring on ").Append(item.Item2.ExpiryDate.Value.ToShortDateString());
            }

	        return sb.ToString();
		}
	}
}

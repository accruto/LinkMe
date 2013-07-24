using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Areas.Employers.Models.Candidates;

namespace LinkMe.Web.Areas.Employers.Views.Candidates
{
    public class Resume
        : ViewUserControl<ViewCandidatesModel>
    {
        protected string GetActionSuffix(CanContactStatus canContact)
        {
            switch (canContact)
            {
                case CanContactStatus.YesWithoutCredit:
                    return string.Empty;

                case CanContactStatus.YesWithCredit:
                    return "-locked";

                default:
                    return "-disabled";
            }
        }

        protected string GetActionSuffix(bool hasBeenAccessed, CanContactStatus canContact, CanContactStatus canContactByPhone)
        {
            switch (canContact)
            {
                case CanContactStatus.YesWithoutCredit:
                    if (hasBeenAccessed)
                    {
                        // If they have already been accessed then they either don't have a phone number
                        // or it is being shown to them.  In either case there is no need to enable the menu item.

                        return "-disabled";
                    }
                    
                    return canContactByPhone == CanContactStatus.No ? "-disabled" : "";

                case CanContactStatus.YesWithCredit:
                    return canContactByPhone == CanContactStatus.No ? "-disabled" : "-locked";

                default:
                    return "-disabled";
            }
        }
    }
}

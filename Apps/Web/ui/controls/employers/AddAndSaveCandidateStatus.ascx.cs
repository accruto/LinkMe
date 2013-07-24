using System;

namespace LinkMe.Web.UI.Controls.Employers
{
    /// <summary>
    /// Displays the status of (progress or result) of adding/moving a candidate to a folder/job ad using the
    /// AddCandidateToList control. The controls must be used together, but don't necessarily have to be next to each
    /// other (hence the need for two controls). The ItemSuffix for the two controls must be the same.
    /// </summary>
    public partial class AddAndSaveCandidateStatus : LinkMeUserControl
    {
        private string itemSuffix = "";

        public string ItemSuffix
        {
            get { return itemSuffix; }
            set { itemSuffix = value; }
        }

#if DEBUG

        internal static void RequireThisControl(LinkMePage page, Type requiringControl, string requiredItemSuffix)
        {
            if (page == null)
                throw new ArgumentNullException("page");
            if (requiringControl == null)
                throw new ArgumentNullException("requiringControl");

            // Make sure there's a "save status" control in the page with the same ItemSuffix.

            if (!page.ContainsControlOfType<AddAndSaveCandidateStatus>())
            {
                throw new ApplicationException("To use the " + requiringControl.Name + " control you must also have an "
                    + typeof(AddAndSaveCandidateStatus).Name + " control on the page (with the same ItemSuffix value).");
            }
        }

#endif
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Unity;
using Constants=LinkMe.Common.Constants;

namespace LinkMe.Web.UI.Controls.Employers
{
	public static class AddToItems
	{
	    private static readonly ICandidateFoldersQuery _candidateFoldersQuery = Container.Current.Resolve<ICandidateFoldersQuery>();
        private static readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();
        private static readonly IJobAdsQuery _jobAdsQuery = Container.Current.Resolve<IJobAdsQuery>();

        public const string SaveToNewValue = "SaveToNew";
        // ListItem value PREFIX for items that shouldn't trigger any action. The exact value should be different for each
        // item, otherwise Firefox tends to randomly change the selection on reloading the page.
        public const string BlankItemValue = "(none)";
        public const string RequireLoginValue = "RequireLogin";
        public const string RequirePurchaseValue = "RequirePurchase";

        private const string Separator = "---------------------";

	    public static ListItem[] GetCandidateListItems(Employer employer, string headingVerb, Guid? excludeListId,
            bool? excludeListIsDefaultList)
		{
            var addToItems = new List<ListItem>();

            headingVerb = headingVerb ?? "Save";
            var headingItem = new ListItem(headingVerb + " to folder", BlankItemValue + " - add");

            if (employer == null)
            {
                // No employer is logged in - return a placeholder "Save to your shortlist" item that redirects
                // them to RequireLogin.aspx.

                return new[]
                {
                    headingItem,
                    new ListItem(Separator, BlankItemValue + " - sep 1"),
                    new ListItem(Constants.DefaultCandidateListDisplayName, RequireLoginValue)
                };
            }

	        // If the caller didn't know whether the list to exclude is the default list do a DB query to check.

            if (excludeListId.HasValue && !excludeListIsDefaultList.HasValue)
            {
                var defaultList = _candidateFoldersQuery.GetShortlistFolder(employer);
                var defaultListId = defaultList.Id;
                excludeListIsDefaultList = defaultListId == excludeListId.Value;
            }

	        // The ListItem value is the candidate list ID. For the default candidate list the value is empty, because
            // it may not exist yet, so we don't have an ID (it's created when the first entry is added).

            addToItems.Add(headingItem);
            addToItems.Add(new ListItem(headingVerb + " to new folder", SaveToNewValue));

	        if (!excludeListId.HasValue || !excludeListIsDefaultList.Value)
            {
                addToItems.Add(new ListItem(Separator, BlankItemValue + " - sep 1"));
            }

            var folders = _candidateFoldersQuery.GetSharedFolders(employer);

            bool separatorAdded = false;
            foreach (var folder in folders)
            {
                if (folder.Id != excludeListId)
                {
                    if (!separatorAdded)
                    {
                        addToItems.Add(new ListItem(Separator, BlankItemValue + " - sep 2"));
                        separatorAdded = true;
                    }

                    addToItems.Add(new ListItem(folder.Name, folder.Id.ToString("n")));
                }
            }

            return addToItems.ToArray();
		}

        public static ListItem[] GetJobAdItems(Employer employer)
        {
            return GetJobAdItems(employer, null, null);
        }

        // The logic here gets somewhat complex:
        // If no employer is logged in show a placeholder item that takes them to LoginRequired.aspx
        // If an employer is logged in and they have job ads show them (of course)
        // If they have no job ads and no job ad credit show a placeholder that takes them to the "pricing" page
        // If they have no job ads, but have job ad credits just don't show anything (return null)
	    public static ListItem[] GetJobAdItems(Employer employer, Guid? excludeJobAdId, string headingText)
		{
            // Add the header item

            headingText = headingText ?? "Add to jobs";

            var addToItems = new List<ListItem> {new ListItem(headingText, BlankItemValue + " - add")};

	        if (employer == null)
            {
                AddJobAdItemsForNoJobAds(addToItems, false);
                return addToItems.ToArray();
            }

	        // For job ads the ListItem value is the job ad ID, since there may not be a candidate list yet - it's created
            // when the first candidate is added.

            var jobAdIds = (_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open).Concat(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed))).ToList();

            if (jobAdIds.Count == 0 || (excludeJobAdId.HasValue && jobAdIds.Count == 1 && jobAdIds[0] == excludeJobAdId))
            {
                var quantity = _employerCreditsQuery.GetEffectiveActiveAllocation<JobAdCredit>(employer).RemainingQuantity;
                if (quantity == null || quantity > 0)
                    return null;

                AddJobAdItemsForNoJobAds(addToItems, true);
                return addToItems.ToArray();
            }

            bool closedHeadingAdded = false;

            if (jobAdIds.Count != 0)
			{
                var jobAds = _jobAdsQuery.GetJobAds<JobAdEntry>(jobAdIds);
                if (jobAds[0].Status == JobAdStatus.Open)
                {
                    addToItems.Add(new ListItem("----- Active jobs -----", BlankItemValue + " - active"));
                }

				foreach (var jobAd in jobAds)
				{
                    if (!excludeJobAdId.HasValue || jobAd.Id != excludeJobAdId.Value)
                    {
                        if (!closedHeadingAdded && jobAd.Status == JobAdStatus.Closed)
                        {
                            // End of Active job ads, all following ads are Closed.

                            addToItems.Add(new ListItem("----- Closed jobs -----", BlankItemValue + " - closed"));
                            closedHeadingAdded = true;
                        }

                        addToItems.Add(new ListItem("  " + jobAd.Integration.ExternalReferenceId + " - " + jobAd.Title, jobAd.Id.ToString("n")));
                    }
				}
			}
			
			return addToItems.ToArray();
		}

	    private static void AddJobAdItemsForNoJobAds(ICollection<ListItem> addToItems, bool isEmployerLoggedIn)
	    {
            addToItems.Add(new ListItem(Separator, BlankItemValue + " - sep 1"));

            string itemValue = (isEmployerLoggedIn ? RequirePurchaseValue : RequireLoginValue);
            addToItems.Add(new ListItem("No jobs listed", itemValue));
	    }
	}
}

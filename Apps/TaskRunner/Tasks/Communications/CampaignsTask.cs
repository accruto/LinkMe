using System;
using System.Collections.Generic;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class CampaignsTask
        : SendCampaignTask
    {
        private readonly ICampaignsCommand _campaignsCommand = Container.Current.Resolve<ICampaignsCommand>();
        private readonly ICampaignsQuery _campaignsQuery = Container.Current.Resolve<ICampaignsQuery>();
        private readonly ICampaignCriteriaCommand _campaignCriteriaCommand = Container.Current.Resolve<ICampaignCriteriaCommand>();

        public CampaignsTask()
            : base(new EventSource<CampaignsTask>())
        {
        }

        public override void ExecuteTask()
        {
            ExecuteTask(null);
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";
            _eventSource.Raise(Event.FlowEnter, method, "Entering Campaigns task.");

            var runs = args != null && args.Length > 0 ? int.Parse(args[0]) : (int?)null;
            var batch = args != null && args.Length > 1 ? int.Parse(args[1]) : (int?)null;
            var wait = args != null && args.Length > 2 ? int.Parse(args[2]) : (int?)null;

            // Get all campaigns that are currently activated.

            var campaigns = _campaignsQuery.GetCampaigns(null, CampaignStatus.Activated);

            // Update the status for all campaigns first.

            UpdateStatus(campaigns, CampaignStatus.Running);

            try
            {
                foreach (var campaign in campaigns)
                    Execute(campaign, runs, batch, wait);
            }
            finally
            {
                // Update the status so they are at least in a reasonable state.

                UpdateStatus(campaigns, CampaignStatus.Stopped);
            }

            _eventSource.Raise(Event.FlowExit, method, "Exiting Campaigns task.");
        }

        private void UpdateStatus(IEnumerable<Campaign> campaigns, CampaignStatus status)
        {
            const string method = "UpdateStatus";

            foreach (var campaign in campaigns)
            {
                try
                {
                    _eventSource.Raise(Event.Information, method, string.Format("Setting the status for the {0} campaign to {1}...", campaign.Name, status));
                    _campaignsCommand.UpdateStatus(campaign, status);
                }
                catch (Exception ex)
                {
                    _eventSource.Raise(Event.Error, method, string.Format("Failed to update the status for the '{0}' campaign to {1}.", campaign.Name, status), ex, new StandardErrorHandler());
                }
            }
        }

        private void Execute(Campaign campaign, int? runs, int? batch, int? wait)
        {
            const string method = "Execute";

            try
            {
                _eventSource.Raise(Event.Flow, method, string.Format("Running the '{0}' campaign.", campaign.Name));

                // Get the criteria for the campaign to determine which set of users to send to.

                Send(campaign, GetUsers(campaign), runs, batch, wait);
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, string.Format("Failed to run the entire '{0}' campaign.", campaign.Name), ex, new StandardErrorHandler());
            }
        }

        private IList<RegisteredUser> GetUsers(Campaign campaign)
        {
            // Check the query first.

            if (!string.IsNullOrEmpty(campaign.Query))
                return _campaignCriteriaCommand.Match(campaign.Category, campaign.Query);
            
            var criteria = _campaignsQuery.GetCriteria(campaign.Id);
            return _campaignCriteriaCommand.Match(campaign.Category, criteria);
        }
    }
}

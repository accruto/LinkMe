using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class CampaignFileTask
        : SendCampaignTask
    {
        private static readonly EventSource EventSource = new EventSource<CampaignFileTask>();
        private readonly ICampaignsQuery _campaignsQuery = Container.Current.Resolve<ICampaignsQuery>();
        private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();

        public CampaignFileTask()
            : base(EventSource)
        {
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";
            EventSource.Raise(Event.FlowEnter, method, "Entering CampaignFile task.");

            if (args.Length < 2)
                throw new ArgumentException("Need at least 2 arguments, \"campaign file\"");

            var campaignName = args[0];
            var fileName = args[1];

            // Get the campaign.

            var campaign = _campaignsQuery.GetCampaign(campaignName);
            if (campaign == null)
                EventSource.Raise(Event.Error, method, string.Format("Cannot find campaign the '{0}' campaign.", campaignName));
            else
                Execute(campaign, fileName);

            EventSource.Raise(Event.FlowExit, method, "Exiting Campaigns task.");
        }

        private void Execute(Campaign campaign, string fileName)
        {
            const string method = "Execute";

            try
            {
                EventSource.Raise(Event.Flow, method, string.Format("Running the '{0}' campaign.", campaign.Name));

                // Open the file and read the login ids.

                var loginIds = new List<string>();
                using (var reader = new StreamReader(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        loginIds.Add(line);
                        line = reader.ReadLine();
                    }
                }

                EventSource.Raise(Event.Flow, method, string.Format("Found {0} login ids in file '{1}'.", loginIds.Count, fileName));

                // Get all members (maybe employers in the future).

                var members = _membersQuery.GetMembers(loginIds);
                Send(campaign, members.Cast<RegisteredUser>(), null, null, null);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, string.Format("Failed to run the entire '{0}' campaign.", campaign.Name), ex, new StandardErrorHandler());
            }
        }
    }
}

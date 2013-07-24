using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public abstract class SendCampaignTask
        : Task
    {
        private readonly ICampaignEmailsCommand _campaignEmailsCommand = Container.Current.Resolve<ICampaignEmailsCommand>();

        protected SendCampaignTask(EventSource eventSource)
            : base(eventSource)
        {
        }

        protected void Send(Campaign campaign, IEnumerable<RegisteredUser> users, int? runs, int? batch, int? wait)
        {
            const string method = "Send";

            try
            {
                var totalCount = users.Count();
                var totalSent = 0;
                _eventSource.Raise(Event.Flow, method, string.Format("Sending emails to {0} users for the '{1}' campaign.", totalCount, campaign.Name));

                var run = 0;
                var runSent = 0;

                foreach (var user in users)
                {
                    var email = _campaignEmailsCommand.CreateEmail(campaign, user);
                    var sent = _campaignEmailsCommand.Send(new[] {email}, campaign.Id, false);
                    runSent += sent;
                    totalSent += sent;

                    if (batch != null && runSent == batch.Value)
                    {
                        // Finished the run.

                        runSent = 0;
                        ++run;
                        if (runs != null && run == runs.Value)
                            break;

                        if (wait != null)
                            Thread.Sleep(wait.Value * 1000);
                    }
                }

                _eventSource.Raise(Event.Flow, method, string.Format("{0} emails have been sent for the '{1}' campaign.", totalSent, campaign.Name));
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, string.Format("Failed to run the entire '{0}' campaign.", campaign.Name), ex, new StandardErrorHandler());
            }
        }
    }
}

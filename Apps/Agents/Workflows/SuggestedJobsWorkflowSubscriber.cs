using System;
using System.Linq;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.PeriodicWorkflow;

namespace LinkMe.Apps.Agents.Workflows
{
    public class SuggestedJobsWorkflowSubscriber
    {
        private static readonly EventSource EventSource = new EventSource<SuggestedJobsWorkflowSubscriber>();

        private readonly IChannelManager<IService> _proxyManager;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly ISettingsQuery _settingsQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly Guid _categoryId;

        public SuggestedJobsWorkflowSubscriber(IChannelManager<IService> proxyManager, ICandidatesQuery candidatesQuery, ISettingsQuery settingsQuery, ISettingsCommand settingsCommand)
        {
            _proxyManager = proxyManager;
            _candidatesQuery = candidatesQuery;
            _settingsQuery = settingsQuery;
            _settingsCommand = settingsCommand;

            Category category = _settingsQuery.GetCategory("SuggestedJobs");
            if (category == null)
                throw new InvalidOperationException("The 'SuggestedJobs' category is not defined.");

            _categoryId = category.Id;
        }

        [SubscribesTo(Users.Members.PublishedEvents.MemberCreated)]
        public void OnMemberCreated(object sender, MemberCreatedEventArgs e)
        {
            var candidate = _candidatesQuery.GetCandidate(e.MemberId);
            HandleStatusChanged(candidate.Id, candidate.Status);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Candidates.PublishedEvents.PropertiesChanged)]
        public void OnStatusChanged(object sender, PropertiesChangedEventArgs e)
        {
            // Look for a status changed property event.

            var statusChangedEvents = e.PropertyChangedEvents.OfType<CandidateStatusChangedEventArgs>();
            foreach (var statusChangedEvent in statusChangedEvents)
                HandleStatusChanged(e.InstanceId, statusChangedEvent.To);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Communications.Settings.PublishedEvents.CategoryFrequencyUpdated)]
        public void OnFrequencyChanged(object sender, CategoryFrequencyEventArgs e)
        {
            // Skip unrelated events.

            if (e.CategoryId != _categoryId)
                return;

            // Notify the workflow on frequency change.

            var proxy = _proxyManager.Create();
            try
            {
                proxy.OnFrequencyChanged(e.UserId, e.Frequency.ToTimeSpan());
                _proxyManager.Close(proxy);
            }
            catch (Exception)
            {
                _proxyManager.Abort(proxy);
                throw;
            }
        }

        private void HandleStatusChanged(Guid candidateId, CandidateStatus status)
        {
            // SettingsCommand will fire the CategoryFrequencyUpdated event
            // that will be handled in this.OnFrequencyChanged() method.

            _settingsCommand.SetFrequency(candidateId, _categoryId, status.SuggestedJobsFrequency());
        }
    }
}
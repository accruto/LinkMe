using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.CandidateStatusWorkflow;

namespace LinkMe.Apps.Agents.Workflows
{
    public class CandidateWorkflowSubscriber
    {
        private readonly IChannelManager<IService> _proxyManager;
        private readonly ICandidatesQuery _candidatesQuery;

        public CandidateWorkflowSubscriber(IChannelManager<IService> proxyManager, ICandidatesQuery candidatesQuery)
        {
            _proxyManager = proxyManager;
            _candidatesQuery = candidatesQuery;
        }

        [SubscribesTo(Users.Members.PublishedEvents.MemberUpdated)]
        public void OnMemberChanged(object sender, EventArgs<Member> e)
        {
            var member = e.Value;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            HandleStatusChanged(candidate.Id, candidate.Status);
        }

        [SubscribesTo(PublishedEvents.CandidateUpdated)]
        public void OnCandidateChanged(object sender, EventArgs<Guid> e)
        {
            var id = e.Value;
            var candidate = _candidatesQuery.GetCandidate(id);
            HandleStatusChanged(candidate.Id, candidate.Status);
        }

        [SubscribesTo(PublishedEvents.PropertiesChanged)]
        public void OnStatusUpdated(object sender, PropertiesChangedEventArgs e)
        {
            // Look for a status changed property event.

            var statusChangedEvents = e.PropertyChangedEvents.OfType<CandidateStatusChangedEventArgs>();
            foreach (var statusChangedEvent in statusChangedEvents)
                HandleStatusChanged(e.InstanceId, statusChangedEvent.To);
        }

        private void HandleStatusChanged(Guid candidateId, CandidateStatus status)
        {
            var proxy = _proxyManager.Create();
            try
            {
                proxy.OnStatusChanged(candidateId, status);
                _proxyManager.Close(proxy);
            }
            catch (Exception)
            {
                _proxyManager.Abort(proxy);
                throw;
            }
        }
    }
}
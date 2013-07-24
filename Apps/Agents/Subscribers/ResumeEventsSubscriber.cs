using LinkMe.Apps.Agents.Users.Members.Handlers;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class ResumeEventsSubscriber
    {
        private readonly IResumesHandler _resumesHandler;

        public ResumeEventsSubscriber(IResumesHandler resumesHandler)
        {
            _resumesHandler = resumesHandler;
        }

        [SubscribesTo(PublishedEvents.ResumeUploaded)]
        public void OnResumeUploaded(object sender, ResumeEventArgs args)
        {
            _resumesHandler.OnResumeUploaded(args.CandidateId, args.ResumeId);
        }

        [SubscribesTo(PublishedEvents.ResumeReloaded)]
        public void OnResumeReloaded(object sender, ResumeEventArgs args)
        {
            _resumesHandler.OnResumeReloaded(args.CandidateId, args.ResumeId);
        }

        [SubscribesTo(PublishedEvents.ResumeEdited)]
        public void OnResumeEdited(object sender, ResumeEventArgs args)
        {
            _resumesHandler.OnResumeEdited(args.CandidateId, args.ResumeId, args.ResumeCreated);
        }
    }
}
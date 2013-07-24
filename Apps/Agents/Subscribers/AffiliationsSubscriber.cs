using LinkMe.Apps.Agents.Domain.Roles.Recruiters.Affiliations.Handlers;
using LinkMe.Domain.Roles.Recruiters.Affiliations;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class AffiliationsSubscriber
    {
        private readonly IAffiliationsHandler _affiliationsHandler;

        public AffiliationsSubscriber(IAffiliationsHandler affiliationsHandler)
        {
            _affiliationsHandler = affiliationsHandler;
        }

        // Communities

        [SubscribesTo(PublishedEvents.EnquiryCreated)]
        public void OnCommunityEnquiryCreated(object sender, EnquiryEventArgs args)
        {
            _affiliationsHandler.OnEnquiryCreated(args.AffiliateId, args.Enquiry);
        }
    }
}
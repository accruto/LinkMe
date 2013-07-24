using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters.Affiliations.Commands
{
    public class OrganisationAffiliationsCommand
        : IOrganisationAffiliationsCommand
    {
        private readonly IRecruitersRepository _repository;

        public OrganisationAffiliationsCommand(IRecruitersRepository repository)
        {
            _repository = repository;
        }

        void IOrganisationAffiliationsCommand.CreateEnquiry(Guid affiliateId, AffiliationEnquiry enquiry)
        {
            // Validate it.

            enquiry.Prepare();
            enquiry.Validate();

            // Save it.

            _repository.CreateAffiliationEnquiry(affiliateId, enquiry);

            // Fire events.

            var handlers = EnquiryCreated;
            if (handlers != null)
                handlers(this, new EnquiryEventArgs(affiliateId, enquiry));
        }

        [Publishes(PublishedEvents.EnquiryCreated)]
        public event EventHandler<EnquiryEventArgs> EnquiryCreated;
    }
}
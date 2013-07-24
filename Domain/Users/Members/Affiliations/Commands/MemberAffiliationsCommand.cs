using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Members.Affiliations.Commands
{
    public class MemberAffiliationsCommand
        : IMemberAffiliationsCommand
    {
        private readonly IMembersRepository _repository;

        public MemberAffiliationsCommand(IMembersRepository repository)
        {
            _repository = repository;
        }

        void IMemberAffiliationsCommand.SetAffiliation(Guid memberId, Guid? affiliateId)
        {
            _repository.SetAffiliation(memberId, affiliateId);

            // Fire events.

            var handlers = MemberChanged;
            if (handlers != null)
                handlers(this, new MemberEventArgs(memberId, affiliateId));
        }

        void IMemberAffiliationsCommand.SetItems(Guid memberId, Guid affiliateId, AffiliationItems items)
        {
            items.Prepare();
            items.Validate();
            _repository.SetAffiliationItems(memberId, affiliateId, items);
        }

        [Publishes(PublishedEvents.MemberChanged)]
        public event EventHandler<MemberEventArgs> MemberChanged;
    }
}
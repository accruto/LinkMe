using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Communities.Commands
{
    public class CommunitiesCommand
        : ICommunitiesCommand
    {
        private readonly ICommunitiesRepository _repository;

        public CommunitiesCommand(ICommunitiesRepository repository)
        {
            _repository = repository;
        }

        void ICommunitiesCommand.CreateCommunity(Community community)
        {
            community.Prepare();
            community.Validate();
            _repository.CreateCommunity(community);
        }

        void ICommunitiesCommand.UpdateCommunity(Community community)
        {
            community.Validate();
            _repository.UpdateCommunity(community);
        }
    }
}
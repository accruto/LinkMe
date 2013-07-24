using System;

namespace LinkMe.Domain.Roles.Representatives.Commands
{
    public class RepresentativesCommand
        : IRepresentativesCommand
    {
        private readonly IRepresentativesRepository _repository;

        public RepresentativesCommand(IRepresentativesRepository repository)
        {
            _repository = repository;
        }

        void IRepresentativesCommand.CreateRepresentative(Guid representeeId, Guid representativeId)
        {
            _repository.CreateRepresentative(representeeId, representativeId);
        }

        void IRepresentativesCommand.DeleteRepresentative(Guid representeeId, Guid representativeId)
        {
            _repository.DeleteRepresentative(representeeId, representativeId);
        }
    }
}
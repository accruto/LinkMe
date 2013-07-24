using System;

namespace LinkMe.Query.Members
{
    public interface IMemberSearchEngineCommand
        : ISearchEngineCommand
    {
    }

    public class MemberSearchEngineCommand
    : IMemberSearchEngineCommand
    {
        private readonly IMemberSearchEngineRepository _repository;

        public MemberSearchEngineCommand(IMemberSearchEngineRepository repository)
        {
            _repository = repository;
        }

        void ISearchEngineCommand.SetModified(Guid id)
        {
            _repository.SetModified(id);
        }
    }
}

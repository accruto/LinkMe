using System;

namespace LinkMe.Query.Resources
{
    public interface IResourceSearchEngineCommand
        : ISearchEngineCommand
    {
    }

    public class ResourceSearchEngineCommand
        : IResourceSearchEngineCommand
    {
        private readonly IResourceSearchEngineRepository _repository;

        public ResourceSearchEngineCommand(IResourceSearchEngineRepository repository)
        {
            _repository = repository;
        }

        void ISearchEngineCommand.SetModified(Guid id)
        {
            _repository.SetModified(id);
        }
    }
}

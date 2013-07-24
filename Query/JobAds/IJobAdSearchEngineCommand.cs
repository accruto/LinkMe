using System;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdSearchEngineCommand
        : ISearchEngineCommand
    {
    }

    public class JobAdSearchEngineCommand
        : IJobAdSearchEngineCommand
    {
        private readonly IJobAdSearchEngineRepository _repository;

        public JobAdSearchEngineCommand(IJobAdSearchEngineRepository repository)
        {
            _repository = repository;
        }

        void ISearchEngineCommand.SetModified(Guid id)
        {
            _repository.SetModified(id);
        }
    }
}
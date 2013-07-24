using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Apps.Agents.Applications.Commands
{
    public class TinyUrlCommand
        : ITinyUrlCommand
    {
        private readonly IApplicationsRepository _repository;

        public TinyUrlCommand(IApplicationsRepository repository)
        {
            _repository = repository;
        }

        void ITinyUrlCommand.CreateMappings(IEnumerable<TinyUrlMapping> mappings)
        {
            foreach (var mapping in mappings)
                mapping.Prepare();

            _repository.CreateMappings(mappings);
        }
    }
}
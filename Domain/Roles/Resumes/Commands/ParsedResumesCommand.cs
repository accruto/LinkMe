using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public class ParsedResumesCommand
        : IParsedResumesCommand
    {
        private readonly IResumesRepository _repository;

        public ParsedResumesCommand(IResumesRepository repository)
        {
            _repository = repository;
        }

        void IParsedResumesCommand.CreateParsedResume(ParsedResume parsedResume)
        {
            Prepare(parsedResume);
            parsedResume.Validate();
            _repository.CreateParsedResume(parsedResume);
        }

        void IParsedResumesCommand.DeleteParsedResume(Guid id)
        {
            _repository.DeleteParsedResume(id);
        }

        private static void Prepare(ParsedResume parsedResume)
        {
            if (parsedResume.Resume != null)
            {
                var now = DateTime.Now;
                parsedResume.Resume.CreatedTime = now;
                parsedResume.Resume.LastUpdatedTime = now;
            }
            if (parsedResume.Resume != null && parsedResume.Resume.IsEmpty)
                parsedResume.Resume = null;
            parsedResume.Prepare();
        }
    }
}

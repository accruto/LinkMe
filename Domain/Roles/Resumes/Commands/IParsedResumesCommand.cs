using System;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public interface IParsedResumesCommand
    {
        void CreateParsedResume(ParsedResume parsedResume);
        void DeleteParsedResume(Guid id);
    }
}

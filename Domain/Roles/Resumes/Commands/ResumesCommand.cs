using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    internal class ResumesCommand
        : IResumesCommand
    {
        private readonly IResumesRepository _repository;

        public ResumesCommand(IResumesRepository repository)
        {
            _repository = repository;
        }

        void IResumesCommand.CreateResume(Resume resume)
        {
            var now = DateTime.Now;
            resume.CreatedTime = now;
            resume.LastUpdatedTime = now;
            resume.Prepare();
            resume.Validate();
            _repository.CreateResume(resume);
        }

        void IResumesCommand.UpdateResume(Resume resume)
        {
            PrepareUpdate(resume);
            resume.Validate();
            _repository.UpdateResume(resume);
        }

        private static void PrepareUpdate(Resume resume)
        {
            resume.LastUpdatedTime = DateTime.Now;

            if (resume.Jobs != null)
            {
                foreach (var job in resume.Jobs)
                    job.Prepare();
            }

            if (resume.Schools != null)
            {
                foreach (var school in resume.Schools)
                    school.Prepare();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Ionic.Zip;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public class EmployerResumeFilesQuery
        : IEmployerResumeFilesQuery
    {
        private const string ZipFileName = "Resumes.zip";
        private readonly IResumesQuery _resumesQuery;
        private readonly IResumeFilesQuery _resumeFilesQuery;

        public EmployerResumeFilesQuery(IResumesQuery resumesQuery, IResumeFilesQuery resumeFilesQuery)
        {
            _resumesQuery = resumesQuery;
            _resumeFilesQuery = resumeFilesQuery;
        }

        DocFile IEmployerResumeFilesQuery.GetResumeFile(EmployerMemberView view)
        {
            // Need to figure out why there is a separate call to get the resume here and why not just use the one from the view ...

            var resume = _resumesQuery.GetResume(view.Resume.Id);
            var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);
            return new DocFile(resumeFile.FileName, resumeFile.Contents);
        }

        ZipFile IEmployerResumeFilesQuery.GetResumeFile(IEnumerable<EmployerMemberView> views)
        {
            var resumes = _resumesQuery.GetResumes(from v in views select v.Resume.Id);

            var zipFile = new ZipFile(ZipFileName);
            var resumeFileNames = new List<string>();

            foreach (var view in views)
            {
                var resume = (from r in resumes where r.Id == view.Resume.Id select r).Single();
                var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);
                zipFile.AddEntry(GetFileName(resumeFileNames, resumeFile.FileName), resumeFile.Contents);
            }

            return zipFile;
        }

        private static string GetFileName(ICollection<string> fileNames, string fileName)
        {
            // Make names unique.

            if (fileNames.Contains(fileName))
            {
                var index = 2;
                var newFileName = fileName + " (" + index + ")";
                while (fileNames.Contains(newFileName))
                {
                    ++index;
                    newFileName = fileName + " (" + index + ")";
                }

                fileNames.Add(newFileName);
                return newFileName;
            }

            fileNames.Add(fileName);
            return fileName;
        }
    }
}

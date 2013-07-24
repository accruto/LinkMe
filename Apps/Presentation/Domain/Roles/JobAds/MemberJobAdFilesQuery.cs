using System.Collections.Generic;
using Ionic.Zip;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds
{
    public class MemberJobAdFilesQuery
        : IMemberJobAdFilesQuery
    {
        private const string ZipFileName = "JobAds.zip";
        private readonly IJobAdFilesQuery _jobAdFilesQuery;

        public MemberJobAdFilesQuery(IJobAdFilesQuery jobAdFilesQuery)
        {
            _jobAdFilesQuery = jobAdFilesQuery;
        }

        DocFile IMemberJobAdFilesQuery.GetJobAdFile(JobAd jobAd)
        {
            var jobAdFile = _jobAdFilesQuery.GetJobAdFile(jobAd);
            return new DocFile(jobAdFile.FileName, jobAdFile.Contents);
        }

        ZipFile IMemberJobAdFilesQuery.GetJobAdFile(IEnumerable<JobAd> jobAds)
        {
            var zipFile = new ZipFile(ZipFileName);
            var jobAdFileNames = new List<string>();

            foreach (var jobAd in jobAds)
            {
                var jobAdFile = _jobAdFilesQuery.GetJobAdFile(jobAd);
                zipFile.AddEntry(GetFileName(jobAdFileNames, jobAdFile.FileName), jobAdFile.Contents);
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

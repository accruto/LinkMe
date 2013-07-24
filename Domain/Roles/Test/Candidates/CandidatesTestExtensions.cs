using System;
using System.IO;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Domain.Roles.Test.Candidates
{
    public static class CandidatesTestExtensions
    {
        private static readonly string TestResumeFile = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\Resume.xml", RuntimeEnvironment.GetSourceFolder());

        public static Resume AddTestResume(this ICandidateResumesCommand candidateResumesCommand, Candidate candidate)
        {
            return candidateResumesCommand.AddTestResume(candidate, null);
        }

        public static Resume AddTestResume(this ICandidateResumesCommand candidateResumesCommand, Candidate candidate, DateTime? lastUpdatedTime)
        {
            var xml = ReadFile(TestResumeFile);
            var resume = Container.Current.Resolve<IParseResumeXmlCommand>().ParseResumeXml(xml).Resume;
            resume.CreatedTime = lastUpdatedTime ?? DateTime.Now;
            resume.LastUpdatedTime = lastUpdatedTime ?? DateTime.Now;
            candidateResumesCommand.CreateResume(candidate, resume);
            return resume;
        }

        private static string ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

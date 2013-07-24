using System.IO;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public class ResumeViewText
    {
        public string Objective { get; private set; }
        public string SelfSummary { get; private set; }
        public string Skills { get; private set; }
        public string EmploymentDetails { get; private set; }
        public string EducationDetails { get; private set; }
        public string ProfessionalDetails { get; private set; }
        public string PersonalDetails { get; private set; }

        public ResumeViewText(EmployerMemberView view)
        {
            if (view.Resume == null)
                return;

            Objective = view.Resume.Objective;
            SelfSummary = view.Resume.Summary;
            Skills = view.Resume.Skills;
            EmploymentDetails = RenderEmploymentDetails(view.Resume);
            EducationDetails = RenderEducationDetails(view.Resume);
            ProfessionalDetails = RenderProfessionalDetails(view.Resume);
            PersonalDetails = RenderPersonalDetails(view.Resume);
        }

        private static string RenderEmploymentDetails(IResume resume)
        {
            if (resume.Jobs == null)
                return null;

            var writer = new StringWriter();

            for (int i = 0; i < resume.Jobs.Count; i++)
            {
                var job = resume.Jobs[i];
                if (i < 3)
                {
                    writer.WriteLine(job.Description);
                }
                else
                {
                    writer.WriteLine(job.Title);
                    writer.WriteLine(job.Company);
                    writer.WriteLine(job.Description);
                }
            }

            return writer.ToString();
        }

        private static string RenderEducationDetails(IResume resume)
        {
            var writer = new StringWriter();

            if (resume.Schools != null)
            {
                foreach (var school in resume.Schools)
                {
                    writer.WriteLine(school.Degree);
                    writer.WriteLine(school.Major);
                    writer.WriteLine(school.Institution);
                    writer.WriteLine(school.Description);
                }
            }

            if (resume.Courses != null)
            {
                foreach (var course in resume.Courses)
                    writer.WriteLine(course);
            }

            return writer.ToString();
        }

        private static string RenderProfessionalDetails(IResume resume)
        {
            var writer = new StringWriter();
            writer.WriteLine(resume.Professional);
            writer.WriteLine(resume.Affiliations);
            return writer.ToString();
        }

        private static string RenderPersonalDetails(IResume resume)
        {
            var writer = new StringWriter();
            writer.WriteLine(resume.Interests);
            writer.WriteLine(resume.Other);
            return writer.ToString();
        }

        public string RenderForSummarizing()
        {
            var writer = new StringWriter();

            writer.WriteLine(Objective);
            writer.WriteLine(SelfSummary);
            writer.WriteLine(Skills);
            writer.WriteLine(EmploymentDetails);
            writer.WriteLine(EducationDetails);
            writer.WriteLine(ProfessionalDetails);
            writer.WriteLine(PersonalDetails);

            var text = writer.ToString();
            return text;
        }
    }
}

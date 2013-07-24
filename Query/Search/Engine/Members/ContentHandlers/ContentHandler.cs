using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;
using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class ContentHandler
    {
        public void AddContent(Document document, BoostingDocumentBuilder docBuilder, MemberContent content)
        {
            if (content.Resume != null)
            {
                AddJobs(content, docBuilder, content.Resume.Jobs);
                AddSchools(docBuilder, content.Resume.Schools);
                AddCourses(docBuilder, content.Resume.Courses);
                AddAwards(docBuilder, content.Resume.Awards);

                docBuilder.AddText(FieldName.Content, content.Resume.Skills);
                docBuilder.AddText(FieldName.Content, content.Resume.Objective);
                docBuilder.AddText(FieldName.Content, content.Resume.Summary);
                docBuilder.AddText(FieldName.Content, content.Resume.Other);
                docBuilder.AddText(FieldName.Content, content.Resume.Affiliations);
                docBuilder.AddText(FieldName.Content, content.Resume.Professional);
                docBuilder.AddText(FieldName.Content, content.Resume.Interests);

                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Skills);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Objective);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Summary);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Other);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Affiliations);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Professional);
                docBuilder.AddText(FieldName.Content_Exact, content.Resume.Interests);
            }

            if (content.Candidate != null)
                AddDesiredJobTitle(document, docBuilder, content.Candidate.DesiredJobTitle);
        }

        private static void AddDesiredJobTitle(Document document, BoostingDocumentBuilder docBuilder, string desiredJobTitle)
        {
            if (!string.IsNullOrEmpty(desiredJobTitle))
            {
                document.add(new Field(FieldName.DesiredJobTitle, desiredJobTitle, Field.Store.NO, Field.Index.ANALYZED));
                document.add(new Field(FieldName.DesiredJobTitle_Exact, desiredJobTitle, Field.Store.NO, Field.Index.ANALYZED));

                docBuilder.AddText(FieldName.Content, desiredJobTitle);
                docBuilder.AddText(FieldName.Content_Exact, desiredJobTitle);
            }
        }

        private static void AddJobs(MemberContent content, BoostingDocumentBuilder docBuilder, IList<Job> jobs)
        {
            if (jobs == null)
                return;

            for (var index = 0; index < jobs.Count; index++)
            {
                var job = jobs[index];

                // Content field.

                docBuilder.AddText(FieldName.Content, job.Title);
                docBuilder.AddText(FieldName.Content, job.Description);

                docBuilder.AddText(FieldName.Content_Exact, job.Title);
                docBuilder.AddText(FieldName.Content_Exact, job.Description);

                // Job title fields.

                docBuilder.AddText(FieldName.JobTitle, job.Title);
                docBuilder.AddText(FieldName.JobTitle_Exact, job.Title);

                if (index == 0)
                {
                    docBuilder.AddText(FieldName.JobTitleLast, job.Title);
                    docBuilder.AddText(FieldName.JobTitleLast_Exact, job.Title);
                }

                if (index < 3)
                {
                    docBuilder.AddText(FieldName.JobTitleRecent, job.Title);
                    docBuilder.AddText(FieldName.JobTitleRecent_Exact, job.Title);
                }

                if (content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume) && content.Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers))
                {
                    docBuilder.AddText(FieldName.Content, job.Company);
                    docBuilder.AddText(FieldName.Content_Exact, job.Company);

                    // Employer fields.

                    docBuilder.AddText(FieldName.Employer, job.Company);
                    docBuilder.AddText(FieldName.Employer_Exact, job.Company);

                    if (index == 0)
                    {
                        docBuilder.AddText(FieldName.EmployerLast, job.Company);
                        docBuilder.AddText(FieldName.EmployerLast_Exact, job.Company);
                    }

                    if (index < 3)
                    {
                        docBuilder.AddText(FieldName.EmployerRecent, job.Company);
                        docBuilder.AddText(FieldName.EmployerRecent_Exact, job.Company);
                    }
                }
            }
        }

        private static void AddSchools(BoostingDocumentBuilder docBuilder, IEnumerable<School> schools)
        {
            if (schools == null)
                return;

            foreach (var school in schools)
            {
                // Content field.

                docBuilder.AddText(FieldName.Content, school.Institution);
                docBuilder.AddText(FieldName.Content, school.Degree);
                docBuilder.AddText(FieldName.Content, school.Major);
                docBuilder.AddText(FieldName.Content, school.Description);
                docBuilder.AddText(FieldName.Content, school.City);
                docBuilder.AddText(FieldName.Content, school.Country);

                docBuilder.AddText(FieldName.Content_Exact, school.Institution);
                docBuilder.AddText(FieldName.Content_Exact, school.Degree);
                docBuilder.AddText(FieldName.Content_Exact, school.Major);
                docBuilder.AddText(FieldName.Content_Exact, school.Description);
                docBuilder.AddText(FieldName.Content_Exact, school.City);
                docBuilder.AddText(FieldName.Content_Exact, school.Country);

                // Education field.

                docBuilder.AddText(FieldName.Education, school.Institution);
                docBuilder.AddText(FieldName.Education, school.Degree);
                docBuilder.AddText(FieldName.Education, school.Major);
                docBuilder.AddText(FieldName.Education, school.Description);
                docBuilder.AddText(FieldName.Education, school.City);
                docBuilder.AddText(FieldName.Education, school.Country);

                docBuilder.AddText(FieldName.Education_Exact, school.Institution);
                docBuilder.AddText(FieldName.Education_Exact, school.Degree);
                docBuilder.AddText(FieldName.Education_Exact, school.Major);
                docBuilder.AddText(FieldName.Education_Exact, school.Description);
                docBuilder.AddText(FieldName.Education_Exact, school.City);
                docBuilder.AddText(FieldName.Education_Exact, school.Country);
            }
        }

        private static void AddCourses(BoostingDocumentBuilder docBuilder, IEnumerable<string> courses)
        {
            if (courses == null)
                return;

            foreach (var course in courses)
            {
                docBuilder.AddText(FieldName.Content, course);
                docBuilder.AddText(FieldName.Content_Exact, course);

                docBuilder.AddText(FieldName.Education, course);
                docBuilder.AddText(FieldName.Education_Exact, course);
            }
        }

        private static void AddAwards(BoostingDocumentBuilder docBuilder, IEnumerable<string> awards)
        {
            if (awards == null)
                return;

            foreach (var award in awards)
            {
                docBuilder.AddText(FieldName.Content, award);
                docBuilder.AddText(FieldName.Content_Exact, award);

                docBuilder.AddText(FieldName.Education, award);
                docBuilder.AddText(FieldName.Education_Exact, award);
            }
        }
    }
}

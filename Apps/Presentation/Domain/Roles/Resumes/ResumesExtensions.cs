using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Apps.Presentation.Domain.Roles.Resumes
{
    public static class ResumesExtensions
    {
        public static string GetCoursesDisplayText(this IResume resume)
        {
            return resume == null
                ? null
                : GetDisplayText(resume.Courses);
        }

        public static string GetAwardsDisplayText(this IResume resume)
        {
            return resume == null
                ? null
                : GetDisplayText(resume.Awards);
        }

        public static IList<string> ParseCourses(this string courses)
        {
            return Parse(courses);
        }

        public static IList<string> ParseAwards(this string awards)
        {
            return Parse(awards);
        }

        public static bool HasJobs(this IResume resume)
        {
            return resume != null && resume.Jobs != null && resume.Jobs.Count != 0;
        }

        public static bool HasSchools(this IResume resume)
        {
            return resume != null && resume.Schools != null && resume.Schools.Count != 0;
        }

        public static bool HasObjective(this IResume resume)
        {
            return resume != null && resume.Objective != null && resume.Objective.Trim() != string.Empty;
        }

        public static bool HasAwards(this IResume resume)
        {
            return resume != null && resume.Awards != null && resume.Awards.Count != 0;
        }

        public static bool HasCourses(this IResume resume)
        {
            return resume != null && resume.Courses != null && resume.Courses.Count != 0;
        }

        public static bool HasInterests(this IResume resume)
        {
            return resume != null && resume.Interests != null && resume.Interests.Trim() != string.Empty;
        }

        public static bool HasProfessional(this IResume resume)
        {
            return resume != null && resume.Professional != null && resume.Professional.Trim() != string.Empty;
        }

        public static bool HasCitizenship(this IResume resume)
        {
            return resume != null && resume.Citizenship != null && resume.Citizenship.Trim() != string.Empty;
        }

        public static bool HasOther(this IResume resume)
        {
            return resume != null && resume.Other != null && resume.Other.Trim() != string.Empty;
        }

        public static bool HasReferees(this IResume resume)
        {
            return resume != null && resume.Referees != null && resume.Referees.Trim() != string.Empty;
        }

        public static bool HasAffiliations(this IResume resume)
        {
            return resume != null && resume.Affiliations != null && resume.Affiliations.Trim() != string.Empty;
        }

        public static bool HasSkills(this IResume resume)
        {
            return resume != null && resume.Skills != null && resume.Skills.Trim() != string.Empty;
        }

        public static bool HasSummary(this IResume resume)
        {
            return resume != null && resume.Summary != null && resume.Summary.Trim() != string.Empty;
        }

        public static string GetCurrentJobsDisplayHtml(this IResume resume)
        {
            return resume == null ? null : resume.CurrentJobs.GetJobsDisplayHtml();
        }

        public static string[] GetCurrentJobTitles(this IResume resume)
        {
            return resume == null ? null : resume.CurrentJobs.GetJobTitles();
        }

        private static string GetDisplayText(ICollection<string> collection)
        {
            if (collection == null || collection.Count == 0)
                return null;
            return string.Join("\n", collection.ToArray());
        }

        private static IList<string> Parse(string value)
        {
            return string.IsNullOrEmpty(value)
                ? null
                : value.Split(new[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
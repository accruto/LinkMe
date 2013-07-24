using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Apps.Presentation.Domain.Roles.Resumes
{
    public static class SchoolsExtensions
    {
        public static string GetCompletionDateDisplayText(this ISchool school)
        {
            if (school == null || school.CompletionDate == null || school.CompletionDate.End == null)
                return null;
            return school.CompletionDate.End.Value.ToString("MMMM yyyy");
        }
    }
}
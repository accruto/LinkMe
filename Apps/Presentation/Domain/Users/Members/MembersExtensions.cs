using System.Text;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Users.Members
{
    public static class MembersExtensions
    {
        public const string AnonymousNameText = "Anonymous member";

        public static string GetDisplayText(this EmployerMemberView view, bool nameOnly)
        {
            return view.GetDisplayText(view, view.Resume, nameOnly);
        }

        public static string GetPageTitle(this EmployerMemberView view)
        {
            var jobTitle = string.IsNullOrEmpty(view.DesiredJobTitle) ? string.Empty : view.DesiredJobTitle;

            if (jobTitle.Length >= 40)
            {
                // Truncate at the first space before 40 chars
                var lastSpace = jobTitle.Substring(0, 37).LastIndexOf(' ');
                jobTitle = string.Format("{0}...", jobTitle.Substring(0, lastSpace < 1 ? 37 : lastSpace));
            }

            var location = string.Empty;

            if (view.Address != null && view.Address.Location != null)
            {
                location = view.Address.Location.IsCountry ? view.Address.Location.Country.ToString() : view.Address.Location.ToString();
            }

            var availability = view.Status == CandidateStatus.AvailableNow || view.Status == CandidateStatus.ActivelyLooking ? "available now" : string.Empty;

            return string.Format("{0} {1} {2}|Candidates Available on LinkMe", jobTitle, location, availability);
        }

        public static string GetDisplayText(this IMember member, ICandidate candidate, IResume resume, bool nameOnly)
        {
            var sb = new StringBuilder();

            var currentJobTitles = resume == null ? null : resume.GetCurrentJobTitles();

            if (!string.IsNullOrEmpty(member.FirstName) && !string.IsNullOrEmpty(member.LastName))
            {
                sb.Append(member.FirstName + " " + member.LastName);
                if (nameOnly)
                    return sb.ToString();
            }
            else if (currentJobTitles != null && currentJobTitles.Length > 0)
            {
                sb.Append(currentJobTitles[0]);
            }
            else if (!candidate.Industries.IsNullOrEmpty())
            {
                sb.Append(candidate.Industries[0].Name);
            }

            if (member.Address.Location.IsCountry)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(member.Address.Location.Country.Name);
            }
            else
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(member.Address.Location.ToString());
            }

            if (sb.Length > 0)
                return sb.ToString();

            // It's possible that we have no details to show at all about this candidate.

            return AnonymousNameText;
        }

        public static string GetCandidateTitle(this EmployerMemberView view)
        {
            if (!string.IsNullOrEmpty(view.FullName))
                return view.FullName;
            
            var currentJobTitles = view.Resume == null ? null : view.Resume.GetCurrentJobTitles();

            if (currentJobTitles != null && currentJobTitles.Length > 0)
                return string.Join(", ", currentJobTitles);

            if (!view.DesiredJobTitle.IsNullOrEmpty())
                return view.DesiredJobTitle;

            return "<Name hidden>";
        }
    }
}

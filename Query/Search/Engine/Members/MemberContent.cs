using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Query.Search.Engine.Members
{
    public class MemberContent
        : Content
    {
        public Member Member { get; set; }
        public Candidate Candidate { get; set; }
        public Resume Resume { get; set; }
        public bool IncludeHidden { get; set; }

        public override bool IsSearchable
        {
            get
            {
                return (!IsHidden || IncludeHidden);
            }
        }

        private bool IsHidden
        {
            get
            {
                return !Member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                    || !Member.IsEnabled;
            }
        }
    }
}
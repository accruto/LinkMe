using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Users.Members.Status;

namespace LinkMe.Apps.Presentation.Domain.Users.Members
{
    public static class MemberStatusDisplay
    {
        private static readonly IDictionary<MemberItem, string> Texts = new Dictionary<MemberItem, string>
        {
            {MemberItem.Name, "Name"},
            {MemberItem.DesiredJob, "Desired job title"},
            {MemberItem.DesiredSalary, "Expected minimum salary"},
            {MemberItem.Address, "Location"},
            {MemberItem.EmailAddress, "Email address"},
            {MemberItem.PhoneNumber, "Phone number"},
            {MemberItem.Status, "Availability"},
            {MemberItem.Objective, "Career objectives"},
            {MemberItem.Industries, "Industry experience"},
            {MemberItem.Jobs, "Employment history"},
            {MemberItem.Schools, "Education"},
            {MemberItem.RecentProfession, "Recent profession"},
            {MemberItem.RecentSeniority, "Recent seniority"},
            {MemberItem.HighestEducationLevel, "Highest education level"},
            {MemberItem.VisaStatus, "Visa status"},
        };

        public static MemberItem[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this MemberItem item)
        {
            string text;
            return Texts.TryGetValue(item, out text) ? text : null;
        }
    }
}

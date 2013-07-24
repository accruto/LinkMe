using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public class GroupAbandonedEmail
        : MemberServicesEmail
    {
        private readonly string _groupName;

        public GroupAbandonedEmail(string groupName)
        {
            _groupName = groupName;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("GroupName", _groupName);
        }
    }
}
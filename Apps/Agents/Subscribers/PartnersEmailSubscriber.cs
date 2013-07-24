using System.Text;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.PartnerEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class PartnersEmailSubscriber
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IResourcesQuery _resourcesQuery;
        private readonly IMembersQuery _membersQuery;

        public PartnersEmailSubscriber(IEmailsCommand emailsCommand, IResourcesQuery resourcesQuery, IMembersQuery membersQuery)
        {
            _emailsCommand = emailsCommand;
            _resourcesQuery = resourcesQuery;
            _membersQuery = membersQuery;
        }

        [SubscribesTo(PublishedEvents.QuestionCreated)]
        public void OnQuestionCreated(object sender, ResourceQuestionEventArgs args)
        {
            var question = args.Question;
            var member = _membersQuery.GetMember(question.AskerId);
            var category = question.CategoryId.HasValue ? _resourcesQuery.GetCategory(question.CategoryId.Value).Name : string.Empty;
            _emailsCommand.TrySend(new NewResourceQuestionEmail(question.Text, member.GetPrimaryEmailAddress().Address, member.FullName, category));
        }
    }
}
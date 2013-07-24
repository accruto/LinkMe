using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.PartnerEmails
{
    public class NewResourceQuestionEmail
        :InternalEmail
    {
        private readonly string _question;
        private readonly string _askerEmailAddress;
        private readonly string _askerName;
        private readonly string _category;

        public NewResourceQuestionEmail(string question, string askerEmailAddress, string askerName, string category)
            : base(null)
        {
            _question = question;
            _askerEmailAddress = askerEmailAddress;
            _askerName = askerName;
            _category = category;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("AskerEmailAddress", _askerEmailAddress);
            properties.Add("AskerName", _askerName);
            properties.Add("ResourceCategory", _category);
            properties.Add("Question", _question);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}

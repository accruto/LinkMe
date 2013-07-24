using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Communications.Campaigns
{
    public class Template
    {
        [RemoveHtml]
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

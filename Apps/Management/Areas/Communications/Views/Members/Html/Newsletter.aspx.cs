using LinkMe.Apps.Management.Areas.Communications.Models.Members;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public class Newsletter
        : CommunicationsViewPage<NewsletterModel>
    {
        public Newsletter()
            : base(new EventSource<Newsletter>())
        {
        }
    }
}
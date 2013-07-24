using LinkMe.Apps.Management.Areas.Communications.Models.Employers;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Employers
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

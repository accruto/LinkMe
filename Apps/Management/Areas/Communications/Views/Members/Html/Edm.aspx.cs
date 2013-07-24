using LinkMe.Apps.Management.Areas.Communications.Models.Members;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public class Edm
        : CommunicationsViewPage<NewsletterModel>
    {
        public Edm()
            : base(new EventSource<Edm>())
        {
        }
    }
}
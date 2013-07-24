using LinkMe.Apps.Management.Areas.Communications.Models.Members;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public class Reengagement
        : CommunicationsViewPage<ReengagementModel>
    {
        public Reengagement()
            : base(new EventSource<Reengagement>())
        {
        }
    }
}
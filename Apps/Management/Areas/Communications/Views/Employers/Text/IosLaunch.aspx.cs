using LinkMe.Apps.Management.Areas.Communications.Models;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Employers.Text
{
    public class IosLaunch
        : CommunicationsViewPage<CommunicationsModel>
    {
        public IosLaunch()
            : base(new EventSource<IosLaunch>())
        {
        }
    }
}

using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.External.Commands
{
    public interface ISendApplicationsCommand
    {
        void SendApplication(InternalApplication application, IEnumerable<ApplicationAnswer> answers);
    }
}

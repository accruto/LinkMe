using System.Collections.Generic;
using LinkMe.Apps.Services.External.JobG8;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    public interface IMockJobG8Server
    {
        UploadResponseMessage Send(UploadRequestMessage request);
        IList<UploadRequestMessage> GetRequests();
        void ClearRequests();
    }
}

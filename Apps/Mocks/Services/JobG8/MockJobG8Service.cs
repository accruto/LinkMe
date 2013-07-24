using System.ServiceModel;
using LinkMe.Apps.Services.External.JobG8;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MockJobG8Service
        : IMockJobG8Service
    {
        public IMockJobG8Server JobG8Server { get; set; }

        UploadResponseMessage IMockJobG8Service.Send(UploadRequestMessage request)
        {
            return JobG8Server.Send(request);
        }
    }
}
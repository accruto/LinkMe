using System.ServiceModel;
using LinkMe.Apps.Services.External.JobG8;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    [ServiceContract]
    public interface IMockJobG8Service
    {
        [OperationContract]
        UploadResponseMessage Send(UploadRequestMessage request);
    }
}

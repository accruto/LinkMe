using System.Collections.Generic;
using LinkMe.Apps.Services.External.JobG8;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    public class MockJobG8Server
        : IMockJobG8Server
    {
        private static readonly List<UploadRequestMessage> Requests = new List<UploadRequestMessage>();

        UploadResponseMessage IMockJobG8Server.Send(UploadRequestMessage request)
        {
            Requests.Add(request);

            return new UploadResponseMessage { Body = new UploadResponseBody { Result = "SUCCESS" } };
        }

        IList<UploadRequestMessage> IMockJobG8Server.GetRequests()
        {
            return Requests;
        }

        void IMockJobG8Server.ClearRequests()
        {
            Requests.Clear();
        }
    }
}

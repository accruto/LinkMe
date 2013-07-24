using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LinkMe.Apps.Management.EmailStatus
{
    [ServiceContract]
    public interface IEmailStatusService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "*")]
        void AddNotification(Stream stream);
    }
}

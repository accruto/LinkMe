using System.Runtime.Serialization;
using System.ServiceModel;

namespace LinkMe.Apps.Services.External.JobG8
{
    [ServiceContract(Name = "ApplicationResponseSoap", Namespace = "http://jobg8.com/")]
    public interface IApplicationResponse
    {
        // CODEGEN: Generating message contract since element name applicationXml from namespace http://jobg8.com/ is not marked nillable
        [OperationContract(Action = "http://jobg8.com/ValidateApplicationResponse", ReplyAction = "*")]
        ValidateResponseMessage ValidateApplication(ValidateRequestMessage request);

        // CODEGEN: Generating message contract since element name applicationXml from namespace http://jobg8.com/ is not marked nillable
        [OperationContract(Action = "http://jobg8.com/UploadApplicationResponse", ReplyAction = "*")]
        UploadResponseMessage UploadApplication(UploadRequestMessage request);
    }

    [MessageContract(IsWrapped = false)]
    public class ValidateRequestMessage
    {
        [MessageBodyMember(Name = "ValidateApplicationResponse", Namespace = "http://jobg8.com/")]
        public ValidateRequestBody Body;
    }

    [DataContract(Namespace = "http://jobg8.com/")]
    public class ValidateRequestBody
    {
        [DataMember(Name = "applicationXml", EmitDefaultValue = false)]
        public string ApplicationXml;
    }

    [MessageContract(IsWrapped = false)]
    public class ValidateResponseMessage
    {
        [MessageBodyMember(Name = "ValidateApplicationResponseResponse", Namespace = "http://jobg8.com/")]
        public ValidateResponseBody Body;
    }

    [DataContract(Namespace = "http://jobg8.com/")]
    public class ValidateResponseBody
    {
        [DataMember(Name = "ValidateApplicationResponseResult", EmitDefaultValue = false)]
        public string Result;
    }

    [MessageContract(IsWrapped = false)]
    public class UploadRequestMessage
    {
        [MessageBodyMember(Name = "UploadApplicationResponse", Namespace = "http://jobg8.com/")]
        public UploadRequestBody Body;
    }

    [DataContract(Namespace = "http://jobg8.com/")]
    public class UploadRequestBody
    {
        [DataMember(Name = "applicationXml", EmitDefaultValue = false)]
        public string ApplicationXml;
    }

    [MessageContract(IsWrapped = false)]
    public class UploadResponseMessage
    {
        [MessageBodyMember(Name = "UploadApplicationResponseResponse", Namespace = "http://jobg8.com/")]
        public UploadResponseBody Body;
    }

    [DataContract(Namespace = "http://jobg8.com/")]
    public class UploadResponseBody
    {
        [DataMember(Name = "UploadApplicationResponseResult", EmitDefaultValue = false)]
        public string Result;
    }
}
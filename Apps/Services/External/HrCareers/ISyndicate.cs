using System.ServiceModel;

namespace LinkMe.Apps.Services.External.HrCareers
{
    [ServiceContract(Namespace = "http://component")]
    public interface ISyndicate
    {
        [OperationContract(Action = "", ReplyAction = "*", Name = "sync")]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        [return: MessageParameter(Name = "syncReturn")]
        string Sync(
            [MessageParameter(Name = "xmlData")] string jobsXml,
            [MessageParameter(Name = "provider")] string username,
            [MessageParameter(Name = "pwd")] string password);
    }
}

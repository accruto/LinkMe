using System;
using System.ServiceModel;

namespace LinkMe.Apps.Services.External.JobSearch
{
    [ServiceContract]
    public interface IJobAdExporter
    {
        [OperationContract(IsOneWay = true)]
        void Add(Guid jobAdId);

        [OperationContract(IsOneWay = true)]
        void Update(Guid jobAdId);

        [OperationContract(IsOneWay = true)]
        void Delete(Guid jobAdId);
    }
}

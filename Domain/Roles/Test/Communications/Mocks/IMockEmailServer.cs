using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    public interface IMockEmailServer
    {
        void Send(MockEmail email);
        IList<MockEmail> GetEmails();
        void ClearEmails();
        void RegisterBadEmailAddress(string badEmailAddress);
    }
}

using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Security.Queries
{
    public class LoginCredentialsQuery
        : ILoginCredentialsQuery
    {
        private readonly ISecurityRepository _repository;

        public LoginCredentialsQuery(ISecurityRepository repository)
        {
            _repository = repository;
        }

        Guid? ILoginCredentialsQuery.GetUserId(string loginId)
        {
            return _repository.GetLoginUserId(loginId);
        }

        string ILoginCredentialsQuery.GetLoginId(Guid userId)
        {
            return _repository.GetLoginId(userId);
        }

        IDictionary<Guid, string> ILoginCredentialsQuery.GetLoginIds(IEnumerable<Guid> userIds)
        {
            return _repository.GetLoginIds(userIds);
        }

        bool ILoginCredentialsQuery.DoCredentialsExist(LoginCredentials credentials)
        {
            return _repository.DoCredentialsExist(credentials);
        }

        LoginCredentials ILoginCredentialsQuery.GetCredentials(Guid userId)
        {
            return _repository.GetLoginCredentials(userId);
        }
    }
}
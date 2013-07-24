using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Users;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Agents.Security.Data
{
    public class SecurityRepository
        : Repository, ISecurityRepository
    {
        private static readonly Func<SecurityDataContext, Guid, RegisteredUserEntity> GetRegisteredUserEntity
            = CompiledQuery.Compile((SecurityDataContext dc, Guid id)
                => (from u in dc.RegisteredUserEntities
                    where u.id == id
                    select u).SingleOrDefault());

        private static readonly Func<SecurityDataContext, string, bool> DoesRegisteredUserExist
            = CompiledQuery.Compile((SecurityDataContext dc, string loginId)
                => (from u in dc.RegisteredUserEntities
                    where u.loginId == loginId
                    select u).Any());

        private static readonly Func<SecurityDataContext, Guid, LoginCredentials> GetLoginCredentials
            = CompiledQuery.Compile((SecurityDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    select u.Map()).SingleOrDefault());

        private static readonly Func<SecurityDataContext, string, Guid> GetLoginUserId
            = CompiledQuery.Compile((SecurityDataContext dc, string loginId)
                => (from u in dc.RegisteredUserEntities
                    where u.loginId == loginId
                    select u.id).SingleOrDefault());

        private static readonly Func<SecurityDataContext, Guid, string> GetLoginId
            = CompiledQuery.Compile((SecurityDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    select u.loginId).SingleOrDefault());

        private static readonly Func<SecurityDataContext, string, IQueryable<Tuple<Guid, string>>> GetLoginIds
            = CompiledQuery.Compile((SecurityDataContext dc, string userIds)
                => from u in dc.RegisteredUserEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, userIds) on u.id equals i.value
                   select new Tuple<Guid, string>(u.id, u.loginId));

        private static readonly Func<SecurityDataContext, Guid, Guid, ExternalUserEntity> GetExternalUserEntity
            = CompiledQuery.Compile((SecurityDataContext dc, Guid id, Guid providerId)
                => (from u in dc.ExternalUserEntities
                    where u.id == id
                    && u.externalProviderId == providerId
                    select u).SingleOrDefault());

        private static readonly Func<SecurityDataContext, Guid, string, bool> DoesExternalUserExist
            = CompiledQuery.Compile((SecurityDataContext dc, Guid providerId, string externalId)
                => (from u in dc.ExternalUserEntities
                    where u.externalProviderId == providerId
                    && u.externalId == externalId
                    select u).Any());

        private static readonly Func<SecurityDataContext, Guid, Guid, ExternalCredentials> GetExternalCredentials
            = CompiledQuery.Compile((SecurityDataContext dc, Guid userId, Guid providerId)
                => (from u in dc.ExternalUserEntities
                    where u.id == userId
                    && u.externalProviderId == providerId
                    select u.Map()).SingleOrDefault());

        private static readonly Func<SecurityDataContext, Guid, string, Guid> GetExternalUserId
            = CompiledQuery.Compile((SecurityDataContext dc, Guid providerId, string externalId)
                => (from u in dc.ExternalUserEntities
                    where u.externalProviderId == providerId
                    && u.externalId == externalId
                    select u.id).SingleOrDefault());

        public SecurityRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void ISecurityRepository.UpdateCredentials(Guid userId, LoginCredentials credentials)
        {
            try
            {
                using (var dc = CreateContext())
                {
                    var entity = GetRegisteredUserEntity(dc, userId);
                    if (entity != null)
                    {
                        credentials.MapTo(entity);
                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Now look for a duplicate error.

                if (ex is SqlException && ex.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.RegisteredUserLoginId' with unique index "))
                    throw new DuplicateUserException();
                throw;
            }
        }

        bool ISecurityRepository.DoCredentialsExist(LoginCredentials credentials)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return DoesRegisteredUserExist(dc, credentials.LoginId);
            }
        }

        LoginCredentials ISecurityRepository.GetLoginCredentials(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // Could not get LINQ-SQL working properly with IS NOT NULL so doing this ugly thing for now.

                var credentials = GetLoginCredentials(dc, userId);
                return credentials == null
                    ? null
                    : credentials.LoginId == null
                        ? null
                        : credentials;
            }
        }

        Guid? ISecurityRepository.GetLoginUserId(string loginId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var userId = GetLoginUserId(dc, loginId);
                return userId == Guid.Empty ? (Guid?)null : userId;
            }
        }

        string ISecurityRepository.GetLoginId(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLoginId(dc, userId);
            }
        }

        IDictionary<Guid, string> ISecurityRepository.GetLoginIds(IEnumerable<Guid> userIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLoginIds(dc, new SplitList<Guid>(userIds).ToString()).ToDictionary(a => a.Item1, a => a.Item2);
            }
        }

        void ISecurityRepository.UpdateCredentials(Guid userId, ExternalCredentials credentials)
        {
            using (var dc = CreateContext())
            {
                var entity = GetExternalUserEntity(dc, userId, credentials.ProviderId);
                if (entity != null)
                    credentials.MapTo(entity);
                else
                    dc.ExternalUserEntities.InsertOnSubmit(credentials.Map(userId));
                dc.SubmitChanges();
            }
        }

        void ISecurityRepository.DeleteCredentials(Guid userId, Guid providerId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetExternalUserEntity(dc, userId, providerId);
                if (entity != null)
                {
                    dc.ExternalUserEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        bool ISecurityRepository.DoCredentialsExist(ExternalCredentials credentials)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return DoesExternalUserExist(dc, credentials.ProviderId, credentials.ExternalId);
            }
        }

        ExternalCredentials ISecurityRepository.GetExternalCredentials(Guid userId, Guid providerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExternalCredentials(dc, userId, providerId);
            }
        }

        Guid? ISecurityRepository.GetExternalUserId(Guid providerId, string externalId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var userId = GetExternalUserId(dc, providerId, externalId);
                return userId == Guid.Empty ? (Guid?)null : userId;
            }
        }

        private SecurityDataContext CreateContext()
        {
            return CreateContext(c => new SecurityDataContext(c));
        }
    }
}

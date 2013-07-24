using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Agents.Profiles.Data
{
    public class ProfilesRepository
        : Repository, IProfilesRepository
    {
        private static readonly Func<ProfilesDataContext, Guid, EmployerProfileEntity> GetEmployerProfileEntity
            = CompiledQuery.Compile((ProfilesDataContext dc, Guid employerId)
                => (from p in dc.EmployerProfileEntities
                    where p.employerId == employerId
                    select p).SingleOrDefault());

        private static readonly Func<ProfilesDataContext, Guid, EmployerProfile> GetEmployerProfile
            = CompiledQuery.Compile((ProfilesDataContext dc, Guid employerId)
                => (from p in dc.EmployerProfileEntities
                    where p.employerId == employerId
                    select p.Map()).SingleOrDefault());

        private static readonly Func<ProfilesDataContext, Guid, MemberProfileEntity> GetMemberProfileEntity
            = CompiledQuery.Compile((ProfilesDataContext dc, Guid memberId)
                => (from p in dc.MemberProfileEntities
                    where p.memberId == memberId
                    select p).SingleOrDefault());

        private static readonly Func<ProfilesDataContext, Guid, MemberProfile> GetMemberProfile
            = CompiledQuery.Compile((ProfilesDataContext dc, Guid memberId)
                => (from p in dc.MemberProfileEntities
                    where p.memberId == memberId
                    select p.Map()).SingleOrDefault());

        public ProfilesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IProfilesRepository.UpdateEmployerProfile(Guid employerId, EmployerProfile profile)
        {
            using (var dc = CreateContext())
            {
                // Create or update as needed.

                var entity = GetEmployerProfileEntity(dc, employerId);
                if (entity == null)
                    dc.EmployerProfileEntities.InsertOnSubmit(profile.Map(employerId));
                else
                    profile.MapTo(entity);
                dc.SubmitChanges();
            }
        }

        EmployerProfile IProfilesRepository.GetEmployerProfile(Guid employerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmployerProfile(dc, employerId);
            }
        }

        void IProfilesRepository.UpdateMemberProfile(Guid memberId, MemberProfile profile)
        {
            using (var dc = CreateContext())
            {
                // Create or update as needed.

                var entity = GetMemberProfileEntity(dc, memberId);
                if (entity == null)
                    dc.MemberProfileEntities.InsertOnSubmit(profile.Map(memberId));
                else
                    profile.MapTo(entity);
                dc.SubmitChanges();
            }
        }

        MemberProfile IProfilesRepository.GetMemberProfile(Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberProfile(dc, memberId);
            }
        }

        private ProfilesDataContext CreateContext()
        {
            return CreateContext(c => new ProfilesDataContext(c));
        }
    }
}

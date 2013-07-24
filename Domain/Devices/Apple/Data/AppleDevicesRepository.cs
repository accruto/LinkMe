using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Devices.Apple.Data
{
    public class AppleDevicesRepository
        : Repository, IAppleDevicesRepository
    {
        private static readonly Func<AppleDevicesDataContext, Guid, UserAppleDeviceEntity> GetUserAppleDeviceEntityQuery
            = CompiledQuery.Compile((AppleDevicesDataContext dc, Guid id)
                => (from e in dc.UserAppleDeviceEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<AppleDevicesDataContext, Guid, AppleDevice> GetUserAppleDeviceQuery
            = CompiledQuery.Compile((AppleDevicesDataContext dc, Guid id)
                => (from e in dc.UserAppleDeviceEntities
                    where e.id == id
                    select e.Map()).SingleOrDefault());

        private static readonly Func<AppleDevicesDataContext, string, AppleDevice> GetUserAppleDeviceByTokenQuery
            = CompiledQuery.Compile((AppleDevicesDataContext dc, string deviceToken)
                => (from e in dc.UserAppleDeviceEntities
                    where e.deviceToken == deviceToken
                    select e.Map()).SingleOrDefault());

        private static readonly Func<AppleDevicesDataContext, Guid, IQueryable<AppleDevice>> GetAppleDevicesByUserIdQuery
            = CompiledQuery.Compile((AppleDevicesDataContext dc, Guid userId)
                => from e in dc.UserAppleDeviceEntities
                   where e.ownerId == userId
                   select e.Map());


        public AppleDevicesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IAppleDevicesRepository.CreateUserAppleDevice(AppleDevice userDevice)
        {
            using (var dc = CreateContext())
            {
                dc.UserAppleDeviceEntities.InsertOnSubmit(userDevice.Map());

                dc.SubmitChanges();
            }
        }

        void IAppleDevicesRepository.UpdateUserAppleDevice(AppleDevice userDevice)
        {
            using (var dc = CreateContext())
            {
                var entity = GetUserAppleDeviceEntityQuery(dc, userDevice.Id);
                if (entity != null)
                {
                    userDevice.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        AppleDevice IAppleDevicesRepository.GetUserAppleDevice(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUserAppleDeviceQuery(dc, id);
            }
        }

        AppleDevice IAppleDevicesRepository.GetUserAppleDevice(string deviceToken)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUserAppleDeviceByTokenQuery(dc, deviceToken);
            }
        }

        IList<AppleDevice> IAppleDevicesRepository.GetUserAppleDevices(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAppleDevicesByUserIdQuery(dc, userId).ToList();
            }
        }

        private AppleDevicesDataContext CreateContext()
        {
            return CreateContext(c => new AppleDevicesDataContext(c));
        }
    }
}
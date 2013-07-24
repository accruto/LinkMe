using System;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Commands
{
    public class UpdateMemberSearchCommand
        : IUpdateMemberSearchCommand
    {
        private readonly IChannelManager<IMemberSearchService> _serviceManager;

        public UpdateMemberSearchCommand(IChannelManager<IMemberSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        void IUpdateMemberSearchCommand.ClearAll()
        {
            var service = _serviceManager.Create();
            try
            {
                service.Clear();
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }

        void IUpdateMemberSearchCommand.AddMember(Guid memberId)
        {
            var service = _serviceManager.Create();
            try
            {
                service.IndexMember(memberId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }

        void IUpdateMemberSearchCommand.RemoveMember(Guid memberId)
        {
            var service = _serviceManager.Create();
            try
            {
                service.UnindexMember(memberId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }
    }
}
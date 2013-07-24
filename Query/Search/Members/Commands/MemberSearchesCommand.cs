using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.Members.Commands
{
    public class MemberSearchesCommand
        : IMemberSearchesCommand
    {
        private const int MaxExecutionResults = 10;

        private readonly IMembersRepository _repository;

        public MemberSearchesCommand(IMembersRepository repository)
        {
            _repository = repository;
        }

        void IMemberSearchesCommand.CreateMemberSearch(IUser owner, MemberSearch search)
        {
            search.OwnerId = owner.Id;
            search.Prepare();
            search.Validate();
            _repository.CreateMemberSearch(search);
        }

        void IMemberSearchesCommand.UpdateMemberSearch(IUser owner, MemberSearch search)
        {
            if (!CanAccessSearch(owner, search))
                throw new MemberSearchesPermissionsException(owner, search.Id);

            if (search.Criteria != null)
                search.Criteria.Prepare();
            search.Validate();
            _repository.UpdateMemberSearch(search);
        }

        void IMemberSearchesCommand.DeleteMemberSearch(IUser owner, Guid id)
        {
            var search = _repository.GetMemberSearch(id);
            if (search != null)
            {
                if (!CanAccessSearch(owner, search))
                    throw new MemberSearchesPermissionsException(owner, search.Id);

                _repository.DeleteMemberSearch(id);
            }
        }

        void IMemberSearchesCommand.CreateMemberSearchExecution(MemberSearchExecution execution)
        {
            execution.Prepare();
            execution.Validate();
            _repository.CreateMemberSearchExecution(execution, MaxExecutionResults);
        }

        private static bool CanAccessSearch(IHasId<Guid> owner, MemberSearch search)
        {
            if (owner == null)
                return false;
            return owner.Id == search.OwnerId;
        }
    }
}
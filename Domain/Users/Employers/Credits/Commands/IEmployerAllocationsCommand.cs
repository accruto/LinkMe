using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;

namespace LinkMe.Domain.Users.Employers.Credits.Commands
{
    public interface IEmployerAllocationsCommand
    {
        void CreateAllocation(Allocation allocation);
        void Deallocate(Guid allocationId);

        void EnsureJobAdCredits(IEmployer employer);
    }
}

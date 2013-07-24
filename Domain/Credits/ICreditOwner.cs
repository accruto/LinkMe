using System;

namespace LinkMe.Domain.Credits
{
    public interface ICreditOwner
    {
        Guid Id { get; }
        string FullName { get; }
    }
}

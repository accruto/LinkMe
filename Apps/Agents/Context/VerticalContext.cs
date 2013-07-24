using System;
using LinkMe.Domain.Roles.Affiliations.Verticals;

namespace LinkMe.Apps.Agents.Context
{
    public interface IVerticalContext
        : ISubActivityContext
    {
        void Set(Vertical vertical);
        void Reset();
        Guid? Id { get; }
    }

    internal class DefaultVerticalContext
        : IVerticalContext
    {
        private Vertical _vertical;

        void IVerticalContext.Set(Vertical vertical)
        {
            _vertical = vertical;
        }

        void IVerticalContext.Reset()
        {
            _vertical = null;
        }

        Guid? IVerticalContext.Id
        {
            get { return _vertical == null ? (Guid?)null : _vertical.Id; }
        }
    }
}
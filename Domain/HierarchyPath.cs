using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Domain
{
    public class HierarchyPath
        : IEnumerable<Guid>
    {
        private readonly List<Guid> _ownerIds;

        public HierarchyPath(Guid ownerId)
        {
            _ownerIds = new List<Guid> {ownerId};
        }

        public HierarchyPath(params Guid[] ownerIds)
        {
            _ownerIds = ownerIds == null || ownerIds.Length == 0
                ? new List<Guid>()
                : new List<Guid>(ownerIds);
        }

        public HierarchyPath(IEnumerable<Guid> ownerIds)
        {
            _ownerIds = ownerIds == null
                ? new List<Guid>()
                : new List<Guid>(ownerIds);
        }

        IEnumerator<Guid> IEnumerable<Guid>.GetEnumerator()
        {
            return _ownerIds.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ownerIds.GetEnumerator();
        }

        protected List<Guid> OwnerIds
        {
            get { return _ownerIds; }
        }
    }
}
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public class PaginatedItemParts<TItem, TPart>
    {
        private readonly TItem _item;
        private readonly PaginatedList<TPart> _parts;

        public PaginatedItemParts(TItem item, int maxPerPage, int currentPage, IEnumerable<TPart> parts)
        {
            _item = item;
            _parts = new PaginatedList<TPart>(maxPerPage, currentPage, parts);
        }

        public TItem Item
        {
            get { return _item; }
        }

        public PaginatedList<TPart> Parts
        {
            get { return _parts; }
        }
    }
}

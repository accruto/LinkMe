using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public abstract class PaginatedList
    {
        private readonly int _currentPage;
        private readonly int _maxPerPage;
        private readonly int _totalItems;
        private readonly int _totalPages;

        protected PaginatedList(int maxPerPage, int totalItems, int currentPage)
        {
            _maxPerPage = maxPerPage;
            _totalItems = totalItems;
            _totalPages = GetTotalPages(totalItems, maxPerPage);
            _currentPage = currentPage;
        }

        public int MaxPerPage
        {
            get { return _maxPerPage; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
        }

        public int TotalItems
        {
            get { return _totalItems; }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
        }

        private static int GetTotalPages(int totalItems, int maxPerPage)
        {
            if ((totalItems % maxPerPage) == 0)
                return (totalItems / maxPerPage);
            else
                return (totalItems / maxPerPage) + 1;
        }
    }

    public class PaginatedList<TItem>
        : PaginatedList
    {
        private readonly IEnumerable<TItem> _currentItems;

        public PaginatedList(int maxPerPage, int totalItems, int currentPage, IEnumerable<TItem> currentItems)
            : base(maxPerPage, totalItems, currentPage)
        {
            _currentItems = currentItems;
        }

        public PaginatedList(int maxPerPage, int currentPage, IEnumerable<TItem> allItems)
            : base(maxPerPage, allItems.Count(), currentPage)
        {
            _currentItems = allItems.Skip(maxPerPage * (currentPage - 1)).Take(maxPerPage);
        }

        public IEnumerable<TItem> CurrentItems
        {
            get { return _currentItems; }
        }
    }
}

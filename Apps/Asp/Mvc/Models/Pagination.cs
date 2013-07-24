using LinkMe.Framework.Utility.Results;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public class Pagination
    {
        public int? Page { get; set; }
        public int? Items { get; set; }

        public Range ToRange()
        {
            // Convert page + items to skip + take.
            // It is assumed that neither of these values are null.

            if (Page == null || Items == null)
                return new Range();

            var skip = (Page.Value - 1) * Items.Value;
            var take = Items.Value;
            return new Range(skip, take);
        }

        public int? GetMaxItemCount()
        {
            var range = ToRange();
            if (range == null || range.Take == null)
                return null;
            return range.Skip + range.Take.Value;
        }
    }
}
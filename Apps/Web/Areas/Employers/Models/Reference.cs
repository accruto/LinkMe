namespace LinkMe.Web.Areas.Employers.Models
{
    public static class Reference
    {
        public const int DefaultItemsPerPage = 25;
        public static readonly int[] ItemsPerPage = new[] { 10, 25, 50, 100 };
        public static readonly int[] Distances = new[] { 0, 5, 10, 20, 50, 100, 200, 500, 1000 };
        public static int[] RecencyDays = new[] { 1, 7, 14, 30, 61, 91, 183, 365, 548, 731 };
        public const int SpellingThreshold = 10;
        public const int MoreResultsThreshold = 5;
    }
}
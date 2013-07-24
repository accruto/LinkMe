namespace LinkMe.Web.Areas.Members.Models
{
    public static class Reference
    {
        public const int DefaultItemsPerPage = 10;      //This MUST stay at 10 to support the old browse job ad links which include the index in the URL
        public static readonly int[] ItemsPerPage = new[] { 10, 25, 50, 100 };
        public static readonly int[] Distances = new[] { 0, 5, 10, 20, 50, 100, 200, 500, 1000 };
        public static int[] RecencyDays = new[] { 1, 2, 3, 4, 7, 14, 21, 30, 45, 60 };
        public const int SpellingThreshold = 10;
        public const int MoreResultsThreshold = 5;
    }
}
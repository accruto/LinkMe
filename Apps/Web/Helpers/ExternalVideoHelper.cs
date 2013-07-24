namespace LinkMe.Web.Helpers
{
    public static class ExternalVideoHelper
    {
        private const string YouTubeUrlPattern = "http://www.youtube.com/v/{0}?version=3&amp;hl=en_US&amp;rel=0&amp;enablejsapi=1&amp;playerapiid=ytplayer&amp;fs=1";

        public static string GetExternalVideoUrl(string externalVideoId)
        {
            return string.Format(YouTubeUrlPattern, externalVideoId);
        }
    }
}

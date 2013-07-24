using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation
{
    public static class UrlSegmentsExtensions
    {
        public static string EncodeUrlSegment(this string segment)
        {
            return TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(segment)).ToLower().Replace(' ', '-');
        }
    }
}

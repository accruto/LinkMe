using System;
using System.Reflection;
using LinkMe.Apps.Asp.Content;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Content
{
    public static class Images
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());

        public static class Block
        {
            public static ReadOnlyUrl Calendar = new ContentUrl(Version, "~/content/block/images/forms/calendar.gif");

            public static ReadOnlyUrl PaymentOptions = new ContentUrl(Version, "~/content/block/images/products/payment-options.png");
            public static ReadOnlyUrl SecurePay = new ContentUrl(Version, "~/content/block/images/products/securepay-smaller.png");
        }

        public static ReadOnlyUrl Help = new ContentUrl(Version, "~/content/images/universal/help.gif");

        public static ReadOnlyUrl PhotoDefault = new ContentUrl(Version, "~/content/images/universal/photo-default.png");
        public static ReadOnlyUrl PhotoBg = new ContentUrl(Version, "~/content/images/employers/view-resume/photo-bg.png");

        public static ReadOnlyUrl IosHomeBackground = new ContentUrl(Version, "~/content/images/employers/home/iOSHomepage.png");
        public static ReadOnlyUrl Baby = new ContentUrl(Version, "~/content/images/homepage/expose-yourself/baby-laptop.png");

        public static ReadOnlyUrl LinkedIn16 = new ContentUrl(Version, "~/content/images/linkedin/LinkedIn_Logo16px.png");
        public static ReadOnlyUrl LinkedIn30 = new ContentUrl(Version, "~/content/images/linkedin/LinkedIn_Logo30px.png");
    }
}

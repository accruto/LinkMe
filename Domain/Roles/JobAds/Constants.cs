using System.Drawing;

namespace LinkMe.Domain.Roles.JobAds
{
    public static class Constants
    {
        private static readonly Size _logoMaxSize = new Size(225, 150);

        public const int MaxTitleLength = 200;
        public const int MaxPositionTitleLength = 200;
        public const int MaxSummaryLength = 300;
        public const int MaxContentLength = 35000;
        public const int MaxIntegratorReferenceIdLength = 50;
        public const int MaxExternalReferenceIdLength = 50;
        public const int MaxExternalApplyUrlLength = 1000;
        public const int MaxBulletPoints = 3;
        public const int MaxBulletPointLength = 255;
        public const int MaxPackageLength = 200;
        public const int JobAdListNameMaxLength = 100;

        public static Size LogoMaxSize
        {
            get { return new Size(_logoMaxSize.Width, _logoMaxSize.Height); }
        }
    }
}

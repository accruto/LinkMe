using System;

namespace LinkMe.Domain.Roles.JobAds
{
    [Flags]
    public enum JobAdFeatures
        : byte
    {
        None = 0,
        Logo = 0x1,
        ExtendedExpiry = 0x2,
        Refresh = 0x4,
        Highlight = 0x8
    }
}
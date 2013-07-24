using System;

namespace LinkMe.Domain
{
    [Flags]
    public enum EthnicStatus
    {
        None,
        Aboriginal = 0x1,
        TorresIslander = 0x2,
    }
}

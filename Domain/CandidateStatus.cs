using System;

namespace LinkMe.Domain
{
    public enum CandidateStatus
        : byte
    {
		Unspecified = 0,
		NotLooking = 1,
		OpenToOffers = 2,
		ActivelyLooking = 3,
        AvailableNow = 4
	}

    [Flags]
    public enum CandidateStatusFlags
        : byte
    {
        AvailableNow = 1 << 0,
        ActivelyLooking = 1 << 1,
        OpenToOffers = 1 << 2,
        NotLooking = 1 << 3,
        Unspecified = 1 << 4,

        All = Unspecified | NotLooking | OpenToOffers | ActivelyLooking | AvailableNow
    }
}
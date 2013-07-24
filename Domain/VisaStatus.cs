using System;

namespace LinkMe.Domain
{
    public enum VisaStatus
    {
        NotApplicable,
        Citizen,
        UnrestrictedWorkVisa,
        RestrictedWorkVisa,
        NoWorkVisa,
    }

    [Flags]
    public enum VisaStatusFlags
    {
        Citizen = 1 << 0,
        UnrestrictedWorkVisa = 1 << 1,
        RestrictedWorkVisa = 1 << 2,
        NoWorkVisa = 1 << 3,

        All = Citizen | UnrestrictedWorkVisa | RestrictedWorkVisa | NoWorkVisa,
    }
}

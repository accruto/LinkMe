using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class CandidateStatusDisplay
    {
        private static readonly IDictionary<CandidateStatus, string> Texts = new Dictionary<CandidateStatus, string>
        {
            {CandidateStatus.AvailableNow, "Immediately available"},
            {CandidateStatus.ActivelyLooking, "Actively looking"},
            {CandidateStatus.OpenToOffers, "Not looking but happy to talk"},
            {CandidateStatus.NotLooking, "Not looking"},
            {CandidateStatus.Unspecified, "Unspecified"},
        };

        private static readonly IDictionary<CandidateStatusFlags, string> FlagsTexts = new[]
        {
            CandidateStatusFlags.AvailableNow,
            CandidateStatusFlags.ActivelyLooking,
            CandidateStatusFlags.OpenToOffers,
            CandidateStatusFlags.NotLooking,
            CandidateStatusFlags.Unspecified
        }.ToDictionary(f => f, f => Texts.First(p => p.Key.ToString("G") == f.ToString("G")).Value);

        public static readonly CandidateStatusFlags[] Values = FlagsTexts.Keys.ToArray();

        public static string GetDisplayText(this CandidateStatus status)
        {
            string text;
            return Texts.TryGetValue(status, out text) ? text : null;
        }

        public static string GetDisplayText(this CandidateStatusFlags status)
        {
            string text;
            return FlagsTexts.TryGetValue(status, out text) ? text : null;
        }
    }
}

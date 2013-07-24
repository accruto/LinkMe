using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class EthnicStatusDisplay
    {
        private static readonly IDictionary<EthnicStatus, string> Texts = new Dictionary<EthnicStatus, string>
        {
            {EthnicStatus.Aboriginal, "Australian Aboriginal"},
            {EthnicStatus.TorresIslander, "Torres Strait Islander"},
        };

        public static EthnicStatus[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this EthnicStatus ethnicStatus)
        {
            string text;
            return Texts.TryGetValue(ethnicStatus, out text) ? text : null;
        }
    }
}

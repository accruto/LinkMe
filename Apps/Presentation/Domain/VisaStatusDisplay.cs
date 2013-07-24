using System.Collections.Generic;
using LinkMe.Domain;
using System.Linq;
using System;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class VisaStatusDisplay
    {
        private static readonly IDictionary<VisaStatus, string> Texts = new Dictionary<VisaStatus, string>
        {
            {VisaStatus.Citizen, "I am an Australian/New Zealand citizen or Permanent Resident"},
            {VisaStatus.UnrestrictedWorkVisa, "I am currently in possession of an unrestricted Australian work visa"},
            {VisaStatus.RestrictedWorkVisa, "I am currently in possession of a restricted Australian work visa"},
            {VisaStatus.NoWorkVisa, "I do not currently possess an Australian work visa"},
            {VisaStatus.NotApplicable, "Not applicable"},
        };

        public static VisaStatus[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this VisaStatus? visaStatus)
        {
            return visaStatus == null
                ? null
                : visaStatus.Value.GetDisplayText();
        }

        public static string GetDisplayText(this VisaStatus visaStatus)
        {
            string text;
            return Texts.TryGetValue(visaStatus, out text) ? text : Enum.GetName(typeof(VisaStatus), visaStatus);
        }
    }

    public static class VisaStatusFlagsDisplay
    {
        private static readonly IDictionary<VisaStatusFlags, string> Texts = new Dictionary<VisaStatusFlags, string>
        {
            {VisaStatusFlags.Citizen, "Citizens"},
            {VisaStatusFlags.UnrestrictedWorkVisa, "Unrestricted work visas"},
            {VisaStatusFlags.RestrictedWorkVisa, "Restricted work visas"},
            {VisaStatusFlags.NoWorkVisa, "No work visas"},
        };

        public static VisaStatusFlags[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this VisaStatusFlags? visaStatus)
        {
            return visaStatus == null
                ? null
                : visaStatus.Value.GetDisplayText();
        }

        public static string GetDisplayText(this VisaStatusFlags visaStatus)
        {
            string text;
            return Texts.TryGetValue(visaStatus, out text) ? text : Enum.GetName(typeof(VisaStatusFlags), visaStatus);
        }
    }
}

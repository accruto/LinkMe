using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class JobTypesDisplay
    {
        private static readonly IDictionary<JobTypes, string> Texts = new Dictionary<JobTypes, string>
        {
            {JobTypes.FullTime, "Full time"},
            {JobTypes.PartTime, "Part time"},
            {JobTypes.Contract, "Contract"},
            {JobTypes.Temp, "Temp"},
            {JobTypes.JobShare, "Job share"},
        };

        private static readonly IDictionary<JobTypes, string> LowerCaseTexts = Texts.ToDictionary(x => x.Key, x => x.Value.ToLower());
        public static readonly JobTypes[] Values = Texts.Keys.ToArray();

        private static string GetLowerCaseDisplayText(this JobTypes jobType)
        {
            string text;
            return LowerCaseTexts.TryGetValue(jobType, out text) ? text : null;
        }

        public static string GetDisplayText(this JobTypes jobType)
        {
            string text;
            return Texts.TryGetValue(jobType, out text) ? text : null;
        }

        public static string GetDisplayText(this JobTypes value, string separator, bool lowerCase, bool showAll)
        {
            if (value == JobTypes.None)
                return string.Empty;

            if (value == JobTypes.All)
                return showAll ? (lowerCase ? JobTypes.All.ToString().ToLower() : JobTypes.All.ToString()) : "";

            return lowerCase
                ? string.Join(separator, value.GetDisplayTexts(Values, GetLowerCaseDisplayText).ToArray())
                : string.Join(separator, value.GetDisplayTexts(Values, GetDisplayText).ToArray());
        }

        public static string GetDesiredClauseDisplayText(this JobTypes jobTypes)
        {
            var text = GetDisplayText(jobTypes, ", ", true, false);
            return text.Length == 0 ? "Open to all job types" : "Prefers " + text + " work";
        }
    }
}
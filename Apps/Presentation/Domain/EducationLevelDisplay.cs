using System.Collections.Generic;
using LinkMe.Domain;
using System.Linq;
using System;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class EducationLevelDisplay
    {
        private static readonly IDictionary<EducationLevel, string> Texts = new Dictionary<EducationLevel, string>
        {
            {EducationLevel.HighSchool, "High school"},
            {EducationLevel.TradeCertificate, "TAFE/Trade certificate"},
            {EducationLevel.Diploma, "Diploma/Advanced diploma"},
            {EducationLevel.Postgraduate, "Post-graduate degree"},
            {EducationLevel.Masters, "Master degree"},
            {EducationLevel.Doctoral, "Doctoral degree"},
            {EducationLevel.NotRelevant, "Not relevant"},
        };

        public static EducationLevel[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this EducationLevel educationLevel)
        {
            string text;
            return Texts.TryGetValue(educationLevel, out text) ? text : Enum.GetName(typeof(EducationLevel), educationLevel);
        }

        public static string GetDisplayText(this EducationLevel? educationLevel)
        {
            return educationLevel == null
                ? null
                : educationLevel.Value.GetDisplayText();
        }
    }
}

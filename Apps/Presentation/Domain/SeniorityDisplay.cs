using System.Collections.Generic;
using LinkMe.Domain;
using System.Linq;
using System;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class SeniorityDisplay
    {
        private static readonly IDictionary<Seniority, string> Texts = new Dictionary<Seniority, string>
        {
            {Seniority.NotApplicable, "Not applicable"},
            {Seniority.Executive, "Executive"},
            {Seniority.Director, "Director"},
            {Seniority.MidSenior, "Mid-senior level"},
            {Seniority.Associate, "Associate"},
            {Seniority.EntryLevel, "Entry level"},
            {Seniority.Internship, "Internship"},
            {Seniority.Student, "Student"},
            {Seniority.Volunteer, "Volunteer"},
        };

        public static Seniority[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this Seniority? seniority)
        {
            if (seniority == null)
                return null;
            string text;
            return Texts.TryGetValue(seniority.Value, out text) ? text : Enum.GetName(typeof(Seniority), seniority.Value);
        }
    }
}

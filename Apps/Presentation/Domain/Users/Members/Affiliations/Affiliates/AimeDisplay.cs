using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;

namespace LinkMe.Apps.Presentation.Domain.Users.Members.Affiliations.Affiliates
{
    public static class AimeDisplayExtension
    {
        private static readonly IDictionary<AimeMemberStatus, string> Texts = new Dictionary<AimeMemberStatus, string>
        {
            {AimeMemberStatus.BecomeMentor, "I want to become an AIME mentor"},
            {AimeMemberStatus.BecomeEmployee, "I want to work for AIME"},
            {AimeMemberStatus.CurrentMentor, "I am currently an AIME mentor"},
            {AimeMemberStatus.CurrentMentoree, "I am being mentored by an AIME mentor"},
        };

        public static AimeMemberStatus[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this AimeMemberStatus? status)
        {
            if (status == null)
                return null;
            string text;
            return Texts.TryGetValue(status.Value, out text) ? text : Enum.GetName(typeof(AimeMemberStatus), status.Value);
        }
    }
}
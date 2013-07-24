using System;
using LinkMe.Web.Areas.Members.Models.Resources;

namespace LinkMe.Web.Areas.Members.Views.Resources
{
    public static class PollsExtensions
    {
        public static int GetBarLength(this PollModel poll, Guid answerId, int controlWidth)
        {
            var percentage = poll.GetPercentage(answerId);
            return percentage * controlWidth / 100;
        }
    }
}
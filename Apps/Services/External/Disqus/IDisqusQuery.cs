using System;

namespace LinkMe.Apps.Services.External.Disqus
{
    public interface IDisqusQuery
    {
        /// <summary>
        /// Get the number of comments made in a particular thread
        /// </summary>
        /// <param name="threadIdentifier">The unique identifier for the thread, eg. the answeredQuestionId</param>
        /// <returns>The number of total comments (including comments on comments)</returns>
        int? GetCommentCount(Guid threadIdentifier);
    }
}

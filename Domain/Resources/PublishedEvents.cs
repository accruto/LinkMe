using System;

namespace LinkMe.Domain.Resources
{
    public static class PublishedEvents
    {
        public const string ResourceViewed = "LinkMe.Domain.Resources.ResourceViewed";
        public const string QuestionCreated = "LinkMe.Domain.Resources.QuestionCreated";
        public const string FaqMarked = "LinkMe.Domain.Resources.FaqMarked";
    }

    public class ResourceEventArgs
        : EventArgs
    {
        public Guid ResourceId { get; private set; }

        public ResourceEventArgs(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }

    public class ResourceQuestionEventArgs
        : EventArgs
    {
        public Question Question { get; private set; }

        public ResourceQuestionEventArgs(Question question)
        {
            Question = question;
        }
    }
}
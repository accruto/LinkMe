using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IApplicationQuestion
    {
        string Id { get; }
        string Text { get; }
        bool IsRequired { get; }
    }

    public abstract class ApplicationQuestion
        : IApplicationQuestion
    {
        public string Id { get; set; }
        public string FormatId { get; set; }
        public string Text { get; set; }
        public bool IsRequired { get; set; }
    }

    public class TextQuestion
        : ApplicationQuestion
    {
    }

    public class MultipleChoiceQuestionAnswer
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class MultipleChoiceQuestion
        : ApplicationQuestion
    {
        public IList<MultipleChoiceQuestionAnswer> Answers { get; set; }
    }

    public abstract class ApplicationAnswer
    {
        public ApplicationQuestion Question { get; set; }
        public string Value { get; set; }
    }

    public class TextAnswer
        : ApplicationAnswer
    {
    }
    
    public class MultipleChoiceAnswer
        : ApplicationAnswer
    {
    }

    public class ApplicationRequirements
    {
        public bool IncludeResume { get; set; }
        public bool IncludeCoverLetter { get; set; }
        public IList<ApplicationQuestion> Questions { get; set; }
    }
}

using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources
{
    public class Question
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid AskerId { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}

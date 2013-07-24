using System;

namespace LinkMe.Framework.Content.Templates
{
    public class TemplateContext
    {
        public Guid Id { get; set; }
        public string Definition { get; set; }
        public string Category { get; set; }
        public Guid? VerticalId { get; set; }
        public Guid? UserId { get; set; }
    }

    public interface ITemplateEngine
    {
        CopyItem GetCopyItem(TemplateContext context, TemplateProperties properties, string[] mimeTypes);

        CopyItemEngine GetCopyItemEngine(TemplateContentItem templateContentItem);
        CopyItem GetCopyItem(CopyItemEngine copyItemEngine, TemplateContext context, TemplateProperties properties, string[] mimeTypes);
    }

    public interface ITextTemplateEngine
    {
        string GetCopyText(TemplateContext context, TemplateProperties properties);
    }
}
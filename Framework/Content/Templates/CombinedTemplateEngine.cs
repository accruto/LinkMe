using System.Linq;

namespace LinkMe.Framework.Content.Templates
{
    public class CombinedTemplateEngine
        : ITemplateEngine
    {
        private readonly string[] _secondTemplateEngineNames;
        private readonly ITemplateEngine _firstTemplateEngine;
        private readonly ITemplateEngine _secondTemplateEngine;

        public CombinedTemplateEngine(ITemplateEngine firstTemplateEngine, ITemplateEngine secondTemplateEngine, string[] secondTemplateEngineNames)
        {
            _firstTemplateEngine = firstTemplateEngine;
            _secondTemplateEngine = secondTemplateEngine;
            _secondTemplateEngineNames = secondTemplateEngineNames;
        }

        CopyItem ITemplateEngine.GetCopyItem(TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            return _secondTemplateEngineNames.Contains(context.Definition)
                ? _secondTemplateEngine.GetCopyItem(context, properties, mimeTypes)
                : _firstTemplateEngine.GetCopyItem(context, properties, mimeTypes);
        }

        CopyItemEngine ITemplateEngine.GetCopyItemEngine(TemplateContentItem templateContentItem)
        {
            return _firstTemplateEngine.GetCopyItemEngine(templateContentItem);
        }

        CopyItem ITemplateEngine.GetCopyItem(CopyItemEngine copyItemEngine, TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            return _firstTemplateEngine.GetCopyItem(copyItemEngine, context, properties, mimeTypes);
        }
    }
}
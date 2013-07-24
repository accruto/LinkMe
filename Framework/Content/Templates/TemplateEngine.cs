using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Content.Templates
{
    public abstract class TemplateEngine
        : ITemplateEngine
    {
        private class State
        {
            public Engines Engines;
            public IList<MasterTemplateContentItem> MasterContentItems;
        }

        private readonly IContentEngine _contentEngine;
        private State _state;

        protected TemplateEngine(IContentEngine contentEngine)
        {
            _contentEngine = contentEngine;
        }

        #region ITemplateEngine

        CopyItem ITemplateEngine.GetCopyItem(TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            Initialise();
            var copyItemEngine = GetCopyItemEngine(context);
            return CreateCopyItem(copyItemEngine, context, properties, mimeTypes);
        }

        CopyItemEngine ITemplateEngine.GetCopyItemEngine(TemplateContentItem contentItem)
        {
            Initialise();

            // Try to find a master.

            var state = _state;
            MasterTemplateContentItem masterContentItem = null;
            if (contentItem.VerticalId != null)
            {
                masterContentItem = (from m in state.MasterContentItems
                                     where m.Name == contentItem.Master
                                           && m.VerticalId == contentItem.VerticalId
                                     select m).SingleOrDefault();
            }

            if (masterContentItem == null)
            {
                // Try to use the default.

                masterContentItem = (from m in state.MasterContentItems
                                     where m.Name == contentItem.Master
                                           && m.VerticalId == null
                                     select m).SingleOrDefault();
            }

            // Create an engine specifically for this template.

            var mergedContentItem = masterContentItem == null
                ? contentItem
                : CreateTemplateContentItem(contentItem, contentItem.VerticalId, masterContentItem);
            return CreateCopyItemEngine(contentItem.VerticalId, mergedContentItem);
        }

        CopyItem ITemplateEngine.GetCopyItem(CopyItemEngine copyItemEngine, TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            Initialise();

            // Create an engine specifically for this template.

            return CreateCopyItem(copyItemEngine, context, properties, mimeTypes);
        }

        #endregion

        protected abstract ITextTemplateEngine CreateTextTemplateEngine(Guid? verticalId, TemplateContentItem contentItem);
        protected abstract ITextTemplateEngine CreateTextTemplateEngine(Guid? verticalId, ViewContentItem contentItem);

        private void Initialise()
        {
            if (_state == null)
                _state = CreateState();
        }

        private State CreateState()
        {
            var unmergedContentItems = _contentEngine.GetContentItems<TemplateContentItem>();
            var masterContentItems = _contentEngine.GetContentItems<MasterTemplateContentItem>();

            // Create the templates.

            var contentItems = MergeTemplateContentItems(unmergedContentItems, masterContentItems);

            var engines = new Engines();
            foreach (var contentItem in contentItems)
                Add(engines, contentItem);

            return new State { Engines = engines, MasterContentItems = masterContentItems };
        }

        private static IEnumerable<TemplateContentItem> MergeTemplateContentItems(IEnumerable<TemplateContentItem> contentItems, IEnumerable<MasterTemplateContentItem> masterContentItems)
        {
            var mergedContentItems = new List<TemplateContentItem>();
            foreach (var contentItem in contentItems)
                MergeTemplateContentItems(mergedContentItems, contentItem, contentItems, masterContentItems);
            return mergedContentItems;
        }

        private static void MergeTemplateContentItems(ICollection<TemplateContentItem> mergedContentItems, TemplateContentItem contentItem, IEnumerable<TemplateContentItem> contentItems, IEnumerable<MasterTemplateContentItem> masterContentItems)
        {
            // If there is no master then simply return it.

            if (string.IsNullOrEmpty(contentItem.Master))
            {
                mergedContentItems.Add(contentItem);
                return;
            }

            // Find its master.

            var masterContentItem = (from m in masterContentItems
                                     where m.Name == contentItem.Master && m.VerticalId == contentItem.VerticalId
                                     select m).SingleOrDefault()
                                     ??
                                     (from m in masterContentItems
                                      where m.Name == contentItem.Master && m.VerticalId == null
                                      select m).SingleOrDefault();

            // Merge.

            if (masterContentItem == null)
                mergedContentItems.Add(contentItem);
            else
                mergedContentItems.Add(CreateTemplateContentItem(contentItem, contentItem.VerticalId, masterContentItem));

            // If there are vertical specific masters for this content, but there is no specific content then use the default, ie this content.

            if (contentItem.VerticalId == null)
                MergeDefaultContentItem(mergedContentItems, contentItem, contentItems, masterContentItems);
        }

        private static void MergeDefaultContentItem(ICollection<TemplateContentItem> mergedContentItems, TemplateContentItem contentItem, IEnumerable<TemplateContentItem> contentItems, IEnumerable<MasterTemplateContentItem> masterContentItems)
        {
            var masters = from m in masterContentItems
                          where m.Name == contentItem.Master && m.VerticalId != null
                          select m;

            foreach (var masterContentItem in masters)
            {
                var verticalSpecificContentItem = from c in contentItems
                                                  where c.Name == contentItem.Name
                                                  && c.Master == contentItem.Master
                                                  && c.VerticalId == masterContentItem.VerticalId
                                                  select c;

                if (!verticalSpecificContentItem.Any())
                    mergedContentItems.Add(CreateTemplateContentItem(contentItem, masterContentItem.VerticalId, masterContentItem));
            }
        }

        private static TemplateContentItem CreateTemplateContentItem(TemplateBaseContentItem contentItem, Guid? verticalId, TemplateBaseContentItem masterContentItem)
        {
            var newContentItem = new TemplateContentItem
            {
                Id = contentItem.Id,
                Name = contentItem.Name,
                VerticalId = verticalId,
                Subject = contentItem.Subject,
            };

            newContentItem.Views = contentItem.Views.Select(viewContentItem => CreateViewContentItem(viewContentItem, masterContentItem)).ToList();
            return newContentItem;
        }

        private static ViewContentItem CreateViewContentItem(ViewContentItem contentItem, TemplateBaseContentItem masterContentItem)
        {
            var mimeType = contentItem.MimeType;

            // If there is a master then use it to substitute all parts into one part.

            if (masterContentItem != null)
            {
                var masterViewPart = GetViewPart(masterContentItem.Views, mimeType);
                if (masterViewPart != null)
                    return CreateViewContentItem(contentItem, masterViewPart);
            }

            // No master so copy all parts over.

            return new ViewContentItem
            {
                MimeType = contentItem.MimeType,
                Parts = contentItem.Parts,
            };
        }

        private static ViewContentItem CreateViewContentItem(ViewContentItem contentItem, ViewPartContentItem masterViewPart)
        {
            var text = masterViewPart.Text;
            if (contentItem.Parts != null)
                text = contentItem.Parts.Aggregate(text, (current, viewPart) => current.Replace("<%=" + viewPart.Name + "%>", viewPart.Text));

            return new ViewContentItem
            {
                MimeType = contentItem.MimeType,
                Parts = new List<ViewPartContentItem>
                {
                    new ViewPartContentItem { Text = text }
                }
            };
        }

        private static ViewPartContentItem GetViewPart(IEnumerable<ViewContentItem> contentItems, string mimeType)
        {
            if (contentItems == null)
                return null;

            return (from contentItem in contentItems
                    where contentItem.MimeType == mimeType
                    where contentItem.Parts != null && contentItem.Parts.Count == 1
                    select contentItem.Parts[0]).FirstOrDefault();
        }

        private void Add(Engines engines, TemplateContentItem contentItem)
        {
            // Look for the engine.

            var engine = engines[contentItem.Name];
            if (engine == null)
            {
                engine = new Engine();
                engines.Add(contentItem.Name, engine);
            }

            engine.CopyItemEngines.Add(contentItem.VerticalId, CreateCopyItemEngine(contentItem));
        }

        private CopyItemEngine CreateCopyItemEngine(TemplateContentItem contentItem)
        {
            return CreateCopyItemEngine(contentItem.VerticalId, contentItem);
        }

        private CopyItemEngine CreateCopyItemEngine(Guid? verticalId, TemplateContentItem contentItem)
        {
            // Create a text engine for this copy.

            var textTemplateEngine = CreateTextTemplateEngine(verticalId, contentItem);

            // Create the engine.

            var engine = new CopyItemEngine(textTemplateEngine);
            foreach (var viewContentItem in contentItem.Views)
                engine.ViewEngines.Add(viewContentItem.MimeType, CreateViewEngine(verticalId, viewContentItem));
            return engine;
        }

        private ViewEngine CreateViewEngine(Guid? verticalId, ViewContentItem contentItem)
        {
            // Create a text engine for this view.

            var templateEngine = CreateTextTemplateEngine(verticalId, contentItem);

            // Create the engine.

            return new ViewEngine(templateEngine);
        }

        private static CopyItem CreateCopyItem(CopyItemEngine copyItemEngine, TemplateContext context, TemplateProperties properties, ICollection<string> mimeTypes)
        {
            var copyItem = new CopyItem(copyItemEngine.GetCopyText(context, properties));
            foreach (var pair in GetViewEngines(copyItemEngine.ViewEngines, mimeTypes))
                copyItem.ViewItems.Add(new ViewItem(pair.Key, pair.Value.GetCopyText(context, properties)));
            return copyItem;
        }

        private Engine GetEngine(string name)
        {
            var state = _state;

            // Look for the name explicitly.

            var copyItemEngine = state.Engines[name];
            if (copyItemEngine != null)
                return copyItemEngine;

            // Look for the default.

            return state.Engines[string.Empty];
        }

        private static CopyItemEngine GetCopyItemEngine(Engine engine, Guid? verticalId)
        {
            // Look for the id explicitly.

            var copyItemEngine = engine.CopyItemEngines[verticalId];
            if (copyItemEngine != null)
                return copyItemEngine;

            // Look for the default.

            return engine.CopyItemEngines[null];
        }

        private CopyItemEngine GetCopyItemEngine(TemplateContext context)
        {
            // Work through the collections.

            var engine = GetEngine(context.Definition);
            return engine == null ? null : GetCopyItemEngine(engine, context.VerticalId);
        }

        private static IEnumerable<KeyValuePair<string, ViewEngine>> GetViewEngines(ViewEngines viewEngines, ICollection<string> mimeTypes)
        {
            var supportedViewEngines = new SortedList<string, ViewEngine>();
            if (mimeTypes != null && mimeTypes.Count != 0)
            {
                // Look for the mime types.

                foreach (var mimeType in mimeTypes)
                {
                    var viewEngine = viewEngines[mimeType];
                    if (viewEngine != null)
                        supportedViewEngines.Add(mimeType, viewEngine);
                }

                // If no templates are explicitly found then return all of them.

                if (supportedViewEngines.Count != 0)
                    return supportedViewEngines;
            }

            // Use them all.

            return viewEngines;
        }
    }
}
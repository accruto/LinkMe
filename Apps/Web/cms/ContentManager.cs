using System;
using System.Collections.Generic;
using System.Reflection;
using LinkMe.Apps.Agents.Domain.Roles.Affiliations.Communities;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Web.Cms.ContentDisplayers;
using LinkMe.Web.Cms.ContentEditors;

namespace LinkMe.Web.Cms
{
    public class ContentManager
    {
        private static readonly IDictionary<Type, IContentDisplayerFactory> DisplayerFactories = new Dictionary<Type, IContentDisplayerFactory>();
        private static readonly IDictionary<Type, IItemContentEditorFactory> ItemEditorFactories = new Dictionary<Type, IItemContentEditorFactory>();
        private static readonly IDictionary<Type, IPropertyContentEditorFactory> PropertyEditorFactories = new Dictionary<Type, IPropertyContentEditorFactory>();

        private static readonly IDictionary<Type, string> DisplayerTemplateUrls = new Dictionary<Type, string>();

        static ContentManager()
        {
            DisplayerFactories[typeof(TextContentItem)] = new TextContentDisplayerFactory();
            DisplayerFactories[typeof(ImageContentItem)] = new ImageContentDisplayerFactory();
            DisplayerFactories[typeof(HtmlContentItem)] = new HtmlContentDisplayerFactory();

            ItemEditorFactories[typeof(TextContentItem)] = new TextContentEditorFactory();
            ItemEditorFactories[typeof(ImageContentItem)] = new ImageContentEditorFactory();
            ItemEditorFactories[typeof(HtmlContentItem)] = new HtmlContentEditorFactory();

            PropertyEditorFactories[typeof(TextContentAttribute)] = new TextPropertyContentEditorFactory();
            PropertyEditorFactories[typeof(string)] = new TextPropertyContentEditorFactory();
            PropertyEditorFactories[typeof(bool)] = new BoolPropertyContentEditorFactory();

            DisplayerTemplateUrls[typeof(SectionContentItem)] = "~/cms/ContentTemplates/SectionContentTemplate.ascx";
            DisplayerTemplateUrls[typeof(CommunityHeaderContentItem)] = "~/cms/ContentTemplates/CommunityHeaderContentTemplate.ascx";
            DisplayerTemplateUrls[typeof(CommunityFooterContentItem)] = "~/cms/ContentTemplates/CommunityFooterContentTemplate.ascx";
        }

        public static IContentDisplayer GetDisplayer(ContentItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            IContentDisplayerFactory factory;
            if (DisplayerFactories.TryGetValue(item.GetType(), out factory))
                return factory.CreateDisplayer(item);
            return null;
        }

        public static string GetDisplayerTemplateUrl(ContentItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            string templateUrl;
            if (DisplayerTemplateUrls.TryGetValue(item.GetType(), out templateUrl))
                return templateUrl;
            return null;
        }

        public static IContentEditor GetEditor(ContentItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            IItemContentEditorFactory factory;
            if (ItemEditorFactories.TryGetValue(item.GetType(), out factory))
                return factory.CreateEditor(item);
            return null;
        }

        public static IContentEditor GetEditor(ContentAttribute attribute, object instance, PropertyInfo propertyInfo)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            IPropertyContentEditorFactory factory;
            if (PropertyEditorFactories.TryGetValue(attribute.GetType(), out factory))
                return factory.CreateEditor(instance, propertyInfo);
            return null;
        }

        public static IContentEditor GetEditor(Type type, object instance, PropertyInfo propertyInfo)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            IPropertyContentEditorFactory factory;
            if (PropertyEditorFactories.TryGetValue(type, out factory))
                return factory.CreateEditor(instance, propertyInfo);
            return null;
        }
    }
}

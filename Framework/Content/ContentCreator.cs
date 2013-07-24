using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Content
{
    public class ContentCreator
        : IContentCreator
    {
        private readonly IDictionary<string, Type> _contentItemTypes = new Dictionary<string, Type>();

        public ContentCreator()
        {
            // Find all content items.

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.BaseType == typeof (ContentItem))
                            _contentItemTypes[type.Name] = type;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        
        ContentItem IContentCreator.CreateContentItem(string typeName)
        {
            // Look for the type.

            Type type;
            if (!_contentItemTypes.TryGetValue(typeName, out type))
                return null;

            return (ContentItem)Activator.CreateInstance(type);
        }
    }
}

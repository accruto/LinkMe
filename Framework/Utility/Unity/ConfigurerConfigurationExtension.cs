using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.Framework.Utility.Unity
{
    public class ResolveConfigurationExtension
        : UnityContainerExtensionConfigurationElement
    {
        public override void Configure(IUnityContainer container)
        {
            foreach (UnityTypeElement element in Types)
            {
                object instance = container.Resolve(element.Type);
                container.RegisterInstance(element.Type, instance); // force instance to be a singleton
            }
        }

        [ConfigurationProperty("types")]
        [ConfigurationCollection(typeof(UnityTypeElementCollection), AddItemName = "type")]
        public UnityTypeElementCollection Types
        {
            get
            {
                var elements = (UnityTypeElementCollection)base["types"];
                elements.TypeResolver = TypeResolver;
                return elements;
            }
        }
    }
}

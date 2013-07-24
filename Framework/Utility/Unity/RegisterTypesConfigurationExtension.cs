using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.Framework.Utility.Unity
{
    public class RegisterTypesConfigurationExtension
        : UnityContainerExtensionConfigurationElement
    {
        public override void Configure(IUnityContainer container)
        {
            foreach (UnityTypeElement element in Types)
            {
                var instance = container.Resolve(element.Type) as IContainerConfigurer;
                if (instance != null)
                    instance.RegisterTypes(container);
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

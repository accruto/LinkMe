using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.Framework.Utility.Unity
{
    public class IncludeConfigurationExtension
        : UnityContainerExtensionConfigurationElement
    {
        public override void Configure(IUnityContainer container)
        {
            foreach (SectionElement section in Sections)
                container.AddConfiguration(section.Name);
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public SectionElementCollection Sections
        {
            get { return (SectionElementCollection)base[""]; }
        }
    }

    [ConfigurationCollection(typeof(SectionElement), AddItemName = "section")]
    public class SectionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SectionElement)element).Name;
        }

        public SectionElement this[int index]
        {
            get { return (SectionElement)BaseGet(index); }
        }
    }

    public class SectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
    }
}

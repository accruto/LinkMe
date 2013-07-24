using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace LinkMe.Framework.Utility.Unity.ServiceModel
{
    public class UnityBehaviorExtensionElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("sectionName", IsRequired = false, DefaultValue = null)]
        public string SectionName 
        {
            get { return (string)base["sectionName"]; }
            set { base["sectionName"] = value; }
        }

        protected override object CreateBehavior()
        {
            if (!string.IsNullOrEmpty(SectionName))
                Container.Current.AddConfiguration(SectionName);

            return new UnityServiceBehavior(Container.Current);
        }

        public override Type BehaviorType
        {
            get { return typeof(UnityServiceBehavior); }
        }
    }
}

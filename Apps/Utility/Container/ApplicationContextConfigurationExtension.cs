using System;
using System.Collections.Generic;
using LinkMe.Utility.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.Utility.Container
{
    /// <summary>
    /// This class dumps all property key-value pairs from ApplicationContext to the Unity container.
    /// </summary>
    public class ApplicationContextConfigurationExtension
        : UnityContainerExtensionConfigurationElement
    {
        public override void Configure(IUnityContainer container)
        {
            foreach (KeyValuePair<string, string> property in ApplicationContext.Instance.GetPropertyValues())
            {
                switch (property.Key)
                {
                    // Int32
                    case "searchme.database.saveTrigger":
                    case "credits.applicant.lowthreshold":
                    case "lens.connection.port":
                        container.RegisterInstance(property.Key, ApplicationContext.Instance.GetIntProperty(property.Key));
                        break;

                    // TimeSpan
                    case "searchme.database.saveInterval":
                    case "searchme.initializer.timeout":
                        {
                            TimeSpan value;
                            if (!TimeSpan.TryParse(property.Value, out value))
                                throw new ArgumentException(string.Format("The '{0}' property value, '{1}', is incorrect.", property.Key, property.Value));
                            container.RegisterInstance(property.Key, value);
                        }
                        break;

                    // string[]
                    case "searchme.updater.endpoints":
                    case "ignored.job.titles":
                        container.RegisterInstance(property.Key, ApplicationContext.Instance.GetStringArrayProperty(property.Key));
                        break;

                    // bool
                    case "equivalent.terms.enabled":
                    case "search.spell.suggestions.enabled":
                    case "enforce.double.check.cookie.ip":
                        {
                            bool value;
                            if (!bool.TryParse(property.Value, out value))
                                throw new ArgumentException(string.Format("The '{0}' property value, '{1}', is incorrect.", property.Key, property.Value));
                            container.RegisterInstance(property.Key, value);
                        }
                        break;

                    // decimal
                    case "equivalent.terms.weight":
                        {
                            decimal value;
                            if (!decimal.TryParse(property.Value, out value))
                                throw new ArgumentException(string.Format("The '{0}' property value, '{1}', is incorrect.", property.Key, property.Value));
                            container.RegisterInstance(property.Key, value);
                        }
                        break;

                    case "linkme.skills.filepath":
                        container.RegisterInstance(property.Key, ApplicationContext.Instance.GetPathRelativeToConfig(property.Value));
                        break;

                    default:
                        container.RegisterInstance(property.Key, property.Value);
                        break;
                }
            }
        }
    }
}

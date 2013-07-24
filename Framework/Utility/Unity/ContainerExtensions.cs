using System;
using System.Configuration;
using LinkMe.Environment;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.Framework.Utility.Unity
{
    public static class ContainerExtensions
    {
        /// <summary>
        /// Configures the specified IUnityContainer from the specified config section. 
        /// The container config elements are processed and merged in the following order:
        /// 1. Default container;
        /// 2. env:envname container;
        /// 3. host:hostname container;
        /// 4. post-config container.
        /// </summary>
        public static IUnityContainer AddConfiguration(this IUnityContainer container, string sectionName)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentException("The section name must be specified.", "sectionName");

            UnityConfigurationSection section;
            try
            {
                section = (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the '" + sectionName + "' configuration section."
                    + " This may happen if you have more than one IIS virtual directory configured"
                    + " for the same physical directory.", ex);
            }

            if (section == null)
                throw new InvalidOperationException(string.Format("The {0} configuration section is not found in the application configuration file.", sectionName));

            UnityContainerElement defaultConfig = section.Containers.Default;
            if (defaultConfig != null)
                defaultConfig.Configure(container);

            UnityContainerElement environmentConfig = section.Containers["env:" + RuntimeEnvironment.EnvironmentName];
            if (environmentConfig != null)
                environmentConfig.Configure(container);

            UnityContainerElement hostConfig = section.Containers["host:" + RuntimeEnvironment.HostName];
            if (hostConfig != null)
                hostConfig.Configure(container);

            UnityContainerElement postConfig = section.Containers["post-config"];
            if (postConfig != null)
                postConfig.Configure(container);

            return container;
        }
    }
}

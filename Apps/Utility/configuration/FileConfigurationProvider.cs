using System;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Xml;
using LinkMe.Environment;

namespace LinkMe.Utility.Configuration
{
    /// <summary>
    /// This class redirects ConfigurationManager to the specified external configuration file.
    /// The file name can be specified using:
    /// 1. An absolute path.
    /// 2. A relative path (treated as relative to the current AppDomain config. file).
    /// 3. A path relative to the config folder. This is controlled by the "configFolder" attribute.
    /// 
    ///  The file name can also include the $(Host) macro that will be replaced by the current host name at runtime. 
    /// </summary>
    public class FileConfigurationProvider
        : ProtectedConfigurationProvider
    {
        public override XmlNode Encrypt(XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override XmlNode Decrypt(XmlNode encryptedNode)
        {
            // Read XML.

            var fileNode = encryptedNode.SelectSingleNode("/EncryptedData/file");
            if (fileNode == null)
                throw new ProviderException("A <file> element is not specified.");

            var nameAttr = fileNode.Attributes["name"];
            if (nameAttr == null || string.IsNullOrEmpty(nameAttr.Value))
                throw new ProviderException("The mandatory 'name' attribute is not specified.");

            var useConfigFolder = false;
            var configFolderAttr = fileNode.Attributes["configFolder"];
            if (configFolderAttr != null)
                useConfigFolder = XmlConvert.ToBoolean(configFolderAttr.Value);

            // Figure out the full path name.

            var fileName = nameAttr.Value.Replace("$(Host)", RuntimeEnvironment.HostName);
            string filePath;

            if (useConfigFolder)
            {
                filePath = ApplicationContext.Instance.GetPathRelativeToConfig(fileName);
            }
            else if (Path.IsPathRooted(fileName))
            {
                filePath = fileName;
            }
            else
            {
                var configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                filePath = Path.Combine(Path.GetDirectoryName(configFile), fileName);
            }

            // Return the root node of the config. section.

            var doc = new XmlDocument();
            doc.Load(filePath);
            return doc.DocumentElement;
        }
    }
}

using System.Xml;

namespace LinkMe.Framework.Configuration.Connection
{
    public interface IContainerDataInfo
    {
        void Import(IConfigurationEventSource eventSource);
        void Import(string filePath, IConfigurationEventSource eventSource);
        void Import(string filePath, XmlReader reader, IConfigurationEventSource eventSource);
        void Export(string filePath, IConfigurationEventSource eventSource);
    }
}

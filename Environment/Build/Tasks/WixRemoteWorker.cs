using System.IO;
using File=LinkMe.Environment.Build.Tasks.Constants.File;

namespace LinkMe.Environment.Build.Tasks
{
    internal class WixRemoteWorker
        : System.MarshalByRefObject
    {
        public WixRemoteWorker()
        {
        }

        public void Initialise(bool isWin64, string buildFolder, string msmFile, System.Guid moduleGuid, System.Version version, string manufacturer)
        {
            m_guid = moduleGuid;
            m_version = version;
            m_manufacturer = manufacturer;
            m_isWin64 = isWin64;
            m_wixBuilder = new WixBuilder(buildFolder, msmFile);
        }

        public void Load(string catalogueFile)
        {
            // Load the catalogue.

            Catalogue catalogue = new Catalogue();
            catalogue.Load(catalogueFile);

            // Load the module.

            WixModuleLoader moduleLoader = new WixModuleLoader();
            moduleLoader.Name = Path.GetFileNameWithoutExtension(catalogueFile);
            moduleLoader.Version = m_version;
            moduleLoader.Guid = m_guid;
            moduleLoader.Manufacturer = m_manufacturer;
            moduleLoader.Load(m_isWin64, catalogue);

            // Save the .wxs file.

            Directory.CreateDirectory(m_wixBuilder.BuildFolder);
            string wxsPath = Path.Combine(m_wixBuilder.BuildFolder, Path.ChangeExtension(Path.GetFileName(catalogueFile), File.Wxs.Extension));
            moduleLoader.Save(wxsPath);

            // Add it to the list.

            m_wixBuilder.Add(moduleLoader);
        }

        public void Build()
        {
            m_wixBuilder.Build();
        }

        private WixBuilder m_wixBuilder;
        private System.Guid m_guid;
        private System.Version m_version;
        private string m_manufacturer;
        private bool m_isWin64;
    }
}
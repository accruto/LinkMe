using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Tools.WindowsInstallerXml;
using Microsoft.Tools.WindowsInstallerXml.Serialize;
using File = LinkMe.Environment.Build.Tasks.Constants.File;

namespace LinkMe.Environment.Build.Tasks
{
    public class WixBuilder
    {
        public WixBuilder(string buildFolder, string installFile)
        {
            m_buildFolder = buildFolder;
            m_installFile = installFile;
            m_wixDocuments = new Dictionary<string, Wix>();
        }

        public string InstallFile
        {
            get { return m_installFile; }
        }

        public string BuildFolder
        {
            get { return m_buildFolder; }
        }

        public void Add(WixModuleLoader moduleLoader)
        {
            m_wixDocuments.Add(moduleLoader.Name, moduleLoader.Wix);
        }

        public void Build()
        {
            try
            {
                // Compile all wix documents.

                Compile();

                // Link the compiled documents into an output.

                Localizer localizer = new Localizer();
                localizer.Message += HandleMessage;
                WixVariableResolver wixVariableResolver = new WixVariableResolver();
                wixVariableResolver.Message += HandleMessage;

                Output output = Link(localizer, wixVariableResolver);

                // Bind everything together into the final install.

                Bind(output, localizer, wixVariableResolver);
            }
            catch (WixException e)
            {
                throw new System.Exception(string.Format(e.Error.ResourceManager.GetString(e.Error.ResourceName), e.Error.MessageArgs));
            }
        }

        private void Compile()
        {
            // Create a compiler.

            Compiler compiler = new Compiler();
            compiler.Message += HandleMessage;
            compiler.ShowPedanticMessages = false;
            compiler.SuppressValidate = false;

            // Iterate over each document.

            foreach (KeyValuePair<string, Wix> pair in m_wixDocuments)
                Compile(compiler, pair.Key, pair.Value);
        }

        private void Compile(Compiler compiler, string name, Wix wix)
        {
            // Load the wix document into an XML document.

            XmlDocument xmlDocument = new XmlDocument();

            using (MemoryStream writerStream = new MemoryStream())
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writerStream, Encoding.Unicode))
                {
                    wix.OutputXml(xmlWriter);
                }

                using (MemoryStream readerStream = new MemoryStream(writerStream.GetBuffer()))
                {
                    xmlDocument.Load(readerStream);
                }
            }

            // Compile it.

            Intermediate intermediate = compiler.Compile(xmlDocument);

            // Save the intermediate.

            string path = Path.Combine(m_buildFolder, name + File.Wixobj.Extension);
            intermediate.Save(path);
        }

        private Output Link(Localizer localizer, WixVariableResolver wixVariableResolver)
        {
            // Create a linker.

            Linker linker = new Linker();
            linker.Message += HandleMessage;
            linker.Localizer = localizer;
            linker.WixVariableResolver = wixVariableResolver;

            // Load each intermediate for each document.

            SectionCollection sections = new SectionCollection();
            foreach (KeyValuePair<string, Wix> pair in m_wixDocuments)
            {
                string path = Path.Combine(m_buildFolder, pair.Key + File.Wixobj.Extension);
                Intermediate intermediate = Intermediate.Load(path, linker.TableDefinitions, false, false);
                sections.AddRange(intermediate.Sections);
            }

            // Link.

            ArrayList transforms = new ArrayList();
            return linker.Link(sections, transforms);
        }

        private void Bind(Output output, Localizer localizer, WixVariableResolver wixVariableResolver)
        {
            // Create a binder.

            Binder binder = new Binder();
            binder.Localizer = localizer;
            binder.WixVariableResolver = wixVariableResolver;
            binder.TempFilesLocation = m_buildFolder;

            try
            {
                binder.Bind(output, m_installFile);
            }
            finally
            {
                binder.DeleteTempFiles();
            }
        }

        private static void HandleMessage(object sender, MessageEventArgs args)
        {
            if (args is WixErrorEventArgs)
                throw new System.Exception(string.Format(args.ResourceManager.GetString(args.ResourceName), args.MessageArgs));
        }

        private readonly string m_buildFolder;
        private readonly string m_installFile;
        private readonly Dictionary<string, Wix> m_wixDocuments;
    }
}
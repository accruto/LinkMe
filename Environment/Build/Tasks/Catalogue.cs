using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace LinkMe.Environment.Build.Tasks
{
    public class Catalogue
        : MarshalByRefObject
    {
        public Catalogue()
        {
        }

        public void Add(Artifact artifact)
        {
            m_artifacts.Add(artifact);
        }

        public IList<Artifact> Artifacts
        {
            get { return m_artifacts.AsReadOnly(); }
        }

        public string FullPath
        {
            get { return m_fullPath; }
        }

        public string RootFolder
        {
            get { return m_rootFolder; }
        }

        public string Guid
        {
            get { return m_guid; }
        }

        public void Save(string outputFolder, string fullPath, string guid)
        {
            // Determine the root folder for the artifacts.

            m_fullPath = fullPath;
            if ( !outputFolder.EndsWith(new string(Path.DirectorySeparatorChar, 1)) )
                outputFolder += Path.DirectorySeparatorChar;
            m_rootFolder = FilePath.GetRelativePath(outputFolder, Path.GetDirectoryName(fullPath));

            m_guid = guid;

            // Construct the catalogue.

            StringBuilder builder = new StringBuilder();
            XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder));
            writer.Indentation = 4;
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement(Constants.Catalogue.Xml.ConfigurationElement);
            writer.WriteStartElement(Constants.Catalogue.Xml.LinkMeElement);
            writer.WriteStartElement(Constants.Catalogue.Xml.CatalogueElement, Constants.Catalogue.Xml.Namespace);

            // Add artifacts.

            if ( m_artifacts.Count > 0 )
            {
                writer.WriteStartElement(Constants.Catalogue.Xml.ArtifactsElement);
                SaveArtifacts(writer, outputFolder);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();

            // Write out the file.

            if ( !Directory.Exists(Path.GetDirectoryName(fullPath)) )
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using ( StreamWriter fileWriter = new StreamWriter(fullPath) )
            {
                fileWriter.Write(builder.ToString());
            }
        }

        public void Load(string fullPath)
        {
            m_artifacts = new List<Artifact>();

            // Load the file.

            m_fullPath = fullPath;
            XmlDocument document = new XmlDocument();
            document.Load(fullPath);
            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace(Constants.Catalogue.Xml.Prefix, Constants.Catalogue.Xml.Namespace);

            string xpath = "/" + Constants.Catalogue.Xml.ConfigurationElement
                           + "/" + Constants.Catalogue.Xml.LinkMeElement
                           + "/" + Constants.Catalogue.Xml.Prefix + ":" + Constants.Catalogue.Xml.CatalogueElement;
            XmlNode catalogueNode = document.SelectSingleNode(xpath, nsManager);

            if ( catalogueNode != null )
            {
                xpath = Constants.Catalogue.Xml.Prefix + ":" + Constants.Catalogue.Xml.ArtifactsElement;
                XmlNode artifactsNode = catalogueNode.SelectSingleNode(xpath, nsManager);
                if ( artifactsNode != null )
                    LoadArtifacts(artifactsNode, nsManager);
            }
        }

        private void SaveArtifacts(XmlTextWriter writer, string outputFolder)
        {
            writer.WriteAttributeString(Constants.Catalogue.Xml.RootFolderAttribute, m_rootFolder);
            writer.WriteAttributeString(Constants.Catalogue.Xml.GuidAttribute, m_guid);

            foreach ( Artifact artifact in m_artifacts )
            {
                writer.WriteStartElement(Constants.Catalogue.Xml.ArtifactElement);
                SaveArtifact(writer, artifact);
                writer.WriteEndElement();
            }
        }

        private void SaveArtifact(XmlTextWriter writer, Artifact artifact)
        {
            writer.WriteAttributeString(Constants.Catalogue.Xml.PathAttribute, artifact.ProjectRelativePath);

            // Write out each piece of metadata.

            foreach ( string name in artifact.MetadataNames )
            {
                string value = artifact.GetMetadata(name);
                writer.WriteAttributeString(name, value);
            }

            // Write out the associated artifacts.

            if ( artifact.AssociatedArtifacts.Count > 0 )
            {
                writer.WriteStartElement(Constants.Catalogue.Xml.AssociatedArtifactsElement);
                foreach ( Artifact associatedArtifact in artifact.AssociatedArtifacts )
                {
                    writer.WriteStartElement(Constants.Catalogue.Xml.ArtifactElement);
                    SaveArtifact(writer, associatedArtifact);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        private void LoadArtifacts(XmlNode artifactsNode, XmlNamespaceManager nsManager)
        {
            // Root folder.

            XmlAttribute attribute = artifactsNode.Attributes[Constants.Catalogue.Xml.RootFolderAttribute];
            if ( attribute != null )
                m_rootFolder = attribute.Value;

            // Guid.

            attribute = artifactsNode.Attributes[Constants.Catalogue.Xml.GuidAttribute];
            if (attribute != null)
                m_guid = attribute.Value;

            // Iterate over all artifacts.

            string xpath = Constants.Catalogue.Xml.Prefix + ":" + Constants.Catalogue.Xml.ArtifactElement;
            foreach ( XmlNode artifactNode in artifactsNode.SelectNodes(xpath, nsManager) )
                LoadArtifact(null, artifactNode, nsManager);
        }

        private void LoadArtifact(Artifact parentArtifact, XmlNode artifactNode, XmlNamespaceManager nsManager)
        {
            // Path.

            string path = string.Empty;
            XmlAttribute attribute = artifactNode.Attributes[Constants.Catalogue.Xml.PathAttribute];
            if ( attribute != null )
                path = attribute.Value;

            // Create the artifact.

            Artifact artifact;
            if (parentArtifact == null)
            {
                artifact = new Artifact(path);
                Add(artifact);
            }
            else
            {
                artifact = parentArtifact.AddAssociatedArtifact(path);
            }

            // The attributes are the metadata for the artifact.

            foreach (XmlAttribute artifactAttribute in artifactNode.Attributes)
            {
                if (artifactAttribute.Name != Constants.Catalogue.Xml.PathAttribute)
                    artifact.SetMetadata(artifactAttribute.Name, artifactAttribute.Value);
            }

            // Load any associated files.

            string xpath = Constants.Catalogue.Xml.Prefix + ":" + Constants.Catalogue.Xml.AssociatedArtifactsElement
                           + "/" + Constants.Catalogue.Xml.Prefix + ":" + Constants.Catalogue.Xml.ArtifactElement;
            foreach ( XmlNode associatedArtifactNode in artifactNode.SelectNodes(xpath, nsManager) )
                LoadArtifact(artifact, associatedArtifactNode, nsManager);
        }

        private List<Artifact> m_artifacts = new List<Artifact>();
        private string m_rootFolder = string.Empty;
        private string m_guid = string.Empty;
        private string m_fullPath = string.Empty;
    }
}
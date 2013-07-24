using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace LinkMe.Environment.Build.Tasks
{
    public class Artifact
        : MarshalByRefObject
    {
        public Artifact(string projectRelativePath)
        {
            m_projectRelativePath = projectRelativePath;
        }

        public string Name
        {
            get { return Path.GetFileName(ProjectRelativePath); }
        }

        public string ProjectRelativePath
        {
            get { return m_projectRelativePath; }
        }

        public IList<Artifact> AssociatedArtifacts
        {
            get { return m_associatedArtifacts.AsReadOnly(); }
        }

        public Artifact AddAssociatedArtifact(string projectRelativePath)
        {
            Artifact associatedArtifact = new Artifact(projectRelativePath);
            m_associatedArtifacts.Add(associatedArtifact);
            return associatedArtifact;
        }

        public List<string> ReferencedFiles
        {
            get { return m_referencedFiles; }
        }

        public void AddReferencedFile(string fileName)
        {
            m_referencedFiles.Add(fileName);
        }

        public IEnumerable<string> MetadataNames
        {
            get { return m_metadata.Keys; }
        }

        public void SetMetadata(string name, string value)
        {
            m_metadata[name] = value;
        }

        public void SetMetadata(string name, bool value)
        {
            m_metadata[name] = XmlConvert.ToString(value);
        }

        public string GetMetadata(string name)
        {
            return m_metadata.ContainsKey(name) ? m_metadata[name] : null;
        }

        public bool GetBooleanMetadata(string name)
        {
            if ( !m_metadata.ContainsKey(name) )
                return false;

            try
            {
                return XmlConvert.ToBoolean(m_metadata[name]);
            }
            catch ( System.Exception )
            {
                return false;
            }
        }

        private string m_projectRelativePath;
        private Dictionary<string, string> m_metadata = new Dictionary<string,string>();
        private List<Artifact> m_associatedArtifacts = new List<Artifact>();
        private List<string> m_referencedFiles = new List<string>();
    }
}
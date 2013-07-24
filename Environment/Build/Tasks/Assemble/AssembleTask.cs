using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal enum Action
    {
        Assemble,
        CopyOnBuild
    }

    public abstract class AssembleTask
        : Task
    {
        protected AssembleTask()
        {
            m_options = new AssembleOptions();
        }

        protected AssembleOptions Options
        {
            get { return m_options; }
        }

        protected ITaskItem[] AssembleItems
        {
            get { return m_assembleItems; }
            set { m_assembleItems = value; }
        }

        protected ITaskItem[] CopyOnBuildItems
        {
            get { return m_copyOnBuildItems; }
            set { m_copyOnBuildItems = value; }
        }

        protected virtual void Initialise()
        {
            // OutputFolder should be absolute.

            m_options.OutputFolder = FilePath.GetAbsolutePath(m_options.OutputFolder, Path.GetDirectoryName(m_options.ProjectFullPath));

            // CatalogueFile should be an absolute path.

            if ( string.IsNullOrEmpty(m_options.CatalogueFile) )
                m_options.CatalogueFile = Path.GetFileNameWithoutExtension(m_options.ProjectFullPath) + Constants.File.Catalogue.Extension;
            if ( !FilePath.IsAbsolutePath(m_options.CatalogueFile) )
                m_options.CatalogueFile = FilePath.GetAbsolutePath(m_options.CatalogueFile, m_options.OutputFolder);
            if (!string.Equals(Path.GetExtension(m_options.CatalogueFile), Constants.File.Catalogue.Extension, System.StringComparison.OrdinalIgnoreCase))
                m_options.CatalogueFile += Constants.File.Catalogue.Extension;

            // Configuration should not be null.

            if ( m_options.Configuration == null )
                m_options.Configuration = string.Empty;
        }

        protected virtual void Finalise()
        {
        }

        protected string GetSourceFullPath(string sourcePath)
        {
            string sourceFullPath = FilePath.GetAbsolutePath(sourcePath, Path.GetDirectoryName(Options.ProjectFullPath));
            return AssembleFile.ConvertToAdjustedPath(sourceFullPath, Options.Configuration);
        }

        protected string GetDestinationFullPath(Artifact artifact)
        {
            return Path.Combine(Options.OutputFolder, artifact.ProjectRelativePath);
        }

        protected Artifact CreateArtifact(ITaskItem item)
        {
            return CreateArtifact(item.ItemSpec, item.GetMetadata(Constants.Project.Item.Link), item.CloneCustomMetadata());
        }

        protected Artifact CreateArtifact(string sourcePath, string projectRelativePath, IDictionary metadata)
        {
            var artifact = new Artifact(projectRelativePath);

            // Set metadata appropriately.

            foreach (DictionaryEntry entry in metadata)
            {
                switch (entry.Key as string)
                {
                    case Constants.Project.Item.Assemble.InstallInGac.Name:
                        artifact.SetMetadata(Constants.Catalogue.Artifact.InstallInGac, System.Convert.ToBoolean(entry.Value as string));
                        break;

                    case Constants.Project.Item.Assemble.GacGuid.Name:
                        artifact.SetMetadata(Constants.Catalogue.Artifact.GacGuid, entry.Value as string);
                        break;

                    case Constants.Project.Item.Assemble.Guid.Name:
                        artifact.SetMetadata(Constants.Catalogue.Artifact.Guid, entry.Value as string);
                        break;

                    case Constants.Project.Item.Assemble.ShortcutName.Name:
                        artifact.SetMetadata(Constants.Catalogue.Artifact.ShortcutName, entry.Value as string);
                        break;

                    case Constants.Project.Item.Assemble.ShortcutPath.Name:
                        artifact.SetMetadata(Constants.Catalogue.Artifact.ShortcutPath, entry.Value as string);
                        break;

                    case Constants.Project.Item.Link:

                        // Handled above.

                        break;

                    default:
                        artifact.SetMetadata(entry.Key as string, entry.Value as string);
                        break;
                }
            }

            // Get the referenced assemblies.

            List<string> assemblies = NetUtil.GetReferencedAssemblies(GetSourceFullPath(sourcePath));
            if (assemblies != null)
            {
                foreach (string assembly in assemblies)
                    artifact.AddReferencedFile(assembly + Constants.File.Dll.Extension);
            }

            return artifact;
        }

        protected LinkedList<KeyValuePair<string, Artifact>> CreateAssembleArtifacts(bool ascendingReferences)
        {
            return CreateArtifacts(ascendingReferences, true);
        }

        protected LinkedList<KeyValuePair<string, Artifact>> CreateCopyOnBuildArtifacts(bool ascendingReferences)
        {
            return CreateArtifacts(ascendingReferences, false);
        }

        private LinkedList<KeyValuePair<string, Artifact>> CreateArtifacts(bool ascendingReferences, bool useAssembleItems)
        {
            // Create all artifacts for all sources.

            Dictionary<string, Artifact> artifacts = new Dictionary<string, Artifact>();
            ITaskItem[] items = useAssembleItems ? AssembleItems : CopyOnBuildItems;
            if ( items != null )
            {
                foreach ( ITaskItem item in items )
                {
                    // Create a new artifact for this source.

                    Artifact artifact = CreateArtifact(item);
                    artifacts.Add(item.ItemSpec, artifact);
                }
            }

            return ascendingReferences ? CreateArtifactsAscending(artifacts) : CreateArtifactsDescending(artifacts);
        }

        protected LinkedList<KeyValuePair<string, Artifact>> CreateArtifactsAscending(Dictionary<string, Artifact> artifacts)
        {
            var dependencies = CreateDependencies(artifacts);

            // Create a sorted list now that all artifacts have been created.

            LinkedList<KeyValuePair<string, Artifact>> sortedArtifacts = new LinkedList<KeyValuePair<string, Artifact>>();

            foreach ( KeyValuePair<string, Artifact> newPair in artifacts )
            {
                bool inserted = false;

                LinkedListNode<KeyValuePair<string, Artifact>> node = sortedArtifacts.First;
                while ( node != null )
                {
                    KeyValuePair<string, Artifact> pair = node.Value;
                    if ( IsReference(newPair.Value, pair.Value, dependencies) )
                    {
                        sortedArtifacts.AddBefore(node, newPair);
                        inserted = true;
                        break;
                    }

                    node = node.Next;
                }

                if ( !inserted )
                    sortedArtifacts.AddLast(newPair);
            }

            return sortedArtifacts;
        }

        private LinkedList<KeyValuePair<string, Artifact>> CreateArtifactsDescending(Dictionary<string, Artifact> artifacts)
        {
            var dependencies = CreateDependencies(artifacts);

            // Create a sorted list now that all artifacts have been created.

            LinkedList<KeyValuePair<string, Artifact>> sortedArtifacts = new LinkedList<KeyValuePair<string, Artifact>>();

            foreach ( KeyValuePair<string, Artifact> newPair in artifacts )
            {
                bool inserted = false;

                LinkedListNode<KeyValuePair<string, Artifact>> node = sortedArtifacts.Last;
                while ( node != null )
                {
                    KeyValuePair<string, Artifact> pair = node.Value;
                    if ( IsReference(newPair.Value, pair.Value, dependencies) )
                    {
                        sortedArtifacts.AddAfter(node, newPair);
                        inserted = true;
                        break;
                    }

                    node = node.Previous;
                }

                if ( !inserted )
                    sortedArtifacts.AddFirst(newPair);
            }

            return sortedArtifacts;
        }

        private static Dictionary<string, List<string>> CreateDependencies(Dictionary<string, Artifact> artifacts)
        {
            var dependencies = new Dictionary<string, List<string>>();

            foreach (var artifact in artifacts.Values)
            {
                var dependencyNames = new List<string>();
                CreateDependencies(artifact, dependencyNames, artifacts);
                dependencies[artifact.Name] = dependencyNames;
            }

            return dependencies;
        }

        private static void CreateDependencies(Artifact artifact, List<string> dependencyNames, Dictionary<string, Artifact> artifacts)
        {
            foreach (var reference in artifact.ReferencedFiles)
            {
                // Check whether it has been done already.

                if (!dependencyNames.Contains(reference))
                {
                    // Add it.

                    dependencyNames.Add(reference);

                    // Find the artifact.

                    Artifact referenceArtifact = (from a in artifacts.Values where a.Name == reference select a).SingleOrDefault();
                    if (referenceArtifact != null)
                        CreateDependencies(referenceArtifact, dependencyNames, artifacts);
                }
            }
        }

        private bool IsReference(Artifact artifact1, Artifact artifact2, Dictionary<string, List<string>> dependencies)
        {
            List<string> dependencyList;
            if (!dependencies.TryGetValue(artifact2.Name, out dependencyList))
                return false;

            return dependencyList.Contains(artifact1.Name);

/*			// Check for direct references.

			foreach ( string reference in artifact2.ReferencedFiles )
			{
				if ( artifact1.Name == reference )
					return true;
			}

			// Now need to determine indirect references.

			foreach ( string reference in artifact2.ReferencedFiles )
			{
				// Try to find the artifact.

				foreach ( Artifact artifact in artifacts.Values )
				{
					if ( artifact.Name == reference )
					{
						if ( IsReference(artifact1, artifact, artifacts) )
							return true;
					}
				}
			}

			return false;
*/
        }

        private ITaskItem[] m_assembleItems;
        private ITaskItem[] m_copyOnBuildItems;
        private AssembleOptions m_options;
    }
}
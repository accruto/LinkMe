using System.IO;
using System.Security;

using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Build.BuildEngine;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	internal class ConfigurationReferenceContainerNode
		:	ReferenceContainerNode,
			IReferenceContainer
	{
		public ConfigurationReferenceContainerNode(ProjectNode root)
			:	base(root)
		{
		}

		protected override ReferenceNode CreateFileComponent(VSCOMPONENTSELECTORDATA selectorData)
		{
			if ( string.Compare(Path.GetExtension(selectorData.bstrFile), ".config", true) == 0 )
				return CreateConfigReferenceNode(selectorData);
			else
				return base.CreateFileComponent(selectorData);
		}

		protected override AssemblyReferenceNode CreateAssemblyReferenceNode(string fileName)
		{
			AssemblyReferenceNode node = null;
			try
			{
				node = new ConfigAssemblyReferenceNode(ProjectMgr, fileName);
			}
			catch ( System.ArgumentNullException )
			{
			}
			catch ( FileNotFoundException )
			{
			}
			catch ( System.BadImageFormatException )
			{
			}
			catch ( FileLoadException )
			{
			}
			catch ( SecurityException )
			{
			}

			return node;
		}

		protected ReferenceNode CreateConfigReferenceNode(VSCOMPONENTSELECTORDATA selectorData)
		{
			return new ConfigReferenceNode(ProjectMgr, selectorData);
		}

		protected ReferenceNode CreateConfigReferenceNode(ProjectElement element)
		{
			return new ConfigReferenceNode(ProjectMgr, element);
		}

		void IReferenceContainer.LoadReferencesFromBuildProject(Project buildProject)
		{
			// Look explicitly for config references.
			
			foreach ( BuildItem item in buildProject.GetEvaluatedItemsByName("ConfigReference") )
			{
				ProjectElement element = new ProjectElement(ProjectMgr, item, false);
				AddChild(CreateConfigReferenceNode(element));
			}

			// Let the base process all other references.

			base.LoadReferencesFromBuildProject(buildProject);
		}
	}
}

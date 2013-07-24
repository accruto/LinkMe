using System.IO;

using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell.Interop;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	public class ConfigReferenceNode
		:	ReferenceNode
	{
		private string m_name;
		private string m_hintPath;

		public ConfigReferenceNode(ProjectNode root, ProjectElement e)
			: base(root, e)
		{
			m_name = ItemNode.GetMetadata(ProjectFileConstants.Include);
			m_hintPath = ItemNode.GetMetadata("HintPath");
		}

		public ConfigReferenceNode(ProjectNode root, VSCOMPONENTSELECTORDATA selectorData)
			: base(root)
		{
			// Initialize private state.

			m_name = System.IO.Path.GetFileNameWithoutExtension(selectorData.bstrTitle);
			m_hintPath = FileSystem.GetRelativePath(selectorData.bstrFile, root.BaseURI.Directory);
		}

		public override string Caption
		{
			get { return m_name; }
		}

		public override string Url
		{
			get { return FileSystem.GetAbsolutePath(m_hintPath, ProjectMgr.BaseURI.Directory); }
		}

		protected override void BindReferenceData()
		{
			if ( ItemNode == null || ItemNode.Item == null )
				ItemNode = CreateProjectElement();
		}
		
		protected override bool IsAlreadyAdded()
		{
			ReferenceContainerNode referencesFolder = ProjectMgr.FindChild(ReferenceContainerNode.ReferencesNodeVirtualName) as ReferenceContainerNode;

			for ( HierarchyNode node = referencesFolder.FirstChild; node != null; node = node.NextSibling )
			{
				if ( node is ConfigReferenceNode )
				{
					ConfigReferenceNode referenceNode = node as ConfigReferenceNode;
					
					// Check if the name is the same.

					if ( string.Compare(referenceNode.Caption, Caption, System.StringComparison.OrdinalIgnoreCase) == 0 )
						return true;
				}
			}
			
			return false;
		}
		
		protected override bool CanShowDefaultIcon()
		{
			// Check whether the file actually exists or not.

			return File.Exists(FileSystem.GetAbsolutePath(m_hintPath, ProjectMgr.BaseURI.Directory));
		}
		
		private ProjectElement CreateProjectElement()
		{
			ProjectElement element = new ProjectElement(ProjectMgr, m_name, "ConfigReference");
			element.SetMetadata("HintPath", m_hintPath);
			return element;
		}
	}
}

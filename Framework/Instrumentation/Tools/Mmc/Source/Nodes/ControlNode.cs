using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Configuration.Management;
using LinkMe.Framework.Instrumentation.Management;

using Catalogue = LinkMe.Framework.Instrumentation.Management.Catalogue;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	public abstract class ControlNode : LinkMe.Framework.Tools.Mmc.OcxNode
	{
		protected ControlNode(Snapin snapin, System.Guid controlTypeID)
			:	base(snapin, controlTypeID, false)
		{
		}

		protected void AddExportMenuItems(object element, Catalogue catalogue)
		{
			AddExportMenuItems(new object[] { element }, catalogue);
		}

		protected void AddExportMenuItems(object[] elements, Catalogue catalogue)
		{
			ContextSubMenuItem subMenuItem = new ContextSubMenuItem("Export To");
			AddTaskMenuItem(subMenuItem);

			// Look for RepositoryWriters in the environment.

			Module module = ConfigurationManager.GetCatalogue(catalogue.EnvironmentContext).Modules[Constants.Module.Name];
			if ( module != null )
			{
				foreach ( RepositoryType repositoryType in module.RepositoryTypes )
				{
					if ( Node.IsWriter(repositoryType) )
						AddMenuItem(subMenuItem, new ContextMenuItem(repositoryType.RepositoryDisplayName, string.Empty, new MenuCommandHandler((new ExportRepositoryHandler(repositoryType, elements, catalogue, Snapin.GetMainWindow())).Export)));
				}
			}
		}

		protected void ApplyProperties(Namespace ns, Namespace newNamespace)
		{
			// Copy properties and commit the changes.

			ns.CopyProperties(newNamespace);
			ns.Commit();
		}

		protected void ApplyProperties(Source source, Source newSource)
		{
			// Copy properties and commit the changes.

			source.CopyProperties(newSource);
			source.Commit();
		}
	}
}

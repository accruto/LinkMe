using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.ObjectProperties;
using LinkMe.Framework.Configuration.Management;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

using ICatalogueElement = LinkMe.Framework.Instrumentation.Management.ICatalogueElement;
using Catalogue = LinkMe.Framework.Instrumentation.Management.Catalogue;
using Module = LinkMe.Framework.Configuration.Management.Module;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	public abstract class Node
		:	ResultNode
	{
		protected Node(Snapin snapin)
			:	base(snapin)
		{
		}

		protected void AddImportMenuItems(ICatalogueElement element)
		{
			// Create the Read menu.

			var subMenuItem = new ContextSubMenuItem("Import From");
			AddTaskMenuItem(subMenuItem);

			// Look for RepositoryReaders in the environment.

			Module module = ConfigurationManager.GetCatalogue().Modules[Constants.Module.Name];
			if ( module != null )
			{
				foreach ( RepositoryType repositoryType in module.RepositoryTypes )
				{
					if ( IsReader(repositoryType) )
					{
						var handler = new ImportRepositoryHandler(repositoryType, element.Catalogue.RepositoryLink, Snapin.GetMainWindow());
						AddMenuItem(subMenuItem, new ContextMenuItem(repositoryType.RepositoryDisplayName, string.Empty, new MenuCommandHandler(handler.Import)));
					}
				}
			}
		}

		protected void AddExportMenuItems(object element, Catalogue catalogue)
		{
			AddExportMenuItems(new[] { element }, catalogue);
		}

		protected void AddExportMenuItems(object[] elements, Catalogue catalogue)
		{
			var subMenuItem = new ContextSubMenuItem("Export To");
			AddTaskMenuItem(subMenuItem);

			// Look for RepositoryWriters in the environment.

			Module module = ConfigurationManager.GetCatalogue(catalogue.EnvironmentContext).Modules[Constants.Module.Name];
			if ( module != null )
			{
				foreach ( RepositoryType repositoryType in module.RepositoryTypes )
				{
					if ( IsWriter(repositoryType) )
						AddMenuItem(subMenuItem, new ContextMenuItem(repositoryType.RepositoryDisplayName, string.Empty, new MenuCommandHandler((new ExportRepositoryHandler(repositoryType, elements, catalogue, Snapin.GetMainWindow())).Export)));
				}
			}
		}

        public static bool IsWriter(RepositoryType repositoryType)
        {
            return repositoryType.Class.SupportsInterface<IConnectionFactory<IRepositoryWriter>>();
        }

        public static bool IsReader(RepositoryType repositoryType)
        {
            return repositoryType.Class.SupportsInterface<IConnectionFactory<IRepositoryReader>>();
        }

		protected void ApplyProperties(EventType eventType, EventType newEventType)
		{
			// Copy properties and commit the changes.

			eventType.CopyProperties(newEventType);
			eventType.Commit();
		}

		protected void ManageTypes()
		{
			// Create a property sheet on a separate thread if it doesn't already exist.

			if ( _manageTypesForm == null )
			{
				var thread = new Thread(ManageTypesFormThread);
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			else
			{
				_manageTypesForm.Activate();
			}
		}

		private void ManageTypesFormThread()
		{
			Application.ThreadException += Application_ThreadException;

			_manageTypesForm = CreateManageTypesForm();
			if ( _manageTypesForm != null )
			{
				_manageTypesForm.Apply += ApplyManageTypesHandler;
				_manageTypesForm.ShowDialog();
				_manageTypesForm = null;
			}
		}

		private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			var dialog = new ExceptionDialog(e.Exception, "The following exception has occurred in the snap-in:");
			dialog.ShowDialog(Snapin);
		}

		protected virtual ObjectPropertyForm CreateManageTypesForm()
		{
			Debug.Fail("CreateManageTypesForm was called, but is not overridden in the derived class, '" + GetType().FullName + "'.");
			return null;
		}

		private void ApplyManageTypesHandler(object sender, ApplyEventArgs args)
		{
			ApplyManageTypes(args.Object);
		}

		protected virtual void ApplyManageTypes(object obj)
		{
			Debug.Fail("ApplyManageTypes was called, but is not overridden in the derived class, '" + GetType().FullName + "'.");
		}

		protected void ApplyManageTypes(Catalogue catalogue, Catalogue newCatalogue)
		{
			// Copy all repository and store types.

			catalogue.Commit();
		}

		private ObjectPropertyForm _manageTypesForm;
	}
}
